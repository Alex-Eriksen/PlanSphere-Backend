@echo off
START cmd /C "call dotnet ef migrations remove --project lib\PlanSphere.Infrastructure\PlanSphere.Infrastructure.csproj --startup-project src\PlanSphere.SystemApi\PlanSphere.SystemApi.csproj --context PlanSphere.Infrastructure.Contexts.PlanSphereDatabaseContext --configuration Debug & PAUSE"