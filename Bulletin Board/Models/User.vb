Imports System.ComponentModel.DataAnnotations

Namespace BulletinBoard_OJT.Models
    Public Class User
        Public Property ID() As Guid

        <Required>
        <RegularExpression("^[A-Z]+[a-zA-Z''-'\s]*$")>
        Public Property NAME() As String
        <EmailAddress(ErrorMessage:="The Email Address is not valid")>
        <Required(ErrorMessage:="Please enter an email address.")>
        <Display(Name:="Email Address:")>
        <RegularExpression("^[_a-z0-9-]+(.[a-z0-9-]+)@[a-z0-9-]+(.[a-z0-9-]+)*(.[a-z]{2,4})$", ErrorMessage:="Invalid Email address")>
        Public Property EMAIL() As String

        <Required(ErrorMessage:="Password is required")>
        <DataType(DataType.Password)>
        Public Property PASSWORD() As String 

        <Required>
        Public Property PROFILE() As String
        Public Property PHONE() As String
        Public Property ADDRESS() As String

        <DataType(DataType.Date)>
        <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)>
        Public Property DOB() As DateTime

        Public Property CRREATED_AT As DateTime
        Public Property UPDATED_AT As DateTime
        Public Property DELETED_AT As DateTime

        Public Property CREATED_USER_ID As Guid
        Public Property UPDATED_USER_ID As Guid
        Public Property DELETED_USER_ID As Guid
        Public Property ROLEID As Guid




    End Class

End Namespace
