#!/bin/bash
set -e

# Czekaj na SQL Server (dodatkowe zabezpieczenie)
echo "Oczekiwanie na SQL Server..."
until /opt/mssql-tools/bin/sqlcmd -S sqlserver,1433 -U sa -P 'TwojaSilnaHaslo123!@#' -Q "SELECT 1"; do
  echo "SQL Server nie gotowy, czekam..."
  sleep 2
done

# Utwórz u¿ytkownika i bazê danych (jeœli nie istnieje)
echo "Tworzenie u¿ytkownika i bazy danych..."
/opt/mssql-tools/bin/sqlcmd -S sqlserver,1433 -U sa -P 'TwojaSilnaHaslo123!@#' -Q "
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'DBGeneratorDB')
BEGIN
    CREATE DATABASE [DBGeneratorDB];
END

USE [DBGeneratorDB];
IF NOT EXISTS (SELECT name FROM sys.database_principals WHERE name = 'dbuser')
BEGIN
    CREATE LOGIN [dbuser] WITH PASSWORD = 'DbUserPass123!@#';
    CREATE USER [dbuser] FOR LOGIN [dbuser];
    ALTER ROLE db_owner ADD MEMBER [dbuser];
END
"

# Wykonaj migracje EF Core
echo "Wykonywanie migracji EF Core..."
dotnet ef database update --connection "Server=sqlserver,1433;Database=DBGeneratorDB;User Id=dbuser;Password=DbUserPass123!;TrustServerCertificate=true"

# Uruchom aplikacjê
echo "Uruchamianie aplikacji..."
exec dotnet DBGenerator.dll
