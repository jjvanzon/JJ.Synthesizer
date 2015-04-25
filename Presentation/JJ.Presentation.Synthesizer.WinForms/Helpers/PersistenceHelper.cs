using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal class PersistenceHelper
    {
        static PersistenceConfiguration _memoryPersistenceConfiguration;

        static PersistenceHelper()
        {
            ConfigurationSection config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            _memoryPersistenceConfiguration = config.MemoryPersistence;
        }

        public static IContext CreateContext()
        {
            return ContextFactory.CreateContextFromConfiguration();
        }

        public static TRepository CreateRepository<TRepository>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepository>(context);
        }

        public static PersistenceWrapper CreatePersistenceWrapper(IContext context)
        {
            return new PersistenceWrapper(
                CreateRepository<IPatchRepository>(context), 
                CreateRepository<IOperatorRepository>(context),
                CreateRepository<IInletRepository>(context), 
                CreateRepository<IOutletRepository>(context), 
                CreateRepository<ICurveRepository>(context), 
                CreateRepository<ISampleRepository>(context));
        }

        public static IContext CreateMemoryContext()
        {
            return ContextFactory.CreateContextFromConfiguration(_memoryPersistenceConfiguration);
        }

        public static TRepository CreateMemoryRepository<TRepository>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepository>(context, _memoryPersistenceConfiguration);
        }
    }
}