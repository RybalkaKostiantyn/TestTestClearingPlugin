set src=%~dp0..
set depl=%src%\..\Build\App\

xcopy /Y /D %src%\bin\Debug\TestTestClearingCommonLogic.dll		%depl%\AdminApp\bin\
xcopy /Y /D %src%\bin\Debug\TestTestClearingCommonLogic.dll		%depl%\CustomerApp\bin\
xcopy /Y /D %src%\bin\Debug\TestTestClearingCommonLogic.dll		%depl%\Common\uStore.CommonControls\bin\

rem pause