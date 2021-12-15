Imports BulletinBoard_OJT.DB_Entity
Namespace BulletinBoard_OJT.Infrastructure
    Public Class CustomAuthorizeAttribute
        Inherits AuthorizeAttribute
        Private ReadOnly allowedroles() As String
        Public Sub New(roles() As String)
            Me.allowedroles = roles
        End Sub

        Protected Overrides Function AuthorizeCore(httpContext As HttpContextBase) As Boolean
            Dim authorize As Boolean = False
            Dim userId = httpContext.Session("CurrentUser")
            If Not userId Is Nothing Then

                Dim db As BulletinBoardEntities = New BulletinBoardEntities()


                Dim userRole = (From u In db.Users
                                Join r In db.Roles On u.Roles_Id Equals r.Id
                                Where u.Id = userId
                                Select New With {r.Type}).FirstOrDefault()

                For Each role In allowedroles
                    If role = userRole.Type Then
                        Return True

                    End If

                Next
            End If
            Return authorize
        End Function

        Protected Overrides Sub HandleUnauthorizedRequest(filterContext As AuthorizationContext)
            filterContext.Result = New RedirectToRouteResult(
               New RouteValueDictionary(New With {.controller = "Users", .action = "UnAuthorized"}))

        End Sub

    End Class
End Namespace
