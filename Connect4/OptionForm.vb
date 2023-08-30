'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' program:      Connect4

' file:         OptionForm.vb

' function:     methods of the OptionForm class

' description:  displays/changes the connect4 game options

' author: Mohammed Safwat (MS)

' environment:  visual studio.net enterprise architect 2003,
'               windows xp professional

' notes:        This is a translated faculy program.

' revisions:    2.5  8/1/2006 (MS) starting construction
'               2.51 9/1/2006 (MS) first release

    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Connect4Library
Imports Microsoft.Win32
Imports System.IO
Public Class OptionForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    ' overloaded constructors
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Public Sub New(ByVal currentOptions As Options)

        Me.New()
        itsOptions = currentOptions
        ' Set the initial colors of the sample tiles.
        a_itsColors(player.minimizing) = _
            currentOptions.a_itsTileColors(player.minimizing)
        a_itsColors(player.maximizing) = _
            currentOptions.a_itsTileColors(player.maximizing)
        ' Associate color changing buttons with the color
        ' changing method.
        AddHandler cmdUserColor.Click, AddressOf ChangeColor
        AddHandler cmdComputerColor.Click, AddressOf ChangeColor

    End Sub ' end of constructor

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
    Private WithEvents pnlOptions As System.Windows.Forms.Panel
    Private WithEvents cmdOK As System.Windows.Forms.Button
    Private WithEvents cmdCancel As System.Windows.Forms.Button
    Private WithEvents lblUser As System.Windows.Forms.Label
    Private WithEvents cmdUserColor As System.Windows.Forms.Button
    Private WithEvents lblComputer As System.Windows.Forms.Label
    Private WithEvents cmdComputerColor As System.Windows.Forms.Button
    Private WithEvents lblDifficulty As System.Windows.Forms.Label
    Private WithEvents cboLevel As System.Windows.Forms.ComboBox
    Private WithEvents lblBackground As System.Windows.Forms.Label
    Private WithEvents lstBackgrounds As System.Windows.Forms.ListBox
    Private WithEvents imgBackground As System.Windows.Forms.PictureBox
    Private WithEvents dlgColor As System.Windows.Forms.ColorDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.pnlOptions = New System.Windows.Forms.Panel
        Me.imgBackground = New System.Windows.Forms.PictureBox
        Me.lstBackgrounds = New System.Windows.Forms.ListBox
        Me.lblBackground = New System.Windows.Forms.Label
        Me.cboLevel = New System.Windows.Forms.ComboBox
        Me.lblDifficulty = New System.Windows.Forms.Label
        Me.cmdComputerColor = New System.Windows.Forms.Button
        Me.lblComputer = New System.Windows.Forms.Label
        Me.cmdUserColor = New System.Windows.Forms.Button
        Me.lblUser = New System.Windows.Forms.Label
        Me.cmdOK = New System.Windows.Forms.Button
        Me.cmdCancel = New System.Windows.Forms.Button
        Me.dlgColor = New System.Windows.Forms.ColorDialog
        Me.pnlOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlOptions
        '
        Me.pnlOptions.Controls.Add(Me.imgBackground)
        Me.pnlOptions.Controls.Add(Me.lstBackgrounds)
        Me.pnlOptions.Controls.Add(Me.lblBackground)
        Me.pnlOptions.Controls.Add(Me.cboLevel)
        Me.pnlOptions.Controls.Add(Me.lblDifficulty)
        Me.pnlOptions.Controls.Add(Me.cmdComputerColor)
        Me.pnlOptions.Controls.Add(Me.lblComputer)
        Me.pnlOptions.Controls.Add(Me.cmdUserColor)
        Me.pnlOptions.Controls.Add(Me.lblUser)
        Me.pnlOptions.Location = New System.Drawing.Point(10, 8)
        Me.pnlOptions.Name = "pnlOptions"
        Me.pnlOptions.Size = New System.Drawing.Size(427, 185)
        Me.pnlOptions.TabIndex = 0
        '
        'imgBackground
        '
        Me.imgBackground.Location = New System.Drawing.Point(312, 68)
        Me.imgBackground.Name = "imgBackground"
        Me.imgBackground.Size = New System.Drawing.Size(105, 97)
        Me.imgBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imgBackground.TabIndex = 8
        Me.imgBackground.TabStop = False
        '
        'lstBackgrounds
        '
        Me.lstBackgrounds.Location = New System.Drawing.Point(192, 68)
        Me.lstBackgrounds.Name = "lstBackgrounds"
        Me.lstBackgrounds.Size = New System.Drawing.Size(105, 95)
        Me.lstBackgrounds.TabIndex = 7
        '
        'lblBackground
        '
        Me.lblBackground.AutoSize = True
        Me.lblBackground.Location = New System.Drawing.Point(192, 40)
        Me.lblBackground.Name = "lblBackground"
        Me.lblBackground.Size = New System.Drawing.Size(110, 16)
        Me.lblBackground.TabIndex = 6
        Me.lblBackground.Text = "Choose Background:"
        '
        'cboLevel
        '
        Me.cboLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLevel.Location = New System.Drawing.Point(78, 144)
        Me.cboLevel.Name = "cboLevel"
        Me.cboLevel.Size = New System.Drawing.Size(56, 21)
        Me.cboLevel.TabIndex = 5
        '
        'lblDifficulty
        '
        Me.lblDifficulty.Location = New System.Drawing.Point(6, 136)
        Me.lblDifficulty.Name = "lblDifficulty"
        Me.lblDifficulty.Size = New System.Drawing.Size(64, 33)
        Me.lblDifficulty.TabIndex = 4
        Me.lblDifficulty.Text = "Foresight Perception:"
        '
        'cmdComputerColor
        '
        Me.cmdComputerColor.Location = New System.Drawing.Point(152, 84)
        Me.cmdComputerColor.Name = "cmdComputerColor"
        Me.cmdComputerColor.Size = New System.Drawing.Size(25, 25)
        Me.cmdComputerColor.TabIndex = 3
        Me.cmdComputerColor.Text = "..."
        '
        'lblComputer
        '
        Me.lblComputer.AutoSize = True
        Me.lblComputer.Location = New System.Drawing.Point(6, 84)
        Me.lblComputer.Name = "lblComputer"
        Me.lblComputer.Size = New System.Drawing.Size(87, 16)
        Me.lblComputer.TabIndex = 2
        Me.lblComputer.Text = "Computer Color:"
        '
        'cmdUserColor
        '
        Me.cmdUserColor.Location = New System.Drawing.Point(152, 24)
        Me.cmdUserColor.Name = "cmdUserColor"
        Me.cmdUserColor.Size = New System.Drawing.Size(25, 25)
        Me.cmdUserColor.TabIndex = 1
        Me.cmdUserColor.Text = "..."
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Location = New System.Drawing.Point(6, 24)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(61, 16)
        Me.lblUser.TabIndex = 0
        Me.lblUser.Text = "User Color:"
        '
        'cmdOK
        '
        Me.cmdOK.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.cmdOK.Location = New System.Drawing.Point(136, 212)
        Me.cmdOK.Name = "cmdOK"
        Me.cmdOK.TabIndex = 1
        Me.cmdOK.Text = "OK"
        '
        'cmdCancel
        '
        Me.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cmdCancel.Location = New System.Drawing.Point(236, 212)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.TabIndex = 2
        Me.cmdCancel.Text = "Cancel"
        '
        'frmOptions
        '
        Me.AcceptButton = Me.cmdOK
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.cmdCancel
        Me.ClientSize = New System.Drawing.Size(446, 252)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.cmdOK)
        Me.Controls.Add(Me.pnlOptions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.ShowInTaskbar = False
        Me.Text = "Options"
        Me.pnlOptions.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub ChangeColor( _
        ByVal objSender As Object, ByVal e As EventArgs)
        Dim bytColorOwner As Byte = CType(objSender, Button).Tag

        dlgColor.Color = a_itsColors(bytColorOwner)

        If dlgColor.ShowDialog() = DialogResult.OK Then

            a_itsColors(bytColorOwner) = dlgColor.Color

        End If

        pnlOptions.Invalidate()

    End Sub ' end of method ChangeColor

    Private Sub cmdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdOK.Click

        itsOptions.iItsBackground = lstBackgrounds.SelectedIndex
        itsOptions.bytItsLevel = cboLevel.SelectedItem
        ' Save the chosen colors of the tiles.
        itsOptions.a_itsTileColors(player.maximizing) = _
            a_itsColors(player.maximizing)
        itsOptions.a_itsTileColors(player.minimizing) = _
            a_itsColors(player.minimizing)

    End Sub ' end of method cmdOK_Click

    Private Sub frmOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim backgroundSourceKey As RegistryKey = _
            Registry.CurrentUser.OpenSubKey( _
            Parameters.strApplicationRegistryKey & _
            RegistryStream.keySeparator & _
            Parameters.strBackgroundRegistryKey), _
            bytCount As Byte

        ' Load the level combo box with the available difficulty
        ' levels.
        cboLevel.Hide() ' Hide the level combo box before filling
        ' it with available difficulty levels to overcome the
        ' flicker that may happen.

        For bytCount = 1 To Options.bytMaxDifficulty

            cboLevel.Items.Add(bytCount)

        Next bytCount

        cboLevel.Show()
        cboLevel.SelectedItem = itsOptions.bytItsLevel
        lstBackgrounds.Items.AddRange( _
            backgroundSourceKey.GetValueNames()) ' Load the names
        ' of the backgrounds from the open registry key to the
        ' list box.
        ' Size the backgrounds array as needed.
        a_itsBackgrounds = _
            New Background(lstBackgrounds.Items.Count - 1) {}
        ' Load the data of backgrounds from the registry to the
        ' array of backgrounds.
        bytCount = 0

        Do While bytCount < a_itsBackgrounds.Length

            a_itsBackgrounds(bytCount) = _
                RegistryStream.Read(backgroundSourceKey, _
                lstBackgrounds.Items(bytCount))
            bytCount += 1

        Loop

        lstBackgrounds.SelectedIndex = itsOptions.iItsBackground
        ' Set the identities of the color change buttons.
        cmdUserColor.Tag = player.minimizing
        cmdComputerColor.Tag = player.maximizing

    End Sub ' end of method frmOptions_Load

    Private Sub lstBackgrounds_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstBackgrounds.SelectedIndexChanged
        Dim strBackgroundFileName As String = _
            Parameters.strBackgroundFolder & _
            Path.DirectorySeparatorChar & a_itsBackgrounds( _
            lstBackgrounds.SelectedIndex).strFileName

        If File.Exists(strBackgroundFileName) Then ' if the
            ' background file really exists

            imgBackground.Image = _
                Image.FromFile(strBackgroundFileName) ' Load the
            ' background.

        Else

            MessageBox.Show("missing background file. The " & _
                "background can't be loaded.", Owner.Text, _
                MessageBoxButtons.OK, MessageBoxIcon.Error)

            ' Undo the user trial to change the current background.
            If lstBackgrounds.SelectedIndex <> _
                itsOptions.iItsBackground Then

                lstBackgrounds.SelectedIndex = _
                    itsOptions.iItsBackground

            End If

        End If

    End Sub ' end of method lstBackgrounds_SelectedIndexChanged

    Private Sub pnlOptions_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlOptions.Paint
        Const bytSampleDiameter As Byte = 49
        Dim sampleArea As New Rectangle(New Point(92, 12), _
            New Size(bytSampleDiameter, bytSampleDiameter))

        e.Graphics.FillEllipse(New SolidBrush( _
            a_itsColors(player.minimizing)), sampleArea) ' Draw
        ' the user tile sample.
        ' Draw the computer tile sample.
        sampleArea.Y = 72
        e.Graphics.FillEllipse(New SolidBrush( _
            a_itsColors(player.maximizing)), sampleArea)

    End Sub ' end of method pnlOptions_Paint
    Private a_itsBackgrounds() As Background
    ' currently chosen colors for user and computer tiles
    Private a_itsColors() As Color = New Color( _
        [Enum].GetValues(GetType(player)).Length - 1) {}
    Private itsOptions As Options ' game options
End Class ' end of class OptionForm
