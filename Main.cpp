/************************************************************

program:      Connect 4.

file:         Main.cpp

function:     methods of the TMainForm form

description:  represents the implementation of the form TMainForm

author:       Mohammed Safwat (MS) and Hany Mohammed (HM)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  28/12/2003 (MS) starting construction
			  1.01	29/12/2003 (MS)	first release
              1.02	1/1/2004   (MS) adding the methods ExecutePlayerTurn
              and ApplyTurn
              1.03  23/1/2004  (MS and HM) adding some extra GUI features
              1.04	3/2/2004   (MS)	adding registry capabilities
              1.05  13/2/2004  (MS) rebuilding in borland c++ builder 6
              1.06  14/2/2004  (MS) centering the background in the form
              1.07  16/2/2004  (MS) adding the falshing effect
              1.08  20/2/2004  (MS) changing most local constants in
              methods to static constants
              1.09  21/2/2004  (MS) replacing the TTimer component(needed
              for flashing) with a FiniteRepeatTimer component and
              removing some unnecessary member data
              1.10  1/10/2004  (MS) modifying the missing background
              exception in the LoadOptions method to quit the program
              1.11  1/10/2004  (MS) changing the file menu name to Game
              instead of File
              1.12  1/10/2004  (MS) swapping the two statements in the
              Redraw method in module Main.cpp
              1.13  1/10/2004  (MS) confining the throwing process to be
              fired by the OnClick event(inside the ciecle) instead of the
              OnMouseUp event
              1.14  2/10/2004  (MS) changing the options dialog from being
              automatically created(an auto-create form) to a manually
              created form

************************************************************/
//---------------------------------------------------------------------------
#include <vcl\vcl.h>
#pragma hdrstop

#include<algorithm>
#include "Main.h"
#include<math.h>
#include "about.h"
#include "OptionsDialog.h"
#include<string>
//---------------------------------------------------------------------------
#pragma link "FiniteRepeatTimer"
#pragma resource "*.dfm"
void ResetColor(TShape* const circle);
const vector< TileColumn > EmptyState(columns);
TMainForm *MainForm;
//---------------------------------------------------------------------------
__fastcall TMainForm::TMainForm(TComponent* Owner)
	: TForm(Owner), ItsBeginner(minimizing), ItsCircleDiameter(52),
    ItsCircles(columns), ItsCurrentState(EmptyState),
    ItsHorizontalSpacing(12)
{
LoadOptions();
Options = NULL;
}
//----------------------------------------------------------------------------
void __fastcall TMainForm::FormCreate(TObject *Sender)
{
    auto_ptr< TShape > CurrentCircle;
    char count = 0, count2;
    const unsigned short int TopClearance = 6,
    TopMargin = BackgroundImage -> Top + TopClearance,
    VerticalSpacing = 12;
    // Create the columns of circles.
    for (; count < columns; count++)
       {
        ItsCircles[count].reserve(rows);
        // For each column,create the circles of this column.
        for (count2 = 0; count2 < rows; count2++)
           {
            ItsCircles[count].push_back(new TShape(this));
            CurrentCircle.reset(ItsCircles[count][count2]);
            CurrentCircle -> Shape = stCircle;
            // Position and size the circle as desired.
            CurrentCircle -> Height = ItsCircleDiameter;
            CurrentCircle -> Width = ItsCircleDiameter;
            CurrentCircle -> Left = ItsLeftMargin +
            count * (ItsCircleDiameter + ItsHorizontalSpacing);
            CurrentCircle -> Top = TopMargin +
            (rows - count2 - 1) * (ItsCircleDiameter + VerticalSpacing);
            CurrentCircle -> OnMouseUp = ExecutePlayerTurn;
            // Give each circle an ID.
            CurrentCircle -> Tag = count * rows + count2;
            InsertControl(CurrentCircle.get());
            CurrentCircle.release();
           }
       }
}
//---------------------------------------------------------------------------
void __fastcall TMainForm::GameNew(TObject *Sender)
{
	//--- Add code to start a new game ---
    bool empty = (ItsCurrentState == EmptyState);
    char count = 0;
    char* GameTitle = new char[Application -> Title.Length() + 1];
    strcpy(GameTitle, Application -> Title.c_str());
    // If a current game is running,verify that the user wants to ignore
    // this game and starts a new one.
    if (empty || Application -> MessageBox(
    "Are you sure you want to start a new game?", GameTitle,
    MB_YESNO + MB_ICONQUESTION + MB_DEFBUTTON2) == IDYES)
       {
        if (! empty)
           {
            ItsCurrentState = EmptyState;// Get ready for a new game.
            empty = true;
           }
    	for (; count < columns; count++)
        std::for_each(ItsCircles[count].begin(), ItsCircles[count].end(),
        ResetColor);
       }
    if (empty && Application -> MessageBox(
    "Do you want to play the first turn?", GameTitle,
    MB_YESNO + MB_ICONQUESTION) == IDNO)
       {
        ItsBeginner = maximizing;
        ToggleUserInterface(false);// Disable the user interface.
        ComputerTurn();// Allow the computer to play.
       }
    else
       {
        ItsBeginner = minimizing;
        ToggleUserInterface(true);
       }
    delete []GameTitle;
}
//----------------------------------------------------------------------------
void __fastcall TMainForm::GameExit(TObject *Sender)
{
	Close();
}
//----------------------------------------------------------------------------
void __fastcall TMainForm::HelpAbout(TObject *Sender)
{
	//---- Add code to show program's About Box ----
    AboutBox -> ShowModal();
}
//----------------------------------------------------------------------------
void
TMainForm::ApplyTurn(const unsigned char column, const player owner)
{
ItsCurrentState[column].push_back(owner);// Update the current state.
// Adjust the flashing parameters.
ItsBlinkingStep = (TileColor[owner] - clWhite) / blinker -> Repetitions;
ItsFlashingColumn = column;
ItsFlashingPlayer = owner;
blinker -> Enabled = true;// Enable the blinking(flashing) timer.
}
void
TMainForm::ComputerTurn(void)
{
using std::string;
string UserMessage;
AlphaBetaTree tree(ItsMaxLevel, ItsCurrentState);
unsigned char NextStep;
StatusLine -> SimpleText = "analyzing...";
tree.ApplyAlphaBeta(goal, NextStep, ItsBeginner);
StatusLine -> SimpleText = "";
if (NextStep == columns)// if no next step is valid
   {
    if (goal) UserMessage = "You win.";
	else UserMessage = string("The game can't be continued any more.") +
    "No one wins.";
    Application -> MessageBox((char*)UserMessage.c_str(),
    Application -> Title.c_str(), MB_ICONINFORMATION);
    ItsCurrentState = EmptyState;// Get ready for a new game.
    ToggleUserInterface(false, crDefault);// Prevent the user from
    // continuing the game.
   }
else ApplyTurn(NextStep, maximizing);// Show the visual effect of the
// player's turn.
}
void __fastcall TMainForm::ExecutePlayerTurn(TObject* Sender,
TMouseButton Button, Classes::TShiftState Shift, int X, int Y)
{
// Detect the column in which the player wants to throw his tile.
auto_ptr< TShape > circle;
static const unsigned short int radius = ItsCircleDiameter / 2;
unsigned char column;
circle.reset(dynamic_cast<TShape*>(Sender));
assert(circle.get());
if (pow(X - radius, 2) + pow(Y - radius, 2) < pow(radius, 2) &&
Button == mbLeft && ! Shift.Contains(ssShift) &&
! Shift.Contains(ssAlt) && ! Shift.Contains(ssCtrl) &&
! Shift.Contains(ssRight) && ! Shift.Contains(ssMiddle))// if a lift click
// is detected inside the circle boundaries
   {
    column = (circle -> Tag) / rows;
    // Validate this movement by checking the current state.
    if (ItsCurrentState[column].size() == rows) Application -> MessageBox(
    "invalid game.This column is full.Please,try again.",
    Application -> Title.c_str(), MB_ICONASTERISK);// if the movement is
    // invalid
    else
       {
        ToggleUserInterface(false);// Disable the user interface.
        ApplyTurn(column, minimizing);// If the movement is valid,show the
        // visual effect of the player's turn.
       }
   }
circle.release();
}
void
TMainForm::LoadOptions(void)
{
auto_ptr< TRegistry > temp(new TRegistry);
auto_ptr< TStrings > RegistryValues(new TStringList);
Background ReadBackground;
bool success = false;
char ChosenBackground, NumberOfBackgrounds;
AnsiString BackgroundFileName;
// Read the colors options from the registry.
temp -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey +
TileColorRegistryKey, true);
TileColor[minimizing] = temp -> ReadInteger(UserRegistryValue);
TileColor[maximizing] = temp -> ReadInteger(ComputerRegistryValue);
temp -> OpenKey(ApplicationRegistryKey + OptionsRegistryKey, true);
ChosenBackground = temp -> ReadInteger(BackgroundRegistryValue);
// Read the difficulty level option from the registry.
try
   {
    ItsMaxLevel = temp -> ReadInteger(LevelRegistryValue);
    if (ItsMaxLevel > 0 && ItsMaxLevel <= MaxDifficulty) success = true;
   }
catch(...)
   {
   }
if (! success) ItsMaxLevel = DefaultDifficulty;
// Get ready to load the background.
success = false;
if (ChosenBackground >= 0)// if the preferred background is valid
   {
    temp -> OpenKey(ApplicationRegistryKey + BackgroundRegistryKey, true);
    temp -> GetValueNames(RegistryValues.get());
    NumberOfBackgrounds = RegistryValues -> Count;
    if (ChosenBackground < NumberOfBackgrounds)// if the preferred
    // background exists in the set of available backgrounds
       {
        temp -> ReadBinaryData(
        RegistryValues -> Strings[ChosenBackground],&ReadBackground,
        sizeof(Background));
        BackgroundFileName = BackgroundRegistryValue + "\\" +
        ReadBackground.FileName;
        if (access(BackgroundFileName.c_str(), 0) == 0)// If the
        // background file really exists,load it to the form.
           {
            try
               {
                BackgroundImage -> Picture -> LoadFromFile(
                BackgroundFileName);
                // Center the background in the form.
                BackgroundImage -> Left = (ClientWidth -
                BackgroundImage -> Width) / 2;
                BackgroundImage -> Top = (ClientHeight -
                BackgroundImage -> Height - StatusLine -> Height) / 2;
                ItsLeftMargin = BackgroundImage -> Left +
                ReadBackground.clearance;
                success = true;
               }
            catch(...)
               {
               }
           }
       }
   }
if (! success)// if the options couldn't be successfully loaded
   {
    Application -> MessageBox((AnsiString("invalid registry entry or ") +
    "background file.can't load a background." +
	solution).c_str(), Application -> Title.c_str(), MB_ICONERROR);
    Application -> Terminate();// Quit the game.
   }
}
void
TMainForm::Redraw(void)
{
Width++;
Width--;
}
void
TMainForm::ToggleUserInterface(const bool state, const TCursor shape)
{
char count = 0, count2;
for (; count < columns; count++) for (count2 = 0; count2 < rows; count2++)
ItsCircles[count][count2] -> Enabled = state;
Cursor = (shape == crNone) ? (state ? crDefault : crHourGlass) : shape;
}
void __fastcall TMainForm::OptionsMenuClick(TObject *Sender)
{
auto_ptr< TShape > CurrentCircle;
char count, count2;
if (Options == NULL) Options = new TOptions(this);// if the options dialog
// hasn't be created before
if (ItsCurrentState == EmptyState)// if no game is running
   {
    if (Options -> ShowModal() == mrOk)
       {
        LoadOptions();
        for (count = 0; count < columns; count++)// Redraw the form.
        for (count2 = 0; count2 < rows; count2++)
           {
            CurrentCircle.reset(ItsCircles[count][count2]);
            CurrentCircle -> Left = ItsLeftMargin +
            count * (CurrentCircle -> Width + ItsHorizontalSpacing);
            CurrentCircle.release();
           }
       }
   }
else Application -> MessageBox(
"Options can't be modified while a game is running.",
Application -> Title.c_str(), MB_OK);
}
//---------------------------------------------------------------------------
void __fastcall TMainForm::blinkerTimer(TObject *Sender)
{
if (blinker -> CurrentTrigger < blinker -> Repetitions - 1)
ItsCircles[ItsFlashingColumn][ItsCurrentState[ItsFlashingColumn].size() -
1] -> Brush -> Color += ItsBlinkingStep;
else// if this is the last flash
   {
    ItsCircles[ItsFlashingColumn][ItsCurrentState[ItsFlashingColumn].
    size() - 1] -> Brush -> Color = TileColor[ItsFlashingPlayer];// Give
    // the current tile its final color.
    if (ItsFlashingPlayer == minimizing) ComputerTurn();// Allow the
    // computer to play.
    else
       {
        if (goal)// If the player or the computer has won,get ready to
        // start a new game(if the user wants to).
           {
            Application -> MessageBox("The computer wins.",
            Application -> Title.c_str(), MB_ICONINFORMATION);
            ItsCurrentState = EmptyState;// Get ready for a new game.
            ToggleUserInterface(false, crDefault);// Prevent the user from
            // continuing the game.
           }
        else ToggleUserInterface(true);// Enable the user interface.
        Redraw();
       }
   }
}
//---------------------------------------------------------------------------
