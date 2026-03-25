# Gym Management System API

A secure ASP.NET Core Web API for managing gym operations, including trainers, trainer profiles, members, class sessions, enrollments, authentication, and role-based access control.

## Overview

This API provides a backend service for gym management with:

- JWT authentication
- Role-based authorization
- DTO-based request and response models
- Validation using Data Annotations
- Entity Framework Core with PostgreSQL
- Swagger documentation
- EF Core migrations

## Entity Relationships

- **One-to-One**: `Trainer` -> `TrainerProfile`
- **One-to-Many**: `Trainer` -> `ClassSessions`
- **Many-to-Many**: `Member` <-> `ClassSession` through `ClassEnrollment`

## Technology Stack

- **ASP.NET Core Web API**: builds RESTful endpoints
- **Entity Framework Core**: ORM for data access and migrations
- **PostgreSQL**: relational database
- **ASP.NET Core Identity**: user and role management
- **JWT Bearer Authentication**: token-based security
- **Swagger / Swashbuckle**: API documentation and testing
- **LINQ**: querying and DTO projection

## Project Structure

```text
GymManagementSystemAPI
├── Controllers
├── Data
├── DTOs
├── Entities
├── Helpers
├── Interfaces
├── Migrations
├── Services
├── appsettings.json
├── Program.cs
└── GymManagementSystemAPI.csproj

## Configuration
Update the application configuration with your PostgreSQL credentials and JWT settings.
