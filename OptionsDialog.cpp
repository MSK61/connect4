/************************************************************

program:      Connect 4.

file:         OptionsDialog.cpp

function:     methods of the TOptions form

description:  represents the implementation of the form TOptions

author:       Mohammed Safwat (MS) and Hany Mohammed (HM)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  23/1/2004 (MS and HM) starting construction
              1.01	3/2/2004  (MS) adding registry capabilities
              1.02  25/2/2004 (MS) adding some run-time registry
              correction features

************************************************************/
//---------------------------------------------------------------------
#include <vcl.h>
#pragma hdrstop

#include "OptionsDialog.h"
//---------------------------------------------------------------------
#pragma resource "*.dfm"
TOptions *Options;
//---------------------------------------------------------------------
__fastcall TOptions::TOptions(TComponent* AOwner)
	: TForm(AOwner)
{
}
//---------------------------------------------------------------------
void __fastcall TOptions::UserColorButtonClick(TObject *Sender)
{
if (ColorDialog -> Execute())
UserColor -> Brush -> Color = ColorDialog -> Color;// If the user has
// chosen a user color, display it.
}
//---------------------------------------------------------------------------
void __fastcall TOptions::ComputerColorButtonClick(TObject *Sender)
{
if (ColorDialog -> Execute())
ComputerColor -> Brush -> Color = ColorDialog -> Color;// If the user has
// chosen a computer color, display it.
}
//---------------------------------------------------------------------------
void __fastcall TOptions::OKBtnClick(TObject *Sender)
{
auto_ptr< TRegistry > savings(new TRegistry);
savings -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey +
TileColorRegistryKey, true);
savings -> LazyWrite = false;
savings -> WriteInteger(UserRegistryValue, UserColor -> Brush -> Color);
savings -> WriteInteger(ComputerRegistryValue,
ComputerColor -> Brush -> Color);
savings -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey, true);
savings -> WriteInteger(LevelRegistryValue,
LevelComboBox -> ItemIndex + 1);
// Write the preferred background to the registry.
if (BackgroundList -> ItemIndex > -1) savings ->
WriteInteger(BackgroundRegistryValue, BackgroundList -> ItemIndex);
}
//---------------------------------------------------------------------------
void __fastcall TOptions::BackgroundListClick(TObject *Sender)
{
AnsiString BackgroundFileName = BackgroundRegistryValue + "\\" +
ItsBackgrounds[BackgroundList -> ItemIndex].FileName;
if (access(BackgroundFileName.c_str(), 0) == 0)// If the background file
// really exists,load the background.
   {
    BackgroundImage -> Picture -> LoadFromFile(BackgroundFileName);
    ItsChosenBackground = BackgroundList -> ItemIndex;
   }
else
   {
    Application -> MessageBox((AnsiString(
    "missing background file.can't load the background") +
    solution).c_str(), Application -> Title.c_str(), MB_ICONERROR);
    BackgroundList -> ItemIndex = ItsChosenBackground;
   }
}
//---------------------------------------------------------------------------
void __fastcall TOptions::FormCreate(TObject *Sender)
{
auto_ptr< TRegistry > InputSource(new TRegistry);
bool success;
char NumberOfBackgrounds,
*GameTitle = new char[Application -> Title.Length() + 1], word[33];
AnsiString BackgroundFileName;
unsigned char count = 1;
strcpy(GameTitle, Application -> Title.c_str());
// Open the colors option registry key.
InputSource -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey +
TileColorRegistryKey, false);
try
   {
    // Load the user and computer colors.
    UserColor -> Brush -> Color =
    InputSource -> ReadInteger(UserRegistryValue);
    ComputerColor -> Brush -> Color =
    InputSource -> ReadInteger(ComputerRegistryValue);
    // Load the level combo box with the available difficulty levels.
    for (; count <= MaxDifficulty; count++)
    LevelComboBox -> Items -> Append(itoa(count, word, 10));
    // Open the options registry key.
    InputSource -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey,
    false);
    // Read the preferred user background.
    ItsChosenBackground =
    InputSource -> ReadInteger(BackgroundRegistryValue);
    // Read the preferred difficulty level.
    LevelComboBox -> ItemIndex =
    InputSource -> ReadInteger(LevelRegistryValue) - 1;
    // Validate the preferred difficulty level.
    if (LevelComboBox -> ItemIndex < 0 || LevelComboBox -> ItemIndex >=
    MaxDifficulty)
       {
        LevelComboBox -> ItemIndex = DefaultDifficulty - 1;
        InputSource -> WriteInteger(LevelRegistryValue,
        LevelComboBox -> ItemIndex);
       }
    // Open the backgrounds registry key.
    InputSource -> OpenKey(ApplicationRegistryKey + BackgroundRegistryKey,
    false);
    // Load the names of the backgrounds from the open registry key to the
    // list box.
    InputSource -> GetValueNames(BackgroundList -> Items);
    // Size the backgrounds vector as needed.
    NumberOfBackgrounds = BackgroundList -> Items -> Count;
    ItsBackgrounds.resize(NumberOfBackgrounds);
    // Load the backgrounds data from the registry to the backgrounds
    // vector.
    for (count = 0; count < NumberOfBackgrounds; count++) InputSource ->
    ReadBinaryData(BackgroundList -> Items -> Strings[count],
    &(ItsBackgrounds[count]), sizeof(Background));
    // Validate the preferred user background.
    if (ItsChosenBackground >= NumberOfBackgrounds)
    ItsChosenBackground = NumberOfBackgrounds - 1;
    if (NumberOfBackgrounds > 0)
       {
        if (ItsChosenBackground <= -1)// if the preferred background is
        // invalid
           {
            ItsChosenBackground = 0;// Change the preferred background to
            // a valid value.
            // Register the new value in the registry.
            InputSource -> OpenKey(ApplicationRegistryKey +
            OptionsRegistryKey, false);
            InputSource -> WriteInteger(BackgroundRegistryValue, 0);
            InputSource -> OpenKey(ApplicationRegistryKey +
            BackgroundRegistryKey, false);// Reopen the backgrounds
            // registry key.
           }
    	// Validate the background file.i.e.Verify its existence.
    	BackgroundFileName = BackgroundRegistryValue + "\\" +
    	ItsBackgrounds[ItsChosenBackground].FileName;
    	success = (access(BackgroundFileName.c_str(), 0) == 0);
    	for (; NumberOfBackgrounds > 0 && ! success && Application ->
    	MessageBox("missing background file.Delete its reference?",
        GameTitle, MB_YESNO + MB_ICONWARNING) == IDYES;
        NumberOfBackgrounds--)// Iterate through all backgrounds until the
        // first valid background is found.
           {
        	// Delete the references to the file from each of the
            // registry,the list box and the backgrounds vector.
        	InputSource -> DeleteValue(BackgroundList -> Items ->
        	Strings[ItsChosenBackground]);
        	BackgroundList -> Items -> Delete(ItsChosenBackground);
        	ItsBackgrounds.erase(&(ItsBackgrounds[ItsChosenBackground]));
        	ItsChosenBackground = 0;
        	BackgroundFileName = BackgroundRegistryValue + "\\" +
            ItsBackgrounds[0].FileName;
        	// Validate the new first background file.i.e.Verify its
        	// existence.
        	success = (access(BackgroundFileName.c_str(), 0) == 0);
           }
        InputSource -> CloseKey();
    	if (NumberOfBackgrounds > 0 && success)// If the background file
    	// really exists,load its background.
           {
        	BackgroundList -> ItemIndex = ItsChosenBackground;
        	BackgroundImage -> Picture -> LoadFromFile(
            BackgroundFileName);
           }
       }
   }
catch(...)// if an exception is thrown(due to error while reading data
// from the registry)
   {
    InputSource -> CloseKey();
    Application -> MessageBox(
    "invalid registry entry.Data may be corrupted.", GameTitle,
    MB_OK + MB_ICONERROR);
    Close();
   }
delete []GameTitle;
}
//---------------------------------------------------------------------------
