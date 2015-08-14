--Elements of the SELECT Statement
USE TSQLFundamentals2008;

--A sample query to select the number of orders year wise and list
--only those years having more than an order 
--and arranging them by empid and order year.
SELECT empid,YEAR(orderdate) AS orderyear, COUNT(*) AS numorders
FROM Sales.Orders
WHERE custid = 71
GROUP BY empid,YEAR(orderdate)
HAVING COUNT(*) > 1
ORDER BY empid,orderyear;

/*
Logical Order or Processing for Query Clauses (FWGHSO)
1. FROM
2. WHERE
3. GROUP BY
4. HAVING
5. SELECT
6. ORDER BY
*/

--1. FROM Clause
SELECT orderid, custid, empid, orderdate, freight
FROM Sales.Orders;

--2. WHERE Clause
SELECT orderid,custid,empid,orderdate,freight
FROM Sales.Orders
WHERE custid=71

--3. GROUP BY Clause
/*Because an aggregate function returns a single value 
per group, elements 'that do not participate in the GROUP
BY list are only allowed as inputs' to an aggregate 
function such as COUNT, SUM, AVG, MIN, or MAX. For
example, the following query returns the total freight and
 number of orders per each employee and order year:*/
SELECT	empid,
		YEAR(orderdate) AS orderyear,
		SUM(freight) AS totalfreight,
		COUNT(*) AS numorders
FROM sales.Orders
WHERE custid=71
GROUP BY empid,YEAR(orderdate);

/*If you try to refer to an attribute that does not
 participate in the GROUP BY list (such as freight) and not 
as an input to an aggregate function in any clause 
that is processed after the GROUP BY clause, you get an 
error in such a case there's no guarantee that 
the expression will return a single value per group.*/

/*SELECT empid, YEAR(orderdate) AS orderyear, freight
FROM Sales.Orders
WHERE custid = 71
GROUP BY empid, YEAR(orderdate);*/

--All Aggregate functions ignore null except one exception COUNT(*)
/*Consider the sample set for qty 30,10,10,NULL,10
COUNT(*) will return 5 - Considers NULL.
COUNT(qty) will return 4 - Considers only known values and omits NULL
COUNT(DISTINCT qty) - Considers only distinct known values.
The expression SUM(qty) would return 60, the expression SUM(DISTINCT qty) would return 40.
The expression AVG(qty) would return 15 while the expression AVG(DISTINCT qty) would return 20.*/

--Returns the number of customers handled by an employee in a given order year.
SELECT empid,YEAR(orderdate) orderyear,COUNT(DISTINCT custid) numcustomers
FROM Sales.Orders
GROUP By empid,YEAR(orderdate);

--4. Having Clause
/*With the help of having clause you can 
specify the predicate / logical expression
to filter groups as opposed to filtering 
individual rows which happens in the WHERE phase*/
SELECT empid,YEAR(orderdate) orderyear, COUNT(*) numorders
FROM Sales.Orders
WHERE custid=71
GROUP BY empid,YEAR(orderdate)
HAVING COUNT(*) > 1

--5. SELECT Clause
/*It is good practice to have proper meaningful alias names
<expression> AS <alias> - Use this!
<alias> = <expression> - This is fine but not used - atleast I have not seen it!
<expression> <alias> - Avoid using this.*/
--All examples above have been written using the SELECT clause with alias names.

/*SELECT is processed only after FROM, WHERE, GROUP BY and HAVING clauses.
Do not refer to alias names used in select statement in WHERE clauses. This will
result in error.

The query below will not work.
SELECT orderid, YEAR(orderdate) AS orderyear
FROM Sales.Orders
WHERE orderyear > 2006;

Msg 207, Level 16, State 1, Line 3
Invalid column name 'orderyear'.
*/

SELECT orderid, YEAR(orderdate) AS orderyear
FROM Sales.Orders
WHERE YEAR(orderdate) > 2006; --SQL Server is intelligent enough to calculate and evaluate this once.

/*SELECT alone does not guarantee uniqueness. So we need to use DISTINCT clause*/

SELECT empid, YEAR(orderdate) AS orderyear
FROM Sales.Orders
WHERE custid = 71; --Returns Duplicates

SELECT DISTINCT empid, YEAR(orderdate) AS orderyear
FROM Sales.Orders
WHERE custid = 71; --Returns unique values

/*Do not use SELECT * as it degrades performance and do not use ordinals to find
coulmns.  This might easy end up in broken code once the schema changes. It is 
always a best practice to used names for column and use names in client code.*/

/*Within the SELECT clause you are still not allowed to 
refer to a column alias that was created in the same SELECT
clause, regardless of whether the expression that assigns
 the alias appears to the left or right of the expression
that attempts to refer to it. For example, the following
attempt is 'invalid':

SELECT orderid,
YEAR(orderdate) AS orderyear,
orderyear + 1 AS nextyear
FROM Sales.Orders;

*/

--6. ORDER BY Clause
/*By default if order is not specified SQL can return records in any order*/
SELECT empid, YEAR(orderdate) AS orderyear, COUNT(*) AS numorders
FROM Sales.Orders
WHERE custid = 71
GROUP BY empid, YEAR(orderdate)
HAVING COUNT(*) > 1
ORDER BY empid, orderyear;

/*T-SQL allows you to specify elements in the ORDER BY
 clause that do not appear in the SELECT clause, meaning
that you can sort by something that you don't necessarily
 want to return in the output.*/

SELECT empid,lastname,firstname,country
FROM HR.Employees
ORDER BY hiredate;

/*However when DISTINCT is specified the ORDER BY 
list is restricted to arributes that appear in the SELECT list.
The query below will fail.

SELECT DISTINCT country
FROM HR.Employees
ORDER BY empid;
*/

SELECT DISTINCT empid, country
FROM HR.Employees
ORDER BY empid;

/*Getting second max of empid*/
SELECT MAX( empid) AS EMPID
	FROM Hr.Employees
	WHERE empid < ( SELECT MAX( empid) AS EMPID
					FROM Hr.Employees)

/*The TOP Option - Helps to limit the number 
or percentage of rows that the query returns.*/
SELECT TOP(5) orderid,orderdate,custid,empid
FROM Sales.Orders
ORDER BY orderdate DESC; --This query returns the 5 most recent orders.

/*The TOP option is processed as part of the SELECT phase, right after the
DISTINCT clause is processed (if one exists).

The ORDER BY Clause serves 2 purposes here.
1. SELECT Phase: TOP relies on ORDER BY to determine the logical precendence of rows.
2. ORDER BY Phase: ORDER BY is also used to sort the output for presentation purpose.
*/

/*PERCENT Keyword : SQL Server calculates the number of rows to return based on a 
percentage of the number of qualifying rows, rounded up.*/
SELECT TOP(1) PERCENT orderid,orderdate,custid,empid
FROM Sales.Orders
ORDER BY orderdate DESC; -- Returns one of the possible result as there is no 
--ordering specified and there can be duplicate order dates.
/*You might have noticed that the ORDER BY list is not 
unique because no primary key or unique constraint is 
defined on the orderdate column.
Multiple rows can have the same order date. In a case
where no tiebreaker is specified, precedence among rows
in case of ties (rows with the same order date) is
undefined. This fact makes the query nondeterministic
more than one result can be considered correct. In case
of ties, SQL Server chooses rows based on whichever
row it physically happens to access first.*/

/*In order to break the tie you can add order id DESC to the query*/
SELECT TOP(5) orderid, orderdate, custid, empid
FROM Sales.Orders
ORDER BY orderdate DESC, orderid DESC;
/*This is the only possible result set as this is ordered by the primary key.*/


/*WITH TIES - Returns all the ties whenever there is tie, 
this could be used instead of adding a tie breaker.*/
SELECT TOP(5) WITH TIES orderid,orderdate,custid,empid
FROM Sales.Orders
ORDER BY orderdate DESC
/*Notice that the output has eight rows even though you 
specified TOP (5). SQL Server first returned the TOP (5)
rows based on orderdate DESC precedence, and also all
other rows from the table that had the same orderdate
value as in the last of the five rows that was accessed.*/

--7. OVER Clause
/*The OVER Clause exposes a window of rows to certain kind of calculations.
An aggregate window function operates against a set of values in a window 
of rows that you expose to it using the OVER clause, and not in the context 
of a GROUP BY query. Therefore, you don't have to group the data, and you 
can return base row attributes and aggregates in the same row.*/

 /*The rows exposed to OVER clause are those available after the FROM, 
 WHERE, GROUP BY and HAVING phases are completed. Note that the OVER 
 clause is allowed only in the SELECT and ORDER BY phases.*/
 
 
SELECT orderid, custid, val, empid,
SUM(val) OVER() AS totalvalue,
SUM(val) OVER(PARTITION BY custid) AS cutomertotal
FROM Sales.OrderValues
 
 /*One benefit of the OVER clause is that by enabling you to return base 
 row attributes and aggregate them in the same row, it also enables you 
 to write expressions that mix the two. For example, the following query 
 calculates for each OrderValues row the percentage of the current value 
 out of the grand total, and also the percentage of the current value 
 out of the customer total*/

SELECT orderid,custid,val,empid,
100. * val/SUM(val) OVER() AS totpct,
100. * val/SUM(val) OVER(PARTITION BY custid) AS ordpct
FROM Sales.OrderValues

/*The OVER function is also supported with four ranking functions.
ROW_NUMBER, RANK, DESNSE_RANK, NTILE*/
/*ROW_NUMBER - Assigns unique row number even in case of a tie.
RANK - Indicates how many rows have a lower ordering value.
(If there are two rank 7's the next item is given rank 9 - indicates 8
rows with lower values.)
DENSE_RANK - Indicates how many distince ordering values are lower.
(If there are two rank 7's the next item is given rank 8 - indiactes 7
distinct lower values.)
NTILE - Associate rows in the result set with tiles (equally size group
of rows) by assigning a tile number to each row. Equally split into N 
tiles. If not exactly divisible then tiles starting from the first will
have extra rows until all rows are assigned to the tiles.
*/
SELECT orderid,empid,custid,val,
ROW_NUMBER()	OVER(ORDER BY val) AS rownum,
RANK()			OVER(ORDER BY val) AS [rank],
DENSE_RANK()	OVER(ORDER BY val) AS [dense_rank],
NTILE(100)		OVER(ORDER BY val) AS [ntile]
FROM Sales.OrderValues

/*Ranking functions also support PARTITION BY clause in the OVER clause.
Example row numbers are assigned to each partition.*/

SELECT orderid,empid,custid,val,
ROW_NUMBER() OVER(PARTITION BY custid
					ORDER BY val) as rownum
FROM Sales.OrderValues
ORDER BY custid,val;

/*If specified in the SELECT phase, window calculations are processed before the DISTINCT clause (if one exists).
FROM
WHERE
GROUP BY
HAVING
SELECT
	OVER
	DISTINCT
	TOP
ORDER BY
*/

/*"NOTE THAT THE DISTINCT CLAUSE IS PROCESSED AFTER THE WINDOW FUNCTIONS 
READ THE EXPLANATION BELOW"*/

/*
Are you wondering why it matters that the DISTINCT clause is processed 
after window calculations that appear in the SELECT clause are processed, 
and not before? I'll explain with an example. Currently the OrderValues 
view has 830 rows with 795 distinct values. Consider the following query and its output:*/

SELECT DISTINCT val, ROW_NUMBER() OVER(ORDER BY val) AS rownum
FROM Sales.OrderValues;
/*
val        rownum
---------- -------
12.50      1
18.40      2
23.80      3
28.00      4
30.00      5
33.75      6
36.00      7
36.00      8
40.00      9
45.00      10
...
12615.05   828
15810.00   829
16387.50   830

(830 row(s) affected)

The ROW_NUMBER function is processed before the DISTINCT clause.
First, unique row numbers are assigned to the 830 rows from the 
OrderValues view. Then the DISTINCT clause is processed—therefore, 
no duplicate rows to remove. You can consider it a best practice 
not to specify both DISTINCT and ROW_NUMBER in the same SELECT 
clause as the DISTINCT clause has no effect in such a case.
If you want to assign row numbers to the 795 unique values,
you need to come up with a different solution. For example,
because the GROUP BY phase is processed before the SELECT
phase, you could use the following query:*/

SELECT val, ROW_NUMBER() OVER(ORDER BY val) AS rownum
FROM Sales.OrderValues
GROUP BY val;
/*
This query generates the following output:

val       rownum
--------- -------
12.50     1
18.40     2
23.80     3
28.00     4
30.00     5
33.75     6
36.00     7
40.00     8
45.00     9
48.00     10
...
12615.05  793
15810.00  794
16387.50  795

(795 row(s) affected) 

Here, the GROUP BY phase produces 795 groups for the 795 distinct 
values, and then the SELECT phase produces a row for each group 
with the value and a row number based on val order.
*/