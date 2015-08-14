/*Flow Elements*/
USE TSQLFundamentals2008;
GO
/*IF ELSE Flow Element*/
IF YEAR(CURRENT_TIMESTAMP) <> YEAR(DATEADD(DAY,1,CURRENT_TIMESTAMP))
	PRINT 'Today is last day of year.'
ELSE
	PRINT 'Today is not last day of year.'

/*IF Inside IF - ELSE*/
IF YEAR(CURRENT_TIMESTAMP) <> YEAR(DATEADD(DAY,1,CURRENT_TIMESTAMP))
	PRINT 'Today is last day of year.'
ELSE
	IF MONTH(CURRENT_TIMESTAMP) <> MONTH(DATEADD(DAY,1,CURRENT_TIMESTAMP))
		PRINT 'Today is last day of the month but not last day of year.'
	ELSE
		PRINT'Today is neither the last day of the month nor the last day of the year'

/*Use BEGIN and END statements to have multiple statements in the IF ELSE blocks.*/
IF DAY(CURRENT_TIMESTAMP) = 1
BEGIN
  PRINT 'Today is the first day of the month.';
  PRINT 'Starting a full database backup.';
  BACKUP DATABASE TSQLFundamentals2008
    TO DISK = 'C:\Temp\TSQLFundamentals2008_Full.BAK' WITH INIT;
  PRINT 'Finished full database backup.';
END
ELSE
BEGIN
  PRINT 'Today is not the first day of the month.'
  PRINT 'Starting a differential database backup.';
  BACKUP DATABASE TSQLFundamentals2008
    TO DISK = 'C:\Temp\TSQLFundamentals2008_Diff.BAK' WITH INIT;
  PRINT 'Finished differential database backup.';
END

/*WHILE Flow Element*/
/* BREAK to break the execution and CONTINUE to proceed with next iteration*/
DECLARE @i as INT;
SET @i = 0;
WHILE @i<=10
BEGIN
	IF @i > 5 	
		PRINT @i;
	SET @i = @i + 1;
END