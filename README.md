# Portfolio Manager API

Professional **ASP.NET Core Web API** for managing portfolio projects.

![.NET](https://img.shields.io/badge/.NET-8-blue)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-Database-blue)
![Dapper](https://img.shields.io/badge/Dapper-ORM-orange)
![Swagger](https://img.shields.io/badge/Swagger-OpenAPI-green)
![Render](https://img.shields.io/badge/Deploy-Render-purple)
![License](https://img.shields.io/badge/license-MIT-lightgrey)

------------------------------------------------------------------------

# 🌐 Live API

Interactive Swagger documentation:

https://portfolio-manager-api-jpfh.onrender.com/swagger

This API is deployed on **Render** and connected to a **PostgreSQL
database**.

------------------------------------------------------------------------

# 📌 Overview

**Portfolio Manager API** is a backend project built with **ASP.NET Core
Web API** that demonstrates modern backend development practices.

The goal of this project is to showcase:

-   Clean architecture concepts
-   RESTful API design
-   Structured logging
-   Pagination and filtering
-   Repository pattern
-   Service layer separation
-   API versioning
-   Database access using **Dapper**

------------------------------------------------------------------------

# 🧰 Technologies

-   .NET 8
-   ASP.NET Core Web API
-   PostgreSQL
-   Dapper
-   Serilog (structured logging)
-   Swagger / OpenAPI
-   API Versioning
-   Dependency Injection
-   Repository Pattern
-   Service Layer
-   Render (deployment)

------------------------------------------------------------------------

# 📖 API Documentation

Swagger provides:

-   Interactive endpoint testing
-   Request/response models
-   Parameter documentation

Access it here:

https://portfolio-manager-api-jpfh.onrender.com/swagger

------------------------------------------------------------------------

# 📸 Swagger Preview

*(Optional: add a screenshot here)*

Example:

    docs/swagger-preview.png

Then reference it like:

    ![Swagger Screenshot](docs/swagger-preview.png)

------------------------------------------------------------------------

# 🚀 Features

-   Create projects
-   Update projects
-   Delete projects
-   Get project by ID
-   Paginated project listing
-   Filter projects by status
-   Search projects by **title**
-   Dashboard statistics endpoint
-   Structured logging with Serilog
-   API versioning
-   Global exception middleware
-   Swagger documentation

------------------------------------------------------------------------

# 📡 API Endpoints

Base URL

https://portfolio-manager-api-jpfh.onrender.com/api/v1/projects

------------------------------------------------------------------------

# Get All Projects

    GET /api/v1/projects

Query Parameters

  Parameter   Description
  ----------- ------------------------------
  page        Page number
  pageSize    Number of items per page
  status      Filter by project status
  search      Search projects by **title**

Example

    /api/v1/projects?page=1&pageSize=10&status=Active&search=portfolio

Example Response

``` json
{
  "data": [
    {
      "id": 1,
      "title": "Portfolio Website",
      "description": "Personal portfolio built with React",
      "status": "Active",
      "createdAt": "2025-02-10"
    }
  ],
  "totalCount": 15,
  "page": 1,
  "pageSize": 10
}
```

------------------------------------------------------------------------

# Get Project by ID

    GET /api/v1/projects/{id}

Example

    /api/v1/projects/1

------------------------------------------------------------------------

# Create Project

    POST /api/v1/projects

Example Body

``` json
{
  "title": "Portfolio Website",
  "description": "Personal portfolio built with React and .NET",
  "status": "Active"
}
```

------------------------------------------------------------------------

# Update Project

    PUT /api/v1/projects/{id}

------------------------------------------------------------------------

# Delete Project

    DELETE /api/v1/projects/{id}

------------------------------------------------------------------------

# 📊 Project Statistics

Endpoint used for dashboards.

    GET /api/v1/projects/stats

Example Response

``` json
{
  "success": true,
  "data": {
    "total": 12,
    "active": 5,
    "inProgress": 3,
    "completed": 3,
    "paused": 1
  },
  "errors": null
}
```

------------------------------------------------------------------------

# 🏗 Architecture

Simplified architecture diagram:

    Client / Frontend
            |
            v
    Controllers
            |
            v
    Application Services
            |
            v
    Repositories
            |
            v
    PostgreSQL Database

Project folder structure:

    Controllers
    Application
    Infrastructure
    Data
    DTOs
    Middleware

------------------------------------------------------------------------

# ▶ Running Locally

Clone repository

    git clone https://github.com/your-username/portfolio-manager-api.git

Configure environment variable

    ConnectionStrings__DefaultConnection

Example

    Host=localhost;Port=5432;Database=portfolio;Username=postgres;Password=postgres

Run the project

    dotnet run

Open Swagger

    https://localhost:xxxx/swagger

------------------------------------------------------------------------

# 📈 Possible Future Improvements

-   Authentication with JWT
-   Docker containerization
-   CI/CD with GitHub Actions
-   Caching with Redis
-   Unit and integration tests

------------------------------------------------------------------------

# 👨‍💻 Author

Silvio Dias Ferreira

Backend Developer
