using merchantPlatform.Entities;
using merchantPlatform.EntityFrameworkCore;
using merchantPlatform.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace merchantPlatform.Repository
{
    public class OcrResultRepository : EfCoreRepository<merchantPlatformDbContext, OcrResult, Guid>, IOcrResultRepository
    {
        public OcrResultRepository(IDbContextProvider<merchantPlatformDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<OcrResult>> GetLatestResultsAsync(int count)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.OrderByDescending(x => x.Date).Take(count).ToListAsync();
        }

        public async Task<List<OcrResult>> GetLabeledResultsAsync()
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.Where(x => !string.IsNullOrEmpty(x.Label))
                              .OrderByDescending(x => x.Date)
                              .ToListAsync();
        }
    }
}
