namespace CoreFitness.Domain.Entities
{
    public class GymClass
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime DateTime { get; set; }
        public string? Instructor { get; set; }
        public string? Category { get; set; }
        public int MaxCapacity { get; set; }
    }
}