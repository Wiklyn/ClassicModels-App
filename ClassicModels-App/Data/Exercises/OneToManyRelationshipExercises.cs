using ClassicModels.Models;

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
        ];
    }
}
