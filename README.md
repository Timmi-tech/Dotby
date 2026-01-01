# 🛒 Dotby

A modern .NET ecommerce Web API built with ASP.NET Core, featuring JWT authentication, PostgreSQL database, and real-time communication with SignalR.

## ✨ Features

### 🛍️ Ecommerce Core
- **🛒 Shopping Cart** - Full cart management with add/remove/update functionality
- **📦 Product Management** - Complete CRUD operations with image upload
- **🏷️ Category System** - Organized product categorization
- **👤 User Profiles** - User management with profile customization
- **🖼️ Image Upload** - Cloudinary integration for product & category images

### 🔧 Technical Features
- **🔐 JWT Authentication** - Secure token-based authentication
- **🐘 PostgreSQL Database** - Robust data persistence
- **⚡ SignalR** - Real-time communication
- **📝 Serilog Logging** - Structured logging
- **🔄 AutoMapper** - Object-to-object mapping
- **📚 Swagger/OpenAPI** - Interactive API documentation
- **🐳 Docker Support** - Containerized deployment

## 🛠️ Tech Stack

- 🚀 .NET 8.0
- 🌐 ASP.NET Core Web API
- 🗄️ Entity Framework Core
- 🐘 PostgreSQL
- ⚡ SignalR
- ☁️ Cloudinary
- 📝 Serilog
- 🔄 AutoMapper
- 🔐 JWT Bearer Authentication

## 📁 Project Structure

```
Dotby/
├── src/
│   ├── API/              # Web API layer
│   ├── Application/      # Business logic and DTOs
│   ├── Domain/           # Domain entities and contracts
│   └── Infrastructure/   # Data access and external services
└── tests/                # Test projects
```

## 🚀 Getting Started

### 📋 Prerequisites

- 🔧 .NET 8.0 SDK
- 🐘 PostgreSQL
- 🐳 Docker (optional)

### ⚙️ Configuration

1. 📝 Create a `.env` file in the `src/API` directory with the following variables:

```env
JWT_SECRET=<your-jwt-secret>
DATABASE_CONNECTION=<your-postgres-connection-string>
CLOUDINARY_CLOUD_NAME=<your-cloudinary-cloud-name>
CLOUDINARY_API_KEY=<your-cloudinary-api-key>
CLOUDINARY_API_SECRET=<your-cloudinary-api-secret>
```

2. 🔧 Update `appsettings.json` if needed

### ▶️ Running the Application

**🖥️ Using .NET CLI:**

```bash
cd src/API
dotnet run
```

**🐳 Using Docker:**

```bash
docker-compose up
```

The API will be available at `https://localhost:5202` (or the configured port).

## 📚 API Documentation

🌐 **Live API Documentation:** https://dotby.onrender.com/swagger/index.html

For local development, access the Swagger UI at:

```
https://localhost:5202/swagger
```

## 🔗 API Endpoints

### 👤 Authentication
- `POST /api/authentication/register` - 📝 Register new user
- `POST /api/authentication/login` - 🔑 User login
- `POST /api/authentication/logout` - 🚪 User logout

### 📦 Products
- `GET /api/products` - 📋 Get all products
- `GET /api/products/{id}` - 🔍 Get product by ID
- `GET /api/products/category/{categoryId}` - 🏷️ Get products by category
- `POST /api/products` - ➕ Create new product (with image upload)

### 🏷️ Categories
- `GET /api/categories` - 📋 Get all categories
- `GET /api/categories/{id}` - 🔍 Get category by ID
- `POST /api/categories` - ➕ Create new category (with image upload)

### 🛍️ Shopping Cart
- `GET /api/cart/{userId}` - 👁️ Get user's cart
- `POST /api/cart/{userId}` - ➕ Add item to cart
- `PUT /api/cart/{userId}/{productId}` - ✏️ Update cart item quantity
- `DELETE /api/cart/{userId}/{productId}` - ➖ Remove item from cart
- `DELETE /api/cart/{userId}` - 🗑️ Clear entire cart

### 🖼️ Photo Management
- `POST /api/photo/upload` - 📷 Upload images to Cloudinary

