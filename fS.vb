Imports fishScript
Imports System.Net
Imports System.IO

Module Program
    Sub Main(args As String())
        Dim banner As String
        banner = "
|\  __|\___     ==========================================================
| \/  _  o \    fishScript Compiler v0.1.2
| /\  \  ___|   For more info visit https://kettle3d.github.io/fishScript/
|/  \______/    =========================================================="

        If Not New WebClient().DownloadString("https://raw.githubusercontent.com/Kettle3D/fishScript/master/VERSION.TXT").StartsWith("0.1.2") Then
            Console.WriteLine("The version of fishScript you're using is no longer supported. Please upgrade to a newer version.")
        End If


        If args.Length = 0 Then
            Console.WriteLine("
Usage:
fS build <file>
    Compiles file producing program.exe. Compiles for win-x86 by default.
fS build <file> <target-runtime>
    Compiles file for the target runtime. Can be any compiler pack that you have installed.
fS <file>
    Runs file and prints out the exit code
fS
    Displays this message
")
            Return
        End If

        'If Not args.Contains("--nobanner") Then
        Console.WriteLine(banner)
        'args = {args(0)}
        'End If

        If args.Length = 3 AndAlso args(0) = "build" Then
            Dim corefs As Core
            corefs = New Core()
            corefs.Compile(New String() {args(1), args(2)})
        ElseIf args.Length = 2 AndAlso args(0) = "build" Then
            Dim corefs As Core
            corefs = New Core()
            corefs.Compile(New String() {args(1)})
        ElseIf args.Length = 1 Then
            Dim corefs As Core
            Dim exitcode As Byte
            corefs = New Core()
            exitcode = corefs.Execute(args(0))
            Console.WriteLine("Process ended with exit code " + exitcode.ToString() + ".")
            If exitcode = 90 Then
                Return
            End If
        Else
            Console.WriteLine(args.Length)
            For Each arg In args
                Console.WriteLine(arg)
            Next
        End If
            Console.WriteLine("
Press any key to close this window...")
        Console.ReadKey()
    End Sub
End Module
