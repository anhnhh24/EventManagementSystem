# Event Management System

A full-featured web application for managing events, built using **ASP.NET MVC**, **Entity Framework Core**, and **Bootstrap 5**.

---

## 📌 Features

- ✅ User Registration & Authentication
- 🎫 Event Creation & Management
- 🗓️ Event Browsing with Filters (Date, Location, Category)
- 📝 Registration & Feedback System
- 🔔 Notifications per User
- 💳 Payment Integration (including **VNPay**)
- 🧾 Admin Dashboard for Analytics & Control

---

## 🧱 Technologies Used

| Layer         | Technologies                                |
|--------------|---------------------------------------------|
| Frontend     | HTML, CSS, Bootstrap 5                      |
| Backend      | ASP.NET MVC (.NET 6+), Entity Framework Core|
| Database     | SQL Server                                   |
| Payment      | VNPay Integration (HTML demo)               |

---

## 🏁 Getting Started

### 1️⃣ Prerequisites

- [.NET 6 SDK+](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/)
- Internet (for CDN styles/scripts)

### 2️⃣ Setup Instructions
git clone https://github.com/yourusername/EventManagementSystem.git
cd EventManagementSystem

"ConnectionStrings": {
  "MyDatabase": "Server=YOUR_SERVER;Database=EventManagementDB;User Id=sa;Password=yourpassword;"
}

dotnet ef database update

dotnet run

