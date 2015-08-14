USE TSQLFundamentals2008;
GO

/*DATETIME SMALLDATETIME DATE TIME DATETIME2 DATETIMEOFFSET*/
/*
TIME(3) means one millisecond accuracy
TIME(7) means one nanosecond accuracy
*/

/*LITERALS*/
--It is a best practice to use character strings to express 
--date and time values, as shown in the following example:

--Implicit conversion
SELECT orderid, custid, empid, orderdate
FROM Sales.Orders
WHERE orderdate = '20070212';

--Explicit conversion
SELECT orderid, custid, empid, orderdate
FROM Sales.Orders
WHERE orderdate = CAST('20070212' AS DATETIME);

/*When the character string is interpreted as DATETIME it is based 
on language setting for the session. We can use SET LANGUAGE but this is 
generally not recommended because some aspects of the code might rely 
on the user's default language. */
SELECT @@LANGUAGE
SET LANGUAGE British;
SELECT CAST('02/12/2007' AS DATETIME);
SET LANGUAGE us_english;
SELECT CAST('02/12/2007' AS DATETIME);

/*Instead modify the DATEFORMAT setting. Always process dates in a 
language netral way*/
/*
 
Data Type			Language-Neutral Formats					Examples
DATETIME			'YYYYMMDD hh:mm:ss.nnn'						'2009-02-12T12:30:15.123' 
					'YYYY-MM-DDThh:mm:ss.nnn'					'20090212 12:30:15.123' 
					'YYYYMMDD'									'20090212'
SMALLDATETIME		'YYYYMMDD hh:mm'							'20090212 12:30' 
					'YYYY-MM-DDThh:mm'							'2009-02-12T12:30' 
					'YYYYMMDD'									'20090212'
DATE				'YYYYMMDD'									'20090212' 
					'YYYY-MM-DD'								'2009-02-12'
DATETIME2			'YYYYMMDD hh:mm:ss.nnnnnnn'					'20090212 12:30:15.1234567' 
					'YYYY-MM-DD hh:mm:ss.nnnnnnn'				'2009-02-12 12:30:15.1234567' 
					'YYYY-MM-DDThh:mm:ss.nnnnnnn'				'2009-02-12T12:30:15.1234567' 
					'YYYYMMDD'									'20090212' 
					'YYYY-MM-DD'								'2009-02-12'
DATETIMEOFFSET		'YYYYMMDD hh:mm:ss.nnnnnnn [+|-]hh:mm'		'20090212 12:30:15.1234567 +02:00' 
					'YYYY-MM-DD hh:mm:ss.nnnnnnn [+|-]hh:mm'	'2009-02-12 12:30:15.1234567 +02:00' 
					'YYYYMMDD'									'20090212' 
					'YYYY-MM-DD'								'2009-02-12'
TIME				'hh:mm:ss.nnnnnnn'							'12:30:15.1234567'

*/
--IMPORTANT
/*With all types that include both date and time components, if you don't 
specify a time part in your literal, SQL Server assumes midnight. If you 
don't specify a time zone, SQL Server assumes 00:00. It is also important 
to note that the formats 'YYYY-MM-DD' and 'YYYY-MM-DD hh:mm...' are 
language-dependent when converted to DATETIME or SMALLDATETIME, and 
language-neutral when converted to DATE, DATETIME2 and DATETIMEOFFSET. 
FOR EXAMPLE, NOTICE IN THE FOLLOWING CODE THAT THE LANGUAGE SETTING 
HAS NO IMPACT ON HOW A LITERAL EXPRESSED WITH THE FORMAT 'YYYYMMDD' 
IS INTERPRETED WHEN CONVERTED TO DATETIME: */
SET LANGUAGE British;
SELECT CAST('20070212' AS DATETIME);
SET LANGUAGE us_english;
SELECT CAST('20070212' AS DATETIME);

/*If you want to use language dependent format then use the CONVERT
function. mm/dd/yyyy - style number 101, dd/mm/yyyy - style number 103.*/
SELECT CONVERT(DATETIME, '02/12/2007', 101);

/*The literal is interpreted as February 12, 2007 regardless of the 
language setting that is in effect. If you want to use the format 
dd/mm/yyyy, use style number 103: */

SELECT CONVERT(DATETIME, '02/12/2007', 103);
--This time the literal is interpreted as December 2, 2007. 

/*WORKING WITH DATE AND TIME SEPARATELY*/
/*If only time is set date defaults to the base date in case of DATETIME data type*/
/*If only date is set time defaults to midnight in case of DATETIME data type*/
/*If the time is stored with non midnight values. Use the range filter.*/
SELECT orderid, custid, empid, orderdate
FROM Sales.Orders
WHERE orderdate >= '20070212'
  AND orderdate < '20070213';

/*FILTERING DATE RANGES*/
--YEAR function.
SELECT orderid, custid, empid, orderdate
FROM Sales.Orders
WHERE YEAR(orderdate) = 2007;

/*To have the potential to use an index efficiently, you need to revise 
the predicate so that there is no manipulation on the filtered column
Better to write the above query as:*/
SELECT orderid, custid, empid, orderdate
FROM Sales.Orders
WHERE orderdate >= '20070101' AND orderdate < '20080101';

/*Similary to get the orders placed in the particular month*/
SELECT orderid, custid, empid, orderdate
FROM Sales.Orders
WHERE YEAR(orderdate) = 2007 AND MONTH(orderdate) = 2;

SELECT orderid, custid, empid, orderdate
FROM Sales.Orders
WHERE orderdate >= '20070201' AND orderdate < '20070301'

/*DATE AND TIME FUNCITONS*/
SELECT GETDATE() AS [Current date and time],
	CURRENT_TIMESTAMP AS [Same as GETDATE but ANSI], --USING CURRENT_TIMESTAMP IS STANDARD!!!
	GETUTCDATE() AS [Current date and time in UTC];
SELECT SYSDATETIME() AS [Current date and time],
	SYSUTCDATETIME() [Current date and time in UTC],
	SYSDATETIMEOFFSET() AS [Current date time including time zone];
/* Output: [May vary based on date time of execution of query]
Current date and time   Same as GETDATE but ANSI Current date and time in UTC
----------------------- ------------------------ ----------------------------
2012-12-10 08:41:01.387 2012-12-10 08:41:01.387  2012-12-10 03:11:01.387

Current date and time  Current date and time in UTC Current date time including time zone
---------------------- ---------------------------- -------------------------------------
2012-12-10 08:41:01.38 2012-12-10 03:11:01.3884883  2012-12-10 08:41:01.3884883 +05:30
*/

/*THE CAST AND CONVERT FUNCTIONS
CAST(value AS datatype)
CONVERT(datatype,value[,style_number])
--Eg, of style_number is nothing but the format of the datetime.

"NOTE: CAST is ANSI and CONVERT isn't, so unless you need style number
prefer using CAST as it will make the code standard."

*/

SELECT CAST(SYSDATETIME() AS DATE);
SELECT CAST(SYSDATETIME() AS TIME);

/*To convert a datetime into character format 'YYYYMMDD' is style_number 112*/
SELECT CONVERT(CHAR(8),CURRENT_TIMESTAMP,112) AS [DATE YYYYMMDD(112)];

/*When the code is converted back to DATETIME, you get the current time in the base date: 
1900-01-01 hh:mm:ss.nnn
*/
SELECT CAST(CONVERT(CHAR(12), CURRENT_TIMESTAMP, 114) AS DATETIME);

/*SWITCHOFFSET
The SWITCHOFFSET function adjusts an input DATETIMEOFFSET value to a specified time zone.
NOTE: This function takes only date time offset as input.
SWITCHOFFSET( datetimeoffset_value, time_zone )
*/
SELECT	SWITCHOFFSET(SYSDATETIMEOFFSET(),'-08:00') AS PST,
		SWITCHOFFSET(SYSDATETIMEOFFSET(),'+00:00') AS GMT,
		SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30') AS IST;


/*TODATETIMEOFFSET
The TODATETIMEOFFSET function sets the time zone offset of an input date and time value.
1. Not restricted to a datetimeoffset value as input
2. It doesn't try to ADJUST the time based on the time zone difference 
   between the source value and the specified time zone, rather simply 
   returns the input date and time value with the specified time zone 
   as a datetimeoffset value. 
*/

SELECT TODATETIMEOFFSET(SYSDATETIMEOFFSET(), '+05:30') AS IST;
SELECT TODATETIMEOFFSET(SYSDATETIME(), '+05:30') AS IST;--Takes even time as input.

/*DATEADD
The DATEADD function allows you to add a specified number of units of 
a specified date part to an input date and time value. 

DATEADD( part, n, dt_val ) 
Values for part include [year, quarter, month, dayofyear, day, week, weekday, hour, 
minute, second, millisecond, microsecond, and nanosecond]*/
SELECT DATEADD(YEAR,1,SYSDATETIME()) AS [NEXT YEAR SAME TIME];

/*DATEDIFF
The DATEDIFF function returns the difference between two date and 
time values in terms of a specified date part. 

DATEDIFF( part, dt_val1, dt_val2 )*/
SELECT DATEDIFF(day, '20080212', '20090211') AS [DIFFERENCE IN DAYS];

/*INTERESTING USE OF DATEADD AND DATEDIFF*/
/*TO GET THE FIRST DAY OF THE YEAR, MONTH USE DIFFERENT DATE PARTS AND FIRST DAY OF YEAR*/
SELECT DATEADD(YEAR,DATEDIFF(YEAR, '20110101', CURRENT_TIMESTAMP), '20110101') AS [FIRSTDAY_OF_YEAR]
SELECT DATEADD(MONTH,DATEDIFF(MONTH, '20110101', CURRENT_TIMESTAMP), '20110101') AS [FIRSTDAY_OF_MONTH]
SELECT DATEADD(DAY,DATEDIFF(DAY, '20110101', CURRENT_TIMESTAMP), '20110101') AS [TODAY]

/*TO GET THE LAST DAY OF MONTH, YEAR USE DIFFERENT DATE PARTS AND LAST DAY OF YEAR*/
SELECT DATEADD(YEAR,DATEDIFF(YEAR, '20111231', CURRENT_TIMESTAMP), '20111231') AS [LASTDAY_OF_YEAR]
SELECT DATEADD(MONTH,DATEDIFF(MONTH, '20111231', CURRENT_TIMESTAMP), '20111231') AS [LASTDAY_OF_MONTH]

/*DATEPART
The DATEPART function returns an integer representing a requested part of a given date and time value. 
DATEPART( dt_val, part ) 
*/

SELECT DATEPART(YEAR,'20121210'); --2012
SELECT DATEPART(MONTH,'20121210'); --12
SELECT DATEPART(DAY,'20121210'); --10

/*The YEAR, MONTH and DAY Functions
The YEAR, MONTH, and DAY functions are abbreviations for the DATEPART 
function returning the integer representation of the year, month, and 
day parts of an input date and time value.*/

SELECT	YEAR(CURRENT_TIMESTAMP) AS [YEAR],
		MONTH(CURRENT_TIMESTAMP) AS [MONTH],
		DAY(CURRENT_TIMESTAMP) AS [DAY]

/*The DATENAME Function
The DATENAME function returns a character string representing the part 
of a given date and time value. 
DATENAME( dt_val, part ) 
*/
SELECT	DATENAME(MONTH, CURRENT_TIMESTAMP) AS [MONTH]
SELECT	DATENAME(DAY, CURRENT_TIMESTAMP) AS [DAY]

/*ISDATE*/
SELECT ISDATE('20120229') AS [YES]
SELECT ISDATE('20120230') AS [NO]

--EOMONTH - SS2012 - Returns last day of the month.
SELECT EOMONTH(CURRENT_TIMESTAMP);