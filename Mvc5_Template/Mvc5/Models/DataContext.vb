Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports System.Data.Entity.SqlServer
Imports System.ComponentModel.DataAnnotations
Imports Mvc5.MyModels

' You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
Namespace MyModels
  
    '* -- MF: changed from ApplicationDbContext to DataContext
    Public Class DataContext
        Inherits IdentityDbContext(Of ApplicationUser)

        '* -- MF: added
        Public Overridable Property Messages As IDbSet(Of Adapters.Message)
        Public Overridable Property Translations As IDbSet(Of Adapters.Translation)

        '* -- MF: added
        Protected Shadows Sub OnModelCreating(modelBuilder As DbModelBuilder)

            MyBase.OnModelCreating(modelBuilder)

            '* Change the name of the table to be Users instead of AspNetUsers
            modelBuilder.Entity(Of IdentityUser).ToTable("Users")
            modelBuilder.Entity(Of ApplicationUser).ToTable("Users")

        End Sub

        Public Sub New()
            '* -- MF: changed from DefaultConnection to DataContext
            MyBase.New("DataContext")
        End Sub

    End Class
End Namespace