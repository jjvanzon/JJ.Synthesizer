using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Wishes.docs;

namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    /// <inheritdoc cref="_persistencehelper"/>
    public static class PersistenceHelper
    {
        private static readonly PersistenceConfiguration _config = 
            ConfigHelper.TryGetSection<PersistenceConfiguration>() ??
            GetDefaultInMemoryConfiguration();

        public static IContext CreateContext()
        {
            return ContextFactory.CreateContextFromConfiguration(_config);
        }

        /// <inheritdoc cref="_createrepository"/>
        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context = null)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context ?? CreateContext(), _config);
        }

        // Defaults for Optional Config
        
        private static PersistenceConfiguration GetDefaultInMemoryConfiguration() => new PersistenceConfiguration
        {
            ContextType = "Memory",
            ModelAssembly = NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.Node>(),
            MappingAssembly = NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.Memory.Mappings.NodeMapping>(),
            RepositoryAssemblies = new[]
            {
                NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.Memory.Repositories.NodeTypeRepository>(),
                NameHelper.GetAssemblyName<JJ.Persistence.Synthesizer.DefaultRepositories.NodeTypeRepository>()
            }
        };
    }
}
