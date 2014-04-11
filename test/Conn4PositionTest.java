
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
 * file:         Conn4PositionTest.java
 *
 * function:     Conn4Position class unit tests
 *
 * description:  tests different methods of the Connect4 class
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.util.LinkedList;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
public class Conn4PositionTest
  {
    public Conn4PositionTest()
    {
    }

    @BeforeClass
    public static void setUpClass() throws Exception
    {
    }

    @AfterClass
    public static void tearDownClass() throws Exception
    {
    }

    /**
     * Test of hashCode method, of class Conn4Position.
     */
    @Test
    public void testHashCode()
    {
        System.out.println("hashCode");
        Conn4Position instance = new Conn4Position();
        int expResult = 0;
        int result = instance.hashCode();
        assertEquals(expResult, result);

    }

    /**
     * Test of GetNextMoves method, of class Conn4Position.
     */
    @Test
    public void testGetNextMoves()
    {
        byte columnCount = 0,
            rowCount;
        // necessary test columns and moves
        final byte[] columnHeights = new byte[Conn4Position.columns],  testHeights = new byte[]
        {
            Conn4Position.rows, Conn4Position.rows / 2, 0
        };
        // It really doesn't matter which player will start.
        boolean player = (new java.util.Random()).nextBoolean();
        final int validTestCols = Math.min(testHeights.length,
            Conn4Position.columns);
        final LinkedList<Conn4Move> moves = new LinkedList<Conn4Move>();

        System.out.println("GetNextMoves with " + Connect4.GetPlayerDesc(player) +
            " beginner");
        // Determine how many tiles to be inserted in each column.
        System.arraycopy(testHeights, 0, columnHeights, 0, validTestCols);
        // Fill the remaining columns completely.
        java.util.Arrays.fill(columnHeights, validTestCols,
            Conn4Position.columns, Conn4Position.rows);
        Conn4Position instance = new Conn4Position();

        // Compose the board layout.
        for (columnCount = 0; columnCount < Conn4Position.columns; columnCount++)
            for (rowCount = 0; rowCount < columnHeights[columnCount]; rowCount++, player =
                    !player) instance = new Conn4Position(instance, player,
                    new Conn4Move(columnCount));

        // Calculate the expected response.
        for (columnCount = 0; columnCount < Conn4Position.columns; columnCount++)
            if (columnHeights[columnCount] < Conn4Position.rows) moves.add(
                    new Conn4Move(columnCount));

        Conn4Move[] expResult = new Conn4Move[moves.size()];
        moves.toArray(expResult);
        Conn4Move[] result = instance.GetNextMoves();
        assertArrayEquals(expResult, result);

    }
  }
