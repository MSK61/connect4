/************************************************************

program:      Connect 4.

file:         Connect4.cpp

function:     WinMain

description:  runs a connect4 game between the computer and the user

author:       Mohammed Safwat (MS)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  28/12/2003 (MS) starting construction
			  1.01	3/2/2004   (MS) adding registry checks
              1.02  13/2/2004  (MS) rearranging header files and
              rebuilding in borland c++ builder 6
              1.03  23/2/2004  (MS) adding a registry check for the
              difficulty level

************************************************************/
//---------------------------------------------------------------------------
#include<vcl\graphics.hpp>
#include <vcl\vcl.h>
#include "State.h"
#pragma hdrstop
//---------------------------------------------------------------------------
USEFORM("Main.cpp", MainForm);
USEFORM("about.cpp", AboutBox);
USEFORM("OptionsDialog.cpp", Options);
//---------------------------------------------------------------------------
WINAPI WinMain(HINSTANCE, HINSTANCE, LPSTR, int)
{
    auto_ptr< TRegistry > temp(new TRegistry);
    auto_ptr< TStrings > RegistryValues(new TStringList);
    AnsiString CurrentRegistryValue;
    bool success = false;
    int value;
    TRegDataInfo DataInformation;
    unsigned char count = 0, NumberOfEntries;
	Application->Initialize();
	Application->Title = "Connect 4.";
    // Check for the existence of registry keys and values.
    // Start with the bakgrounds registry key and its values.
    temp -> OpenKey(ApplicationRegistryKey + BackgroundRegistryKey, true);
    temp -> GetValueNames(RegistryValues.get());
    NumberOfEntries = RegistryValues -> Count;
    for (; count < NumberOfEntries; count++)
       {
        CurrentRegistryValue = RegistryValues -> Strings[count];
        if (! temp -> GetDataInfo(CurrentRegistryValue,
        DataInformation) || DataInformation.RegData != rdBinary ||
        DataInformation.DataSize != sizeof(Background))
        temp -> DeleteValue(CurrentRegistryValue);
       }
    RegistryValues.reset();
    // Check the options registry key and its values.
    temp -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey, true);
    temp -> LazyWrite = false;
    if (! temp -> ValueExists(BackgroundRegistryValue) ||
    temp -> GetDataType(BackgroundRegistryValue) != rdInteger)
    temp -> WriteInteger(BackgroundRegistryValue, 0);
    try
       {
        value = temp -> ReadInteger(LevelRegistryValue);
        if (value > 0 && value <= MaxDifficulty) success = true;
       }
    catch(...)
       {
       }
    if (! success) temp -> WriteInteger(LevelRegistryValue, 1);
    // Check the colors options registry key and its values.
    temp -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey +
    TileColorRegistryKey, true);
    if (! temp -> ValueExists(UserRegistryValue) ||
    temp -> GetDataType(UserRegistryValue) != rdInteger)
    temp -> WriteInteger(UserRegistryValue, clRed);// just a default value
    if (! temp -> ValueExists(ComputerRegistryValue) ||
    temp -> GetDataType(ComputerRegistryValue) != rdInteger)
    temp -> WriteInteger(ComputerRegistryValue, clGreen);// just a default
    // value
    temp.reset();
		Application->CreateForm(__classid(TMainForm), &MainForm);
         Application->CreateForm(__classid(TAboutBox), &AboutBox);
         Application->Run();

	return 0;
}
//---------------------------------------------------------------------------
