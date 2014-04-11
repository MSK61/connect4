
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
 * file:         Main.java
 *
 * function:     application entry point
 *
 * description:  runs a connect4 game between the computer and the user
 *
 * author:       Mohammed El-Afifi (ME)
 *
 * environment:  NetBeans IDE 6.5, Fedora release 10 (Cambridge)
 *
 * notes:        This is a private program.
 *
 ************************************************************/
import java.io.BufferedReader;
import java.io.IOException;
import java.util.logging.Logger;
import java.util.regex.Pattern;

/**
 * Application layer, contains the entry point.
 *
 * @author Mohammed El-Afifi <Mohammed_ElAfifi@yahoo.com>
 */
public class Main
  {
    /**
     * @param args the command line arguments
     */
    public static void main(String[] args)
    {
        final Logger globalLogger = Logger.getLogger(Logger.GLOBAL_LOGGER_NAME);
        final BufferedReader inStream = new BufferedReader(
            new java.io.InputStreamReader(System.in));
        final String question = "Do you want to start first? ";
        String res = null;
        final Pattern yesEx =
            Pattern.compile("y(es)?", Pattern.CASE_INSENSITIVE),  noEx =
            Pattern.compile("no?", Pattern.CASE_INSENSITIVE);
        boolean yesRes;

        // Get the user for preference for starting first.
        System.out.print(question);
        try
        {

            res = inStream.readLine();
            yesRes = yesEx.matcher(res).matches();

            while (!yesRes && !noEx.matcher(res).matches())
            {

                // No valid answer was provided; read over.
                System.out.print(question);
                res = inStream.readLine();
                yesRes = yesEx.matcher(res).matches();

            }

            // Let's have fun.
            (new Connect4()).playGame(new Conn4Position(), yesRes);
        }
        catch (IOException iOException)
        {

            globalLogger.severe(iOException.getMessage());
            globalLogger.severe("Exiting...");

        }
    }
  }
