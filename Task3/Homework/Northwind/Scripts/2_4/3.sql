SELECT c.ContactName
FROM Northwind.Customers as c
WHERE NOT EXISTS (SELECT o.OrderID FROM Northwind.Orders as o WHERE o.CustomerID = c.CustomerID)