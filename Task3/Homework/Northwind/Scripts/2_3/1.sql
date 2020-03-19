SELECT DISTINCT e.EmployeeID
FROM Northwind.Employees as e
JOIN Northwind.EmployeeTerritories as et
ON et.EmployeeID = e.EmployeeID
JOIN Northwind.Territories as t
ON t.TerritoryID = et.TerritoryID
JOIN Northwind.[Regions] as r
ON r.RegionID = t.RegionID
WHERE r.RegionDescription = 'Western'