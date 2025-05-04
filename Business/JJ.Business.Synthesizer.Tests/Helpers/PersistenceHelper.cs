using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class PersistenceHelper
    {
        public static IContext CreateContext()
        {
            return ServiceFactory.CreateContext();
        }

        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            return ServiceFactory.CreateRepository<TRepositoryInterface>(context);
        }
    }
}
