'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' program:      Connect4

' file:         AboutForm.vb

' function:     methods of the AboutForm class

' description:  displays the connect4 game information

' author:       Mohammed Safwat (MS)

' environment:  visual studio.net enterprise architect 2003,
'               windows xp professional

' notes:        This is a translated faculy program.

' revisions:    2.5  20/11/2005 (MS) first release

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Public Class AboutForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Private WithEvents cmdOK As System.Windows.Forms.Button
    Private WithEvents imgProgramIcon As System.Windows.Forms.PictureBox
    Private WithEvents lblProductName As System.Windows.Forms.Label
    Private WithEvents lblVersion As System.Windows.Forms.Label
    Private WithEvents lblCopyright As System.Windows.Forms.Label
    Private WithEvents lblComments As System.Windows.Forms.Label
    Private WithEvents pnlGameData As System.Windows.Forms.Panel
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(AboutForm))
        Me.pnlGameData = New System.Windows.Forms.Panel
        Me.lblComments = New System.Windows.Forms.Label
        Me.lblCopyright = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.lblProductName = New System.Windows.Forms.Label
        Me.imgProgramIcon = New System.Windows.Forms.PictureBox
        Me.cmdOK = New System.Windows.Forms.Button
        Me.pnlGameData.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlGameData
        '
        Me.pnlGameData.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlGameData.Controls.Add(Me.lblComments)
        Me.pnlGameData.Controls.Add(Me.lblCopyright)
        Me.pnlGameData.Controls.Add(Me.lblVersion)
        Me.pnlGameData.Controls.Add(Me.lblProductName)
        Me.pnlGameData.Controls.Add(Me.imgProgramIcon)
        Me.pnlGameData.Location = New System.Drawing.Point(10, 8)
        Me.pnlGameData.Name = "pnlGameData"
        Me.pnlGameData.Size = New System.Drawing.Size(281, 161)
        Me.pnlGameData.TabIndex = 0
        '
        'lblComments
        '
        Me.lblComments.AutoSize = True
        Me.lblComments.Location = New System.Drawing.Point(8, 128)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(159, 16)
        Me.lblComments.TabIndex = 3
        Me.lblComments.Text = "Comments: All rights reserved."
        '
        'lblCopyright
        '
        Me.lblCopyright.Location = New System.Drawing.Point(8, 80)
        Me.lblCopyright.Name = "lblCopyright"
        Me.lblCopyright.Size = New System.Drawing.Size(220, 40)
        Me.lblCopyright.TabIndex = 2
        Me.lblCopyright.Text = "Copyright ©2005 Fourth Year, Electronics and Communications Dept., Faculty of Eng" & _
        "ineering, Cairo University."
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Location = New System.Drawing.Point(88, 40)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(61, 16)
        Me.lblVersion.TabIndex = 1
        Me.lblVersion.Text = "Version 2.5"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Location = New System.Drawing.Point(88, 16)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(0, 16)
        Me.lblProductName.TabIndex = 0
        '
        'imgProgramIcon
        '
        Me.imgProgramIcon.Image = CType(resources.GetObject("imgProgramIcon.Image"), System.Drawing.Image)
        Me.imgProgramIcon.Location = New System.Drawing.Point(8, 8)
        Me.imgProgramIcon.Name = "imgProgramIcon"
        Me.imgProgramIcon.Size = New System.Drawing.Size(65, 57)
        Me.imgProgramIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgProgramIcon.TabIndex = 0
        Me.imgProgramIcon.TabStop = False
        '
        'cmdOK
        '
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Location = New System.Drawing.Point(113, 180)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.Size = New System.Drawing.Size(75, 25)
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        '
        'frmAbout
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.cmdOK
        Me.ClientSize = New System.Drawing.Size(300, 215)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.pnlGameData)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAbout"
        Me.ShowInTaskbar = False
        Me.Text = "About"
        Me.pnlGameData.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        lblProductName.Text = Application.ProductName

    End Sub ' end of method frmAbout_Load
End Class ' end of class AboutForm
