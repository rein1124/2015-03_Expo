echo off
rem msbuild Zip.msbuild /p:Configuration=Debug /t:ZipBin
msbuild Zip.msbuild /p:Configuration=Debug /t:ZipBinDebug
pause