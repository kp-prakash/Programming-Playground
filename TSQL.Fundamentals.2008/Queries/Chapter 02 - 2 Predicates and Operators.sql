USE TSQLFundamentals2008;

/*Predicates supported by SQL Server IN BETWEEN and LIKE*/

--IN
SELECT orderid, empid, orderdate
FROM Sales.Orders
WHERE orderid IN(10248, 10249, 10250);

--BETWEEN - AND (Incluldes boundary values)
SELECT orderid, empid,orderdate
FROM Sales.Orders
WHERE orderid BETWEEN 10251 AND 10300;

--LIKE
SELECT  empid, firstname, lastname
FROM HR.Employees
WHERE lastname like N'D%';
/*If you're curious about the use of the letter N to prefix the 
string 'D%', it stands for National and is used to denote that 
a character string is of a Unicode data type (NCHAR or NVARCHAR), 
as opposed to a regular character data type (CHAR or VARCHAR).
Because the data type of the lastname attribute is NVARCHAR(40),
the letter N is used to prefix the string.*/

/*T-SQL Supports the following comparison operators: 
=, >, <, >=, <=, <>, !=, !>, !<, 
out of which the last three are not standard.*/
SELECT empid,orderid,orderdate
FROM Sales.Orders
WHERE orderdate >= '20080101';

/*Combine operators*/
SELECT empid,orderid,orderdate
FROM Sales.Orders
WHERE orderdate >= '20080101'
AND empid IN (1,3,5,7,9);

/*Note that the data type of a scalar expression involving 
two operands is determined in T-SQL by the higher of the two 
in terms of data type precedence. If both operands are of the 
same data type, the result of the expression is of the same 
data type as well. For example, a division between two integers 
(INT) yields an integer. The expression 5/2 returns the integer 
2 and not the numeric 2.5. This is not a problem when dealing 
with constants because you can always specify the values as 
numeric ones with a decimal point. But when dealing with, say, 
two integer columns, such as col1/col2, you need to cast the 
operands to the appropriate type if you want the calculation 
to be a numeric one: 
CAST(col1 AS NUMERIC(12, 2))/ CAST(col2 AS NUMERIC(12, 2)). 
The data type NUMERIC(12, 2) has precision 12 and scale 2, 
meaning twelve digits in total, two of which are after the decimal point. */

/*e.g.*/ SELECT CAST(9999999999 AS NUMERIC(12,2))

/*If the two operands are of different types, the one with the lower 
precedence is promoted to the one that is higher. For example, in the 
expression 5/2.0 the first operand is INT and the second is NUMERIC. 
Because NUMERIC is considered higher than INT, the INT operand 5 is 
implicitly converted to the NUMERIC 5.0 before the arithmetic 
operation, and you get the result 2.5.*/

/*The following list has the precedence among operators, from highest to lowest: 
1. ( ) (Parentheses)
2. * (Multiply), / (Division), % (Modulo)
3. + (Positive), –(Negative), + (Add), (+ Concatenate), –(Subtract)
4. =, >, <, >=, <=, <>, !=, !>, !< (Comparison operators)
5. NOT
6. AND
7. BETWEEN, IN, LIKE, OR
8. = (Assignment)
*/

SELECT orderid,custid, empid,orderdate
FROM Sales.Orders
WHERE custid=1
AND empid in (1,3,5)
OR custid = 85
AND empid in (2,4,6);

/*AND preceeds OR in execution in order to improve readability
and maintainability for others we can use parantesis.*/

SELECT orderid,custid,empid,orderdate
FROM Sales.Orders
WHERE (custid =1 AND empid in (1,3,5))
OR (custid = 85 AND empid in (2,4,6));

SELECT 10 + 2 * 6; -- = 22
SELECT (10 + 2) * 6; -- = 72