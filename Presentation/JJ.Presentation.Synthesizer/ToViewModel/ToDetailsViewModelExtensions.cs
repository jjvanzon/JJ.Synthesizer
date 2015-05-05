using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Names;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    public static class ToDetailsViewModelExtensions
    {
        public static AudioFileOutputDetailsViewModel ToDetailsViewModel(
            this AudioFileOutput entity,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository)
        {
            var viewModel = new AudioFileOutputDetailsViewModel
            {
                AudioFileOutput = entity.ToViewModelWithRelatedEntities(),
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository)
            };
           
            if (entity.Document != null)
            {
                IList<Outlet> outlets = entity.Document.Patches
                                                       .SelectMany(x => x.Operators)
                                                       .Where(x => String.Equals(x.OperatorTypeName, PropertyNames.PatchOutlet))
                                                       .SelectMany(x => x.Outlets)
                                                       .ToArray();

                viewModel.OutletLookup = outlets.Select(x => x.ToIDName()).ToArray();
            }
            else
            {
                viewModel.OutletLookup = new IDName[0];
            }

            return viewModel;
        }

        public static CurveDetailsViewModel ToDetailsViewModel(this Curve curve, INodeTypeRepository nodeTypeRepository)
        {
            if (curve == null) throw new NullException(() => curve);

            var viewModel = new CurveDetailsViewModel
            {
                Curve = curve.ToViewModelWithRelatedEntities(),
                NodeTypes = ViewModelHelper.CreateNodeTypesLookupViewModel(nodeTypeRepository)
            };

            return viewModel;
        }

        public static SampleDetailsViewModel ToDetailsViewModel(
            this Sample entity,
            IAudioFileFormatRepository audioFileFormatRepository,
            ISampleDataTypeRepository sampleDataTypeRepository,
            ISpeakerSetupRepository speakerSetupRepository,
            IInterpolationTypeRepository interpolationTypeRepository)
        {
            var viewModel = new SampleDetailsViewModel
            {
                Sample = entity.ToViewModel(),
                AudioFileFormats = ViewModelHelper.CreateAudioFileFormatLookupViewModel(audioFileFormatRepository),
                SampleDataTypes = ViewModelHelper.CreateSampleDataTypeLookupViewModel(sampleDataTypeRepository),
                SpeakerSetups = ViewModelHelper.CreateSpeakerSetupLookupViewModel(speakerSetupRepository),
                InterpolationTypes = ViewModelHelper.CreateInterpolationTypesLookupViewModel(interpolationTypeRepository)
            };

            return viewModel;
        }

        public static DocumentDetailsViewModel ToDetailsViewModel(this Document document)
        {
            var viewModel = new DocumentDetailsViewModel
            {
                Document = document.ToIDName(),
                Messages = new List<Message>()
            };

            return viewModel;
        }
    }
}
