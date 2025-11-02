# Tekus Provider Management API

## Overview
This project implements a RESTful API for managing providers and their services for Tekus S.A.S. It is built with **.NET 7 (ASP.NET Core)**, **Entity Framework Core**, and follows **Domain-Driven Design (DDD)** principles with a modular and clean architecture.

The system allows:
- CRUD operations for Providers and Services.
- Each service can be linked to multiple countries.
- Custom fields for providers.
- JWT-based authentication with a default user.
- Paginated, searchable, and sortable lists.
- Reporting endpoints (services per country, providers per country).

## Table of Contents
- [Tech Stack](#tech-stack)
- [Database Schema](#database-schema)
- [Authentication](#authentication)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Running Migrations & Seed Data](#running-migrations--seed-data)
- [Testing](#testing)
- [Folder Structure](#folder-structure)
- [Next Steps / Frontend Integration](#next-steps--frontend-integration)

## Tech Stack
- **Backend:** .NET 8, C#
- **ORM:** Entity Framework Core
- **Database:** SQL Server 2018+
- **Authentication:** JWT Bearer Token
- **Tools:** Visual Studio 2022 / Visual Studio Code, Swagger/OpenAPI

## Database Schema
**Entities:**
- **Provider:** Id, NIT, Name, Email, CustomFieldsJson, CreatedAt
- **Service:** Id, ProviderId, Name, HourlyRateUsd
- **Country:** Id, IsoCode, Name
- **ServiceCountry** (junction table): ServiceId, CountryId

**Diagram:** Attach a diagram image here (e.g., `db-schema.png`).

## Authentication
- JWT-based authentication.
- Default user credentials (configurable in `appsettings.json`):
```json
"Auth": {
  "DefaultUser": {
    "Username": "tekus.admin",
    "Password": "Tekus@123"
  }
}
```
- **Login endpoint:** `POST /api/auth/login` returns JWT token to access protected routes.

## Getting Started
1. Clone the repository:
```bash
git clone <your-repo-url>
cd Tekus.Api
```

2. Configure database connection in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=<your-server>;Database=TekusDb;User Id=<user>;Password=<password>;TrustServerCertificate=True;"
}
```

3. Build the project:
```bash
dotnet build
```

4. Run the project:
```bash
dotnet run
```
- Swagger UI will be available at `https://localhost:5001/swagger/index.html`

## API Endpoints
### Auth
- `POST /api/auth/login` – Login with default user, returns JWT token.

### Providers
- `GET /api/providers` – List providers (paginated, searchable, sortable)
- `GET /api/providers/{id}` – Get provider by ID
- `POST /api/providers` – Create a new provider (JWT required)
- `PUT /api/providers/{id}` – Update provider (JWT required)
- `DELETE /api/providers/{id}` – Delete provider (JWT required)
- `POST /api/providers/{id}/customfields` – Add custom field (JWT required)

### Services
- `GET /api/services` – List services (paginated, searchable, sortable)
- `GET /api/services/{id}` – Get service by ID
- `POST /api/services` – Create a service (JWT required)
- `PUT /api/services/{id}` – Update service (JWT required)
- `DELETE /api/services/{id}` – Delete service (JWT required)

### Countries
- `GET /api/countries` – Fetch country list (refresh optional)

### Reports
- `GET /api/reports/summary` – Returns summary indicators:
  - Services count per country
  - Providers count per country

## Running Migrations & Seed Data
The database is automatically migrated and seeded on startup:
```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
    await DataSeeder.SeedAsync(db);
}
```
- Seed data includes at least 10 records per table.
- Countries are fetched from an external service if needed.

## Testing
- Unit tests and integration tests can be added in a separate project `Tekus.Tests`.
- Example test types:
  - Provider creation
  - Service-country mapping
  - JWT authentication validation

## Folder Structure
```
/Tekus.Api
  /Controllers
  /Application
    /DTOs
    /Services
  /Domain
    /Entities
    /Interfaces
  /Infrastructure
    /Data
    /Repositories
    /Services
  Program.cs
  appsettings.json
```
- Following **DDD + Clean Architecture principles**:
  - Controllers call Application Services
  - Application Services use Repositories
  - Domain layer holds Entities and business logic

## Next Steps / Frontend Integration
- Frontend can be implemented with React / Vue / Angular.
- Suggested structure:
  - Authentication via JWT stored in localStorage
  - Providers and Services management pages
  - Use Material Design or similar for UI
  - Connect to API endpoints using Axios or Fetch

## Deployment / Submission
- Ensure your project is fully functional locally.
- Commit all changes and push to GitHub.
- Include in the repository:
  - Complete backend code
  - README.md
  - Database schema diagram
  - Seed scripts
- Email the repository link to: `jaime.marin@tekus.co`
- Optional: Include notes on how frontend would be structured or added later.
