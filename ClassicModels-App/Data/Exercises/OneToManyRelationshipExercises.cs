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
        ];
    }
}
