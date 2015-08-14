USE TSQLFundamentals2008;

/*The two forms of CASE expression are simple and searched. 
The simple form allows you to compare one value, or scalar 
expression, with a list of possible values, and return a 
value back for the first match. If no value in the list 
is equal to the tested value, the CASE expression returns 
the value that appears in the ELSE clause (if one exists). 
If a CASE expression doesn't have an ELSE clause, it 
defaults to ELSE NULL.*/

/*Simple example of SELECT CASE*/
SELECT productid,productname,categoryid,
  CASE categoryid
	WHEN 1 THEN 'Beverages'
    WHEN 2 THEN 'Condiments'
    WHEN 3 THEN 'Confections'
    WHEN 4 THEN 'Dairy Products'
    WHEN 5 THEN 'Grains/Cereals'
    WHEN 6 THEN 'Meat/Poultry'
    WHEN 7 THEN 'Produce'
    WHEN 8 THEN 'Seafood'
    ELSE 'Unknown Category'
  END AS category
FROM Production.Products;

/*In this example orders are prioritized based on val as Low Medium n High*/
SELECT orderid,custid,empid,val,
CASE NTILE(3) OVER(ORDER BY val) 
	WHEN 1 THEN 'Low'
	WHEN 2 THEN 'Medium'
	WHEN 3 THEN 'High'
END AS [priority]
FROM Sales.OrderValues
ORDER BY val;


/*The 'simple' CASE form has a single test value, or expression, right after 
the CASE keyword that is compared with a list of possible values in the 
WHEN clauses. The 'searched' CASE form is more flexible because it allows 
you to specify predicates, or logical expressions, in the WHEN clauses 
rather than restricting you to equality comparisons. The searched CASE 
expression returns the value in the THEN clause that is associated with 
the first WHEN logical expression that evaluates to TRUE. If none of the 
WHEN expressions evaluates to TRUE, the CASE expression returns the value 
that appears in the ELSE clause (or NULL if an ELSE clause is not specified). 
For example, the following query produces a value category description based 
on whether the value is less than 1,000.00, between 1,000.00 and 3,000.00, 
or greater than 3,000.00:*/

SELECT orderid,custid,orderdate,val,
CASE 
WHEN val < 1000  THEN 'Less than 1000'
WHEN val BETWEEN 1000 AND 3000 THEN 'Between 1000 and 3000'
WHEN val > 3000  THEN 'More than 3000'
ELSE 'UNKNOWN'
END AS description
FROM Sales.OrderValues
ORDER BY val;