
Imports System.IO
Imports System.Net

Imports BulletinBoard_OJT
Imports BulletinBoard_OJT.DB_Entity
Imports BulletinBoard_OJT.Infrastructure
Imports BulletinBoard_OJT.Utility
Imports PagedList
Imports EntityState = System.Data.EntityState

Namespace Controllers
    <CustomAuthenticationFilter>
    Public Class UsersController
        Inherits System.Web.Mvc.Controller
        Private log As Logger = New Logger()
        Private db As New BulletinBoardEntities

        ' GET: Users
        <CustomAuthorize({"Administrator"})>
        Function Index(ByVal sortOrder As String, currentFilter As String, searchName As String, searchEmail As String, searchStart As Nullable(Of Date), SearchEnd As Nullable(Of Date), page As Integer?) As ActionResult
            ViewBag.CurrentSort = sortOrder
            ViewBag.NameSortParm = If(String.IsNullOrEmpty(sortOrder), "name_desc", String.Empty)
            ViewBag.DateSortParm = If(sortOrder = "Date", "date_desc", "Date")

            If Not searchName Is Nothing And searchEmail Is Nothing Then
                page = 1
            ElseIf searchName Is Nothing Then
                searchName = currentFilter
            Else
                searchEmail = currentFilter
            End If

            If Not searchName Is Nothing Then
                ViewBag.CurrentFilter = searchName
            Else
                ViewBag.CurrentFilter = searchEmail
            End If


            Dim users = From u In db.Users Select u Where u.Is_Deleted = "active" Order By u.Created_At


            If Not String.IsNullOrEmpty(searchName) Then
                users = users.Where(Function(u) u.Name.Contains(searchName))
            End If

            If Not String.IsNullOrEmpty(searchEmail) Then
                users = users.Where(Function(u) u.Email.Contains(searchEmail))
            End If

            If (searchStart IsNot Nothing And SearchEnd IsNot Nothing) Then

                users = users.Where(Function(u) u.Created_At >= searchStart And u.Created_At <= SearchEnd)
            End If

            Select Case sortOrder
                Case "name_desc"
                    users = users.OrderByDescending(Function(u) u.Name)
                Case "Date"
                    users = users.OrderBy(Function(u) u.Created_At)
                Case "date_desc"
                    users = users.OrderByDescending(Function(u) u.Created_At)
                Case Else
                    users = users.OrderBy(Function(u) u.Name)
            End Select


            Dim pageSize As Integer = 5
            Dim pageNumber As Integer = If(page, 1)
            Return View(users.ToPagedList(pageNumber, pageSize))



        End Function

        ' GET: Users/Details/5
        <CustomAuthorize({"Administrator", "User", "Visitor"})>
        Function Details(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                id = Session("CurrentUser")
                Dim userslt As Users = db.Users.Find(id)

            End If
            Dim users As Users = db.Users.Find(id)
            If IsNothing(users) Then
                Return HttpNotFound()
            End If
            Return View(users)
        End Function

        ' GET: Users/Create
        <CustomAuthorize({"Administrator"})>
        Function Create() As ActionResult
            ViewBag.Roles_Id = New SelectList(db.Roles, "Id", "Type")

            Return View()
        End Function

        ' POST: Users/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(ByVal users As Users) As ActionResult
            Try
                Dim sha1Pswd As String = String.Empty
                Dim currentUserId = Session("CurrentUser")
                If Not String.IsNullOrEmpty(users.Password) Then
                    sha1Pswd = Utility.GetMd5HashBase64(users.Password)
                End If
                If Not String.IsNullOrEmpty(users.ConfirmPassowrd) Then
                    users.ConfirmPassowrd = Utility.GetMd5HashBase64(users.ConfirmPassowrd)
                End If

                If ModelState.IsValid Then

                    If users.ImageFile Is Nothing Then
                        ViewData("Message") = "Please choose image file!"
                        ViewBag.Roles_Id = New SelectList(db.Roles, "Id", "Type", users.Roles_Id)

                        Return View(users)
                    End If
                    Dim postedFile As HttpPostedFileBase = Request.Files(0)
                    Dim user = db.Users.FirstOrDefault(Function(model) model.Email = users.Email)

                    'Dim FileName As String = postedFile.FileName
                    Dim Name = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName)
                    Dim extension As String = System.IO.Path.GetExtension(postedFile.FileName)
                    Name = Name + "_" + DateTime.Now.ToString("yyyy-dd-MMM--hh-") + extension
                    users.Profile = Name
                    users.Password = sha1Pswd
                    users.Created_At = DateTime.Now
                    users.Updated_At = DateTime.Now
                    users.Created_User_Id = currentUserId
                    users.Updated_User_Id = currentUserId

                    If user IsNot Nothing Then

                        ViewData("Email") = "Email already exists"
                        ViewBag.Roles_Id = New SelectList(db.Roles, "Id", "Type", users.Roles_Id)
                        Return View(users)
                    Else
                        db.Users.Add(users)


                        db.SaveChanges()
                        log.WriteLog(Logger.LogType.Info, "UsersController", "Create", "Create User Successful")

                        Dim newUser As Guid = users.Id
                        Dim path As String = Server.MapPath("~/Uploads/Images/")
                        Console.Write(newUser)
                        Dim newPath = System.IO.Path.Combine(path, newUser.ToString)
                        Dim oldpath As String = Server.MapPath("~/Uploads/Images/NewUserTemp/")
                        System.IO.Directory.Move(oldpath, newPath)

                        If (System.IO.File.Exists(oldpath)) Then
                            System.IO.File.Delete(oldpath)
                        End If

                        Return RedirectToAction("Index")
                    End If

                Else

                    If users.ImageFile Is Nothing Then
                        ViewData("Message") = "Please choose image file!"

                    End If
                    ViewBag.Roles_Id = New SelectList(db.Roles, "Id", "Type", users.Roles_Id)

                    Return View(users)

                End If
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "UsersController", "Create", detailErrorLog)

            End Try
        End Function
        <HttpPost()>
        Function ImageUpload(ByVal users As Users) As ActionResult

            Try
                Dim postedFile As HttpPostedFileBase = Request.Files(0)
                If postedFile IsNot Nothing Then
                    Dim filePath As String = String.Empty
                    Dim path As String = Server.MapPath("~/Uploads/Images/NewUserTemp/")

                    If Not Directory.Exists(path) Then
                        Directory.CreateDirectory(path)
                    End If
                    Dim Name = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName)
                    Dim extension As String = System.IO.Path.GetExtension(postedFile.FileName)
                    Name = Name + "_" + DateTime.Now.ToString("yyyy-dd-MMM--hh-") + extension
                    filePath = path & Name
                    Dim FileName As String = postedFile.FileName
                    postedFile.SaveAs(filePath)
                    log.WriteLog(Logger.LogType.Info, "UsersController", "ImageUpload", "ImageUpload Successful")
                    Return Content(Name)
                End If
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "UsersController", "ImageUpload", detailErrorLog)

            End Try
        End Function

        ' GET: Users/Edit/5
        <CustomAuthorize({"Administrator"})>
        Function Edit(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim users As Users = db.Users.Find(id)
            users.ConfirmPassowrd = users.Password
            Dim uploadPath As String = System.IO.Path.Combine(Server.MapPath("~/Uploads/Images/NewUserTemp/"), id.ToString() & "\")
            If Directory.Exists(uploadPath) Then
                System.IO.Directory.Delete(uploadPath, True)

            End If
            If IsNothing(users) Then
                Return HttpNotFound()
            End If
            ViewBag.Roles_Id = New SelectList(db.Roles, "Id", "Type", users.Roles_Id)

            Return View(users)
        End Function

        ' POST: Users/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(ByVal users As Users) As ActionResult
            Try
                If ModelState.IsValid Then
                    Dim currentUserId = Session("CurrentUser")
                    users.Updated_At = DateTime.Now
                    users.Updated_User_Id = currentUserId
                    db.Entry(users).State = EntityState.Modified

                    Dim uploadPath As String = System.IO.Path.Combine(Server.MapPath("~/Uploads/Images/NewUserTemp/"), users.Id.ToString() & "\")
                    If Directory.Exists(uploadPath) Then
                        Dim files = Directory.EnumerateFiles(uploadPath, "*.*", SearchOption.AllDirectories).Where(Function(s) s.EndsWith(".png") OrElse s.EndsWith(".jpg"))
                        If files.Any() Then
                            Dim fullpath = files.FirstOrDefault()
                            ' Dim dirName = fullpath.Split(New String() {"/"}, StringSplitOptions.None).Last



                            Dim newUser As Guid = users.Id
                            Dim path As String = Server.MapPath("~/Uploads/Images/")
                            Dim destPath = System.IO.Path.Combine(path, newUser.ToString)

                            If Not Directory.Exists(destPath) Then
                                Directory.CreateDirectory(destPath)
                            End If

                            Dim oldFiles = Directory.EnumerateFiles(destPath, "*.*", SearchOption.AllDirectories).Where(Function(s) s.EndsWith(".png") OrElse s.EndsWith(".jpg"))
                            If oldFiles.Any() Then
                                For Each item In oldFiles
                                    System.IO.File.Delete(item)
                                Next

                            End If
                            If Not Directory.Exists(destPath) Then
                                Directory.CreateDirectory(destPath)
                            End If

                            System.IO.File.Copy(fullpath, System.IO.Path.Combine(destPath, System.IO.Path.GetFileName(fullpath)), True)
                            'System.IO.File.Move(fullpath, destPath)
                            users.Profile = System.IO.Path.GetFileName(fullpath)
                            If (System.IO.File.Exists(fullpath)) Then
                                System.IO.File.Delete(fullpath)
                            End If
                            If Directory.Exists(uploadPath) Then
                                System.IO.Directory.Delete(uploadPath, True)

                            End If
                        End If

                    End If

                    db.SaveChanges()
                    log.WriteLog(Logger.LogType.Info, "UsersController", "Edie", "Edit User Successful")
                    Return RedirectToAction("Index")
                End If
                ViewBag.Roles_Id = New SelectList(db.Roles, "Id", "Type", users.Roles_Id)
                ViewBag.Created_User_Id = New SelectList(db.Users, "Id", "Name", users.Created_User_Id)
                ViewBag.Updated_User_Id = New SelectList(db.Users, "Id", "Name", users.Updated_User_Id)
                Return View(users)
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "UsersController", "Edit", detailErrorLog)

            End Try
        End Function

        <HttpPost()>
        Function ImageEdit(ByVal users As Users) As ActionResult
            Try
                Dim postedFile As HttpPostedFileBase = Request.Files(0)
                Dim userId As String = Request.Form("userID")
                If postedFile IsNot Nothing Then
                    Dim filePath As String = String.Empty
                    Dim path As String = Server.MapPath("~/Uploads/Image/NewUserTemp/")
                    Dim uploadPath As String = System.IO.Path.Combine(Server.MapPath("~/Uploads/Images/NewUserTemp/"), userId & "\")

                    If Not Directory.Exists(uploadPath) Then
                        Directory.CreateDirectory(uploadPath)
                    End If
                    Dim files = Directory.EnumerateFiles(uploadPath, "*.*", SearchOption.AllDirectories).Where(Function(s) s.EndsWith(".png") OrElse s.EndsWith(".jpg"))

                    If (System.IO.File.Exists(files.FirstOrDefault())) Then
                        System.IO.File.Delete(files.FirstOrDefault())
                    End If

                    Dim Name = System.IO.Path.GetFileNameWithoutExtension(postedFile.FileName)
                    Dim extension As String = System.IO.Path.GetExtension(postedFile.FileName)
                    Name = Name + "_" + DateTime.Now.ToString("yyyy-dd-MMM--hh-") + extension
                    filePath = uploadPath & Name
                    Dim FileName As String = postedFile.FileName
                    postedFile.SaveAs(filePath)
                    log.WriteLog(Logger.LogType.Info, "UsersController", "ImageEdit", "User ImageEdit Successful")
                    Return Content(Name)
                End If
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "UsersController", "ImageEdit", detailErrorLog)

            End Try
        End Function

        ' GET: Users/Delete/5
        <CustomAuthorize({"Administrator"})>
        Function Delete(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim users As Users = db.Users.Find(id)
            If IsNothing(users) Then
                Return HttpNotFound()
            End If
            Return View(users)
        End Function

        ' POST: Users/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Guid) As ActionResult
            Try
                Dim users As Users = db.Users.Find(id)

                users.Is_Deleted = "inactive"
                users.Deleted_At = DateTime.Now()
                users.Deleted_User_Id = Session("CurrentUser")
                db.SaveChanges()
                log.WriteLog(Logger.LogType.Info, "UsersController", "Delete", "Delete User Successful")
                Return RedirectToAction("Index")
            Catch ex As Exception
                Dim source As String = If(ex.Source Is Nothing, "", "Source:" & ex.Source)
                Dim stackTrace As String = If(ex.StackTrace Is Nothing, "", "Stack Trace:" & ex.StackTrace)
                Dim innerExp As String = If(ex.InnerException Is Nothing, "", "Inner Exception Message:" & ex.InnerException.Message)
                Dim innerExp1 As String = If(ex.InnerException IsNot Nothing, (If(ex.InnerException.InnerException Is Nothing, "", "Inner Inner Exception Message:" & ex.InnerException.InnerException.Message)), "")
                Dim detailErrorLog As String = "Message:" & ex.Message & innerExp & innerExp1 & source & stackTrace & "Target Site:" & ex.TargetSite.ToString
                log.WriteLog(Logger.LogType.Fatal, "UsersController", "Delete", detailErrorLog)

            End Try
        End Function

        Function IsUserExist(ByVal email As String) As ActionResult

            Dim user = db.Users.FirstOrDefault(Function(model) model.Email = email)
            If user Is Nothing Then
                Return Content("false")
            Else
                Return Content("true")
            End If
        End Function

        Function UnAuthorized() As ActionResult
            ViewBag.Message = "Un Authorized Page!"
            Return View()
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
