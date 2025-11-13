# 👨‍💼 Simple Employee Management Curd (Clean Architecture - ASP.NET Core MVC)

This project is a simple yet complete **Employee Management System** built using **ASP.NET Core MVC**, following the principles of **Clean Architecture**.  
It demonstrates a real-world CRUD application with **server-side pagination, sorting, and searching**, along with layered architecture and manual DTO–Entity mapping (no AutoMapper).

---

## 🏗️ Project Overview

The system manages employees, their addresses, departments, and roles.  
Each employee belongs to one department and one role.

It is designed with **separation of concerns** in mind:
- **Data Layer (Infrastructure):** Handles database access using **Entity Framework Core**.
- **Application Layer:** Contains business logic and services.
- **Presentation Layer (Web):** ASP.NET Core MVC application with **Bootstrap + jQuery AJAX** frontend.

---

## ⚙️ Key Features

✅ Full CRUD (Create, Read, Update, Delete) operations for:
- Employee  
- Department  
- Role  
- Address

✅ Server-Side Processing:
- **Pagination**
- **Searching**
- **Sorting**

✅ Clean Architecture:
- Repository pattern for data access  
- Service layer for business logic  
- Controller for presentation  
- Manual DTO <-> Entity mapping (no AutoMapper)

✅ Tech Stack:
- ASP.NET Core MVC (.NET 6 / 7 / 8)
- Entity Framework Core
- SQL Server
- jQuery AJAX
- Bootstrap 5
- DataTables (for grid UI)
- Custom Mapping Classes

---

## 🧩 Architecture Layers

```
Solution
│
├── 📁 Application        --> Service layer (business logic, DTOs, interfaces)
│
├── 📁 Domain             --> Entity classes (Employee, Department, Role, Address)
│
├── 📁 Infrastructure     --> Data layer (EF Core DbContext, repositories)
│
├── 📁 WebUI              --> MVC project (controllers, views, JS, CSS)
│
└── EmployeeManagement.sln
```

---

## 🗄️ Database Design

### Tables
- **Employee**
  - Id (PK)
  - Name
  - Email
  - DepartmentId (FK)
  - RoleId (FK)
  - AddressId (FK)
  - DateOfBirth
  - CreatedDate
  - IsActive

- **Department**
  - Id (PK)
  - Name
  - Description

- **Role**
  - Id (PK)
  - Name
  - Description

- **Address**
  - Id (PK)
  - Country
  - State
  - City
  - Pincode

### Relationships
- Employee → Department (Many-to-One)
- Employee → Role (Many-to-One)
- Employee → Address (One-to-One)

---

## 🔄 Data Flow (Front-End ↔ Back-End)

1. User interacts with UI (Bootstrap form / DataTable)
2. jQuery AJAX sends data to Controller action (e.g., `/Employee/GetAll`)
3. Controller calls Service Layer
4. Service Layer interacts with Repository in Data Layer
5. Repository fetches data via EF Core
6. DTOs are returned → Controller → JSON → AJAX response → DataTable

---

## 💻 Technologies Used

| Layer | Technology |
|-------|-------------|
| Presentation | ASP.NET Core MVC, Bootstrap, jQuery, AJAX |
| Application | C#, DTOs, Services |
| Data | EF Core, Repository Pattern |
| Database | SQL Server |
| Mapping | Manual Mapping (No AutoMapper) |

---

## 📊 Server-Side DataTable Features

Implemented using `DataTables` + jQuery AJAX:

- `search` → filters records on server
- `order` → applies sorting on columns
- `start` + `length` → used for pagination
- Controller sends parameters → Service → Repository (with `Skip()` and `Take()` in LINQ)

Example LINQ:
```csharp
list = list.Skip(request.Start).Take(request.Length);
```

---

## 🔧 How to Run the Project

### 1️⃣ Clone Repository
```bash
git clone https://github.com/YourUsername/EmployeeManagementSystem.git
```

### 2️⃣ Update Database Connection
In `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3️⃣ Apply Migrations
```bash
cd Infrastructure
dotnet ef database update
```

### 4️⃣ Run the Application
```bash
cd WebUI
dotnet run
```
Open your browser → `https://localhost:5001`

---

## 🧠 Custom Mapping Example
Instead of using AutoMapper, mapping is handled manually.

```csharp
public static Employee ToEntity(this EmployeeDto dto)
{
    return new Employee
    {
        Id = dto.Id,
        Name = dto.Name,
        DepartmentId = dto.DepartmentId,
        RoleId = dto.RoleId,
        AddressId = dto.AddressId,
        DateOfBirth = dto.DateOfBirth,
        IsActive = dto.IsActive
    };
}

public static EmployeeDto ToDto(this Employee entity)
{
    return new EmployeeDto
    {
        Id = entity.Id,
        Name = entity.Name,
        DepartmentName = entity.Department?.Name,
        RoleName = entity.Role?.Name,
        DateOfBirth = entity.DateOfBirth,
        IsActive = entity.IsActive
    };
}
```

---

## 📦 Repository Pattern Example

```csharp
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
        => await _dbSet.FirstOrDefaultAsync(filter);

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
```

---

## 🧑‍💻 Author

**Jaimin Patel**  
💼 ASP.NET Core Developer | 💻 Front-End Enthusiast  
📧 Email: pateljaimin047@gmail.com 
🌐 GitHub: [https://github.com/jaiminpatel047]

---

## 🪪 License
This project is licensed under the [MIT License](LICENSE).

---

## 🌟 Future Enhancements
- Add authentication (Login/Register)
- Role-based access control
- Export to Excel / PDF
- Advanced filtering in DataTable
- Unit testing for service layer
