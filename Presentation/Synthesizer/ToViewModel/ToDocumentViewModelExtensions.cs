using System.Linq;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToDocumentViewModelExtensions
    {
        public static DocumentViewModel ToViewModel(
            this Document document,
            IList<UsedInDto<Patch>> grouplessPatchUsedInDtos,
            IList<PatchGroupDto_WithUsedIn> patchGroupDtos_WithUsedIn,
            IList<DocumentReferencePatchGroupDto> documentReferencePatchGroupDtos,
            IList<UsedInDto<Curve>> curveUsedInDtos,
            IList<UsedInDto<Sample>> sampleUsedInDtos,
            RepositoryWrapper repositories,
            EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);
            if (documentReferencePatchGroupDtos == null) throw new NullException(() => documentReferencePatchGroupDtos);
            if (curveUsedInDtos == null) throw new NullException(() => curveUsedInDtos);
            if (sampleUsedInDtos == null) throw new NullException(() => sampleUsedInDtos);
            // ReSharper disable once ImplicitlyCapturedClosure
            if (repositories == null) throw new NullException(() => repositories);

            var sampleRepositories = new SampleRepositories(repositories);
            var patchRepositories = new PatchRepositories(repositories);

            var viewModel = new DocumentViewModel
            {
                ID = document.ID,
                AudioFileOutputGrid = document.ToAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesDictionary = document.AudioFileOutputs.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                AutoPatchPopup = ViewModelHelper.CreateEmptyAutoPatchViewModel(),
                CurrentInstrument = ViewModelHelper.CreateCurrentInstrumentViewModelWithEmptyList(document),
                CurveDetailsDictionary = document.Curves.Select(x => x.ToDetailsViewModel()).ToDictionary(x => x.Curve.ID),
                CurveGrid = curveUsedInDtos.ToGridViewModel(document.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(curveUsedInDtos),
                CurvePropertiesDictionary = document.Curves.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
                DocumentProperties = document.ToPropertiesViewModel(),
                LibraryGrid = document.ToLibraryGridViewModel(),
                LibrarySelectionPopup = document.ToEmptyLibrarySelectionPopupViewModel(),
                LibraryPatchGridDictionary = documentReferencePatchGroupDtos.ToLibraryPatchGridViewModelDictionary(),
                LibraryPatchPropertiesDictionary = document.ToLibraryPatchPropertiesViewModelList().ToDictionary(x => x.PatchID),
                LibraryPropertiesDictionary = document.LowerDocumentReferences.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.DocumentReferenceID),
                NodePropertiesDictionary = document.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                OperatorPropertiesDictionary = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_WithoutAlternativePropertiesView(repositories.PatchRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCaches = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCaches(repositories.InterpolationTypeRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCurves = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCurves(repositories.CurveRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCustomOperators = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCustomOperators(repositories.PatchRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForInletsToDimension = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForInletsToDimension()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForNumbers = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchInlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchOutlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForSamples = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSamples(repositories.SampleRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithCollectionRecalculation = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithCollectionRecalculation()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithInterpolation = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithInterpolation()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithOutletCount = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithOutletCount()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithInletCount = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithInletCount()).ToDictionary(x => x.ID),
                PatchDetailsDictionary = document.Patches.Select(x => x.ToDetailsViewModel(repositories.SampleRepository, repositories.CurveRepository, repositories.PatchRepository, entityPositionManager)).ToDictionary(x => x.Entity.ID),
                PatchGridDictionary = ViewModelHelper.CreatePatchGridViewModelDictionary(grouplessPatchUsedInDtos, patchGroupDtos_WithUsedIn, document.ID),
                PatchPropertiesDictionary = document.Patches.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(document),
                SampleGrid = sampleUsedInDtos.ToGridViewModel(document.ID),
                SamplePropertiesDictionary = document.Samples.Select(x => x.ToPropertiesViewModel(sampleRepositories)).ToDictionary(x => x.Entity.ID),
                ScaleGrid = document.Scales.ToGridViewModel(document.ID),
                ScalePropertiesDictionary = document.Scales.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                ToneGridEditDictionary = document.Scales.Select(x => x.ToToneGridEditViewModel()).ToDictionary(x => x.ScaleID)
            };

            var converter = new RecursiveToDocumentTreeViewModelFactory();
            viewModel.DocumentTree = converter.ToTreeViewModel(document, repositories);

            if (document.AudioOutput != null)
            {
                viewModel.AudioOutputProperties = document.AudioOutput.ToPropertiesViewModel();
            }
            else
            {
                viewModel.AudioOutputProperties = ViewModelHelper.CreateEmptyAudioOutputPropertiesViewModel();
            }

            viewModel.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(document, repositories);

            return viewModel;
        }

        public static AutoPatchPopupViewModel ToAutoPatchViewModel(
            this Patch patch,
            ISampleRepository sampleRepository,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            IInterpolationTypeRepository interpolationTypeRepository,
            EntityPositionManager entityPositionManager)
        {
            if (patch == null) throw new NullException(() => patch);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (interpolationTypeRepository == null) throw new NullException(() => interpolationTypeRepository);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            var viewModel = new AutoPatchPopupViewModel
            {
                OperatorPropertiesDictionary = patch.ToOperatorPropertiesViewModelList_WithoutAlternativePropertiesView(patchRepository).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCaches = patch.ToPropertiesViewModelList_ForCaches(interpolationTypeRepository).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCurves = patch.ToPropertiesViewModelList_ForCurves(curveRepository).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCustomOperators = patch.ToPropertiesViewModelList_ForCustomOperators(patchRepository).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForInletsToDimension = patch.ToPropertiesViewModelList_ForInletsToDimension().ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForNumbers = patch.ToPropertiesViewModelList_ForNumbers().ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchInlets = patch.ToPropertiesViewModelList_ForPatchInlets().ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchOutlets = patch.ToPropertiesViewModelList_ForPatchOutlets().ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForSamples = patch.ToPropertiesViewModelList_ForSamples(sampleRepository).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithCollectionRecalculation = patch.ToPropertiesViewModelList_WithCollectionRecalculation().ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithInterpolation = patch.ToPropertiesViewModelList_WithInterpolation().ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithOutletCount = patch.ToPropertiesViewModelList_WithOutletCount().ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithInletCount = patch.ToPropertiesViewModelList_WithInletCount().ToDictionary(x => x.ID),
                PatchDetails = patch.ToDetailsViewModel(sampleRepository, curveRepository, patchRepository, entityPositionManager),
                PatchProperties = patch.ToPropertiesViewModel(),
                ValidationMessages = new List<MessageDto>()
            };

            viewModel.PatchDetails.CanSave = true;

            return viewModel;
        }
    }
}