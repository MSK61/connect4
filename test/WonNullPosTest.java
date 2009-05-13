
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
 * file:         WonNullPosTest.java
 *
 * function:     robustness test
 *
 * description:  tests the wonPosition method vs null positions
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
public class WonNullPosTest
  {
    private static Connect4 testObj;
    private boolean itsPlayer;
    private boolean itsExpectedRes;

    public WonNullPosTest(final boolean player, final boolean testResult)
    {

        itsPlayer = player;
        itsExpectedRes = testResult;

    }

    @Parameterized.Parameters
    public static java.util.Collection<Object[]> InjectData()
    {

        return java.util.Arrays.asList(new Object[]
            {
                Connect4.HUMAN, true
            }, new Object[]
            {
                Connect4.PROGRAM, false
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

        System.out.println("wonPosition with " + Connect4Test.GetPlayerDesc(
            itsPlayer) + " player");
        assertEquals(testObj.wonPosition(null, itsPlayer), itsExpectedRes);

    }
  }