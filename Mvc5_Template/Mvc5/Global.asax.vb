Imports System.Web.Optimization

Public Class MvcApplication
    Inherits System.Web.HttpApplication
    Protected Sub Application_Start()

        AreaRegistration.RegisterAllAreas()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)

        '* MF: seed database with Identity and JSON records (or hard-coded values)
        System.Data.Entity.Database.SetInitializer(New MyDbInitializer())
        '* MF: ------------------------------------------------------

    End Sub
End Class

