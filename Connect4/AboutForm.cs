/************************************************************

program:      Connect4

file:         AboutForm.cs

function:     methods of the AboutForm class

description:  displays the connect4 game information

author:       Mohammed Safwat (MS)

environment:  visual studio.net enterprise architect 2003, windows xp
			  professional

notes:        This is a translated faculy program.

revisions:    2.5  20/11/2005 (MS) starting construction
			  2.56 8/1/2006   (MS) changing some control names to
			  meaningful names

************************************************************/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Connect4
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Label productName;
		private System.Windows.Forms.PictureBox programIcon;
		private System.Windows.Forms.Label version;
		private System.Windows.Forms.Label copyright;
		private System.Windows.Forms.Label comments;
		private System.Windows.Forms.Panel gameDataPanel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
			this.gameDataPanel = new System.Windows.Forms.Panel();
			this.comments = new System.Windows.Forms.Label();
			this.copyright = new System.Windows.Forms.Label();
			this.version = new System.Windows.Forms.Label();
			this.programIcon = new System.Windows.Forms.PictureBox();
			this.productName = new System.Windows.Forms.Label();
			this.OKButton = new System.Windows.Forms.Button();
			this.gameDataPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// gameDataPanel
			// 
			this.gameDataPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.gameDataPanel.Controls.Add(this.comments);
			this.gameDataPanel.Controls.Add(this.copyright);
			this.gameDataPanel.Controls.Add(this.version);
			this.gameDataPanel.Controls.Add(this.programIcon);
			this.gameDataPanel.Controls.Add(this.productName);
			this.gameDataPanel.Location = new System.Drawing.Point(10, 8);
			this.gameDataPanel.Name = "gameDataPanel";
			this.gameDataPanel.Size = new System.Drawing.Size(281, 161);
			this.gameDataPanel.TabIndex = 0;
			// 
			// comments
			// 
			this.comments.AutoSize = true;
			this.comments.Location = new System.Drawing.Point(8, 128);
			this.comments.Name = "comments";
			this.comments.Size = new System.Drawing.Size(159, 16);
			this.comments.TabIndex = 3;
			this.comments.Text = "Comments: All rights reserved.";
			// 
			// copyright
			// 
			this.copyright.Location = new System.Drawing.Point(8, 80);
			this.copyright.Name = "copyright";
			this.copyright.Size = new System.Drawing.Size(220, 40);
			this.copyright.TabIndex = 2;
			this.copyright.Text = "Copyright ©2005 Fourth Year, Electronics and Communications Dept., Faculty of Eng" +
				"ineering, Cairo University.";
			// 
			// version
			// 
			this.version.AutoSize = true;
			this.version.Location = new System.Drawing.Point(88, 40);
			this.version.Name = "version";
			this.version.Size = new System.Drawing.Size(61, 16);
			this.version.TabIndex = 1;
			this.version.Text = "Version 2.5";
			// 
			// programIcon
			// 
			this.programIcon.Image = ((System.Drawing.Image)(resources.GetObject("programIcon.Image")));
			this.programIcon.Location = new System.Drawing.Point(8, 8);
			this.programIcon.Name = "programIcon";
			this.programIcon.Size = new System.Drawing.Size(65, 57);
			this.programIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.programIcon.TabIndex = 1;
			this.programIcon.TabStop = false;
			// 
			// productName
			// 
			this.productName.AutoSize = true;
			this.productName.Location = new System.Drawing.Point(88, 16);
			this.productName.Name = "productName";
			this.productName.Size = new System.Drawing.Size(0, 16);
			this.productName.TabIndex = 0;
			// 
			// OKButton
			// 
			this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.OKButton.Location = new System.Drawing.Point(113, 180);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(75, 25);
			this.OKButton.TabIndex = 1;
			this.OKButton.Text = "OK";
			// 
			// AboutForm
			// 
			this.AcceptButton = this.OKButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.OKButton;
			this.ClientSize = new System.Drawing.Size(300, 215);
			this.Controls.Add(this.OKButton);
			this.Controls.Add(this.gameDataPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AboutForm";
			this.ShowInTaskbar = false;
			this.Text = "About";
			this.Load += new System.EventHandler(this.AboutForm_Load);
			this.gameDataPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void AboutForm_Load(object sender, System.EventArgs e)
		{

			productName.Text = Application.ProductName;

		}// end of method AboutForm_Load
	}// end of class AboutForm
}
