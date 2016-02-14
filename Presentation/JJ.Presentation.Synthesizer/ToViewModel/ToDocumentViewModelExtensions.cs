using System.Linq;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Collections.Generic;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToDocumentViewModelExtensions
    {
        public static DocumentViewModel ToViewModel(this Document document, RepositoryWrapper repositories, EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);
            if (repositories == null) throw new NullException(() => repositories);

            var viewModel = new DocumentViewModel
            {
                ID = document.ID,
                AudioFileOutputGrid = document.ToAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesList = document.AudioFileOutputs.Select(x => x.ToPropertiesViewModel(
                    repositories.AudioFileFormatRepository,
                    repositories.SampleDataTypeRepository,
                    repositories.SpeakerSetupRepository)).ToList(),
                PatchDocumentList = document.ChildDocuments.Select(x => x.ToPatchDocumentViewModel(repositories, entityPositionManager)).ToList(),
                CurrentPatches = ViewModelHelper.CreateEmptyCurrentPatchesViewModel(),
                CurveDetailsList = document.Curves.Select(x => x.ToDetailsViewModel(repositories.NodeTypeRepository)).ToList(),
                CurveGrid = document.Curves.ToGridViewModel(document.ID),
                CurvePropertiesList = document.Curves.Select(x => x.ToPropertiesViewModel()).ToList(),
                DocumentProperties = document.ToPropertiesViewModel(),
                DocumentTree = document.ToTreeViewModel(),
                PatchGridList = document.ToPatchGridViewModelList(),
                NodePropertiesList = document.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel(repositories.NodeTypeRepository)).ToList(),
                SampleGrid = document.Samples.ToGridViewModel(document.ID),
                SamplePropertiesList = document.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositories))).ToList(),
                ScaleGrid = document.Scales.ToGridViewModel(document.ID),
                ScalePropertiesList = document.Scales.Select(x => x.ToPropertiesViewModel(repositories.ScaleTypeRepository)).ToList(),
                ToneGridEditList = document.Scales.Select(x => x.ToToneGridEditViewModel()).ToList(),
                AutoPatchDetails = ViewModelHelper.CreateEmptyPatchDetailsViewModel()
            };

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

            var viewModel = new PatchDocumentViewModel
            {
                ChildDocumentID = childDocument.ID,
                CurveDetailsList = childDocument.Curves.Select(x => x.ToDetailsViewModel(repositories.NodeTypeRepository)).ToList(),
                CurveGrid = childDocument.Curves.ToGridViewModel(childDocument.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(childDocument.ParentDocument, childDocument),
                CurvePropertiesList = childDocument.Curves.Select(x => x.ToPropertiesViewModel()).ToList(),
                NodePropertiesList = childDocument.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel(repositories.NodeTypeRepository)).ToList(),
                OperatorPropertiesList = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList()).ToList(),
                OperatorPropertiesList_ForAggregates = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForAggregates()).ToList(),
                OperatorPropertiesList_ForBundles = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForBundles()).ToList(),
                OperatorPropertiesList_ForCurves = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCurves(repositories.CurveRepository)).ToList(),
                OperatorPropertiesList_ForCustomOperators = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForCustomOperators(repositories.PatchRepository)).ToList(),
                OperatorPropertiesList_ForNumbers = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToList(),
                OperatorPropertiesList_ForPatchInlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets(repositories.InletTypeRepository)).ToList(),
                OperatorPropertiesList_ForPatchOutlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets(repositories.OutletTypeRepository)).ToList(),
                OperatorPropertiesList_ForRandoms = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForRandoms()).ToList(),
                OperatorPropertiesList_ForResamples = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForResamples()).ToList(),
                OperatorPropertiesList_ForSamples = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSamples(repositories.SampleRepository)).ToList(),
                OperatorPropertiesList_ForSpectrums = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSpectrums()).ToList(),
                OperatorPropertiesList_ForUnbundles = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForUnbundles()).ToList(),
                PatchDetails = childDocument.Patches[0].ToDetailsViewModel(repositories.OperatorTypeRepository, repositories.SampleRepository, repositories.CurveRepository, repositories.PatchRepository, entityPositionManager),
                PatchProperties = childDocument.ToPatchPropertiesViewModel(),
                SampleGrid = childDocument.Samples.ToGridViewModel(childDocument.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(childDocument.ParentDocument, childDocument),
                SamplePropertiesList = childDocument.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositories))).ToList()
            };

            return viewModel;
        }
    }
}
