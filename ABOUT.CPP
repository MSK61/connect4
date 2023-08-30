/************************************************************

program:      Connect 4.

file:         AlphaBetaTree.cpp

function:     methods of the AlphaBetaTree class

description:  represents the implementation of the class AlphaBetaTree

author:       Mohammed Safwat (MS)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  28/12/2003 (MS) starting construction
			  1.01	29/12/2003 (MS)	first release
              1.02	1/1/2004   (MS) adding the methods ExecutePlayerTurn
              and ApplyTurn
              1.03	3/2/2004   (MS)	adding registry capabilities

************************************************************/
//---------------------------------------------------------------------
#include <vcl.h>
#pragma hdrstop

#include "about.h"
//--------------------------------------------------------------------- 
#pragma resource "*.dfm"
TAboutBox *AboutBox;
//--------------------------------------------------------------------- 
__fastcall TAboutBox::TAboutBox(TComponent* AOwner)
	: TForm(AOwner)
{
ProductName -> Caption = Application -> Title;
}
//--------------------------------------------------------------------- 
