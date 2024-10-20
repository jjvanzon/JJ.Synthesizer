using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;

// ReSharper disable CheckNamespace
// ReSharper disable RedundantNameQualifier
#pragma warning disable IDE0001 // RedundantNameQualifier

namespace JJ.Business.Synthesizer
{
    /// <summary>
    /// Can get persistence configuration from config, or otherwise falls back
    /// to default in-memory persistence.
    /// </summary>
    public static class PersistenceHelper
    {
        private static readonly PersistenceConfiguration _config = 
            ConfigHelper.TryGetSection<PersistenceConfiguration>() ??
            GetDefaultInMemoryConfiguration();

        public static IContext CreateContext()
        {
            return ContextFactory.CreateContextFromConfiguration(_config);
        }

        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context, _config);
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
