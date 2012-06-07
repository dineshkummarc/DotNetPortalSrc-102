@echo off
echo Makeing Install Package.....

rmdir /S /Q Portal
mkdir Portal

rem Content -------------------------------------------------------------
xcopy ..\*.aspx Portal
xcopy ..\*.ascx Portal
xcopy ..\Users.xsd Portal
xcopy ..\Global.asax Portal
xcopy ..\Frameset.htm Portal
xcopy ..\README.TXT Portal

rem DLLs -------------------------------------------------------------
mkdir Portal\bin
xcopy ..\bin\*.dll Portal\bin

rem Images/Styles -------------------------------------------------------------
mkdir Portal\Images
xcopy ..\Images\*.* Portal\Images
xcopy ..\Portal.css Portal
ren Portal\Portal.css Portal.css.install

rem Portal Config -------------------------------------------------------------
xcopy Users.config.install Portal
xcopy Portal.config.install Portal

rem Portal Header/Footer -------------------------------------------------------------
del Portal\PortalHeader.ascx
del Portal\PortalFooter.ascx

xcopy /Y ..\PortalHeader.ascx Portal
xcopy /Y ..\PortalFooter.ascx Portal

ren Portal\PortalHeader.ascx PortalHeader.ascx.install
ren Portal\PortalFooter.ascx PortalFooter.ascx.install

rem Config -------------------------------------------------------------
xcopy ..\web.config Portal
ren Portal\web.config web.config.install

xcopy ..\web.config.1.1 Portal
ren Portal\web.config web.config.1.1.install

rem Module Content -------------------------------------------------------------
mkdir Portal\Modules
xcopy /s ..\Modules\*.ascx Portal\Modules
xcopy /s ..\Modules\ModuleSettings*.config Portal\Modules
xcopy /s ..\Modules\Module.config* Portal\Modules
xcopy /s ..\Modules\Module.xsd* Portal\Modules
xcopy /s ..\Modules\*.gif Portal\Modules
xcopy /s ..\Modules\*.jpg Portal\Modules
xcopy /s ..\Modules\*.aspx Portal\Modules 

rem Forum ----------------------------------------------------
xcopy /s ..\Modules\RiversideInternetForums\createdatabase.cmd Portal\Modules\RiversideInternetForums
xcopy /s ..\Modules\RiversideInternetForums\websolution.sql Portal\Modules\RiversideInternetForums
xcopy /s ..\Modules\RiversideInternetForums\updatedatabase.cmd Portal\Modules\RiversideInternetForums
xcopy /s ..\Modules\RiversideInternetForums\update1to2.sql Portal\Modules\RiversideInternetForums
del /Q Portal\Modules\RiversideInternetForums\avatars

echo Finished...
pause
