using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToDocumentViewModelExtensions
    {
        public static DocumentViewModel ToViewModel(
            this Document document,
            IList<Patch> grouplessPatches,
            IList<PatchGroupDto> patchGroupDtos,
            IList<UsedInDto<Curve>> curveUsedInDtos,
            IList<UsedInDto<Sample>> sampleUsedInDtos,
            RepositoryWrapper repositories, 
            EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);
            if (curveUsedInDtos == null) throw new NullException(() => curveUsedInDtos);
            if (sampleUsedInDtos == null) throw new NullException(() => sampleUsedInDtos);
            if (repositories == null) throw new NullException(() => repositories);

            var sampleRepositories = new SampleRepositories(repositories);

            var viewModel = new DocumentViewModel
            {
                ID = document.ID,
                AudioFileOutputGrid = document.ToAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesDictionary = document.AudioFileOutputs.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                AutoPatchDetails = ViewModelHelper.CreateEmptyPatchDetailsViewModel(),
                CurrentPatches = ViewModelHelper.CreateEmptyCurrentPatchesViewModel(),
                CurveDetailsDictionary = document.Curves.Select(x => x.ToDetailsViewModel()).ToDictionary(x => x.CurveID),
                CurveGrid = curveUsedInDtos.ToGridViewModel(document.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(curveUsedInDtos),
                CurvePropertiesDictionary = document.Curves.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
                DocumentProperties = document.ToPropertiesViewModel(),
                DocumentTree = document.ToTreeViewModel(grouplessPatches, patchGroupDtos),
                NodePropertiesDictionary = document.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                OperatorPropertiesDictionary = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_WithoutAlternativePropertiesView()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForBundles = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForBundles()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCaches = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCaches(repositories.InterpolationTypeRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCurves = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCurves(repositories.CurveRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCustomOperators = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCustomOperators(repositories.PatchRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForMakeContinuous = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForMakeContinuous()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForNumbers = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchInlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchOutlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForSamples = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSamples(repositories.SampleRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimension = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimension()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimensionAndCollectionRecalculation()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimensionAndInterpolation = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimensionAndInterpolation()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimensionAndOutletCount = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimensionAndOutletCount()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithInletCount = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithInletCount()).ToDictionary(x => x.ID),
                PatchDetailsDictionary = document.Patches.Select(x => x.ToDetailsViewModel(repositories.SampleRepository, repositories.CurveRepository, repositories.PatchRepository, entityPositionManager)).ToDictionary(x => x.Entity.ID),
                PatchGridDictionary = ViewModelHelper.CreatePatchGridViewModelDictionary(grouplessPatches, patchGroupDtos, document.ID),
                PatchPropertiesDictionary = document.Patches.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(document),
                SampleGrid = sampleUsedInDtos.ToGridViewModel(document.ID),
                SamplePropertiesDictionary = document.Samples.Select(x => x.ToPropertiesViewModel(sampleRepositories)).ToDictionary(x => x.Entity.ID),
                ScaleGrid = document.Scales.ToGridViewModel(document.ID),
                ScalePropertiesDictionary = document.Scales.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                ToneGridEditDictionary = document.Scales.Select(x => x.ToToneGridEditViewModel()).ToDictionary(x => x.ScaleID)
            };

            if (document.AudioOutput != null)
            {
                viewModel.AudioOutputProperties = document.AudioOutput.ToPropertiesViewModel();
            }
            else
            {
                viewModel.AudioOutputProperties = ViewModelHelper.CreateEmptyAudioOutputPropertiesViewModel();
            }

            viewModel.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(document.Patches);

            return viewModel;
        }
    }
}
