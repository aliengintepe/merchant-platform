using merchantPlatform.Samples;
using Xunit;

namespace merchantPlatform.EntityFrameworkCore.Applications;

[Collection(merchantPlatformTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<merchantPlatformEntityFrameworkCoreTestModule>
{

}
