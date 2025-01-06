using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassicModels.Utils
{
    public class Util
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.Never
        };

        public static void PrintAsJson<T>(T data)
        {
            var json = JsonSerializer.Serialize(data, Options);
            Console.WriteLine();
            Console.WriteLine(json);
        }
    }
}
