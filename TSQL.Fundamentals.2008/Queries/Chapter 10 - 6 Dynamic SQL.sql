USE TSQLFundamentals2008;
GO

/*
DYNAMIC SQL
* Automating administrative tasks
* Improving performance of certain tasks
* Constructing elements of the code based on querying the actual data
*/

/*EXEC*/

DECLARE @sql AS VARCHAR(100);
SET @sql = 'PRINT ''This message was printed by a dynamic SQL batch.'';';
EXEC(@sql);
GO

DECLARE @sql1 AS NVARCHAR(300),
		@schemaname1 AS sysname,
		@tablename1 AS sysname;
		
DECLARE C CURSOR FAST_FORWARD FOR 
SELECT TABLE_SCHEMA,TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE='BASE TABLE';

OPEN C

FETCH NEXT FROM C INTO @schemaname1, @tablename1;

WHILE @@FETCH_STATUS = 0 
BEGIN
	SET @sql1 = N'EXEC sp_spaceused N'''
	+ QUOTENAME(@schemaname1) + N'.'
	+ QUOTENAME(@tablename1) + N''';';
	
	PRINT (@sql1);
	EXEC(@sql1);
	
	FETCH NEXT FROM C INTO @schemaname1, @tablename1;
END

CLOSE C;

DEALLOCATE C;

/*SP_EXECUTESQL
Supports I/O Parameters
Supports only unicode characters.
The fact that you can use input and output parameters in your 
dynamic SQL code can help you write more secure and more 
efficient code. In terms of security, parameters that appear 
in the code cannot be considered part of the code—they can 
only be considered operands in expressions. So by using 
parameters, you can remove exposure to SQL injection.

Performs better as it reuses execution plan.
*/
DECLARE @query AS NVARCHAR(300);
SET @query = N'SELECT orderid, orderdate,custid,empid
FROM Sales.Orders
WHERE orderid=@orderid';

EXEC sp_executesql
@stmt = @query,
@params = N'@orderid AS INT',
@orderid = 10248;

/*SP_EXECUTESQL With OUTPUT params*/
DECLARE @Counts TABLE
(
  schemaname sysname NOT NULL,
  tablename sysname NOT NULL,
  numrows INT NOT NULL,
  PRIMARY KEY(schemaname, tablename)
);

DECLARE
  @sql2 AS NVARCHAR(350),
  @schemaname2 AS sysname,
  @tablename2  AS sysname,
  @numrows    AS INT;

DECLARE C CURSOR FAST_FORWARD FOR
  SELECT TABLE_SCHEMA, TABLE_NAME
  FROM INFORMATION_SCHEMA.TABLES;

OPEN C

FETCH NEXT FROM C INTO @schemaname2, @tablename2;

WHILE @@fetch_status = 0
BEGIN
  SET @sql2 =
    N'SET @n = (SELECT COUNT(*) FROM '
    + QUOTENAME(@schemaname2) + N'.'
    + QUOTENAME(@tablename2) + N');';

  EXEC sp_executesql
    @stmt = @sql2,
    @params = N'@n AS INT OUTPUT',
    @n = @numrows OUTPUT;

  INSERT INTO @Counts(schemaname, tablename, numrows)
    VALUES(@schemaname2, @tablename2, @numrows);
  FETCH NEXT FROM C INTO @schemaname2, @tablename2;
END

CLOSE C;

DEALLOCATE C;

SELECT schemaname, tablename, numrows
FROM @Counts;

/*Using PIVOT with Dynamic SQL*/
USE TSQLFundamentals2008;
GO

SELECT *
FROM (SELECT shipperid, YEAR(orderdate) AS orderyear, freight
      FROM Sales.Orders) AS D
  PIVOT(SUM(freight) FOR orderyear IN([2006],[2007],[2008])) AS P
   ORDER BY shipperid;
/*Using CURSOR to fetch the order years and dynamically construct the SQL.*/   
DECLARE
	@sql3 AS NVARCHAR(300),
	@orderyear AS INT,
	@first AS INT;

DECLARE C CURSOR FAST_FORWARD FOR
	SELECT DISTINCT(YEAR(orderdate))
	FROM Sales.Orders
	ORDER BY YEAR(orderdate);

SET @first = 1;
SET @sql3 = N'SELECT *
FROM (SELECT shipperid, YEAR(orderdate) AS orderyear, freight
      FROM Sales.Orders) AS D
  PIVOT(SUM(freight) FOR orderyear IN(';

OPEN C;
FETCH NEXT FROM C INTO @orderyear;
WHILE @@fetch_status = 0
BEGIN
	IF @first=0
		SET @sql3 = @sql3 + N',';
	ELSE
		SET @first = 0;
	SET @sql3 = @sql3 + QUOTENAME(@orderyear);
	FETCH NEXT FROM C INTO @orderyear;
END
CLOSE C;
DEALLOCATE C;
SET @sql3 = @sql3 + N')) AS P ORDER BY shipperid;';
EXEC sp_executesql @stmt = @sql3;