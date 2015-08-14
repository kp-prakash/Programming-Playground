USE TSQLFundamentals2008;
-- Self Contained and Correlated
-- Single Valued, Multi Valued and Table Valued

/*A subquery can be either self-contained or correlated. A self-contained subquery has no dependency
on the outer query that it belongs to, whereas a correlated subquery does. A subquery can
be single-valued, multivalued, or table-valued. That is, a subquery can return a single value (a scalar
value), multiple values, or a whole table result.*/

-- Self-Contained Scalar Subquery Examples
DECLARE @maxid AS INT = (SELECT MAX(orderid) FROM Sales.Orders);
SELECT orderid, orderdate, empid, custid
FROM Sales.Orders
WHERE orderid = @maxid;

-- This could be written as
SELECT orderid, orderdate, empid, custid
FROM Sales.Orders
WHERE orderid = (SELECT MAX(orderid) FROM Sales.Orders);

/*For a scalar subquery to be valid, it must return no more than one value. If a scalar subquery can
return more than one value, it might fail at run time. The following query happens to run without
failure.*/

SELECT orderid
FROM Sales.Orders
WHERE empid =
(SELECT E.empid
FROM HR.Employees AS E
WHERE E.lastname LIKE N'D%');

/*The above query returns the following error!
Msg 512, Level 16, State 1, Line 25
Subquery returned more than 1 value. This is not permitted when the subquery follows =, !=, <, <= , >, >= or when the subquery is used as an expression.
*/

-- Self-Contained Multivalued Subquery Examples
-- Use IN operator for multi valued queries and NOT IN to negate the condition.
SELECT orderid
FROM Sales.Orders
WHERE empid IN 
(SELECT E.empid
FROM HR.Employees AS E
WHERE E.lastname LIKE N'D%');

-- The above query could be written as a JOIN as well.

SELECT O.orderid
FROM HR.Employees AS E
JOIN Sales.Orders AS O
ON E.empid = O.empid
WHERE E.lastname LIKE N'D%';

/*Similarly, you are likely to stumble into many other querying problems that you can solve with
either subqueries or joins. In my experience, there’s no reliable rule of thumb that says that a subquery
is better than a join. In some cases, the database engine interprets both types of queries the
same way. Sometimes joins perform better than subqueries, and sometimes the opposite is true. My
approach is to first write the solution query for the specified task in an intuitive form, and if performance
is not satisfactory, one of my tuning approaches is to try query revisions. Such query revisions
might include using joins instead of subqueries or using subqueries instead of joins.*/

SElECT orderid
FROM Sales.Orders
WHERE empid NOT IN 
(SELECT E.empid
FROM HR.Employees AS E
WHERE E.lastname LIKE N'D%');

