using anisTraining.DataServices.Data;
using anisTraining.Services.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace anisTraining.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork , IDisposable
    {
        private readonly AppDbContext _context;

        public IDriveRepository Drivers { get;}
        public IAchievementRepository Achievements { get;}

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;

            var logger = loggerFactory.CreateLogger("logs");

            Drivers = new DriverRepository(_context,logger);
            Achievements = new AchievementRepository(_context, logger);
        }

        public async Task<bool> CompleteAsync()
        {
          var result = await _context.SaveChangesAsync();

            return result > 0;
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
