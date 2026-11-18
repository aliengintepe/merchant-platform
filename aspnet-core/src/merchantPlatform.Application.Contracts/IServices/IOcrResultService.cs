using merchantPlatform.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace merchantPlatform.IServices
{
   public interface IOcrResultService:IApplicationService
    {
        Task<OcrResultDto> CreateAsync(CreateOcrResultDto input);
        Task<OcrResultDto> GetAsync(Guid id);
        Task<List<OcrResultDto>> GetListAsync();
        Task<OcrResultDto> UpdateAsync(Guid id, UpdateOcrResultDto input);
        Task DeleteAsync(Guid id);
        Task<List<OcrResultDto>> GetLabeledResultsAsync();
    }
}
