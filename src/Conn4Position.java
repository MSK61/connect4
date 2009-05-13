
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
 * file:         Conn4Position.java
 *
 * function:     connect4 board snapshot
 *
 * description:  depicts the connect4 board a certain time instant
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.util.Arrays;
import java.util.logging.Logger;

/**
 * connect4 board layout, a snapshot of the connect4 board at a given time
 * instance
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
public class Conn4Position extends Position
  {
    /**
     * number of rows on the board
     */
    static final byte rows = 6;

    Conn4Position()
    {

        logger.fine("Creating an empty board...");
        itsTiles = new TileType[columns][];

    }

    /**
     * Returns a snapshot of the current board layout.
     *
     * @return the tiles constituting the board layout
     */
    private TileType[][] getItsTiles()
    {
        return itsTiles;
    }

    private enum TileType
      {
        /**
         * tile representing the human
         */
        humanTile,
        /**
         * tile representing the computer
         */
        programTile
      }
    /**
     * number of columns on the board
     */
    static final byte columns = 7;

    /**
     * Creates a board layout by applying a new tile to an existing layout.
     * This constructor creates a new board layout by inserting a new tile for
     * the given player at the specified column. It can be used for generating
     * a child(direct offspring) of a certain layout. The resulting board layout
     * is the same as the provided one except for the specified column having an
     * extra tile for the given player.
     *
     * @param conn4Position the conn4Position to append the new tile to
     * @param player        <code>boolean</code> representing the owner of the
     *                      tile to be inserted
     * @param conn4Move     the conn4Move to apply to compose this board layout
     */
    public Conn4Position(Conn4Position conn4Position, boolean player,
        Conn4Move conn4Move)
    {
        // column representing the move
        final byte moveColIndex = conn4Move.getItsColumn();
        final TileType[][] srcTiles = conn4Position.getItsTiles();

        logger.fine("Creating a board layout based on a previous layout...");
        logger.finest("for player " + player + "with move at column " +
            moveColIndex);
        /// Copy the original board layout first.
        itsTiles = Arrays.copyOf(srcTiles, columns);
        assert moveColIndex >= 0 && moveColIndex < columns:
            "invalid insertion column";
        assert itsTiles[moveColIndex] == null || itsTiles[moveColIndex].length <
            rows: "full column";

        /// Copy the column of the new tile carefully.
        if (itsTiles[moveColIndex] == null) itsTiles[moveColIndex] =
                new TileType[1];
        else
        {

            logger.finer("Inserting new tile into a non-empty column...");
            itsTiles[moveColIndex] =
                new TileType[srcTiles[moveColIndex].length + 1];
            System.arraycopy(srcTiles[moveColIndex], 0, itsTiles[moveColIndex],
                0, srcTiles[moveColIndex].length);

        }

        itsTiles[moveColIndex][itsTiles[moveColIndex].length - 1] = GetTile(
            player);/// Insert the new tile to its column.

    }

    @Override
    public boolean equals(Object obj)
    {
        byte colCount;
        TileType[][] rHS;

        logger.finer("Comparing two positions...");

        /// Heterogeneous objects aren't equal.
        if (getClass() == obj.getClass()) return false;

        rHS = ((Conn4Position)obj).getItsTiles();

        /// Any different columns qualify for inequality.
        for (colCount = 0; colCount < columns && Arrays.equals(
            itsTiles[colCount], rHS[colCount]); colCount++)
        {

            logger.finest("Column " + colCount +
                " is identical in both positions");

        }

        return colCount == columns;

    }

    @Override
    public int hashCode()
    {
        int hash = 5;
        return hash;
    }

    /**
     * Returns the possible moves to generate the direct offspring layouts.
     * The returned moves are the ones needed to generate the next level of
     * board layouts.
     *
     * @return moves leading to the offspring layouts of this board layout
     */
    public Conn4Move[] GetNextMoves()
    {

        return null;

    }

    private static final TileType GetTile(final boolean player)
    {

        return (player == GameSearch.HUMAN) ? TileType.humanTile : TileType.programTile;

    }

    /**
     * Indicates whether this position is a dead end.
     * This method determines if the game can be further continued or not. It
     * can be used as a criterion for stopping the tree expansion for search
     * algorithms.
     *
     * @return <code>true</code> if the game can't be continued, otherwise
     *         <code>false</code>
     */
    public boolean Drawn()
    {

        return false;

    }

    /**
     * Calculates the heuristic value for the specified player.
     * This method returns a fitness value of this <code>Conn4Position</code>
     * for the specified player; it's most likely called at search tree leaves.
     * Note that the heuristic value for a certain player is the exact negative
     * of that for the other player.
     *
     * @param player <code>boolean</code> representing the player for which the
     *               heuristic (fitness) value is calculated
     * @return       heuristic value for the specified player
     */
    public final short Heuristic(final boolean player)
    {

        return 0;

    }

    /**
     * Indicates whether the specified player won the game.
     * This method determines if the spcified player won the game. It can be
     * used as a criterion for stopping the tree expansion for search
     * algorithms.
     *
     * @param player <code>boolean</code> representing the player who might have
     *               won
     * @return       <code>true</code> if the specified player won, otherwise
     *               <code>false</code>
     */
    public boolean Won(final boolean player)
    {

        return false;

    }
    /**
     * board layout
     */
    private TileType[][] itsTiles;
    private static final Logger logger = Logger.getLogger("Conn4Position");
  }
