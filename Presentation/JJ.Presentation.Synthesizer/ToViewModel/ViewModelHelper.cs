using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ViewModelHelper
    {
        public static IList<OperatorTypeViewModel> CreateOperatorTypesViewModel()
        {
            // TODO: Eventually these should be registered centrally somewhere.
            var viewModels = new OperatorTypeViewModel[]
            {
                new OperatorTypeViewModel { OperatorTypeName = "Adder", Symbol = PropertyDisplayNames.Adder },
                new OperatorTypeViewModel { OperatorTypeName = "Add", Symbol = PropertyDisplayNames.Add },
                new OperatorTypeViewModel { OperatorTypeName = "CurveIn", Symbol = PropertyDisplayNames.CurveIn },
                new OperatorTypeViewModel { OperatorTypeName = "Divide", Symbol = PropertyDisplayNames.Divide },
                new OperatorTypeViewModel { OperatorTypeName = "Multiply", Symbol = PropertyDisplayNames.Multiply },
                new OperatorTypeViewModel { OperatorTypeName = "PatchInlet", Symbol = PropertyDisplayNames.PatchInlet },
                new OperatorTypeViewModel { OperatorTypeName = "PatchOutlet", Symbol = PropertyDisplayNames.PatchOutlet },
                new OperatorTypeViewModel { OperatorTypeName = "Power", Symbol = PropertyDisplayNames.Power },
                new OperatorTypeViewModel { OperatorTypeName = "SampleOperator", Symbol = PropertyDisplayNames.SampleOperator },
                new OperatorTypeViewModel { OperatorTypeName = "Sine", Symbol = PropertyDisplayNames.Sine },
                new OperatorTypeViewModel { OperatorTypeName = "Substract", Symbol = PropertyDisplayNames.Substract },
                new OperatorTypeViewModel { OperatorTypeName = "TimeAdd", Symbol = PropertyDisplayNames.TimeAdd },
                new OperatorTypeViewModel { OperatorTypeName = "TimeDivide", Symbol = PropertyDisplayNames.TimeDivide },
                new OperatorTypeViewModel { OperatorTypeName = "TimeMultiply", Symbol = PropertyDisplayNames.TimeMultiply },
                new OperatorTypeViewModel { OperatorTypeName = "TimePower", Symbol = PropertyDisplayNames.TimePower },
                new OperatorTypeViewModel { OperatorTypeName = "TimeSubstract", Symbol = PropertyDisplayNames.TimeSubstract },
                new OperatorTypeViewModel { OperatorTypeName = "ValueOperator", Symbol =  PropertyDisplayNames.ValueOperator }
            };

            return viewModels;
        }

        public static IList<IDName> CreateAudioFileFormatLookupViewModel(IAudioFileFormatRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<AudioFileFormat> entities = repository.GetAll().ToArray();
            IList<IDName> idNames = entities.Select(x => x.ToIDName()).ToArray();

            return idNames;
        }

        public static IList<IDName> CreateSampleDataTypeLookupViewModel(ISampleDataTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SampleDataType> entities = repository.GetAll().ToArray();
            IList<IDName> idNames = entities.Select(x => x.ToIDName()).ToArray();

            return idNames;
        }

        public static IList<IDName> CreateSpeakerSetupLookupViewModel(ISpeakerSetupRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<SpeakerSetup> entities = repository.GetAll().ToArray();
            IList<IDName> idNames = entities.Select(x => x.ToIDName()).ToArray();

            return idNames;
        }

        public static IList<IDName> CreateNodeTypesLookupViewModel(INodeTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<NodeType> entities = repository.GetAll().ToArray();
            IList<IDName> idNames = entities.Select(x => x.ToIDName()).ToArray();

            return idNames;
        }

        public static IList<IDName> CreateInterpolationTypesLookupViewModel(IInterpolationTypeRepository repository)
        {
            if (repository == null) throw new NullException(() => repository);

            IList<InterpolationType> entities = repository.GetAll().ToArray();
            IList<IDName> idNames = entities.Select(x => x.ToIDName()).ToArray();

            return idNames;
        }
    }
}
