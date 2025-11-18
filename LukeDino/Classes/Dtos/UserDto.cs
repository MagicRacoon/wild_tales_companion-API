using LukeDino.Entities;

namespace LukeDino.Classes.Dtos
{
    public class UserDto
    {
        public int? UserId { get; set; }

        public string FirebaseuserId { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsMember { get; set; }

        public string? AvatarUrl { get; set; }

        public string Uid { get; set; } = null!;

        public string? YoutubeChannelId { get; set; }

        public string? YoutubeChannelTitle { get; set; }

        public DateTimeOffset? YoutubeLinkedDate { get; set; }

        public string? YoutubeAvatarUrl { get; set; }
    }
}
