namespace LukeDino.Entities;

public partial class UserDino
{
    public int UserDinoId { get; set; }

    public int UserId { get; set; }

    public int DinoId { get; set; }

    public virtual Dino Dino { get; set; } = null!;

    public virtual Userprofile User { get; set; } = null!;
}