'*************************************************************************
'* Filename: /Models/Adapters/Message.vb
'*************************************************************************

Imports VBLib
Imports System.ComponentModel.DataAnnotations

Namespace MyModels.Adapters

    <Schema.Table("Persons")>
    Public Class Person



        <Key()>
        Public Property PersonId As Integer        '* Primary key for Person

        Public Property Companies As List(Of Adapters.Company)
            Get
                Return _comps
            End Get
            Set(value As List(Of Adapters.Company))
                Try
                    ' messageModelState.Clear()
                    _comps = value
                Catch ex As Exception
                    '* IMPORTANT 
                    '  key (first parameter) should be same as property (Message has a Text property)
                    modelPersonState.AddModelError("Companies", ex.Message)
                End Try
                If value IsNot Nothing Then
                    _comps = value
                Else
                    If (_comps Is Nothing) Then
                        _comps = New List(Of Adapters.Company)()
                    End If
                End If

            End Set
        End Property

        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                Try
                    ' messageModelState.Clear()
                    _name = value
                Catch ex As Exception
                    '* IMPORTANT 
                    '  key (first parameter) should be same as property (Message has a Text property)
                    modelPersonState.AddModelError("Name", ex.Message)
                End Try

            End Set
        End Property

        Public Property Email As String
            Get
                Return _email
            End Get
            Set(value As String)
                Try
                    ' messageModelState.Clear()
                    _email = value
                Catch ex As Exception
                    '* IMPORTANT 
                    '  key (first parameter) should be same as property (Message has a Text property)
                    modelPersonState.AddModelError("Email", ex.Message)
                End Try

            End Set
        End Property

        Public Property Specializations As List(Of Adapters.Specialization)
            Get
                Return _specs
            End Get
            Set(value As List(Of Adapters.Specialization))
                Try
                    _specs = value
                Catch ex As Exception
                    '* IMPORTANT 
                    '  key (first parameter) should be same as property (Message has a Text property)
                    modelPersonState.AddModelError("Specializations", ex.Message)
                End Try
                If value IsNot Nothing Then
                    _specs = value
                Else
                    If (_specs Is Nothing) Then
                        _specs = New List(Of Adapters.Specialization)()
                    End If
                End If

            End Set
        End Property

        Public Sub New()
            _person = New VBLib.Person
            _comps = New List(Of Adapters.Company)
            modelPersonState = New ModelStateDictionary
        End Sub

        '* creates a new message from given VBLib.Person
        Public Sub New(psn As VBLib.Person, id As Integer)
            PersonId = id
            _person = New VBLib.Person

            _person.Companies = psn.Companies
            _person.Specialization = psn.Specialization
            _person.Name = psn.Name
            _person.Email = psn.Email

            '* copy all translations for the given message
            Dim mvcCompsList As New List(Of Adapters.Company)
            For Each item In psn.Companies
                mvcCompsList.Add(New Adapters.Company With {
                                 .CompanyId = id,
                                 .Name = item.Name})
            Next
            '* assign to the Translations property (details)
            _comps = mvcCompsList

            Dim mvcSpecsList As New List(Of Adapters.Specialization)
            For Each item In psn.Specialization
                mvcSpecsList.Add(New Adapters.Specialization With {
                                 .SpecId = id,
                                 .Name = item.Name})
            Next
            '* assign to the Translations property (details)
            _specs = mvcSpecsList

            modelPersonState = New ModelStateDictionary

        End Sub

        Dim _comps As List(Of Adapters.Company)
        Dim _specs As List(Of Adapters.Specialization)
        Dim _person As VBLib.Person
        Dim modelPersonState As ModelStateDictionary
        Dim _name As String
        Dim _email As String


    End Class

End Namespace