USE TSQLFundamentals2008;
/*CROSS JOIN*/
/*Logically, a cross join is the simplest type of join. A cross 
join implements only one logical query processing phase-a 
Cartesian Product. This phase operates on the two tables provided 
as inputs to the join, and produces a Cartesian product of the two. 
That is, each row from one input is matched with all rows from the 
other. So if you have m rows in one table and n rows in the other, 
you get m x n rows in the result. */
/*ANSI SQL 92 Syntax*/ -------Much Preferred.
SELECT Customers.Custid, Employees.Empid
FROM Sales.Customers AS Customers
CROSS JOIN HR.Employees AS Employees;

/*ANSI SQL-89 Syntax*/
SELECT Customers.Custid, Employees.Empid
FROM	Sales.Customers AS Customers,
		HR.Employees AS Employees;

/*There is no logical or performance difference between the two syntaxes.*/

/*Self Cross Joins*/
SELECT
  E1.empid, E1.firstname, E1.lastname,
  E2.empid, E2.firstname, E2.lastname
FROM HR.Employees AS E1
  CROSS JOIN HR.Employees AS E2;

/*In a self-join, aliasing tables is not optional. Without table aliases, all 
column names in the result of the join would be ambiguous. */

/*PRODUCING TABLES OF NUMBERS*/

USE tempdb;
IF OBJECT_ID('dbo.Digits', 'U') IS NOT NULL DROP TABLE dbo.Digits;
CREATE TABLE dbo.Digits(digit INT NOT NULL PRIMARY KEY);
INSERT INTO dbo.Digits(digit)
  VALUES (0),(1),(2),(3),(4),(5),(6),(7),(8),(9);
/*
Note:
Above INSERT syntax is new in Microsoft SQL Server 2008.
In earlier versions use:
INSERT INTO dbo.Digits(digit) VALUES(0);
INSERT INTO dbo.Digits(digit) VALUES(1);
INSERT INTO dbo.Digits(digit) VALUES(2);
INSERT INTO dbo.Digits(digit) VALUES(3);
INSERT INTO dbo.Digits(digit) VALUES(4);
INSERT INTO dbo.Digits(digit) VALUES(5);
INSERT INTO dbo.Digits(digit) VALUES(6);
INSERT INTO dbo.Digits(digit) VALUES(7);
INSERT INTO dbo.Digits(digit) VALUES(8);
INSERT INTO dbo.Digits(digit) VALUES(9);
*/
SELECT D3.digit * 100 + D2.digit * 10 + D1.digit + 1 AS n
FROM         dbo.Digits AS D1
  CROSS JOIN dbo.Digits AS D2
  CROSS JOIN dbo.Digits AS D3
ORDER BY N;

/*INNER JOIN*/
/*An inner join applies two logical query processing phases it applies 
a Cartesian product between the two input tables like a cross join, and 
then it filters rows based on a predicate that you specify. Like cross 
joins, inner joins have two standard syntaxes: ANSI SQL-92 and ANSI SQL-89.

2 Phases
1. Cartesian Product
2. Apply the ON filter.

*/

/*ANSI SQL-92 Syntax*/
USE TSQLFundamentals2008;
SELECT e.empid, e.firstname, e.lastname ,o.orderid FROM
		HR.Employees AS E 
JOIN	Sales.Orders AS O
ON E.empid = O.empid;

/*Like with the WHERE and HAVING clauses, the ON clause also returns only 
rows for which the predicate returns TRUE, and does not return rows for 
which the predicate evaluates to FALSE or UNKNOWN.*/

/*ANSI SQL-89 Syntax*/
SELECT E.empid, E.firstname, E.lastname, O.orderid
FROM HR.Employees AS E, Sales.Orders AS O
WHERE E.empid = O.empid;

/*FURTHER JOIN EXAMPLES - composite joins, non-equi joins, and multi-table joins.*/
/*COMPOSITE JOINS*/
/*A composite join is commonly required when you need to join two tables 
based on a primary key-foreign key relationship, and the relationship is 
composite: that is, based on more than one attribute:
FROM dbo.Table1 AS T1
  JOIN dbo.Table2 AS T2
    ON T1.col1 = T2.col1
    AND T1.col2 = T2.col2*/

USE TSQLFundamentals2008;
IF OBJECT_ID('Sales.OrderDetailsAudit', 'U') IS NOT NULL
  DROP TABLE Sales.OrderDetailsAudit;
CREATE TABLE Sales.OrderDetailsAudit
(
	lsn			INT	NOT NULL IDENTITY,
	orderid		INT NOT NULL,
	productid	INT NOT NULL,
	dt			DATETIME NOT NULL,
	loginname	sysname NOT NULL,
	columnname	sysname NOT NULL,
	oldval		SQL_VARIANT,
	newval		SQL_VARIANT,
	CONSTRAINT PK_OrderDetailsAudit PRIMARY KEY(lsn),
	CONSTRAINT FK_OrderDetailsAudit_OrderDetails
	FOREIGN KEY(orderid, productid)
	REFERENCES Sales.Orderdetails(orderid,productid)
);

SELECT OD.orderid, OD.productid, OD.qty,
  ODA.dt, ODA.loginname, ODA.oldval, ODA.newval
FROM Sales.OrderDetails AS OD
  JOIN Sales.OrderDetailsAudit AS ODA
    ON OD.orderid = ODA.orderid
    AND OD.productid = ODA.productid
WHERE ODA.columnname = N'qty';

/*NON EQUI JOIN*/
/*When the join condition involves any operator besides the equality operator
it is known as non-equi join.*/

SELECT
  E1.empid, E1.firstname, E1.lastname,
  E2.empid, E2.firstname, E2.lastname
FROM HR.Employees AS E1
  JOIN HR.Employees AS E2
    ON E1.empid < E2.empid;

/*MULTI-TABLE JOINS*/
SELECT
  C.custid, C.companyname, O.orderid,
  OD.productid, OD.qty
FROM Sales.Customers AS C
  JOIN Sales.Orders			AS O	ON	C.custid = O.custid
  JOIN Sales.OrderDetails	AS OD	ON	O.orderid = OD.orderid;
  
/*Outer Join
3 Phases:
1. Cartesian Product.
2. Apply the ON Filter.
3. Adding Outer Rows.*/

/*LO Join - Returns customers with no orders*/
SELECT C.custid, C.companyname, O.orderid, O.orderdate
FROM Sales.Customers AS C
	LEFT OUTER JOIN Sales.Orders AS O
	ON C.custid = O.custid;

/*When you need to express a predicate that is not final meaning -
a predicate that determines which rows to match from the nonpreserved 
side - specify the predicate in the ON clause. When you need a filter 
to be applied after outer rows are produced, and you want the filter 
to be final, specify the predicate in the WHERE clause.*/

SELECT C.custid, C.companyname
FROM Sales.Customers AS C
	LEFT OUTER JOIN Sales.Orders AS O
    ON C.custid = O.custid
WHERE O.orderid IS NULL;

/* WHAT IS AN IDEAL COLUMN TO BE USED IN THE WHERE CLAUSE IN AN OUTER JOIN ?
The choice of which attribute from the nonpreserved side of the join 
to filter is important. You should choose an attribute that can only 
have a NULL when the row is an outer row and not otherwise (for example, 
a NULL originating from the base table). For this purpose, three cases 
are safe to consider - a primary key column, a join column, and a column 
defined as NOT NULL.*/

/*BEYOND THE FUNDAMENTALS OF OUTER JOIN*/

/*Including Missing Values*/

USE TSQLFundamentals2008;
SET NOCOUNT ON;
IF OBJECT_ID('dbo.Nums','U') IS NOT NULL
	DROP TABLE dbo.Nums;
GO
CREATE TABLE dbo.Nums (N INT NOT NULL PRIMARY KEY);
GO
BEGIN TRAN
	DECLARE @i AS INT = 1 ;
	WHILE @i <= 10000
	BEGIN
		INSERT INTO dbo.Nums (N) VALUES (@i);
		SET @i = @i + 1;
	END
COMMIT TRAN
SET NOCOUNT OFF;
SELECT N FROM dbo.Nums;

--Generates a set of dates from 2006 01 01 to 2008 12 31
SELECT DATEADD(DAY, N-1, '20060101') AS OrderDate
FROM dbo.Nums
WHERE N <= DATEDIFF(DAY, '20060101', '20081231') + 1;

--Get the orders placed on a date and returns NULL for no matching orders.
SELECT DATEADD(DAY, NUMS.N - 1, '20060101') AS OrderDate,
	O.orderid AS OrderId,
	O.custid AS CustomerId, 
	O.shipcountry AS Country
FROM dbo.Nums AS NUMS
LEFT OUTER JOIN Sales.Orders AS O
ON DATEADD(DAY, NUMS.N - 1, '20060101') = O.orderdate
ORDER BY Orderdate;

/*Filtering Attributes from the Nonpreserved Side of an Outer Join*/

/*"When you need to review code involving outer joins to look for logical 
bugs, one of the things you should examine is the WHERE clause." If the 
predicate in the WHERE clause refers to an attribute from the nonpreserved 
side of the join using an expression in the form <attribute> <operator> <value>, 
it's usually an indication of a bug*/

/*Very important - Read carefully*/
/*This is because attributes from the nonpreserved side of the join are NULLs in 
outer rows, and an expression in the form NULL <operator> <value> yields UNKNOWN 
(unless it's the IS NULL operator explicitly looking for NULLs). Recall that a 
WHERE clause filters UNKNOWN out. Such a predicate in the WHERE clause causes 
all outer rows to be filtered out, effectively nullifying the outer join. In 
other words, it's as if the join type logically becomes an inner join. So the 
programmer either made a mistake in the choice of the join type, or made a 
mistake in the predicate. If this is not clear yet, the following example 
might help.*/

SELECT C.custid, C.companyname, O.orderid, O.orderdate
FROM Sales.Customers AS C
  LEFT OUTER JOIN Sales.Orders AS O
    ON C.custid = O.custid
WHERE O.orderdate >= '20070101';
/*The programmer made a mistake in using OUTER join or in the WHERE predicate.*/

/*Using Outer Joins in a Multi-Table Join*/
/*Very important - Read carefully*/
/*Some interesting logical bugs have to do with the logical order in which 
outer joins are processed. For example, a common logical bug involving outer 
joins could be considered a variation of the bug in the previous section. 
Suppose that you write a multi-table join query with an outer join between 
two tables, followed by an inner join with a third table. If the predicate 
in the inner join's ON clause compares an attribute from the nonpreserved 
side of the outer join and an attribute from the third table, all outer 
rows are filtered out. Remember that outer rows have NULLs in the attributes 
from the nonpreserved side of the join, and comparing a NULL with anything 
yields UNKNOWN, and UNKNOWN is filtered out by the ON filter. In other words, 
such a predicate would nullify the outer join and logically it would be as if 
you specified an inner join. For example, consider the following query:*/

/*Query with bug: Cust id 22 57 are missed out!*/
SELECT C.custid, O.orderid, OD.productid, OD.qty
FROM Sales.Customers AS C
  LEFT OUTER JOIN Sales.Orders AS O
    ON C.custid = O.custid
  JOIN Sales.OrderDetails AS OD
    ON O.orderid = OD.orderid;

/*To generalize the problem: outer rows are nullified whenever any kind of outer 
join (left, right, or full) is followed by a subsequent inner join or right outer 
join. That's assuming, of course, that the join condition compares the NULLs from 
the left side with something from the right side.*/

/*1st fix: First join the orders and order details with 
inner join and then apply outer join*/
SELECT C.custid, O.orderid, OD.productid, OD.qty
FROM Sales.Customers AS C
	LEFT OUTER JOIN 
		(Sales.Orders O 
			JOIN Sales.OrderDetails OD
			ON O.orderid=OD.orderid)
	ON C.custid=O.custid;


/*2nd Fix: Use LEFT OUTER JOIN in the second join as well*/
SELECT C.custid, O.orderid, OD.productid, OD.qty
FROM Sales.Customers AS C
  LEFT OUTER JOIN Sales.Orders AS O
    ON C.custid = O.custid
  LEFT OUTER JOIN Sales.OrderDetails AS OD
    ON O.orderid = OD.orderid;
    
/*3rd Fix: Use INNER JOIN first between Order and Order details and then 
use RIGHT OUTER JOIN with customers*/
SELECT C.custid, O.orderid, OD.productid, OD.qty
FROM Sales.Orders O
  JOIN Sales.OrderDetails AS OD
    ON O.orderid = OD.orderid
  RIGHT OUTER JOIN Sales.Customers C
	ON O.custid = C.custid;

/*Using the COUNT Aggregate with Outer Joins*/
/*Another common logical bug involves using COUNT with outer joins. When you 
group the result of an outer join and use the COUNT(*) aggregate, the aggregate 
takes into consideration both inner rows and outer rows because it counts rows 
regardless of their contents. Usually, you're not supposed to take outer rows 
into consideration for the purposes of counting. For example, the following 
query is supposed to return the count of orders for each customer:*/

SELECT C.Custid, COUNT(*) AS NumOrders
FROM Sales.Customers C
LEFT OUTER JOIN Sales.Orders O
ON C.custid = O.custid
GROUP BY C.custid; --Output is incorrect as Count(*) shows cust 22 and 57 with one 
--order but they dont have any orders.

/*The COUNT(*) aggregate function cannot detect whether a row 
really represents an order. To fix the problem you should use 
COUNT(<column>) instead of COUNT(*), and provide a column from 
the nonpreserved side of the join. This way, the COUNT() aggregate 
ignores outer rows because they have a NULL in that column. 
Remember to use a column that can only be NULL in case the row is 
an outer row—for example, the primary key column orderid:*/

SELECT C.Custid, COUNT(O.orderid) AS NumOrders
FROM Sales.Customers C
LEFT OUTER JOIN Sales.Orders O
ON C.custid = O.custid
GROUP BY C.custid;--Shows cust 22 and 57 with 0 orders