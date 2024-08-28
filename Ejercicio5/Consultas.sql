SELECT 
    p.ProductName AS ProductName,
    c.CategoryName AS CategoryName
FROM 
    Products p
JOIN 
    Categories c ON p.CategoryID = c.CategoryID
WHERE 
    p.Discontinued = 0  
ORDER BY 
    p.ProductName;


SELECT DISTINCT 
    c.CompanyName AS CustomerName
FROM 
    Customers c
JOIN 
    Orders o ON c.CustomerID = o.CustomerID
JOIN 
    Employees e ON o.EmployeeID = e.EmployeeID
WHERE 
    e.LastName = 'Davolio'
    AND e.FirstName = 'Nancy';


SELECT 
    YEAR(o.OrderDate) AS Year,
    SUM(od.UnitPrice * od.Quantity * (1 - od.Discount)) AS TotalAmount
FROM 
    Orders o
JOIN 
    OrderDetails od ON o.OrderID = od.OrderID
JOIN 
    Employees e ON o.EmployeeID = e.EmployeeID
WHERE 
    e.LastName = 'Buchanan'
    AND e.FirstName = 'Steven'
GROUP BY 
    YEAR(o.OrderDate)
ORDER BY 
    Year;


WITH EmployeeHierarchy AS (

    SELECT 
        EmployeeID,
        LastName,
        FirstName,
        ReportsTo
    FROM 
        Employees
    WHERE 
        LastName = 'Fuller'
        AND FirstName = 'Andrew'
    
    UNION ALL
    
    SELECT 
        e.EmployeeID,
        e.LastName,
        e.FirstName,
        e.ReportsTo
    FROM 
        Employees e
    INNER JOIN 
        EmployeeHierarchy eh ON e.ReportsTo = eh.EmployeeID
)
SELECT 
    LastName,
    FirstName
FROM 
    EmployeeHierarchy
WHERE 
    EmployeeID <> (SELECT EmployeeID FROM Employees WHERE LastName = 'Fuller' AND FirstName = 'Andrew');
