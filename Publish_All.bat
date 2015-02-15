echo off
cls
echo ===========Build Start==========
cd /d %~dp0 
set filePath=Publish_All.msbuild
set isPause=true
if "%1"=="/q" (
set isPause=false
)

call MSBuild.exe %filePath% /t:Publish_All /maxcpucount:4 /p:BuildNumber=1.2.3.4


echo ===========Build Ended==========
if %isPause%==true (
echo ===========Build Paused==========
pause
)