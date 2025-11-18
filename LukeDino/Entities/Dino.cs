using System;
using System.Collections.Generic;

namespace LukeDino.Entities;

public partial class Dino
{
    public int DinoId { get; set; }

    public string Name { get; set; } = null!;

    public string DinoType { get; set; } = null!;

    public string DietType { get; set; } = null!;

    public string? ImageUrl { get; set; }

    public int Length { get; set; }

    public string? EraLived { get; set; }

    public string? LocationLived { get; set; }

    public int? Weight { get; set; }

    public virtual ICollection<UserDino> UserDinos { get; set; } = new List<UserDino>();
}
