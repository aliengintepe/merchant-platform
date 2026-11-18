using merchantPlatform.Dtos;
using merchantPlatform.Entities;
using merchantPlatform.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace merchantPlatform.Services
{
    public class OcrResultService : ApplicationService, IOcrResultService

    {
        private readonly IOcrResultRepository _repository;

        public OcrResultService(IOcrResultRepository repository)
        {
            _repository = repository;
        }

        public async Task<OcrResultDto> CreateAsync(CreateOcrResultDto input)
        {
           
            var entity = new OcrResult(GuidGenerator.Create(), input.FileName, input.OcrText, input.Label, input.Accuracy, input.TcKimlikNo);
            await _repository.InsertAsync(entity, autoSave: true);

            return ObjectMapper.Map<OcrResult, OcrResultDto>(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<OcrResultDto> GetAsync(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            return ObjectMapper.Map<OcrResult, OcrResultDto>(entity);
        }

        public Task<List<OcrResultDto>> GetLabeledResultsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<OcrResultDto>> GetListAsync()
        {
            var list = await _repository.GetListAsync();
            return ObjectMapper.Map<List<OcrResult>, List<OcrResultDto>>(list);
        }

        public async Task<OcrResultDto> UpdateAsync(Guid id, UpdateOcrResultDto input)
        {
            var entity = await _repository.GetAsync(id);
            entity.Label = input.Label;
            await _repository.UpdateAsync(entity);
            return ObjectMapper.Map<OcrResult, OcrResultDto>(entity);
        }


    }
}

