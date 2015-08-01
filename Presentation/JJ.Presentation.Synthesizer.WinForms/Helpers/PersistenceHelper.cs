using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;

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
        
        public static RepositoryWrapper CreateRepositoryWrapper(IContext context)
        {
            if (context == null) throw new NullException(() => context);

            var repositoryWrapper = new RepositoryWrapper
            (
                PersistenceHelper.CreateRepository<IDocumentRepository>(context),
                PersistenceHelper.CreateRepository<ICurveRepository>(context),
                PersistenceHelper.CreateRepository<IPatchRepository>(context),
                PersistenceHelper.CreateRepository<ISampleRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileOutputRepository>(context),
                PersistenceHelper.CreateRepository<IDocumentReferenceRepository>(context),
                PersistenceHelper.CreateRepository<INodeRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileOutputChannelRepository>(context),
                PersistenceHelper.CreateRepository<IOperatorRepository>(context),
                PersistenceHelper.CreateRepository<IOperatorTypeRepository>(context),
                PersistenceHelper.CreateRepository<IInletRepository>(context),
                PersistenceHelper.CreateRepository<IOutletRepository>(context),
                PersistenceHelper.CreateRepository<IEntityPositionRepository>(context),
                PersistenceHelper.CreateRepository<IAudioFileFormatRepository>(context),
                PersistenceHelper.CreateRepository<IInterpolationTypeRepository>(context),
                PersistenceHelper.CreateRepository<INodeTypeRepository>(context),
                PersistenceHelper.CreateRepository<ISampleDataTypeRepository>(context),
                PersistenceHelper.CreateRepository<ISpeakerSetupRepository>(context),
                PersistenceHelper.CreateRepository<IChildDocumentTypeRepository>(context),
                PersistenceHelper.CreateRepository<IIDRepository>(context)
            );

            return repositoryWrapper;
        }
    }
}