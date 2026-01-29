# AccountKeep

**AccountKeep** is a web application for managing financial accounts and transactions.  
It allows users to create accounts, manage transactions, and view account details securely.

## Features

- Manage persons and their accounts  
- Add, edit, and delete transactions  
- Automatic account balance updates  
- Responsive UI built with Bootstrap  
- Login functionality for secure access  

## Setup Instructions

### Prerequisites

- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later  
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)  

### Steps to Run

1. **Clone the repository**  
   ```bash
   git clone https://github.com/YourUsername/AccountKeep.git
2. **Open the project**  
   Open AccountKeep.sln in Visual Studio.
   
3. **Configure the database**  
   Update the connection string in appsettings.json to your SQL Server instance.
   Run the SQL scripts in /DatabaseScripts to create tables and insert sample data.

3. **Run the application**  
   Press F5 in Visual Studio or run:
   ```bash
   dotnet run
4. **Navigate** 
   to https://localhost:5001 (or the port shown) in your browser.


## Folder Structure

/Controllers – MVC controllers
/Models – Data models (Account, Person, Transaction, User)
/Views – Razor views for the UI
/wwwroot – CSS, JS, and images
/Services – Business logic and data access
/Interfaces – Service interfaces

## Notes

Home, About, and Login pages have background images and overlay effects.

Uses Bootstrap 5.3 and Bootstrap Icons for styling.

Transaction rules ensure data consistency (no zero-amount or future transactions).
