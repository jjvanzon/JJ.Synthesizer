using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static partial class ViewModelHelper
    {
        public static IList<OperatorTypeViewModel> CreateOperatorTypesViewModel(IOperatorTypeRepository operatorTypeRepository)
        {
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);

            IList<OperatorType> operatorTypes = operatorTypeRepository.GetAllOrderedBySortOrder();
            IList<OperatorTypeViewModel> operatorTypeViewModels = operatorTypes.Select(x => x.ToViewModel()).ToArray();

            return operatorTypeViewModels;
        }

        public static IList<IDAndName> CreateAudioFileFormatLookupViewModel(IAudioFileFormatRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<AudioFileFormat> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();

            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateSampleDataTypeLookupViewModel(ISampleDataTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SampleDataType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateSpeakerSetupLookupViewModel(ISpeakerSetupRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SpeakerSetup> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateNodeTypesLookupViewModel(INodeTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<NodeType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }

        public static IList<IDAndName> CreateInterpolationTypesLookupViewModel(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<InterpolationType> entities = repository.GetAll().OrderBy(x => x.Name).ToArray();
            IList<IDAndName> idNames = entities.Select(x => x.ToIDAndName()).ToArray();

            return idNames;
        }


    }
}
