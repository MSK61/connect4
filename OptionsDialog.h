/************************************************************

program:      Connect 4.

file:         OptionsDialog.h

function:

description:  represents the interface of the form TOptions

author:       Mohammed Safwat (MS) and Hany Mohammed (HM)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  23/1/2004 (MS and HM) starting construction
              1.01	3/2/2004   (MS)	adding the data member
              ItsChosenBackground

************************************************************/
//----------------------------------------------------------------------------
#ifndef OptionDialogH
#define OptionDialogH
//----------------------------------------------------------------------------
#include "State.h"
#include <vcl\System.hpp>
#include <vcl\Windows.hpp>
#include <vcl\SysUtils.hpp>
#include <vcl\Classes.hpp>
#include <vcl\Graphics.hpp>
#include <vcl\StdCtrls.hpp>
#include <vcl\Forms.hpp>
#include <vcl\Controls.hpp>
#include <vcl\Buttons.hpp>
#include <vcl\ExtCtrls.hpp>
#include <vcl\Dialogs.hpp>
//----------------------------------------------------------------------------
class TOptions : public TForm
{
__published:
	TButton *OKBtn;
	TButton *CancelBtn;
	TBevel *Bevel1;
	TColorDialog *ColorDialog;
	TLabel *UserLabel;
	TShape *UserColor;
	TButton *UserColorButton;
	TButton *ComputerColorButton;
	TShape *ComputerColor;
	TLabel *ComputerLabel;
	TLabel *BackgroundLabel;
	TListBox *BackgroundList;
	TImage *BackgroundImage;
    TLabel *Label1;
    TComboBox *LevelComboBox;
	void __fastcall UserColorButtonClick(TObject *Sender);
	void __fastcall ComputerColorButtonClick(TObject *Sender);
	void __fastcall OKBtnClick(TObject *Sender);
	void __fastcall BackgroundListClick(TObject *Sender);
    void __fastcall FormCreate(TObject *Sender);
private:
char ItsChosenBackground;
vector< Background > ItsBackgrounds;
public:
	virtual __fastcall TOptions(TComponent* AOwner);
};
//----------------------------------------------------------------------------
extern TOptions *Options;
//----------------------------------------------------------------------------
#endif    
