# Band Catalogue

## Overview
Band Catalogue is a **C# ASP.NET MVC web application** that allows users to manage albums and songs. It includes **CRUD operations** for albums and songs, utilizing **Entity Framework Core** with a SQL database.

## Features
- View a list of Bands, albums and their songs
- View band, album and song details
- Add new bands, albums and songs
- Edit band, album and song details
- Delete bands, albums and songs with confirmation prompts
- Uses **ASP.NET Core MVC** for the UI
- Implements **RESTful operations**
- Uses **Entity Framework Core** for database interaction

## Technologies Used
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL lite Database**
- **C#**

## Getting Started

### 1. Clone the Repository
```sh
git clone https://github.com/jeremiah3643/music-catalogue.git
cd music-catalogue/band-catalogue
```

### 2. Initialize Database and Migrate
- **Ensure SQLite is installed**
- **Ensure EF Core Tools are installed**
```sh
dotnet tool install --global dotnet-ef
```
- **Create Initial Migration**
```sh
dotnet ef migrations add InitialCreate
```
- **Apply Migration**
```sh
dotnet ef database update
```
- **Run Program**
```sh
dotnet watch run
```

### 3. Using Application
- Home Page gives buttons for Bands and Albums
- Clicking either will take you to their respective pages
- Can add, edit, or delete bands, and albums
- Includes details page
- In the albums page you can go into details to view songs and add edit or delete those also
