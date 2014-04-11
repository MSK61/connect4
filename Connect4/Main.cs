/************************************************************

program:      Connect4

file:         Main.cs

function:     methods of the MainForm class

description:  runs a connect4 game between the computer and the user

author:       Mohammed Safwat (MS)

environment:  visual studio.net enterprise architect 2003, windows xp
			  professional

notes:        This is a translated faculy program.

revisions:    2.5  7/11/2005 (MS) starting construction
			  2.54 21/11/2005 (MS) first GUI release

************************************************************/
using Connect4;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Data;

namespace Connect4
{
	/// <summary>
	/// Summary description for MianForm.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			itsOptions.Load(Parameters.applicationRegistryKey +
				RegistryStream.keySeparator + Parameters.optionsRegistryKey);
			ValidatePreferredBackground();
			itsFlashingColumn = Parameters.columns;// No column has to be flashed
				// currently.

		}// end of constructor MainForm

#if TRACE
		~MainForm()
		{

			if (itsLogFile != null) itsLogFile.Close();

		}// end of destructor ~MainForm

#endif
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
			this.mainMenu = new System.Windows.Forms.MainMenu();
			this.gameMenu = new System.Windows.Forms.MenuItem();
			this.gameNewItem = new System.Windows.Forms.MenuItem();
			this.gameMenuSeparator = new System.Windows.Forms.MenuItem();
			this.gameExitItem = new System.Windows.Forms.MenuItem();
			this.optionsMenu = new System.Windows.Forms.MenuItem();
			this.helpMenu = new System.Windows.Forms.MenuItem();
			this.helpAboutItem = new System.Windows.Forms.MenuItem();
			this.backgroundImage = new System.Windows.Forms.PictureBox();
			this.statusLine = new System.Windows.Forms.StatusBar();
			this.blinker = new FiniteRepeatTimer.FiniteRepeatTimer();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.gameMenu,
																					 this.optionsMenu,
																					 this.helpMenu});
			// 
			// gameMenu
			// 
			this.gameMenu.Index = 0;
			this.gameMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.gameNewItem,
																					 this.gameMenuSeparator,
																					 this.gameExitItem});
			this.gameMenu.Text = "&Game";
			// 
			// gameNewItem
			// 
			this.gameNewItem.Index = 0;
			this.gameNewItem.Text = "&New Game";
			this.gameNewItem.Click += new System.EventHandler(this.gameNewItem_Click);
			// 
			// gameMenuSeparator
			// 
			this.gameMenuSeparator.Index = 1;
			this.gameMenuSeparator.Text = "-";
			// 
			// gameExitItem
			// 
			this.gameExitItem.Index = 2;
			this.gameExitItem.Text = "E&xit";
			this.gameExitItem.Click += new System.EventHandler(this.gameExitItem_Click);
			// 
			// optionsMenu
			// 
			this.optionsMenu.Index = 1;
			this.optionsMenu.Text = "&Options";
			this.optionsMenu.Click += new System.EventHandler(this.optionsMenu_Click);
			// 
			// helpMenu
			// 
			this.helpMenu.Index = 2;
			this.helpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.helpAboutItem});
			this.helpMenu.Text = "&Help";
			// 
			// helpAboutItem
			// 
			this.helpAboutItem.Index = 0;
			this.helpAboutItem.Text = "&About...";
			this.helpAboutItem.Click += new System.EventHandler(this.helpAboutItem_Click);
			// 
			// backgroundImage
			// 
			this.backgroundImage.Location = new System.Drawing.Point(0, 0);
			this.backgroundImage.Name = "backgroundImage";
			this.backgroundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.backgroundImage.TabIndex = 0;
			this.backgroundImage.TabStop = false;
			this.backgroundImage.Paint += new System.Windows.Forms.PaintEventHandler(this.backgroundImage_Paint);
			this.backgroundImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ExecutePlayerTurn);
			// 
			// statusLine
			// 
			this.statusLine.Location = new System.Drawing.Point(0, 250);
			this.statusLine.Name = "statusLine";
			this.statusLine.Size = new System.Drawing.Size(292, 22);
			this.statusLine.TabIndex = 1;
			// 
			// blinker
			// 
			this.blinker.Interval = 50;
			this.blinker.Repetitions = ((short)(3));
			this.blinker.Tick += new System.EventHandler(this.blinker_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.BackColor = System.Drawing.Color.Green;
			this.ClientSize = new System.Drawing.Size(292, 272);
			this.Controls.Add(this.statusLine);
			this.Controls.Add(this.backgroundImage);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu;
			this.Name = "MainForm";
			this.Text = "Connect 4";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{

			Options.EnsureValidRegistryData(Parameters.applicationRegistryKey +
				RegistryStream.keySeparator + Parameters.optionsRegistryKey);
			Application.Run(new MainForm());

		}// end of method Main

		/// <summary>
		/// This method simulates a tile throw for the specified player.
		/// </summary>
		private void ApplyTurn(byte column, player owner)
		{

#if ! TRACE
			// If the board is empty, allocate a new board.
#endif
			if (itsCurrentState ==
#if TRACE
				null)// if the board is empty
			{

				// Allocate a new board.
#else
				null)
#endif
				itsCurrentState = new ArrayList[Parameters.columns];
#if TRACE
				// Open a log file for tracing.
				itsLogFile = new StreamWriter("traceSharp.log");
				itsLogFile.AutoFlush = true;

			}
#endif

			if (itsCurrentState[column] == null)// if the column is
				// empty
				itsCurrentState[column] =
					new ArrayList(Parameters.rows);// Create the tile
				// column.

			itsCurrentState[column].Add(owner);
#if TRACE
			itsLogFile.WriteLine(((owner == player.maximizing) ?
				"computer" : "user") + ": " + column);// Dump this
				// throw to the log file.
#endif
			// Adjust the flashing parameters.
			itsBlinkingStep = (itsOptions.itsTileColors[(int)(owner)].ToArgb() -
				emptyTileColor.ToArgb()) / blinker.Repetitions;
			itsFlashingColumn = column;
			itsFlashingTileColor = emptyTileColor;
			blinker.Enabled = true;// Enable the blinking(flashing) timer.

		}// end of method ApplyTurn

		private void backgroundImage_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			// brushes for drawing tiles
			SolidBrush[] brushes = new SolidBrush[Enum.GetValues(typeof(player)).Length];
			// counters for rows and columns
			byte columnCount = 0,
				lastTile,// topmost utilized tile in a column
				rowCount;
			SolidBrush emptyBrush = new SolidBrush(emptyTileColor);
			const byte maximizing = (byte)(player.maximizing),
					  minimizing = (byte)(player.minimizing);
			Rectangle tileArea = new Rectangle(
				new Point(0, 0), new Size(itsTileDiameter, itsTileDiameter));

			// Create the brushes for drawing user and computer tiles.
			brushes[minimizing] = new SolidBrush(itsOptions.itsTileColors[minimizing]);
			brushes[maximizing] = new SolidBrush(itsOptions.itsTileColors[maximizing]);

			for (; columnCount < Parameters.columns; columnCount++)// Draw columns of
				// tiles
			{

				tileArea.X = itsHorizontalTicks[columnCount];
				rowCount = 0;

				if (itsCurrentState != null &&
					itsCurrentState[columnCount] != null)// if the
					// column isn't empty
				{

					lastTile = (byte)(
						itsCurrentState[columnCount].Count - 1);

					for (; rowCount < lastTile; rowCount++)// Draw all
						// the tiles except the last one.
					{

						tileArea.Y = itsVerticalTicks[rowCount];
						e.Graphics.FillEllipse(
							brushes[(int)(itsCurrentState[
							columnCount][rowCount])], tileArea);

					}

					// Draw the last tile in the column taking into
					// consideration whether it is flashing or not.
					tileArea.Y = itsVerticalTicks[lastTile];
					e.Graphics.FillEllipse(
						(itsFlashingColumn == columnCount) ?
						new SolidBrush(itsFlashingTileColor) :
						brushes[(int)(itsCurrentState[columnCount][
						lastTile])], tileArea);
					rowCount =
						(byte)(itsCurrentState[columnCount].Count);

				}

				for (; rowCount < Parameters.rows; rowCount++)// Draw the remaining
					// (unutilized) part of the column.
				{

					tileArea.Y = itsVerticalTicks[rowCount];
					e.Graphics.FillEllipse(emptyBrush, tileArea);

				}

			}

		}// end of method backgroundImage_Paint

		private void blinker_Tick(object sender, System.EventArgs e)
		{
			player flashingPlayer;

			if (blinker.CurrentTrigger == blinker.Repetitions)// if this is the last color
				// change
			{

				flashingPlayer = (player)(itsCurrentState[itsFlashingColumn][
					itsCurrentState[itsFlashingColumn].Count - 1]);
				itsFlashingColumn = Parameters.columns;// Give the current tile its final
					// color.
				backgroundImage.Invalidate();

				if (flashingPlayer == player.minimizing) ComputerTurn();// Allow the
					// computer to play.
				else
				{
					
					if (itsGoal)// If the computer has won, get ready
						// to start a new game.
					{

#if TRACE
						DisconnectLogFile();
#endif
						MessageBox.Show("The computer wins.", Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Information);
						itsCurrentState = null;// Get ready for a new
							// game.

					}

				ToggleUserInterface(true);// Enable the user interface.

				}

			}
			else// if this is a gradual color change
			{
				
				itsFlashingTileColor =
					Color.FromArgb(itsFlashingTileColor.ToArgb() + itsBlinkingStep);
				backgroundImage.Invalidate();

			}

		}// end of method blinker_Tick

		/// <summary>
		/// This method allows the computer to think and throw a tile.
		/// </summary>
		private void ComputerTurn()
		{
			byte nextStep;
			AlphaBetaTree tree = new AlphaBetaTree(itsOptions.itsLevel, itsCurrentState);
			string userMessage;

			statusLine.Text = "analyzing...";
			tree.ApplyAlphaBeta(out itsGoal, out nextStep, itsBeginner);
			statusLine.Text = "";

			if (nextStep == Parameters.columns)// if no next step is valid
			{

#if TRACE
				DisconnectLogFile();
#endif
				userMessage = itsGoal ?
					"You win." : "The game can't be continued any more. No one wins.";
				MessageBox.Show(
					userMessage, Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
				itsCurrentState = null;
				ToggleUserInterface(true);// Enable the user
					// interface.

			}
			else ApplyTurn(nextStep, player.maximizing);// Show the visual effect of the
				// computer's turn.

		}// end of method ComputerTurn

#if TRACE
		/// <summary>
		/// This method closes the associated tracing log file.
		/// </summary>
		private void DisconnectLogFile()
		{

			if (itsLogFile != null)
			{

				itsLogFile.Close();
				itsLogFile = null;

			}

		}// end of method DisconnectLogFile

#endif
		private void ExecutePlayerTurn(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			sbyte count;
			short relativeMouse;// position of the mouse relative to the left edge of a
				// tile column

			if (e.Button == MouseButtons.Left)// if a left click is detected
			{

				// Search for the column within which the mouse was clicked.
				for (count = Parameters.columns - 1, relativeMouse = (short)(
					e.X - itsHorizontalTicks[count]); count >= 0 && relativeMouse < 0;
					relativeMouse = (short)(e.X - itsHorizontalTicks[--count]));

				if (count >= 0 && relativeMouse < itsTileDiameter)// if the click was
					// determined to be within the boundaries of a column
					// If the column is full, notify the user.
					if (itsCurrentState != null &&
						itsCurrentState[count] != null &&
						itsCurrentState[count].Count ==
						Parameters.rows)
						MessageBox.Show("invalid throw. This column" +
							" is full. Please, try again.", Text,
							MessageBoxButtons.OK,
							MessageBoxIcon.Error);
					else// if the throw is valid
					{

						ToggleUserInterface(false);// Disable the user interface.
						// Show its visual effect.
						ApplyTurn((byte)count, player.minimizing);

					}

			}

		}// end of method ExecutePlayerTurn

		private void gameExitItem_Click(object sender, System.EventArgs e)
		{

			Close();

		}// end of method gameExitItem_Click

		private void gameNewItem_Click(object sender, System.EventArgs e)
		{

			// If a current game is running, verify that the user wants to ignore this
			// game and start a new one.
			if (itsCurrentState != null && MessageBox.Show(
				"Are you sure you want to start a new game?", Text,
				MessageBoxButtons.YesNo, MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{

				itsCurrentState = null;
				backgroundImage.Invalidate();

			}

			if (itsCurrentState == null)
			{

#if TRACE
				DisconnectLogFile();

#endif
				if (MessageBox.Show("Do you want to play the first turn?", Text,
					MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					
					itsBeginner = player.minimizing;
					ToggleUserInterface(true);
				
				}
				else
				{
					
					itsBeginner = player.maximizing;
					ToggleUserInterface(false);// Disable the user interface.
					ComputerTurn();// Allow the computer to play.
				
				}

			}

		}// end of method gameNewItem_Click

		private void helpAboutItem_Click(object sender, System.EventArgs e)
		{
			AboutForm aboutDialog = new AboutForm();

			aboutDialog.ShowDialog();

		}// end of method helpAboutItem_Click

		private void MainForm_Load(object sender, System.EventArgs e)
		{
			string backgroundFileName;
			RegistryKey backgroundKey;// registry key of the background files
			// counters for columns and rows
			byte count;
			const byte lastRow = Parameters.rows - 1;
			Background readBackground;
			string[] registryValues;
			bool success = false;

			backgroundKey =
				Registry.CurrentUser.CreateSubKey(Parameters.applicationRegistryKey +
				RegistryStream.keySeparator + Parameters.backgroundRegistryKey);
			registryValues = backgroundKey.GetValueNames();

			if (itsOptions.itsBackground < registryValues.Length)// if the preferred
				// background exists in the set of available backgrounds
			{

				readBackground = (Background)(RegistryStream.Read(
					backgroundKey, registryValues[itsOptions.itsBackground]));
				backgroundFileName = Parameters.backgroundFolder +
					Path.DirectorySeparatorChar + readBackground.fileName;

				if (File.Exists(backgroundFileName))// If the background file really
					// exists, load it to the form.
				{

					try
					{

						backgroundImage.Image = Image.FromFile(backgroundFileName);
						// Center the background in the form.
						backgroundImage.Left =
							(ClientSize.Width - backgroundImage.Width) / 2;
						backgroundImage.Top = (ClientSize.Height -
							backgroundImage.Height - statusLine.Height) / 2;

						// Calculate the positions of tile columns.
						for (count = 0; count < Parameters.columns; count++)
							itsHorizontalTicks[count] =
								(ushort)(readBackground.clearance.X +
								count * (itsTileDiameter + 11));

						// Calculate the positions of tile rows.
						for (count = 0; count < Parameters.rows; count++)
							itsVerticalTicks[count] =
								(ushort)(readBackground.clearance.Y +
								(lastRow - count) * (itsTileDiameter + 11));

						success = true;

					}
					catch (SystemException)
					{
					}

				}

			}

			if (! success)// if the options couldn't be successfully loaded
			{

				MessageBox.Show("invalid background file. " +
					"The background file can't be loaded. " + Parameters.solution,
					Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();// Quit the game.

			}

		}// end of method MainForm_Load

		private void optionsMenu_Click(object sender, System.EventArgs e)
		{
			OptionForm optionDialog = new OptionForm(itsOptions);

			if (optionDialog.ShowDialog() == DialogResult.OK)
			{

				itsOptions.Save(Parameters.applicationRegistryKey +
					RegistryStream.keySeparator + Parameters.optionsRegistryKey);
				MainForm_Load(sender, e);
				backgroundImage.Invalidate();

			}

		}// end of method optionsMenu_Click

		/// <summary>
		/// This method enables/disables the user interface and changes the shape of the mouse accordingly.
		/// </summary>
		private void ToggleUserInterface(bool state)
		{

			backgroundImage.Enabled = state;
			Cursor = state ? Cursors.Default : Cursors.WaitCursor;

		}// end of method ToggleUserInterface

		/// <summary>
		/// This method validates the preferred background chosen by the user.
		/// </summary>
		private void ValidatePreferredBackground()
		{

			if (itsOptions.itsBackground < 0)// if the preferred background is invalid
			{

				MessageBox.Show("invalid background option" + Parameters.solution, Text,
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();// Quit the game.

			}

		}// end of method ValidatePreferredBackground

		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem gameMenu;
		private System.Windows.Forms.MenuItem gameNewItem;
		private System.Windows.Forms.MenuItem gameMenuSeparator;
		private System.Windows.Forms.MenuItem gameExitItem;
		private System.Windows.Forms.MenuItem optionsMenu;
		private System.Windows.Forms.MenuItem helpMenu;
		private System.Windows.Forms.MenuItem helpAboutItem;
		private System.Windows.Forms.PictureBox backgroundImage;
		private System.Windows.Forms.StatusBar statusLine;
		private FiniteRepeatTimer.FiniteRepeatTimer blinker;

		/// <summary>
		/// color of an empty tile
		/// </summary>
		private static readonly Color emptyTileColor = Color.White;
		/// <summary>
		/// the player who played the first turn in the current game
		/// </summary>
		private player itsBeginner;
		/// <summary>
		/// the step by which the color of a tile gradually changes
		/// </summary>
		private int itsBlinkingStep;
		private ArrayList[] itsCurrentState = null;// holds the
			// utilization of the game board
		/// <summary>
		/// the column having the currently flashing tile
		/// </summary>
		private byte itsFlashingColumn;
		/// <summary>
		/// the current color of the flashing tile
		/// </summary>
		private Color itsFlashingTileColor;
		/// <summary>
		/// indicates whether the last tile has achieved the goal for any of the players
		/// </summary>
		private bool itsGoal;
		private ushort[] itsHorizontalTicks = new ushort[Parameters.columns];// horizontal
			// positions at which tile columns are drawn
#if TRACE
		/// <summary>
		/// log file to store tracing information
		/// </summary>
		private StreamWriter itsLogFile = null;
#endif
		/// <summary>
		/// game options
		/// </summary>
		private Options itsOptions = new Options();
		/// <summary>
		/// diameter of a tile
		/// </summary>
		private const ushort itsTileDiameter = 53;
		// vertical positions at which tile rows are drawn
		private ushort[] itsVerticalTicks = new ushort[Parameters.rows];
	}// end of class MainForm
}
