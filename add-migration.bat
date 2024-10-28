@echo off
set /p mName= "Enter migration name: "
START cmd /C "call dotnet ef migrations add --project lib\PlanSphere.Infrastructure\PlanSphere.Infrastructure.csproj --startup-project src\PlanSphere.SystemApi\PlanSphere.SystemApi.csproj --context PlanSphere.Infrastructure.Contexts.PlanSphereDatabaseContext --configuration Debug %mName% --output-dir Migrations & PAUSE"