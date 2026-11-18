using Xunit;

namespace merchantPlatform.EntityFrameworkCore;

[CollectionDefinition(merchantPlatformTestConsts.CollectionDefinitionName)]
public class merchantPlatformEntityFrameworkCoreCollection : ICollectionFixture<merchantPlatformEntityFrameworkCoreFixture>
{

}
