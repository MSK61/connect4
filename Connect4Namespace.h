/************************************************************

program:      Connect 4.

file:         Connect4 namespace.h.h

function:

description:  represents the the namespace Connect4Parameters

author:       Mohammed Safwat (MS)

environment:  borland c++ builder 1.0, windows 98 second edition

notes:        This is a project program.

revisions:    1.00	1/2/2004 (MS) first release
              1.01  23/2/2004 (MS) adding registry values for the
              difficulty level

************************************************************/
//---------------------------------------------------------------------------
#ifndef Connect4NamespaceH
#define Connect4NamespaceH
#include<vcl/dstring.h>
namespace Connect4Parameters
   {
    const unsigned char columns = 7, DefaultDifficulty = 1,
    MaxDifficulty = 6, rows = 6;
    const System::AnsiString ApplicationRegistryKey =
    "\\Software\\Mohammed Safwat\\Connect4", BackgroundRegistryKey =
    "\\Background", BackgroundRegistryValue = "background",
    ComputerRegistryValue = "Computer", LevelRegistryValue = "Level",
    OptionsRegistryKey = "\\Options",
    solution = "Running SetupReg may solve the problem.",
    TileColorRegistryKey = "\\Tile Colors", UserRegistryValue = "User";
    const char collection = 4;
   }
//---------------------------------------------------------------------------
#endif
