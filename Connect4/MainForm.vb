'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' program:      Connect4

' file:         Main.vb

' function:     methods of the MainForm class

' description:  handles GUI operations of the connect4 game

' author:       Mohammed Safwat (MS)

' environment:  visual studio.net enterprise architect 2003,
'               windows xp professional

' notes:        This is a translated faculy program.

' revisions:    2.5  8/1/2006 (MS) starting construction
'               2.51 11/1/2006 (MS) first release

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Connect4Library
Imports Microsoft.Win32
Imports System.IO
Public Class MainForm
    Inherits System.Windows.Forms.Form

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        itsOptions.Load(Parameters.strApplicationRegistryKey & _
            RegistryStream.keySeparator & _
            Parameters.strOptionsRegistryKey)
        ValidatePreferredBackground()
        bytItsFlashingColumn = Parameters.bytColumns ' No column
        ' has to be flashed currently.
        AddHandler imgBackground.MouseUp, _
            AddressOf ExecutePlayerTurn

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
    Private WithEvents mainMenuBar As System.Windows.Forms.MainMenu
    Friend WithEvents mnuGame As System.Windows.Forms.MenuItem
    Private WithEvents mnuGameNew As System.Windows.Forms.MenuItem
    Private WithEvents mnuGameSeparator As System.Windows.Forms.MenuItem
    Private WithEvents mnuGameExit As System.Windows.Forms.MenuItem
    Private WithEvents mnuOptions As System.Windows.Forms.MenuItem
    Private WithEvents mnuHelp As System.Windows.Forms.MenuItem
    Private WithEvents mnuHelpAbout As System.Windows.Forms.MenuItem
    Private WithEvents imgBackground As System.Windows.Forms.PictureBox
    Private WithEvents statusLine As System.Windows.Forms.StatusBar
    Private WithEvents blinker As FiniteRepeatTimer.FiniteRepeatTimer
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(MainForm))
        Me.mainMenuBar = New System.Windows.Forms.MainMenu
        Me.mnuGame = New System.Windows.Forms.MenuItem
        Me.mnuGameNew = New System.Windows.Forms.MenuItem
        Me.mnuGameSeparator = New System.Windows.Forms.MenuItem
        Me.mnuGameExit = New System.Windows.Forms.MenuItem
        Me.mnuOptions = New System.Windows.Forms.MenuItem
        Me.mnuHelp = New System.Windows.Forms.MenuItem
        Me.mnuHelpAbout = New System.Windows.Forms.MenuItem
        Me.imgBackground = New System.Windows.Forms.PictureBox
        Me.statusLine = New System.Windows.Forms.StatusBar
        Me.blinker = New FiniteRepeatTimer.FiniteRepeatTimer
        Me.SuspendLayout()
        '
        'mainMenuBar
        '
        Me.mainMenuBar.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuGame, Me.mnuOptions, Me.mnuHelp})
        '
        'mnuGame
        '
        Me.mnuGame.Index = 0
        Me.mnuGame.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuGameNew, Me.mnuGameSeparator, Me.mnuGameExit})
        Me.mnuGame.Text = "&Game"
        '
        'mnuGameNew
        '
        Me.mnuGameNew.Index = 0
        Me.mnuGameNew.Text = "&New Game"
        '
        'mnuGameSeparator
        '
        Me.mnuGameSeparator.Index = 1
        Me.mnuGameSeparator.Text = "-"
        '
        'mnuGameExit
        '
        Me.mnuGameExit.Index = 2
        Me.mnuGameExit.Text = "E&xit"
        '
        'mnuOptions
        '
        Me.mnuOptions.Index = 1
        Me.mnuOptions.Text = "&Options"
        '
        'mnuHelp
        '
        Me.mnuHelp.Index = 2
        Me.mnuHelp.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.mnuHelpAbout})
        Me.mnuHelp.Text = "&Help"
        '
        'mnuHelpAbout
        '
        Me.mnuHelpAbout.Index = 0
        Me.mnuHelpAbout.Text = "&About..."
        '
        'imgBackground
        '
        Me.imgBackground.Location = New System.Drawing.Point(0, 0)
        Me.imgBackground.Name = "imgBackground"
        Me.imgBackground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.imgBackground.TabIndex = 0
        Me.imgBackground.TabStop = False
        '
        'statusLine
        '
        Me.statusLine.Location = New System.Drawing.Point(0, 250)
        Me.statusLine.Name = "statusLine"
        Me.statusLine.Size = New System.Drawing.Size(292, 22)
        Me.statusLine.TabIndex = 1
        '
        'blinker
        '
        Me.blinker.Interval = 50
        Me.blinker.Repetitions = CType(3, Short)
        '
        'frmMain
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.Green
        Me.ClientSize = New System.Drawing.Size(292, 272)
        Me.Controls.Add(Me.statusLine)
        Me.Controls.Add(Me.imgBackground)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Menu = Me.mainMenuBar
        Me.Name = "frmMain"
        Me.Text = "Connect 4"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub

#End Region
#If TRACE Then
    Protected Overrides Sub Finalize()

        If Not (itsLogFile Is Nothing) Then itsLogFile.Close()

        MyBase.Finalize()

    End Sub ' end of destructor

#End If
    Private Sub ApplyTurn( _
        ByVal bytColumn As Byte, ByVal owner As player)

        If a_itsCurrentState Is Nothing Then ' if the board is
            ' empty

            ' Allocate a new board.
            ReDim a_itsCurrentState(Parameters.bytColumns - 1)
#If TRACE Then
            ' Open a log file for tracing.
            itsLogFile = New StreamWriter("traceVB.net.log")
            itsLogFile.AutoFlush = True
#End If

        End If

        If a_itsCurrentState(bytColumn) Is Nothing Then ' if the
            ' column is empty

            a_itsCurrentState(bytColumn) = _
                New ArrayList(Parameters.bytRows) ' Create the
            ' tile column.

        End If

        a_itsCurrentState(bytColumn).Add(owner)
#If TRACE Then
        itsLogFile.WriteLine(IIf(owner = player.maximizing, _
            "computer", "user") & ": " & bytColumn) ' Dump this
        ' throw to the log file.
#End If
        ' Adjust the flashing parameters.
        iItsBlinkingStep = _
            (itsOptions.a_itsTileColors(owner).ToArgb() - _
            emptyTileColor.ToArgb()) / blinker.Repetitions
        bytItsFlashingColumn = bytColumn
        itsFlashingTileColor = emptyTileColor
        blinker.Enabled = True ' Enable the blinking(flashing)
        ' timer.

    End Sub ' end of method ApplyTurn

    Private Sub blinker_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles blinker.Tick
        Dim flashingPlayer As player

        If blinker.CurrentTrigger = blinker.Repetitions Then ' if
            ' this is the last color change

            flashingPlayer = a_itsCurrentState( _
                bytItsFlashingColumn)(a_itsCurrentState( _
                bytItsFlashingColumn).Count - 1)
            bytItsFlashingColumn = Parameters.bytColumns ' Give
            ' the current tile its final color.
            imgBackground.Invalidate()

            If flashingPlayer = player.minimizing Then

                ComputerTurn() ' Allow the computer to play.

            ElseIf bItsGoal Then ' If the computer has won, get
                ' ready to start a new game.

#If TRACE Then
                DisconnectLogFile()
#End If
                MessageBox.Show("The computer wins.", Text, _
                    MessageBoxButtons.OK, _
                    MessageBoxIcon.Information)
                a_itsCurrentState = Nothing ' Get ready for a new
                ' game.
                ToggleUserInterface(True) ' Enable the user
                ' interface.

            Else

                ToggleUserInterface(True) ' Enable the user
                ' interface.

            End If

        Else ' if this is a gradual color change

            itsFlashingTileColor = Color.FromArgb( _
                itsFlashingTileColor.ToArgb() + iItsBlinkingStep)
            imgBackground.Invalidate()

        End If

    End Sub ' end of method blinker_Tick

    Private Sub ComputerTurn()
        Dim bytNextStep As Byte, _
            struserMessage As String, _
            tree As New AlphaBetaTree( _
            itsOptions.bytItsLevel, a_itsCurrentState)

        statusLine.Text = "analyzing..."
        tree.ApplyAlphaBeta(bItsGoal, bytNextStep, itsBeginner)
        statusLine.Text = ""

        If bytNextStep = Parameters.bytColumns Then ' if no next
            ' step is valid

#If TRACE Then
            DisconnectLogFile()
#End If
            struserMessage = _
                IIf(bItsGoal, "You win.", "The game can't be" & _
                " continued any more. No one wins.")
            MessageBox.Show(struserMessage, Text, _
                MessageBoxButtons.OK, MessageBoxIcon.Information)
            a_itsCurrentState = Nothing
            ToggleUserInterface(True) ' Enable the user
            ' interface.

        Else

            ApplyTurn(bytNextStep, player.maximizing) ' Show the
            ' visual effect of the computer's turn.

        End If

    End Sub ' end of method ComputerTurn

#If TRACE Then
    Private Sub DisconnectLogFile()

        If Not (itsLogFile Is Nothing) Then

            itsLogFile.Close()
            itsLogFile = Nothing

        End If

    End Sub ' end of method DisconnectLogFile

#End If
    Private Sub ExecutePlayerTurn( _
        ByVal objSender As Object, ByVal e As MouseEventArgs)
        Dim iCount As Integer, _
            relativeMouse As Short ' position of the mouse
        ' relative to the left edge of a tile column

        If e.Button = MouseButtons.Left Then ' if a left click is
            ' detected

            ' Search for the column within which the mouse was
            ' clicked.
            iCount = Parameters.bytColumns - 1
            relativeMouse = e.X - a_itsHorizontalTicks(iCount)

            Do While iCount >= 0 And relativeMouse < 0

                iCount -= 1
                relativeMouse = e.X - a_itsHorizontalTicks(iCount)

            Loop

            If iCount >= 0 And _
                relativeMouse < itsTileDiameter Then ' if the
                ' click was determined to be within the
                ' boundaries of a column

                If Not (a_itsCurrentState Is Nothing) AndAlso _
                    Not (a_itsCurrentState( _
                    iCount) Is Nothing) AndAlso _
                    a_itsCurrentState(iCount).Count = _
                    Parameters.bytRows Then ' if the column is
                    ' full

                    MessageBox.Show("invalid throw. This " & _
                        "column is full. Please, try again.", _
                        Text, MessageBoxButtons.OK, _
                        MessageBoxIcon.Error) ' Notify the user.

                Else ' if the throw is valid

                    ToggleUserInterface(False) ' Disable the user
                    ' interface.
                    ApplyTurn(iCount, player.minimizing) ' Show
                    ' its visual effect.

                End If

            End If

        End If

    End Sub ' end of method ExecutePlayerTurn

    Private Sub MainForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim a_strRegistryValues() As String, _
            backgroundKey As RegistryKey ' registry key of the
        ' Background files counters for columns and rows
        Dim bSuccess As Boolean, _
            bytCount As Byte
        Const bytLastColumn As Byte = _
            Parameters.bytColumns - 1, _
            bytLastRow As Byte = Parameters.bytRows - 1
        Dim readBackground As Background, _
            strBackgroundFileName As String

        backgroundKey = Registry.CurrentUser.CreateSubKey( _
            Parameters.strApplicationRegistryKey & _
            RegistryStream.keySeparator & _
            Parameters.strBackgroundRegistryKey)
        a_strRegistryValues = backgroundKey.GetValueNames()

        If itsOptions.iItsBackground < _
            a_strRegistryValues.Length Then ' if the preferred
            ' background exists in the set of available
            ' backgrounds

            readBackground = RegistryStream.Read(backgroundKey, _
                a_strRegistryValues(itsOptions.iItsBackground))
            strBackgroundFileName = _
                Parameters.strBackgroundFolder & _
                Path.DirectorySeparatorChar & _
                readBackground.strFileName

            If File.Exists(strBackgroundFileName) Then ' If the
                ' background file really exists, load it to the
                ' form.

                Try

                    imgBackground.Image = _
                        Image.FromFile(strBackgroundFileName)
                    ' Center the background in the form.
                    imgBackground.Left = (ClientSize.Width - _
                        imgBackground.Width) / 2
                    imgBackground.Top = (ClientSize.Height - _
                        imgBackground.Height - _
                        statusLine.Height) / 2

                    For bytCount = 0 To bytLastColumn ' Calculate
                        ' the positions of tile columns.

                        a_itsHorizontalTicks(bytCount) = _
                            readBackground.clearance.X + _
                            bytCount * (itsTileDiameter + 11)

                    Next bytCount

                    For bytCount = 0 To bytLastRow ' Calculate
                        ' the positions of tile rows.

                        a_itsVerticalTicks(bytCount) = _
                            readBackground.clearance.Y + _
                            (bytLastRow - bytCount) * _
                            (itsTileDiameter + 11)

                    Next bytCount

                    bSuccess = True

                Catch ex As SystemException
                End Try

            End If

        End If

        If Not bSuccess Then ' if the options couldn't be
            ' successfully loaded

            MessageBox.Show("invalid background file. The " & _
                "background file can't be loaded. " & _
                Parameters.strSolution, Text, _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit() ' Quit the game.

        End If

    End Sub ' end of method frmMain_Load

    Private Sub imgBackground_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles imgBackground.Paint
        Dim a_brushes([Enum].GetValues( _
            GetType(player)).Length - 1) As SolidBrush ' brushes
        ' for drawing tiles
        Dim bytColumnCount, _
            bytLastTile As Byte ' topmost utilized tile in a
        ' column
        Const bytLastColumn = Parameters.bytColumns - 1
        Dim emptyBrush As New SolidBrush(emptyTileColor), _
            tileArea As New Rectangle(New Point(0, 0), _
            New Size(itsTileDiameter, itsTileDiameter)), _
            iLastFixedTile As Integer ' topmost tile having a
        ' fixed color in a column
        Dim rowCount As Short

        ' Create the brushes for drawing user and computer tiles.
        a_brushes(player.minimizing) = New SolidBrush( _
            itsOptions.a_itsTileColors(player.minimizing))
        a_brushes(player.maximizing) = New SolidBrush( _
            itsOptions.a_itsTileColors(player.maximizing))

        For bytColumnCount = 0 To bytLastColumn ' Draw columns of
            ' tiles

            tileArea.X = a_itsHorizontalTicks(bytColumnCount)
            rowCount = 0

            ' Test if the column isn't empty.
            If Not (a_itsCurrentState Is Nothing) AndAlso Not ( _
            a_itsCurrentState(bytColumnCount) Is Nothing) Then

                bytLastTile = _
                    a_itsCurrentState(bytColumnCount).Count - 1
                iLastFixedTile = bytLastTile - 1

                For rowCount = 0 To iLastFixedTile ' Draw all
                    ' the tiles except the last one.

                    tileArea.Y = a_itsVerticalTicks(rowCount)
                    e.Graphics.FillEllipse( _
                        a_brushes(a_itsCurrentState( _
                        bytColumnCount)(rowCount)), tileArea)

                Next rowCount

                ' Draw the last tile in the column taking into
                ' consideration whether it is flashing or not.
                tileArea.Y = a_itsVerticalTicks(bytLastTile)
                e.Graphics.FillEllipse( _
                    IIf(bytItsFlashingColumn = bytColumnCount, _
                    New SolidBrush(itsFlashingTileColor), _
                    a_brushes(a_itsCurrentState( _
                    bytColumnCount)(bytLastTile))), tileArea)
                rowCount = _
                    a_itsCurrentState(bytColumnCount).Count

            End If

            Do While rowCount < Parameters.bytRows ' Draw the
                ' remaining (unutilized) part of the column.

                tileArea.Y = a_itsVerticalTicks(rowCount)
                e.Graphics.FillEllipse(emptyBrush, tileArea)
                rowCount += 1

            Loop

        Next bytColumnCount

    End Sub ' end of method imgBackground_Paint

    Private Sub mnuGameExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGameExit.Click

        Close()

    End Sub ' end of method mnuGameExit_Click

    Private Sub mnuGameNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuGameNew.Click

        ' If a current game is running, verify that the user wants
        ' to ignore this game and start a new one.
        If Not (a_itsCurrentState Is Nothing) AndAlso _
            MessageBox.Show( _
            "Are you sure you want to start a new game?", Text, _
            MessageBoxButtons.YesNo, MessageBoxIcon.Question, _
            MessageBoxDefaultButton.Button2) = _
            DialogResult.Yes Then

            a_itsCurrentState = Nothing
            imgBackground.Invalidate()

        End If

        If a_itsCurrentState Is Nothing Then

#If TRACE Then
            DisconnectLogFile()

#End If
            If MessageBox.Show( _
                "Do you want to play the first turn?", Text, _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question) = DialogResult.Yes Then

                itsBeginner = player.minimizing
                ToggleUserInterface(True)

            Else

                itsBeginner = player.maximizing
                ToggleUserInterface(False) ' Disable the user
                ' interface.
                ComputerTurn() ' Allow the computer to play.

            End If

        End If

    End Sub ' end of method mnuGameNew_Click

    Private Sub mnuHelpAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuHelpAbout.Click
        Dim frmAbout As New AboutForm

        frmAbout.ShowDialog()

    End Sub ' end of method mnuHelpAbout_Click

    Private Sub mnuOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOptions.Click
        Dim frmOptions As New OptionForm(itsOptions)

        If frmOptions.ShowDialog() = DialogResult.OK Then

            itsOptions.Save( _
                Parameters.strApplicationRegistryKey & _
                RegistryStream.keySeparator & _
                Parameters.strOptionsRegistryKey)
            MainForm_Load(sender, e)
            imgBackground.Invalidate()

        End If

    End Sub ' end of method mnuOptions_Click

    Private Sub ToggleUserInterface(ByVal bState As Boolean)

        imgBackground.Enabled = bState
        Cursor = IIf(bState, Cursors.Default, Cursors.WaitCursor)

    End Sub ' end of method ToggleUserInterface

    Private Sub ValidatePreferredBackground()

        If itsOptions.iItsBackground < 0 Then ' if the preferred
            ' background is invalid

            MessageBox.Show("invalid background option" & _
                Parameters.strSolution, Text, _
                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit() ' Quit the game.

        End If

    End Sub ' end of method ValidatePreferredBackground
    Private a_itsCurrentState() As ArrayList = Nothing ' holds
    ' the utilization of the game board
    Private a_itsHorizontalTicks( _
        Parameters.bytColumns - 1) As Short ' horizontal
    ' positions at which tile columns are drawn
    ' vertical positions at which tile rows are drawn
    Private a_itsVerticalTicks(Parameters.bytRows - 1) As Short
    Private bItsGoal As Boolean ' indicates whether the last tile
    ' has achieved the goal for any of the players
    Private bytItsFlashingColumn As Byte ' the column having the
    ' currently flashing tile
    Private Shared ReadOnly emptyTileColor As Color = _
        Color.White ' color of an empty tile
    Private itsBeginner As player ' the player who played the
    ' first turn in the current game
    Private iItsBlinkingStep As Integer ' the step by which the
    ' color of a tile gradually changes
    Private itsFlashingTileColor As Color ' the current color of
    ' the flashing tile
#If TRACE Then
    Private itsLogFile As StreamWriter = Nothing ' log file to
    ' store tracing information
#End If
    Private itsOptions As New Options ' game options
    Private Const itsTileDiameter As Integer = 53 ' diameter of a
    ' tile
End Class ' end of class frmMain
