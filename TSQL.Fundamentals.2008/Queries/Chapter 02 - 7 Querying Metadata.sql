USE TSQLFundamentals2008;
GO

/*QUERYING METADATA*/
/*CATALOG VIEWS, INFORMATION SCHEMA VIEWS, SYSTEM STORED PROCEDURES, AND FUNCTIONS*/

/*CATALOG VIEWS - Detailed information about the objects in the database*/
SELECT SCHEMA_NAME(schema_id) AS [schema_name], name AS table_name
FROM sys.tables;

SELECT name AS column_name,
  TYPE_NAME(system_type_id) AS column_type,
  max_length,
  collation_name,
  is_nullable
FROM sys.columns 
WHERE object_id=OBJECT_ID(N'Sales.Orders');

/*INFORMATION SCHEMA VIEWS
Information schema views are a set of views that reside in a schema 
called INFORMATION_ SCHEMA and provide metadata information in a 
standard manner. That is, the views are defined in the ANSI SQL 
standard, so naturally they don't cover SQL Server specific aspects.*/

SELECT TABLE_SCHEMA, TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = N'BASE TABLE';

SELECT
  COLUMN_NAME, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH,
  COLLATION_NAME, IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = N'Sales'
  AND TABLE_NAME = N'Orders';

/*System Stored Procedures and Functions*/
/*The sp_tables stored procedure returns a list of objects 
(such as tables and views) that can be queried in the 
current database: */
EXEC sys.sp_tables;

/*The sp_help procedure accepts an object name as input and 
returns multiple result sets with general information about 
the object, and also information about columns, indexes, 
constraints, and more.*/
EXEC sys.sp_help
  @objname = N'Sales.Orders';

EXEC sys.sp_help
  @objname = N'HR.Employees';
  
/*The sp_columns procedure returns information about columns 
in an object. For example, the following code returns 
information about columns in the Orders table: */
EXEC sys.sp_columns
  @table_name = N'Orders',
  @table_owner = N'Sales';

/*The sp_helpconstraint procedure returns information about
constraints in an object. For example, the following code 
returns information about constraints in the Orders table: */
EXEC sys.sp_helpconstraint
  @objname = N'Sales.Orders';
  
/*One set of functions returns information about properties 
of entities such as the SQL Server instance, database, object, 
column, and so on. The SERVERPROPERTY function returns the 
requested property of the current instance. For example, the 
following code returns the product level (such as RTM, SP1, 
SP2, and so on) of the current instance: */
SELECT SERVERPROPERTY('ProductLevel');

/*The DATABASEPROPERTYEX function returns the requested property 
of the given database name. For example, the following code returns 
the collation of the TSQLFundamentals2008 database: */
SELECT DATABASEPROPERTYEX(N'TSQLFundamentals2008', 'Collation')
 
/*The OBJECTPROPERTY function returns the requested property of 
the given object name. For example, the output of the following 
code indicates whether the Orders table has a primary key: */
SELECT OBJECTPROPERTY(OBJECT_ID(N'Sales.Orders'), 'TableHasPrimaryKey');

/*Notice the nesting of the function OBJECT_ID within OBJECTPROPERTY. 
The OBJECTPROPERTY function expects an object ID and not a name, so 
the OBJECT_ID function is used to return the ID of the Orders table.*/
 
/*The COLUMNPROPERTY function returns the requested property of a given 
column. For example, the output of the following code indicates whether 
the shipcountry column in the Orders table is NULLable:*/

SELECT COLUMNPROPERTY(OBJECT_ID(N'Sales.Orders'), N'shipcountry', 'AllowsNull');