-- ONE TO MANY RELATIONSHIP

-- 1. Report the account representative for each customer.
SELECT
	c.customerName,
	e.firstName || ' ' || e.lastName AS accountRepresentative
FROM
	Customers AS c
	INNER JOIN Employees AS e ON c.salesRepEmployeeNumber = e.employeeNumber;


-- 2. Report total payments for Atelier graphique.
SELECT
	c.customerName,
	'$' || TO_CHAR(SUM(p.amount), '999,999,990.00') AS totalPayments
FROM
	Payments AS p
	INNER JOIN Customers AS c ON p.customerNumber = c.customerNumber
WHERE
	c.customerName = 'Atelier graphique'
GROUP BY
	c.customerName;


-- 3. Report the total payments by date.
SELECT
	p.paymentDate,
	'$' || TO_CHAR(SUM(p.amount), '999,999,990.00') AS totalPayments
FROM
	Payments AS p
GROUP BY
	p.paymentDate;


-- 4. Report the products that have not been sold.
SELECT
	p.productCode,
	p.productName,
	o.orderNumber,
	o.status
FROM
	Orders AS O
	INNER JOIN OrderDetails AS od ON o.orderNumber = od.orderNumber
	INNER JOIN products AS p ON od.productCode = p.productCode
WHERE
	o.status <> 'Shipped'
ORDER BY
	p.productName;


-- 5. List the amount paid by each customer.
SELECT
	c.customerName, 
	'$' || TO_CHAR(SUM(p.amount), '999,999,990.00') AS totalPayments
FROM
	Payments AS p
	INNER JOIN Customers AS c ON p.customerNumber = c.customerNumber
GROUP BY
	c.customerName
ORDER BY
	c.customerName;


-- 6. How many orders have been placed by Herkku Gifts?
SELECT
	c.customerName,
	COUNT(o.orderNumber) AS numberOfOrders
FROM
	Orders AS o
	INNER JOIN Customers AS c ON o.customerNumber = c.customerNumber
WHERE
	c.customerName = 'Herkku Gifts'
GROUP BY
	c.customerName;


-- 7. Who are the employees in Boston?
SELECT
	e.employeeNumber,
	e.firstName || ' ' || e.lastName as employeeName,
	e.extension,
	e.email,
	e.reportsTo,
	e.jobTitle
FROM
	Employees AS e
	INNER JOIN Offices As o ON e.officeCode = o.officeCode
WHERE
	o.city = 'Boston';


-- 8. Report those payments greater than $100,000. Sort the report so the customer who made the highest payment appears first.
SELECT
	c.customerNumber,
	c.customerName,
	'$' || TO_CHAR(p.amount, '999,999,990.00') AS amount
FROM
	Payments AS p
	INNER JOIN Customers AS c ON p.customerNumber = c.customerNumber
WHERE
	p.amount > 100_000
ORDER BY
	p.amount DESC;


-- 9. List the value of 'On Hold' orders.
SELECT
	o.orderNumber,
	'$' || TO_CHAR(SUM(od.quantityOrdered * od.priceEach), '999,999,990.00') AS orderValue
FROM
	OrderDetails AS od
	INNER JOIN Orders AS o ON od.orderNumber = o.orderNumber
WHERE
	o.status = 'On Hold'
GROUP BY
	o.orderNumber;


-- 10. Report the number of orders 'On Hold' for each customer.
SELECT
	c.customerNumber,
	COUNT(o.orderNumber) AS ordersOnHold
FROM
	Orders AS o
	INNER JOIN Customers AS c ON o.customerNumber = c.customerNumber
WHERE
	o.status = 'On Hold'
GROUP BY
	c.customerNumber;
