
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
 * file:         DrawnTest.java
 *
 * function:     unit test
 *
 * description:  tests the Drawn method of the Conn4Position class for different
 *               connect4 board positions
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import org.junit.AfterClass;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import org.junit.runners.Parameterized;
import static org.junit.Assert.*;

/**
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
@org.junit.runner.RunWith(Parameterized.class)
public class DrawnTest
  {
    private byte itsColHeight;
    private boolean itsExpectedRes;
    private Conn4Position itsTestObj;

    public DrawnTest(final byte colHeight, final boolean testResult)
    {

        itsColHeight = colHeight;
        itsExpectedRes = testResult;

    }

    @Parameterized.Parameters
    public static java.util.List<Object[]> InjectData()
    {

        return java.util.Arrays.asList(new Object[]
            {
                (byte)0, false
            }, new Object[]
            {
                (byte)(Conn4Position.rows / 2), false
            }, new Object[]
            {
                Conn4Position.rows, true
            });

    }

    @BeforeClass
    public static void setUpClass() throws Exception
    {
    }

    @AfterClass
    public static void tearDownClass() throws Exception
    {
    }

    @Before
    public void setUp()
    {

        itsTestObj = new Conn4Position();

    }

    // TODO add test methods here.
    // The methods must be annotated with annotation @Test. For example:
    //
    // @Test
    // public void hello() {}
    @Test
    public void Test()
    {
        byte columnCount = 0,
            rowCount;
        // The first player doesn't really matter here.
        boolean player = (new java.util.Random()).nextBoolean();

        System.out.println("Drawn with " + itsColHeight + " rows and " +
            Connect4.GetPlayerDesc(player) + " beginner");

        // Compose the board layout.
        for (; columnCount < Conn4Position.columns; columnCount++)
            for (rowCount = 0; rowCount < itsColHeight; rowCount++, player =
                    !player) itsTestObj = new Conn4Position(itsTestObj, player,
                    new Conn4Move(columnCount));

        assertEquals(itsExpectedRes, itsTestObj.Drawn());

    }
  }
