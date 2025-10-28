# AutoServePro: Vehicle Service Management System 

## Project Title
**AutoServePro: Vehicle Service Management System**

---

## Description
AutoServePro is a modern, web-based Vehicle Service Management System designed to streamline the process of booking and managing vehicle service appointments. It supports a multi-role structure, allowing **Customers** to browse available services, book appointments, and view their service history, while providing a dedicated **Admin Dashboard** for system administrators to manage users, update appointment statuses, and maintain the list of available services.

### Key Features
* **Customer Registration & Login:** Secure authentication for customers.
* **Admin Login:** Dedicated access for system administrators.
* **Available Services List:** Customers can view all services and their prices on the homepage and dashboard.
* **Appointment Booking:** Customers can book services, specifying the vehicle and preferred date/time.
* **Customer Dashboard:** View appointments and easily book new services.
* **Admin Dashboard:**
    * View and manage registered users (Admin and Customer roles).
    * View and update the status of all appointments (e.g., from 'Pending' to 'Completed').
    * Add new services with descriptions and pricing.
* **Database:** Uses **SQLite Studio** for persistent data storage (Users, Services, and Appointments).

---

## Installation Steps
Since this is a .NET project, you'll need the following prerequisites installed on your system:

### Prerequisites
1.  **[.NET SDK](https://dotnet.microsoft.com/download):** Ensure you have the correct version of the .NET SDK installed (The version depends on your specific project configuration, but usually the latest stable version works well).
2.  **Code Editor:** A suitable IDE such as **Visual Studio** (recommended) or **Visual Studio Code**.
3.  **SQLite Studio:** While not required to run the application, it's used for viewing and managing the SQLite database file (`.db`).

### Project Setup
1.  **Clone the Repository:**
    ```bash
    git clone [Your Repository URL Here]
    cd AutoServePro
    ```
    *(Replace `[Your Repository URL Here]` with the actual link to your project's repository)*
2.  **Restore Dependencies:** Open a terminal in the project's root directory and run:
    ```bash
    dotnet restore
    ```
3.  **Database Migration (if applicable):** If your project uses Entity Framework Core migrations, run the following to ensure the database is created and up-to-date:
    ```bash
    dotnet ef database update
    ```
    *Note: The database file should be automatically generated (e.g., `autoserverpro.db`) in the project's data directory.*

---

## How to Run the Project

You can run the project using either your IDE or the command line.

### 1. Using Visual Studio (Recommended)
1.  Open the **`.sln`** file (Solution file) in Visual Studio.
2.  Select the **AutoServePro** project as the startup project.
3.  Press **F5** or the **"Start Debugging"** button (▶) to build and run the application. The application will launch in your default web browser.

### 2. Using Command Line
1.  Open a terminal in the project's main directory (where the `.csproj` file is located).
2.  Run the application using:
    ```bash
    dotnet run
    ```
3.  The console output will indicate the URL where the application is hosted.

### Initial Access Credentials
You can use the following credentials:

| Role | Email | Initial Password |
| :--- | :--- | :--- |
| **Admin** | `admin@autoservepro.com` | Admin123! |
 **Register** link on the home page.