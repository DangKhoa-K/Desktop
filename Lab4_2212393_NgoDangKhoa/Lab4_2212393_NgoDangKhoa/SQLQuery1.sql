CREATE DATABASE RestaurantManagement;
GO
USE RestaurantManagement;
GO
CREATE TABLE Category (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    [Type] INT NOT NULL -- 1: Thức ăn, 0: Đồ uống
);
CREATE TABLE Food (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Unit NVARCHAR(50),
    Price FLOAT CHECK (Price >= 0),
    CategoryID INT NOT NULL FOREIGN KEY REFERENCES Category(ID)
);
CREATE TABLE Account (
    UserName NVARCHAR(100) PRIMARY KEY,
    DisplayName NVARCHAR(100),
    Password NVARCHAR(100),
    IsActive BIT DEFAULT 1
);
CREATE TABLE Bill (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Account NVARCHAR(100) FOREIGN KEY REFERENCES Account(UserName),
    DateCheckIn DATETIME DEFAULT GETDATE(),
    DateCheckOut DATETIME,
    Discount FLOAT DEFAULT 0,
    Tax FLOAT DEFAULT 0,
    Total FLOAT DEFAULT 0,
    Status NVARCHAR(50)
);
CREATE TABLE BillDetails (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    BillID INT FOREIGN KEY REFERENCES Bill(ID),
    FoodID INT FOREIGN KEY REFERENCES Food(ID),
    Quantity INT CHECK (Quantity > 0),
    Price FLOAT,
    Total FLOAT
);
-- Category
INSERT INTO Category VALUES (N'Cơm', 1);
INSERT INTO Category VALUES (N'Canh', 1);
INSERT INTO Category VALUES (N'Nước ngọt', 0);

-- Food
INSERT INTO Food VALUES (N'Cơm gà', N'Phần', 35000, 1);
INSERT INTO Food VALUES (N'Cơm sườn', N'Phần', 40000, 1);
INSERT INTO Food VALUES (N'Canh chua cá', N'Tô', 30000, 2);
INSERT INTO Food VALUES (N'Pepsi', N'Chai', 15000, 3);
INSERT INTO Food VALUES (N'Coca-Cola', N'Chai', 15000, 3);

-- Account
INSERT INTO Account VALUES (N'admin', N'Quản trị viên', N'123', 1);
INSERT INTO Account VALUES (N'staff1', N'Nhân viên A', N'123', 1);
