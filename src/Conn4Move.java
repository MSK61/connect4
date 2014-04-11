
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
 * file:         Conn4Move.java
 *
 * function:     connect4 board transition
 *
 * description:  depicts a transition between two board layouts
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
/**
 * Transition between two <code>{@link Conn4Position}</code> instances, usually
 * used to create offspring <code>Conn4Position</code>.
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
public class Conn4Move extends Move
  {
    Conn4Move(byte targetMove)
    {

        itsColumn = targetMove;

    }

    @Override
    public boolean equals(Object obj)
    {
        return (getClass() == obj.getClass() && itsColumn == ((Conn4Move)obj).
            getItsColumn());
    }

    @Override
    public int hashCode()
    {
        return itsColumn;
    }
    private byte itsColumn;

    /**
     * Returns the column of the transition tile.
     * To apply this transition, a tile should be inserted at the column
     * specified by the return value
     *
     * @return column of the tile to be inserted
     * @see    Conn4Position#Conn4Position(Conn4Position, boolean, Conn4Move)
     */
    public byte getItsColumn()
    {
        return itsColumn;
    }
  }
