
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
 * file:         WonTest.java
 *
 * function:     unit test
 *
 * description:  tests the Won method of the Conn4Position class for different
 *               connect4 board positions
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.awt.Point;
import java.util.LinkedList;
import java.util.Random;
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
public class WonTest
  {
    private static Conn4Position CreateWinPos(Point startPoint, GroupDir posDir,
        boolean tilePlayer)
    {

        return CreateGroupPos(startPoint, Conn4Position.collection, posDir,
            tilePlayer);

    }

    private enum GroupDir
      {
        horSlope,
        verSlope,
        oneSlope,
        negOneSlope
      }

    private static Conn4Position CreateGroupPos(Point startPoint, byte grLen,
        GroupDir posDir, boolean tilePlayer)
    {
        byte colCount = (byte)startPoint.x,
            deltaX,
            deltaY,
            rowCount,
            tileCount = 0;
        int colHeight = startPoint.y,
            groupEnd = colCount;
        Conn4Move colMove;
        final boolean otherPlayer = !tilePlayer;
        Conn4Position testPos = new Conn4Position();

        // Determine how the group will extend across the x-axis.
        if (posDir == GroupDir.verSlope)
        {

            deltaX = 0;
            groupEnd++;

        }
        else
        {

            deltaX = 1;
            groupEnd += grLen;

        }

        // Determine how the columns of the group will vary in height.
        switch (posDir)
        {

            case oneSlope:

                deltaY = 1;
                break;

            case negOneSlope:

                deltaY = -1;
                break;

            default:

                deltaY = 0;
                break;

        }

        // Insert all the tiles before the group tiles.
        for (; colCount < groupEnd; colCount++, colHeight += deltaY)
        {

            colMove = new Conn4Move(colCount);

            for (rowCount = 0; rowCount < colHeight; rowCount++) testPos =
                    new Conn4Position(testPos, otherPlayer, colMove);

        }

        // Insert the group tiles.
        colCount = (byte)startPoint.x;

        while (grLen > 0)
        {

            testPos = new Conn4Position(testPos, tilePlayer, new Conn4Move(
                colCount));
            colCount += deltaX;
            grLen--;

        }

        return testPos;

    }
    private Conn4Position itsTestObj;
    private boolean itsPlayer;
    private boolean itsExpectedRes;

    public WonTest(final Conn4Position testPos, final boolean player,
        final boolean testResult)
    {

        itsTestObj = testPos;
        itsPlayer = player;
        itsExpectedRes = testResult;

    }

    @Parameterized.Parameters
    public static LinkedList<Object[]> InjectData()
    {
        byte count = 2;
        final GroupDir[] directions = GroupDir.values();
        final Conn4Move move = new Conn4Move((byte)randomGen.nextInt(
            Conn4Position.columns));
        final boolean player = randomGen.nextBoolean();
        final LinkedList<Object[]> testData = new LinkedList<Object[]>();
        Conn4Position testObj = new Conn4Position();

        // empty position
        testData.add(new Object[]{testObj, player, false});
        // single-tile position
        testObj = new Conn4Position(testObj, player, move);
        testData.add(new Object[]{testObj, player, false});

        for (GroupDir dir: directions) testData.addAll(MakeTestObjects(dir));

        //mixed tile position(interrupted collection)
        testObj = new Conn4Position(testObj, !player, move);

        for (; count < Conn4Position.collection; count++) testObj = new Conn4Position(testObj, player, move);

        testData.add(new Object[]{testObj, player, false});
        return testData;

    }

    private static LinkedList<Object[]> MakeTestObjects(final GroupDir objDir)
    {
        byte boundaryCorr = 0,// boundary correction factor
            deltaStartY = 0,
            groupHeight;// winning group height
        byte curLen = 2;// group length
        // winning group width
        final byte groupWidth =
            (objDir == GroupDir.verSlope) ? 1 : Conn4Position.collection;
        final int maxStartX = Conn4Position.columns - groupWidth;
        int maxStartY;
        final boolean refPlayer = randomGen.nextBoolean(),  otherPlayer =
            !refPlayer;
        final Point startPoint = new Point(0, 0);
        final LinkedList<Object[]> testobjects = new LinkedList<Object[]>();

        switch (objDir)
        {

            case horSlope:

                groupHeight = 1;
                break;

            case negOneSlope:

                groupHeight = -Conn4Position.collection;// negative height
                startPoint.y = 1;
                deltaStartY = 1;
                boundaryCorr = -1;
                break;

            default:

                groupHeight = Conn4Position.collection;
                break;

        }

        // Generate test data for incomplete groups(non-winning groups).
        for (; curLen < Conn4Position.collection; curLen++, startPoint.y +=
                deltaStartY) testobjects.add(new Object[]
                {
                    CreateGroupPos(startPoint, curLen, objDir, refPlayer),
                    refPlayer, false
                });

        // Generate test data for a winning group at the bottom left corner.
        testobjects.add(new Object[]
            {
                CreateWinPos(startPoint, objDir, refPlayer), refPlayer, true
            });
        // Set the starting point coordinates.
        startPoint.x = maxStartX / 2;
        maxStartY = Conn4Position.rows - groupHeight;
        startPoint.y = boundaryCorr + maxStartY / 2;
        testobjects.add(new Object[]
            {
                CreateWinPos(startPoint, objDir, otherPlayer), refPlayer, false
            });// Generate test data for a winning group in the middle.
        // Set the starting point coordinates.
        startPoint.x = maxStartX;
        startPoint.y = Math.min(maxStartY, Conn4Position.rows - 1);
        // Generate test data for a winning group at the top right corner.
        testobjects.add(new Object[]
            {
                CreateWinPos(startPoint, objDir, refPlayer), otherPlayer, false
            });
        return testobjects;

    }

    @BeforeClass
    public static void setUpClass() throws Exception
    {
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
    public void Test()
    {

        System.out.println("Won with test position:\n" + itsTestObj + "\nand " +
            Connect4.GetPlayerDesc(itsPlayer) + " maximizing player");
        assertEquals(itsExpectedRes, itsTestObj.Won(itsPlayer));

    }
    private static Random randomGen = new Random();
  }