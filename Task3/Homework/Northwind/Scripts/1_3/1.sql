SELECT DISTINCT od.OrderID
FROM Northwind.[Order Details] as od
WHERE od.Quantity BETWEEN 3 AND 10