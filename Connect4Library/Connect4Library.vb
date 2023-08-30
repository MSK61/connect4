'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

' program:      connect4 library

' file:         Connect4Library.vb

' function:     classes of the Connect4Library class library

' description:  defines general parameters and data types for use
'               with the connect4 game

' author:       Mohammed Safwat (MS)

' environment:  visual studio.net enterprise architect 2003,
'               windows xp professional

' notes:        This is a translated faculy program.

' revisions:    2.5  7/1/2006 (MS) starting construction
'               2.51 8/1/2006 (MS) first release

'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' stores the options of the connect4 game
Imports Microsoft.Win32
Imports System
Imports System.Drawing
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
<Serializable()> Public Structure Background
    Public Sub New( _
        ByVal strFile As String, ByVal margin As Point)

        clearance = margin
        strFileName = strFile

    End Sub ' end of constructor
    Public clearance As Point
    Public strFileName As String
End Structure ' end of structure Background
Public Enum player
    minimizing
    maximizing
End Enum
' stores the options of the connect4 game
Public Class Options
    ' overloaded constructors
    Public Sub New()
    End Sub ' end of constructor

    Public Sub New(ByVal iBackgroundIndex As Integer, _
        ByVal bytLevel As Byte, ByVal a_tileColors() As Color)

        iItsBackground = iBackgroundIndex
        bytItsLevel = bytLevel
        a_itsTileColors = a_tileColors

    End Sub ' end of constructor

    ' This method ensures that the specified registry key has
    ' valid options.
    Public Shared Sub EnsureValidRegistryData( _
        ByVal strRegistryKey As String)
        Dim a_strRegistryValues() As String ' registry values
        ' under a registry key
        Dim bSuccess As Boolean = False, _
            bytTempValue As Byte, _
            objTemp As Object, _
            temp As RegistryKey = _
            Registry.CurrentUser.CreateSubKey(strRegistryKey)

        ' Check for the existence of registry keys and values.
        ' Check the registry key and values of the options.
        a_strRegistryValues = temp.GetValueNames()

        If Array.IndexOf(a_strRegistryValues, _
            strBackgroundRegistryValue) = -1 Then

            temp.SetValue(strBackgroundRegistryValue, 0)

        End If

        objTemp = temp.GetValue(strLevelRegistryValue)

        If Not (objTemp Is Nothing) Then

            bytTempValue = Byte.Parse(objTemp.ToString())

            If bytTempValue > 0 And bytTempValue <= _
                bytMaxDifficulty Then bSuccess = True

        End If

        If Not (bSuccess) Then

            temp.SetValue(strLevelRegistryValue, _
                Parameters.defaultOptions.bytItsLevel)

        End If

        ' Check the registry key and values of the color options.
        temp = temp.CreateSubKey(strTileColorRegistryKey)
        a_strRegistryValues = temp.GetValueNames()

        If Array.IndexOf( _
            a_strRegistryValues, strUserRegistryValue) = -1 Then

            temp.SetValue( _
                        strUserRegistryValue, Color.Red)

        End If

        If Array.IndexOf(a_strRegistryValues, _
            strComputerRegistryValue) = -1 Then

            temp.SetValue(strComputerRegistryValue, Color.Green)

        End If

        temp.Close() ' Close the registry key of the color
        ' options.

    End Sub ' end of method EnsureValidRegistryData

    ' This method loads the options from the specified registry
    ' key.
    Public Sub Load(ByVal strRegistryKey As String)
        Dim temp As RegistryKey = _
            Registry.CurrentUser.CreateSubKey(strRegistryKey)

        iItsBackground = _
            temp.GetValue(strBackgroundRegistryValue, _
            Parameters.defaultOptions.iItsBackground) ' Read the
        ' preferred background option from the registry.
        bytItsLevel = temp.GetValue(strLevelRegistryValue, _
            Parameters.defaultOptions.bytItsLevel) ' Read the
        ' difficulty level option from the registry.

        If bytItsLevel = 0 Or bytItsLevel > bytMaxDifficulty Then

            bytItsLevel = Parameters.defaultOptions.bytItsLevel

        End If

        ' Read the colors options from the registry.
        temp = temp.CreateSubKey(strTileColorRegistryKey)

        a_itsTileColors(player.minimizing) = _
            Color.FromArgb(temp.GetValue(strUserRegistryValue, _
            Parameters.defaultOptions.a_itsTileColors( _
            player.minimizing).ToArgb()))
        a_itsTileColors(player.maximizing) = Color.FromArgb( _
            temp.GetValue(strComputerRegistryValue, _
            Parameters.defaultOptions.a_itsTileColors( _
            player.maximizing).ToArgb()))
        temp.Close() ' Close the registry key of the color
        ' options.

    End Sub ' end of method Load

    ' This method saves the options to the specified registry
    ' key.
    Public Sub Save(ByVal strRegistryKey As String)
        Dim currentKey As RegistryKey = Registry.CurrentUser.CreateSubKey(strRegistryKey)

        ' Register the preferred background.
        currentKey.SetValue( _
            strBackgroundRegistryValue, iItsBackground)
        ' Register the preferred difficulty level.
        currentKey.SetValue(strLevelRegistryValue, bytItsLevel)
        ' Register the color options.
        currentKey = _
            currentKey.CreateSubKey(strTileColorRegistryKey)
        currentKey.SetValue(strUserRegistryValue, _
            a_itsTileColors(player.minimizing).ToArgb())
        currentKey.SetValue(strComputerRegistryValue, _
            a_itsTileColors(player.maximizing).ToArgb())
        currentKey.Close() ' Close the registry key of tile
        ' colors.

    End Sub ' end of method Save
    ' the preferred tile colors of the players
    Public a_itsTileColors() As Color = New Color( _
        [Enum].GetValues(GetType(player)).Length - 1) {}
    Public bytItsLevel As Byte ' the preferred difficulty level
    Public Const bytMaxDifficulty As Byte = 6 ' maximum
    ' difficulty level for the connect4 game
    Public iItsBackground As Integer ' the preferred background
    Private Const strBackgroundRegistryValue As String = _
        "Background" ' registry value for the preferred
    ' background
    ' registry value for the color of computer tiles
    Private Const strComputerRegistryValue As String = "Computer"
    ' registry value for the game difficulty level
    Private Const strLevelRegistryValue As String = "Level"
    Private Const strTileColorRegistryKey As String = _
        "Tile Colors" ' registrty key for preferred tile colors
    ' registry value for user color preferences
    Private Const strUserRegistryValue As String = "User"
End Class ' end of class Options
' holds the parameters characterizing the connect4 game
Public NotInheritable Class Parameters
    ' This method returns the opponent player of the specified
    ' player.
    Public Shared Function GetOpponent( _
        ByVal operand As player) As player

        GetOpponent = IIf(operand = player.maximizing, _
            player.minimizing, player.maximizing)

    End Function ' end of method GetOpponent
    Public Const bytCollection As Byte = 4 ' number of adjacent
    ' tiles that mean winning the game
    Public Const bytColumns As Byte = 7 ' number of columns of
    ' tiles
    Public Const bytRows As Byte = 6 ' number of rows of tiles
    Public Shared ReadOnly defaultOptions As New Options(0, 1, _
        New Color() {Color.Red, Color.Green}) ' default options
    ' for the connect4 game
    Public Const strApplicationRegistryKey As String = _
        "Software\Mohammed Safwat\vb.net\Connect4" ' the root
    ' registry key for the connect4 game
    ' the folder containing the background files
    Public Const strBackgroundFolder As String = "background"
    Public Const strBackgroundRegistryKey As String = _
        "Background" ' registry key for the available backgrounds
    ' registry key for the connect4 game options
    Public Const strOptionsRegistryKey As String = "Options", _
        strSolution As String = _
        "Running SetupReg may solve the problem." ' solution
    ' message for registry errors
End Class ' end of class Parameters
' performs common registry operations with objects
Public NotInheritable Class RegistryStream
    ' This method retrieves an object from the registry at the
    ' specified key and value.
    Public Shared Function Read(ByVal key As RegistryKey, _
        ByVal strRegistryValue As String) As Object
        Dim binaryConverter As New BinaryFormatter
        Dim conversionStream As New MemoryStream( _
            CType(key.GetValue(strRegistryValue), Byte()))
        Dim objRetrieved As Object = _
            binaryConverter.Deserialize(conversionStream)

        conversionStream.Close() ' Close the temporary stream.
        Read = objRetrieved

    End Function ' end of method Read

    ' This method saves the specified object to the registry at
    ' the specified key and value.
    Public Shared Sub Write( _
        ByVal objData As Object, ByVal key As RegistryKey, _
        ByVal strRegistryValue As String)
        Dim binaryConverter As New BinaryFormatter ' to convert
        ' the specified object into binary data before writing it
        ' to the registry
        Dim conversionStream As New MemoryStream ' temporary
        ' stream for converting objetcs into binary data

        ' Convert the object to binary data.
        binaryConverter.Serialize(conversionStream, objData)
        key.SetValue( _
            strRegistryValue, conversionStream.GetBuffer())
        conversionStream.Close() ' Close the temporary stream.

    End Sub ' end of method Write
    Public Const keySeparator As Char = "\"c ' separator used to
    ' separate different levels in a fully qualified registry key
End Class ' end of class RegistryStream