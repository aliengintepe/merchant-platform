using merchantPlatform.Samples;
using Xunit;

namespace merchantPlatform.EntityFrameworkCore.Domains;

[Collection(merchantPlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<merchantPlatformEntityFrameworkCoreTestModule>
{

}
