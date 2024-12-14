# 📝 Task Management System Documentation

## 🚀 Overview
The **Task Management System** is a robust and scalable application built using the latest technologies in the .NET ecosystem. Designed with an **N-Tier Architecture**, this project demonstrates a structured approach to user and task management, adhering to clean code principles and ensuring testability through comprehensive unit tests.

---

## ⚙️ Technologies Used

### Core Frameworks and Libraries
- **.NET 9** 🚀: The latest version of .NET, offering enhanced performance, minimal APIs, and modern features.
- **Entity Framework Core** 🛠️: ORM for managing database operations.
- **FluentValidation** 🛡️: For validation of business rules and user input.
- **xUnit** 🧪: A testing framework for writing unit tests.
- **Moq** 🔧: A library for mocking dependencies in unit tests.

### Database
- **In-Memory Database** 🗄️: Used for testing and demonstration purposes, leveraging Entity Framework Core’s in-memory provider.

### Tools
- **Rider IDE** 🌐: For streamlined development.
- **Git** 🛠️: Version control system for managing project history.
- **Swagger** 📢: For API documentation and testing.

---

## 🏗️ Project Architecture
The application adheres to **N-Tier Architecture**, ensuring separation of concerns and modularity:

1. **Domain Layer** 🌐:
   - Contains core business entities like `User` and `Task`.

2. **Data Layer** 🔧:
   - Manages database operations through `AppDbContext`.

3. **Business Layer** 🛠️:
   - Implements business logic through services like `TaskService` and `UserService`.

4. **API Layer** 📲:
   - Provides API endpoints for interacting with the system.

---

## 🌐 API Endpoints

### User Endpoints
- **POST /users**: Create a new user.
- **GET /users**: Retrieve all users.

### Task Endpoints
- **POST /tasks**: Create a new task and assign it to a user.
- **GET /tasks**: Retrieve all tasks.
- **GET /tasks/user/{userId}**: Retrieve tasks assigned to a specific user.
- **PUT /tasks/{taskId}/complete**: Mark a task as completed.

---


---

## 🧪 Testing and Validation
- **Unit Tests**: Written using **xUnit** to ensure functionality of services like `TaskService`.
- **Mocking**: **Moq** is used to mock dependencies in unit tests.
- **Validation**: **FluentValidation** ensures business rules are adhered to during API calls.

---

## 🚀 How to Run the Project

### 1️⃣ Clone the Repository
```bash
git clone https://github.com/username/task-management.git
cd task-management
```

### 2️⃣ Build the Project
```bash
dotnet build
```

### 3️⃣ Run the Application
```bash
dotnet run --project TaskManagement.API
```
The application will run on `http://localhost:5000`.

### 4️⃣ Run Tests
```bash
# Run all tests

dotnet test

# Run tests from a specific project

dotnet test ./TaskManagement.Tests/TaskManagement.Tests.csproj


```

---

## 💡 Key Highlights
- Built using **.NET 9**, showcasing modern development practices.
- Fully tested with over **10 unit tests**, covering core functionalities.
- Adheres to clean code principles for maintainability.
- **Swagger Integration** for easy API documentation and testing.

---

## 📜 Swagger API Testing

The **Task Management System** comes with **Swagger** integration for easy testing and visualization of API endpoints. With Swagger, you can test all available endpoints directly from your browser.
---
<img width="731" alt="Ekran Resmi 2024-12-14 04 38 35" src="https://github.com/user-attachments/assets/83b47c67-d4aa-43bf-88ca-818b9acc6f3c" />

<img width="725" alt="Ekran Resmi 2024-12-14 11 04 16" src="https://github.com/user-attachments/assets/7f0f8a81-623c-4c4d-a88e-7174e04ac871" />

 <img width="725" alt="Ekran Resmi 2024-12-14 11 03 54" src="https://github.com/user-attachments/assets/419ab5c4-c570-4f78-abd2-b45cd09ba4e7" />
<img width="735" alt="Ekran Resmi 2024-12-14 11 03 30" src="https://github.com/user-attachments/assets/1307924f-0514-4f1c-b5cc-822ca02a6aeb" />
<img width="727" alt="Ekran Resmi 2024-12-14 10 56 03" src="https://github.com/user-attachments/assets/61630ae2-828e-4b05-a27f-27c43708e71f" />
<img width="723" alt="Ekran Resmi 2024-12-14 10 55 50" src="https://github.com/user-attachments/assets/43ea45f9-906d-473f-89d2-62a0964f2fc6" />
 🙏 TTHANK YOU FOR REVIEWING MY PROJECT!
