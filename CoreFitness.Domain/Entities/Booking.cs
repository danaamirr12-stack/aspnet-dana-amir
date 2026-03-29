namespace CoreFitness.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int GymClassId { get; set; }
        public DateTime BookedAt { get; set; }

        public GymClass? GymClass { get; set; }
    }
}