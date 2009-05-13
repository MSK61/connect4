
/************************************************************
 *
 * Copyright 2009 Mohammed El-Afifi
 * This file is part of Connect4.
 *
 * Connect4 is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Lesser General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * Connect4 is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public License
 * along with Connect4.  If not, see <http://www.gnu.org/licenses/>.
 *
 * file:         Connect4.java
 *
 * function:     high-level connect4 game semantics
 *
 * description:  manages different roles of the connect4 game
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.io.BufferedReader;
import java.io.IOException;
import java.util.logging.Level;
import java.util.logging.Logger;

/**
 * connect4 game manager, provides input values and decision criteria for the
 * alpha-beta algorithm
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
public class Connect4 extends GameSearch
  {
    /**
     * number of players
     */
    static final byte players = 2;
    /**
     * maximum analysis tree depth
     */
    final byte itsDepth;
    private BufferedReader itsStdIn;

    /**
     * Creates a connect4 game with the speicfied look-ahead depth.
     *
     * @param itsDepth a <code></code> representing the maximum depth of the
     *                 tree
     */
    public Connect4(byte itsDepth)
    {

        logger.fine("Creating a connect4 game...");
        logger.finest("with a custom depth of " + itsDepth);
        this.itsDepth = itsDepth;
        SetStdIn();

    }

    /**
     * Creates a connect4 game with a default look-ahead depth.
     */
    public Connect4()
    {
        final byte defMaxDepth = 3;

        logger.fine("Creating a connect4 game...");
        logger.finest("with a default depth of " + defMaxDepth);
        itsDepth = defMaxDepth;
        SetStdIn();

    }

    @Override
    public boolean drawnPosition(Position p)
    {

        if (p == null) return true;

        return ((Conn4Position)p).Drawn();

    }

    /**
     * Determines if the given player has won the game.
     * The method analyzes the specified position to find if the given player
     * has a winning condition in the position
     *
     * @param p      position to test winning condition for
     * @param player <code>boolean</code> indicating the player to test
     *               the winning condition for
     * @return       <code>true</code> if the specified player has won,
     *               otherwise <code>false</code>
     * @see          Conn4Position#Won(boolean)
     */
    @Override
    public boolean wonPosition(Position p, boolean player)
    {

        /// An invalid position is equivalent to human domination.
        if (p == null) return player == HUMAN;

        return ((Conn4Position)p).Won(player);

    }

    /**
     * Evaluates the alpha/beta value for the given position.
     * The method returns the heuristic value for the given position from the
     * perspective of the specified player. The method is most often called on
     * leaf nodes on a search tree.
     *
     * @param p      position to calculate the heuristic for
     * @param player <code>boolean</code> indicating the player from whose
     *               perspective the heuristic value is to be calculated
     * @return       the heuristic value
     * @see          Conn4Position#Heuristic(boolean)
     */
    @Override
    public float positionEvaluation(Position p, boolean player)
    {

        return ((Conn4Position)p).Heuristic(player);

    }

    /**
     * Prints a representation of the connect4 board layout.
     *
     * @param p position to show
     */
    @Override
    public void printPosition(Position p)
    {

        System.out.println(p);

    }

    /**
     * Gets the next positions directly descendant from the given position.
     * The method generates and returns all positions that could be reached with
     * one move.
     *
     * @param p      position to generate next position from
     * @param player <code>boolean</code> indicating the player to make the next
     *               move
     * @return       array of all possible positions directly reached from the
     *               current position with one move
     * @see          Conn4Position#Conn4Position(
     *               Conn4Position, boolean, Conn4Move)
     * @see          Conn4Position#GetNextMoves()
     */
    @Override
    public Position[] possibleMoves(Position p, boolean player)
    {
        byte count = 0;
        Conn4Position curPos = (Conn4Position)p;
        final Conn4Move[] moves = curPos.GetNextMoves();
        final Conn4Position[] children = new Conn4Position[moves.length];

        logger.fine("moves obtained, generating corresponding children...");

        // Generate a child position for each move.
        for (; count < moves.length; count++) children[count] =
                new Conn4Position(curPos, player, moves[count]);

        return children;

    }

    /**
     * Applies the given move to the given position on behalf of the player.
     * The method appends the provided move to the position resulting in a new
     * child position. The move is made on behalf of the stated player.
     *
     * @param p      position acting as the base for applying the move
     * @param player <code>boolean</code> representing the player on behalf of
     *               whom the move is made
     * @param move   move to appended to the position
     * @return       the new position resulting from making the move
     * @see          Conn4Position#Conn4Position(
     *               Conn4Position, boolean, Conn4Move)
     */
    @Override
    public Position makeMove(Position p, boolean player, Move move)
    {

        return new Conn4Position((Conn4Position)p, player, (Conn4Move)move);

    }

    /**
     * Identifies the end of tree expansion in a certain branch.
     * This method determines if the tree can be further expanded after the
     * given position or not.
     *
     * @param p     position to test the tree expandability at
     * @param depth current length of the active branch in the tree
     * @return      <code>true</code> if the tree can't be further expanded,
     *              otherwise <code>false</code>
     */
    @Override
    public boolean reachedMaxDepth(Position p, int depth)
    {
        Conn4Position pos = (Conn4Position)p;

        return (depth == itsDepth || pos.Won(HUMAN) || pos.Won(PROGRAM) || pos.
            Drawn());

    }

    /** Reads a move from the human user.
     * The method prompts the user for a move to make when it's the human's turn
     * to play. The obtained move is validated statically(but not dynamically
     * depending on the current board layout). Upon entering an invalid move,
     * the method will repeat the prompt until it can get "what seems like" a
     * valid move.
     *
     * @return the move entered specified by the player
     */
    @Override
    public Move createMove()
    {
        boolean invalidMove = true;// move validity falg
        byte move = 0;// just to appease the compiler

        do
            try
            {
                System.out.print(
                    "Enter the column index where you want to throw your tile [0, " + Conn4Position.columns + "]: ");
                move = ReadByte();
                if (move < 0 || move >= Conn4Position.columns)
                    throw new ArrayIndexOutOfBoundsException(
                        "invalid column index,");

                invalidMove = false;

            }
            catch (Exception ex)
            {
                Logger.getLogger(Connect4.class.getName()).
                    log(Level.SEVERE, null, ex);
                System.out.print(ex.toString());
                System.out.print(' ');

            }
        while (invalidMove);

        return new Conn4Move(move);

    }

    private byte ReadByte() throws IOException
    {

        return Byte.parseByte(itsStdIn.readLine());

    }

    private void SetStdIn()
    {

        logger.finer("Chaining to the standard input stream...");
        itsStdIn = new BufferedReader(new java.io.InputStreamReader(System.in));

    }
    private static final Logger logger = Logger.getLogger("Connect4");
  }
