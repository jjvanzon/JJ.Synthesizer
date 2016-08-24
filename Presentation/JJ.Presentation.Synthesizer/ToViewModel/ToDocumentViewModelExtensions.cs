using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Dto;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToDocumentViewModelExtensions
    {
        public static DocumentViewModel ToViewModel(
            this Document document,
            IList<Document> grouplessChildDocuments,
            IList<ChildDocumentGroupDto> childDocumentGroupDtos,
            RepositoryWrapper repositories, 
            EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);
            if (repositories == null) throw new NullException(() => repositories);

            var viewModel = new DocumentViewModel
            {
                ID = document.ID,
                AudioFileOutputGrid = document.ToAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesDictionary = document.AudioFileOutputs.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                PatchDocumentDictionary = document.ChildDocuments.Select(x => x.ToPatchDocumentViewModel(repositories, entityPositionManager)).ToDictionary(x => x.ChildDocumentID),
                CurrentPatches = ViewModelHelper.CreateEmptyCurrentPatchesViewModel(),
                CurveDetailsDictionary = document.Curves.Select(x => x.ToDetailsViewModel()).ToDictionary(x => x.CurveID),
                CurveGrid = document.Curves.ToGridViewModel(document.ID),
                CurvePropertiesDictionary = document.Curves.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
                DocumentProperties = document.ToPropertiesViewModel(),
                DocumentTree = document.ToTreeViewModel(grouplessChildDocuments, childDocumentGroupDtos),
                PatchGridDictionary = ViewModelHelper.CreatePatchGridViewModelDictionary(grouplessChildDocuments, childDocumentGroupDtos, document.ID),
                NodePropertiesDictionary = document.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                SampleGrid = document.Samples.ToGridViewModel(document.ID),
                SamplePropertiesDictionary = document.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositories))).ToDictionary(x => x.Entity.ID),
                ScaleGrid = document.Scales.ToGridViewModel(document.ID),
                ScalePropertiesDictionary = document.Scales.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                ToneGridEditDictionary = document.Scales.Select(x => x.ToToneGridEditViewModel()).ToDictionary(x => x.ScaleID),
                AutoPatchDetails = ViewModelHelper.CreateEmptyPatchDetailsViewModel()
            };

            if (document.AudioOutput != null)
            {
                viewModel.AudioOutputProperties = document.AudioOutput.ToPropertiesViewModel();
            }
            else
            {
                viewModel.AudioOutputProperties = ViewModelHelper.CreateEmptyAudioOutputPropertiesViewModel();
            }

            IList<Patch> patches = document.ChildDocuments.SelectMany(x => x.Patches).ToArray();
            viewModel.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(patches);

            return viewModel;
        }

        public static PatchDocumentViewModel ToPatchDocumentViewModel(
            this Document childDocument,
            RepositoryWrapper repositories,
            EntityPositionManager entityPositionManager)
        {
            if (childDocument == null) throw new NullException(() => childDocument);
            if (repositories == null) throw new NullException(() => repositories);
            if (childDocument.ParentDocument == null) throw new NullException(() => childDocument);
            if (childDocument.Patches.Count < 1) throw new LessThanException(() => childDocument.Patches.Count, 1);

            var sampleRepositories = new SampleRepositories(repositories);

            var viewModel = new PatchDocumentViewModel
            {
                ChildDocumentID = childDocument.ID,
                CurveDetailsDictionary = childDocument.Curves.Select(x => x.ToDetailsViewModel()).ToDictionary(x => x.CurveID),
                CurveGrid = childDocument.Curves.ToGridViewModel(childDocument.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(childDocument.ParentDocument, childDocument),
                CurvePropertiesDictionary = childDocument.Curves.Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.ID),
                NodePropertiesDictionary = childDocument.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel()).ToDictionary(x => x.Entity.ID),
                OperatorPropertiesDictionary = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_WithoutAlternativePropertiesView()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForBundles = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForBundles()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCaches = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCaches(repositories.InterpolationTypeRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCurves = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCurves(repositories.CurveRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForCustomOperators = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCustomOperators(repositories.PatchRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForMakeContinuous = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForMakeContinuous()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForNumbers = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchInlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForPatchOutlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_ForSamples = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSamples(repositories.SampleRepository)).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimension = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimension()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimensionAndCollectionRecalculation = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimensionAndCollectionRecalculation()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimensionAndInterpolation = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimensionAndInterpolation()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithDimensionAndOutletCount = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithDimensionAndOutletCount()).ToDictionary(x => x.ID),
                OperatorPropertiesDictionary_WithInletCount = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_WithInletCount()).ToDictionary(x => x.ID),
                PatchDetails = childDocument.Patches[0].ToDetailsViewModel(repositories.OperatorTypeRepository, repositories.SampleRepository, repositories.CurveRepository, repositories.PatchRepository, entityPositionManager),
                PatchProperties = childDocument.ToPatchPropertiesViewModel(),
                SampleGrid = childDocument.Samples.ToGridViewModel(childDocument.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(childDocument.ParentDocument, childDocument),
                SamplePropertiesDictionary = childDocument.Samples.Select(x => x.ToPropertiesViewModel(sampleRepositories)).ToDictionary(x => x.Entity.ID)
            };

            return viewModel;
        }
    }
}
