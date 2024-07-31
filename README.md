# Product REST API Service

This repository contains a .NET application implementing a "product" REST API service using a 3-layered architecture. It also includes an Angular frontend for user interactions, covering login, registration, and CRUD operations for products.

## Architecture Overview

The implementation of the 3-layered architecture in .NET involves three main layers (or projects):

1. **Data Layer (DataAccessLayer or DAL)**: This ClassLibrary project handles communication with the database. It contains:
   - Models
   - DbContext and migrations
   - Repository interfaces and their implementations
   - Logic for data retrieval, modification, saving, and deletion

2. **Business Layer (BusinessLogicLayer or BALL)**: This ClassLibrary project contains the application's core logic. It:
   - Retrieves data from the database
   - Performs necessary calculations and additional logic
   - Converts models to DTO objects and returns them to the upper layer
   - Contains interfaces and implementations of services, validators, and DTO objects
   - References the Data Layer project

3. **Presentation Layer (WebApi)**: This WebApi project serves as the entry point of the application. It:
   - Contains controllers with HTTP methods
   - Handles necessary configurations, such as JWT token requirements
   - Communicates with the Business Layer via service interfaces
   - Receives DTOs as input parameters, passes them to the service, and returns DTOs to the external world
   - References both the Data Layer (for context and interface registration) and the Business Layer

4. **Frontend (Angular)**: This Angular project provides a user interface for interacting with the API. It includes:
   - **Login and Registration**: Forms and functionality for user authentication and account creation.
   - **CRUD Operations**: Interfaces for creating, reading, updating, and deleting products.
   - **User-Specific Views**: Displays products assigned to the logged-in user.

## Implementation Details

### Core Requirements

- **CRUD Operations**: Implement standard CRUD operations (GET, POST, PUT, DELETE) for the product REST API service.
- **Documentation**: Utilize Swagger for API documentation.
- **Persistence**: Use SQLite and EntityFramework for data persistence.

### Bonus Features

- **Input Validation**: Implement input validation using FluentValidation.
- **Error Handling**: Include comprehensive error handling.
- **3-Layer Architecture**: Focus on the "interface segregation" principle.
- **Authentication and Authorization**: Implement JWT-based authentication and authorization.

### Additional Specifications

- **Many-to-Many Relationship**: Create a table for linking users and products. Each user can have multiple products, and each product can have multiple users.
- **Product Deletion**: Only the user who added the product (owner) can delete that product. This means that only the user who created the product can perform the delete operation.
- **Product Assignment**: Products can be assigned to other users. This functionality allows a single product to be associated with multiple users.
- **Product Listing**: The API for listing products should return only the products assigned to a specific user. This means that when a user requests a list of products, they will receive only the products assigned to them.

## Project Structure

- **DataLayer (ClassLibrary)**
  - Models
  - DbContext
  - Migrations
  - Repository interfaces and implementations

- **BusinessLayer (ClassLibrary)**
  - Services and their interfaces
  - Validators
  - DTO objects

- **PresentationLayer (WebApi)**
  - Controllers
  - Configurations (e.g., JWT)
  - Dependency Injection setup

- **Frontend (Angular)**
  - Components for login, registration, and CRUD operations
  - Services for API communication
  - Routing and guards for user access

## How to Run

1. Clone the repository.
2. Navigate to the project directory.
3. Build the solution using Visual Studio or the .NET CLI.
4. Run the backend project using Visual Studio or the .NET CLI:
    ```bash
    dotnet build
    dotnet run --project PresentationLayer
    ```
5. Navigate to the Angular project directory.
6. Install dependencies and start the Angular application:
    ```bash
    npm install
    ng serve
    ```

This setup provides a complete end-to-end solution, from API service to user interface.
