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
        public static IList<OperatorTypeViewModel> CreateOperatorTypesViewModel()
        {
            // TODO: Eventually these should be registered centrally somewhere.
            var viewModels = new OperatorTypeViewModel[]
            {
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Adder, Symbol = PropertyDisplayNames.Adder },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Add, Symbol = PropertyDisplayNames.Add },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.CurveIn, Symbol = PropertyDisplayNames.CurveIn },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Divide, Symbol = PropertyDisplayNames.Divide },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Multiply, Symbol = PropertyDisplayNames.Multiply },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.PatchInlet, Symbol = PropertyDisplayNames.PatchInlet },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.PatchOutlet, Symbol = PropertyDisplayNames.PatchOutlet },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Power, Symbol = PropertyDisplayNames.Power },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Sample, Symbol = PropertyDisplayNames.Sample },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Sine, Symbol = PropertyDisplayNames.Sine },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Substract, Symbol = PropertyDisplayNames.Substract },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.TimeAdd, Symbol = PropertyDisplayNames.TimeAdd },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.TimeDivide, Symbol = PropertyDisplayNames.TimeDivide },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.TimeMultiply, Symbol = PropertyDisplayNames.TimeMultiply },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.TimePower, Symbol = PropertyDisplayNames.TimePower },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.TimeSubstract, Symbol = PropertyDisplayNames.TimeSubstract },
                new OperatorTypeViewModel { OperatorTypeName = PropertyNames.Value, Symbol =  PropertyDisplayNames.Value }
            };

            return viewModels;
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
