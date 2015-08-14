/*Error Handling*/
BEGIN TRY
	PRINT 10/2;
	PRINT '10 / 2 = 5 , NO ERROR';
END TRY
BEGIN CATCH
	PRINT 'ERROR';
END CATCH

BEGIN TRY
	PRINT 10/0;
	PRINT 'NO ERROR';
END TRY
BEGIN CATCH
	PRINT '10/0 DIVIDE BY ZERO ERROR';
	PRINT ERROR_MESSAGE();
END CATCH

/*SQL gives error information with set of funcions
ERROR_NUMBER function returns an integer with the number of the error.
ERROR_MESSAGE function returns the error message text.
To get the list of error numbers query the catalog view sys.messages.
The ERROR_SEVERITY and ERROR_STATE functions return the error severity and state. 
The ERROR_LINE function returns the line number where the error happened. 
Finally, the ERROR_PROCEDURE returns the name of the procedure where the 
error happened, and returns NULL if the error did not happen within a procedure.
*/

USE tempdb;
GO
IF OBJECT_ID('dbo.Employees') IS NOT NULL
	DROP TABLE dbo.Employees;

CREATE TABLE dbo.Employees
(
	empid INT NOT NULL,
	empname NVARCHAR(30),
	mgrid INT,
	CONSTRAINT PK_EMPLOYEES_EMPID PRIMARY KEY(empid),
	CONSTRAINT CHK_EMPLOYEES_EMPID CHECK(empid > 0),
	CONSTRAINT FK_EMPLOYEES_EMPLOYEES 
	FOREIGN KEY(mgrid) REFERENCES dbo.Employees(empid)
)

BEGIN TRY
	INSERT INTO dbo.Employees(empid,empname,mgrid)
	VALUES (1,'emp1',NULL);--Also try with 0 and NULL for empid.
END TRY
BEGIN CATCH
	  IF ERROR_NUMBER() = 2627
  BEGIN
    PRINT '    Handling PK violation...';
  END
  ELSE IF ERROR_NUMBER() = 547
  BEGIN
    PRINT '    Handling CHECK/FK constraint violation...';
  END
  ELSE IF ERROR_NUMBER() = 515
  BEGIN
    PRINT '    Handling NULL violation...';
  END
  ELSE IF ERROR_NUMBER() = 245
  BEGIN
    PRINT '    Handling conversion error...';
  END
  ELSE
  BEGIN
    PRINT '    Handling unknown error...';
  END
  PRINT '    Error Number  : ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
  PRINT '    Error Message : ' + ERROR_MESSAGE();
  PRINT '    Error Severity: ' + CAST(ERROR_SEVERITY() AS VARCHAR(10));
  PRINT '    Error State   : ' + CAST(ERROR_STATE() AS VARCHAR(10));
  PRINT '    Error Line    : ' + CAST(ERROR_LINE() AS VARCHAR(10));
  PRINT '    Error Proc    : ' + COALESCE(ERROR_PROCEDURE(), 'Not within proc');
END CATCH

/*Using a Store Procedure to encapsulate error handling logic*/
IF OBJECT_ID('dbo.usp_err_messages', 'P') IS NOT NULL
  DROP PROC dbo.usp_err_messages;
GO

CREATE PROC dbo.usp_err_messages
AS
SET NOCOUNT ON;

IF ERROR_NUMBER() = 2627
BEGIN
  PRINT 'Handling PK violation...';
END
ELSE IF ERROR_NUMBER() = 547
BEGIN
  PRINT 'Handling CHECK/FK constraint violation...';
END
ELSE IF ERROR_NUMBER() = 515
BEGIN
  PRINT 'Handling NULL violation...';
END
ELSE IF ERROR_NUMBER() = 245
BEGIN
  PRINT 'Handling conversion error...';
END
ELSE
BEGIN
  PRINT 'Handling unknown error...';
END

PRINT 'Error Number  : ' + CAST(ERROR_NUMBER() AS VARCHAR(10));
PRINT 'Error Message : ' + ERROR_MESSAGE();
PRINT 'Error Severity: ' + CAST(ERROR_SEVERITY() AS VARCHAR(10));
PRINT 'Error State   : ' + CAST(ERROR_STATE() AS VARCHAR(10));
PRINT 'Error Line    : ' + CAST(ERROR_LINE() AS VARCHAR(10));
PRINT 'Error Proc    : ' + COALESCE(ERROR_PROCEDURE(), 'Not within proc');
GO

BEGIN TRY
  INSERT INTO dbo.Employees(empid, empname, mgrid)
    VALUES(1, 'Emp1', NULL);
END TRY
BEGIN CATCH
  EXEC dbo.usp_err_messages;
END CATCH

