set src=%~dp0..
set src1=%~1
set depl=%src%\..\Build\App

xcopy /Y /D %src%\bin\TestTestClearingCustomerApp.dll		%depl%\CustomerApp\bin\
xcopy /Y /D %src%\bin\TestTestClearingCommonLogic.dll		%depl%\CustomerApp\bin\
xcopy /Y /D %src%\TestTestClearingDataEntry.ascx			%depl%\CustomerApp\Clearing\
xcopy /Y /D %src%\CustomHtmlClearingPluginWsApi.asmx		%depl%\CustomerApp\Clearing\

7z a -tzip "%src1%..\Build\App.zip" "%src1%..\Build\App"

echo unlock hangged file
unlocker /S %src%\..\Build\App.zip

rem pause