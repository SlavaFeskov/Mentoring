SELECT DISTINCT c1.City, c2.ContactName
FROM Northwind.Customers as c1,
Northwind.Customers as c2
WHERE c1.City = c2.City
AND c1.CustomerID != c2.CustomerID
ORDER BY c1.City