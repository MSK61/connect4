
/************************************************************
 *
 * Copyright 2009, 2014 Mohammed El-Afifi
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
 * description:  depicts the connect4 board at a certain time instant
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.awt.Point;
import java.util.Arrays;
import java.util.LinkedList;
import java.util.Queue;
import java.util.TreeSet;
import java.util.logging.Logger;

/**
 * Connect4 board layout, a snapshot of the connect4 board at a given time
 * instance.
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
        itsTiles = new boolean[columns][];

    }

    /**
     * Returns the string representation of this position.
     * The method will return a valid string even if no tiles exist in this
     * position.
     *
     * @return          string representation of the row
     */
    @Override
    public String toString()
    {
        byte colCount = 0,
            rowCount = rows - 1;
        final char emptyTile = ' ',  humanTile = 'H',  progTile = 'P',  topWall =
            '-',  topRightCorner = '\\',  rightWall = '|';
        final String lineSepProp = "line.separator",  newLine = System.
            getProperty(lineSepProp);
        StringBuilder posStr = new StringBuilder();

        // Draw the top wall.
        for (; colCount < columns; colCount++) posStr.append(topWall);

        posStr.append(topRightCorner);

        // Display the rows along with the right wall.
        for (; rowCount >= 0; rowCount--)
        {

            posStr.append(newLine);
            for (boolean[] col: itsTiles) posStr.append((col == null ||
                    col.length <= rowCount) ? emptyTile : ((col[rowCount] ==
                    Connect4.HUMAN) ? humanTile : progTile));
            posStr.append(rightWall);

        }

        return posStr.toString();

    }

    /**
     * Moves a point to the first possible position in the given directions.
     * The method keeps changing the point coordinates in the specified
     * x and y directions until they match a valid position on the board.
     *
     * @param point  <code>point</code> to be calibrated
     * @param deltaX change step along the x-axis
     * @param deltaY change step along the y-axis
     */
    private static void ClosestPoint(Point point, int deltaX, int deltaY)
    {

        while (!PosExists(point))
        {

            point.x += deltaX;
            point.y += deltaY;

        }

    }

    /**
     * Determines if the given location can be part of a tile group of a player.
     * A location is considered an aiding location for a player if it's occupied
     * by a tile pertaining to that player or if it'll be occupied by such a
     * tile when the directly next insertion to its column is performed by that
     * player.
     *
     * @param tilePoint <code>point</code> to be checked
     * @param player    <code>boolean</code> representing the player estimating
     *                  the location
     * @return          <code>true</code> if the position is an aid point to the
     *                  player, otherwise <code>false</code>
     */
    private boolean AidingPos(Point tilePoint, boolean player)
    {
        return tilePoint.y ==
            ((itsTiles[tilePoint.x] == null) ? 0 : itsTiles[tilePoint.x].length) ||
            OccupiedPos(tilePoint, player);
    }

    /**
     * Returns the groups comopsed  by a player in the given direction.
     * The method calculates the number of non-winning groups in each group type
     * that were composed by the given player in the specified direction and
     * accordingly updates the provided array of group counters. Only surface
     * groups are counted; deeply immersed groups that can't be extended any
     * more won't be counted.
     *
     * @param player <code>boolean</code> representing the player whose groups
     *               will be counted
     * @param groups array of group counters to be updated
     * @param deltaX <code>byte</code> representing how the winning group
     *               extends along the x-axis
     * @param deltaY <code>byte</code> representing how the winning group
     *               extends along the y-axis
     */
    private void CountGroups(boolean player, short[] groups, byte deltaX,
        byte deltaY)
    {
        byte colCount = 0,
            groupSize;
        java.util.Queue<Byte> excludedColumns = new LinkedList<Byte>();

        // Nomalize delta steps across the x and y axes.
        if (deltaX > 1) deltaX = 1;

        if (deltaY > 1) deltaY = 1;
        else if (deltaY < -1) deltaY = -1;

        logger.finest("Counting groups for " + Connect4.GetPlayerDesc(player) +
            " player with slope " + ((deltaX == 0) ? "infinity" : (deltaY /
            deltaX)) + "...");

        for (; colCount < columns; colCount++)// Scan all columns.
        {

            logger.finest("Checking column " + colCount + "...");

            // Skip full columns.
            if (itsTiles[colCount] != null && itsTiles[colCount].length == rows)
                logger.fine("Full column!");
            // Skip the column if its lowest empty tile has been already scanned
            // with the lowest empty tile of a previous column.
            else if ((new Byte(colCount)).equals(excludedColumns.peek()))
            {

                logger.fine("Column already checked, skipping...");
                excludedColumns.remove();

            }
            // Scan tiles surrounding the lowest empty tile in the current
            // column.
            else
            {

                logger.fine("Scanning vicinty tiles...");
                groupSize = GetMaxGroupLen(colCount, player, deltaX, deltaY,
                    excludedColumns);
                logger.finest("The group for column " + colCount + " contains " +
                    groupSize + " tiles.");

                if (groupSize > 0)
                {

                    groups[groupSize - 1]++;
                    logger.fine("New group added!");

                }

            }

        }

    }

    /**
     * Returns the number of tiles owned by a player in the given range.
     * The method counts the number of tiles pertaining to the specified player
     * between the given start and end points. Counting stops upon encountering
     * the first tile belonging to the other player or the first empty tile
     * which can't be filled with a single insertion in its column.
     *
     * @param player     <code>boolean</code> representing the player whose
     *                   tiles are to be counted
     * @param startPoint <code>point</code> indicating the range start
     * @param endPoint   <code>point</code> indicating the range end
     * @param excludes   <code>set</code> of columns to exclude from future
     *                   analysis. This set is also updated inside the method
     *                   (possibly by adding new columns).
     * @return           number of tiles owned by the player in the given range
     */
    private final byte CountTiles(final boolean player, final Point startPoint,
        final Point endPoint, final java.util.Set<Byte> excludes)
    {
        // movement steps along the x and y axes
        int deltaX,
            deltaY;
        byte numOfTiles = 0;

        // Deduce the normalized movement steps along the x and y axes.
        deltaX = (startPoint.x == endPoint.x) ? 0 : 1;
        deltaY = endPoint.y - startPoint.y;

        if (deltaY > 1) deltaY = 1;
        else if (deltaY < -1) deltaY = -1;

        // Scan the range.
        for (; !startPoint.equals(endPoint) && AidingPos(startPoint, player);
            startPoint.x += deltaX, startPoint.y += deltaY) if (OccupiedPos(
                startPoint))
            {

                logger.finest("Found one more tile at " + startPoint + '!');
                numOfTiles++;

            }
            else// Exclude the column from future analysis if necessary.
            {

                logger.finest("Position " + startPoint +
                    " is the lowest empty one in its column; excluding column" +
                    " from further scans...");
                excludes.add((byte)(startPoint.x));

            }

        return 0;

    }

    /**
     * Returns the groups comopsed  by each player in the given direction.
     * The method calculates the number of non-winning groups in each group type
     * that were composed by each player in the specified direction and
     * accordingly updates the provided arrays of group counters. Only surface
     * groups are counted; deeply immersed groups that can't be extended any
     * more won't be counted.
     *
     * @param humanGroups array of group counters to be updated
     * @param progGroups  array of group counters to be updated
     * @param deltaX      <code>byte</code> representing how the winning group
     *                    extends along the x-axis
     * @param deltaY      <code>byte</code> representing how the winning group
     *                    extends along the y-axis
     */
    private void DirGroups(short[] humanGroups, short[] progGroups, byte deltaX,
        byte deltaY)
    {

        // Nomalize delta steps across the x and y axes.
        if (deltaX > 1) deltaX = 1;

        if (deltaY > 1) deltaY = 1;
        else if (deltaY < -1) deltaY = -1;

        logger.finest("Counting groups with slope " + ((deltaX == 0) ? "infinity"
            : (deltaY / deltaX)) + "...");
        // Count groups for both players.
        CountGroups(Connect4.HUMAN, humanGroups, deltaX, deltaY);
        CountGroups(Connect4.PROGRAM, progGroups, deltaX, deltaY);
    }

    /**
     * Returns the length of the group found for a certain player.
     * The method measures the length of the group tangential to the given
     * column in the given direction for the specified player.
     *
     * @param colIndex      <code>byte</code> representing the column through
     *                      which the measured group passes
     * @param player        <code>boolean</code> representing the player whose
     *                      group length will be measured
     * @param deltaX        <code>byte</code> representing how the group extends
     *                      along the x-axis
     * @param deltaY        <code>byte</code> representing how the group extends
     *                      along the y-axis
     * @param totalExcludes <code>queue</code> of columns to exclude from future
     *                      analysis. This queue is also updated inside the
     *                      method(possibly by appending new columns).
     * @return              length of the group owned by the player tangential
     *                      to the column in the given direction
     */
    private byte GetMaxGroupLen(byte colIndex, boolean player, byte deltaX,
        byte deltaY, Queue<Byte> totalExcludes)
    {
        // flag to indicate if the current tile is the lowest empty tile in the
        // column
        boolean colReached;
        byte curGrLen,
            curExclude = 0,
            // length of the largest group passing through the provided column
            maxGrLen = 0;
        // y-coordinate of the first empty location the in given column
        final int emptyTileY =
            (itsTiles[colIndex] == null) ? 0 : itsTiles[colIndex].length,  startSpan =
            collection - 1;
        // group boundaries
        Point curGrStart,
            lastGrStart,
            lastGrEnd;
        final Point curGrEnd = new Point(),  emptyTilePoint =
            new Point(colIndex, emptyTileY),  tilePoint = new Point();
        java.util.Iterator<Byte> excludeCount;
        // columns excluded during the analysis of this column
        TreeSet<Byte> localExcludes = new TreeSet<Byte>();
        int maxGrEndX = 0,// end column of the largest group
            // ranges of start points for possible vicinities
            startSpanX,
            negStartSpanX,
            startSpanY,
            negStartSpanY;

        // Nomalize delta steps across the x and y axes.
        if (deltaX > 1) deltaX = 1;

        if (deltaY > 1) deltaY = 1;
        else if (deltaY < -1) deltaY = -1;

        logger.finest("Searching for the largest tangential group passing through column " +
            colIndex + " with slope " + ((deltaX == 0) ? "infinity" : deltaY /
            deltaX) + " for " + Connect4.GetPlayerDesc(player) + " player...");
        // Calculate the boundaries of location movement in the x and y
        // directions.
        startSpanX = deltaX * startSpan;
        negStartSpanX = -startSpanX;
        startSpanY = deltaY * startSpan;
        negStartSpanY = -startSpanY;
        // Calculate boundary points for vicinities.
        curGrStart = new Point(colIndex + negStartSpanX, emptyTileY +
            negStartSpanY);
        ClosestPoint(curGrStart, deltaX, deltaY);
        lastGrEnd = new Point(colIndex + startSpanX, emptyTileY + startSpanY);
        ClosestPoint(lastGrEnd, -deltaX, -deltaY);
        lastGrStart = new Point(lastGrEnd.x + negStartSpanX + deltaX,
            lastGrEnd.y + negStartSpanY + deltaY);
        ClosestPoint(lastGrStart, deltaX, deltaY);

        // Scan all possible vicinities.
        for (curGrEnd.x = curGrStart.x + deltaX * collection, curGrEnd.y =
                curGrStart.y + deltaY * collection; !curGrStart.equals(
            lastGrStart); curGrStart.x += deltaX, curGrStart.y += deltaY, curGrEnd.x +=
                deltaX, curGrEnd.y += deltaY)
        {

            logger.finest("Scanning adjacent tiles...\n" +
                "Scanning tiles before position " + curGrStart + "...");
            curGrLen = 0;
            // Scan the part of the vicinity before the current column tile.
            tilePoint.x = curGrStart.x;
            tilePoint.y = curGrStart.y;
            colReached = tilePoint.equals(emptyTilePoint);

            while (!colReached && AidingPos(tilePoint, player))
            {

                if (OccupiedPos(tilePoint))
                {

                    logger.finest("Found one more tile at " + tilePoint + '!');
                    curGrLen++;

                }

                tilePoint.x += deltaX;
                tilePoint.y += deltaY;
                colReached = tilePoint.equals(emptyTilePoint);

            }

            // Scan the part of the vicinity past the current column tile if the
            // current vicinity group hasn't been interrupted.
            if (colReached)
            {

                logger.finest("Scanning tiles after position " + tilePoint +
                    "...");
                // Skip the tile of the current column.
                tilePoint.x += deltaX;
                tilePoint.y += deltaY;
                curGrLen += CountTiles(player, tilePoint, curGrEnd,
                    localExcludes);

            }

            if (curGrLen > maxGrLen)// Update the length of the largest group.
            {

                logger.finest("Found a larger group with length " + curGrLen +
                    '!');
                maxGrLen = curGrLen;
                maxGrEndX = curGrEnd.x;

            }

        }

        // Add the columns that were excluded during the analysis of this column
        // to the global queue of excluded columns.
        excludeCount = localExcludes.iterator();

        while (excludeCount.hasNext() && curExclude < maxGrEndX)
        {

            curExclude = excludeCount.next();

            if (curExclude < maxGrEndX)
            {

                logger.finest("Confirming column " + curExclude +
                    " for exclusion from future analysis...");
                totalExcludes.add(curExclude);

            }

        }

        return maxGrLen;

    }

    /**
     * Indicates whether the specified column is part of a player winning group.
     * This method determines if the spcified player has a winning group which
     * extends with the specified criteria along the x and y axes and spanning
     * the specified column.
     *
     * @param colIndex        <code>byte</code> for the column index
     * @param player          <code>boolean</code> representing the player who
     *                        might own a winning group
     * @param deltaX          <code>byte</code> representing how the winning
     *                        group extends along the x-axis
     * @param deltaY          <code>byte</code> representing how the winning
     *                        group extends along the y-axis
     * @param excludedColumns <code>queue</code> of columns to exclude from
     *                        future analysis. This queue is also updated inside
     *                        the method(possibly by appending new columns).
     * @return                <code>true</code> if the specified player has a
     *                        winning group, otherwise <code>false</code>
     */
    private boolean IsWinCol(byte colIndex, boolean player, byte deltaX,
        byte deltaY,
        Queue<Byte> excludedColumns)
    {
        // index of the last tile in the column
        final int baseY = itsTiles[colIndex].length - 1;
        byte groupLen;
        final String playerDesc = Connect4.GetPlayerDesc(player);
        String slope;
        Point tilePoint = new Point();// tile coordinates

        // Nomalize delta steps across the x and y axes.
        if (deltaX > 1) deltaX = 1;

        if (deltaY > 1) deltaY = 1;
        else if (deltaY < -1) deltaY = -1;

        slope = (deltaX == 0) ? "infinity" : String.valueOf(deltaY / deltaX);
        logger.finest("Checking the win condition for " + playerDesc +
            " player with slope " + slope + " at column " + colIndex + "...");

        // Analyze the column only if its last tile belongs to the given player.
        if (itsTiles[colIndex][baseY] == player)
        {

            logger.fine("The last tile in the column belongs to the player.\n" +
                "Scanning adjacent tiles...\n" +
                "Scanning in the forward direction...");
            groupLen = 1;

            // Scan the tiles in the adjacent tiles in pursuit of a winning
            // group.
            // Scan the adjacent tiles in the forward direction.
            for (tilePoint.x = colIndex + deltaX, tilePoint.y = baseY + deltaY;
                TileExists(tilePoint, player) && groupLen < collection;
                tilePoint.x += deltaX, tilePoint.y += deltaY)
            {

                logger.finest("Found one more tile at " + tilePoint + '!');
                groupLen++;

                // Exclude the column from future analysis if necessary.
                if (itsTiles[tilePoint.x].length == tilePoint.y + 1)
                {

                    logger.finest("Tile " + tilePoint +
                        " is the last one in its column; excluding column " +
                        "from further scans...");
                    excludedColumns.add((byte)(tilePoint.x));

                }

            }

            logger.fine("Scanning in the reverse direction...");

            // Scan the adjacent tiles in the backward direction.
            for (tilePoint.x = colIndex - deltaX, tilePoint.y = baseY - deltaY;
                TileExists(tilePoint, player) && groupLen < collection;
                tilePoint.x -= deltaX, tilePoint.y -= deltaY)
            {

                logger.finest("Found one more tile at " + tilePoint + '!');
                groupLen++;

            }

            if (groupLen == collection)// Check the win condition.
            {

                logger.info("Winning group matched at column " + colIndex +
                    " with slope " + slope + " for the " + playerDesc +
                    " player with this position\n" + this + '!');
                return true;

            }

        }

        logger.finest("No winning groups pass through column " + colIndex +
            " in the direction " + slope + '!');
        return false;

    }

    /**
     * Indicates whether a tile exists at the specified location.
     * This method determines if the spcified location is occupied by a tile. It
     * doesn't validate the coordinates with respect to the board dimensions.
     * For such functionality, see {@link #PosExists(java.awt.Point)}.
     *
     * @param locPoint <code>point</code> indicating the location to be checked
     * @return         <code>true</code> if the specified location contains a
     *                 tile, otherwise <code>false</code>
     * @see            #OccupiedPos(java.awt.Point, boolean)
     * @see            #TileExists(java.awt.Point, boolean)
     */
    private final boolean OccupiedPos(Point locPoint)
    {

        return itsTiles[locPoint.x] != null && itsTiles[locPoint.x].length >
            locPoint.y;

    }

    /**
     * Indicates whether the specified location exists in this position.
     * This method determines if the spcified location is valid with respect to
     * the board dimensions. It doesn't check if the specified location is
     * occupied by a tile or not. For such functionality, see {@link
     * #OccupiedPos(java.awt.Point)}.
     *
     * @param locPoint <code>point</code> indicating the location to be checked
     * @return         <code>true</code> if the specified location is valid,
     *                 otherwise <code>false</code>
     * @see            #TileExists(java.awt.Point, boolean)
     */
    private static boolean PosExists(Point locPoint)
    {
        return locPoint.x >= 0 && locPoint.x < columns && locPoint.y >= 0 &&
            locPoint.y < rows;
    }

    /**
     * Indicates whether a tile exists for a player at the specified location.
     * This method determines if the spcified location is occupied by a tile
     * pertaining to the specified player. It doesn't validate the coordinates
     * with respect to the board dimensions.
     * For such functionality, see <code>PosExists(Point)</code>.
     *
     * @param tilePoint <code>point</code> indicating the location to be checked
     * @param player    <code>boolean</code> representing the player who might
     *                  be occupying the location
     * @return          <code>true</code> if the specified location contains a
     *                  tile, otherwise <code>false</code>
     * @see             #OccupiedPos(java.awt.Point)
     * @see             #TileExists(java.awt.Point, boolean)
     */
    private boolean OccupiedPos(Point tilePoint, boolean player)
    {
        return OccupiedPos(tilePoint) && itsTiles[tilePoint.x][tilePoint.y] ==
            player;
    }

    /**
     * Indicates whether a tile exists at the specified location.
     * This method determines if the spcified location is occupied by a tile. It
     * validates both the coordinates and the existence of a tile at these
     * coordinates. For just validating the coordinates, see
     * <code>PosExists</code>. For just validating that location occupation, see
     * <code>OccupiedPos</code>.
     *
     * @param tilePoint <code>point</code> indicating the location to be
     *                  checked
     * @param player    <code>boolean</code> representing the player to be
     *                  checked as the owner of the tile at the location
     * @return          <code>true</code> if the specified tile exists,
     *                  otherwise <code>false</code>
     */
    private boolean TileExists(Point tilePoint, final boolean player)
    {
        return PosExists(tilePoint) && OccupiedPos(tilePoint, player);
    }

    /**
     * Returns a snapshot of this board layout.
     *
     * @return the tiles constituting the board layout
     */
    private boolean[][] getItsTiles()
    {
        return itsTiles;
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
        final boolean[][] srcTiles = conn4Position.getItsTiles();

        logger.fine("Creating a board layout based on a previous layout...");
        logger.finest("for player " + player + "with move at column " +
            moveColIndex);
        // Copy the original board layout first.
        itsTiles = Arrays.copyOf(srcTiles, columns);
        assert moveColIndex >= 0 && moveColIndex < columns:
            "invalid insertion column";
        assert itsTiles[moveColIndex] == null || itsTiles[moveColIndex].length <
            rows: "full column";

        // Copy the column of the new tile carefully.
        if (itsTiles[moveColIndex] == null) itsTiles[moveColIndex] =
                new boolean[1];
        else
        {

            logger.fine("Inserting new tile into a non-empty column...");
            itsTiles[moveColIndex] = new boolean[srcTiles[moveColIndex].length +
                1];
            System.arraycopy(srcTiles[moveColIndex], 0, itsTiles[moveColIndex],
                0, srcTiles[moveColIndex].length);

        }

        // Insert the new tile to its column.
        itsTiles[moveColIndex][itsTiles[moveColIndex].length - 1] = player;

    }

    @Override
    public boolean equals(Object obj)
    {
        byte colCount;
        boolean[][] rHS;

        logger.finer("Comparing two positions...");

        // Heterogeneous objects aren't equal.
        if (!(obj instanceof Conn4Position)) return false;

        rHS = ((Conn4Position)obj).getItsTiles();

        // Any different columns qualify for inequality.
        for (colCount = 0; colCount < columns && Arrays.equals(
            itsTiles[colCount], rHS[colCount]); colCount++) logger.finest(
                "Column " + colCount + " is identical in both positions.");

        return colCount == columns;

    }

    @Override
    public int hashCode()
    {
        int hash = 0;

        for (boolean[] col: itsTiles) if (col != null) hash += col.length;

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
        byte colCount = 0;
        LinkedList<Conn4Move> nextMoves = new LinkedList<Conn4Move>();

        logger.finer("Searching for possible moves...");

        for (; colCount < columns; colCount++)
        {

            logger.finest("Analyzing column " + colCount + "... ");

            if (itsTiles[colCount] == null ||
                itsTiles[colCount].length < rows)
            {

                logger.fine("Possible move found!");
                nextMoves.add(new Conn4Move(
                    colCount));

            }

        }

        return nextMoves.toArray(new Conn4Move[nextMoves.size()]);

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

        logger.finer("Checking the drawn condition...");

        // Scan columns for an incomplete one.
        for (boolean[] col: itsTiles) if (col == null || col.length < rows)
            {

                logger.fine("Incomplete column found!");
                return false;

            }

        logger.finer("All columns complete!");
        return true;

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
        byte count = 0;
        // factors determining how groups extend in all directions
        final byte deltaXHor = 1,  deltaYVer = 1,  deltaYNegVer = -1,  noDelta =
            0,  lastWeight = collection - 1;
        short heuristic = 0;
        // tile groups of players
        final short[] humanGroups = new short[lastWeight],  progGroups =
            new short[lastWeight],  weights = new short[]
        {
            1, 23, 303
        };
        short[] maxGroups,// maximizing player groups
            minGroups;// minimizing player groups
        final short noGroups = 0;

        logger.finest("Calculating the heuristic for " + Connect4.GetPlayerDesc(
            player) + " player with this position\n" + this);
        if (Won(player)) return Short.MAX_VALUE;

        if (Won(!player)) return -Short.MAX_VALUE;

        // Initialize group counters.
        Arrays.fill(humanGroups, noGroups);
        Arrays.fill(progGroups, noGroups);
        // Accumulate numbers of all groups from all directions for both
        // players.
        DirGroups(humanGroups, progGroups, deltaXHor, noDelta);
        DirGroups(humanGroups, progGroups, noDelta, deltaYVer);
        DirGroups(humanGroups, progGroups, deltaXHor, deltaYVer);
        DirGroups(humanGroups, progGroups, deltaXHor, deltaYNegVer);

        // Determine the maximizing and minimizing players.
        if (player == Connect4.HUMAN)
        {

            maxGroups = humanGroups;
            minGroups = progGroups;

        }
        else
        {

            maxGroups = progGroups;
            minGroups = humanGroups;

        }

        // Accumulate the heuristic value.
        for (; count < lastWeight; count++) heuristic += (maxGroups[count] -
                minGroups[count]) * weights[count];

        return heuristic;

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
        // factors determining how groups extend in all directions
        final byte deltaXHor = 1,  deltaYVer = 1,  deltaYNegVer = -1,  noDelta =
            0;

        logger.finest("Checking the win condition for " +
            Connect4.GetPlayerDesc(player) + " player with this position\n" +
            this);
        // Analyze the winning condition in all directions.
        return WonDir(player, deltaXHor, noDelta) || WonDir(player, noDelta,
            deltaYNegVer) || WonDir(player, deltaXHor, deltaYVer) || WonDir(
            player, deltaXHor, deltaYNegVer);

    }

    /**
     * Indicates whether the specified player has won in the given direction.
     * This method determines if the spcified player has a winning group in the
     * specified direction.
     *
     * @param player <code>boolean</code> representing the player who might have
     *               won
     * @param deltaX <code>byte</code> representing how the winning group
     *               extends along the x-axis
     * @param deltaY <code>byte</code> representing how the winning group
     *               extends along the y-axis
     * @return       <code>true</code> if the specified player won, otherwise
     *               <code>false</code>
     */
    private final boolean WonDir(final boolean player, byte deltaX, byte deltaY)
    {
        byte colCount = 0;
        java.util.Queue<Byte> excludedColumns = new LinkedList<Byte>();

        // Nomalize delta steps across the x and y axes.
        if (deltaX > 1) deltaX = 1;

        if (deltaY > 1) deltaY = 1;
        else if (deltaY < -1) deltaY = -1;

        logger.finest("Checking the win condition for " +
            Connect4.GetPlayerDesc(player) + " player with slope " + ((deltaX ==
            0) ? "infinity" : (deltaY / deltaX)) + "...");

        for (; colCount < columns; colCount++)// Scan all columns.
        {

            logger.finest("Checking column " + colCount + "...");

            // Skip empty columns.
            if (itsTiles[colCount] == null) logger.fine("Empty column!");
            // Skip the column if its topmost tile has been already scanned with
            // the topmost tile of a previous column.
            else if ((new Byte(colCount)).equals(excludedColumns.peek()))
            {

                logger.fine("Column already checked, skipping...");
                excludedColumns.remove();

            }
            // Scan tiles surrounding the last tile in the current column.
            else if (IsWinCol(colCount, player, deltaX, deltaY, excludedColumns))
            {

                logger.finest("Win condition detected at column " + colCount +
                    '!');
                return true;

            }

        }

        logger.finer("No win condition detected!");
        return false;

    }
    /**
     * a winning collection
     */
    static final byte collection = 4;
    /**
     * board layout
     */
    private boolean[][] itsTiles;
    private static final Logger logger = Logger.getLogger("Conn4Position");
  }
