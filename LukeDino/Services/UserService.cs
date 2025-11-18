using LukeDino.Classes.Dtos;
using LukeDino.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LukeDino.Services
{
    public interface IUserService
    {
        Task ConfirmUserAsync(int userId);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task CreateUserAsync(UserDto form);
    }
    public class UserService(LukeDinoContext context, IDinoService dinoService) : IUserService
    {
        public async Task ConfirmUserAsync(int userId)
        {
            var user = await context.Userprofiles.FindAsync(userId);
            user.IsMember = true;

            var randomDinoId = await context.Dinos.Select(s => s.DinoId)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefaultAsync();

            await dinoService.AddUserDinoAsync(userId, randomDinoId);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var user = await context.Userprofiles
                .Select(s => new UserDto
                {
                    UserId = s.UserId,
                    Email = s.Email,
                    IsMember = s.IsMember,
                    AvatarUrl = s.AvatarUrl,
                    Uid = s.Uid,
                    YoutubeChannelId = s.YoutubeChannelId,
                    YoutubeChannelTitle = s.YoutubeChannelTitle,
                    YoutubeLinkedDate = s.YoutubeLinkedDate,
                    YoutubeAvatarUrl = s.YoutubeAvatarUrl
                })
                .ToListAsync();

            return user;
        }

        public async Task CreateUserAsync(UserDto form)
        {
            var uIdExists = await context.Userprofiles.AnyAsync(w => w.Uid == form.Uid);
            if (uIdExists)
                throw new Exception("User already exists");

            var userToAdd = new Userprofile
            {
                FirebaseuserId = form.FirebaseuserId,
                Email = form.Email,
                IsMember = form.IsMember,
                AvatarUrl = form.AvatarUrl,
                Uid = form.Uid,
                YoutubeChannelId = form.YoutubeChannelId,
                YoutubeChannelTitle = form.YoutubeChannelTitle,
                YoutubeLinkedDate = form.YoutubeLinkedDate,
                YoutubeAvatarUrl = form.YoutubeAvatarUrl
            };           

            await context.Userprofiles.AddAsync(userToAdd);
            await context.SaveChangesAsync();

            return;
        }
    }
}
