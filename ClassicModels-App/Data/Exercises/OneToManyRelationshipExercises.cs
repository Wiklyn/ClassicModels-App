﻿using ClassicModels.Models;
using System.Globalization;

namespace ClassicModels.Data.Exercises
{
    public class OneToManyRelationshipExercises
    {
        public static readonly List<Exercise> ExerciseList = [
            new (
                1, "Report the account representative for each customer.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Customers
                        .Where(customer => customer.Employee != null)
                        .Select(customer => new
                        {
                            customer.CustomerNumber,
                            customer.CustomerName,
                            AccountRepNumber = customer.Employee.EmployeeNumber,
                            AccountRepName = customer.Employee.FirstName + " " + customer.Employee.LastName
                        })
                        .ToList();
                }
            ),
            new (
                2, "Report total payments for Atelier graphique.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Payments
                        .Where(payment => payment.Customer.CustomerName == "Atelier graphique")
                        .GroupBy(payment => payment.Customer.CustomerName)
                        .Select(group => new
                        {
                            CustomerName = group.Key,
                            TotalPayments = '$' + group.Sum(payment => payment.Amount).ToString("N2", new CultureInfo("en-US"))
                        })
                        .ToList();
                }
            ),
            new (
                3, "Report the total payments by date.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Payments
                        .GroupBy(payment => payment.PaymentDate)
                        .Select(group => new
                        {
                            PaymentDate = group.Key,
                            TotalPayments = '$' + group.Sum(payment => payment.Amount).ToString("N2", new CultureInfo("en-US"))
                        })
                        .ToList();
                }
            ),
            new (
                4, "Report the products that have not been sold.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Orders
                        .Where(order => order.Status != "Shipped")
                        .SelectMany(order =>
                            order.OrderDetail,
                            (order, orderDetail) => new
                            {
                                order.OrderNumber,
                                order.Status,
                                orderDetail.Product
                            }
                        )
                        .Where(result => result.Product != null)
                        .OrderBy(result => result.Product.ProductName)
                        .Select(result => new
                        {
                            result.Product.ProductCode,
                            result.Product.ProductName,
                            result.OrderNumber,
                            result.Status
                        })
                        .ToList();
                }
            ),
            new (
                5, "List the amount paid by each customer.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Payments
                        .GroupBy(
                            payment => payment.Customer.CustomerName,
                            payment => payment.Amount,
                            (customerName, amounts) => new
                            {
                                CustomerName = customerName,
                                TotalPayments = amounts.Sum()
                            }
                        )
                        .OrderBy(result => result.CustomerName)
                        .Select(result => new
                        {
                            result.CustomerName,
                            TotalPayments = '$' + result.TotalPayments.ToString("N2", new CultureInfo("en-US"))
                        })
                        .ToList();
                }
            ),
            new (
                6, "How many orders have been placed by Herkku Gifts?",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Orders
                        .Where(order => order.Customer.CustomerName == "Herkku Gifts")
                        .GroupBy(order => order.Customer.CustomerName)
                        .Select(group => new
                        {
                            CustomerName = group.Key,
                            OrderCount = group.Count()
                        })
                        .ToList();
                }
            ),
            new (
                7, "Who are the employees in Boston?",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Employees
                        .Where(employee => employee.Office.City == "Boston")
                        .Select(employee => new
                        {
                            employee.EmployeeNumber,
                            EmployeeName = employee.FirstName + ' ' + employee.LastName,
                            employee.Extension,
                            employee.Email,
                            employee.OfficeCode,
                            employee.ReportsTo,
                            employee.JobTitle
                        })
                        .ToList();
                }
            ),
            new (
                8, "Report those payments greater than 100,000. Sort the report so the customer who made the highest payment appears first.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Payments
                        .Where(payment =>
                            payment.Amount > 100_000
                            && payment.Customer != null
                        )
                        .OrderByDescending(payment => payment.Amount)
                        .Select(payment => new
                        {
                            payment.CustomerNumber,
                            payment.Customer.CustomerName,
                            Amount = '$' + payment.Amount.ToString("N2", new CultureInfo("en-US"))
                        })
                        .ToList();
                }
            ),
            new (
                9, "List the value of 'On Hold' orders.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.OrderDetails
                        .Where(orderdetail => orderdetail.Order.Status == "On Hold")
                        .GroupBy(
                            orderdetail => orderdetail.OrderNumber,
                            orderdetail => orderdetail.QuantityOrdered * orderdetail.PriceEach,
                            (orderNumber, orderValues) => new
                            {
                                OrderNumber = orderNumber,
                                OrderValue = '$' + orderValues.Sum().ToString("N2", new CultureInfo("en-US"))
                            }
                        )
                        .ToList();
                }
            ),
            new (
                10, "Report the number of orders 'On Hold' for each customer.",
                () =>
                {
                    var context = new AppDbContext();

                    return context.Orders
                        .Where(order => order.Status == "On Hold")
                        .GroupBy(
                            order => new { order.CustomerNumber, order.Customer.CustomerName},
                            order => order.OrderNumber
                        )
                        .Select(group => new
                        {
                            group.Key.CustomerNumber,
                            group.Key.CustomerName,
                            OrdersOnHold = group.Count()
                        })
                        .ToList();
                }
            )
        ];
    }
}
