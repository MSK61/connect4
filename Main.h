/************************************************************

program:      Connect 4.

file:         Main.h

function:

description:  represents the interface of the class TMainForm

author:       Mohammed Safwat (MS) and Hany Mohammed (HM)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  28/12/2003 (MS) starting construction
			  1.01	29/12/2003 (MS) first release
              1.02	1/1/2004   (MS) adding the methods ExecutePlayerTurn
              and ApplyTurn and the member TileColor
              1.03  24/1/2004  (MS and HM) adding some extra GUI features
              1.04  13/2/2004  (MS) rebuilding in borland c++ builder 6
              1.05  16/2/2004  (MS) adding relevant data to the flashing
              effect
              1.06  21/2/2004  (MS) replacing the TTimer component(needed
              for flashing) with a FiniteRepeatTimer component and
              removing some unnecessary member data
              1.07  22/2/2004  (MS) adding the method Redraw

************************************************************/
//---------------------------------------------------------------------------
#ifndef MainH
#define MainH
//---------------------------------------------------------------------------
#include "AlphaBetaTree.h"
#include <vcl\ComCtrls.hpp>
#include <vcl\ExtCtrls.hpp>
#include <Classes.hpp>
#include <Controls.hpp>
#include <Menus.hpp>
#include "FiniteRepeatTimer.h"
//---------------------------------------------------------------------------
using System::TObject;
class TMainForm : public TForm
{
__published:
	TMainMenu *MainMenu;
	TMenuItem *FileNewItem;
	TMenuItem *FileExitItem;
	TMenuItem *HelpAboutItem;
	TStatusBar *StatusLine;
	TMenuItem *OptionsMenu;
	TImage *BackgroundImage;
    FiniteRepeatTimer *blinker;
    TMenuItem *GameMenu;
	void __fastcall GameNew(TObject *Sender);
	void __fastcall GameExit(TObject *Sender);
	void __fastcall HelpAbout(TObject *Sender);

	void __fastcall OptionsMenuClick(TObject *Sender);

	void __fastcall FormCreate(TObject *Sender);
    void __fastcall blinkerTimer(TObject *Sender);
private:        // private user declarations
void ApplyTurn(const unsigned char column, const player owner);
void ComputerTurn(void);
void __fastcall ExecutePlayerTurn(TObject* Sender,
TMouseButton Button, Classes::TShiftState Shift, int X, int Y);
void LoadOptions(void);
void Redraw(void);
void ToggleUserInterface(const bool state, const TCursor shape = crNone);
player ItsBeginner, ItsFlashingPlayer;
vector< vector< TShape* > > ItsCircles;
vector< TileColumn > ItsCurrentState;
const unsigned short int ItsCircleDiameter, ItsHorizontalSpacing;
unsigned short int ItsLeftMargin;
TColor TileColor[2];
short int ItsBlinkingStep;
unsigned char ItsFlashingColumn, ItsMaxLevel;
bool goal;
public:         // public user declarations
	virtual __fastcall TMainForm(TComponent* Owner);
};
//---------------------------------------------------------------------------
extern TMainForm *MainForm;
//---------------------------------------------------------------------------
#endif
