echo off
cls
echo ===========Build Start==========
cd /d %~dp0 
set filePath=Publish.msbuild
set isPause=true
if "%1"=="/q" (
set isPause=false
)

call msbuild %filePath% /p:Configuration=Release /t:Publish /maxcpucount:4


echo ===========Build Ended==========
if %isPause%==true (
echo ===========Build Paused==========
pause
)