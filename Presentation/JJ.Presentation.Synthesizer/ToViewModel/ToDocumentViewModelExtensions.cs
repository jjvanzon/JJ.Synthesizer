using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System.Linq;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToDocumentViewModelExtensions
    {
        public static DocumentViewModel ToViewModel(this Document document, RepositoryWrapper repositoryWrapper, EntityPositionManager entityPositionManager)
        {
            if (document == null) throw new NullException(() => document);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            var viewModel = new DocumentViewModel
            {
                ID = document.ID,
                AudioFileOutputGrid = document.ToAudioFileOutputGridViewModel(),
                AudioFileOutputPropertiesList = document.AudioFileOutputs.Select(x => x.ToPropertiesViewModel(
                    repositoryWrapper.AudioFileFormatRepository,
                    repositoryWrapper.SampleDataTypeRepository,
                    repositoryWrapper.SpeakerSetupRepository)).ToList(),
                ChildDocumentList = document.ChildDocuments.Select(x => x.ToChildDocumentViewModel(repositoryWrapper, entityPositionManager)).ToList(),
                ChildDocumentPropertiesList = document.ChildDocuments.Select(x => x.ToChildDocumentPropertiesViewModel(repositoryWrapper.ChildDocumentTypeRepository)).ToList(),
                CurveDetailsList = document.Curves.Select(x => x.ToDetailsViewModel(repositoryWrapper.NodeTypeRepository)).ToList(),
                CurveGrid = document.Curves.ToGridViewModel(document.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(document),
                CurvePropertiesList = document.Curves.Select(x => x.ToPropertiesViewModel()).ToList(),
                DocumentProperties = document.ToPropertiesViewModel(),
                DocumentTree = document.ToTreeViewModel(),
                EffectGrid = document.ToChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Effect),
                InstrumentGrid = document.ToChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Instrument),
                NodePropertiesList = document.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel(repositoryWrapper.NodeTypeRepository)).ToList(),
                OperatorPropertiesList = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList()).ToList(),
                OperatorPropertiesList_ForCurves = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCurves(repositoryWrapper.CurveRepository)).ToList(),
                OperatorPropertiesList_ForCustomOperators = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCustomOperators(repositoryWrapper.DocumentRepository)).ToList(),
                OperatorPropertiesList_ForNumbers = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToList(),
                OperatorPropertiesList_ForPatchInlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToList(),
                OperatorPropertiesList_ForPatchOutlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToList(),
                OperatorPropertiesList_ForSamples = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForSamples(repositoryWrapper.SampleRepository)).ToList(),
                PatchDetailsList = document.Patches.Select(x => x.ToDetailsViewModel(repositoryWrapper.OperatorTypeRepository, repositoryWrapper.SampleRepository, repositoryWrapper.CurveRepository, repositoryWrapper.DocumentRepository, entityPositionManager)).ToList(),
                PatchGrid = document.Patches.ToGridViewModel(document.ID),
                SampleGrid = document.Samples.ToGridViewModel(document.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(document),
                SamplePropertiesList = document.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositoryWrapper))).ToList(),
                ScaleGrid = document.Scales.ToGridViewModel(document.ID),
                ScalePropertiesList = document.Scales.Select(x => x.ToPropertiesViewModel(repositoryWrapper.ScaleTypeRepository)).ToList(),
                ToneGridEditList = document.Scales.Select(x => x.ToDetailsViewModel()).ToList(),
                UnderlyingDocumentLookup = ViewModelHelper.CreateUnderlyingDocumentLookupViewModel(document.ChildDocuments)
            };

            return viewModel;
        }

        public static ChildDocumentViewModel ToChildDocumentViewModel(
            this Document childDocument,
            RepositoryWrapper repositoryWrapper,
            EntityPositionManager entityPositionManager)
        {
            if (childDocument == null) throw new NullException(() => childDocument);
            if (childDocument.ParentDocument == null) throw new NullException(() => childDocument);
            if (repositoryWrapper == null) throw new NullException(() => repositoryWrapper);

            var viewModel = new ChildDocumentViewModel
            {
                ID = childDocument.ID,
                CurveDetailsList = childDocument.Curves.Select(x => x.ToDetailsViewModel(repositoryWrapper.NodeTypeRepository)).ToList(),
                CurveGrid = childDocument.Curves.ToGridViewModel(childDocument.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(childDocument.ParentDocument, childDocument),
                CurvePropertiesList = childDocument.Curves.Select(x => x.ToPropertiesViewModel()).ToList(),
                NodePropertiesList = childDocument.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel(repositoryWrapper.NodeTypeRepository)).ToList(),
                OperatorPropertiesList = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList()).ToList(),
                OperatorPropertiesList_ForCurves = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCurves(repositoryWrapper.CurveRepository)).ToList(),
                OperatorPropertiesList_ForCustomOperators = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCustomOperators(repositoryWrapper.DocumentRepository)).ToList(),
                OperatorPropertiesList_ForNumbers = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToList(),
                OperatorPropertiesList_ForPatchInlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToList(),
                OperatorPropertiesList_ForPatchOutlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToList(),
                OperatorPropertiesList_ForSamples = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForSamples(repositoryWrapper.SampleRepository)).ToList(),
                PatchDetailsList = childDocument.Patches.Select(x => x.ToDetailsViewModel(repositoryWrapper.OperatorTypeRepository, repositoryWrapper.SampleRepository, repositoryWrapper.CurveRepository, repositoryWrapper.DocumentRepository, entityPositionManager)).ToList(),
                PatchGrid = childDocument.Patches.ToGridViewModel(childDocument.ID),
                SampleGrid = childDocument.Samples.ToGridViewModel(childDocument.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(childDocument.ParentDocument, childDocument),
                SamplePropertiesList = childDocument.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositoryWrapper))).ToList()
            };

            return viewModel;
        }
    }
}
