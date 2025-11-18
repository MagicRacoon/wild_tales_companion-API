namespace LukeDino.Classes.Dtos
{
    public class DinoDto
    {
        public int? DinoId { get; set; }

        public string Name { get; set; } = null!;

        public string DinoType { get; set; } = null!;

        public string DietType { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public int Length { get; set; }

        public string? EraLived { get; set; }

        public string? LocationLived { get; set; }

        public int? Weight { get; set; }
    }
}
