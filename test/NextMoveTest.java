
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
 * file:         NextMoveTest.java
 *
 * function:     unit test
 *
 * description:  tests the possibleMoves method of the Connect4 class for
 *               different players
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.util.ArrayList;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import org.junit.runners.Parameterized;
import static org.junit.Assert.*;

/**
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
@org.junit.runner.RunWith(Parameterized.class)
public class NextMoveTest
  {
    private static Connect4 testObj;
    private boolean itsPlayer;

    public NextMoveTest(final boolean player)
    {

        itsPlayer = player;

    }

    @Parameterized.Parameters
    public static java.util.Collection<Object[]> InjectData()
    {

        return java.util.Arrays.asList(new Object[]
            {
                Connect4.HUMAN
            }, new Object[]
            {
                Connect4.PROGRAM
            });

    }

    @BeforeClass
    public static void setUpClass() throws Exception
    {

        testObj = new Connect4();

    }

    @AfterClass
    public static void tearDownClass() throws Exception
    {
    }

    // TODO add test methods here.
    // The methods must be annotated with annotation @Test. For example:
    //
    // @Test
    // public void hello() {}
    @Test
    public void test()
    {
        byte columnCount = 0,
            rowCount,
            moveCount = 0;
        // necessary test columns and moves
        final byte[] columnHeights = new byte[Conn4Position.columns],  testHeights = new byte[]
        {
            Conn4Position.rows - 1, Conn4Position.rows / 2, 0
        };
        // move needed to initialize the mock position
        final byte mockMove = 0;
        int numOfMoves;
        Conn4Position testPos = new Conn4Position();
        byte totalTiles = 0;
        final int validTestCols = Math.min(testHeights.length,
            Conn4Position.columns);
        final ArrayList<Conn4Move> moves = new ArrayList(validTestCols);
        final Conn4Position[] expectedResult;

        System.out.println("possibleMoves with " + Connect4Test.GetPlayerDesc(
            itsPlayer) + " player");

        // Determine how many tiles to be inserted in each column.
        for (; columnCount < validTestCols; columnCount++)
        {

            columnHeights[columnCount] = testHeights[columnCount];
            totalTiles += columnHeights[columnCount];

        }

        // Fill the remaining columns completely.
        for (; columnCount < Conn4Position.columns; columnCount++)
        {

            columnHeights[columnCount] = Conn4Position.rows;
            totalTiles += columnHeights[columnCount];

        }

        // Determine the first player.
        if (totalTiles / Connect4.players == 0) itsPlayer = !itsPlayer;

        // Compose the board layout.
        for (columnCount = 0; columnCount < Conn4Position.columns; columnCount++)
            for (rowCount = 0; rowCount < columnHeights[columnCount]; rowCount++, itsPlayer =
                    !itsPlayer) testPos = new Conn4Position(testPos, itsPlayer,
                    new Conn4Move(columnCount));

        // Fill the array of moves.
        columnHeights[mockMove]++;

        for (columnCount = 0; columnCount < Conn4Position.columns; columnCount++)
            if (columnHeights[columnCount] < Conn4Position.rows) moves.add(
                    new Conn4Move(columnCount));


        try
        {
            testPos = org.easymock.classextension.EasyMock.createMock(
                Conn4Position.class,
                new org.easymock.classextension.ConstructorArgs(
                Conn4Position.class.getConstructor(Conn4Position.class,
                boolean.class, Conn4Move.class), testPos, itsPlayer,
                new Conn4Move(mockMove)), Conn4Position.class.getMethod(
                "GetNextMoves"));
            // Get the play role to the same as the one originally provided to
            // the test case.
            itsPlayer = !itsPlayer;
            numOfMoves = moves.size();
            org.easymock.classextension.EasyMock.expect(testPos.GetNextMoves()).
                andReturn(moves.toArray(new Conn4Move[numOfMoves]));
            org.easymock.classextension.EasyMock.replay(testPos);
            expectedResult = new Conn4Position[numOfMoves];

            for (moveCount = 0; moveCount < numOfMoves; moveCount++)
                expectedResult[moveCount] =
                    new Conn4Position(testPos, itsPlayer, moves.get(moveCount));

            assertArrayEquals(expectedResult, testObj.possibleMoves(testPos,
                itsPlayer));
        }
        catch (Exception err)
        {

            fail(err.toString());

        }

    }
  }