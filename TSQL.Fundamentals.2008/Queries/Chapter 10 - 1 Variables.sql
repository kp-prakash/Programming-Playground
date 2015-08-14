USE TSQLFundamentals2008;

DECLARE @i AS INT;--Until SQL Server 2005 separate DECLARE and SET.
SET @i=10;

DECLARE @j AS INT = 100; -- Only allowed in SQL Server 2008.

--Value assigned to the variable can be from a scalar subquery.
DECLARE @name as NVarchar(30);
SET @name = (SELECT lastname + N', ' + firstname
				FROM HR.Employees
				WHERE empid=3)
SELECT @name AS [Full Name]

/*SQL Server also supports a nonstandard assignment SELECT statement, 
allowing you to query data and assign multiple values obtained from 
the same row to multiple variables using a single statement. */
DECLARE @FirstName AS NVarchar(20)
DECLARE @LastName AS NVarchar(20)
SELECT @firstName = firstname,
@LastName = lastname
FROM HR.Employees
WHERE empid=3

SELECT @LastName + N', '+@FirstName AS [Full Name]

/*The assignment SELECT has predictable behavior when exactly one row 
qualifies. However, note that if the query has more than one qualifying 
row, the code doesn't fail. The assignments take place per each qualifying 
row, and with each row accessed, the values from the current row overwrite 
the existing values in the variables. When the assignment SELECT finishes, 
the values in the variables are those from the last row that SQL Server 
happened to access. For example, the following assignment SELECT has two 
qualifying rows: */

DECLARE @empname as VARCHAR(61);

SELECT @empname=firstname + ',' + lastname
FROM HR.Employees
WHERE mgrid=2;

SELECT @empname as empname;

/*SET is safer as it complains of non scalar output.*/

/*DECLARE @empname1 AS NVARCHAR(61);
SET @empname1 = (SELECT firstname + N' ' + lastname
                FROM HR.Employees
                WHERE mgrid = 2);
SELECT @empname1 AS empname; */

/* Error message for above query.
Msg 512, Level 16, State 1, Line 3
Subquery returned more than 1 value. This is not permitted when 
the subquery follows =, !=, <, <= , >, >= or when the subquery 
is used as an expression.
*/