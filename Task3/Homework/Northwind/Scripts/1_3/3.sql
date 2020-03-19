SELECT c.CustomerID, c.Country
FROM Northwind.Customers as c
WHERE SUBSTRING(c.Country, 1, 1) >= 'b'
AND SUBSTRING(c.Country, 1, 1) <= 'g'
ORDER BY c.Country