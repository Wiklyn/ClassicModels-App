using ClassicModels.Models;
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
        ];
    }
}
