using AutoMapper;
using merchantPlatform.Dtos;
using merchantPlatform.Entities;

namespace merchantPlatform;

public class merchantPlatformApplicationAutoMapperProfile : Profile
{
    public merchantPlatformApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        
        CreateMap<OcrResult, OcrResultDto>();

        CreateMap<CreateOcrResultDto, OcrResult>();
        CreateMap<UpdateOcrResultDto, OcrResult>();
    }
}
