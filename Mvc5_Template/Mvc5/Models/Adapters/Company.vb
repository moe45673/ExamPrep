Imports VBLib
Imports System.ComponentModel.DataAnnotations

Namespace MyModels.Adapters

    <Schema.Table("Companies")>
    Public Class Company
        <Key()>
        Public Property CompanyId As Integer        '* Primary key for Company

        Public Property PersonId As Integer        '* Foreign key for Person

        <Schema.ForeignKey("PersonId")>
        Public Property Person As MyModels.Adapters.Person

        Public Property Name As String
            Get
                Return modelComp.Name
            End Get
            Set(value As String)
                Try
                    modelCompState.Clear()
                    modelComp.Name = value
                Catch ex As Exception
                    '* IMPORTANT key value should be same as the property
                    modelCompState.AddModelError("Name", ex.Message)
                End Try
            End Set
        End Property

        Dim modelComp As VBLib.Company

        Dim modelCompState As ModelStateDictionary
        Public ReadOnly Property ModelState() As ModelStateDictionary
            Get
                Return modelCompState
            End Get
        End Property
    End Class

End Namespace
