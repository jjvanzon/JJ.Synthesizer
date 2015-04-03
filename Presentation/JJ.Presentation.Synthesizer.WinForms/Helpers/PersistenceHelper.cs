using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.WinForms.Helpers
{
    internal class PersistenceHelper
    {
        public static IContext CreateContext()
        {
            return ContextFactory.CreateContextFromConfiguration();
        }

        public static TRepository CreateRepository<TRepository>(IContext context)
        {
            return RepositoryFactory.CreateRepositoryFromConfiguration<TRepository>(context);
        }

        internal static PersistenceWrapper CreatePersistenceWrapper(IContext context)
        {
            return new PersistenceWrapper(
                CreateRepository<IOperatorRepository>(context),
                CreateRepository<IInletRepository>(context), 
                CreateRepository<IOutletRepository>(context), 
                CreateRepository<ICurveInRepository>(context), 
                CreateRepository<IValueOperatorRepository>(context), 
                CreateRepository<ISampleOperatorRepository>(context));
        }
    }
}