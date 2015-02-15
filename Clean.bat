echo off
echo ===========Clean Start==========
cd /d %~dp0 
@rem set currentDir=%~dp0
set filePath=Clean.msbuild
set isPause=true
if "%1"=="/q" (
set isPause=false
)



call msbuild %filePath% /t:Clean /maxcpucount:1


echo ===========Clean Ended==========
if %isPause%==true (
echo ===========Clean Paused==========
pause
)
