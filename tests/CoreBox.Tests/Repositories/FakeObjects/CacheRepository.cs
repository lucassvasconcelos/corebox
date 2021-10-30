using CoreBox.Repositories.Cache;

namespace CoreBox.Tests.Repositories.FakeObjects
{
    public class CacheRepository : AbstractCache
    {
        public CacheRepository(Context context) : base(context.Caches, context)
        {
        }
    }
}