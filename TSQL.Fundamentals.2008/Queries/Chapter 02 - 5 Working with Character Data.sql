USE TSQLFundamentals2008;

/*Data Types
Regular CHAR VARCHAR
~Every Character is represented by 1 byte.
~No need to prefix with N, just characters with in single quotes ''.
~Limited options only 256 characters can be represented.
~Only english characters. 

Unicode NCHAR NVARCHAR
~Every Character is represented by 2 bytes.
~Need to prefix with N and characters with in single quotes ''. e.g. N'This is unicode'
~N stands for National.
~2^16 = 65,536 characters can be represented.
~Support for many languages.

CHAR and NCHAR - Fixed length. SQL Server preserves space in the row based on the 
column's defined size and not on the actual number of characters in the character string.

VARCHAR and NVARCHAR - Variable length SQL Server uses as much storage space in the row as 
required to store the characters that appear in the character string, plus two extra bytes 
for offset data.

IMP NOTE:  Because no expansion of the row is required when the strings are expanded, 
fixed-length data types are more suited for 'write-focused' systems. But because storage 
consumption is not optimal, you pay more when reading data.

Because storage consumption is reduced when compared to that of fixed-length types, read 
operations are faster. However, updates might result in row expansion, which may result 
in data movement outside the current page. Therefore, updates of data having variable-length 
data types are less efficient than updates of data having fixed-length data types.


You can also define the variable-length data types with the MAX specifier instead of a maximum 
number of characters. When the column is defined with the MAX specifier, a value with a size up 
to a certain threshold (8,000 bytes by default) is stored inline in the row. A value with a size 
above the threshold is stored external to the row as a large object (LOB).


Collation:
Property of character data that encapsualtes several aspects including, language support,
sort order, case sensitivity, accent sensitivity, and more. 

*/
SELECT name, description
FROM sys.fn_helpcollations();

/*For example, the collation Latin1_General_CI_AS means that:

Latin1_General The supported language is English.

Dictionary sorting Sorting and comparison of character data is based on 
dictionary order ('A' and 'a' < 'B' and 'b').

You determine that it is dictionary order because that's the default when
no other ordering is defined explicitly. More specifically, the element BIN
doesn't explicitly appear in the collation name. If the element BIN appeared,
it would mean that sorting and comparison of character data was based on the
binary representation of characters ('A' < 'B' < 'a' < 'b').

CI The data is case insensitive ('a' = 'A').

AS The data is accent sensitive ('à' <> 'ä').

Can be defined in 4 levels
Instance, Database, Column, Expression (Lowest level is the effective)

Use COLLATE Clause to specify collation for a database, if not instance default is assumed.
*/

/*Database collation determines collation of meta data objects in the data base.
If collation is case insensitive we cannot create two tables called T1 and t1. 
If it is case sensitive we can!
*/

/*Use COLLATE Clause to specify collation for a column as part of its definition,
if not database collation is assumed.*/

/*In a case insensitve environment the following query will return even if lastname is Davis*/
SELECT empid, firstname, lastname
FROM HR.Employees
WHERE lastname = N'davis';

SELECT empid,firstname,lastname
FROM HR.Employees
WHERE lastname COLLATE Latin1_General_CS_AS = N'davis';--Will retrieve no records.

/*QUOTED_IDENTIFIER
When the setting is turned on, the behavior is according to standard SQL, meaning that 
double quotes are used to delimit identifiers. When the setting is turned off, the 
behavior is nonstandard, and double quotes are used to delimit literal character 
strings. It is strongly recommended to follow best practices and stick to standard 
behavior (setting is on). Most database interfaces including OLEDB and ODBC turn 
this setting on by default.
*/

/*Functions and Operators*/
SELECT empid,firstname + N' ' + lastname AS fullname
FROM HR.Employees

--Concatenation will NULL should yield NULL as per SQL standard
SELECT country,region,city, country + N',' + region + N',' + city AS location
FROM Sales.Customers

SET CONCAT_NULL_YIELDS_NULL OFF --NULL values treated as empty strings during concat.
SELECT country,region,city, country + N',' + region + N',' + city AS location
FROM Sales.Customers
SET CONCAT_NULL_YIELDS_NULL ON

/*Not a good practice to set the CONCAT_NULL_YIELDS_NULL better use the COALESCE 
function as shown below.*/
SELECT country,region,city, country + N',' + COALESCE(region,N'') +  N',' + city as location
FROM Sales.Customers

/*STRING FUNCTIONS*/
SELECT SUBSTRING(N'ABCDE',1,3) /* ABC. If 3rd arg > string length, 
everything is returned without raising an exception. Use a large value to return everything.*/

SELECT LEFT( N'ABCDE', 3 ), RIGHT( N'ABCDE', 3 )

/*	LEN			Returns number of characters
	DATALENGTH	Returns number of bytes (Remember unicode string 1 character takes 2 bytes)
Another difference between LEN and DATALENGTH is that the former 
excludes trailing blanks while the latter doesn't.*/

SELECT LEN('ABCDE') AS CHARLENGTH, DATALENGTH('ABCDE') AS CHARDATALENGTH,
LEN(N'ABCDE') AS NCHARLENGTH, DATALENGTH(N'ABCDE') AS NCHARDATALENGTH
/*
CHARLENGTH  CHARDATALENGTH NCHARLENGTH NCHARDATALENGTH
----------- -------------- ----------- ---------------
5           5              5           10
(1 row(s) affected)
*/

SELECT CHARINDEX(' ','Itzik Ben-Gan'); --6

/*The PATINDEX function returns the position of the first occurrence of a pattern within a string.*/
SELECT PATINDEX('%[0-9]%', 'abcd123efgh');
/*This code returns the output 5.*/

SELECT REPLACE('1-a 2-b', '-', ':');

/*Using replace to count the number of occurrences of a character in a string
REPLACE character with empty string amd the find the diff between original and new.
*/
SELECT empid, lastname,
  LEN(lastname) - LEN(REPLACE(lastname, 'e', '')) AS numoccur
FROM HR.Employees;

/*REPLICATE repeats the string n number of times*/
SELECT REPLICATE('abc',5);-- 'abcabcabcabcabc'

/*The next example demonstrates using the REPLICATE function, along with the RIGHT
function and string concatenation. The following query against the Production.
Suppliers table generates a 10-digit string representation of the integer 
supplier ID with leading zeros:*/

--CAST used to cast the number to a string, else it will become integer addtion.
SELECT supplierid, RIGHT(REPLICATE('0',9) 
							+ CAST(supplierid AS VARCHAR(10))
						,10) AS supplieridformat
FROM Production.Suppliers;

/* OUTPUT:
supplierid  supplieridformat
----------- ----------------
29          0000000029
28          0000000028
4           0000000004
21          0000000021
2           0000000002
22          0000000022
14          0000000014
11          0000000011
25          0000000025
7           0000000007
13          0000000013
26          0000000026
8           0000000008
9           0000000009
1           0000000001
12          0000000012
15          0000000015
3           0000000003
19          0000000019
6           0000000006
5           0000000005
24          0000000024
18          0000000018
23          0000000023
17          0000000017
10          0000000010
16          0000000016
27          0000000027
20          0000000020
(29 row(s) affected)
*/

/* The STUFF Function
The STUFF function allows you to remove a substring from a string and insert a new substring instead.
Syntax
STUFF( string, pos, delete_length, insertstring )
*/
SELECT STUFF('abc',2,1,'xyz') --axyzc

/*The UPPER and LOWER Functions
The UPPER and LOWER functions return the input string with all uppercase or lowercase characters.
Syntax
UPPER( string ), LOWER( string )
For example, the following code returns 'ITZIK BEN-GAN':*/

SELECT UPPER('Itzik Ben-Gan');

/*The following code returns 'itzik ben-gan':*/
SELECT LOWER('Itzik Ben-Gan');
/*The RTRIM and LTRIM Functions
The RTRIM and LTRIM functions return the input string with leading or trailing spaces removed.

Syntax
RTRIM( string ), LTRIM( string )

If you want to remove both leading and trailing spaces, use the result of one 
function as the input to the other. For example, the following code removes both 
leading and trailing spaces from the input string returning 'abc':
*/
SELECT RTRIM(LTRIM(' abc '));

/*FORMAT function - SS 2012*/

SELECT FORMAT(1759.125, '000000000.00');

/*The LIKE predicate*/
--% The % (Percent) Wildcard
SELECT empid, lastname
FROM HR.Employees
WHERE lastname LIKE N'D%'; --Starts with D

--The _ (Underscore) Wildcard - Represents a single character
SELECT empid, lastname
FROM HR.Employees
WHERE lastname LIKE N'_e%';--First any character second is 'e'

--The [<List of Characters>] Wildcard
SELECT empid, lastname
FROM HR.Employees
WHERE lastname LIKE N'[ABC]%'--lastnames that start with A or B or C

--The [<Character>-<Character>] Wildcard
SELECT empid,lastname
FROM HR.Employees
WHERE lastname LIKE N'[A-C]%'--lastnames where first character is A through C

--The [^<Character List or Range>] Wildcard
SELECT empid,lastname
FROM HR.Employees
WHERE lastname LIKE N'[^A-C]%'--lastnames where first character is not A through C


/*The ESCAPE Character
If you want to look for a character that is also used as a wildcard, ('%', '_', '[', ']', 
for example) you can use an escape character. Specify a character that you know for sure 
doesn't appear in the data as the escape character in front of the character you are looking 
for, and specify the keyword ESCAPE followed by the escape character right after the pattern. 
For example, to check whether a column called col1 contains an underscore, use col1 LIKE '%!_%' ESCAPE '!'.
For wildcards '%', '_', and '[' you can use square brackets instead of an escape character. 
Instead of col1 LIKE '%!_%' ESCAPE '!' you can use col1 LIKE '%[_]%'.
*/