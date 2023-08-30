'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' program:      connect4 SetupReg utility

' file:         SetupReg.vb

' function:     Main

' description:  writes the default options of the connect4 game
'               to the registry

' author:       Mohammed Safwat (MS)

' environment:  visual studio.net enterprise architect 2003,
'               windows xp professional

' notes:        This is a translated faculy program.

' revisions:    2.5  8/1/2006 (MS) first release

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Connect4Library
Imports Microsoft.Win32
Imports System.Drawing
Module SetupReg

    Sub Main()
        Dim backgroundKey As RegistryKey ' registry key for
        ' information about background files
        Dim backgrounds As New ArrayList, _
            bytCount As Byte = 0, _
            currentBackground As Background
        Const strExtension As String = ".bmp", _
            strGamePrefix As String = "c4"
        Dim strRegistryValue As String

        ' Fill the vector of backgrounds with their data. Note
        ' that all file names are relative to the folder of
        ' backgrounds.
        backgrounds.Add( _
            New Background("classic", New Point(5, 5)))
        backgrounds.Add(New Background("wood", New Point(7, 5)))
        Try

            Registry.CurrentUser.DeleteSubKeyTree( _
                Parameters.strApplicationRegistryKey) ' just for
            ' safety

        Catch ex As ArgumentException
        End Try
        backgroundKey = Registry.CurrentUser.CreateSubKey( _
            Parameters.strApplicationRegistryKey & _
            RegistryStream.keySeparator & _
            Parameters.strBackgroundRegistryKey) ' Register the
        ' backgrounds in the registry.

        Do While bytCount < backgrounds.Count

            currentBackground = backgrounds(bytCount)
            strRegistryValue = currentBackground.strFileName
            currentBackground.strFileName = strGamePrefix & _
                currentBackground.strFileName + strExtension

            If System.IO.File.Exists( _
                Parameters.strBackgroundFolder & _
                RegistryStream.keySeparator & _
                currentBackground.strFileName) Then ' If the
                ' background file really exists, register it.

                RegistryStream.Write(currentBackground, _
                    backgroundKey, strRegistryValue)
                bytCount += 1

            Else ' Alert the user that a background file is
                ' missing.

                backgrounds.RemoveAt(bytCount)
                Console.WriteLine("missing background file " & _
                    currentBackground.strFileName & _
                    ". failed to register")

            End If

        Loop

        backgroundKey.Close() ' Close the registry key of the
        ' backgrounds.
        Parameters.defaultOptions.Save( _
            Parameters.strApplicationRegistryKey & _
            RegistryStream.keySeparator & _
            Parameters.strOptionsRegistryKey) ' Fill the options
        ' with default values.

    End Sub ' end of function Main

End Module
