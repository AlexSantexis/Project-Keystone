# PROJECT KEYSTONE BACK-END 
## Developed with asp.net web api 
Project Keystone is a .NET 8.0 web application showcasing various modern technologies and best practices.

## Technologies Used

- **ASP.NET Core 8.0.5**
- **Entity Framework Core 8.0.5**
- **Authentication and Authorization**
  - Microsoft.AspNetCore.Authentication.JwtBearer 8.0.5
  - Microsoft.AspNetCore.Identity.EntityFrameworkCore 8.0.5
- **Mapping**
  - AutoMapper 13.0.1
- **Environment Variables**
  - DotNetEnv 3.0.0
- **Logging**
  - Serilog 3.1.1
  - Serilog.AspNetCore 8.0.1
- **Swagger Documentation**
  - Swashbuckle.AspNetCore 6.6.2


## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) installed on your machine.

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/AlexSantexis/Project-Keystone.git
   cd Project-Keystone

2. Create an .env file in the root directory and configure your environment variables if necessary (e.g., database connection strings, API keys).

3. Create Database:
You need to create a database in your preferred SQL Server instance (e.g., SQL Server Management Studio, Azure SQL).

4.Open a terminal or command prompt and navigate to the project directory.
Run the following command to add an initial migration: dotnet ef migrations add InitialCreate --startup-project Project-Keystone

5. Apply the migration to update the database: dotnet ef database update --startup-project Project-Keystone
You are ready to go!




