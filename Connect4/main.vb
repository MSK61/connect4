'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' program:      Connect4 SetupReg utility

' file:         main.vb

' function:     Main

' description:  runs a connect4 game between the computer and the
'               user

' author:       Mohammed Safwat (MS)

' environment:  visual studio.net enterprise architect 2003,
'               windows xp professional

' notes:        This is a translated faculy program.

' revisions:    2.5  11/1/2006 (MS) first release

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
Imports Connect4Library
Module main
    Sub Main()

        Options.EnsureValidRegistryData( _
            Parameters.strApplicationRegistryKey & _
            RegistryStream.keySeparator & _
            Parameters.strOptionsRegistryKey)
        Application.Run(New MainForm)

    End Sub
End Module
