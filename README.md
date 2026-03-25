# Gym Management System API

A professional ASP.NET Core Web API for managing gym operations, including trainers, trainer profiles, members, class sessions, enrollments, authentication, and role-based access control.

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
- [Why HTTP-only Cookies Are Commonly Used](#why-http-only-cookies-are-commonly-used)
- [Testing Workflow](#testing-workflow)
- [Screenshots](#screenshots)
- [Future Improvements](#future-improvements)

---

## Project Overview

Gym Management System API is a RESTful backend application built with ASP.NET Core Web API to support the core operations of a gym management platform.

The system provides secure authentication, role-based authorization, structured request and response contracts through DTOs, database persistence using PostgreSQL, and interactive API documentation through Swagger.

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

- JWT-based authentication
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
Used as the ORM for mapping entities to the database, managing relationships, and creating migrations.

### PostgreSQL
Used as the relational database system for storing application data.

### ASP.NET Core Identity
Used for user management, password hashing, and role handling.

### JWT Bearer Authentication
Used to secure endpoints with token-based authentication.

### Swagger / Swashbuckle
Used to document and test API endpoints interactively.

### LINQ
Used for querying, filtering, and projecting entities into DTOs.

### Git and GitHub
Used for version control and source code hosting.

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
git clone https://github.com/YOUR_USERNAME/YOUR_REPOSITORY_NAME.git
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

The API uses JWT authentication.

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

Examples:

* Admin can manage core records
* Trainer can access trainer/class-related endpoints
* Member can access member/enrollment-related functionality depending on the controller configuration

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

## Why HTTP-only Cookies Are Commonly Used

Although this project uses JWT tokens in the `Authorization` header, HTTP-only cookies are commonly used as an industry standard because they help protect authentication tokens from JavaScript access. This reduces the risk of token theft through Cross-Site Scripting (XSS) attacks.

In many secure web applications, HTTP-only cookies are preferred because:

* they cannot be read directly by client-side JavaScript
* they reduce exposure of sensitive authentication tokens
* they can be combined with secure cookie settings and CSRF protection for stronger overall security

For this project, JWT bearer authentication was used because it is simple to test in Swagger and suitable for demonstrating protected API endpoints.

---

## Testing Workflow

The API was tested in Swagger using the following sequence:

1. `POST /api/Auth/login`
2. Authorize using the returned token
3. `GET /api/Trainers`
4. `POST /api/Trainers`
5. `POST /api/TrainerProfiles`
6. `POST /api/Members`
7. `POST /api/ClassSessions`
8. `POST /api/Enrollments`
9. Validation/error scenario testing

### Sample Request Bodies Used in Testing

#### Login

```json
{
  "email": "admin@gmail.com",
  "password": "Admin123!"
}
```

#### Create Trainer

```json
{
  "fullName": "Ahmed Ali",
  "email": "ahmed.trainer@gmail.com",
  "phoneNumber": "01012345678",
  "specialization": "CrossFit",
  "yearsOfExperience": 5
}
```

#### Create Trainer Profile

```json
{
  "bio": "Certified CrossFit trainer with strong experience in strength and conditioning.",
  "certification": "CrossFit Level 1",
  "emergencyContactName": "Mohamed Ali",
  "emergencyContactPhone": "01099998888",
  "trainerId": 1
}
```

#### Create Member

```json
{
  "fullName": "Omar Hassan",
  "email": "omar.member@gmail.com",
  "phoneNumber": "01077776666",
  "membershipType": "Premium",
  "joinDate": "2026-03-18T10:00:00Z"
}
```

#### Create Class Session

```json
{
  "title": "Morning Cardio",
  "description": "High-energy cardio training session.",
  "roomName": "Room A",
  "capacity": 20,
  "startTime": "2026-03-20T08:00:00Z",
  "endTime": "2026-03-20T09:00:00Z",
  "trainerId": 1
}
```

#### Create Enrollment

```json
{
  "memberId": 1,
  "classSessionId": 1
}
```

#### Validation Error Example

```json
{
  "title": "Wrong Class",
  "description": "Invalid timing test",
  "roomName": "Room B",
  "capacity": 10,
  "startTime": "2026-03-20T10:00:00Z",
  "endTime": "2026-03-20T09:00:00Z",
  "trainerId": 1
}
```

---

## Screenshots


### Swagger Home

<img width="1906" height="865" alt="image" src="https://github.com/user-attachments/assets/35d198f1-b464-4531-983f-1cde103381ee" />
<img width="1808" height="895" alt="image" src="https://github.com/user-attachments/assets/2f22b751-170a-4d58-a42e-b2e6581cb39b" />
<img width="1798" height="897" alt="image" src="https://github.com/user-attachments/assets/ab7651e0-2c4c-4aa0-8cb5-93892df5870d" />
<img width="1785" height="202" alt="image" src="https://github.com/user-attachments/assets/fed78e26-2cf3-44bc-8638-6448e714a69b" />





### Login Success

![Login Success](docs/screenshots/login-success.png)

### Authorized Trainers Endpoint

![Authorized Trainers](docs/screenshots/authorized-trainers.png)

### Create Trainer

![Create Trainer](docs/screenshots/create-trainer.png)

### Create Trainer Profile

![Create Trainer Profile](docs/screenshots/create-trainerprofile.png)

### Create Member

![Create Member](docs/screenshots/create-member.png)

### Create Class Session

![Create Class Session](docs/screenshots/create-classsession.png)

### Create Enrollment

![Create Enrollment](docs/screenshots/create-enrollment.png)

### Validation Error Example

![Validation Error](docs/screenshots/validation-error.png)

### Database Tables

![Database Tables](docs/screenshots/database-tables.png)

### Database Rows Example

![Database Rows Example](docs/screenshots/database-rows.png)

---

## Future Enhancements

* Refresh token support
* Background jobs using Hangfire
* More advanced role restrictions
* Reporting and analytics endpoints
* Frontend integration with a web or mobile client

---

