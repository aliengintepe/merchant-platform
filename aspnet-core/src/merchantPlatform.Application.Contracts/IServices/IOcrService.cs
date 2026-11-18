using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace merchantPlatform.IServices
{
    public interface IOcrService : IApplicationService
    {
       Task<string> ExtractTextAsync(string imagePath);
       
    }
}
