SELECT e.City, e.FirstName, c.ContactName
FROM Northwind.Employees as e,
Northwind.Customers as c
WHERE c.City = e.City
ORDER BY e.City