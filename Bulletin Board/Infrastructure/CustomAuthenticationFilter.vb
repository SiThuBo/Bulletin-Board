Imports System.Web.Mvc.Filters
Imports System.Web.Routing

Namespace BulletinBoard_OJT.Infrastructure

    Public Class CustomAuthenticationFilter
        Inherits ActionFilterAttribute
        Implements IAuthenticationFilter

        Public Sub OnAuthentication(filterContext As AuthenticationContext) Implements IAuthenticationFilter.OnAuthentication
            If filterContext.HttpContext.Session("CurrentUser") Is Nothing Then
                Console.Write(filterContext.HttpContext.Session("CurrentUser"))

                filterContext.Result = New HttpUnauthorizedResult()

            End If

        End Sub

        Public Sub OnAuthenticationChallenge(filterContext As AuthenticationChallengeContext) Implements IAuthenticationFilter.OnAuthenticationChallenge


            If filterContext.Result Is Nothing Then
                filterContext.Result = New RedirectToRouteResult(
                        New RouteValueDictionary(
                        New With {.controller = "Users", .action = "Index"}))
            End If
        End Sub
    End Class
End Namespace
