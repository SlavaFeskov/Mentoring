SELECT o.EmployeeID, o.CustomerID, COUNT(*) as [Count of orders for customer from employee]
FROM Northwind.Orders as o
WHERE o.OrderDate BETWEEN '1998-01-01' AND '1998-12-31'
GROUP BY o.EmployeeID, o.CustomerID
ORDER BY o.EmployeeID;

-- test query
SELECT o.EmployeeID, o.CustomerID
FROM Northwind.Orders as o
WHERE o.EmployeeId = 1
AND o.OrderDate BETWEEN '1998-01-01' AND '1998-12-31'
ORDER BY o.CustomerID