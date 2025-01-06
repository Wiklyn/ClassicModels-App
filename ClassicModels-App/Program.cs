using ClassicModels.Data.Exercises;
using ClassicModels.Enums;
using ClassicModels.Presentation;
using ClassicModels.Services;

namespace ClassicModels
{
    public class Program
    {
        static void Main(string[] args)
        {
            Interface.ShowApresentation();

            while (true)
            {
                Interface.ShowCategoriesMenu();

                if (!int.TryParse(Console.ReadLine(), out int categoryChoice))
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid exercise number.\n");
                    continue;
                }

                if (categoryChoice == -1)
                {
                    Console.WriteLine("\nFinishing the program. Thanks for passing by.");
                    break;
                }

                categoryChoice -= 1;

                if (categoryChoice < 0 || categoryChoice >= Enum.GetValues(typeof(ExerciseCategories)).Length)
                {
                    Console.WriteLine("\nInvalid input. Please enter a valid category number.\n");
                    continue;
                }

                Console.WriteLine();
                ExecuteExercisesByCategory((ExerciseCategories)categoryChoice);

                while (true)
                {
                    Console.WriteLine("\nWhat you want to do now? Type -1 to finish the program\n");
                    Console.WriteLine("1. Choose another exercise on the same category");
                    Console.WriteLine("2. Choose another category\n");
                    Console.Write("Type the option number: ");

                    if (!int.TryParse(Console.ReadLine(), out int option))
                    {
                        Console.WriteLine("\nInvalid input. Please enter a valid exercise number\n.");
                        return;
                    }

                    if (option == 1)
                    {
                        Console.WriteLine();
                        ExecuteExercisesByCategory((ExerciseCategories)categoryChoice);
                    }
                    else if (option == 2)
                    {
                        Console.WriteLine();
                        break;
                    }
                    else if (option == -1)
                    {
                        Console.WriteLine("\nFinishing the program. Thanks for passing by.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("\nInvalid option. Try again.\n");
                    }
                }
            }
        }

        static void ExecuteExercisesByCategory(ExerciseCategories category)
        {
            switch (category)
            {
                case ExerciseCategories.SingleEntity:
                    ExerciseServices.Run(
                        SingleEntityExercises.ExerciseList,
                        ExerciseCategories.SingleEntity.GetDescription()
                    );
                    break;

                default:
                    Console.WriteLine("Invalid cateagory.");
                    break;
            }
        }
    }
}
