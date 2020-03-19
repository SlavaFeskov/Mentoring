SELECT o.OrderID,
CASE
	WHEN o.ShippedDate IS NUll THEN 'Not Shipped'
	ELSE CONVERT(nvarchar, o.ShippedDate, 23)
END as ShippedDate
FROM Northwind.Orders as o