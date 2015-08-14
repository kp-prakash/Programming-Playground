/*NULLs - VERY IMPORTANT READ CAREFULLY!!!
As explained in Chapter 1, "Background to T-SQL Querying and Programming," SQL supports 
the NULL mark to represent missing values, and uses three-valued logic, meaning that 
predicates can evaluate to TRUE, FALSE, or UNKNOWN. T-SQL follows the standard 
in this respect. Treatment of NULLs and UNKNOWN in SQL can be very confusing because 
intuitively people are more accustomed to thinking in terms of two-valued logic 
(TRUE, FALSE). To add to the confusion, different language elements in SQL treat 
NULLs and UNKNOWN differently.

Let's start with three-valued predicate logic. A logical expression involving only 
existing or present, values evaluates to either TRUE or FALSE, but when the logical 
expression involves a missing value, it evaluates to UNKNOWN. For example, consider 
the predicate salary > 0. When salary is equal to 1000, the expression evaluates to 
TRUE. When salary is equal to –1000, the expression evaluates to FALSE. When salary 
is NULL, the expression evaluates to UNKNOWN.

SQL treats TRUE and FALSE in an intuitive and probably expected manner. For example, 
if the predicate salary > 0 appears in a query filter (the WHERE and HAVING clauses), 
rows or groups for which the expression evaluates to TRUE are returned, while those 
for which the expression evaluates to FALSE are filtered out. Similarly, if the 
predicate salary > 0 appears in a CHECK constraint in a table, INSERT or UPDATE 
statements for which the expression evaluates to TRUE are accepted, while those 
for which the expression evaluates to FALSE are rejected.

SQL has different treatments for UNKNOWN in different language elements (and for some 
people, not necessarily the expected treatments). The correct definition of the 
treatment SQL has for query filters is "accept TRUE," meaning that both FALSE and 
UNKNOWN are filtered out. Conversely, the definition of the treatment SQL has for 
CHECK constraints is "reject FALSE," meaning that both TRUE and UNKNOWN are accepted. 
If SQL used two-valued predicate logic, there wouldn't be a difference between the 
definitions "accept TRUE" and "reject FALSE." But with three-valued predicate logic, 
"accept TRUE" rejects UNKNOWN (accepts TRUE, hence rejects both FALSE and UNKNOWN) 
while "reject FALSE" accepts it (rejects FALSE, hence accepts both TRUE and UNKNOWN). 
Using the predicate salary > 0 from the previous example, a NULL salary would cause 
the expression to evaluate to UNKNOWN. If this predicate appears in a query's WHERE 
clause, a row with a NULL salary would be filtered out. If this predicate appears in 
a CHECK constraint in a table, a row with a NULL salary would be accepted.


One of the tricky aspects of UNKNOWN is that when you negate it, you still get UNKNOWN. 
For example, given the predicate NOT (salary > 0), when salary is NULL, salary > 0 
evaluates to UNKNOWN, and NOT UNKNOWN remains UNKNOWN.

What some people find surprising is that an expression comparing two NULLs (NULL = NULL) 
evaluates to UNKNOWN. The reasoning for this is that a NULL represents a missing or 
unknown value, and you can't really tell whether one unknown value is equal to another. 
Therefore, SQL provides you with the predicates IS NULL and IS NOT NULL, which you 
should use instead of = NULL and <> NULL.
*/
USE TSQLFundamentals2008;

SELECT custid,country,region,city
FROM Sales.Customers
WHERE region = N'WA';

SELECT custid,country,region,city
FROM Sales.Customers
WHERE region <> N'WA';

SELECT custid,country,region,city
FROM Sales.Customers
WHERE region = NULL; -- Return no records Use IS NULL!

SELECT custid,country,region,city
FROM Sales.Customers
WHERE region IS NULL; --Works as expected. Returns record with NULLs.

SELECT custid, country, region, city
FROM Sales.Customers
WHERE region <> N'WA'
   OR region IS NULL; --Returns Regions not WA and NULLS.

/*SQL also treats NULLs inconsistently in different language elements for comparison 
and sorting purposes. Some elements treat two NULLs as equal to each other and others 
as different.

For example, for grouping and sorting purposes, two NULLs are considered equal. That is, 
the GROUP BY clause arranges all NULLs in one group just like present values, and the 
ORDER BY clause sorts all NULLs together. ANSI SQL leaves it to the product 
implementation as to whether NULLs sorts before present values or after. T-SQL sorts 
NULLs before present values.

As mentioned earlier, query filters "accept TRUE." An expression comparing two NULLs 
yields UNKNOWN; therefore, such a row is filtered out.

ANSI SQL has two kinds of UNIQUE constraint: one that treats NULLs as equal (allowing 
only one NULL) and one that treats NULLs as different (allowing multiple NULLs). T-SQL 
implemented only the former.

Keeping in mind the inconsistent treatment SQL has for UNKNOWN and NULLs and the potential 
for logical errors, you should explicitly think of three-valued logic in every query that 
you write. If the default treatment is not the one you desire, you have to intervene 
explicitly; otherwise, just ensure that the default behavior is in fact the one you 
are after.*/


/*A NOTE ON ALL AT ONCE OPERATIONS
SQL supports a concept called all-at-once operations, which means that all expressions that 
appear in the same logical query processing phase are evaluated as if at the same point in time.
This concept explains why, for example, you cannot refer to column aliases assigned in the 
SELECT clause within the same SELECT clause, even if it seems intuitively that you should be able to.
SELECT clause and WHERE clause evaluate all the expressions at once.
SQL Server does support short circuits, but because of the all-at-once operations concept in 
ANSI SQL, SQL Server is free to process the expressions in the WHERE clause in any order that 
it likes. SQL Server usually makes decisions like this based on cost estimations, meaning that 
typically the expression that is cheaper to evaluate is evaluated first.
SQL Server guarantees the processing order of the WHEN clauses in a CASE expression.
*/