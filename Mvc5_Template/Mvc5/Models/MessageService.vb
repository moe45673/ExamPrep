Imports System.Collections.Generic
Imports System.Data.Entity
Imports System.Linq
Imports System.Threading.Tasks
Imports Mvc5.MyModels

Public Class MessageService

    Public Function AddMessage(m As VBLib.Message, id As Integer) As MyModels.Adapters.Message
        Dim result = context_.Messages.Add(New MyModels.Adapters.Message(m, id))
        context_.SaveChanges()
        Return result
    End Function

    Public Function GetAllMessages() As List(Of MyModels.Adapters.Message)
        Dim query = From messages In context_.Messages
                    Order By messages.Text
                    Select messages

        Return query.ToList()

    End Function

    Sub New(ctx As DataContext)
        If ctx IsNot Nothing Then
            context_ = ctx
        Else
            context_ = New DataContext
        End If
    End Sub

    Sub New()
        context_ = New DataContext
    End Sub

    Private context_ As DataContext

End Class
