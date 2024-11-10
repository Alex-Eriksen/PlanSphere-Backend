@echo off
set /p mName= "Enter migration name(empty for latest): "
set connString= "Server=127.0.0.1;User ID=sa;Password=Passw0rd>;Database=plansphere;TrustServerCertificate=True;Encrypt=False;"
START cmd /C "call dotnet ef database update %mName% --project lib\PlanSphere.Infrastructure\PlanSphere.Infrastructure.csproj --startup-project src\PlanSphere.SystemApi\PlanSphere.SystemApi.csproj --context PlanSphere.Infrastructure.Contexts.PlanSphereDatabaseContext --connection %connString% & PAUSE"