echo off
rem call Zip_src
rem call Zip_bin_Debug
rem call Zip_bin_Release
msbuild Zip.msbuild /t:ZipAll
pause