echo off
cls
echo ===========Build Start==========
cd /d %~dp0 
set filePath=Build.targets
set isPause=true
if "%1"=="/q" (
set isPause=false
)

REM call %msBuildDir%\msbuild %filePath% /t:CopyLib
call msbuild %filePath% /p:Configuration=Release /t:Build /maxcpucount:4


echo ===========Build Ended==========
if %isPause%==true (
echo ===========Build Paused==========
pause
)