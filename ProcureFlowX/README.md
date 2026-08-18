# ProcureFlowX – Procurement Management System

ProcureFlowX is a web-based procurement management system developed using **ASP.NET MVC**, **C#**, and **SQL Server**.  
It allows organizations to manage suppliers, products, and goods receipt processes efficiently with real-time analytics.

---

# 🚀 Features

## 🔹 Supplier Management
- Add, Edit, Delete (Soft Delete)
- View Supplier Details
- Active/Inactive Status Handling

## 🔹 Product Management
- Supplier-wise product mapping
- Stock and price management
- Product activation control

## 🔹 Goods Receipt (GRN)
- Create GRN with multiple products
- Auto stock deduction
- Supplier-based product loading (AJAX)
- GRN tracking and updates

## 🔹 📊 Sales Analytics Dashboard (NEW 🔥)
- Bar Chart (Product vs Sales Quantity)
- Pie Chart (Product distribution)
- Monthly / Yearly filter
- Product dropdown filtering
- 🔍 Live search filtering (real-time)
- Dynamic chart updates using Chart.js
- Auto-scaling graph for large data

## 🔹 📝 Logging System
- Custom Logger implemented
- Logs stored in:
  - `Data/Error.txt`
  - `Data/Success.txt`
- Logs integrated in:
  - Controllers
  - DAL Layer

---

# 🧠 Tech Stack

| Layer        | Technology |
|-------------|-----------|
| Frontend    | HTML, CSS, Bootstrap, JavaScript |
| Backend     | ASP.NET MVC (C#) |
| Database    | SQL Server |
| Charts      | Chart.js |
| Architecture| MVC + 3 Layer (Controller + DAL + Model) |

---

# 📁 Project Structure

```plaintext
ProcureFlowX/
│
├── Controllers/
│   ├── SupplierController.cs
│   ├── ProductController.cs
│   ├── GoodsReceiptController.cs
│
├── DAL/
│   ├── SupplierDAL.cs
│   ├── ProductDAL.cs
│   ├── GoodsReceiptDAL.cs
│
├── Models/
│   ├── SupplierModel.cs
│   ├── ProductModel.cs
│   ├── GoodsReceiptModel.cs
│   ├── GoodsReceiptItemModel.cs
│   ├── SupplierSalesModel.cs   <-- (Analytics)
│
├── Views/
│   ├── Supplier/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   ├── Details.cshtml   <-- (Analytics Dashboard Added)
│   │
│   ├── Product/
│   ├── GoodsReceipt/
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
└── Global.asax


Database Setup
Open SQL Server Management Studio
Create database (e.g., ProcureFlowX)
Run SQL scripts (already provided)

⚠️ Make sure tables and stored procedures are created correctly

🔌 Database Connection

Update connection string in:

📄 Web.config

<connectionStrings>
  <add name="PRACTICE"
       connectionString="Data Source=.;Initial Catalog=ProcureFlowX;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>


▶️ How to Run Project
Open solution in Visual Studio
Restore NuGet packages (if needed)
Build solution
Run using IIS Express
Navigate to:
https://localhost:xxxx/


Sales Analytics (How it Works)
Data fetched via AJAX from:
/Supplier/GetSalesData
Uses:
Stored Procedure: sp_Supplier_Sales_Report
Chart.js for visualization
Supports:
Live filtering
Product selection
Dynamic rendering


⚠️ Known Issues / Limitations
❌ No authentication (Login system not implemented)
❌ No role-based access
❌ Logging is file-based (not scalable for production)
❌ No API layer (pure MVC)
❌ UI is basic (can be improved with React/Angular)
❌ Monthly/Yearly filter logic is same (can be enhanced)
❌ No pagination for large data


🔧 Future Improvements
Add Authentication (JWT / Identity)
Convert to ASP.NET Core
Implement Repository Pattern + DI
Add Dashboard with KPIs
Export reports (Excel/PDF)
Replace Logger with Serilog
Add Revenue Analytics (₹ instead of Qty)


This project demonstrates:

✔ MVC Architecture
✔ 3-Layer Design
✔ Stored Procedures & Transactions
✔ AJAX-based dynamic UI
✔ Chart.js Integration
✔ Logging implementation
✔ Real-time filtering (advanced feature)

📌 Conclusion

ProcureFlowX is a complete procurement solution showcasing backend logic, database integration, and modern frontend analytics features.

👨‍💻 Author

    Purushottam Thakur


📍 Pune, India
💼 .NET Full Stack Developer

---

Database code:-

CREATE TABLE SupplierMaster
(
    SupplierId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName VARCHAR(25) NOT NULL,
    ContactNumber CHAR(10) NOT NULL,
    EmailAddress VARCHAR(25) NOT NULL,
    SupplierAddress VARCHAR(30) NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedOn DATETIME DEFAULT GETDATE(),

    CONSTRAINT CK_Supplier_Contact 
        CHECK (ContactNumber NOT LIKE '%[^0-9]%'),

    CONSTRAINT CK_Supplier_Email 
        CHECK (EmailAddress LIKE '%@%.%')
)

SELECT * FROM SupplierMaster
DELETE FROM SupplierMaster
WHERE SupplierId = 1;

-- STORED PROCEDURES 
CREATE PROCEDURE sp_Supplier_Insert
(
    @SupplierName VARCHAR(25),
    @ContactNumber CHAR(10),
    @EmailAddress VARCHAR(25),
    @SupplierAddress VARCHAR(30)
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        INSERT INTO SupplierMaster
        (SupplierName, ContactNumber, EmailAddress, SupplierAddress)
        VALUES
        (@SupplierName, @ContactNumber, @EmailAddress, @SupplierAddress)

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- UPDATE SUPPLIER
CREATE PROCEDURE sp_Supplier_Update
(
    @SupplierId INT,
    @SupplierName VARCHAR(25),
    @ContactNumber CHAR(10),
    @EmailAddress VARCHAR(25),
    @SupplierAddress VARCHAR(30),
    @IsActive BIT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE SupplierMaster
        SET SupplierName = @SupplierName,
            ContactNumber = @ContactNumber,
            EmailAddress = @EmailAddress,
            SupplierAddress = @SupplierAddress,
            IsActive = @IsActive
        WHERE SupplierId = @SupplierId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- SOFT DELETE SUPPLIER
CREATE PROCEDURE sp_Supplier_Delete
(
    @SupplierId INT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE SupplierMaster
        SET IsActive = 0
        WHERE SupplierId = @SupplierId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- GET ALL SUPPLIERS
CREATE PROCEDURE sp_Supplier_GetAll
AS
BEGIN
    SELECT * FROM SupplierMaster
END
GO

-- GET SUPPLIER BY ID
CREATE PROCEDURE sp_Supplier_GetById
(
    @SupplierId INT
)
AS
BEGIN
    SELECT * FROM SupplierMaster
    WHERE SupplierId = @SupplierId
END
GO

-- SEARCH SUPPLIER
CREATE PROCEDURE sp_Supplier_Search
(
    @Keyword VARCHAR(25)
)
AS
BEGIN
    SELECT * FROM SupplierMaster
    WHERE SupplierName LIKE '%' + @Keyword + '%'
END
GO

CREATE PROCEDURE sp_Supplier_GetActive
AS
BEGIN
  SET NOCOUNT ON;
  SELECT 
	SupplierId,
	SupplierName
	FROM SupplierMaster
	WHERE IsActive = 1
	ORDER BY SupplierName
END
GO

----------------------------------------------------------------------------------

CREATE TABLE ProductCatalog
(
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierId INT NOT NULL,
    ProductName VARCHAR(25) NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    StockQty INT NOT NULL,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedOn DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_Product_Supplier
        FOREIGN KEY (SupplierId)
        REFERENCES SupplierMaster(SupplierId),

    CONSTRAINT CK_Product_Price CHECK (UnitPrice >= 0),
    CONSTRAINT CK_Product_Qty CHECK (StockQty >= 0)
)
GO

-- INSERT PRODUCT
CREATE PROCEDURE sp_Product_Insert
(
    @SupplierId INT,
    @ProductName VARCHAR(25),
    @UnitPrice DECIMAL(10,2),
    @StockQty INT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        INSERT INTO ProductCatalog
        (SupplierId, ProductName, UnitPrice, StockQty)
        VALUES
        (@SupplierId, @ProductName, @UnitPrice, @StockQty)

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- UPDATE PRODUCT
CREATE PROCEDURE sp_Product_Update
(
    @ProductId INT,
    @SupplierId INT,
    @ProductName VARCHAR(25),
    @UnitPrice DECIMAL(10,2),
    @StockQty INT,
    @IsActive BIT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE ProductCatalog
        SET SupplierId = @SupplierId,
            ProductName = @ProductName,
            UnitPrice = @UnitPrice,
            StockQty = @StockQty,
            IsActive = @IsActive
        WHERE ProductId = @ProductId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- SOFT DELETE PRODUCT
CREATE PROCEDURE sp_Product_Delete
(
    @ProductId INT
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE ProductCatalog
        SET IsActive = 0
        WHERE ProductId = @ProductId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- GET ALL PRODUCTS (WITH SUPPLIER NAME)
CREATE PROCEDURE sp_Product_GetAll
AS
BEGIN
    SELECT 
        P.ProductId,
        P.SupplierId,
        S.SupplierName,
        P.ProductName,
        P.UnitPrice,
        P.StockQty,
        P.IsActive
    FROM ProductCatalog P
    INNER JOIN SupplierMaster S
        ON P.SupplierId = S.SupplierId
END
GO

-- GET PRODUCT BY ID
CREATE PROCEDURE sp_Product_GetById
(
    @ProductId INT
)
AS
BEGIN
    SELECT * 
    FROM ProductCatalog
    WHERE ProductId = @ProductId
END
GO

------------------------------------------------------------------

-- HEADER TABLE
CREATE TABLE GoodsReceiptHeader
(
    GRNId INT IDENTITY(1,1) PRIMARY KEY,
    SupplierId INT NOT NULL,
    GRNDate DATE NOT NULL,
    GRNStatus VARCHAR(15) NOT NULL DEFAULT 'Pending',

    CONSTRAINT FK_GRN_Supplier
        FOREIGN KEY (SupplierId)
        REFERENCES SupplierMaster(SupplierId)
)
GO

-- DETAIL TABLE
CREATE TABLE GoodsReceiptLine
(
    GRNLineId INT IDENTITY(1,1) PRIMARY KEY,
    GRNId INT NOT NULL,
    ProductId INT NOT NULL,
    ReceivedQty INT NOT NULL,
    UnitRate DECIMAL(10,2) NOT NULL,

    CONSTRAINT FK_GRN_Header
        FOREIGN KEY (GRNId)
        REFERENCES GoodsReceiptHeader(GRNId),

    CONSTRAINT FK_GRN_Product
        FOREIGN KEY (ProductId)
        REFERENCES ProductCatalog(ProductId)
)
GO

-- USER DEFINED TABLE TYPE
CREATE TYPE GRNItemType AS TABLE
(
    ProductId INT,
    ReceivedQty INT,
    UnitRate DECIMAL(10,2)
)
GO

-- GET PRODUCTS BY SUPPLIER
CREATE OR ALTER PROCEDURE sp_GRN_GetProductsBySupplier
(
    @SupplierId INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ProductId, ProductName, StockQty, UnitPrice
    FROM ProductCatalog
    WHERE SupplierId = @SupplierId AND IsActive = 1
END
GO

-- CREATE GRN 
CREATE PROCEDURE sp_GRN_Create
(
    @SupplierId INT,
    @GRNDate DATE,
    @Items GRNItemType READONLY
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        DECLARE @GRNId INT

        INSERT INTO GoodsReceiptHeader (SupplierId, GRNDate)
        VALUES (@SupplierId, @GRNDate)

        SET @GRNId = SCOPE_IDENTITY()

        INSERT INTO GoodsReceiptLine
        (GRNId, ProductId, ReceivedQty, UnitRate)
        SELECT 
            @GRNId,
            ProductId,
            ReceivedQty,
            UnitRate
        FROM @Items

        UPDATE P
        SET StockQty = StockQty - I.ReceivedQty
        FROM ProductCatalog P
        INNER JOIN @Items I
            ON P.ProductId = I.ProductId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

-- GET ALL GRNs
CREATE PROCEDURE sp_GRN_GetAll
AS
BEGIN
    SELECT 
        G.GRNId,
        G.GRNDate,
        G.GRNStatus,
        S.SupplierName
    FROM GoodsReceiptHeader G
    INNER JOIN SupplierMaster S
        ON G.SupplierId = S.SupplierId
    ORDER BY G.GRNId DESC
END
GO

-- GET GRN DETAILS
CREATE PROCEDURE sp_GRN_GetDetails
(
    @GRNId INT
)
AS
BEGIN
    SELECT
        G.GRNId,
        G.GRNDate,
        G.GRNStatus,

        S.SupplierName,
        S.ContactNumber,
        S.SupplierAddress,

        P.ProductName,
        L.ReceivedQty,
        L.UnitRate,
        (L.ReceivedQty * L.UnitRate) AS LineTotal
    FROM GoodsReceiptHeader G
    INNER JOIN SupplierMaster S ON G.SupplierId = S.SupplierId
    INNER JOIN GoodsReceiptLine L ON G.GRNId = L.GRNId
    INNER JOIN ProductCatalog P ON L.ProductId = P.ProductId
    WHERE G.GRNId = @GRNId
END
GO

-- UPDATE GRN 
CREATE PROCEDURE sp_GRN_Update
(
    @GRNId INT,
    @GRNDate DATE,
    @GRNStatus VARCHAR(15)
)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION

        UPDATE GoodsReceiptHeader
        SET GRNDate = @GRNDate,
            GRNStatus = @GRNStatus
        WHERE GRNId = @GRNId

        COMMIT TRANSACTION
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION
        THROW
    END CATCH
END
GO

CREATE OR ALTER PROCEDURE sp_GRN_Delete
    @GRNId INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRANSACTION;

        -- 1️⃣ Delete child records first
        DELETE FROM GoodsReceiptLine
        WHERE GRNId = @GRNId;

        -- 2️⃣ Delete header record
        DELETE FROM GoodsReceiptHeader
        WHERE GRNId = @GRNId;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        THROW;
    END CATCH
END
GO

-------------------------------------------------------

CREATE TYPE GRNProductType AS TABLE
	(
	ProductId INT,
	ReceivedQty Int,
	UnitRate DECIMAL(10,2)
	);


CREATE PROCEDURE sp_Supplier_Sales_Report
    @SupplierId INT,
    @Type VARCHAR(10) -- 'Monthly' or 'Yearly'
AS
BEGIN
    IF (@Type = 'Monthly')
    BEGIN
        SELECT 
            P.ProductName,
            SUM(GI.ReceivedQty) AS TotalQty
        FROM GoodsReceiptItems GI
        JOIN Product P ON GI.ProductId = P.ProductId
        JOIN GoodsReceipt G ON GI.GRNId = G.GRNId
        WHERE P.SupplierId = @SupplierId
        GROUP BY P.ProductName
    END
    ELSE
    BEGIN
        SELECT 
            P.ProductName,
            SUM(GI.ReceivedQty) AS TotalQty
        FROM GoodsReceiptItems GI
        JOIN Product P ON GI.ProductId = P.ProductId
        JOIN GoodsReceipt G ON GI.GRNId = G.GRNId
        WHERE P.SupplierId = @SupplierId
        GROUP BY P.ProductName
    END
END

------------------------ Dsiplay ------------------------------------
-- Tables 
SELECT * FROM SupplierMaster;
SELECT * FROM ProductCatalog;
SELECT * FROM GoodsReceiptHeader;
SELECT * FROM GoodsReceiptLine;

EXEC sp_help 'GRNProductType';


-- Procedures 
-- 1. Supplier Table Procedures
EXEC sp_helptext 'sp_Supplier_Insert';
EXEC sp_helptext 'sp_Supplier_Update';
EXEC sp_helptext 'sp_Supplier_Delete';
EXEC sp_helptext 'sp_Supplier_GetAll';
EXEC sp_helptext 'sp_Supplier_GetById';
EXEC sp_helptext 'sp_Supplier_Search';
EXEC sp_helptext 'sp_Supplier_GetActive';

-- 2. Product Table Procedures
EXEC sp_helptext 'sp_Product_Insert';
EXEC sp_helptext 'sp_Product_Update';
EXEC sp_helptext 'sp_Product_Delete';
EXEC sp_helptext 'sp_Product_GetAll';
EXEC sp_helptext 'sp_Product_GetById';

-- 3. Goods Receipt (GRN) Header Procedures
EXEC sp_helptext 'sp_GRN_Create';
EXEC sp_helptext 'sp_GRN_GetAll';
EXEC sp_helptext 'sp_GRN_GetDetails';
EXEC sp_helptext 'sp_GRN_Update';
EXEC sp_helptext 'sp_GRN_Delete';
-- 4. Goods Receipt (GRN) Items / Line Procedures
EXEC sp_helptext 'sp_GRN_GetProductsBySupplier';
EXEC sp_help 'sp_Supplier_Sales_Report';
