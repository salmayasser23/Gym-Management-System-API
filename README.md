# Gym Management System API

A secure and scalable ASP.NET Core Web API for managing gym operations, including trainers, trainer profiles, members, class sessions, enrollments, and authentication.

---

## Overview

Gym Management System API is designed to provide a clean, role-based backend for gym administration and class management. The system supports secure authentication, structured API contracts, relational data management, and interactive API documentation.

The project follows a layered architecture to separate concerns clearly:

- **Entities** define the database schema
- **DTOs** define API input and output contracts
- **Services** contain business logic
- **Controllers** expose RESTful endpoints
- **EF Core** handles persistence and migrations
- **JWT** secures protected resources

---

## Core Features

- JWT-based authentication
- Role-based authorization using **Admin**, **Trainer**, and **Member**
- Trainer management
- Trainer profile management
- Member management
- Class session management
- Enrollment management
- DTO-based request and response handling
- Data Annotation validation
- Entity Framework Core migrations
- Swagger/OpenAPI documentation
- PostgreSQL integration

---

## Entity Relationships

This API demonstrates the following relationship types:

- **One-to-One**  
  `Trainer` -> `TrainerProfile`

- **One-to-Many**  
  `Trainer` -> `ClassSessions`

- **Many-to-Many**  
  `Member` <-> `ClassSession` through `ClassEnrollment`

---

## Technology Stack

- **ASP.NET Core Web API**  
  Used to build RESTful HTTP endpoints.

- **Entity Framework Core**  
  Used as the ORM for database access, relationship mapping, and migrations.

- **PostgreSQL**  
  Used as the relational database engine.

- **ASP.NET Core Identity**  
  Used for user and role management.

- **JWT Bearer Authentication**  
  Used to secure protected endpoints with token-based authentication.

- **Swagger / Swashbuckle**  
  Used to document and test the API interactively.

- **LINQ**  
  Used for filtering, querying, and projecting entities into DTOs.

- **Git & GitHub**  
  Used for version control and source code hosting.

- **Visual Studio Code**  
  Used as the development environment.

---

## Project Structure

```text
GymManagementSystemAPI
├── Controllers
├── Data
├── DTOs
│   ├── Auth
│   ├── Trainers
│   ├── TrainerProfiles
│   ├── Members
│   ├── ClassSessions
│   └── Enrollments
├── Entities
├── Helpers
├── Interfaces
├── Migrations
├── Services
├── Properties
├── appsettings.json
├── appsettings.Development.json
├── Program.cs
└── GymManagementSystemAPI.csproj

## Folder Responsibilities
- Controllers: Expose API endpoints
- Data: Contains AppDbContext and database initialization logic
- DTOs: Request and response models
- Entities: Database models
- Helpers: JWT and service registration helpers
- Interfaces: Service contracts
- Migrations: EF Core migration files
- Services: Business logic and database interaction
