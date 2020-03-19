SELECT 
	(SELECT e.LastName + ' ' + e.FirstName 
		FROM Northwind.Employees as e 
		WHERE e.EmployeeID = o.EmployeeID) as [Seller], 
	COUNT(*) as [Amount]
FROM Northwind.Orders as o
GROUP BY o.EmployeeID
ORDER BY COUNT(*) DESC