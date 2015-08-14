/*Batches*/
/*A Batch as a unit of parsing.
A batch is a set of commands that are parsed and executed 
as a unit. If the parsing is successful, SQL Server will 
then attempt to execute the batch. In the event of a syntax 
error in the batch, the whole batch is not submitted to SQL 
Server for execution. For example, the following code has 
three batches, the second of which has a syntax error (FOM 
instead of FROM in the second query): */

--Valid Batch
PRINT 'FIRST BATCH';
USE TSQLFundamentals2008;
GO

--Invalid Batch
PRINT 'SECOND BATCH';
SELECT * FOM HR.Employees;
GO

--Valid Batch
PRINT 'THIRD BATCH';
SELECT * FROM HR.Employees;
GO

/*Batches and Variables
Variables declared in one batch cannot be referenced in 
another batch.*/

DECLARE @i AS INT;
SET @i = 10;
-- Succeeds
PRINT @i;
GO
-- Fails
PRINT @i;
GO
/*The following statements cannot be combined with other statements 
in the same batch: CREATE DEFAULT, CREATE FUNCTION, CREATE PROCEDURE, 
CREATE RULE, CREATE SCHEMA, CREATE TRIGGER, and CREATE VIEW. For example, 
the following code has an IF statement followed by a CREATE VIEW 
statement in the same batch and therefore is invalid: 
*/

IF OBJECT_ID('Sales.MyView', 'V') IS NOT NULL DROP VIEW Sales.MyView;
GO
-- Without this GO you will get the error mentioned below.
CREATE VIEW Sales.MyView
AS
SELECT YEAR(orderdate) AS orderyear, COUNT(*) AS numorders
FROM Sales.Orders
GROUP BY YEAR(orderdate);
GO

/*An attempt to run this code generates the following error: 

Msg 111, Level 15, State 1, Line 3
'CREATE VIEW' must be the first statement in a query batch.

To get around the problem, separate the IF and CREATE VIEW 
statements into different batches by adding a GO command 
after the IF statement. */

/*A Batch as a Unit of Resolution.*/

USE tempdb;
IF OBJECT_ID('dbo.T1','U') IS NOT NULL 
	DROP TABLE dbo.T1;
CREATE TABLE dbo.T1(COL1 integer);
ALTER TABLE dbo.T1 ADD COL2 INT, COL3 INT;
GO 
/*Separate DDL and DML statements and ensure that they are 
part of different batches.*/
SELECT COL1, COL2,COL3 FROM DBO.T1;

/*The GO n Option*/
USE tempdb;
IF OBJECT_ID('dbo.T1','U') IS NOT NULL
	DROP TABLE dbo.T1;

CREATE TABLE dbo.T1 (COL1 INT IDENTITY);
GO
SET NOCOUNT ON;	
/*Code to suppress the default output produced by 
DML statements indicating how many rows were affected.*/
INSERT INTO dbo.T1 DEFAULT VALUES;
GO 1000 --Remember that GO is a client side command and not a T-SQL 
--Server side command.
SET NOCOUNT OFF;
SELECT * FROM dbo.T1;


