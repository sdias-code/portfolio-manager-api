# Portfolio Manager API

Professional ASP.NET Core Web API for managing portfolio projects.

## 🌐 API Online

https://portfolio-manager-api-jpfh.onrender.com/swagger

## Overview

This project demonstrates a professional backend architecture using
ASP.NET Core Web API.\
It was designed as a portfolio project to showcase backend development
skills such as clean architecture, REST API design, logging, versioning,
and database access using Dapper.

## Technologies

-   .NET 8
-   ASP.NET Core Web API
-   Dapper
-   PostgreSQL
-   Serilog (structured logging)
-   Swagger / OpenAPI
-   API Versioning
-   Dependency Injection
-   Repository Pattern
-   Service Layer

 ## 📖 Documentação

https://portfolio-manager-api-sqrr.onrender.com/swagger

## Features

-   Create projects
-   Update projects
-   Delete projects
-   Get project by id
-   Paginated project listing
-   Structured logging
-   API versioning
-   Global exception handling
-   Swagger documentation

## Project Structure

Controllers\
Handles HTTP requests and responses.

Application\
Contains business logic and service interfaces.

Infrastructure\
Repositories and database access.

Data\
Database connection configuration.

DTOs\
Data Transfer Objects used by the API.

Middleware\
Global exception handling.

## Running the Project

1.  Clone the repository

2.  Configure environment variable:

ConnectionStrings\_\_DefaultConnection

3.  Run the application

dotnet run

4.  Open Swagger

https://localhost:xxxx/swagger

## Author

Silvio Dias Ferreira
