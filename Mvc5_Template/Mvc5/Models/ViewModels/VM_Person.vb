Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations
Imports Mvc5.MyModels
Imports VBLib
Imports System

Namespace ViewModels
    Public Class PersonBase

        <Key()>
        Public Property Id As Integer

        <Required()>
        Public Property Name As String

        Public Property Email As String

    End Class

    <Authorize(Roles:="Admin")>
    Public Class PersonFull
        Inherits PersonBase

        Public Property Companies As List(Of CompanyFull)

        Public Property Specializations As List(Of SpecializationFull)

        Public Sub New()
            Me.Companies = New List(Of CompanyFull)
            Me.Specializations = New List(Of SpecializationFull)
        End Sub

    End Class

    '* Http GET method of Message/Create sends this object to the browser
    Public Class PersonForHttpGet

        <Display(Name:="Name")>
        Public Property Name As String

        <Display(Name:="Email")>
        Public Property Email As String

        <Display(Name:="Company")>
        Public Property CompaniesSelect As SelectList

        <Display(Name:="Company")>
        Public Property SpecializationSelect As SelectList

        Sub Clear()
            Name = String.Empty
            Email = String.Empty
        End Sub

    End Class

    '* Http POST method of Message/Create recieves this object from the browser
    Public Class PersonForHttpPost

        <Required>
        Public Property Name As String
        <Required>
        Public Property Email As String

        <Required(ErrorMessage:="Select A Company!")>
        Public Property CompanyId As Integer

        Public Property SpecializationId As Integer

    End Class

End Namespace