rem SET VSTEST_HOST_DEBUG=1

dotnet C:\Projects\a\Microsoft.TestPlatform.CLI.15.0.0\contentFiles\any\any\vstest.console.dll C:\Projects\DevTeam\TestTool\dotNetCore\SimpleTests\bin\Debug\SimpleTests.dll /Framework:FrameworkCore10 --TestAdapterPath:C:\Projects\DevTeam\TestTool\DevTeam.TestAdapter\bin\Debug --logger:DevTeam
rem --Diag:a.txt
rem dotnet C:\Projects\a\Microsoft.TestPlatform.CLI.15.0.0\contentFiles\any\any\vstest.console.dll --TestAdapterPath:C:\Projects\DevTeam\TestTool\DevTeam.TestAdapter\bin\Debug --logger:TeamCity --help
