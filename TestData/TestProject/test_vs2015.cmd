nuget restore ..\Samples.sln
msbuild TestProject.csproj
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" bin\Debug\net45\TestProject.dll /TestAdapterPath:.