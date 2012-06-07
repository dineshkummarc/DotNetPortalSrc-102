@echo off
echo Makeing Install Package.....

rmdir /S /Q PortalSrc
mkdir PortalSrc

rem Content -------------------------------------------------------------
xcopy ..\*.aspx PortalSrc
xcopy ..\*.ascx PortalSrc
xcopy ..\*.cs PortalSrc
xcopy ..\*.resx PortalSrc
xcopy ..\Users.xsd PortalSrc
xcopy ..\Users.xsx PortalSrc
xcopy ..\Global.asax PortalSrc
xcopy ..\Frameset.htm PortalSrc
xcopy ..\README.TXT PortalSrc
xcopy ..\Portal.csproj PortalSrc
xcopy ..\Portal.sln PortalSrc

rem DLLs -------------------------------------------------------------
mkdir PortalSrc\bin
xcopy ..\bin\*.dll PortalSrc\bin

rem Images/Styles -------------------------------------------------------------
mkdir PortalSrc\Images
xcopy ..\Images\*.* PortalSrc\Images
xcopy ..\Portal.css PortalSrc
ren PortalSrc\Portal.css Portal.css.install

rem Portal Config -------------------------------------------------------------
xcopy Users.config.install PortalSrc
xcopy Portal.config.install PortalSrc

rem Portal Header/Footer -------------------------------------------------------------
del PortalSrc\PortalHeader.ascx
del PortalSrc\PortalFooter.ascx

xcopy /Y ..\PortalHeader.ascx PortalSrc
xcopy /Y ..\PortalFooter.ascx PortalSrc

ren PortalSrc\PortalHeader.ascx PortalHeader.ascx.install
ren PortalSrc\PortalFooter.ascx PortalFooter.ascx.install

rem Config -------------------------------------------------------------
xcopy ..\web.config PortalSrc
ren PortalSrc\web.config web.config.install

xcopy ..\web.config.1.1 PortalSrc
ren PortalSrc\web.config web.config.1.1.install

rem Portal API -------------------------------------------------------------
mkdir PortalSrc\Portal.API
xcopy /s ..\Portal.API\*.cs PortalSrc\Portal.API
xcopy /s ..\Portal.API\*.csproj PortalSrc\Portal.API

rem TreeWebControlPrj -------------------------------------------------------------
mkdir PortalSrc\TreeWebControlPrj
xcopy /s ..\TreeWebControlPrj\*.cs PortalSrc\TreeWebControlPrj
xcopy /s ..\TreeWebControlPrj\*.bmp PortalSrc\TreeWebControlPrj
xcopy /s ..\TreeWebControlPrj\*.csproj PortalSrc\TreeWebControlPrj

rem Installer -------------------------------------------------------------
mkdir PortalSrc\Installer
xcopy ..\Installer\*.* PortalSrc\Installer
del PortalSrc\Installer\.cvsignore
del PortalSrc\Installer\*.zip
del PortalSrc\Installer\*.swp

rem Module Content -------------------------------------------------------------
mkdir PortalSrc\Modules
xcopy /s ..\Modules\*.ascx PortalSrc\Modules
xcopy /s ..\Modules\*.cs PortalSrc\Modules
xcopy /s ..\Modules\*.resx PortalSrc\Modules
xcopy /s ..\Modules\*.csproj PortalSrc\Modules
xcopy /s ..\Modules\ModuleSettings*.config PortalSrc\Modules
xcopy /s ..\Modules\Module.config* PortalSrc\Modules
xcopy /s ..\Modules\Module.xsd* PortalSrc\Modules
xcopy /s ..\Modules\*.gif PortalSrc\Modules
xcopy /s ..\Modules\*.jpg PortalSrc\Modules
xcopy /s ..\Modules\*.aspx PortalSrc\Modules 

rem Forum ----------------------------------------------------
xcopy /s ..\Modules\RiversideInternetForums\createdatabase.cmd PortalSrc\Modules\RiversideInternetForums
xcopy /s ..\Modules\RiversideInternetForums\websolution.sql PortalSrc\Modules\RiversideInternetForums
xcopy /s ..\Modules\RiversideInternetForums\updatedatabase.cmd PortalSrc\Modules\RiversideInternetForums
xcopy /s ..\Modules\RiversideInternetForums\update1to2.sql PortalSrc\Modules\RiversideInternetForums

del /Q Portal\Modules\RiversideInternetForums\avatars

echo Finished...
pause
