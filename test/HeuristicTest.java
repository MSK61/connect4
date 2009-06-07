
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
 * file:         HeuristicTest.java
 *
 * function:     unit test
 *
 * description:  tests the Heuristic method of the Conn4Position class for
 *               different players
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
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
public class HeuristicTest
  {
    private static boolean tilePlayer;
    private boolean itsMaxPlayer;

    public HeuristicTest(final boolean tilePlayer)
    {

        itsMaxPlayer = tilePlayer;

    }

    @Parameterized.Parameters
    public static java.util.List<Object[]> InjectData()
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

        tilePlayer = (new java.util.Random()).nextBoolean();

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
        byte colCount = 0;
        Conn4Position testObj = new Conn4Position();

        System.out.println("Heuristic with " +
            Connect4.GetPlayerDesc(tilePlayer) + " tiles and " + Connect4.
            GetPlayerDesc(itsMaxPlayer) + " maximizing player");

        // Compose the board layout.
        for (; colCount < Conn4Position.collection; colCount++) testObj =
                new Conn4Position(testObj, tilePlayer, new Conn4Move(colCount));

        assertEquals(
            (tilePlayer == itsMaxPlayer) ? Short.MAX_VALUE : -Short.MAX_VALUE,
            testObj.Heuristic(itsMaxPlayer));

    }
  }