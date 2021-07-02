set src=%~dp0..
set depl=%src%\..\Build\App\

xcopy /Y /D %src%\bin\TestTestClearingAdminApp.dll					%depl%\AdminApp\bin\
xcopy /Y /D %src%\TestTestClearingConfig.ascx						%depl%\AdminApp\ClearingModels\ClearingConfigControls\
xcopy /Y /D %src%\TestTestClearingDataEntryConfig.ascx				%depl%\AdminApp\ClearingModels\UserDataCollection\

rem pause