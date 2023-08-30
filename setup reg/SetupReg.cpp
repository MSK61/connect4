/************************************************************

program:      SetupReg

file:         AlphaBetaTree.cpp

function:     main (complete program listing in this file)

description:  registers the Connect4 game in the windows registry

author:       Mohammed Safwat (MS)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  28/12/2003 (MS) starting construction
			  1.01	29/12/2003 (MS)	first release
              1.02	23/2/2004  (MS) adding a registry value for the
              difficulty level and rebuilding in borland c++ builder 6

************************************************************/
//---------------------------------------------------------------------------
#include<conio.h>
#include<io.h>
#include<iostream.h>
#include<memory>
#include <vcl\condefs.h>
#include"Background.h"
#include<vcl\graphics.hpp>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include<vcl\registry.hpp>
#include<vector>

#pragma hdrstop
//---------------------------------------------------------------------------
USERES("SetupReg.res");
//---------------------------------------------------------------------------
int main(int argc, char **argv)
{
    using Connect4Parameters::ApplicationRegistryKey;
    using Connect4Parameters::BackgroundRegistryKey;
    using Connect4Parameters::BackgroundRegistryValue;
    using Connect4Parameters::ComputerRegistryValue;
    using Connect4Parameters::OptionsRegistryKey;
    using Connect4Parameters::TileColorRegistryKey;
    using Connect4Parameters::UserRegistryValue;
    using Connect4Parameters::MaxDifficulty;
    using Connect4Parameters::LevelRegistryValue;
    using Connect4Parameters::DefaultDifficulty;
    const System::AnsiString extension = ".bmp", GamePrefix = "c4";
    std::auto_ptr<TRegistry> temp(new TRegistry);
    unsigned char count = 0, NumberOfBackgrounds = 2;
    std::vector<Background> backgrounds(NumberOfBackgrounds);
    System::AnsiString RegistryValue;
    // Fill the backgrounds vector with backgrounds data.
    // Note that all file names are relative to the backgrounds folder.
    strcpy(backgrounds[0].FileName, "classic");
    backgrounds[0].clearance = 6;
    strcpy(backgrounds[1].FileName, "wood");
    backgrounds[1].clearance = 8;
    temp -> DeleteKey(ApplicationRegistryKey);// just for safety
    // Register the backgrounds in the registry.
    temp -> OpenKey(ApplicationRegistryKey + BackgroundRegistryKey, true);
    while(count < NumberOfBackgrounds)
       {
        RegistryValue = backgrounds[count].FileName;
        strcpy(backgrounds[count].FileName, (GamePrefix +
        backgrounds[count].FileName + extension).c_str());
        if (access((BackgroundRegistryValue + "\\" +
    	backgrounds[count].FileName).c_str(), 0) == 0)// If the background
        // file really exists,register it.
           {
        	temp -> WriteBinaryData(RegistryValue,
    		(char*)&(backgrounds[count]), sizeof(backgrounds[count]));
        	count++;
           }
    	else// Alert the user that a background file is missing.
           {
        	backgrounds.erase(&(backgrounds[count]));
        	cout << "missing background file " <<
        	backgrounds[count].FileName << ".failed to register\n";
        	NumberOfBackgrounds--;
           }
       }
    // Register the preferred background.
    temp -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey, true);
    if (NumberOfBackgrounds > 0)
    temp -> WriteInteger(BackgroundRegistryValue, 0);
    else temp -> WriteInteger(BackgroundRegistryValue, -1);
    // Register the preferred difficulty level.
    temp -> WriteInteger(LevelRegistryValue, DefaultDifficulty);
    // Register the color options.
    temp -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey +
    TileColorRegistryKey, true);
    temp -> WriteInteger(UserRegistryValue, clRed);
    temp -> WriteInteger(ComputerRegistryValue, clGreen);
    temp -> CloseKey();
    cout << "registration complete.Please,press any key to close.";
    if (getch() == 0) getch();
	return 0;
}
//---------------------------------------------------------------------------
