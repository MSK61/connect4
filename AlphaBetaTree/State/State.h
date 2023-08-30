/************************************************************

program:      Connect 4.

file:         State.h

function:

description:  represents the interface of the class State

author:       Mohammed Safwat (MS) and Tarek Kamel (TK) 	

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  26/12/2003 (MS) starting construction
			  1.01  28/12/2003 (MS) first release
              1.02  30/12/2003 (MS) adding a default constructor and an
              assignment operator and removing the NextStep method
              1.03	18/1/2004  (MS)	adding the data member ItsLastChild
              1.04	21/1/2004  (MS)	removing both the overloaded
              assignment operator and the data member ItsLevel
              1.05	27/1/2004  (TK)	adding heuristic methods
              1.06	4/2/2004   (MS) compressing Tarek's code
              1.07	9/2/2004   (MS) separating the class State from the
              class AlphaBetaTree
              1.08  13/2/2004  (MS) replacing the overloaded inequality
              operator with an overloaded equality operator
              1.09  14/2/2004  (MS) using vectors in calculating the
              heuristic
              1.10  7/3/2004   (MS) adding a destructor and declaring the
              class AlphaBetaTree as a friend

************************************************************/
//---------------------------------------------------------------------------
#ifndef StateH
#define StateH
// #define NDEBUG// Comment out in case of debugging.
#include<assert.h>
#include "Background.h"
#include<checks>
#include<io.h>
#include<limits>
#include<list>
#include<memory>
#include<vcl/registry.hpp>
#include<vector>
using Connect4Parameters::columns;
using Connect4Parameters::ApplicationRegistryKey;
using Connect4Parameters::BackgroundRegistryKey;
using Connect4Parameters::BackgroundRegistryValue;
using Connect4Parameters::ComputerRegistryValue;
using Connect4Parameters::OptionsRegistryKey;
using Connect4Parameters::TileColorRegistryKey;
using Connect4Parameters::UserRegistryValue;
using Connect4Parameters::solution;
using Connect4Parameters::rows;
using Connect4Parameters::MaxDifficulty;
using Connect4Parameters::LevelRegistryValue;
using Connect4Parameters::DefaultDifficulty;
using std::auto_ptr;
using std::list;
using std::numeric_limits;
using std::vector;
using System::AnsiString;
enum player{minimizing, maximizing};
typedef vector< player > TileColumn;
class State
   {
    public:
    State();// just to appease the compiler
    ~State();
    bool operator ==(const State& RHS) const;
    friend class AlphaBetaTree;
    private:
    State(const player owner, const vector< TileColumn >& CurrentTile,
    const unsigned char PositionToInsert = columns);
    typedef short int HeuristicType;
    const vector< TileColumn >& GetTiles(void) const;
    void ApplyAlphaBeta(const unsigned char CurrentLevel,
    const unsigned char MaxLevel, const HeuristicType threshold,
    bool& goal, HeuristicType& heuristic, unsigned char& NextStep,
    list< State >& OpenStates, const player beginner,
    const unsigned char ThresholdLevel);
    void Diagonal(vector< unsigned char >& MinGr,
    vector< unsigned char >& MaxGr, unsigned char& MinStreat,
    unsigned char& MaxStreat, const player beginner);
    void Evaluate(const unsigned char Rep, vector< unsigned char >& gr);
    void Evaluate(const unsigned char Row, const unsigned char Rep,
    const player Joker, vector< unsigned char >& MinGr,
    vector< unsigned char >& MaxGr, unsigned char& MinStreat,
    unsigned char& MaxStreat, const player beginner);
    HeuristicType Heuristic(const player beginner);
    void Horizontally(vector< unsigned char >& MinGr,
    vector< unsigned char >& MaxGr, unsigned char& MinStreat,
    unsigned char& MaxStreat, const player beginner);
    void HorizontalScanning(const unsigned char xh,
    const unsigned char row, vector< unsigned char >& MinGr,
    vector< unsigned char >& MaxGr, unsigned char& MinStreat,
    unsigned char& MaxStreat, const player beginner);
    void Incline(const unsigned char x1, const unsigned char y1,
    vector< unsigned char >& MinGr, vector< unsigned char >& MaxGr,
    unsigned char& MinStreat, unsigned char& MaxStreat,
    const player beginner, const char slope);
    void Scan(const unsigned char I, const unsigned char J,
    vector< unsigned char >& MinGr, vector< unsigned char >& MaxGr,
    unsigned char& MinStreat, unsigned char& MaxStreat,
    const player beginner, const char slope);
    void Vertically(vector< unsigned char >& MinGr,
    vector< unsigned char >& MaxGr);
    void VerticalScanning(const unsigned char xv, const unsigned char yv,
    const unsigned char col, vector< unsigned char >& MinGr,
    vector< unsigned char >& MaxGr);
    short int ItsAlphaBeta;
    unsigned char ItsLastChild;// This member variable stores the number
    // of columns that have been tested to generate possible children.This
    // variable helps the node to resume the alpha-beta procedure invoked
    // by the current parent node after this procedure had been started by
    // a previous parent node without repeating children.i.e.The children
    // are generated starting from the last column checked in the last run
    // of the procedure.The old children(that had already been generated)
    // aren't re-generated.
    vector< State* > ItsChildren;
    player ItsOwner;
    vector< TileColumn > ItsTiles;
    bool ItsGoal;
   };
//---------------------------------------------------------------------------
#endif
