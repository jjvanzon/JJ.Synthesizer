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
                DocumentTree = document.ToTreeViewModel(),
                DocumentProperties = document.ToPropertiesViewModel(),
                AudioFileOutputPropertiesList = document.AudioFileOutputs.Select(x => x.ToPropertiesViewModel(
                    repositoryWrapper.AudioFileFormatRepository,
                    repositoryWrapper.SampleDataTypeRepository,
                    repositoryWrapper.SpeakerSetupRepository)).ToList(),
                AudioFileOutputGrid = document.ToAudioFileOutputGridViewModel(),
                ChildDocumentPropertiesList = document.ChildDocuments.Select(x => x.ToChildDocumentPropertiesViewModel(repositoryWrapper.ChildDocumentTypeRepository)).ToList(),
                ChildDocumentList = document.ChildDocuments.Select(x => x.ToChildDocumentViewModel(repositoryWrapper, entityPositionManager)).ToList(),
                InstrumentGrid = document.ToChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Instrument),
                EffectGrid = document.ToChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Effect),
                CurveDetailsList = document.Curves.Select(x => x.ToDetailsViewModel(repositoryWrapper.NodeTypeRepository)).ToList(),
                CurveGrid = document.Curves.ToGridViewModel(document.ID),
                OperatorPropertiesList = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList()).ToList(),
                OperatorPropertiesList_ForCustomOperators = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCustomOperators(repositoryWrapper.DocumentRepository)).ToList(),
                OperatorPropertiesList_ForPatchInlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToList(),
                OperatorPropertiesList_ForPatchOutlets = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToList(),
                OperatorPropertiesList_ForSamples = document.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForSamples(repositoryWrapper.SampleRepository)).ToList(),
                OperatorPropertiesList_ForValues = document.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForValues()).ToList(),
                PatchDetailsList = document.Patches.Select(x => x.ToDetailsViewModel(repositoryWrapper.OperatorTypeRepository, repositoryWrapper.SampleRepository, repositoryWrapper.CurveRepository, repositoryWrapper.DocumentRepository, entityPositionManager)).ToList(),
                PatchGrid = document.Patches.ToGridViewModel(document.ID),
                SampleGrid = document.Samples.ToGridViewModel(document.ID),
                SamplePropertiesList = document.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositoryWrapper))).ToList(),
                UnderlyingDocumentLookup = ViewModelHelper.CreateUnderlyingDocumentLookupViewModel(document.ChildDocuments),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(document)
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
                SampleGrid = childDocument.Samples.ToGridViewModel(childDocument.ID),
                SamplePropertiesList = childDocument.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositoryWrapper))).ToList(),
                CurveGrid = childDocument.Curves.ToGridViewModel(childDocument.ID),
                CurveDetailsList = childDocument.Curves.Select(x => x.ToDetailsViewModel(repositoryWrapper.NodeTypeRepository)).ToList(),
                OperatorPropertiesList = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList()).ToList(),
                OperatorPropertiesList_ForCustomOperators = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCustomOperators(repositoryWrapper.DocumentRepository)).ToList(),
                OperatorPropertiesList_ForPatchInlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets()).ToList(),
                OperatorPropertiesList_ForPatchOutlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets()).ToList(),
                OperatorPropertiesList_ForSamples = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForSamples(repositoryWrapper.SampleRepository)).ToList(),
                OperatorPropertiesList_ForValues = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForValues()).ToList(),
                PatchGrid = childDocument.Patches.ToGridViewModel(childDocument.ID),
                PatchDetailsList = childDocument.Patches.Select(x => x.ToDetailsViewModel(repositoryWrapper.OperatorTypeRepository, repositoryWrapper.SampleRepository, repositoryWrapper.CurveRepository, repositoryWrapper.DocumentRepository, entityPositionManager)).ToList(),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(childDocument.ParentDocument, childDocument)
            };

            return viewModel;
        }
    }
}
