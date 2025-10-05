# ASP.NET Backend Setup Guide

## Prerequisites
- .NET 8.0 SDK
- SQL Server or SQL Server Express
- Git

## Setup Commands

### 1. Clone Repository
```bash
git clone <repository-url>
cd <project-folder>
```

### 2. Install EF Tools
```bash
dotnet tool install --global dotnet-ef
```

### 3. Configure Database
Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=YourDatabaseName;Trusted_Connection=true"
  }
}
```



### 4. Database Migration
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

#### Database Script
You can find the database script file inside the finap/SQL Script folder.

### 5. Build and Run
```bash
dotnet restore
dotnet build
dotnet run
```

## Verification
- API: `https://localhost:5001`
- Swagger: `https://localhost:5001/swagger`


