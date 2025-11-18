using LukeDino.Classes.Dtos;
using LukeDino.Entities;
using Microsoft.EntityFrameworkCore;

namespace LukeDino.Services
{
    public interface IDinoService
    {        
        Task<IEnumerable<DinoDto>> GetAllDinosAsync(int? dinoId = null);
        Task AddDinoAsync(DinoDto dino);
        Task AddUserDinoAsync(int userId, int dinoId);
    }

    public class DinoService(LukeDinoContext context) : IDinoService
    {
        public async Task<IEnumerable<DinoDto>> GetAllDinosAsync(int? dinoId = null)
        {
            var dinos = context.Dinos.AsQueryable();
            if (dinoId != null)
            {
                dinos = dinos.Where(w => w.DinoId == dinoId);
            }

            var result = await dinos.Select(s => new DinoDto
            {
                DinoId = s.DinoId,
                Name = s.Name,
                DinoType = s.DinoType,
                DietType = s.DietType,
                ImageUrl = s.ImageUrl,
                Length = s.Length,
                EraLived = s.EraLived,
                LocationLived = s.LocationLived,
                Weight = s.Weight,


            }).ToListAsync();
            return result;
        }

        public async Task AddDinoAsync(DinoDto dino)
        {
            var dinoToAdd = new Dino
            {
                Name = dino.Name,
                DinoType = dino.DinoType,
                DietType = dino.DietType,
                ImageUrl = dino.ImageUrl,
                Length = dino.Length,
                EraLived = dino.EraLived,
                LocationLived = dino.LocationLived,
                Weight = dino.Weight
            };

            await context.Dinos.AddAsync(dinoToAdd);
            await context.SaveChangesAsync();
        }

        public async Task AddUserDinoAsync(int userId, int dinoId)
        {
            var userDino = new UserDino
            {
                UserId = userId,
                DinoId = dinoId
            };

            await context.UserDinos.AddAsync(userDino);
            return;
        }
    }
}
