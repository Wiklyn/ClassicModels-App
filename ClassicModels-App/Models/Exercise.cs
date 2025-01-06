namespace ClassicModels.Models
{
    public class Exercise(int Number, string Statement, Func<IEnumerable<object>> Query)
    {
        public int Number { get; } = Number;
        public string Statement { get; } = Statement;
        public Func<IEnumerable<object>> Query { get; } = Query;
    }
}
