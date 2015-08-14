USE master;

--Creates a new database if it is not available
IF DB_ID('TESTDB') IS NOT NULL
	DROP DATABASE TESTDB;
CREATE DATABASE TESTDB;
	
USE TESTDB;

IF OBJECT_ID('dbo.Employees', 'U') IS NOT NULL
	DROP TABLE dbo.Employees;
--Creating tables
CREATE TABLE dbo.Employees
(
	EmpID INT NOT NULL,
	FirstName VARCHAR(30) NOT NULL,
	LastName VARCHAR(30) NOT NULL,
	HireDate DATE NOT NULL,
	MgrID int NULL,
	SSN VARCHAR(20) NOT NULL,
	Salary MONEY NOT NULL
);

--Primary Key Constraint
ALTER TABLE dbo.Employees
	ADD CONSTRAINT PK_Employees
	PRIMARY KEY(EmpID);

--Unique Constraint
ALTER TABLE dbo.Employees
	ADD CONSTRAINT UNQ_Employees_SSN
	UNIQUE(SSN);

--Orders Table
IF OBJECT_ID('ORDERS','U') IS NOT NULL
	DROP TABLE dbo.Orders;
CREATE TABLE dbo.Orders
(
	OrderID INT NOT NULL,
	EmpID INT NOT NULL,
	CustID INT NOT NULL,
	OrderTs DATETIME NOT NULL,
	Qty INT NOT NULL
);

--Primary Key
ALTER TABLE dbo.Orders
	ADD CONSTRAINT PK_Orders
	PRIMARY KEY(OrderId);

--Foreign Key
ALTER TABLE dbo.Orders
	ADD CONSTRAINT FK_Orders_Employees
	FOREIGN KEY(EmpID)
	REFERENCES dbo.Employees(EmpID)
	ON DELETE CASCADE
	ON UPDATE CASCADE;

--Foreign Key for MgrId
ALTER TABLE dbo.Employees
	ADD CONSTRAINT FK_Employees_Employees
	FOREIGN KEY(MgrID)
	REFERENCES dbo.Employees(EmpID)
	ON DELETE NO ACTION
	ON UPDATE NO ACTION;

--Check Constraint
ALTER TABLE dbo.Employees
	ADD CONSTRAINT CHK_Employees_Salary
	CHECK (Salary >0);

--Default Constraint
ALTER TABLE dbo.Orders
	ADD CONSTRAINT DFT_Orders_OrderTs
	DEFAULT(CURRENT_TIMESTAMP) FOR OrderTs;