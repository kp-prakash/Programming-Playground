USE TSQLFundamentals2008;
SELECT orderid, orderdate, custid, empid
FROM Sales.Orders
WHERE YEAR(orderdate) = 2007 AND MONTH(orderdate) = 6;

/*BEST PRACTICE - Helps using indexes better.*/
SELECT orderid, orderdate, custid, empid
FROM Sales.Orders
WHERE orderdate >= '20070601'
 AND orderdate < '20070701';

/*Orders placed on last day of a month*/
SELECT orderid, orderdate, custid, empid
FROM Sales.Orders
WHERE orderdate = DATEADD(month, DATEDIFF(month, '19991231', orderdate), '19991231');

/*Names with 2 or more a's*/
SELECT empid, firstname, lastname
FROM HR.Employees
WHERE lastname LIKE '%a%a%';

/*Sum of orders greater than 10000*/
SELECT orderid, SUM(qty*unitprice) as totalvalue
FROM Sales.OrderDetails
GROUP BY orderid
HAVING SUM(qty*unitprice)>10000
ORDER BY totalvalue DESC;

/*Return the three ship countries with the highest average freight in 2007.*/
SELECT TOP(3) shipcountry, AVG(freight) AS Average
FROM Sales.Orders
WHERE orderdate >= '20070101' AND orderdate < '20080101'
GROUP BY shipcountry
ORDER By Average DESC;

/*Calculate row numbers for orders based on order date ordering 
(using order ID as tiebreaker) for each customer separately. */

SELECT custid, orderdate, orderid, 
ROW_NUMBER() OVER(PARTITION BY custid ORDER BY orderdate, orderid) AS rownum
FROM Sales.Orders
ORDER BY custid, rownum;


/*Figure out the SELECT statement that returns for each employee the 
gender based on the title of courtesy. For 'Ms.' and 'Mrs.' return 
'Female'; for 'Mr.' return 'Male'; and in all other cases 
(for example, 'Dr.') return 'Unknown'. */
SELECT empid,firstname,lastname,titleofcourtesy,
CASE 
	WHEN titleofcourtesy IN ('Ms.', 'Mrs.') THEN 'Female'
	WHEN titleofcourtesy = 'Mr.' THEN 'Male'
	ELSE 'UnKnown'
END AS gender
FROM HR.Employees;

/*By default SQL Server sorts NULLs before non-NULL values. To get 
NULLs to sort last, you can use a CASE expression that returns 1 
when the region column is NULL and 0 when it is not NULL. Non-NULLs 
get 0 back from the expression; therefore, they sort before NULLs 
(which get 1). This CASE expression is used as the first sort column. 
The region column should be specified as the second sort column. This 
way, non-NULLs sort correctly among themselves. Here's the complete 
solution query:*/

SELECT custid, region
FROM Sales.Customers
ORDER BY 
	CASE WHEN region IS NULL THEN 1 ELSE 0 END, region;