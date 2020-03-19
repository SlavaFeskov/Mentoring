SELECT 
	e1.FirstName as Employee, 
CASE 
	WHEN (SELECT e2.FirstName
		FROM Northwind.Employees as e2 
		WHERE e1.ReportsTo = e2.EmployeeID) IS NULL THEN 'boss of bosses'
	ELSE (SELECT e2.FirstName
		FROM Northwind.Employees as e2 
		WHERE e1.ReportsTo = e2.EmployeeID)
END
FROM Northwind.Employees as e1

SELECT e1.FirstName,
CASE 
	WHEN e2.FirstName IS NULL THEN 'boss of bosses'
	ELSE e2.FirstName
END
FROM Northwind.Employees as e1
LEFT JOIN Northwind.Employees as e2
ON e1.ReportsTo = e2.EmployeeID