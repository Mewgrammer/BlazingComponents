@echo Off
set config=%1
if "%config%" == "" (
    set config=Release
)

set version=
if not "%BuildCounter%" == "" (
   set packversionsuffix=--version-suffix ci-%BuildCounter%
)

REM Restore
call dotnet restore
if not "%errorlevel%"=="0" goto failure

REM Build
call dotnet build --configuration %config%
if not "%errorlevel%"=="0" goto failure

REM Package
mkdir %cd%\artifacts
call dotnet pack --configuration %config% %packversionsuffix% --include-symbols --include-source --output %cd%\artifacts
if not "%errorlevel%"=="0" goto failure

:success
exit 0

:failure
pause