![CI](https://github.com/nachosaborio/ParkingSystem/actions/workflows/ci.yml/badge.svg)

# Parking System

![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-512BD4?logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?logo=dotnet&logoColor=white)
![SQLite](https://img.shields.io/badge/SQLite-003B57?logo=sqlite&logoColor=white)
![REST API](https://img.shields.io/badge/API-REST-009688)
![Architecture](https://img.shields.io/badge/architecture-API%20%2B%20MVC%20Client%20%2B%20DI-2b6cb0)

A parking-lot management system built as **two decoupled ASP.NET Core applications**: a **REST API** backed by Entity Framework Core / SQLite, and a separate **ASP.NET Core MVC web client** that consumes it over HTTP. Full CRUD for parking lots, tickets, and employees.

Originally the final assignment for the Web Programming course of the Informatics Engineering degree at **Universidad Estatal a Distancia de Costa Rica (UNED)**.

> ℹ️ Academic work (2023), built on **.NET 6 / ASP.NET Core**. Kept here because it demonstrates a clean separation between a REST API and a consuming front-end, with a service layer and dependency injection on both sides.

🎥 **Demo video:** [https://youtu.be/7j-EFqa-Ic8](https://youtu.be/7j-EFqa-Ic8) (link also stored in [`docs/Link video.txt`](docs/Link%20video.txt)).

---

## Why this system is interesting

Instead of a single monolithic web app, the system is split into **two independent services** that talk over HTTP/JSON:

```
┌─────────────────────────────┐        HTTP + JSON        ┌─────────────────────────────┐
│        MVC Web Client        │  ───────────────────────▶ │          REST API            │
│  (Razor views + controllers) │   api/Parqueo/...          │  [ApiController] endpoints    │
│                              │   api/Tiquete/...          │                              │
│  Typed service layer:        │ ◀─────────────────────────│  EF Core DbContext           │
│  IParqueoServices →          │        JSON responses      │  (Empleado/Parqueo/Tiquete)  │
│  ParqueoServices (HttpClient)│                            └──────────────┬───────────────┘
└─────────────────────────────┘                                           │ EF Core (Sqlite provider)
                                                                           ▼
                                                              SQLite database (parking.db)
```

- The **client never touches the database directly** — it only calls the API.
- Each entity has a **typed service behind an interface** (`IParqueoServices` → `ParqueoServices`, etc.), registered with ASP.NET Core **dependency injection** (`AddScoped`). Controllers depend on the interface, not the concrete class.
- The API uses **constructor-injected `DbContext`** and returns `ActionResult` with proper HTTP verbs (`GET`/`POST`/`DELETE`).
- Both applications share a single definition of the domain models through the **`Parking.Shared`** class library.

This is a small but faithful example of the layered, DI-driven architecture used in production .NET backends.

---

## Repository structure

```
/
├── ParkingSystem.sln          # Solution with Parking.Api, Parking.Client and Parking.Shared
├── src/
│   ├── Parking.Api/           # ASP.NET Core Web API + EF Core (SQLite) + seeder
│   ├── Parking.Client/        # ASP.NET Core MVC client (Razor views, typed services, LibMan)
│   └── Parking.Shared/        # Class library with the domain models (Empleado, Parqueo, Tiquete)
├── db/                        # Notes about the SQLite database (parking.db is generated, not versioned)
└── docs/                      # Documentation (demo video link)
```

| Component | Role |
|---|---|
| [**src/Parking.Api**](src/Parking.Api) | ASP.NET Core **Web API**. `[ApiController]` endpoints under `api/{resource}`, EF Core `DbContext` (`Contexto`) with `DbSet`s for Empleado, Parqueo, Tiquete. Swagger/OpenAPI enabled in Development. Creates the SQLite schema on startup via `EnsureCreated()` and seeds sample data. |
| [**src/Parking.Client**](src/Parking.Client) | ASP.NET Core **MVC** front-end. Razor views (Index/Create/Edit/Delete) per entity, controllers that call a **typed service layer** (`HttpClient`, async/await, JSON) registered via DI. Bootstrap UI, front-end libraries restored with LibMan. |
| [**src/Parking.Shared**](src/Parking.Shared) | net6.0 class library with the domain models in `Parking.Shared.Models`, referenced by both the API and the client. |
| [**db/**](db) | Explains where the SQLite database lives and how it is generated. |
| [**docs/**](docs) | Supporting documentation, including the [demo video link](docs/Link%20video.txt). |

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

- **Language:** C# (.NET 6 / ASP.NET Core, `net6.0`)
- **API:** ASP.NET Core Web API, **Entity Framework Core** (`Microsoft.EntityFrameworkCore.Sqlite`), Swagger / OpenAPI
- **Client:** ASP.NET Core **MVC** (Razor), `HttpClient`, dependency injection, Bootstrap
- **Database:** SQLite (local file `parking.db`, no server required)
- **Front-end libraries:** Bootstrap, jQuery, jQuery Validation and jQuery Validation Unobtrusive, managed with **LibMan** (`src/Parking.Client/libman.json`)
- **Serialization:** JSON (System.Text.Json / Newtonsoft.Json)

---

## Database

- **Schema:** generated by EF Core from the model with `EnsureCreated()` when the API starts. There are no SQL scripts to run.
- **Sample data:** on startup the seeder inserts 3 fictitious rows into each table (`Parqueo`, `Empleado`, `Tiquete`) **only if that table is empty**, so you can explore the UI right away.
- **Location:** the connection string `AppConnection` is `Data Source=parking.db`. When running with `dotnet run --project src/Parking.Api`, the file is created at `src/Parking.Api/parking.db` (git-ignored).
- **Reset / regenerate:** stop the API, delete `src/Parking.Api/parking.db` (and any `parking.db-shm` / `parking.db-wal` files), then start the API again. The schema and sample data are recreated automatically.

---

## Getting started

### Prerequisites

- **.NET 6 SDK** (the solution targets `net6.0`)
- Visual Studio 2022 or the `dotnet` CLI
- Network access on the first build, so LibMan can download the front-end libraries

No SQL Server installation and no database credentials are needed.

### 1. Build

```bash
dotnet build ParkingSystem.sln
```

During `dotnet build`, LibMan restores the front-end libraries into `src/Parking.Client/wwwroot/lib/` (this folder is not versioned). The first build needs network access; later builds use the local LibMan cache.

### 2. Run the API

```bash
dotnet run --project src/Parking.Api
```

Swagger UI is available at <http://localhost:5154/swagger> for exploring the endpoints. The SQLite database and sample data are created on first start.

### 3. Run the client

In a second terminal:

```bash
dotnet run --project src/Parking.Client
```

Open <http://localhost:5040> and manage parking lots, tickets, and employees through the Razor UI. The client's service classes call the API at `http://localhost:5154`; update that base URL if your API runs on a different port.

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
Web Programming course final assignment · 2023
