# PlanSphere-Backend
For at kunne kører projektet er der en forudsætning for at DOTNET CLI er installeret på din maskine.

For at kunne kører backend projektet skal du have et par ting installeret:
- Aspire
- Docker


## Installér og opsætning af Docker
Gå til https://www.docker.com/get-started/ og download den version du har brug for.
Efter installationen brug så denne kommando i en terminal:
```
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=Passw0rd>" -p 1433:1433 --name plansphere --hostname plansphere -d mcr.microsoft.com/mssql/server:2022-latest
```

Åben derefter din fortrukne SQL viewer og naviger til database serveren med disse login information:
Username: sa
Password: Passw0rd>

Opret 2 database tabeller ved navn:
- plansphere
- identity

## Installér Aspire
For at kunne bruge aspire skal du først have et aspire workload installeret, det kan du gøre ved at køre følgende kommando i en terminal:
```
dotnet workload install aspire
```

## Opret database tabeller
Navigér til backend projektet og kør følgende kommandoer:
```
dotnet ef database update --project lib\PlanSphere.Infrastructure\PlanSphere.Infrastructure.csproj --startup-project src\PlanSphere.SystemApi\PlanSphere.SystemApi.csproj --context PlanSphere.Infrastructure.Contexts.PlanSphereDatabaseContext --connection "Server=127.0.0.1;User ID=sa;Password=Passw0rd>;Database=plansphere;TrustServerCertificate=True;Encrypt=False;"
```
og
```
dotnet ef database update --project lib\PlanSphere.Infrastructure\PlanSphere.Infrastructure.csproj --startup-project src\PlanSphere.SystemApi\PlanSphere.SystemApi.csproj --context PlanSphere.Infrastructure.Contexts.IdentityDatabaseContext --connection "Server=127.0.0.1;User ID=sa;Password=Passw0rd>;Database=plansphere;TrustServerCertificate=True;Encrypt=False;"
```

## Opsætning
Projektet har ikke launch profiles med fordi det kan være forskelligt var maskine til maskine derfor har vi nogle standarde nogle her som du kan bruge.
Launch settings skal ligges ind i "Properties" under projektet også i en fil som hedder nøjagtigt "launchSettings.json"
Her er de launch settings du kunne få brug for:

### AppHost Launch Settings
```json
{  
  "$schema": "https://json.schemastore.org/launchsettings.json",  
  "profiles": {  
    "https": {  
      "commandName": "Project",  
      "dotnetRunMessages": true,  
      "launchBrowser": true,  
      "applicationUrl": "https://localhost:17077;http://localhost:15234",  
      "environmentVariables": {  
        "ASPNETCORE_ENVIRONMENT": "Development",  
        "DOTNET_ENVIRONMENT": "Development",  
        "DOTNET_DASHBOARD_OTLP_ENDPOINT_URL": "https://localhost:21273",  
        "DOTNET_RESOURCE_SERVICE_ENDPOINT_URL": "https://localhost:22241"  
      }  
    },  
    "http": {  
      "commandName": "Project",  
      "dotnetRunMessages": true,  
      "launchBrowser": true,  
      "applicationUrl": "http://localhost:15234",  
      "environmentVariables": {  
        "ASPNETCORE_ENVIRONMENT": "Development",  
        "DOTNET_ENVIRONMENT": "Development",  
        "DOTNET_DASHBOARD_OTLP_ENDPOINT_URL": "http://localhost:19259",  
        "DOTNET_RESOURCE_SERVICE_ENDPOINT_URL": "http://localhost:20218"  
      }  
    }  
  }  
}
```

### System Api Launch Settings
```json
{  
  "profiles": {  
    "PlanSphere.Api.SystemApi": {  
      "commandName": "Project",  
      "dotnetRunMessages": true,  
      "launchBrowser": true,  
      "launchUrl": "swagger",  
      "applicationUrl": "https://localhost:61348;http://localhost:61349",  
      "environmentVariables": {  
          "AzureStorageAccountConfiguration__ConnectionString": "DefaultEndpointsProtocol=https;AccountName=plansphereblob;AccountKey=MPVY8pJz31/vJTVNJfnEWyOCNYjsKj+nffrz1y8SybmY8mKov/HvrEsN1cVH2LrnyXTyLkt11DqI+AStHBdnew==;EndpointSuffix=core.windows.net",  
          "ASPNETCORE_URLS": "https://localhost:61348;http://localhost:61349",  
          "API_URL": "https://localhost:61348",  
          "FRONTEND_URL": "http://localhost:4200",  
          "ASPNETCORE_ENVIRONMENT": "Development",  
          "EMAIL_USR": "plansphere2024@gmail.com",  
          "EMAIL_PSW": "ysgw jisp vrfy euab"
      }  
    }  
  }  
}
```
