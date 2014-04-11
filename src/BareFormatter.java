
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
 * file:         BareFormatter.java
 *
 * function:     easy log formatter
 *
 * description:  applies a no-change bare formatting to log records
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.util.logging.LogRecord;

/**
 * Bare logging formatter.
 * This formatter delivers log messages in their raw format AS IS, without any
 * added formatting.
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
public class BareFormatter extends java.util.logging.Formatter
  {
    @Override
    public String format(LogRecord record)
    {
        final String newLine = System.getProperty("line.separator");

        return formatMessage(record) + newLine;
    }
  }
