SELECT o.OrderID, o.ShippedDate, o.ShipVia
FROM Northwind.Orders as o
WHERE o.ShippedDate >= '1998-05-06'
AND o.ShipVia >= 2