-- DROP Foreign Keys 
SET NOCOUNT ON
DECLARE @fk_id int, @fk_name nvarchar(500), @parent_id int, @parent_name nvarchar(500), @schema_name nvarchar(500)
declare @sql nvarchar(2000)

DECLARE fk_cursor CURSOR FOR 
SELECT [name] as fkname, schema_name([SCHEMA_ID]) as schemaname,object_name(parent_object_id) as parentname 
from sys.objects where [type] = 'F' 

OPEN fk_cursor

FETCH NEXT FROM fk_cursor 
INTO @fk_name, @schema_name, @parent_name

WHILE @@FETCH_STATUS = 0
BEGIN
	 SET @sql = N''
	 SET @sql = N'ALTER TABLE [' + @schema_name + N'].[' + @parent_name + N'] DROP ' + @fk_name 
	 -- EXEC(@sql)    -- Don't use this unless you mean it!
	 -- PRINT N'Executed: ' + @sql
	 PRINT @sql
	 FETCH NEXT FROM fk_cursor INTO @fk_name, @schema_name, @parent_name
END

CLOSE fk_cursor
DEALLOCATE fk_cursor




-- DROP Prodeures
DECLARE @pk_id int, @pk_name nvarchar(500)

DECLARE pk_cursor CURSOR FOR 
SELECT [name] as pkname
from sysobjects where [type] = 'P' 

OPEN pk_cursor

FETCH NEXT FROM pk_cursor 
INTO @pk_name

WHILE @@FETCH_STATUS = 0
BEGIN
	 SET @sql = N''
	 SET @sql = N'DROP PROCEDURE [' + @pk_name + ']'
	 -- EXEC(@sql)    -- Don't use this unless you mean it!
	 -- PRINT N'Executed: ' + @sql
	 PRINT @sql
	 FETCH NEXT FROM pk_cursor INTO @pk_name
END

CLOSE pk_cursor
DEALLOCATE pk_cursor





-- DROP user tables
SET NOCOUNT ON
DECLARE @tbl_name nvarchar(500)


DECLARE tbl_cursor CURSOR FOR 
SELECT schema_name([SCHEMA_ID]) as schemaname, [name] 
from sys.objects where [type] = 'U' order by [name]


OPEN tbl_cursor

FETCH NEXT FROM tbl_cursor 
INTO @schema_name, @tbl_name

WHILE @@FETCH_STATUS = 0
BEGIN
	 SET @sql = N'DROP TABLE [' + @schema_name + '].[' + @tbl_name + N'] '
	 -- EXEC(@sql)    -- Don't use this unless you mean it!
	 -- PRINT N'Executed: ' + @sql
	 PRINT @sql

	 FETCH NEXT FROM tbl_cursor INTO @schema_name, @tbl_name

END

CLOSE tbl_cursor
DEALLOCATE tbl_cursor