USE TSQLFundamentals2008;
GO

USE tempdb;

IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;

CREATE TABLE dbo.Orders
(
  orderid   INT         NOT NULL,
  orderdate DATE        NOT NULL,
  empid     INT         NOT NULL,
  custid    VARCHAR(5)  NOT NULL,
  qty       INT         NOT NULL,
  CONSTRAINT PK_Orders PRIMARY KEY(orderid)
);

INSERT INTO dbo.Orders(orderid, orderdate, empid, custid, qty)
VALUES
  (30001, '20070802', 3, 'A', 10),
  (10001, '20071224', 2, 'A', 12),
  (10005, '20071224', 1, 'B', 20),
  (40001, '20080109', 2, 'A', 40),
  (10006, '20080118', 1, 'C', 14),
  (20001, '20080212', 2, 'B', 12),
  (40005, '20090212', 3, 'A', 10),
  (20002, '20090216', 1, 'C', 20),
  (30003, '20090418', 2, 'B', 15),
  (30004, '20070418', 3, 'C', 22),
  (30007, '20090907', 3, 'D', 30);

SELECT * FROM dbo.Orders;

GO

SELECT empid,custid,SUM(qty)
FROM dbo.Orders
GROUP BY empid, custid
ORDER BY empid, custid;

/* What we need is a Pivoted View of Total Quantity per 
Employee (On Rows) and Customer (On Columns) 
empid 	A 		B 		C 		D 
1 		NULL 	20 		34 		NULL 
2 		52 		27 		NULL 	NULL 
3 		20 		NULL 	22 		30

This can be obtained in two ways as shown below:
*/

/*1. By using CASE WHEN and SUM*/
SELECT empid,
  SUM(CASE WHEN custid = 'A' THEN qty END) AS A,
  SUM(CASE WHEN custid = 'B' THEN qty END) AS B,
  SUM(CASE WHEN custid = 'C' THEN qty END) AS C,
  SUM(CASE WHEN custid = 'D' THEN qty END) AS D
FROM dbo.Orders
GROUP BY empid;

SELECT empid, A, B, C, D
FROM (SELECT custid,empid,qty FROM
 dbo.Orders) AS D
PIVOT(sum(qty) FOR custid in (A,B,C,D)) AS P;