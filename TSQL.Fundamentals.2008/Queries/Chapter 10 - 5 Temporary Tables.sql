USE TSQLFundamentals2008;
GO

/*SQL Server supports three kinds of temporary tables that you 
may find more convenient to work with in such cases: 
1. Local temporary tables. 
2. Global temporary tables.
3. Table variables.*/

/*1. LOCAL TEMPORARY TABLES*/
/*Single # sign as prefix. Eg. #T1. Created in tempdb.*/
/*A local temporary table is visible only to the session that 
created it, in the creating level and all inner levels in the 
call stack (inner procedures, functions, triggers, and dynamic 
batches). A local temporary table is destroyed automatically 
by SQL Server when the creating level in the call stack gets 
out of scope.*/

IF OBJECT_ID('tempdb.dbo.#MyOrderTotalsByYear') IS NOT NULL
	DROP TABLE tempdb.dbo.#MyOrderTotalsByYear;
GO

SELECT 
	YEAR(O.orderdate) AS orderyear,
	SUM(OD.qty) AS qty
INTO #MyOrderTotalsByYear
FROM Sales.Orders O
	 JOIN Sales.OrderDetails OD
	 ON OD.orderid = O.orderid
GROUP BY YEAR(orderdate)
ORDER BY YEAR(orderdate);

SELECT cur.orderyear, cur.qty curqty, prev.qty prevqty
FROM #MyOrderTotalsByYear cur
LEFT OUTER JOIN #MyOrderTotalsByYear prev
ON cur.orderyear=prev.orderyear+1;
GO

/* RESULT
orderyear   curqty      prevqty
----------- ----------- -----------
2006        9581        NULL
2007        25489       9581
2008        16247       25489

(3 row(s) affected)
*/

/*
To verify that the local temporary table is visible only to the 
creating session, try accessing it from another session:

SELECT orderyear, qty FROM dbo.#MyOrderTotalsByYear;

You get the following error:
Msg 208, Level 16, State 0, Line 1
Invalid object name '#MyOrderTotalsByYear'.
*/


/*2. GLOBAL TEMPORARY TABLES.*/
/*When you create a global temporary table, it is visible 
to all other sessions. They are destroyed automatically by 
SQL Server when the creating session disconnects, and there 
are no active references to the table. You create a global 
temporary table by naming it with two number signs as a 
prefix, such as ##T1.*/

/*Global temporary tables are useful when you want to share 
temporary data with everyone. No special permissions are 
required and everyone has full DDL and DML access. OF COURSE, 
THE FACT THAT EVERYONE HAS FULL ACCESS MEANS THAT ANYONE CAN 
EVEN DROP THE TABLE, SO CONSIDER THE ALTERNATIVES CAREFULLY.*/

CREATE TABLE dbo.##GLOBALS
(
	id sysname NOT NULL PRIMARY KEY,
	val SQL_VARIANT NOT NULL
);

/*This table is supposed to mimic global variables that 
are not supported by SQL Server. The id column is of a 
sysname data type—the type SQL Server uses internally 
to represent identifiers, and the val column is of a 
SQL_VARIANT data type—a generic type that can store within 
it a value of almost any base type.
 
Anyone can insert rows into the table. For example, run the 
following code to insert a row representing a variable called 
i and initialize it with the integer value 10: */

INSERT INTO dbo.##Globals(id, val) VALUES(N'i', CAST(10 AS INT));
 /* Anyone can modify and retrieve data from the table. For example, 
 run the following code from any session to query the current value 
 of the variable i:  */
SELECT val FROM dbo.##Globals WHERE id = N'i';
 
/*This code returns the following output:
 
val
-----------
10*/
/* Keep in mind that as soon as the session that created the global
 temporary table disconnects and there are no active references to 
 the table, SQL Server automatically destroys it.

DROP the table excplicity if you don't want SQL server to do it.*/
DROP TABLE dbo.##Globals;

/*TABLE VARIABLES*/
/*As with local temporary tables, table variables have a physical 
presence as a table in the tempdb database, contrary to the COMMON 
MISCONCEPTION THAT THEY EXIST ONLY IN MEMORY. Like local temporary 
tables, table variables are VISIBLE ONLY TO THE CREATING SESSION, 
BUT HAVE A MORE LIMITED SCOPE, WHICH IS ONLY THE CURRENT BATCH. 
TABLE VARIABLES ARE VISIBLE NEITHER TO INNER BATCHES IN THE CALL 
STACK NOR TO SUBSEQUENT BATCHES IN THE SESSION.*/

/*If an explicit transaction is rolled back, changes made to temporary 
tables in that transaction are rolled back as well; however, changes 
made to table variables by statements that completed in the transaction 
aren't rolled back. Only changes made by the active statement that failed 
or was terminated before completion are undone.
 
Temporary tables and table variables also have optimization differences,
but those are outside the scope of this book. For now, I'll just say 
that in terms of PERFORMANCE, usually it MAKES MORE SENSE TO USE TABLE 
VARIABLES WITH VERY SMALL VOLUMES OF DATA (ONLY A FEW ROWS) AND TO USE 
LOCAL TEMPORARY TABLES OTHERWISE.*/

DECLARE @MyOrderTotalsByYear TABLE
(
	orderyear INT NOT NULL PRIMARY KEY,
	qty INT NOT NULL
);

INSERT INTO @MyOrderTotalsByYear (orderyear,qty)
SELECT YEAR(O.orderdate) AS orderyear,
    SUM(OD.qty) AS qty
  FROM Sales.Orders AS O
    JOIN Sales.OrderDetails AS OD
      ON OD.orderid = O.orderid
  GROUP BY YEAR(orderdate);

SELECT cur.orderyear,cur.qty,prev.qty 
FROM @MyOrderTotalsByYear cur
LEFT OUTER JOIN @MyOrderTotalsByYear prev
ON cur.orderyear = prev.orderyear + 1;

/*
orderyear   qty         qty
----------- ----------- -----------
2006        9581        NULL
2007        25489       9581
2008        16247       25489

(3 row(s) affected)
*/

/*TABLE TYPES*/
/*SQL Server 2008 introduces support for table types. By creating a 
table type you preserve a table definition in the database and can 
later reuse it as the table definition of table variables and input 
parameters of stored procedures and user-defined functions.*/

USE TSQLFundamentals2008;
IF TYPE_ID('dbo.OrderTotalsByYear') IS NOT NULL
	DROP TYPE dbo.OrderTotalsByYear;
	
CREATE TYPE dbo.OrderTotalsByYear AS TABLE
(
	orderyear INT NOT NULL PRIMARY KEY,
	qty INT NOT NULL
);
GO

DECLARE @MyOrderTotalsByYear1 AS dbo.OrderTotalsByYear;

INSERT INTO @MyOrderTotalsByYear1(orderyear,qty)
SELECT	YEAR(O.orderdate) AS orderyear,
		SUM(OD.qty) AS qty
FROM Sales.Orders AS O
LEFT OUTER JOIN Sales.OrderDetails AS OD
ON OD.orderid = O.orderid
GROUP BY YEAR(orderdate);

SELECT orderyear, qty FROM @MyOrderTotalsByYear1;

/*orderyear   qty
----------- -----------
2006        9581
2007        25489
2008        16247

(3 row(s) affected)*/

