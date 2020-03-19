SELECT COUNT(*) - COUNT(o.ShippedDate) as [Count of not shipped]
FROM Northwind.Orders as o