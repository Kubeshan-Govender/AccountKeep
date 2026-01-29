AccountKeep

AccountKeep is a web-based application for managing financial accounts and transactions. It allows users to:

Create and manage persons and their accounts

Record, edit, and delete transactions

View account details and transaction history

Securely log in and access account information

Features

User Management: Add, edit, and manage persons.

Account Management: Open or close accounts, view balances.

Transaction Management: Add, edit, delete transactions with automatic balance updates.

Responsive UI: Built with Bootstrap, optimized for desktops and tablets.

Secure Login: Basic login functionality with username/password.

Getting Started

These instructions will help you set up the project on your local machine.

Prerequisites

Visual Studio 2022
 or later

.NET 8 SDK

SQL Server

(Optional) Git

Setup

Clone the repository:

git clone https://github.com/YourUsername/AccountKeep.git


Open in Visual Studio:

Open AccountKeep.sln.

Configure the Database:

Update the connection string in appsettings.json to point to your SQL Server instance.

Ensure the database exists, or use the provided scripts in /DatabaseScripts to create tables and sample data.

Build and Run:

Press F5 in Visual Studio or run:

dotnet run


The app should open in your default browser.

Folder Structure

/Controllers – ASP.NET Core MVC controllers

/Models – Data models (Account, Person, Transaction, User)

/Views – Razor views for the UI

/wwwroot – CSS, JS, images

/Services – Business logic and data access services

/Interfaces – Service interfaces for dependency injection

Notes

Bootstrap & Icons: The app uses Bootstrap 5.3 and Bootstrap Icons for styling.

Backgrounds & Themes: Home, About, and Login pages have dedicated background images and overlays.

Database Initialization: Use the SQL script in /DatabaseScripts to populate sample accounts, persons, and transactions.
