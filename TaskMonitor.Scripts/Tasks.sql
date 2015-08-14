-- Get all tasks
SELECT * FROM dbo.tasks
ORDER BY StartDate DESC;

--Tasks in done last year
SELECT * FROM dbo.Tasks
WHERE DATEDIFF(MONTH,StartDate,GETUTCDATE()) <= 1
ORDER BY StartDate DESC;

--Tasks done in last month
SELECT * FROM dbo.Tasks
WHERE DATEDIFF(MONTH,StartDate,GETUTCDATE()) <= 12
ORDER BY StartDate DESC;

--Tasks done last week
SELECT * FROM dbo.Tasks
WHERE DATEDIFF(DAY,StartDate,GETUTCDATE()) <= 7
ORDER BY StartDate DESC;

--Tasks done today
SELECT * FROM dbo.Tasks
WHERE DATEDIFF(DAY,StartDate,GETUTCDATE()) <= 1
ORDER BY StartDate DESC;

SELECT [Description] AS Task, DATEDIFF(MINUTE,StartDate, EndDate) AS [Time in Minutes]
FROM dbo.Tasks 
WHERE [Description] LIKE '%code%'
ORDER BY StartDate DESC

SELECT SUM(DATEDIFF(MINUTE,StartDate, EndDate)) AS [Time in Minutes]
FROM dbo.Tasks 
WHERE [Description] LIKE '%code%'
--Queries to be written!

