using anisTraining.DataServices.Data;
using anisTraining.Entities.DbSet;
using anisTraining.Services.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anisTraining.Services.Repositories
{
    public class DriverRepository : GenericRepository<Driver>, IDriveRepository
    {
        public DriverRepository(AppDbContext context, ILogger logger) : base(context, logger) 
        { }

        public override async Task<IEnumerable<Driver>> All()
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
                _logger.LogError(e, message: "All function Error" , typeof(DriverRepository));
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
                _logger.LogError(e, message: "Delete function Error", typeof(DriverRepository));
                throw;
            }
        }


        public override async Task<bool> Update(Driver driver)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == driver.Id);

                if (result == null)
                    return false;
                
                result.UpdatedDate = DateTime.Now;
                result.DriverNumber = driver.DriverNumber;
                result.FirstName = driver.FirstName;
                result.LastName = driver.LastName;
                result.DateOfBirth = driver.DateOfBirth;

                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, message:"Update function Error", typeof(DriverRepository));
                throw;
            }
        }
    }
}
