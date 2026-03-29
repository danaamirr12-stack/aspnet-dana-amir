using System.ComponentModel.DataAnnotations;

namespace CoreFitness.Web.ViewModels
{
    public class GymClassViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Namn krävs")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Datum och tid krävs")]
        public DateTime DateTime { get; set; }

        public string? Instructor { get; set; }

        public string? Category { get; set; }

        [Range(1, 100, ErrorMessage = "Kapacitet måste vara mellan 1 och 100")]
        public int MaxCapacity { get; set; }
    }
}