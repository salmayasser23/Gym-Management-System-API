# Gym Management System API

An ASP.NET Core Web API for managing gym operations, including trainers, trainer profiles, members, class sessions, enrollments, authentication, and role-based access control.

---

## Table of Contents

- [Project Overview](#project-overview)
- [System Scope](#system-scope)
- [Key Features](#key-features)
- [Entity Relationships](#entity-relationships)
- [Technology Stack](#technology-stack)
- [Architecture and Project Structure](#architecture-and-project-structure)
- [Configuration](#configuration)
- [How to Run the Project](#how-to-run-the-project)
- [Authentication and Authorization](#authentication-and-authorization)
- [API Endpoint Documentation](#api-endpoint-documentation)
- [Validation and Query Optimization](#validation-and-query-optimization)
- [Authentication with HTTP-only Cookies](#authentication-with-http-only-cookies)
- [Screenshots](#screenshots)

---

## Project Overview

Gym Management System API is a RESTful backend application built with ASP.NET Core Web API to support the core operations of a gym management platform.

The system provides secure authentication using JWT access tokens stored in HTTP-only cookies, role-based authorization, structured request and response contracts through DTOs, database persistence using PostgreSQL, and interactive API documentation through Swagger.

This API is designed to demonstrate clean backend structure, service-layer design, relational modeling, validation, and optimized data access using Entity Framework Core.

---

## System Scope

The API supports management of the following core modules:

- Authentication and login
- Trainers
- Trainer profiles
- Members
- Class sessions
- Enrollments

The application is designed as a backend service and can later be integrated with a web frontend, mobile application, or admin dashboard.

---

## Key Features

- JWT-based authentication using HTTP-only cookies
- Role-based authorization using **Admin**, **Trainer**, and **Member**
- CRUD operations for trainers
- CRUD operations for trainer profiles
- CRUD operations for members
- CRUD operations for class sessions
- Enrollment creation and deletion
- DTO-based request and response models
- Validation using Data Annotations
- EF Core migrations
- Swagger/OpenAPI documentation
- PostgreSQL database integration
- Optimized LINQ queries using `Select()`
- Read-only query optimization using `AsNoTracking()`

---

## Entity Relationships

This project demonstrates the three required relationship types:

### 1. One-to-One
- `Trainer` -> `TrainerProfile`

Each trainer can have one trainer profile, and each trainer profile belongs to one trainer.

### 2. One-to-Many
- `Trainer` -> `ClassSessions`

One trainer can manage multiple class sessions, while each class session belongs to one trainer.

### 3. Many-to-Many
- `Member` <-> `ClassSession` through `ClassEnrollment`

A member can enroll in multiple class sessions, and a class session can contain multiple members.

---

## Technology Stack

### ASP.NET Core Web API
Used to build RESTful endpoints and handle HTTP requests and responses.

### Entity Framework Core
Used as the ORM (Object-Relational Mapping) for mapping entities to the database, managing relationships, and creating migrations.

### PostgreSQL
Used as the relational database system for storing application data.

### ASP.NET Core Identity
Used for user management, password hashing, and role handling.

### JWT (JSON Web Token) Authentication with HTTP-only Cookies
Used to generate access tokens during login and store them in an HTTP-only cookie for authenticated requests.

### Swagger / Swashbuckle
Used to document and test API endpoints interactively.

### LINQ
Used for querying, filtering, and projecting entities into DTOs.

### Visual Studio Code
Used as the development environment.

---

## Architecture and Project Structure

The project follows a layered structure for separation of concerns:

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
````

### Folder Responsibilities

* **Controllers**: Expose HTTP endpoints
* **Data**: Contains `AppDbContext` and database initialization logic
* **DTOs**: Defines request and response models
* **Entities**: Defines database models
* **Helpers**: Contains JWT and service registration helpers
* **Interfaces**: Defines service contracts
* **Migrations**: Contains EF Core migration files
* **Services**: Contains business logic and data access operations

---

## Configuration

The application uses ASP.NET Core configuration files.

### Example `appsettings.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=gym_management_db;Username=postgres;Password=YOUR_PASSWORD"
  },
  "Jwt": {
    "Key": "YOUR_SUPER_SECRET_KEY_HERE",
    "Issuer": "GymManagementSystemApi",
    "Audience": "GymManagementSystemApiUsers",
    "DurationInMinutes": 60
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

### Recommended Configuration Practice

* Keep placeholder values in `appsettings.json`
* Store local secrets in `appsettings.Development.json`
* Ignore `appsettings.Development.json` in source control

---

## How to Run the Project

### 1. Clone the Repository

```bash
git clone https://github.com/salmayasser23/Gym-Management-System-API.git
```

### 2. Open the Project

Open the project in **Visual Studio Code** or **Visual Studio**.

### 3. Configure the Environment

Update the configuration file with:

* PostgreSQL connection details
* JWT secret key
* JWT issuer
* JWT audience

### 4. Create the Database

Create a PostgreSQL database named:

```text
gym_management_db
```

### 5. Apply EF Core Migrations

```bash
dotnet ef database update --project .\GymManagementSystemAPI\GymManagementSystemAPI.csproj
```

### 6. Run the Application

```bash
dotnet run --project .\GymManagementSystemAPI\GymManagementSystemAPI.csproj
```

### 7. Open Swagger

Use the HTTPS URL shown in the terminal, then open:

```text
/swagger
```

Example:

```text
https://localhost:7043/swagger
```

---

## Authentication and Authorization

The API uses JWT authentication with the access token stored in an HTTP-only cookie.

### Login Endpoint

```text
POST /api/Auth/login
```

### Default Seeded Admin User

* **Email**: `admin@gmail.com`
* **Password**: `Admin123!`

### Swagger Authorization Steps

1. Execute `POST /api/Auth/login`
2. Copy the returned token value
3. Click **Authorize** in Swagger
4. Paste the raw token only
5. Confirm authorization
6. Test protected endpoints

### Roles Implemented

* **Admin**
* **Trainer**
* **Member**

### Authorization Design

Role-based authorization is applied using the `[Authorize]` attribute and role restrictions on protected endpoints.

### Current Role Access in This Project

- **Admin** has full access to the main management endpoints.
- **Trainer** can access trainer-related read endpoints, manage class sessions, and view enrollments for class sessions.
- **Member** can access trainer and class session read endpoints and manage enrollments.

---

## API Endpoint Documentation

## Auth

* `POST /api/Auth/login`
* `POST /api/Auth/register`

## Trainers

* `GET /api/Trainers`
* `GET /api/Trainers/{id}`
* `POST /api/Trainers`
* `PUT /api/Trainers/{id}`
* `DELETE /api/Trainers/{id}`

## TrainerProfiles

* `GET /api/TrainerProfiles`
* `GET /api/TrainerProfiles/{id}`
* `GET /api/TrainerProfiles/trainer/{trainerId}`
* `POST /api/TrainerProfiles`
* `PUT /api/TrainerProfiles/{id}`
* `DELETE /api/TrainerProfiles/{id}`

## Members

* `GET /api/Members`
* `GET /api/Members/{id}`
* `POST /api/Members`
* `PUT /api/Members/{id}`
* `DELETE /api/Members/{id}`

## ClassSessions

* `GET /api/ClassSessions`
* `GET /api/ClassSessions/{id}`
* `GET /api/ClassSessions/trainer/{trainerId}`
* `POST /api/ClassSessions`
* `PUT /api/ClassSessions/{id}`
* `DELETE /api/ClassSessions/{id}`

## Enrollments

* `GET /api/Enrollments`
* `GET /api/Enrollments/member/{memberId}`
* `GET /api/Enrollments/classsession/{classSessionId}`
* `POST /api/Enrollments`
* `DELETE /api/Enrollments/{memberId}/{classSessionId}`

---

## Validation and Query Optimization

### DTO Validation

The project uses DTO validation through Data Annotation attributes such as:

* `Required`
* `MaxLength`
* `MinLength`
* `EmailAddress`
* `Range`

Invalid requests return HTTP 400 responses before database operations are performed.

### Query Optimization

For clean and efficient data access:

* Read-only queries use `AsNoTracking()`
* LINQ `Select()` is used to project entities into DTOs
* Controllers do not return entity models directly
* Async EF Core methods such as `ToListAsync()`, `FirstOrDefaultAsync()`, and `SaveChangesAsync()` are used throughout the project

---

## Authentication with HTTP-only Cookies

This project uses JWT authentication with the access token stored in an HTTP-only cookie.

After a successful login:

* the server generates a JWT access token
* the token is stored in an HTTP-only cookie
* the token is not returned in the response body
* protected endpoints read the token from the cookie during authentication

### Why this approach is used

HTTP-only cookies improve security because:

* they cannot be accessed directly by client-side JavaScript
* they reduce exposure of authentication tokens in Cross-Site Scripting (XSS) scenarios
* they provide a cleaner authentication flow for browser-based clients



---


## Screenshots


### Swagger Home

<img width="1898" height="860" alt="image" src="https://github.com/user-attachments/assets/9e8e84e9-f4fe-49d7-acf9-0c978e69503a" />
<img width="1901" height="903" alt="image" src="https://github.com/user-attachments/assets/8cb64813-a30c-4ed1-b521-24d6e858515a" />
<img width="1899" height="894" alt="image" src="https://github.com/user-attachments/assets/ac34f511-0ec8-4d3e-ab0f-35803108ee10" />
<img width="1901" height="217" alt="image" src="https://github.com/user-attachments/assets/42a9c497-f2ae-44bb-9c0b-535ffb357532" />









### Login Success

<img width="1791" height="797" alt="Screenshot 2026-03-25 024224" src="https://github.com/user-attachments/assets/e30d163a-fa60-40d6-91d1-2765af7d6376" />
<img width="1772" height="801" alt="image" src="https://github.com/user-attachments/assets/6b1aeb54-c717-4cd0-bdd7-e16d4c3e7672" />







### Create Trainer

<img width="1781" height="797" alt="Screenshot 2026-03-25 024528" src="https://github.com/user-attachments/assets/c3989d95-ff05-4f65-923e-617d5bb94dd5" />
<img width="1790" height="909" alt="Screenshot 2026-03-25 024550" src="https://github.com/user-attachments/assets/0b59f0eb-3a63-45af-89d8-121340d1bac2" />



### Create Trainer Profile

<img width="1788" height="797" alt="Screenshot 2026-03-25 024932" src="https://github.com/user-attachments/assets/aa6e9007-ce20-4593-a225-54576dc74686" />
<img width="1777" height="905" alt="Screenshot 2026-03-25 024950" src="https://github.com/user-attachments/assets/ddadf6fe-6a3a-4088-a0f3-ade9c171ba7d" />





### Create Member

<img width="1783" height="811" alt="Screenshot 2026-03-25 025603" src="https://github.com/user-attachments/assets/6fa7b50e-e530-4107-baa2-3c713f471b09" />
<img width="1778" height="901" alt="Screenshot 2026-03-25 025624" src="https://github.com/user-attachments/assets/ba266911-db9c-44bc-b595-4652417d0f16" />



### Create Class Session

<img width="1790" height="802" alt="Screenshot 2026-03-25 025750" src="https://github.com/user-attachments/assets/4b1fc47c-b10d-4c35-8dbf-697d67a0c291" />
<img width="1779" height="870" alt="Screenshot 2026-03-25 025814" src="https://github.com/user-attachments/assets/fa178e86-505d-4672-a4ab-ce5850ff3fc5" />
<img width="1772" height="155" alt="Screenshot 2026-03-25 025832" src="https://github.com/user-attachments/assets/5069a6b0-4fd3-4be2-9a49-5164038da305" />





### Create Enrollment

<img width="1790" height="804" alt="Screenshot 2026-03-25 030015" src="https://github.com/user-attachments/assets/f90f5a81-7d45-4f64-b472-7264f4382604" />
<img width="1776" height="836" alt="Screenshot 2026-03-25 030033" src="https://github.com/user-attachments/assets/d03b2b8a-3709-4cf4-b070-e2fa747cd08e" />



### Validation Error Example

<img width="1790" height="809" alt="Screenshot 2026-03-25 030437" src="https://github.com/user-attachments/assets/2420ad4e-9fc6-4277-b05b-95d3ded66132" />
<img width="1785" height="907" alt="Screenshot 2026-03-25 030450" src="https://github.com/user-attachments/assets/bb80e1f8-9d43-473e-90fd-975e3c6c5af5" />
<img width="1773" height="136" alt="Screenshot 2026-03-25 030459" src="https://github.com/user-attachments/assets/8c40ceab-576c-4047-b29b-baddf48be46a" />




### Database Tables

<img width="314" height="416" alt="image" src="https://github.com/user-attachments/assets/0923cf02-098c-44cf-8216-cd61974319f7" />


---


