Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports BulletinBoard_OJT.DB_Entity

Namespace Controllers
    Public Class ImageFilesController
        Inherits System.Web.Mvc.Controller

        Private db As New BulletinBoardEntities

        ' GET: ImageFiles
        Function Index() As ActionResult
            Return View(db.ImageFiles.ToList())
        End Function

        ' GET: ImageFiles/Details/5
        Function Details(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim imageFile As ImageFile = db.ImageFiles.Find(id)
            If IsNothing(imageFile) Then
                Return HttpNotFound()
            End If
            Return View(imageFile)
        End Function

        ' GET: ImageFiles/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: ImageFiles/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="Id,Name,FileSize,FilePath,Uploaded_User_Id")> ByVal imageFile As ImageFile) As ActionResult
            If ModelState.IsValid Then
                imageFile.Id = Guid.NewGuid()
                db.ImageFiles.Add(imageFile)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(imageFile)
        End Function

        ' GET: ImageFiles/Edit/5
        Function Edit(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim imageFile As ImageFile = db.ImageFiles.Find(id)
            If IsNothing(imageFile) Then
                Return HttpNotFound()
            End If
            Return View(imageFile)
        End Function

        ' POST: ImageFiles/Edit/5
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="Id,Name,FileSize,FilePath,Uploaded_User_Id")> ByVal imageFile As ImageFile) As ActionResult
            If ModelState.IsValid Then
                db.Entry(imageFile).State = Data.EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            Return View(imageFile)
        End Function

        ' GET: ImageFiles/Delete/5
        Function Delete(ByVal id As Guid?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim imageFile As ImageFile = db.ImageFiles.Find(id)
            If IsNothing(imageFile) Then
                Return HttpNotFound()
            End If
            Return View(imageFile)
        End Function

        ' POST: ImageFiles/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Guid) As ActionResult
            Dim imageFile As ImageFile = db.ImageFiles.Find(id)
            db.ImageFiles.Remove(imageFile)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
