Imports System.Data.Entity
Imports System.Web.Optimization
Imports BulletinBoard_OJT.DB_Entity

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        log4net.Config.XmlConfigurator.Configure()

        Database.SetInitializer(Of BulletinBoardEntities)(New CreateInitializer())

    End Sub
End Class
