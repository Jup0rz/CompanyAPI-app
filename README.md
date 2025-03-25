## Project Overview

This project contains a basic **C# .NET Core 8 RESTful API**, designed using the **Code First** methodology. 

There is **no need to provide external scripts** for the database, as all data records are included in the migration files. Unit tests are written using **NUnit** and cover **all methods** in the controller.

The web client is built using **React** (latest version), ensuring a modern and efficient front-end experience.

### Future Work
- **TODO:** In the future, the goal is to extract the **Entity Framework** (EF) from the API and create a separate project dedicated solely to the database operations.

To get everything up and running smoothly, please follow the steps below to properly configure the front-end:

## Front-End Setup

### 1. Install Node.js
Ensure you have **Node.js** installed on your system. If you don't have it yet, download it from the official website:
- [Download Node.js](https://nodejs.org/)

### 2. Install npm (Node Package Manager) on Windows
To install **npm** on Windows, follow these steps:
- Unzip the **npm** archive where **Node.js** is installed. For detailed instructions, refer to the official documentation:
   - [npm Archive for Windows](https://nodejs.org/dist/npm/)
   - [npm GitHub Repository - Windows Install Guide](https://github.com/isaacs/npm#fancy-windows-install)

### 3. Running the Front-End
After setting up Node.js and npm, open a terminal in **VS Code** and run the following command to start the development server:
```bash
npm run dev
```
### 4. Check the Running Application
Once the development server is running, you should see the front-end being served. To confirm, open your browser and navigate to:

[http://localhost:5173](http://localhost:5173)

> **Note:** To consume data from the back-end, ensure that the API server is running first. The front-end relies on the API to fetch and display data.

### 5. Preview of the Interface
![image](https://github.com/user-attachments/assets/3d424e39-eab7-4a7e-801d-05087d80bbbf)
