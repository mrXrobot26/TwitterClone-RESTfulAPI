# Twitter Clone API
Twitter Clone API is a robust web application built using cutting-edge technologies to provide a seamless user experience. The application adopts an N-tier architecture, leveraging ASP.NET Core 8, Entity Framework Core. The RESTful API backend ensures efficient communication, while JWT authentication enhances security.

## Technologies
* ASP.NET Core 8
* Entity Framework Core
* RESTful APIs
* MS SQL Server
* JWT Authentication
* Identity for User Management
* AutoMapper

## Architecture
Twitter Clone API follows a robust N-tier architecture, which includes:

* **Business Layer**: Implements core business logic.
* **Data Access Layer**: Utilizes the Repository and Unit of Work patterns for efficient data retrieval.
* **Repository Pattern**: Organizes data access logic.
* **Unit of Work Pattern**: Manages transactions.
* **Dependency Injection**: Enhances code modularity and testability.

## Backend
The RESTful APIs provide JSON responses. Key APIs include:

### User Management and Authentication
* User registration and login.
* Follow and unfollow other users.
* Check if one user is following another.

### Post Management
* Create, update, and delete posts.
* Like and unlike posts.
* Comment on posts.

### Timeline
* View posts from users you follow.
* Get mutual followers.

### Following
* Follow and unfollow users.
* Get list of users you are following.
* Get list of users who follow you.

## Running the Project
To run Twitter Clone API:

1. Clone the repository.
2. Configure connection strings in `appsettings.json` for seamless database interaction.
3. Run database migrations to initialize the data structure.
4. Build and run the application.

#Explore the power of Twitter Clone API, where technology meets social interaction.

#Contact
For any inquiries or issues, please contact the repository owner @mrXrobot26.

## Configuration
Ensure that your `appsettings.json` is correctly set up for your environment. Here is an example:
 
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "YourConnectionStringHere"
  },
  "Jwt": {
    "Key": "YourJwtKeyHere",
    "Issuer": "YourIssuer",
    "Audience": "YourAudience"
  }
}

