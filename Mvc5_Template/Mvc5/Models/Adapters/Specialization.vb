Imports VBLib
Imports System.ComponentModel.DataAnnotations

Namespace MyModels.Adapters

    <Schema.Table("Specializations")>
    Public Class Specialization
        <Key()>
        Public Property SpecId As Integer        '* Primary key for Specialization

        Public Property PersonId As Integer        '* Foreign key for Person

        <Schema.ForeignKey("PersonId")>
        Public Property Person As MyModels.Adapters.Person

        Public Property Name As String
            Get
                Return modelSpec.Name
            End Get
            Set(value As String)
                Try
                    modelSpecState.Clear()
                    modelSpec.Name = value
                Catch ex As Exception
                    '* IMPORTANT key value should be same as the property
                    modelSpecState.AddModelError("Name", ex.Message)
                End Try
            End Set
        End Property

        Dim modelSpec As VBLib.Specialization

        Dim modelSpecState As ModelStateDictionary
        Public ReadOnly Property ModelState() As ModelStateDictionary
            Get
                Return modelSpecState
            End Get
        End Property
    End Class



End Namespace
