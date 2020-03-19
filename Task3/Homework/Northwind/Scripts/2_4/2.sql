SELECT e.FirstName
FROM Northwind.Employees as e
WHERE (SELECT COUNT(*) FROM Northwind.Orders as o WHERE o.EmployeeID = e.EmployeeID) > 150