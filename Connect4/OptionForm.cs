/************************************************************

program:      Connect4

file:         OptionForm.cs

function:     methods of the OptionForm class

description:  displays/changes the connect4 game options

author:       Mohammed Safwat (MS)

environment:  visual studio.net enterprise architect 2003, windows xp
			  professional

notes:        This is a translated faculy program.

revisions:    2.5  7/11/2005 (MS) starting construction

************************************************************/
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Connect4
{
	/// <summary>
	/// Summary description for OptionForm.
	/// </summary>
	public class OptionForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// overloaded constructors
		public OptionForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public OptionForm(Options currentOptions): this()
		{
			const byte maximizing = (byte)(player.maximizing),
					  minimizing = (byte)(player.minimizing);

			itsOptions = currentOptions;
			// Set the initial colors of the sample tiles.
			itsColors[minimizing] = currentOptions.itsTileColors[minimizing];
			itsColors[maximizing] = currentOptions.itsTileColors[maximizing];

		}// end of constructor OptionForm

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.optionPanel = new System.Windows.Forms.Panel();
			this.backgroundImage = new System.Windows.Forms.PictureBox();
			this.backgroundList = new System.Windows.Forms.ListBox();
			this.backgroundLabel = new System.Windows.Forms.Label();
			this.levelComboBox = new System.Windows.Forms.ComboBox();
			this.difficultyLabel = new System.Windows.Forms.Label();
			this.computerColorButton = new System.Windows.Forms.Button();
			this.computerLabel = new System.Windows.Forms.Label();
			this.userColorButton = new System.Windows.Forms.Button();
			this.userLabel = new System.Windows.Forms.Label();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.OKButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.optionPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// optionPanel
			// 
			this.optionPanel.Controls.Add(this.backgroundImage);
			this.optionPanel.Controls.Add(this.backgroundList);
			this.optionPanel.Controls.Add(this.backgroundLabel);
			this.optionPanel.Controls.Add(this.levelComboBox);
			this.optionPanel.Controls.Add(this.difficultyLabel);
			this.optionPanel.Controls.Add(this.computerColorButton);
			this.optionPanel.Controls.Add(this.computerLabel);
			this.optionPanel.Controls.Add(this.userColorButton);
			this.optionPanel.Controls.Add(this.userLabel);
			this.optionPanel.Location = new System.Drawing.Point(10, 8);
			this.optionPanel.Name = "optionPanel";
			this.optionPanel.Size = new System.Drawing.Size(427, 185);
			this.optionPanel.TabIndex = 0;
			this.optionPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.optionPanel_Paint);
			// 
			// backgroundImage
			// 
			this.backgroundImage.Location = new System.Drawing.Point(312, 68);
			this.backgroundImage.Name = "backgroundImage";
			this.backgroundImage.Size = new System.Drawing.Size(105, 97);
			this.backgroundImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.backgroundImage.TabIndex = 8;
			this.backgroundImage.TabStop = false;
			// 
			// backgroundList
			// 
			this.backgroundList.Location = new System.Drawing.Point(192, 68);
			this.backgroundList.Name = "backgroundList";
			this.backgroundList.Size = new System.Drawing.Size(105, 95);
			this.backgroundList.TabIndex = 7;
			this.backgroundList.SelectedIndexChanged += new System.EventHandler(this.backgroundList_SelectedIndexChanged);
			// 
			// backgroundLabel
			// 
			this.backgroundLabel.AutoSize = true;
			this.backgroundLabel.Location = new System.Drawing.Point(192, 40);
			this.backgroundLabel.Name = "backgroundLabel";
			this.backgroundLabel.Size = new System.Drawing.Size(110, 16);
			this.backgroundLabel.TabIndex = 6;
			this.backgroundLabel.Text = "Choose Background:";
			// 
			// levelComboBox
			// 
			this.levelComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.levelComboBox.Location = new System.Drawing.Point(78, 144);
			this.levelComboBox.Name = "levelComboBox";
			this.levelComboBox.Size = new System.Drawing.Size(56, 21);
			this.levelComboBox.TabIndex = 5;
			// 
			// difficultyLabel
			// 
			this.difficultyLabel.Location = new System.Drawing.Point(6, 136);
			this.difficultyLabel.Name = "difficultyLabel";
			this.difficultyLabel.Size = new System.Drawing.Size(64, 33);
			this.difficultyLabel.TabIndex = 4;
			this.difficultyLabel.Text = "Foresight Perception:";
			// 
			// computerColorButton
			// 
			this.computerColorButton.Location = new System.Drawing.Point(152, 84);
			this.computerColorButton.Name = "computerColorButton";
			this.computerColorButton.Size = new System.Drawing.Size(25, 25);
			this.computerColorButton.TabIndex = 3;
			this.computerColorButton.Text = "...";
			this.computerColorButton.Click += new System.EventHandler(this.ChangeColor);
			// 
			// computerLabel
			// 
			this.computerLabel.AutoSize = true;
			this.computerLabel.Location = new System.Drawing.Point(6, 84);
			this.computerLabel.Name = "computerLabel";
			this.computerLabel.Size = new System.Drawing.Size(87, 16);
			this.computerLabel.TabIndex = 2;
			this.computerLabel.Text = "Computer Color:";
			// 
			// userColorButton
			// 
			this.userColorButton.Location = new System.Drawing.Point(152, 24);
			this.userColorButton.Name = "userColorButton";
			this.userColorButton.Size = new System.Drawing.Size(25, 25);
			this.userColorButton.TabIndex = 1;
			this.userColorButton.Text = "...";
			this.userColorButton.Click += new System.EventHandler(this.ChangeColor);
			// 
			// userLabel
			// 
			this.userLabel.AutoSize = true;
			this.userLabel.Location = new System.Drawing.Point(6, 24);
			this.userLabel.Name = "userLabel";
			this.userLabel.Size = new System.Drawing.Size(61, 16);
			this.userLabel.TabIndex = 0;
			this.userLabel.Text = "User Color:";
			// 
			// OKButton
			// 
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKButton.Location = new System.Drawing.Point(136, 212);
			this.OKButton.Name = "OKButton";
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(236, 212);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			// 
			// OptionForm
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(446, 252);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.optionPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionForm";
			this.ShowInTaskbar = false;
			this.Text = "OptionForm";
			this.Load += new System.EventHandler(this.OptionForm_Load);
			this.optionPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void backgroundList_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string backgroundFileName =
				Parameters.backgroundFolder + Path.DirectorySeparatorChar +
				itsBackgrounds[backgroundList.SelectedIndex].fileName;

			if (File.Exists(backgroundFileName))// if the background file really exists
				backgroundImage.Image = Image.FromFile(backgroundFileName);// Load the
				// background.
			else
			{

				MessageBox.Show(
					"missing background file. The background can't be loaded.",
					Owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);

				// Undo the user trial to change the current background.
				if (backgroundList.SelectedIndex != itsOptions.itsBackground)
					backgroundList.SelectedIndex = itsOptions.itsBackground;

			}

		}// end of method backgroundList_SelectedIndexChanged

		private void ChangeColor(object sender, System.EventArgs e)
		{
			byte colorOwner = (byte)(player)(((Button)sender).Tag);

			colorDialog.Color = itsColors[colorOwner];

			if (colorDialog.ShowDialog() == DialogResult.OK)
				itsColors[colorOwner] = colorDialog.Color;

			optionPanel.Invalidate();

		}// end of method ChangeColor

		private void OKButton_Click(object sender, System.EventArgs e)
		{
			const byte maximizing = (byte)(player.maximizing),
					  minimizing = (byte)(player.minimizing);

			itsOptions.itsBackground = (sbyte)backgroundList.SelectedIndex;
			itsOptions.itsLevel = (byte)levelComboBox.SelectedItem;
			// Save the chosen colors of the tiles.
			itsOptions.itsTileColors[maximizing] = itsColors[maximizing];
			itsOptions.itsTileColors[minimizing] = itsColors[minimizing];

		}// end of method OKButton_Click

		private void OptionForm_Load(object sender, System.EventArgs e)
		{
			RegistryKey backgroundSourceKey =
				Registry.CurrentUser.OpenSubKey(
				Parameters.applicationRegistryKey +
				RegistryStream.keySeparator +
				Parameters.backgroundRegistryKey);
			byte count = 1;

			// Load the level combo box with the available difficulty
			// levels.
			levelComboBox.Hide();// Hide the level combo box before
				// filling it with available difficulty levels to
				// overcome the flicker that may happen.

			for (; count <= Options.maxDifficulty; count++)
				levelComboBox.Items.Add(count);

			levelComboBox.Show();
			levelComboBox.SelectedItem = itsOptions.itsLevel;
			backgroundList.Items.AddRange(
				backgroundSourceKey.GetValueNames());// Load the
				// names of the backgrounds from the open registry key
				// to the list box.
			itsBackgrounds =
				new Background[backgroundList.Items.Count];// Size the
				// backgrounds array as needed.

			// Load the data of backgrounds from the registry to the
			// array of backgrounds.
			for (count = 0; count < itsBackgrounds.Length; count++)
				itsBackgrounds[count] = (Background)(
					RegistryStream.Read(backgroundSourceKey,
					(string)(backgroundList.Items[count])));

			backgroundList.SelectedIndex = itsOptions.itsBackground;
			// Set the identities of the color change buttons.
			userColorButton.Tag = player.minimizing;
			computerColorButton.Tag = player.maximizing;

		}// end of method OptionForm_Load

		private void optionPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			const byte sampleDiameter = 49;
			Rectangle sampleArea = new Rectangle(new Point(92, 12),
				new Size(sampleDiameter, sampleDiameter));

			// Draw the user tile sample.
			e.Graphics.FillEllipse(new SolidBrush(
				itsColors[(int)(player.minimizing)]), sampleArea);
			// Draw the computer tile sample.
			sampleArea.Y = 72;
			e.Graphics.FillEllipse(new SolidBrush(
				itsColors[(int)(player.maximizing)]), sampleArea);

		}// end of method optionPanel_Paint

		private System.Windows.Forms.Panel optionPanel;
		private System.Windows.Forms.Label userLabel;
		private System.Windows.Forms.Button userColorButton;
		private System.Windows.Forms.Label difficultyLabel;
		private System.Windows.Forms.ComboBox levelComboBox;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.Label backgroundLabel;
		private System.Windows.Forms.ListBox backgroundList;
		private System.Windows.Forms.PictureBox backgroundImage;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button computerColorButton;
		private System.Windows.Forms.Label computerLabel;

		private Background[] itsBackgrounds;
		// currently chosen colors for user and computer tiles
		private Color[] itsColors = new Color[Enum.GetValues(typeof(player)).Length];
		/// <summary>
		/// game options
		/// </summary>
		private Options itsOptions;
	}// end of class OptionForm
}
