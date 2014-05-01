Imports Mvc5.MyModels

Namespace ViewModels.Managers

    '* RepoBase (or base repository) holds a copy of the models datacontext. 
    '* IMPORTANT: all communiction with the database goes through here
    Public Class RepoBase

        Public Sub New()
            dc = New MyModels.DataContext

            '* turn off lazy loading, we do it ourselves
            dc.Configuration.ProxyCreationEnabled = False
            dc.Configuration.LazyLoadingEnabled = False

        End Sub

        Protected dc As DataContext
    End Class

End Namespace
