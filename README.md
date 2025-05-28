# Inventory Management API

A robust RESTful API for managing inventory built with ASP.NET Core. This API provides comprehensive endpoints for managing products, categories, suppliers, and user authentication with role-based access control.

## 🌟 Features

### Authentication & Authorization
- JWT-based authentication
- Role-based access control (Admin and User roles)
- Secure password hashing
- Token-based session management

### Product Management
- Create, Read, Update, Delete (CRUD) operations for products
- Product categorization
- Stock level tracking
- Low stock notifications
- Product search and filtering

### Category Management
- CRUD operations for product categories
- Category hierarchy support
- Category-based product organization

### Supplier Management
- CRUD operations for suppliers
- Supplier contact information
- Supplier-product relationships

### Additional Features
- Swagger/OpenAPI documentation
- Error handling middleware
- Request validation
- Database migrations
- Environment-based configuration

## 🛠️ Technologies Used

- **Framework**: ASP.NET Core 9.0
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Object Mapping**: AutoMapper
- **API Documentation**: Swagger/OpenAPI
- **Logging**: Built-in ASP.NET Core logging
- **Validation**: Data Annotations

## 📋 Prerequisites

- .NET 9.0 SDK or later
- SQL Server (2019 or later)
- Visual Studio 2022 or VS Code
- Git

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone https://github.com/Sahaj2310/Inventory-Management-Api.git
cd Inventory-Management-Api
```

### 2. Configure the Database
1. Update the connection string in `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=your_database;Trusted_Connection=True;Encrypt=False"
  }
}
```

### 3. Configure JWT Settings
1. Create `appsettings.Development.json` with your JWT settings:
```json
{
  "Jwt": {
    "Key": "your-secure-key-here",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "ExpiresInMinutes": 60
  }
}
```

### 4. Install Dependencies
```bash
dotnet restore
```

### 5. Run Database Migrations
```bash
dotnet ef database update
```

### 6. Run the Application
```bash
dotnet run
```

The API will be available at `https://localhost:7186`

## 📚 API Documentation

### Authentication Endpoints

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "string",
  "email": "string",
  "password": "string"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "string",
  "password": "string"
}
```

### Product Endpoints

#### Get All Products
```http
GET /api/product
Authorization: Bearer {token}
```

#### Get Product by ID
```http
GET /api/product/{id}
Authorization: Bearer {token}
```

#### Create Product
```http
POST /api/product
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "string",
  "description": "string",
  "price": 0,
  "quantity": 0,
  "categoryId": "guid",
  "supplierId": "guid"
}
```

### Category Endpoints

#### Get All Categories
```http
GET /api/category
Authorization: Bearer {token}
```

#### Create Category
```http
POST /api/category
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "string",
  "description": "string"
}
```

### Supplier Endpoints

#### Get All Suppliers
```http
GET /api/supplier
Authorization: Bearer {token}
```

#### Create Supplier
```http
POST /api/supplier
Authorization: Bearer {token}
Content-Type: application/json

{
  "name": "string",
  "contactPerson": "string",
  "email": "string",
  "phone": "string",
  "address": "string"
}
```

## 🔒 Security

- All sensitive data is stored in environment variables or `appsettings.Development.json`
- JWT tokens are used for authentication
- Passwords are hashed using secure algorithms
- Role-based access control for sensitive operations
- HTTPS is enforced in production

## 🧪 Testing

The API includes comprehensive testing:
1. Unit tests for business logic
2. Integration tests for API endpoints
3. Authentication and authorization tests

To run tests:
```bash
dotnet test
```

## 📦 Deployment

### Production Deployment
1. Set up environment variables for sensitive data
2. Configure production database connection
3. Enable HTTPS
4. Set up proper logging
5. Configure CORS if needed

### Docker Deployment
```bash
docker build -t inventory-api .
docker run -p 8080:80 inventory-api
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- Sahaj2310 - Initial work

## 🙏 Acknowledgments

- ASP.NET Core team
- Entity Framework Core team
- All contributors and supporters

## 📞 Support

For support, please open an issue in the GitHub repository or contact the maintainers.

## ⚠️ Environment Variables for Secrets

**Important:** For security, do not store sensitive information (like JWT keys or database connection strings) in your `appsettings.json` or commit them to source control. Instead, use environment variables.

### Setting Environment Variables

#### On Windows (PowerShell):
```powershell
$env:ConnectionStrings__DefaultConnection="Server=localhost;Database=Myappdb;Trusted_Connection=True;Encrypt=False"
$env:Jwt__Key="your-very-secret-key"
$env:Jwt__Issuer="https://localhost:7186/"
$env:Jwt__Audience="https://localhost:7186/"
```

#### On Linux/macOS (bash):
```bash
export ConnectionStrings__DefaultConnection="Server=localhost;Database=Myappdb;Trusted_Connection=True;Encrypt=False"
export Jwt__Key="your-very-secret-key"
export Jwt__Issuer="https://localhost:7186/"
export Jwt__Audience="https://localhost:7186/"
```

- The double underscore `__` is used to represent nested configuration keys.
- These environment variables will override values in `appsettings.json` and `appsettings.Development.json`.

### Local Development
- You can use a tool like [dotnet user-secrets](https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets) for local development:
  ```bash
  dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=Myappdb;Trusted_Connection=True;Encrypt=False"
  dotnet user-secrets set "Jwt:Key" "your-very-secret-key"
  dotnet user-secrets set "Jwt:Issuer" "https://localhost:7186/"
  dotnet user-secrets set "Jwt:Audience" "https://localhost:7186/"
  ```
- Never commit your secrets to source control! 