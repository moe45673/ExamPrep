Imports Mvc5.MyModels
Imports Microsoft.AspNet.Identity
Imports Microsoft.AspNet.Identity.EntityFramework
Imports Microsoft.AspNet.Identity.Owin
Imports System
Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Web
Imports System.Web.Hosting


'Public Module IdentityConfig
'End Module

'* This is useful if you do not want to tear down the database each time you run the application.
'* You want to create a new database if the Model changes
'* public class MyDbInitializer : DropCreateDatabaseIfModelChanges( OfMyDbContext)
Public Class MyDbInitializer
    Inherits DropCreateDatabaseAlways(Of DataContext)

    Protected Overrides Sub Seed(context As DataContext)
      

        '* initialize Identity
        InitializeIdentityForEF(context)

        '' left this here to demonstrate how I debugged unit testing issues
        'Using sw = New IO.StreamWriter("logFile.txt", True)

        '    sw.WriteLine("Done with identity, now initializing json")
        '    sw.WriteLine("your directory is: " + Environment.CurrentDirectory)
        '    sw.WriteLine("Calling jsonFile: " + jsonFile)

        '    'Using srj = New IO.StreamReader(jsonFile)
        '    '    sw.WriteLine(srj.ReadToEnd())
        '    'End Using
        'End Using

        '* initialize data with JSON data
        Dim sd = New SeedData(jsonFile)
        sd.InitializeDbWithValues(context)

        '' left this here to demonstrate how I debugged unit testing issues
        'Using sw = New IO.StreamWriter("logFile.txt", True)
        '    sw.WriteLine("Done initializing JSON")
        'End Using

        MyBase.Seed(context)

    End Sub

    Private Sub InitializeIdentityForEF(context As DataContext)

        Dim UserManager = New UserManager(Of ApplicationUser)(New UserStore(Of ApplicationUser)(context))
        Dim RoleManager = New RoleManager(Of IdentityRole)(New RoleStore(Of IdentityRole)(context))

        '* Admin user details
        Dim adminInfo = New ApplicationUserInfo() With {
            .FirstName = "System",
            .LastName = "Administrator",
            .HomeTown = "Toronto"
        }
        Dim adminUser = "Admin"
        Dim password = "123456"

        '* Create Role Admin if it does not exist
        If (Not RoleManager.RoleExists(adminUser)) Then
            Dim roleResult = RoleManager.Create(New IdentityRole(adminUser))
        End If

        '* Create admin=Admin with password=123456
        Dim admin = New ApplicationUser()
        admin.UserName = adminUser
        admin.Info = adminInfo
        Dim adminresult = UserManager.Create(admin, password)

        '* Add admin Admin to Role Admin
        If (adminresult.Succeeded) Then
            Dim result = UserManager.AddToRole(admin.Id, adminUser)
        End If

        '* Non-admin user details
        Dim memberUser = "mark"
        Dim memberRole = "MEMBER"
        Dim memberInfo = New ApplicationUserInfo() With {
            .FirstName = "Mark",
            .LastName = "Fernandes",
            .HomeTown = "Toronto"
        }

        '* Create Role VB if it does not exist and add User ian to it
        If (Not RoleManager.RoleExists(memberRole)) Then
            Dim roleResult = RoleManager.Create(New IdentityRole(memberRole))
        End If

        '* Create user=mark with password=123456 
        Dim member = New ApplicationUser()
        member.UserName = memberUser
        member.Info = memberInfo
        Dim createResult = UserManager.Create(member, password)

        '* Add User test to Role VB
        If (createResult.Succeeded) Then
            Dim result = UserManager.AddToRole(member.Id, memberRole)
        End If

    End Sub

    Dim jsonFile As String

    Sub New(Optional jf As String = "~\App_Data\messages.json")
        '' left this here to demonstrate how I debugged unit testing issues
        'Using sw = New IO.StreamWriter("logFile.txt", False)
        '    sw.WriteLine("New: Before all initializing")
        'End Using

        jsonFile = HostingEnvironment.MapPath(jf)
    End Sub

End Class