using ClassicModels.Models;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace ClassicModels.Data.Exercises
{
    public class SingleEntityExercises
    {
        public static readonly List<Exercise> ExerciseList = [
            new (
                1, "Prepare a list of offices sorted by country, state, city.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Offices
                        .OrderBy(office => office.Country)
                        .ThenBy(office => office.State)
                        .ThenBy(office => office.City)
                        .Select(office => new
                        {
                            office.OfficeCode,
                            office.City,
                            office.Phone,
                            office.AddressLine1,
                            office.AddressLine2,
                            office.State,
                            office.Country,
                            office.PostalCode,
                            office.Territory
                        })
                        .ToList();
                }
            ),
            new (
                2, "How many employees are there in the company?",
                () =>
                {
                    var context = new AppDbContext();

                    return [ new { employeeCount = context.Employees.Count() } ];
                }
            ),
            new (
                3, "What is the total of payments received?",
                () =>
                {
                    var context = new AppDbContext();

                    return [
                        new { totalOfPayments = '$' + context.Payments.Sum(p => p.Amount).ToString("N2", new CultureInfo("en-US")) }
                    ];
                }
            ),
            new (
                4, "List the product lines that contain 'Cars'.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.ProductLines
                        .Where(
                            productLine => EF.Functions.Like(productLine.ProductLineName, "%Cars%")
                        )
                        .Select(productLine => new
                        {
                            productLine.ProductLineName,
                            productLine.TextDescription,
                            productLine.HtmlDescription,
                            productLine.Image
                        })
                        .ToList();
                }
            ),
            new (
                5, "Report total payments for October 28, 2004.",
                () =>
                {
                    var context = new AppDbContext();

                    var payments = context.Payments
                        .Where(p => p.PaymentDate == new DateTime(2004, 10, 28).ToUniversalTime())
                        .Sum(p => p.Amount);

                    return [ new { payments = '$' + payments.ToString("N2", new CultureInfo("en-US")) } ];
                }
            ),
            new (
                6, "Report those payments greater than $100,000.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Payments
                        .Where(payment => payment.Amount > 100_000)
                        .Select(payment => new
                        {
                            payment.CustomerNumber,
                            payment.CheckNumber,
                            payment.PaymentDate,
                            Amount = '$' + payment.Amount.ToString("N2", new CultureInfo("en-US"))
                        })
                        .ToList();
                }
            ),
            new (
                7, "List the products in each product line.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Products
                        .OrderBy(p => p.ProductLineName)
                        .ThenBy(p => p.ProductName)
                        .Select(p => new
                        {
                            p.ProductCode,
                            p.ProductLineName,
                            p.ProductName,
                            p.ProductDescription,
                            p.ProductScale,
                            p.ProductVendor,
                            p.QuantityInStock,
                            p.BuyPrice,
                            p.Msrp
                        })
                        .ToList();
                }
            ),
            new (
                8, "How many products in each product line?",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Products
                        .GroupBy(product => product.ProductLineName)
                        .Select(group => new
                        {
                            ProductLineName = group.Key,
                            ProductCount = group.Count()
                        })
                        .OrderBy(group => group.ProductLineName)
                        .ToList();
                }
            ),
            new (
                9, "What is the minimum payment received?",
                () =>
                {
                    var context = new AppDbContext();

                    return [ new
                    {
                        minimumPayment = '$' + context.Payments.Min(payment => payment.Amount).ToString("N2", new CultureInfo("en-US"))
                    }];
                }
            ),
            new (
                10, "List all payments greater than twice the average payment.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Payments
                        .Where(payment =>
                            payment.Amount > 2 * context.Payments.Average(payment => payment.Amount)
                        )
                        .Select(payment => new
                        {
                            payment.CustomerNumber,
                            payment.CheckNumber,
                            payment.PaymentDate,
                            Amount = '$' + payment.Amount.ToString("N2", new CultureInfo("en-US"))
                        })
                        .ToList();
                }
            ),
            new (
                11, "What is the average percentage markup of the MSRP on buyPrice?",
                () =>
                {
                    var context = new AppDbContext();

                    var averageMarkup = context.Products
                        .Average(p => (p.Msrp - p.BuyPrice) / p.BuyPrice) * 100;

                    return [
                        new { avgMarkup = averageMarkup.ToString("F2", new CultureInfo("en-US")) + '%' }
                    ];
                }
            ),
            new (
                12, "How many distinct products does ClassicModels sell?",
                () =>
                {
                    var context = new AppDbContext();

                    var distinctProductCount = context.Products
                        .Select(product => product.ProductName)
                        .Distinct()
                        .Count();

                    return [ new { distinctProductCount } ];
                }
            ),
            new (
                13, "Report the name and city of customers who don't have sales representatives?",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Customers
                        .Where(customer => customer.SalesRepEmployeeNumber == null)
                        .Select(customer => new
                        {
                            customer.CustomerName,
                            customer.City
                        })
                        .ToList();
                }
            ),
            new (
                14, "What are the names of executives with VP or Manager in their title?",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Employees
                        .Where(employee =>
                            EF.Functions.Like(employee.JobTitle, "%VP%")
                            || EF.Functions.Like(employee.JobTitle, "%Manager%")
                        )
                        .Select(employee => new
                        {
                            EmployeeName = employee.FirstName + ' ' + employee.LastName,
                            employee.JobTitle
                        })
                        .ToList();
                }
            ),
            new (
                15, "Which orders have a value greater than $5,000?",
                () =>
                {
                    var context = new AppDbContext();

                    return context.OrderDetails
                        .Where(orderDetail => orderDetail.PriceEach * orderDetail.QuantityOrdered > 5000)
                        .Select(orderDetail=> new
                        {
                            orderDetail.OrderNumber,
                            orderDetail.ProductCode,
                            orderDetail.QuantityOrdered,
                            orderDetail.PriceEach,
                            orderDetail.OrderLineNumber
                        })
                        .ToList();
                }
            )
        ];
    }
}
