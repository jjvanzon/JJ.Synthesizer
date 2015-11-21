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
                ChildDocumentList = document.ChildDocuments.Select(x => x.ToChildDocumentViewModel(repositories, entityPositionManager)).ToList(),
                ChildDocumentPropertiesList = document.ChildDocuments.Select(x => x.ToChildDocumentPropertiesViewModel(repositories.ChildDocumentTypeRepository)).ToList(),
                CurveDetailsList = document.Curves.Select(x => x.ToDetailsViewModel(repositories.NodeTypeRepository)).ToList(),
                CurveGrid = document.Curves.ToGridViewModel(document.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(document),
                CurvePropertiesList = document.Curves.Select(x => x.ToPropertiesViewModel()).ToList(),
                DocumentProperties = document.ToPropertiesViewModel(),
                DocumentTree = document.ToTreeViewModel(),
                EffectGrid = document.ToChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Effect),
                InstrumentGrid = document.ToChildDocumentGridViewModel((int)ChildDocumentTypeEnum.Instrument),
                NodePropertiesList = document.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel(repositories.NodeTypeRepository)).ToList(),
                SampleGrid = document.Samples.ToGridViewModel(document.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(document),
                SamplePropertiesList = document.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositories))).ToList(),
                ScaleGrid = document.Scales.ToGridViewModel(document.ID),
                ScalePropertiesList = document.Scales.Select(x => x.ToPropertiesViewModel(repositories.ScaleTypeRepository)).ToList(),
                ToneGridEditList = document.Scales.Select(x => x.ToDetailsViewModel()).ToList(),
                UnderlyingDocumentLookup = ViewModelHelper.CreateUnderlyingDocumentLookupViewModel(document.ChildDocuments)
            };

            return viewModel;
        }

        public static ChildDocumentViewModel ToChildDocumentViewModel(
            this Document childDocument,
            RepositoryWrapper repositories,
            EntityPositionManager entityPositionManager)
        {
            if (childDocument == null) throw new NullException(() => childDocument);
            if (childDocument.ParentDocument == null) throw new NullException(() => childDocument);
            if (repositories == null) throw new NullException(() => repositories);

            var viewModel = new ChildDocumentViewModel
            {
                ID = childDocument.ID,
                CurveDetailsList = childDocument.Curves.Select(x => x.ToDetailsViewModel(repositories.NodeTypeRepository)).ToList(),
                CurveGrid = childDocument.Curves.ToGridViewModel(childDocument.ID),
                CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(childDocument.ParentDocument, childDocument),
                CurvePropertiesList = childDocument.Curves.Select(x => x.ToPropertiesViewModel()).ToList(),
                NodePropertiesList = childDocument.Curves.SelectMany(x => x.Nodes).Select(x => x.ToPropertiesViewModel(repositories.NodeTypeRepository)).ToList(),
                OperatorPropertiesList = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList()).ToList(),
                OperatorPropertiesList_ForBundles = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForBundles()).ToList(),
                OperatorPropertiesList_ForCurves = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCurves(repositories.CurveRepository)).ToList(),
                OperatorPropertiesList_ForCustomOperators = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForCustomOperators(repositories.DocumentRepository)).ToList(),
                OperatorPropertiesList_ForNumbers = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForNumbers()).ToList(),
                OperatorPropertiesList_ForPatchInlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchInlets(repositories.InletTypeRepository)).ToList(),
                OperatorPropertiesList_ForPatchOutlets = childDocument.Patches.SelectMany(x => x.ToPropertiesViewModelList_ForPatchOutlets(repositories.OutletTypeRepository)).ToList(),
                OperatorPropertiesList_ForSamples = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForSamples(repositories.SampleRepository)).ToList(),
                OperatorPropertiesList_ForUnbundles = childDocument.Patches.SelectMany(x => x.ToOperatorPropertiesViewModelList_ForUnbundles()).ToList(),
                PatchDetails = childDocument.Patches[0].ToDetailsViewModel(repositories.OperatorTypeRepository, repositories.SampleRepository, repositories.CurveRepository, repositories.DocumentRepository, entityPositionManager),
                SampleGrid = childDocument.Samples.ToGridViewModel(childDocument.ID),
                SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(childDocument.ParentDocument, childDocument),
                SamplePropertiesList = childDocument.Samples.Select(x => x.ToPropertiesViewModel(new SampleRepositories(repositories))).ToList()
            };

            return viewModel;
        }
    }
}
