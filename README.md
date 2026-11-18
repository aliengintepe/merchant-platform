# Merchant Platform

Merchant Platform is a document OCR and classification demo. Its Angular frontend accepts document images, and an ASP.NET Core API built on ABP extracts Turkish text with Tesseract. ML.NET classifies text into identity cards, tax certificates, or trade registry gazettes. The backend stores OCR results through Entity Framework Core and SQL Server.

## Project structure

- **angular** contains the document-upload UI and API clients.
- **aspnet-core/src/merchantPlatform.HttpApi.Host** hosts the API, Swagger, and OpenIddict.
- **aspnet-core/src/merchantPlatform.Application** contains OCR, image preprocessing, and the ML.NET training pipeline.
- **aspnet-core/src/merchantPlatform.EntityFrameworkCore** contains persistence and migrations.
- **aspnet-core/src/merchantPlatform.DbMigrator** applies migrations and seeds initial data.
- **aspnet-core/test** contains backend tests.

The included CSV is a small synthetic fixture for demonstrating the classification pipeline. Use sample documents with fictional data when trying the app. Model quality for real documents is outside the scope of this fixture.

## Run locally

The local setup uses .NET 9, Node.js 20, a trusted ASP.NET Core development certificate, and SQL Server Express LocalDB. The backend and database migrator share the same demo database. To use another SQL Server instance, configure **ConnectionStrings__Default** for both processes.

Run the following from the repository root in PowerShell.

~~~powershell
dotnet restore .\aspnet-core\merchantPlatform.sln
dotnet dev-certs https --trust
dotnet run --project .\aspnet-core\src\merchantPlatform.DbMigrator
~~~

Start the API in one terminal.

~~~powershell
cd .\aspnet-core\src\merchantPlatform.HttpApi.Host
dotnet run --launch-profile merchantPlatform.HttpApi.Host
~~~

Start the frontend in a separate terminal from the repository root.

~~~powershell
cd .\angular
npm ci
npm start
~~~

Open [the Angular app](http://localhost:4200) or [Swagger](https://localhost:44380/swagger). See the [backend setup notes](aspnet-core/README.md) for ABP client libraries and signing certificates. Use environment variables for credentials and environment-specific configuration.

## Tests

~~~powershell
dotnet test .\aspnet-core\merchantPlatform.sln
~~~
