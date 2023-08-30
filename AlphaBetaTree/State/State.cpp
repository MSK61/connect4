/************************************************************

program:      Connect 4.

file:         State.cpp

function:     methods of the State class

description:  represents the implementation of the class State

author:       Mohammed Safwat (MS) and Tarek Kamel (TK)

environment:  borland c++ builder 1.0, windows 98 second edition
              borland c++ builder 6, windows98 second edition

notes:        This is a project program.

revisions:    1.00  26/12/2003 (MS) starting construction
              1.01  30/12/2003 (MS) adding a default constructor and an
              assignment operator and removing the NextStep method
              1.02	18/1/2004  (MS)	initializing the member ItsLastChild
              and modifying the alpha-beta procedure
              1.03	21/1/2004  (MS)	removing both the overloaded
              assignment operator and the data member ItsLevel
              1.04	27/1/2004  (TK)	adding heuristic methods
              1.05	4/2/2004   (MS) compressing Tarek's code
              1.06	9/2/2004   (MS) separating the class State from the
              class AlphaBetaTree
              1.07  13/2/2004  (MS) replacing the overloaded inequality
              operator with an overloaded equality operator
              1.08  14/2/2004  (MS) using vectors in calculating the
              heuristic
              1.09  20/2/2004  (MS) changing most local constants in
              methods to static constants
              1.10  7/3/2004   (MS) adding a destructor and declaring the
              class AlphaBetaTree as a friend

************************************************************/
//---------------------------------------------------------------------------
#include <vcl\vcl.h>
#pragma hdrstop

#include "State.h"
//---------------------------------------------------------------------------
using Connect4Parameters::collection;
State::State()// just to appease the compiler
{
}
State::~State()
{
unsigned char count = 0, children = ItsChildren.size();
// Separate this state from all its children.
for (; count < children; count++) ItsChildren[count] = NULL;
}
bool
State::operator ==(const State& RHS) const
{
return (ItsTiles == RHS.GetTiles());
}
State::State(const player owner, const vector< TileColumn >& CurrentTiles,
const unsigned char PositionToInsert):ItsOwner(owner), ItsLastChild(0),
ItsTiles(CurrentTiles)
{
WARN(PositionToInsert >= columns, "invalid insertion column");
if (PositionToInsert < columns)// if there is a tile to insert
   {
    assert(ItsTiles[PositionToInsert].size() < rows);
    ItsTiles[PositionToInsert].push_back(owner == maximizing ?
    minimizing : maximizing);// Insert the new tile.
   }
}
const vector< TileColumn >&
State::GetTiles(void) const
{
return ItsTiles;
}
void
State::ApplyAlphaBeta(const unsigned char CurrentLevel,
const unsigned char MaxLevel, const HeuristicType threshold, bool& goal,
HeuristicType& heuristic, unsigned char& NextStep,
list< State >& OpenStates, const player beginner,
const unsigned char ThresholdLevel)
{
// At the end of this function,the two variables goal and NextStep
// determines the result of this function as follows.
// 	goal	NextStep	result
//	false	<columns	The next maximizing player's step won't make him
//						win.
//	false	columns		The game has ended to a position after which it
//						can't be continued.i.e.All tile places have been
//						filled and neither the maximizing player nor the
//						minimizing one has won.
//	true	<columns	The next maximizing player's step will make him
//						win.i.e.The goal will be achieved by the
//						maximizing player.
//	true	columns		The minimizing player has won.
// Note that if NextStep equals columns,the next step is invalid and the
// game can't be continued any more.Note also that NextStep and goal are
// significant only if they're returned by the root state.NextStep has no
// meaning if it is returned by a state other than the root one.The value
// of goal in the states other than the root one changes according to
// the internal processing.
auto_ptr< State > temp;
list< State >::iterator end, result;
if (ItsLastChild == 0)// if this is the first time the procedure is
// applied for this state
   {
    ItsAlphaBeta = Heuristic(beginner);
    if (ItsGoal || CurrentLevel == MaxLevel) ItsLastChild = columns;// an
    // indication that there are no more children to be generated
	if (CurrentLevel == 0) NextStep = columns;// Initialize the next step
    // with an invalid step.
   }
// Test if this state satisfies the goal.If it satisfies the goal,inform
// its parent to stop generating children.This is done because a direct
// solution has been found to achieve the goal and make the parent state
// player wins.This saves time of finding another possible solutions(Only
// one solution is enough).
// Note also that if a state satisfies a goal,then it's barren(i.e. It has
// no children) since the game can't be continued after one of the players
// wins.
if (ItsLastChild < columns)// if this isn't the last level in the tree and
// the goal hasn't been reached yet(by the current state or one of its
// children in a previous call for the algorithm for this state)
   {
    goal = false;
    // Iterate through all possible children to apply the alpha-beta
    // pruning for each(depth-first).
	// Note that a child state satisfying the goal has the right to
    // prevent its parent(the root state) from generating more children
    // since it provides a direct solution.
    for (; ItsLastChild < columns && (ItsOwner == maximizing ?
    ItsAlphaBeta < threshold : ItsAlphaBeta > threshold) && ! goal;
    ItsLastChild++) if (ItsTiles[ItsLastChild].size() < rows)// if the
    // current column isn't full
       {
        // If this is the first child to be generated for this state,
        // initialize the value of alpha(or beta) with an extreme value(a
        // value that guarantee that the current state will be affected by
        // the value of beta(or alpha) of this child.
        if (ItsChildren.size() == 0) ItsAlphaBeta =
        ItsOwner == maximizing ? numeric_limits<HeuristicType>::min() :
        numeric_limits<HeuristicType>::max();
        // Generate a child state.Note that the new child belongs to the
        // minimizing player if the current state(the parent) belongs to
        // the maximizing one and vice versa.Note also that the level of
        // the new child is 1 higher than the current one(the parent).
        temp.reset(new State(ItsOwner == maximizing ?
        minimizing : maximizing, ItsTiles,  ItsLastChild));
        if (CurrentLevel >= ThresholdLevel)// If the child just created
        // could have been generated by another parent,check if this child
        // has already been generated by another parent.
           {
            end = OpenStates.end();
            result = std::find(OpenStates.begin(), end, *temp);
            if (result == end)// if this is the first time the child is
            // generated
               {
            	OpenStates.push_front(*temp);// Add this child to the
            	// OpenStates set.
                temp.reset(&(OpenStates.front()));
               }
        	else temp.reset(&(*result));// if this child has already been
            // generated by another parent
           }
        temp -> ApplyAlphaBeta(CurrentLevel + 1, MaxLevel, ItsAlphaBeta,
        goal, heuristic, NextStep, OpenStates, beginner, ThresholdLevel);
        ItsChildren.push_back(temp.release());// Add this child to the set
        // of its parent's children.
        // Update the alpha-beta value of this state and determine
        // temporarily the next step.
        if ((ItsOwner == maximizing ? heuristic > ItsAlphaBeta :
        heuristic < ItsAlphaBeta) || goal)
           {
            ItsAlphaBeta = heuristic;
            if (CurrentLevel == 0) NextStep = ItsLastChild;
           }
       }
    if (goal) ItsLastChild = columns;// just an indication that there are
    // no more children to be generated(for later calls for this function
    // for the same state)
   }
if (CurrentLevel > 0 || ItsGoal) goal = ItsGoal;// Prevent any goal
// achieved by any of the current state's children from being passed to
// the current state's parent.The root state is an exception since a child
// state satisfying the goal means that a direct solution(with one step
// only) has been found.
// Return the alpha-beta value of this state to the parent state.
heuristic = ItsAlphaBeta;
}
void
State::Diagonal(vector< unsigned char >& MinGr,
vector< unsigned char >& MaxGr, unsigned char& MinStreat,
unsigned char& MaxStreat, const player beginner)
{
static const unsigned char MaxHorizontalStart = columns - collection,
MaxVerticalStart = rows - collection;
unsigned char count = 0, count2;
for (; count <= MaxVerticalStart && ! ItsGoal; count++)
   {
    for (count2 = 0; count2 <= MaxHorizontalStart && ! ItsGoal; count2++)
    Scan(count2, count, MinGr, MaxGr, MinStreat, MaxStreat, beginner, 1);
    for (count2 = collection - 1; count2 < columns && ! ItsGoal; count2++)
    Scan(count2, count, MinGr, MaxGr, MinStreat, MaxStreat, beginner, -1);
   }
}
void
State::Evaluate(const unsigned char Rep, vector< unsigned char >& gr)
{
gr[collection - Rep - 1]++;
if (Rep == 0) ItsGoal = true;
}
void
State::Evaluate(const unsigned char Row, const unsigned char Rep,
const player Joker, vector< unsigned char >& MinGr,
vector< unsigned char >& MaxGr, unsigned char& MinStreat,
unsigned char& MaxStreat, const player beginner)
{
if (Joker == maximizing)
   {
    Evaluate(Rep, MaxGr);
    if (Rep == 1 && ((Row % 2 == 0 && beginner == maximizing) ||
    (Row % 2 == 1 && beginner == minimizing))) MaxStreat++;
   }
else
   {
    Evaluate(Rep, MinGr);
    if (Rep == 1 && ((Row % 2 == 0 && beginner == minimizing) ||
    (Row % 2 == 1 && beginner == maximizing))) MaxStreat++;
   }
}
State::HeuristicType
State::Heuristic(const player beginner)
{
static const unsigned char LastWeight = collection - 1,
ThreatGroup = LastWeight - 1;
HeuristicType heuristic;
unsigned char count, MinStreat = 0, MaxStreat = 0;
vector< unsigned char > MinGr(collection, 0), MaxGr(collection, 0);
vector< HeuristicType > MaxWeight(collection), MinWeight(collection);
MaxWeight[0] = 1;
MaxWeight[1] = 23;
MaxWeight[2] = 303;
MaxWeight[3] = 3483;
MinWeight[0] = 12;
MinWeight[1] = 265;
MinWeight[2] = 3482;
MinWeight[3] = 32170;
ItsGoal = false;
Vertically(MinGr, MaxGr);
if (! ItsGoal)// if the goal hasn't been reached
   {
    Horizontally(MinGr, MaxGr, MinStreat, MaxStreat, beginner);
	if (! ItsGoal)// if the goal hasn't been reached
       {
        Diagonal(MinGr, MaxGr, MinStreat, MaxStreat, beginner);
        if (! ItsGoal)// if the goal hasn't been reached
           {
            heuristic = 0;
            for (count = 0; count < LastWeight; count++)// In mat lab,this
            // operation would be written in one statement.
            heuristic += MaxWeight[count] * MaxGr[count] -
            MinWeight[count] * MinGr[count];
            heuristic += MaxWeight[ThreatGroup] * MaxStreat -
            MinWeight[ThreatGroup] * MinStreat;
            return heuristic;
           }
       }
   }
return (MaxGr[LastWeight] > 0 ? MaxWeight[LastWeight] :
-MinWeight[LastWeight]);
}
void
State::Horizontally(vector< unsigned char >& MinGr,
vector< unsigned char >& MaxGr, unsigned char& MinStreat,
unsigned char& MaxStreat, const player beginner)
{
static const unsigned char MaxStart = columns - collection;
bool max, min;
unsigned char count, j, i;
for (j = 0; j < rows && ! ItsGoal; j++)// Iterate as long as the goal
// hasn't been reached.
for (i = 0; i <= MaxStart && ! ItsGoal; i++)// Iterate as long as the goal
// hasn't been reached.
   {
	max = false;
	min = false;
    for (count = 0; count < collection && (! min || ! max); count++)
    if (ItsTiles[i + count].size() > j)
    if (ItsTiles[i + count][j] == minimizing) min = true;
    else max = true;
   	if((! max && min) || (! min && max)) HorizontalScanning(i, j, MinGr,
    MaxGr, MinStreat, MaxStreat, beginner);
   }
}
void
State::HorizontalScanning(const unsigned char xh, const unsigned char row,
vector< unsigned char >& MinGr, vector< unsigned char >& MaxGr,
unsigned char& MinStreat, unsigned char& MaxStreat, const player beginner)
{
unsigned char rep = 0;
player joker;
const unsigned char GroupEnd = collection + xh;
for (int i = xh; i < GroupEnd; i++) if (ItsTiles[i].size() <= row) rep++;
else joker = ItsTiles[i][row];
Evaluate(row, rep, joker, MinGr, MaxGr, MinStreat, MaxStreat, beginner);
}
void
State::Incline(const unsigned char x1, const unsigned char y1,
vector< unsigned char >& MinGr, vector< unsigned char >& MaxGr,
unsigned char& MinStreat, unsigned char& MaxStreat, const player beginner,
const char slope)
{
unsigned char rep = 0, row, k = 0, j = y1;
player joker;
char i = x1;
for(; k < collection; i += slope, j++, k++) if (ItsTiles[i].size() <= j)
   {
    rep++;
    row = i;
   }
else joker = ItsTiles[i][j];
Evaluate(row, rep, joker, MinGr, MaxGr, MinStreat, MaxStreat, beginner);
}
void
State::Scan(const unsigned char I, const unsigned char J,
vector< unsigned char >& MinGr, vector< unsigned char >& MaxGr,
unsigned char& MinStreat, unsigned char& MaxStreat, const player beginner,
const char slope)
{
bool min = false, max = false;
char i = I;
unsigned char j = J, k = 0;
for (; k < collection && (! min || ! max); i += slope, j++, k++)
if (ItsTiles[i].size() > j) if (ItsTiles[i][j] == minimizing) min = true;
else max = true;
if((! max && min) || (! min && max))
Incline(I, J, MinGr, MaxGr, MinStreat, MaxStreat, beginner, slope);
}
void
State::Vertically(vector< unsigned char >& MinGr,
vector< unsigned char >& MaxGr)
{
unsigned char T;
for (unsigned char i = 0; i < columns && ! ItsGoal; i++)// Iterate as long
// as the goal hasn't been reached.
   {
    T = ItsTiles[i].size();
    if (T != 0)// not empty
   	   {
        T--;
        VerticalScanning(T < collection ? 0 : T - collection + 1, T, i,
        MinGr, MaxGr);
       }
   }
}
void
State::VerticalScanning(const unsigned char xv, const unsigned char yv,
const unsigned char col, vector< unsigned char >& MinGr,
vector< unsigned char >& MaxGr)
{
char count;
unsigned char complement, GoalColumn;
// We want to see how many adjacent tiles belong to the same player.
for (count = yv; count >= xv && ItsTiles[col][yv] == ItsTiles[col][count];
count--);
GoalColumn = collection + count;
if (GoalColumn < rows)//*************reach the top
   {
    complement = GoalColumn - yv;
    if (ItsTiles[col][yv] == maximizing)
	Evaluate(complement, MaxGr);
	else Evaluate(complement, MinGr);
   }
}
