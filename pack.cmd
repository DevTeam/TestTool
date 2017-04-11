set BuildNumber=1
msbuild build.proj /t:Clear;GetNuGet;Build;Test;CreatePackages /p:Configuration=Release