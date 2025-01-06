using ClassicModels.Models;
using ClassicModels.Presentation;
using ClassicModels.Utils;

namespace ClassicModels.Services
{
    public class ExerciseServices
    {
        public static int GetExerciseNumber()
        {
            Console.WriteLine();
            Console.Write("Type the exercise number: ");

            if (!int.TryParse(Console.ReadLine(), out int exerciseChoice))
            {
                Console.WriteLine("\nInvalid input. Please enter a valid exercise number.\n");
                return -1;
            }

            Console.WriteLine();
            return exerciseChoice;
        }

        public static void Run(List<Exercise> ExerciseList, string category)
        {
            Interface.ShowExercisesMenu(ExerciseList, category);

            int exerciseChoice = GetExerciseNumber();

            if (exerciseChoice == -1)
                return;

            else if (exerciseChoice == 0)
            {
                foreach (var exercise in ExerciseList)
                {
                    ExecuteExercise(ExerciseList, exercise.Number);
                }
            }
            else if (exerciseChoice > 0 && exerciseChoice <= ExerciseList.Count)
            {
                ExecuteExercise(ExerciseList, exerciseChoice);
            }
            else
            {
                Console.WriteLine("Exercise not found. Please choose a valid option.\n");
            }
        }

        public static void ExecuteExercise(List<Exercise> ExerciseList, int exerciseNumber)
        {
            var exercise = ExerciseList.FirstOrDefault(exercise => exercise.Number == exerciseNumber);

            Console.WriteLine($"{exercise.Number}. {exercise.Statement}");

            var result = exercise.Query();
            Util.PrintAsJson(result);
            Console.WriteLine();
        }
    }
}
