nuget restore ..\Samples.sln
msbuild SampleTests.csproj
"C:\Program Files (x86)\Microsoft Visual Studio\2017\Professional\Common7\IDE\CommonExtensions\Microsoft\TestWindow\vstest.console.exe" bin\Debug\net462\SampleTests.dll