Class clsSerialization

    Private myObjekt As Object
    Private Wnd As Window
    Sub New(Objekt As Object, Optional Okno As Window = Nothing)
        myObjekt = Objekt : Wnd = Okno
    End Sub

    Public Sub WriteXml(fileName As String)
        Dim mySerializer As New Xml.Serialization.XmlSerializer(myObjekt.GetType)
        If myFile.Delete(fileName, False, False) Then
            If myFolder.Exist(myFile.Path(fileName), True) Then
                Using fileStream As New IO.FileStream(fileName, IO.FileMode.Create, IO.FileAccess.Write)
                    mySerializer.Serialize(fileStream, myObjekt)
                End Using
            End If
        End If
    End Sub

    Public Function WriteXml() As IO.MemoryStream
        Dim mySerializer As New Xml.Serialization.XmlSerializer(myObjekt.GetType)
        Dim memStream As New IO.MemoryStream
        mySerializer.Serialize(memStream, myObjekt)
        Return memStream
    End Function

    Public Function ReadXml(fileName As String) As Object
        Dim mySerializer As New Xml.Serialization.XmlSerializer(myObjekt.GetType)
        Using reader As New IO.StreamReader(fileName)
            Try
                Return mySerializer.Deserialize(reader)
            Catch Err As Exception
                Call (New wpfDialog(Wnd, fileName & NR & "Unsupported file format.", Application.Title, wpfDialog.Ikona.ok)).ShowDialog()
            End Try
        End Using
        Return myObjekt
    End Function

    Public Function ReadXml(fileStream As IO.Stream) As Object
        If fileStream Is Nothing Then Return myObjekt
        Dim mySerializer As New Xml.Serialization.XmlSerializer(myObjekt.GetType)
        Using reader As New IO.StreamReader(fileStream)
            Try
                Return mySerializer.Deserialize(reader)
            Catch
                Call (New wpfDialog(Wnd, "Unsupported file format.", Application.Title, wpfDialog.Ikona.ok)).ShowDialog()
            End Try
        End Using
        fileStream.Dispose()
        Return myObjekt
    End Function
End Class