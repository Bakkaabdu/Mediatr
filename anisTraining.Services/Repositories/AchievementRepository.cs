using anisTraining.DataServices.Data;
using anisTraining.Entities.DbSet;
using anisTraining.Services.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anisTraining.Services.Repositories
{
    public class AchievementRepository : GenericRepository<Achievement>, IAchievementRepository
    {
        public AchievementRepository(AppDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<Achievement?> GetDriverAchievementAsync(Guid driverId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);

            }
            catch(Exception e) 
            {
                _logger.LogError(e, message: "GetDriverAchievementAsync function Error", typeof(AchievementRepository));
                throw;
            }
        }
        public override async Task<IEnumerable<Achievement>> All()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e, message: "All function Error", typeof(AchievementRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                // get my entity

                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);

                if (result == null)
                    return false;

                result.Status = 0;
                result.UpdatedDate = DateTime.Now;

                return true;
            }

            catch (Exception e)
            {
                _logger.LogError(e, message: "Delete function Error", typeof(AchievementRepository));
                throw;
            }
        }


        public override async Task<bool> Update(Achievement achievement)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == achievement.Id);

                if (result == null)
                    return false;

                result.UpdatedDate = DateTime.Now;
                result.FastestLap = achievement.FastestLap;
                result.PolePosition = achievement.PolePosition;
                result.RaceWins = achievement.RaceWins;
                result.WorldChampionship = achievement.WorldChampionship;

                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, message: "Update function Error", typeof(DriverRepository));
                throw;
            }
        }
    }
}
