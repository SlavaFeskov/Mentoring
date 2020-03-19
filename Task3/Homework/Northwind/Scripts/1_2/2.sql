SELECT c.ContactName, c.Country
FROM Northwind.Customers as c
WHERE c.Country NOT IN ('USA', 'Canada')
ORDER BY c.ContactName