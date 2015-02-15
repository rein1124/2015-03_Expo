echo off
cls
echo ===========Build Start==========
cd /d %~dp0 

set filePath=Pre-Build.msbuild
set isPause=true
if "%1"=="/q" (
set isPause=false
)

call msbuild %filePath% /p:Configuration=Debug /maxcpucount:1
REM call %msBuildDir%\msbuild %filePath% /t:BuildDebug


echo ===========Build Ended==========
if %isPause%==true (
echo ===========Build Paused==========
pause
)