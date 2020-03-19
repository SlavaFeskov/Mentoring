SELECT c.ContactName, COUNT(o.CustomerID) as [Amount of orders]
FROM Northwind.Customers as c
LEFT JOIN Northwind.Orders as o
ON c.CustomerID = o.CustomerID
GROUP BY c.ContactName
ORDER BY COUNT(o.CustomerID)