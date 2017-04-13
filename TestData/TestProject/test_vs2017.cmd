nuget restore ..\Samples.sln
msbuild TestProject.csproj
set VSTEST_HOST_DEBUG=1
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" bin\Debug\net45\TestProject.dll