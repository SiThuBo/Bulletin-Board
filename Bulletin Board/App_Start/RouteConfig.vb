Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc
Imports System.Web.Routing
Imports BulletinBoard_OJT.DB_Entity

Public Module RouteConfig

    Public Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        routes.MapRoute(
            name:="Login",
            url:="{controller}/{action}/{id}",
            defaults:=New With {.controller = "Login", .action = "Login", .id = UrlParameter.Optional}
        )

        routes.MapRoute(
            name:="Default",
            url:="{controller}/{action}/{id}",
            defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional}
        )

        'Dim seeder As BulletinBoardEntities.BulletainBoardInitializer = New BulletinBoardEntities.BulletainBoardInitializer()


    End Sub
End Module