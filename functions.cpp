/************************************************************

program:      Connect 4.

file:         functions.cpp

function:     ResetColor

description:  represents the circle color to empty

author:       Mohammed Safwat (MS)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  31/12/2003 (MS) first release

************************************************************/
//---------------------------------------------------------------------------
#include <vcl\vcl.h>
#pragma hdrstop

//---------------------------------------------------------------------------
void ResetColor(TShape* const circle)
{
circle -> Brush -> Color = clWhite;
}