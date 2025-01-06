﻿using ClassicModels.Models;
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
        ];
    }
}
