
using merchantPlatform.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;


namespace merchantPlatform.IServices
{
    public interface IOcrResultRepository :IRepository<OcrResult, Guid>
    {
        Task<List<OcrResult>> GetLatestResultsAsync(int count);
        Task<List<OcrResult>> GetLabeledResultsAsync();
    }
}
