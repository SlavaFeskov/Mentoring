SELECT SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) as [Totals]
FROM Northwind.[Order Details] as od