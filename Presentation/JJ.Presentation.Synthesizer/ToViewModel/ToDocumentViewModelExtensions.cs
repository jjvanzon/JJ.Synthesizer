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

            // This should eventually (2016-01-22) be one of the few places where ToViewModel will set Successful to true.
            viewModel.AudioFileOutputGrid.Successful = true;
            viewModel.AudioFileOutputPropertiesList.ForEach(x => x.Successful = true);
            viewModel.CurrentPatches.Successful = true;
            viewModel.CurveDetailsList.ForEach(x => x.Successful = true);
            viewModel.CurveGrid.Successful = true;
            viewModel.CurvePropertiesList.ForEach(x => x.Successful = true);
            viewModel.DocumentProperties.Successful = true;
            viewModel.DocumentTree.Successful = true;
            viewModel.PatchGridList.ForEach(x => x.Successful = true);
            viewModel.NodePropertiesList.ForEach(x => x.Successful = true);
            viewModel.SampleGrid.Successful = true;
            viewModel.SamplePropertiesList.ForEach(x => x.Successful = true);
            viewModel.ScaleGrid.Successful = true;
            viewModel.ScalePropertiesList.ForEach(x => x.Successful = true);
            viewModel.ToneGridEditList.ForEach(x => x.Successful = true);
            viewModel.AutoPatchDetails.Successful = true;
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
                OperatorPropertiesList_ForSamples = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSamples(repositories.SampleRepository)).ToList(),
                OperatorPropertiesList_ForSpectrums = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForSpectrums()).ToList(),
                OperatorPropertiesList_ForUnbundles = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForUnbundles()).ToList(),
                PatchDetails = childDocument.Patches[0].ToDetailsViewModel(repositories.OperatorTypeRepository, repositories.SampleRepository, repositories.CurveRepository, repositories.PatchRepository, entityPositionManager),
                PatchProperties = childDocument.ToPatchPropertiesViewModel(),
                SampleGrid = childDocument.Samples.ToGridViewModel(childDocument.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(childDocument.ParentDocument, childDocument),
                SamplePropertiesList = childDocument.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositories))).ToList()
            };

            // This should eventually (2016-01-22) be one of the few places where ToViewModel will set Successful to true.
            viewModel.CurveDetailsList.ForEach(x => x.Successful = true);
            viewModel.CurveGrid.Successful = true;
            viewModel.CurvePropertiesList.ForEach(x => x.Successful = true);
            viewModel.NodePropertiesList.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForAggregates.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForBundles.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForCurves.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForCustomOperators.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForNumbers.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForPatchInlets.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForPatchOutlets.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForSamples.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesList_ForUnbundles.ForEach(x => x.Successful = true);
            viewModel.PatchDetails.Successful = true;
            viewModel.PatchProperties.Successful = true;
            viewModel.SampleGrid.Successful = true;
            viewModel.SamplePropertiesList.Select(x => x.Successful = true);

            return viewModel;
        }
    }
}
