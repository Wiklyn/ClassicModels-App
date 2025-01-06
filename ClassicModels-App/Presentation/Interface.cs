using ClassicModels.Enums;
using ClassicModels.Models;

namespace ClassicModels.Presentation
{
    public class Interface
    {
        public static void ShowApresentation()
        {
            Console.WriteLine("Hello! This program allows you to see my solutions to the ClassicModels database exercises I found on HEREGOESTHELINK. So far I have answered only a few of those.");
            Console.WriteLine("The results of the queries are formatted as JSON.");
            Console.WriteLine("First you choose which category of exercises you want to see the answers to, and then you choose a specific exercise (or all of them).");
            Console.WriteLine();
        }

        public static void ShowCategoriesMenu()
        {
            Console.WriteLine("Choose a category of exercises or type -1 to finish the program:");
            Console.WriteLine();

            foreach (ExerciseCategories category in Enum.GetValues(typeof(ExerciseCategories)))
            {
                int intValue = Convert.ToInt32(category) + 1;
                Console.WriteLine($"{intValue}. {category.GetDescription()}");
            }

            Console.WriteLine();
            Console.Write("Type the category number: ");
        }

        public static void ShowExercisesMenu(List<Exercise> ExerciseList, string category)
        {
            Console.WriteLine($"\n{category} Exercises\n");
            Console.WriteLine("Choose an exercise or type 0 to see the answers to all exercises:");
            Console.WriteLine();

            foreach (var exercise in ExerciseList)
            {
                Console.WriteLine($"{exercise.Number}. {exercise.Statement}");
            }
        }
    }
}
