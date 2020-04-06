SELECT o.OrderID,
CASE
	WHEN o.ShippedDate IS NUll THEN 'Not Shipped'
END as ShippedDate
FROM Northwind.Orders as o
WHERE o.ShippedDate IS NULL