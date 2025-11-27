-- 1. Setup Database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'hotel')
BEGIN
    CREATE DATABASE hotel;
END
GO

USE hotel;
GO

-- 2. Users Table (For Login Form)
IF OBJECT_ID('tblUser', 'U') IS NULL
CREATE TABLE tblUser (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    username NVARCHAR(50),
    password NVARCHAR(50)
);
-- Insert default admin
IF NOT EXISTS (SELECT * FROM tblUser)
INSERT INTO tblUser (username, password) VALUES ('admin', 'admin');

-- 3. Guests Table (For Guest Form)
IF OBJECT_ID('tblGuest', 'U') IS NULL
CREATE TABLE tblGuest (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    GuestFName NVARCHAR(50),
    GuestMName NVARCHAR(50),
    GuestLName NVARCHAR(50),
    GuestAddress NVARCHAR(100),
    GuestContactNumber NVARCHAR(20),
    GuestGender NVARCHAR(10),
    GuestEmail NVARCHAR(50),
    Status NVARCHAR(20),    -- 'Active'
    Remarks NVARCHAR(50)    -- 'Available', 'Checkin', 'Reserve'
);

-- 4. Rooms Table (For Room Form - UPDATED)
-- Added 'ID' as Primary Key and 'NoOfOccupancy' based on frmRoom.vb
IF OBJECT_ID('tblRoom', 'U') IS NULL
CREATE TABLE tblRoom (
    ID INT IDENTITY(1,1) PRIMARY KEY, 
    RoomNumber INT,           -- Used as a unique identifier in code logic
    RoomType NVARCHAR(50),
    RoomRate DECIMAL(18, 2),
    NoOfOccupancy INT,        -- Found in frmRoom.vb
    Status NVARCHAR(20)       -- 'Available', 'Occupied', 'Reserve'
);

-- 5. Employee Table (For Employee Form - NEW)
-- Found in frmEmployee.vb
IF OBJECT_ID('tblEmployee', 'U') IS NULL
CREATE TABLE tblEmployee (
    EmpID INT IDENTITY(1,1) PRIMARY KEY,
    FName NVARCHAR(50),
    LName NVARCHAR(50),
    Email NVARCHAR(100),
    Phone NVARCHAR(20),
    Address NVARCHAR(100),
    Status NVARCHAR(20)       -- e.g., 'Active'
);

-- 6. Discounts Table (For Discount Form - UPDATED)
-- Added 'Status' based on frmDiscount.vb
IF OBJECT_ID('tblDiscount', 'U') IS NULL
CREATE TABLE tblDiscount (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    DiscountType NVARCHAR(50),
    DiscountRate DECIMAL(5, 2),
    Status NVARCHAR(20)       -- Found in frmDiscount.vb
);
-- Insert default discounts if empty
IF NOT EXISTS (SELECT * FROM tblDiscount)
INSERT INTO tblDiscount (DiscountType, DiscountRate, Status) VALUES 
('None', 0.00, 'Active'),
('Senior Citizen', 0.20, 'Active');

-- 7. Transactions Table (For CheckIn & Reserve Forms - UPDATED)
-- Added 'ReservationDate' based on frmReserve.vb
IF OBJECT_ID('tblTransaction', 'U') IS NULL
CREATE TABLE tblTransaction (
    TransID INT IDENTITY(1,1) PRIMARY KEY,
    GuestID INT,
    RoomNum INT,
    CheckInDate DATE,
    CheckOutDate DATE,
    ReservationDate DATE,     -- Found in frmReserve.vb
    NoOfChild INT,
    NoOfAdult INT,
    AdvancePayment DECIMAL(18, 2),
    DiscountID INT,
    Remarks NVARCHAR(50),     -- 'Checkin', 'Checkout', 'Reserve', 'Cancelled'
    Status NVARCHAR(20)       -- 'Active'
);


USE hotel;
GO

-- =============================================
-- 1. Insert Login Users (for frmLogin.vb)
-- =============================================
INSERT INTO tblUser (username, password) VALUES 
('admin', 'admin'),
('reception', '12345'),
('manager', 'pass123');

-- =============================================
-- 2. Insert Guests (for frmGuest.vb)
-- Note: Emails must be gmail.com or hotmail.com as per code validation
-- =============================================
INSERT INTO tblGuest (GuestFName, GuestMName, GuestLName, GuestAddress, GuestContactNumber, GuestGender, GuestEmail, Status, Remarks) VALUES 
('John', 'D', 'Doe', '123 Main St, New York', '1234567890', 'Male', 'johndoe@gmail.com', 'Active', 'Available'),
('Jane', 'A', 'Smith', '456 Oak Ave, California', '0987654321', 'Female', 'janesmith@hotmail.com', 'Active', 'Available'),
('Michael', 'B', 'Jordan', '789 Pine Rd, Texas', '1122334455', 'Male', 'mikej@gmail.com', 'Active', 'Checkin'),
('Sarah', 'C', 'Connor', '321 Elm St, Florida', '5544332211', 'Female', 'sarahc@gmail.com', 'Active', 'Available');

-- =============================================
-- 3. Insert Employees (for frmEmployee.vb)
-- Note: Phone must be 10 digits
-- =============================================
INSERT INTO tblEmployee (FName, LName, Email, Phone, Address, Status) VALUES 
('Alice', 'Wonder', 'alice@gmail.com', '9988776655', 'Wonderland Blvd', 'Active'),
('Bob', 'Builder', 'bob@hotmail.com', '6677889900', 'Construction Site 1', 'Active'),
('Charlie', 'Chocolate', 'charlie@gmail.com', '5556667777', 'Factory Lane', 'On Leave');

-- =============================================
-- Verification
-- =============================================
SELECT * FROM tblUser;
SELECT * FROM tblGuest;
SELECT * FROM tblEmployee;