/************************************************************

program:      Connect4 SetupReg utility

file:         SetupReg.cs

function:     methods of the SetupReg class

description:  writes the default options of the connect4 game to the
			  registry

author:       Mohammed Safwat (MS)

environment:  visual studio.net enterprise architect 2003, windows xp
			  professional

notes:        This is a translated faculy program.

revisions:    2.5  7/11/2005 (MS) starting construction

************************************************************/
using Connect4;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Drawing;

namespace Connect4
{
	/// <summary>
	/// Summary description for SetupReg.
	/// </summary>
	class SetupReg
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			ArrayList backgrounds = new ArrayList();
			byte count = 0;
			Background currentBackground;
			const string extension = ".bmp",
					  gamePrefix = "c4";
			string registryValue;
			RegistryKey backgroundKey;// registry key for information about background
				// files

			// Fill the vector of backgrounds with their data. Note that all file names
			// are relative to the folder of backgrounds.
			backgrounds.Add(new Background("classic", new Point(5, 5)));
			backgrounds.Add(new Background("wood", new Point(7, 5)));
			try
			{
				
				// just for safety
				Registry.CurrentUser.DeleteSubKeyTree(Parameters.applicationRegistryKey);

			}
			catch (ArgumentException)
			{
			}
			// Register the backgrounds in the registry.
			backgroundKey =
				Registry.CurrentUser.CreateSubKey(Parameters.applicationRegistryKey +
				RegistryStream.keySeparator + Parameters.backgroundRegistryKey);

			while (count < backgrounds.Count)
			{

				currentBackground = (Background)(backgrounds[count]);
				registryValue = currentBackground.fileName;
				currentBackground.fileName =
					gamePrefix + currentBackground.fileName + extension;

				if (System.IO.File.Exists(Parameters.backgroundFolder +
					RegistryStream.keySeparator + currentBackground.fileName))// If the
					// background file really exists, register it.
				{

					RegistryStream.Write(currentBackground, backgroundKey, registryValue);
					count++;

				}
				else// Alert the user that a background file is missing.
				{

					backgrounds.RemoveAt(count);
					Console.WriteLine("missing background file " +
						currentBackground.fileName + ". failed to register");

				}

			}

			backgroundKey.Close();// Close the registry key of the backgrounds.
			// Fill the options with default values.
			Parameters.defaultOptions.Save(Parameters.applicationRegistryKey +
				RegistryStream.keySeparator + Parameters.optionsRegistryKey);

		}// end of method Main
	}// end of class SetupReg
}
