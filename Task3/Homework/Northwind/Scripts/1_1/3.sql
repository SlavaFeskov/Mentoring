SELECT o.OrderID as [Order Number], 
CASE 
	WHEN o.ShippedDate IS NULL THEN 'Not Shipped'
	ELSE CONVERT(nvarchar, o.ShippedDate, 23) 
END as [Shipped Date]
FROM Northwind.Orders as o
WHERE o.ShippedDate > '1998-05-06'
OR o.ShippedDate IS NULL