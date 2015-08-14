USE TSQLFundamentals2008;
GO

/*USER DEFINED FUNCTIONS*/
/*
Scalar User Defined Functions
Table Valued User Defined Functions

UDF are not allowed to have side effects.
Hence they are not allowed to make schema changes, 
data changes. They cannot call two functions RAND and NEWID
as these functions have side effects.
*/

IF OBJECT_ID('dbo.fn_age') IS NOT NULL
	DROP FUNCTION dbo.fn_age;
GO

CREATE FUNCTION dbo.fn_age
(
	@birthdate AS DATETIME,
	@eventdate AS DATETIME
)
RETURNS INT
AS
BEGIN
	RETURN DATEDIFF(YEAR, @birthdate, @eventdate)
			- CASE WHEN 100 * MONTH(@eventdate)+DAY(@eventdate)
						< 100 * MONTH(@birthdate) + DAY(@birthdate)
					THEN 1 ELSE 0
			  END
END
GO

SELECT
  empid, firstname, lastname, birthdate,
  dbo.fn_age(birthdate, CURRENT_TIMESTAMP) AS age
FROM HR.Employees;

/*STORED PROCEDURES*/
/*	* Server side routines that encapsulate T-SQL code.
	* Has I/O Parameters. Can invoke code with side effects.
	* Not only modify data using SPs but also make schema changes.
	* If a change is applied to SP it is applicable to all users.
	* Gives better control of security. Give access to a SP without
	  giving direct access to the table to perform the operation.
	  Eg. Perform delete using a SP and not give delete permission to user.
	* We can incorporate error handling inside the stored procedure.
	* Better performance due to execution plan.
	* Reduced network traffic from the client as we need to pass only the name.
*/

USE TSQLFundamentals2008;
GO

IF OBJECT_ID('Sales.usp_GetCustomerOrders') IS NOT NULL
	DROP PROCEDURE Sales.usp_GetCustomerOrders;
GO
CREATE PROCEDURE Sales.usp_GetCustomerOrders
	@custid AS INT,
	@fromdate AS DATETIME = '19000101',
	@todate AS DATETIME = '99991231',
	@numrows AS INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	SELECT orderid, custid, empid, orderdate
	FROM Sales.Orders
	WHERE custid=@custid
	AND orderdate >= @fromdate
	AND orderdate < @todate;
	SET @numrows = @@rowcount;
	SET NOCOUNT OFF;
END
GO

/*Calling the stored procedure.*/
DECLARE @rc AS INT;

EXEC Sales.usp_GetCustomerOrders
	@custid=1,
	@fromdate='20070101',
	@todate='20080101',
	@numrows=@rc OUTPUT;
	
SELECT @rc AS numrows;

/*TRIGGERS*/
/*DDL Triggers and DML Triggers*/
/*A trigger is a special kind of stored procedure
 one that cannot be executed explicitly.
 Common Uses:
	1. Auditing
	2. Enforcing integrity rules
	3. Enforcing policies and so on.
 */

/*In the trigger's code, you can access tables called inserted 
and deleted that contain the rows that were affected by the 
modification that caused the trigger to fire. The inserted 
table holds the new image of the affected rows in the case 
of INSERT and UPDATE statements. The deleted table holds 
the old image of the affected rows in the case of DELETE 
and UPDATE statements. In the case of instead of triggers, 
the inserted and deleted tables contain the rows that were 
supposed to be affected by the modification that caused the 
trigger to fire.*/

/*DML Triggers*/
USE tempdb;

IF OBJECT_ID('dbo.T1_Audit') IS NOT NULL DROP TABLE dbo.T1_Audit;
IF OBJECT_ID('dbo.T1') IS NOT NULL DROP TABLE dbo.T1;

CREATE TABLE T1
(
	keycol	INT				NOT NULL PRIMARY KEY,
	datacol	VARCHAR(100)	NOT NULL
);

CREATE TABLE T1_Audit
(
	audit_lsn	INT			  NOT NULL	IDENTITY PRIMARY KEY,
	dt			DATETIME	  NOT NULL	DEFAULT(CURRENT_TIMESTAMP),
	login_name	sysname		  NOT NULL	DEFAULT(SUSER_SNAME()),
	keycol		int			  NOT NULL,
	datacol		VARCHAR(100) NOT NULL
)
GO

CREATE TRIGGER trg_T1_insert_audit ON dbo.T1 AFTER INSERT
AS
SET NOCOUNT ON;
INSERT INTO dbo.T1_Audit(keycol,datacol)
	SELECT keycol,datacol FROM inserted;
GO

INSERT INTO dbo.T1(keycol, datacol) VALUES(101, 'a');
INSERT INTO dbo.T1(keycol, datacol) VALUES(102, 'x');
INSERT INTO dbo.T1(keycol, datacol) VALUES(103, 'g');


SELECT audit_lsn,dt, login_name,keycol,datacol
FROM dbo.T1_Audit;
GO

/*DDL Triggers*/
/*Two scopes
1.Server scope
2.Database scope

SQL Server supports only after DDL triggers.
It doesn't support instead of DDL triggers.
Within the trigger you obtain information on the event that 
caused the trigger to fire by querying a function called 
EVENTDATA that returns the event info as an XML value. 
You can use XQuery expressions to extract event attributes 
such as post time, event type, login name, and others from 
the XML value.
*/

USE master;
IF DB_ID('testdb') IS NOT NULL DROP DATABASE testdb;
CREATE DATABASE testdb;
GO
USE testdb;
GO

IF OBJECT_ID('dbo.AuditDDLEvents', 'U') IS NOT NULL
  DROP TABLE dbo.AuditDDLEvents;

CREATE TABLE dbo.AuditDDLEvents
(
  audit_lsn        INT      NOT NULL IDENTITY,
  posttime         DATETIME NOT NULL,
  eventtype        sysname  NOT NULL,
  loginname        sysname  NOT NULL,
  schemaname       sysname  NOT NULL,
  objectname       sysname  NOT NULL,
  targetobjectname sysname  NULL,
  eventdata        XML      NOT NULL,
  CONSTRAINT PK_AuditDDLEvents PRIMARY KEY(audit_lsn)
);
GO

CREATE TRIGGER trg_audit_ddl_events
	ON DATABASE FOR DDL_DATABASE_LEVEL_EVENTS
AS
SET NOCOUNT ON;

DECLARE @eventdata as XML;
SET @eventdata = EVENTDATA();

INSERT INTO dbo.AuditDDLEvents 
	(posttime, eventtype, loginname, schemaname,
	 objectname, targetobjectname, eventdata)
VALUES
(
	@eventdata.value('(/EVENT_INSTANCE/PostTime)[1]',          'VARCHAR(23)'),
	@eventdata.value('(/EVENT_INSTANCE/EventType)[1]',         'sysname'),
	@eventdata.value('(/EVENT_INSTANCE/LoginName)[1]',         'sysname'),
	@eventdata.value('(/EVENT_INSTANCE/SchemaName)[1]',        'sysname'),
	@eventdata.value('(/EVENT_INSTANCE/ObjectName)[1]',        'sysname'),
	@eventdata.value('(/EVENT_INSTANCE/TargetObjectName)[1]',  'sysname'),
	@eventdata
);
GO
IF OBJECT_ID('dbo.T1') IS NOT NULL DROP TABLE dbo.T1;
GO
CREATE TABLE dbo.T1(col1 INT NOT NULL PRIMARY KEY);
ALTER TABLE dbo.T1 ADD col2 INT NULL;
ALTER TABLE dbo.T1 ALTER COLUMN col2 INT NOT NULL;
CREATE NONCLUSTERED INDEX idx1 ON dbo.T1(col2);
GO

SELECT * FROM dbo.AuditDDLEvents;
GO

USE master;
IF DB_ID('testdb') IS NOT NULL DROP DATABASE testdb;
GO