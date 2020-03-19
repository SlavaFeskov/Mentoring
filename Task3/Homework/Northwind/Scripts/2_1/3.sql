SELECT COUNT(*)
FROM (SELECT DISTINCT o.CustomerID
FROM Northwind.Orders as o) as c