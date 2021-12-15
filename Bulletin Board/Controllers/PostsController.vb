
Imports System
Imports System.String
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Data.SqlClient
Imports System.Net
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports BulletinBoard_OJT.DB_Entity
Imports EntityState = System.Data.EntityState
Imports PagedList
Imports System.IO
Imports BulletinBoard_OJT.Infrastructure
Imports BulletinBoard_OJT.Utility

Namespace Controllers
    <CustomAuthenticationFilter>
    Public Class PostsController
        Inherits System.Web.Mvc.Controller
        Private log As Logger = New Logger()
        Private db As New BulletinBoardEntities

        ' GET: Posts
        <CustomAuthorize({"Administrator", "User", "Vistior"})>
        Function Index(ByVal sortOrder As String, currentFilter As String, searchString As String, page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder
            ViewBag.NameSortParm = If(String.IsNullOrEmpty(sortOrder), "name_desc", String.Empty)
            ViewBag.DateSortParm = If(sortOrder = "Date", "date_desc", "Date")
            If Not searchString Is Nothing Then
                page = 1
            Else
                searchString = currentFilter
            End If

            ViewBag.CurrentFilter = searchString

            Dim userId = Session("CurrentUser")
            Dim user As Users = db.Users.Find(userId)

            Dim roles As Roles = db.Roles.Find(user.Roles_Id)

            If roles.Type = "User" Then
                Dim posts = From p In db.Posts Select p Where p.Created_User_Id = userId And p.Status = "1"

                If Not String.IsNullOrEmpty(searchString) Then
                    posts = posts.Where(Function(p) p.Title.ToUpper().Contains(searchString.ToUpper()) _
                                          Or p.Description.ToUpper().Contains(searchString.ToUpper()))

                End If

                Select Case sortOrder
                    Case "name_desc"
                        posts = posts.OrderByDescending(Function(p) p.Title)
                    Case "Date"
                        posts = posts.OrderBy(Function(p) p.Created_At)
                    Case "date_desc"
                        posts = posts.OrderByDescending(Function(p) p.Created_At)
                    Case Else
                        posts = posts.OrderBy(Function(p) p.Title)
                End Select

                Dim pageSize As Integer = 5
                Dim pageNumber As Integer = If(page, 1)
                Return View(posts.ToPagedList(pageNumber, pageSize))
            Else
                Dim posts = From p In db.Posts Select p Where p.Status = "1"

                If Not String.IsNullOrEmpty(searchString) Then
                    posts = posts.Where(Function(p) p.Title.ToUpper().Contains(searchString.ToUpper()) _
                                          Or p.Description.ToUpper().Contains(searchString.ToUpper()))

                End If

                Select Case sortOrder
                    Case "name_desc"
                        posts = posts.OrderByDescending(Function(p) p.Title)
                    Case "Date"
                        posts = posts.OrderBy(Function(p) p.Created_At)
                    Case "date_desc"
                        posts = posts.OrderByDescending(Function(p) p.Created_At)
                    Case Else
                        posts = posts.OrderBy(Function(p) p.Title)
                End Select

                Dim pageSize As Integer = 5
                Dim pageNumber As Integer = If(page, 1)
                Return View(posts.ToPagedList(pageNumber, pageSize))
            End If


        End Function
        ' GET: Posts/Details/5

        <AllowAnonymous>
        Function Details(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim posts As Posts = db.Posts.Find(id)
            If IsNothing(posts) Then
                Return HttpNotFound()
            End If
            Return View(posts)
        End Function

        ' GET: Posts/Create
        <CustomAuthorize({"Administrator", "User"})>
        Function Create() As ActionResult
            ViewBag.Updated_User_Id = New SelectList(db.Users, "Id", "Name")
            ViewBag.Created_User_Id = New SelectList(db.Users, "Id", "Name")
            Return View()
        End Function

        ' POST: Posts/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <CustomAuthorize({"Administrator", "User"})>
        Function Create(<Bind(Include:="Id,Title,Description,Status,Created_At,Updated_User_Id,Created_User_Id,Updated_At")> ByVal posts As Posts) As ActionResult
            Try
                If ModelState.IsValid Then
                    posts.Id = Guid.NewGuid()
                    Dim user_id = Session("CurrentUser")
                    posts.Created_User_Id = user_id
                    posts.Updated_User_Id = user_id

                    posts.Created_At = DateTime.Now
                    posts.Updated_At = DateTime.Now
                    db.Posts.Add(posts)
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                    log.WriteLog(Logger.LogType.Info, "PostsController", "Create", "Post Create Successful")
                End If
                ViewBag.Updated_User_Id = New SelectList(db.Users, "Id", "Name", posts.Updated_User_Id)
                ViewBag.Created_User_Id = New SelectList(db.Users, "Id", "Name", posts.Created_User_Id)
                Return View(posts)

            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "PostsController", "Create", detailErrorLog)

            End Try
        End Function

        ' GET: Posts/Edit/5
        <CustomAuthorize({"Administrator", "User"})>
        Function Edit(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim posts As Posts = db.Posts.Find(id)
            If IsNothing(posts) Then
                Return HttpNotFound()
            End If
            ViewBag.Updated_User_Id = New SelectList(db.Users, "Id", "Name", posts.Updated_User_Id)
            ViewBag.Created_User_Id = New SelectList(db.Users, "Id", "Name", posts.Created_User_Id)
            Return View(posts)
        End Function

        ' POST: Posts/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        <CustomAuthorize({"Administrator", "User"})>
        Function Edit(ByVal posts As Posts) As ActionResult
            Try
                If ModelState.IsValid Then
                    posts.Updated_At = DateTime.Now()
                    Dim user_id = Session("CurrentUser")
                    posts.Updated_User_Id = user_id

                    db.Entry(posts).State = Data.EntityState.Modified
                    db.SaveChanges()
                    Return RedirectToAction("Index")
                    log.WriteLog(Logger.LogType.Info, "PostsController", "Edit", "Post Update Successful")
                End If
                ViewBag.Updated_User_Id = New SelectList(db.Users, "Id", "Name", posts.Updated_User_Id)
                ViewBag.Created_User_Id = New SelectList(db.Users, "Id", "Name", posts.Created_User_Id)
                Return View(posts)
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "PostsController", "Edit", detailErrorLog)

            End Try
        End Function

        ' GET: Posts/Delete/5
        <CustomAuthorize({"Administrator", "User"})>
        Function Delete(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim posts As Posts = db.Posts.Find(id)
            If IsNothing(posts) Then
                Return HttpNotFound()
            End If
            Return View(posts)
        End Function

        ' POST: Posts/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Guid) As ActionResult
            Try
                Dim posts As Posts = db.Posts.Find(id)

                posts.Status = 0
                db.SaveChanges()
                log.WriteLog(Logger.LogType.Info, "PostsController", "Delete", "Post Delete Successful")
                Return RedirectToAction("Index")
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "PostsController", "Delete", detailErrorLog)

            End Try
        End Function


        ' GET: Posts/ImportCSV
        <CustomAuthorize({"Administrator", "User"})>
        Function ImportCSV() As ActionResult
            Return View()
        End Function
        ' POST: Posts/ImportCSV
        <HttpPost()>
        Function ImportCSV(ByVal postedFile As HttpPostedFileBase) As ActionResult
            Try
                Dim filePath As String = String.Empty
                Dim currentUserId = Session("CurrentUser")
                Dim time = DateTime.Now

                If postedFile IsNot Nothing Then
                    Dim path As String = Server.MapPath("~/Uploads/")

                    If Not Directory.Exists(path) Then
                        Directory.CreateDirectory(path)
                    End If

                    filePath = path & System.IO.Path.GetFileName(postedFile.FileName)
                    Dim extension As String = System.IO.Path.GetExtension(postedFile.FileName)
                    If extension.EndsWith(".csv") Then


                        postedFile.SaveAs(filePath)

                        Dim csvData As String = System.IO.File.ReadAllText(filePath)

                        Dim result As String() = csvData.Split(New String() {vbLf}, StringSplitOptions.None)
                        Dim valuesCount1 = result.Count()
                        Console.Write(valuesCount1)
                        Dim posts As Posts = db.Posts.Create
                        For Each row As String In result

                            If Not String.IsNullOrEmpty(row) Then
                                Dim i As Integer = 0
                                Dim title, description, status


                                For Each cell As String In row.Split(New String() {","}, StringSplitOptions.None)

                                    If i = 0 Then
                                        title = cell
                                        posts.Title = title
                                    ElseIf i = 1 Then
                                        description = cell
                                        posts.Description = description
                                    ElseIf i = 2 Then
                                        status = cell
                                        posts.Status = status.ToString().Trim
                                    End If
                                    i += 1
                                Next

                                posts.Created_At = time
                                posts.Updated_User_Id = currentUserId
                                posts.Created_User_Id = currentUserId
                                posts.Updated_At = time
                                db.Posts.Add(posts)
                                db.SaveChanges()
                                log.WriteLog(Logger.LogType.Info, "PostsController", "ImportCSV", "CSV Upload Successful")
                            End If

                        Next


                    Else
                        ViewData("Message") = "Please choose only csv file!"
                        Return View()
                    End If
                Else
                    ViewData("Message") = "Please choose only csv file!"
                    Return View()

                End If
                Return RedirectToAction("Index")
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "PostsController", "ImportCSV", detailErrorLog)

            End Try
        End Function
        Private Function GetData(ByVal cmd As SqlCommand) As DataTable
            Dim dt As New DataTable()
            Dim strConnString As [String] = ConfigurationManager.ConnectionStrings("conString").ConnectionString
            Dim con As New SqlConnection(strConnString)
            Dim sda As New SqlDataAdapter()
            cmd.CommandType = CommandType.Text
            cmd.Connection = con
            Try
                con.Open()
                sda.SelectCommand = cmd
                sda.Fill(dt)
                Return dt
            Catch ex As Exception
                Throw ex
            Finally
                con.Close()
                sda.Dispose()
                con.Dispose()
            End Try
        End Function

        <CustomAuthorize({"Administrator", "User"})>
        Function ExportToCSV(ByVal sender As Object, ByVal e As EventArgs)
            Try
                'Get the data from database into datatable 
                Dim strQuery As String = "select Posts.Id As ID, Posts.Title As TITLE, Posts.Description As DESCRIPTION, Posts.Created_At As POSTED_DATE, Users.Name As POSTED_USER from Posts, Users
                Where Posts.Created_User_Id = Users.Id"
                Dim cmd As New SqlCommand(strQuery)
                Dim dt As DataTable = GetData(cmd)

                Response.Clear()
                Response.Buffer = True
                Response.AddHeader("content-disposition", "attachment;filename=ExportCSV.csv")
                Response.Charset = ""
                Response.ContentType = "application/text"


                Dim sb As New StringBuilder()
                For k As Integer = 0 To dt.Columns.Count - 1
                    'add separator 
                    sb.Append(dt.Columns(k).ColumnName + ","c)
                Next
                'append new line 
                sb.Append(vbCr & vbLf)
                For i As Integer = 0 To dt.Rows.Count - 1
                    For k As Integer = 0 To dt.Columns.Count - 1
                        'add separator 
                        sb.Append(dt.Rows(i)(k).ToString().Replace(",", ";") + ","c)
                    Next
                    'append new line 
                    sb.Append(vbCr & vbLf)
                Next
                Response.Output.Write(sb.ToString())
                Response.Flush()
                Response.End()
                log.WriteLog(Logger.LogType.Info, "PostsController", "ExportToCSV", "CSV Download Successful")
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
            Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
            Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
            Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
            Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "PostsController", "ExportToCSV", detailErrorLog)

            End Try
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

    End Class
End Namespace
