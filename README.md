# Parking System — REST API + MVC Client

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![REST API](https://img.shields.io/badge/API-REST-009688)
![Architecture](https://img.shields.io/badge/architecture-API%20%2B%20MVC%20Client%20%2B%20DI-2b6cb0)

A parking-lot management system built as **two decoupled ASP.NET Core applications**: a **REST API** backed by Entity Framework Core / SQL Server, and a separate **ASP.NET Core MVC web client** that consumes it over HTTP. Full CRUD for parking lots, tickets, and employees.

Originally the final project ("Proyecto 3") for the Web Programming course of the Informatics Engineering degree at **Universidad Estatal a Distancia de Costa Rica (UNED)**.

> ℹ️ Academic project (2023), built on **.NET / ASP.NET Core**. Kept here because it demonstrates a clean separation between a REST API and a consuming front-end, with a service layer and dependency injection on both sides.

---

## Why this project is interesting

Instead of a single monolithic web app, the system is split into **two independent services** that talk over HTTP/JSON:

```
┌─────────────────────────────┐        HTTP + JSON        ┌─────────────────────────────┐
│        MVC Web Client        │  ───────────────────────▶ │          REST API            │
│  (Razor views + controllers) │   api/Parqueo/...          │  [ApiController] endpoints    │
│                              │   api/Tiquete/...          │                              │
│  Typed service layer:        │ ◀─────────────────────────│  EF Core DbContext           │
│  IParqueoServices →          │        JSON responses      │  (Empleado/Parqueo/Tiquete)  │
│  ParqueoServices (HttpClient)│                            └──────────────┬───────────────┘
└─────────────────────────────┘                                           │ EF Core
                                                                           ▼
                                                                   SQL Server database
```

- The **client never touches the database directly** — it only calls the API.
- Each entity has a **typed service behind an interface** (`IParqueoServices` → `ParqueoServices`, etc.), registered with ASP.NET Core **dependency injection** (`AddScoped`). Controllers depend on the interface, not the concrete class.
- The API uses **constructor-injected `DbContext`** and returns `ActionResult` with proper HTTP verbs (`GET`/`POST`/`DELETE`).

This is a small but faithful example of the layered, DI-driven architecture used in production .NET backends.

---

## Projects

| Project | Role |
|---|---|
| **Proyecto3API** | ASP.NET Core **Web API**. `[ApiController]` endpoints under `api/{resource}`, EF Core `DbContext` (`Contexto`) with `DbSet`s for Empleado, Parqueo, Tiquete. Swagger/OpenAPI enabled in Development. Creates the schema on startup via `EnsureCreated()`. |
| **Proyecto3Client** | ASP.NET Core **MVC** front-end. Razor views (Index/Create/Edit/Delete) per entity, controllers that call a **typed service layer** (`HttpClient`, async/await, JSON) registered via DI. Bootstrap UI. |

### Domain entities

- **Empleado** — parking-lot employees
- **Parqueo** — parking lots
- **Tiquete** — parking tickets (entry/exit datetime, plate, hourly / half-hour rate) with `DataAnnotations` validation (`[Required]`, `[Key]`, `[Display]`)

### API endpoints (per resource)

| Method | Route | Action |
|---|---|---|
| `GET`  | `api/Parqueo/GetAllParqueos` | List all |
| `GET`  | `api/Parqueo/GetParqueoXId/{id}` | Get by id |
| `POST` | `api/Parqueo/AddParqueo` | Create |
| `POST` | `api/Parqueo/UpdateParqueo` | Update |
| `DELETE` | `api/Parqueo/DeleteParqueo/{id}` | Delete |

(The same pattern exists for `Empleado` and `Tiquete`.)

---

## Tech stack

- **Language:** C# (.NET / ASP.NET Core)
- **API:** ASP.NET Core Web API, **Entity Framework Core**, Swagger / OpenAPI
- **Client:** ASP.NET Core **MVC** (Razor), `HttpClient`, dependency injection, Bootstrap
- **Database:** SQL Server
- **Serialization:** JSON (System.Text.Json / Newtonsoft.Json)

---

## Getting started

### Prerequisites

- **.NET SDK** (6/7+)
- **SQL Server** (Express or LocalDB is fine)
- Visual Studio 2022 or `dotnet` CLI

### 1. Configure the database connection (API)

In **Proyecto3API**, set the `AppConnection` connection string. **Do not commit real credentials** — use `appsettings.Development.json` (git-ignored) or User Secrets:

```json
{
  "ConnectionStrings": {
    "AppConnection": "data source=YOUR_SERVER;initial catalog=SistemaDeParqueos;user id=YOUR_USER;password=YOUR_PASSWORD;TrustServerCertificate=true"
  }
}
```

The API calls `EnsureCreated()` on startup, so the schema is created automatically from the EF Core model on first run. (`SistemaDeParqueos.sql` is also included for reference.)

### 2. Run the API

```bash
cd Proyecto3API
dotnet run
```

Swagger UI is available in Development for exploring the endpoints.

### 3. Point the client at the API

In **Proyecto3Client**, the service classes use a base URL (`http://localhost:5154` by default). Update it if your API runs on a different port.

### 4. Run the client

```bash
cd Proyecto3Client
dotnet run
```

Open the client in the browser and manage parking lots, tickets, and employees through the Razor UI.

---

## What I learned

- Designing a **decoupled REST API** and a separate front-end that consumes it — rather than one monolithic MVC app.
- Building a **typed service layer behind interfaces** and wiring it with **dependency injection** (`AddScoped`).
- Consuming an HTTP API cleanly with `HttpClient` + async/await + JSON (de)serialization.
- Entity Framework Core basics: `DbContext`, `DbSet`, model configuration, and `DataAnnotations` validation.

---

## Author

**Sergio Ignacio Saborío Segura**
Informatics Engineer — Universidad Estatal a Distancia de Costa Rica (UNED)
Web Programming final project · 2023
