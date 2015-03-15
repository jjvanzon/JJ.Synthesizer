using JJ.Business.Synthesizer.Tests.Configuration;
using JJ.Framework.Configuration;
using JJ.Framework.Persistence;
using JJ.Framework.Persistence.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class PersistenceHelper
    {
        private static ConfigurationSection _config;

        static PersistenceHelper()
        {
            _config = CustomConfigurationManager.GetSection<ConfigurationSection>();
        }

        public static IContext CreateMemoryContext()
        {
            return ContextFactory.CreateContextFromConfiguration(_config.MemoryPersistenceConfiguration);
        }

        public static IContext CreateDatabaseContext()
        {
            return ContextFactory.CreateContextFromConfiguration(_config.DatabasePersistenceConfiguration);
        }

        public static TRepositoryInterface CreateRepository<TRepositoryInterface>(IContext context)
        {
            if (context is MemoryContext)
            {
                return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context, _config.MemoryPersistenceConfiguration);
            }

            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepositoryInterface>(context, _config.DatabasePersistenceConfiguration);
        }
    }
}
