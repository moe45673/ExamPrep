Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports System.Data.Entity
Imports System.Linq
Imports System.Web
Imports Mvc5.MyModels

'* Put any application specific classes here
Public Class ApplicationUser
    Inherits IdentityUser

    <Key()>
    Public ApplicationUserId As Integer

    '* user info details are stored different table
    Public Overridable Property Info As ApplicationUserInfo

    '* Each user has their own messages
    Public Overridable Property Messages As ICollection(Of Adapters.Message)

End Class

Public Class ApplicationUserInfo

    <Key()>
    Public Property Id As Integer
    Public Property FirstName As String
    Public Property LastName As String
    Public Property HomeTown As String

End Class

