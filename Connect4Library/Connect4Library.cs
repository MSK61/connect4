/************************************************************

program:      connect4 library

file:         Connect4Library.cs

function:     classes of the Connect4Library class library

description:  defines general parameters and data types for use with
			  the connect4 game

author:       Mohammed Safwat (MS)

environment:  visual studio.net enterprise architect 2003, windows xp
			  professional

notes:        This is a translated faculy program.

revisions:    2.5  7/11/2005 (MS) starting construction
			  2.51 9/11/2005 (MS) adding the connect4 game parameters class
			  2.52 9/11/2005 (MS) adding the Background structure
			  2.53 13/11/2005 (MS) adding the Options class

************************************************************/
using Microsoft.Win32;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Connect4
{
	[Serializable]
	public struct Background
	{
		public Background(string file, Point margin)
		{

			clearance = margin;
			fileName = file;

		}// end of constructor Background
		public Point clearance;
		public string fileName;
	}// end of structure Background
	public enum player{minimizing, maximizing};
	/// <summary>
	/// stores the options of the connect4 game
	/// </summary>
	public class Options
	{
		// overloaded constructors
		public Options()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		internal Options(sbyte background, byte level, Color[] tileColors)
		{

			itsBackground = background;
			itsLevel = level;
			itsTileColors = tileColors;

		}// end of constructor Options

		/// <summary>
		/// This method ensures that the specified registry key has valid options.
		/// </summary>
		public static void EnsureValidRegistryData(string registryKey)
		{
			string[] registryValues;// registry values under a registry key
			bool success = false;
			RegistryKey temp = Registry.CurrentUser.CreateSubKey(registryKey);
			object tempObject;
			byte tempValue;

			// Check for the existence of registry keys and values.
			// Check the registry key and values of the options.
			registryValues = temp.GetValueNames();

			if (Array.IndexOf(registryValues, backgroundRegistryValue) == -1)
				temp.SetValue(backgroundRegistryValue, 0);

			tempObject = temp.GetValue(levelRegistryValue);

			if (tempObject != null)
			{
				
				tempValue = Byte.Parse(tempObject.ToString());

				if (tempValue > 0 && tempValue <= maxDifficulty) success = true;

			}

			if (! success)
				temp.SetValue(levelRegistryValue, Parameters.defaultOptions.itsLevel);

			// Check the registry key and values of the color options.
			temp = temp.CreateSubKey(tileColorRegistryKey);
			registryValues = temp.GetValueNames();

			if (Array.IndexOf(registryValues, userRegistryValue) == -1)
				temp.SetValue(userRegistryValue, Color.Red);

			if (Array.IndexOf(registryValues, computerRegistryValue) == -1)
				temp.SetValue(computerRegistryValue, Color.Green);

			temp.Close();// Close the registry key of the color options.

		}// end of method EnsureValidRegistryData

		/// <summary>
		/// This method loads the options from the specified registry key.
		/// </summary>
		public void Load(string registryKey)
		{
			const byte maximizing = (byte)(player.maximizing),
					  minimizing = (byte)(player.minimizing);
			RegistryKey temp =
				Registry.CurrentUser.CreateSubKey(registryKey);

			// Read the preferred background option from the registry.
			itsBackground =
				SByte.Parse(temp.GetValue(backgroundRegistryValue,
				Parameters.defaultOptions.itsBackground).ToString());
			// Read the difficulty level option from the registry.
			itsLevel = Byte.Parse(temp.GetValue(levelRegistryValue,
				Parameters.defaultOptions.itsLevel).ToString());
			
			if (itsLevel == 0 || itsLevel > maxDifficulty)
				itsLevel = Parameters.defaultOptions.itsLevel;

			// Read the colors options from the registry.
			temp = temp.CreateSubKey(tileColorRegistryKey);
			itsTileColors[minimizing] =
				Color.FromArgb((int)(temp.GetValue(userRegistryValue,
				Parameters.defaultOptions.itsTileColors[minimizing].ToArgb())));
			itsTileColors[maximizing] =
				Color.FromArgb((int)(temp.GetValue(computerRegistryValue,
				Parameters.defaultOptions.itsTileColors[maximizing].ToArgb())));
			temp.Close();// Close the registry key of the color
				// options.

		}// end of method Load

		/// <summary>
		/// This method saves the options to the specified registry key.
		/// </summary>
		public void Save(string registryKey)
		{
			RegistryKey currentKey = Registry.CurrentUser.CreateSubKey(registryKey);

			currentKey.SetValue(backgroundRegistryValue, itsBackground);// Register the
				// preferred background.
			currentKey.SetValue(levelRegistryValue, itsLevel);// Register the preferred
				// difficulty level.
			// Register the color options.
			currentKey = currentKey.CreateSubKey(tileColorRegistryKey);
			currentKey.SetValue(
				userRegistryValue, itsTileColors[(int)(player.minimizing)].ToArgb());
			currentKey.SetValue(
				computerRegistryValue, itsTileColors[(int)(player.maximizing)].ToArgb());
			currentKey.Close();// Close the registry key of tile colors.

		}// end of method Save

		/// <summary>
		/// registry value for the preferred background
		/// </summary>
		private const string backgroundRegistryValue = "Background";
		/// <summary>
		/// registry value for the color of computer tiles
		/// </summary>
		private const string computerRegistryValue = "Computer";
		/// <summary>
		/// the preferred background
		/// </summary>
		public sbyte itsBackground;
		/// <summary>
		/// the preferred difficulty level
		/// </summary>
		public byte itsLevel;
		public Color[] itsTileColors =
			new Color[Enum.GetValues(typeof(player)).Length];// the
			// preferred tile colors of the players
		/// <summary>
		/// registry value for the game difficulty level
		/// </summary>
		private const string levelRegistryValue = "Level";
		/// <summary>
		/// maximum difficulty level for the connect4 game
		/// </summary>
		public const byte maxDifficulty = 6;
		/// <summary>
		/// registrty key for preferred tile colors
		/// </summary>
		private const string tileColorRegistryKey = "Tile Colors";
		/// <summary>
		/// registry value for user color preferences
		/// </summary>
		private const string userRegistryValue = "User";
	}// end of class Options
	/// <summary>
	/// holds the parameters characterizing the connect4 game
	/// </summary>
	public sealed class Parameters
	{
		public Parameters()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// This method returns the opponent player of the specified player.
		/// </summary>
		public static player GetOpponent(player operand)
		{

			return ((operand == player.maximizing) ?
				player.minimizing : player.maximizing);

		}// end of method GetOpponent

		/// <summary>
		/// the root registry key for the connect4 game
		/// </summary>
		public const string applicationRegistryKey = @"Software\Mohammed Safwat\C#\Connect4";
		/// <summary>
		/// the folder containing the background files
		/// </summary>
		public const string backgroundFolder = "background";
		/// <summary>
		/// registry key for the available backgrounds
		/// </summary>
		public const string backgroundRegistryKey = "Background";
		/// <summary>
		/// number of adjacent tiles that mean winning the game
		/// </summary>
		public const byte collection = 4;
		/// <summary>
		/// number of columns of tiles
		/// </summary>
		public const byte columns = 7;
		/// <summary>
		/// default options for the connect4 game
		/// </summary>
		public static readonly Options defaultOptions =
			new Options(0, 1, new Color[]{Color.Red, Color.Green});
		/// <summary>
		/// registry key for the connect4 game options
		/// </summary>
		public const string optionsRegistryKey = "Options";
		/// <summary>
		/// number of rows of tiles
		/// </summary>
		public const byte rows = 6;
		/// <summary>
		/// solution message for registry errors
		/// </summary>
		public const string solution = "Running SetupReg may solve the problem.";
	}// end of class Parameters
	/// <summary>
	/// performs common registry operations with objects
	/// </summary>
	public sealed class RegistryStream
	{
		public RegistryStream()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// This method retrieves an object from the registry at the specified key and value.
		/// </summary>
		public static object Read(RegistryKey key, string registryValue)
		{
			BinaryFormatter binaryConverter = new BinaryFormatter();
			// temporary stream for converting binary data into an
			// object
			MemoryStream conversionStream = new MemoryStream((byte[])(
				key.GetValue(registryValue)));
			object retrievedObject =
				binaryConverter.Deserialize(conversionStream);

			conversionStream.Close();// Close the temporary stream.
			return retrievedObject;

		}// end of method Read

		/// <summary>
		/// This method saves the specified object to the registry at the specified key and value.
		/// </summary>
		public static void Write(object data, RegistryKey key, string registryValue)
		{

			BinaryFormatter binaryConverter = new BinaryFormatter();// to convert the
			// specified object into binary data before writing it to the registry
			MemoryStream conversionStream = new MemoryStream();// temporary stream for
			// converting objetcs into binary data

			binaryConverter.Serialize(conversionStream, data);// Convert the object to
			// binary data.
			key.SetValue(registryValue, conversionStream.GetBuffer());
			conversionStream.Close();// Close the temporary stream.

		}// end of method Write

		/// <summary>
		/// separator used to separate different levels in a fully qualified registry key
		/// </summary>
		public const char keySeparator = '\\';
	}// end of class RegistryStream
}
