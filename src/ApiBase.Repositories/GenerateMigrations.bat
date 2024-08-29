@ECHO OFF

ECHO ----------------------------------------------------
ECHO Script para criacao de migrations
ECHO -----
ECHO Pre-requisitos:
ECHO   1. Runtime do DotNet Core 6
ECHO   2. Ferramentas de linha de comando do EF
ECHO      - dotnet tool install --global dotnet-ef
ECHO      (Para atualizar:)
ECHO      - dotnet tool update --global dotnet-ef
ECHO ----------------------------------------------------

SET /p name="Informe o nome da migration: "

IF [%name%] == [] GOTO end

SET STARTUP_PROJECT=../ApiBase.UI

ECHO ---
ECHO SqlServer:
ECHO ---
dotnet ef migrations add %name% --startup-project %STARTUP_PROJECT% --context ApiBase.Repositories.DbContexts.SqlServerDbContext --output-dir Migrations/SqlServer
IF %ERRORLEVEL% NEQ 0 GOTO error

ECHO ---
ECHO MySql:
ECHO ---
dotnet ef migrations add %name% --startup-project %STARTUP_PROJECT% --context ApiBase.Repositories.DbContexts.MySqlDbContext --output-dir Migrations/MySql
IF %ERRORLEVEL% NEQ 0 GOTO error

ECHO ---
ECHO Script concluido com sucesso!
ECHO ---

GOTO end

:error
ECHO ---
ECHO Script concluido com erro
ECHO ---
pause

:end