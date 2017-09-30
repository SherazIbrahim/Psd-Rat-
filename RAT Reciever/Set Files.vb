Imports System.Net
Imports System.Net.Mail
Imports System.Net.Sockets
Imports System.IO
Imports System.DirectoryServices
Imports Microsoft
Imports System.Runtime.InteropServices
Imports System.Text
Public Class Set_Files
    Public Shared Function Captureit() As Drawing.Image
        Dim Bmp As New Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height)
        Using g As Graphics = Graphics.FromImage(Bmp)
            g.CopyFromScreen(0, 0, 0, 0, Bmp.Size)
        End Using
        Return Bmp
    End Function
    Public Shared Function Now() As String
        Return DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.ffff").Replace("/", "_").Replace(":", "_").Replace(" ", "_").Replace(".", "_").Replace("-", "_")
    End Function
    Public Shared Sub Pack(ByVal directory As String, ByVal Packet As String)
        Try
            Dim dir = New IO.DirectoryInfo(directory)
            Dim Files = IO.Directory.GetFiles(directory)
            Dim stream = New IO.FileStream(Packet, IO.FileMode.Create)
            Dim writer = New IO.BinaryWriter(stream)
            Dim Count As Long = 0
            Dim Ext As String
            Dim F As New FileInfo(Files(1))
            Ext = F.Extension
            For Each fil In Files
                Count += 1
            Next
            For I = 1 To Count
                Dim bytes = IO.File.ReadAllBytes(directory + "/" + I.ToString + Ext)
                writer.Write(bytes)
                writer.Flush()
            Next
            writer.Dispose()
            stream.Dispose()
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub UnPack(ByVal Packet As String, ByVal Directori As String)
        Try
            If IO.Directory.Exists(Directori) Then
            Else
                IO.Directory.CreateDirectory(Directori)
            End If
            Dim Bytes As Byte() = IO.File.ReadAllBytes(Packet)
            Dim Stream As MemoryStream = New MemoryStream(Bytes)
            Dim Array As Byte() = New Byte(1000 - 1) {}
            Dim Stream2 As New IO.MemoryStream
            Dim Count As Integer = 0
            Dim Nam As Long = 0
            Dim Info As New FileInfo(Packet)
            Dim Ext As String = Info.Extension
            Do
                Nam += 1
                Count = Stream.Read(Array, 0, 1000)
                If Count > 0 Then
                    MsgBox(Count.ToString)
                    Dim Stream1 As New FileStream(Directori + "/" + Nam.ToString + Ext, IO.FileMode.Create)
                    Dim Writer As New BinaryWriter(Stream1)
                    Writer.Write(Array, 0, Count)
                    Writer.Dispose()
                    Stream1.Dispose()
                End If
            Loop While (Count > 0)
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Function Decompress(ByVal gZip As Byte()) As Byte()
        Dim buffer As Byte()
        Using Stream As IO.Compression.GZipStream = New IO.Compression.GZipStream(New IO.MemoryStream(gZip), IO.Compression.CompressionMode.Decompress)
            Dim Array As Byte() = New Byte(&H1000 - 1) {}
            Using Stream2 As IO.MemoryStream = New IO.MemoryStream
                Dim Count As Integer = 0
                Do
                    Count = Stream.Read(Array, 0, &H1000)
                    If Count > 0 Then
                        Stream2.Write(Array, 0, Count)
                    End If
                Loop While (Count > 0)
                buffer = Stream2.ToArray
            End Using
        End Using
        Return buffer
    End Function
    Public Shared Function encrypt(ByVal Data As Byte(), ByVal Password As String) As Byte()
        Dim Encrypted(Data.Count - 1) As Byte
        Dim Passpos As Integer
        Dim Passchar As Char
        Dim b As Integer
        Dim a As Integer
        For i = 0 To Data.Count - 1
            Passpos = CircularPositionPositive(i, Password.Count - 1)
            Passchar = Password(Passpos)
            b = Asc(Passchar)
            a = Data(i)
            Encrypted(i) = a Xor b
        Next
        Return Encrypted
    End Function
    Public Shared Function CircularPositionPositive(ByVal Current As Integer, ByVal Max As Integer) As Integer
        Dim value = ((Current / Max) - Math.Truncate(Current / Max)) * Max
        If value < 0 Then
            value = Max + value
        End If
        Return value
    End Function
    Public Shared Function Compress(ByVal Raw As Byte()) As Byte()
        Using Stream As IO.MemoryStream = New IO.MemoryStream
            Using Stream2 As IO.Compression.GZipStream = New IO.Compression.GZipStream(Stream, IO.Compression.CompressionMode.Compress, True)
                Stream2.Write(Raw, 0, Raw.Length)
            End Using
            Return Stream.ToArray()
        End Using
    End Function
    Public Shared Function Decrypt(ByVal Data As Byte(), ByVal Password As String) As Byte()
        Dim Encrypted(Data.Count - 1) As Byte
        Dim Passpos As Integer
        Dim Passchar As Char
        Dim b As Integer
        Dim a As Integer
        For i = 0 To Data.Count - 1
            Passpos = CircularPositionPositive(i, Password.Count - 1)
            Passchar = Password(Passpos)
            b = Asc(Passchar)
            a = Data(i)
            Encrypted(i) = a Xor b
        Next
        Return Encrypted
    End Function
End Class
