# ProcureFlowX – Procurement Management System

ProcureFlowX is a web-based **Procurement Management System** developed using **ASP.NET MVC, C#, and SQL Server**. The application helps organizations manage suppliers, products, goods receipt processes, inventory-related operations, and sales analytics through a structured MVC-based architecture.

The project also includes **AJAX-based dynamic operations, Chart.js analytics, SQL Server stored procedures, transactions, and a custom file-based logging system**.

---

## 🚀 Features

### 🔹 Supplier Management

* Add suppliers
* Edit supplier information
* Soft delete suppliers
* View supplier details
* Search suppliers
* Active/Inactive supplier status
* Display active suppliers

### 🔹 Product Management

* Add products
* Edit product information
* Soft delete products
* Supplier-wise product mapping
* Manage product price
* Manage stock quantity
* Product activation/deactivation
* Display products with supplier information

### 🔹 Goods Receipt (GRN)

* Create Goods Receipt Notes (GRN)
* Select suppliers
* Load supplier-specific products dynamically
* Add multiple products to a GRN
* Store received quantity and unit rate
* Track GRN date and status
* View GRN details
* Update GRN information
* Delete GRNs
* Transaction-based GRN operations

### 📊 Sales Analytics Dashboard

The project includes a sales analytics dashboard with dynamic chart-based visualization.

Features include:

* Product vs. sales quantity bar chart
* Product distribution pie chart
* Monthly/Yearly selection
* Product dropdown filtering
* Live search filtering
* Dynamic Chart.js rendering
* Automatic graph scaling for larger datasets
* AJAX-based data loading

Sales analytics data is retrieved through the:

`sp_Supplier_Sales_Report`

procedure and exposed through the supplier analytics functionality.

### 📝 Logging System

A custom logging system is implemented to record application activity and errors.

Log files:

```text
Data/
├── Error.txt
└── Success.txt
```

Logging is integrated into:

* Controllers
* DAL layer

---

## 🧠 Technology Stack

| Layer              | Technology                       |
| ------------------ | -------------------------------- |
| Frontend           | HTML, CSS, Bootstrap, JavaScript |
| Backend            | ASP.NET MVC, C#                  |
| Database           | Microsoft SQL Server             |
| Database Access    | SQL Server Stored Procedures     |
| Charts             | Chart.js                         |
| Dynamic Operations | AJAX                             |
| Architecture       | MVC + 3-Layer Architecture       |
| Logging            | Custom File-Based Logger         |
| IDE                | Visual Studio                    |

---

## 🏗️ Architecture

ProcureFlowX follows an **MVC + 3-Layer design approach**.

```text
                ┌─────────────────────┐
                │       Browser       │
                │ HTML / CSS / JS     │
                └──────────┬──────────┘
                           │
                           ▼
                ┌─────────────────────┐
                │    Controllers      │
                │ ASP.NET MVC / C#    │
                └──────────┬──────────┘
                           │
                           ▼
                ┌─────────────────────┐
                │        DAL          │
                │ Data Access Layer   │
                └──────────┬──────────┘
                           │
                           ▼
                ┌─────────────────────┐
                │     SQL Server      │
                │ Tables / Procedures │
                └─────────────────────┘
```

The project separates application responsibilities into:

* **Controllers** – Handle application requests and business flow
* **DAL** – Handle database operations
* **Models** – Represent application data
* **Views** – Provide the user interface
* **Logger** – Handles application logging

---

## 📁 Project Structure

```text
ProcureFlowX/
│
├── Controllers/
│   ├── SupplierController.cs
│   ├── ProductController.cs
│   └── GoodsReceiptController.cs
│
├── DAL/
│   ├── SupplierDAL.cs
│   ├── ProductDAL.cs
│   └── GoodsReceiptDAL.cs
│
├── Models/
│   ├── SupplierModel.cs
│   ├── ProductModel.cs
│   ├── GoodsReceiptModel.cs
│   ├── GoodsReceiptItemModel.cs
│   └── SupplierSalesModel.cs
│
├── Views/
│   ├── Supplier/
│   ├── Product/
│   └── GoodsReceipt/
│
├── Log/
│   └── Logger.cs
│
├── Data/
│   ├── Error.txt
│   └── Success.txt
│
├── Content/
├── Scripts/
├── Web.config
├── Global.asax
├── Global.asax.cs
├── packages.config
├── ProcureFlowX.csproj
└── ProcureFlowX.sln
```

---

## 🗄️ Database

The application uses **Microsoft SQL Server**.

Main database entities include:

### Supplier

```text
SupplierMaster
```

Stores supplier information such as:

* Supplier ID
* Supplier Name
* Contact Number
* Email Address
* Supplier Address
* Active Status
* Created Date

### Product

```text
ProductCatalog
```

Stores:

* Product ID
* Supplier ID
* Product Name
* Unit Price
* Stock Quantity
* Active Status
* Created Date

### Goods Receipt

```text
GoodsReceiptHeader
GoodsReceiptLine
```

The GRN structure separates the receipt header information from individual receipt line items.

---

## 🔐 Database Stored Procedures

The project uses SQL Server stored procedures for database operations.

### Supplier Procedures

```text
sp_Supplier_Insert
sp_Supplier_Update
sp_Supplier_Delete
sp_Supplier_GetAll
sp_Supplier_GetById
sp_Supplier_Search
sp_Supplier_GetActive
```

### Product Procedures

```text
sp_Product_Insert
sp_Product_Update
sp_Product_Delete
sp_Product_GetAll
sp_Product_GetById
```

### GRN Procedures

```text
sp_GRN_Create
sp_GRN_GetAll
sp_GRN_GetDetails
sp_GRN_Update
sp_GRN_Delete
sp_GRN_GetProductsBySupplier
```

### Analytics Procedure

```text
sp_Supplier_Sales_Report
```

---

## 🔄 Transaction Handling

Important database operations use SQL Server transactions with `TRY...CATCH`.

Example operations include:

* Supplier insertion
* Supplier updates
* Supplier soft deletion
* Product insertion
* Product updates
* Product soft deletion
* GRN creation
* GRN updates
* GRN deletion

This helps ensure that related database operations are committed together or rolled back when an error occurs.

---

## 🔌 Database Connection

The database connection is configured in:

```text
Web.config
```

Example:

```xml
<connectionStrings>
  <add name="PRACTICE"
       connectionString="Data Source=.;Initial Catalog=ProcureFlowX;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

Before running the application, update the connection string according to your SQL Server environment.

---

## ⚙️ Database Setup

### 1. Install SQL Server

Install Microsoft SQL Server and SQL Server Management Studio (SSMS).

### 2. Create the Database

Create a database named:

```text
ProcureFlowX
```

### 3. Run the SQL Script

Execute the provided database SQL script in SQL Server Management Studio.

The script contains:

* Table creation
* Foreign keys
* Constraints
* Stored procedures
* User-defined table types
* GRN-related database operations
* Sales analytics procedure

### 4. Verify the Database

Verify that the required tables and stored procedures have been created successfully.

---

## ▶️ How to Run the Project

### Prerequisites

Make sure you have:

* Visual Studio
* .NET Framework compatible with the project
* Microsoft SQL Server
* SQL Server Management Studio
* Required NuGet packages

### Steps

1. Clone the repository.

```bash
git clone https://github.com/YOUR-USERNAME/ProcureFlowX.git
```

2. Open the solution:

```text
ProcureFlowX.sln
```

3. Restore NuGet packages if required.

4. Create/configure the SQL Server database.

5. Update the connection string in:

```text
Web.config
```

6. Build the solution.

7. Run the project using **IIS Express** from Visual Studio.

8. Open the local application URL provided by Visual Studio.

Example:

```text
https://localhost:xxxx/
```

> **Important:** This is an ASP.NET MVC web application. Run it through IIS Express/Visual Studio rather than attempting to execute `ProcureFlowX.dll` directly as a Windows executable.

---

## 📊 Sales Analytics

The analytics functionality uses AJAX to retrieve sales-related data and display it dynamically using Chart.js.

The supplier analytics functionality includes:

```text
Supplier
   │
   ▼
GetSalesData
   │
   ▼
AJAX Request
   │
   ▼
sp_Supplier_Sales_Report
   │
   ▼
Sales Data
   │
   ▼
Chart.js
   │
   ├── Bar Chart
   └── Pie Chart
```

The dashboard supports:

* Product selection
* Live filtering
* Dynamic chart rendering
* Product quantity visualization
* Monthly/Yearly selection

---

## 📝 Logging

ProcureFlowX contains a custom logger:

```text
Log/
└── Logger.cs
```

Application logs are stored under:

```text
Data/
├── Error.txt
└── Success.txt
```

The logger is used across the Controllers and DAL layer to help track application activity and errors.

---

## ⚠️ Known Limitations

The current version has the following limitations:

* No authentication/login system
* No role-based authorization
* File-based logging is not intended for large-scale production environments
* No separate API layer
* UI can be further enhanced
* Monthly/Yearly analytics logic can be improved
* No pagination for large datasets

---

## 🔮 Future Improvements

Possible future enhancements include:

* Add ASP.NET Identity authentication
* Add role-based authorization
* Convert the application to ASP.NET Core
* Implement Repository Pattern
* Implement Dependency Injection
* Add dashboard KPIs
* Add Excel/PDF report export
* Replace custom file logging with Serilog
* Add revenue analytics
* Improve analytics filtering
* Add pagination
* Improve UI/UX
* Add REST API support

---

## 🎯 Project Highlights

This project demonstrates practical implementation of:

* ASP.NET MVC
* C#
* MVC architecture
* 3-Layer architecture
* SQL Server
* Stored Procedures
* Transactions
* Foreign Keys
* User-Defined Table Types
* AJAX
* Chart.js
* Dynamic filtering
* Supplier management
* Product management
* Goods Receipt management
* Inventory-related operations
* Custom application logging

---

## 📌 Purpose of the Project

ProcureFlowX was developed as a practical procurement management application demonstrating how a business-oriented web application can combine:

```text
ASP.NET MVC
      +
C#
      +
SQL Server
      +
Stored Procedures
      +
AJAX
      +
Chart.js
      +
Custom Logging
```

into a structured procurement management solution.

---

## 👨‍💻 Author

**Purushottam Thakur**

**.NET Full Stack Developer**

Pune, India

---

## 📄 License

This project is available for learning, demonstration, and portfolio purposes.

If you use or modify this project, please provide appropriate attribution to the original author.

<img width="958" height="353" alt="A1" src="https://github.com/user-attachments/assets/a0dcd98c-9a89-48d2-ab56-114e2628f194" />
