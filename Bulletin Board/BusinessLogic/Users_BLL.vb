Imports BulletinBoard_OJT.DB_Entity


Namespace BulletinBoard_OJT.BusinessLogic
    Public Class Users_BLL


        Public Shared Function getUsersByEmail(ByVal valEmail As String, ByVal valUserPassword As String) As List(Of Users)
            Dim db As BulletinBoardEntities = New BulletinBoardEntities()
            Dim lstUsers = From tbUsers In db.Users
                           Where (tbUsers.Password.ToUpper() = valUserPassword.ToUpper()) AndAlso (valEmail = "" OrElse tbUsers.Email.ToUpper() = valEmail.ToUpper())
                           Order By tbUsers.Updated_At
                           Select tbUsers

            If lstUsers IsNot Nothing AndAlso lstUsers.Count() > 0 Then
                Dim user = lstUsers.FirstOrDefault
                System.Web.HttpContext.Current.Session.Add("CurrentUser", user.Id)
                System.Web.HttpContext.Current.Session.Add("CurrentUserName", user.Name)

                Return lstUsers.ToList()
            Else
                Return Nothing
            End If
        End Function


        Public Shared Function ValidateUser(userEmail As String, password As String) As Integer?


            Dim sha1Pswd = String.Empty
            If Not String.IsNullOrEmpty(password) Then
                sha1Pswd = Utility.Utility.GetMd5HashBase64(password)
            End If

            Dim user As Users = New Users()
            Dim userObj = getUsersByEmail(userEmail, sha1Pswd)

            If userObj IsNot Nothing AndAlso userObj.Count() >= 1 Then

                Return 1
            Else

                Return 0
            End If
        End Function
    End Class
End Namespace

