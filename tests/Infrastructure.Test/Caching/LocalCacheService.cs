using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;

public class LocalCacheService : CacheService<FSH.Starter.Infrastructure.Caching.LocalCacheService>
{
    protected override FSH.Starter.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<FSH.Starter.Infrastructure.Caching.LocalCacheService>.Instance);
}