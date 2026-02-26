
--Level 1: The Join Foundation

--Basic Linking :List all Product Names along with their Category Names.
SELECT p.ProductName, c.CategoryName
FROM Products p
INNER JOIN Categories c
ON p.CategoryID = c.CategoryID;

--Order Details : Display every Order ID alongside the Company Name of the customer who placed it.
SELECT o.OrderID, c.CompanyName
FROM Orders o
INNER JOIN Customers c
ON o.CustomerID = c.CustomerID;

--Supplier Tracking: Show all Product Names and the Company Name of their respective suppliers.
SELECT p.ProductName, s.CompanyName
FROM Products p
INNER JOIN Suppliers s
ON p.SupplierID = s.SupplierID;


--Employee Sales : List all Orders (ID and Date) and the First/Last Name of the employee who processed them.
SELECT o.OrderID, o.OrderDate, e.FirstName, e.LastName
FROM Orders o
INNER JOIN Employees e
ON o.EmployeeID = e.EmployeeID;

--International Logistics: Find all Orders shipped to France.
SELECT o.OrderID, s.CompanyName AS ShipperName
FROM Orders o
INNER JOIN Shippers s
ON o.ShipVia = s.ShipperID
WHERE o.ShipCountry = 'France';


--Level 2 Aggregations with Joins

--Category Stock
SELECT c.CategoryName, SUM(p.UnitsInStock) AS TotalUnitsInStock
FROM Categories c
INNER JOIN Products p
ON c.CategoryID = p.CategoryID
GROUP BY c.CategoryName;

--Customer Spend
SELECT c.CompanyName,
       SUM(od.UnitPrice * od.Quantity) AS TotalSpent
FROM Customers c
INNER JOIN Orders o ON c.CustomerID = o.CustomerID
INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
GROUP BY c.CompanyName;

--Employee Performance
SELECT e.LastName,
       COUNT(o.OrderID) AS TotalOrders
FROM Employees e
INNER JOIN Orders o
ON e.EmployeeID = o.EmployeeID
GROUP BY e.LastName;

--Shipping Costs
SELECT s.CompanyName,
       SUM(o.Freight) AS TotalFreight
FROM Shippers s
INNER JOIN Orders o
ON s.ShipperID = o.ShipVia
GROUP BY s.CompanyName;


--Top 5 Products by Quantity Sold
SELECT TOP 5 p.ProductName,
       SUM(od.Quantity) AS TotalQuantitySold
FROM Products p
INNER JOIN [Order Details] od
ON p.ProductID = od.ProductID
GROUP BY p.ProductName
ORDER BY TotalQuantitySold DESC;



--Level 3: Subqueries & Self-Joins

--Above Average Price
SELECT ProductName, UnitPrice
FROM Products
WHERE UnitPrice > (
    SELECT AVG(UnitPrice) FROM Products
);

--The Bosses (Self Join)
SELECT e.FirstName + ' ' + e.LastName AS Employee,
       m.FirstName + ' ' + m.LastName AS Manager
FROM Employees e
LEFT JOIN Employees m
ON e.ReportsTo = m.EmployeeID;

--Customers with No Orders
SELECT CompanyName
FROM Customers c
WHERE NOT EXISTS (
    SELECT 1 FROM Orders o
    WHERE o.CustomerID = c.CustomerID
);

--High-Value Orders
SELECT o.OrderID
FROM Orders o
INNER JOIN [Order Details] od
ON o.OrderID = od.OrderID
GROUP BY o.OrderID
HAVING SUM(od.UnitPrice * od.Quantity) >
(
    SELECT AVG(OrderTotal)
    FROM (
        SELECT SUM(UnitPrice * Quantity) AS OrderTotal
        FROM [Order Details]
        GROUP BY OrderID
    ) AS OrderAverages
);

--Late Bloomers
SELECT ProductName
FROM Products p
WHERE NOT EXISTS (
    SELECT 1
    FROM [Order Details] od
    INNER JOIN Orders o
    ON od.OrderID = o.OrderID
    WHERE od.ProductID = p.ProductID
      AND YEAR(o.OrderDate) > 1997
);


--Level 4: Complex Logic & Advanced Joins


--Territory Coverage
SELECT e.FirstName + ' ' + e.LastName AS EmployeeName,
       r.RegionDescription
FROM Employees e
INNER JOIN EmployeeTerritories et
ON e.EmployeeID = et.EmployeeID
INNER JOIN Territories t
ON et.TerritoryID = t.TerritoryID
INNER JOIN Region r
ON t.RegionID = r.RegionID;

--Duplicate Cities
SELECT c.CompanyName AS CustomerName,
       s.CompanyName AS SupplierName,
       c.City
FROM Customers c
INNER JOIN Suppliers s
ON c.City = s.City;


--Multi-Category Customers
SELECT c.CompanyName
FROM Customers c
INNER JOIN Orders o ON c.CustomerID = o.CustomerID
INNER JOIN [Order Details] od ON o.OrderID = od.OrderID
INNER JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CompanyName
HAVING COUNT(DISTINCT p.CategoryID) > 3;

--Discontinued Sales Revenue
SELECT SUM(od.UnitPrice * od.Quantity) AS DiscontinuedRevenue
FROM [Order Details] od
INNER JOIN Products p
ON od.ProductID = p.ProductID
WHERE p.Discontinued = 1;

--Most Expensive Product in Each Category
SELECT c.CategoryName,
       p.ProductName,
       p.UnitPrice
FROM Products p
INNER JOIN Categories c
ON p.CategoryID = c.CategoryID
WHERE p.UnitPrice = (
    SELECT MAX(UnitPrice)
    FROM Products
    WHERE CategoryID = p.CategoryID
);