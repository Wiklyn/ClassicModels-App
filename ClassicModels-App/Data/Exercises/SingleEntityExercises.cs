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
        ];
    }
}
