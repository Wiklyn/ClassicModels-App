using System.ComponentModel;

namespace ClassicModels.Enums
{
    public enum ExerciseCategories
    {
        [Description("Single Entity")]
        SingleEntity,

        [Description("One To Many Relationship")]
        OneToManyRelationship,
    }
}
