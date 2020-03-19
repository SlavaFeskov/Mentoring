SELECT s.CompanyName
FROM Northwind.Suppliers as s
WHERE s.SupplierID IN (SELECT p.SupplierID FROM Northwind.Products as p WHERE p.UnitsInStock = 0)