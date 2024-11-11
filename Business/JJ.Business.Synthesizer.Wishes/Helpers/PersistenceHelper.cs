using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <inheritdoc cref="_persistencehelper"/>
    public static class PersistenceHelper
    {

        public static IContext CreateContext() 
            => ContextFactory.CreateContextFromConfiguration(ConfigHelper.PersistenceConfiguration);

        /// <inheritdoc cref="_createrepository"/>
        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context = null) 
            => RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context ?? CreateContext(), ConfigHelper.PersistenceConfiguration);
    }
}
