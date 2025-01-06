using ClassicModels.Models;

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
        ];
    }
}
