/*Working with a cursor generally involves the following steps:
1.  Declare the cursor based on a query.
2.  Open the cursor.
3.  Fetch attribute values from the first cursor record into variables.
4.  While the end of the cursor is not reached (the value of a function 
	called @@FETCH_ STATUS is 0), loop through the cursor records; in each 
	iteration of the loop fetch attribute values from the current cursor 
	record into variables, and perform the processing needed for the current row.
5.  Close the cursor.
6.  Deallocate the cursor. 
*/

USE TSQLFundamentals2008;
GO

SET NOCOUNT ON;

DECLARE @result TABLE
(
	custid INT,
	ordermonth DATETIME,
	qty INT,
	runqty INT,
	PRIMARY KEY(custid, ordermonth)
);

DECLARE 
	@custid INT,
	@prevcustid INT,
	@ordermonth DATETIME,
	@qty INT,
	@runqty INT;
	
DECLARE C CURSOR FAST_FORWARD FOR /*READ ONLY - FORWARD ONLY*/
SELECT custid,ordermonth,qty
FROM Sales.CustOrders
ORDER BY custid,ordermonth;


OPEN C 

FETCH FROM C INTO @custid,@ordermonth,@qty;

SELECT @prevcustid = @custid, @runqty = 0;

WHILE @@FETCH_STATUS = 0
BEGIN
	IF @custid <> @prevcustid
		SELECT @prevcustid = @custid, @runqty = 0;
	SET @runqty = @runqty + @qty;
	INSERT INTO @result VALUES (@custid, @ordermonth, @qty, @runqty);
	FETCH FROM C INTO @custid, @ordermonth, @qty;
END

CLOSE c;

DEALLOCATE C;

SELECT custid, CONVERT(VARCHAR(7), ordermonth, 121) as ordermonth , qty, runqty
FROM @result
ORDER BY custid,ordermonth;