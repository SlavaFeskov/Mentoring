SELECT c.CustomerID, c.Country
FROM Northwind.Customers as c
WHERE SUBSTRING(c.Country, 1, 1) BETWEEN 'b' AND 'g'
ORDER BY c.Country