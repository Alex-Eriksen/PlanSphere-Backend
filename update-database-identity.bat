@echo off
set /p updateMigrationName= "Enter migration name(empty for latest): "
START cmd /C "call dotnet ef database update %updateMigrationName% --project lib\PlanSphere.Infrastructure\PlanSphere.Infrastructure.csproj --startup-project src\PlanSphere.SystemApi\PlanSphere.SystemApi.csproj --context PlanSphere.Infrastructure.Contexts.IdentityDatabaseContext --connection "Server=127.0.0.1;User ID=sa;Password=Passw0rd>;Database=identity;TrustServerCertificate=True;" & PAUSE"
