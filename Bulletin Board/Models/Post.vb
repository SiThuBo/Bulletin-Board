Imports System.ComponentModel.DataAnnotations

Public Class Post
    Public Property Id() As Guid

    <Required(ErrorMessage:="Title is required")>
    Public Property title() As String
    Public Property description() As String

    Public Property status() As String


    Public Property created_at As DateTime
    Public Property updated_at As DateTime
    Public Property deleted_at As DateTime
    Public Property created_user_id As Guid
    Public Property updated_user_id As Guid
    Public Property deleted_user_id As Guid

End Class
