SET VSTEST_HOST_DEBUG=1

rem dotnet C:\Projects\vstest\artifacts\Debug\netcoreapp1.0\vstest.console.dll C:\Projects\DevTeam\TestTool\dotNetCore\SimpleTests\bin\Debug\SimpleTests.dll /Framework:FrameworkCore10 --TestAdapterPath:C:\Projects\DevTeam\TestTool\DevTeam.TestAdapter\bin\Debug
dotnet C:\Projects\vstest\artifacts\Debug\netcoreapp1.0\vstest.console.dll C:\Projects\DevTeam\TestTool\dotNet\SimpleTests\bin\Debug\SimpleTests.dll /Framework:Framework45 --TestAdapterPath:C:\Projects\DevTeam\TestTool\DevTeam.TestAdapter\bin\Debug
