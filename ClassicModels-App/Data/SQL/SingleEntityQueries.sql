-- SINGLE ENTITY EXERCISES

-- 1. Prepare a list of offices sorted by country, state, city.
SELECT
	o.officeCode,
	o.city,
	o.phone,
	o.addressLine1,
	o.addressLine2,
	o.state,
	o.country,
	o.postalCode,
	o.territory
FROM
	Offices AS o
ORDER BY
	o.country, o.state, o.city;


-- 2. How many employees are there in the company?
SELECT
	COUNT(0) AS numbersOfEmployees
FROM
	Employees;


-- 3. What is the total of payments received?
SELECT
	COUNT(0) AS numberOfPayments,
	'$' || TO_CHAR(SUM(p.amount), '999,999,990.00') AS totalOfPayments
FROM
	Payments AS p;


-- 4. List the product lines that contain 'Cars'.
SELECT
	pl.productLine,
	pl.textDescription,
	pl.htmlDescription,
	pl.image
FROM
	ProductLines AS pl
WHERE
	pl.productLine LIKE '%Cars%';
	

-- 5. Report total payments for October 28, 2004.
SELECT
	'$' || TO_CHAR(SUM(p.amount), '999,999,990.00') AS totalOfPayments
FROM
	Payments as p
WHERE
	p.paymentDate = '2004-10-28';


-- 6. Report those payments greater than $100,000.
SELECT
	p.customerNumber,
	p.checkNumber,
	p.paymentDate,
	'$' || TO_CHAR(p.amount, '999,999,990.00') AS amount
FROM
	Payments AS p
WHERE
	p.amount > 100_000;


-- 7. List the products in each product line.
SELECT
	p.productCode,
	p.productName,
	p.productLine,
	p.productScale,
	p.productVendor,
	p.productDescription,
	p.quantityInStock,
	p.buyPrice,
	p.msrp
FROM
	Products AS p
ORDER BY
	p.productLine, p.productName;


-- 8. How many products in each product line?
SELECT
	p.productLine,
	COUNT(*) AS productCount
FROM
	Products AS p
GROUP BY
	p.productLine
ORDER BY
	p.productLine;


-- 9. What is the minimum payment received?
SELECT
	'$' || TO_CHAR(MIN(p.amount), '999,999,990.00') AS minimumPayment
FROM
	Payments AS p;


-- 10. List all payments greater than twice the average payment.
SELECT
	p.customerNumber,
	p.checkNumber,
	p.paymentDate,
	'$' || TO_CHAR(p.amount, '999,999,990.00') AS amount
FROM
	Payments AS p
WHERE
	p.amount > 2 * (
		SELECT AVG(p.amount) FROM Payments AS p
	);


-- 11. What is the average percentage markup of the MSRP on buyPrice?
SELECT
	TO_CHAR((AVG((p.msrp - p.buyprice) / p.buyprice) * 100), 'FM999999990.00') || '%' AS markup
FROM
	Products AS p;


-- 12. How many distinct products does ClassicModels sell?
SELECT
	COUNT(DISTINCT(p.productName)) AS distinctProductCount
FROM
	Products AS p;


-- 13. Report the name and city of customers who don't have sales representatives?
SELECT
	c.customerName,
	c.city
FROM
	Customers AS c
WHERE
	c.salesRepEmployeeNumber IS NULL;


-- 14. What are the names of executives with VP or Manager in their title? Use the CONCAT function to combine the employee's first name and last name into a single field for reporting.
SELECT
	e.firstName || ' ' || e.lastName AS employeeName,
	e.jobTitle
FROM
	Employees AS e
WHERE
	e.jobTitle LIKE '%VP%'
	OR e.jobTitle LIKE '%Manager%';


-- 15. Which orders have a value greater than $5,000?
SELECT
	od.orderNumber,
	od.productCode,
	od.quantityOrdered,
	'$' || TO_CHAR(od.priceEach, '999,999,990.00') AS priceEach,
	od.orderLineNumber,
	'$' || TO_CHAR(od.quantityOrdered * od.priceEach, '999,999,990.00') AS orderValue
FROM
	OrderDetails AS od
WHERE
	od.quantityOrdered * od.priceEach > 5_000
ORDER BY
	orderValue;
