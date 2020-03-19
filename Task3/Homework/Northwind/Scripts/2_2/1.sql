SELECT DATEPART(YEAR, o.OrderDate) as [Year], COUNT(o.OrderDate) as [Total]
FROM Northwind.Orders as o
GROUP BY DATEPART(YEAR, o.OrderDate)

DECLARE @r1 INT = 0;
DECLARE @r2 INT = 0;

SET @r1 = (SELECT COUNT(*)
FROM Northwind.Orders as o);

SET @r2 = (SELECT SUM(r.Total)
FROM (SELECT DATEPART(YEAR, o.OrderDate) as [Year], COUNT(o.OrderDate) as [Total]
FROM Northwind.Orders as o
GROUP BY DATEPART(YEAR, o.OrderDate)) as r);

SELECT
CASE 
	WHEN @r1 = @r2 THEN 'Success'
	ELSE 'Fail'
END as Result