
# 🌱 Agri-Energy Connect

**Agri-Energy Connect** is a web-based platform designed to bridge the gap between sustainable agriculture and green energy in South Africa. This MVC-based application enables farmers and energy experts to collaborate, share resources, list products, and drive innovation in eco-conscious farming.

---

## 🛠️ Getting Started

### Prerequisites

Ensure you have the following installed:

- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- .NET 9 SDK
- SQL Server Express / LocalDB
- Git (optional but recommended)

---

### 🔧 Setup Instructions

1. **Clone the Repository**

   ```bash
   git clone https://github.com/your-repo/agri-energy-connect.git
   cd agri-energy-connect
   ```

2. **Open the Solution**

   Open `AgriEnergyConnectt.sln` in Visual Studio 2022.

3. **Update Database**

   Open the **Package Manager Console** (Tools > NuGet Package Manager > PMC) and run:

   ```bash
   Update-Database
   ```

   This will apply the migrations and create the necessary schema, including Identity tables and the `Products` table.

4. **Run the Application**

   Hit `F5` or click **IIS Express** to run the project.

---

## 🔐 Seeded Accounts

When you first run the application, the following users and roles are automatically created:

### 👨‍💼 Admin
| Email              | Password     | Role  |
|-------------------|--------------|-------|
| `admin@aec.com`   | `Admin1234!` | Admin |

### 👩‍🌾 Farmers
| Email               | Password       | Role    |
|--------------------|----------------|---------|
| `farmer1@aec.com`  | `Farmer1234!`  | Farmer  |
| `farmer2@aec.com`  | `Farmer1234!`  | Farmer  |

### 🧑‍💼 Employees
| Email                | Password        | Role      |
|---------------------|-----------------|-----------|
| `employee1@aec.com` | `Employee1234!` | Employee  |
| `employee2@aec.com` | `Employee1234!` | Employee  |

All these accounts have their emails marked as confirmed.

---

## 📖 How to Use the Application

### 🔐 Authentication

- Users can register a new account via the **Register** page.
- An admin must assign a role and confirm the email before the new user can access role-specific features.

---

### 👨‍💼 Admin Features

- Navigate to **Manage Users** from the navigation bar.
- Assign roles: `Farmer`, `Employee`, or `Admin`.
- Confirm user emails.
- Delete unwanted users.

---

### 👩‍🌾 Farmer Features

Once logged in with a **Farmer** role:

- **My Products**: View a list of your submitted products.
- **Add Product**: Submit new products with a name, category, and production date.
- Products are linked to your account and visible in your profile.

---

### 🧑‍💼 Employee Features

Once logged in with an **Employee** role:

- **All Farmers**: View a list of all farmers.
  - Click "View Profile" to see a farmer's submitted products.
- **All Products**: View all products across all users.
  - Filter by category, name, or production date.
- **Add Farmer**: Register a new Farmer with `EmailConfirmed = true` by default.

---

## 🎨 UI Theme

This project uses a custom **Olive Grove** color scheme to reflect its connection to sustainable agriculture. The theme is loaded via `olivegrove.css`.

---

## 🛡️ Security Notes

- All sensitive routes are protected using ASP.NET Core’s `[Authorize]` attribute.
- Role-based access is implemented for Admin, Farmer, and Employee users.
- Email confirmation is required before access to protected content is granted.

---

## 🤝 Contributions

Contributions are welcome! Please open an issue or submit a pull request if you would like to:

- Add new features (e.g. messaging, farmer dashboards)
- Improve UI styling
- Expand testing coverage

---

## 📄 License

This project is for educational purposes only as part of a Portfolio of Evidence (POE) for software development training. Not licensed for production use.

---

## 📧 Contact

If you have any questions or run into issues, please reach out via your instructor or contact the original developer.

---
