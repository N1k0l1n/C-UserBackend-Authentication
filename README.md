# C-UserBackend-Authentication

### C-UserBackend-Authentication is a backend web application built with C# that provides user authentication functionality. The application allows users to create an account, log in, and log out.

## Getting Started
# Prerequisites
.NET SDK (version 5.0 or higher)
A database management system (SQL Server, MySQL, PostgreSQL, etc.)
Installing
Clone the repository:

bash
Copy code
# git clone https://github.com/username/c-userbackend-authentication.git
Navigate to the C-UserBackend-Authentication directory:

Copy code
cd c-userbackend-authentication/C-UserBackend-Authentication
Open the appsettings.json file and update the database connection string with your database credentials.

Run the following commands to create and migrate the database:

sql
Copy code
dotnet ef database update
## Run the application:

Copy code
dotnet run
The application should now be running on # http://localhost:5000.

Usage
Endpoints
The following endpoints are available:

POST /api/auth/register - create a new user account
POST /api/auth/login - log in as an existing user and receive a JWT token
POST /api/auth/logout - log out the current user
The /api/auth/register and /api/auth/login endpoints return a JWT token that you can include in the Authorization header of your requests to access protected endpoints.

## Authorization
To access protected endpoints, you must include the JWT token in the Authorization header of your requests. You can do this by adding the following header to your requests:

makefile
Copy code
Authorization: Bearer <jwt_token>
Replace <jwt_token> with the token returned by the /api/auth/register or /api/auth/login endpoint.

# Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

# License
This project is licensed under the No License - see the LICENSE file for details.
