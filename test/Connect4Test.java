
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
 * file:         Connect4Test.java
 *
 * function:     Connect4 class unit tests
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
import static org.easymock.classextension.EasyMock.createNiceMock;
import static org.easymock.classextension.EasyMock.expect;
import static org.easymock.classextension.EasyMock.replay;
import org.junit.AfterClass;
import org.junit.BeforeClass;
import org.junit.Test;
import static org.junit.Assert.*;

/**
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
public class Connect4Test
  {
    public Connect4Test()
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
     * Test of drawnPosition method, of class Connect4.
     */
    @Test
    public void testDrawnPosition()
    {
        System.out.println("drawnPosition");
        Position p = null;
        Connect4 instance = new Connect4();
        boolean expResult = true;
        boolean result = instance.drawnPosition(p);
        assertEquals(expResult, result);

    }

    @Test
    public void testDeadEnd()
    {
        final Connect4 testObj = new Connect4();
        final Conn4Position testPos = createNiceMock(Conn4Position.class);

        System.out.println("reachedMaxDepth with dead end");
        expect(testPos.Drawn()).andReturn(true);
        replay(testPos);
        assertTrue(testObj.reachedMaxDepth(testPos, testObj.itsDepth - 1));

    }

    @Test
    public void testNoMaxDepth()
    {
        final Connect4 testObj = new Connect4();
        final Conn4Position testPos = createNiceMock(Conn4Position.class);

        System.out.println("reachedMaxDepth with an expandable tree");
        replay(testPos);
        assertFalse(testObj.reachedMaxDepth(testPos, testObj.itsDepth - 1));

    }

    @Test
    public void testPlayerStop()
    {
        final Connect4 testObj = new Connect4();
        final Conn4Position testPos = createNiceMock(Conn4Position.class);

        System.out.println("reachedMaxDepth with player win");
        expect(testPos.Won(org.easymock.classextension.EasyMock.anyBoolean())).
            andReturn(true);
        replay(testPos);
        assertTrue(testObj.reachedMaxDepth(testPos, testObj.itsDepth - 1));

    }

    /**
     * Test of reachedMaxDepth method, of class Connect4.
     */
    @Test
    public void testReachedMaxDepth()
    {
        System.out.println("reachedMaxDepth with maximum static depth");
        Position p = createNiceMock(Conn4Position.class);
        replay(p);
        Connect4 instance = new Connect4();
        int depth = instance.itsDepth;
        boolean expResult = true;
        boolean result = instance.reachedMaxDepth(p, depth);
        assertEquals(expResult, result);

    }
  }
