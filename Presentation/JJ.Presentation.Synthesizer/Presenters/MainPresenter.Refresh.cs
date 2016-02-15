using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        private void AudioFileOutputGridRefresh()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Partial Action
            AudioFileOutputGridViewModel viewModel = _audioFileOutputGridPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void AudioFileOutputPropertiesRefresh(AudioFileOutputPropertiesViewModel userInput)
        {
            // Partial Action
            AudioFileOutputPropertiesViewModel viewModel = _audioFileOutputPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void CurrentPatchesRefresh()
        {
            // GetViewModel
            CurrentPatchesViewModel userInput = MainViewModel.Document.CurrentPatches;

            // Partial Action
            CurrentPatchesViewModel viewModel = _currentPatchesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void CurveDetailsNodeRefresh(int nodeID)
        {
            // TODO: This is not very fast.
            CurveDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetCurveDetailsViewModel_ByNodeID(MainViewModel.Document, nodeID);

            // Remove original node
            detailsViewModel.Nodes.RemoveFirst(x => x.ID == nodeID);

            // Add new version of the node
            Node node = _repositories.NodeRepository.Get(nodeID);
            NodeViewModel nodeViewModel = node.ToViewModel();
            detailsViewModel.Nodes.Add(nodeViewModel);
        }

        private void CurveDetailsRefresh(CurveDetailsViewModel userInput)
        {
            CurveDetailsViewModel viewModel = _curveDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void CurveGridRefresh(CurveGridViewModel userInput)
        {
            CurveGridViewModel viewModel = _curveGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void CurveLookupsItemsRefresh(int curveID)
        {
            Curve curve = _repositories.CurveRepository.Get(curveID);

            foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
            {
                IDAndName idAndName2 = patchDocumentViewModel.CurveLookup.Where(x => x.ID == curve.ID).FirstOrDefault();
                if (idAndName2 != null)
                {
                    idAndName2.Name = curve.Name;
                    patchDocumentViewModel.CurveLookup = patchDocumentViewModel.CurveLookup.OrderBy(x => x.Name).ToList();
                }
            }
        }

        private void CurvePropertiesRefresh(CurvePropertiesViewModel userInput)
        {
            CurvePropertiesViewModel viewModel = _curvePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void DocumentGridRefresh()
        {
            TemplateActionMethod(MainViewModel.DocumentGrid, _documentGridPresenter.Refresh);
        }

        private void DocumentPropertiesRefresh()
        {
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;
            DocumentPropertiesViewModel viewModel = _documentPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void DocumentTreeRefresh()
        {
            DocumentTreeViewModel viewModel = _documentTreePresenter.Refresh(MainViewModel.Document.DocumentTree);
            DispatchViewModel(viewModel);
        }

        private void DocumentViewModelRefresh()
        {
            DocumentViewModel viewModel = MainViewModel.Document;

            AudioFileOutputGridRefresh();
            CurrentPatchesRefresh();
            CurveGridRefresh(viewModel.CurveGrid);
            DocumentPropertiesRefresh();
            DocumentTreeRefresh();
            SampleGridRefresh(viewModel.SampleGrid);
            ScaleGridRefresh();
            viewModel.AudioFileOutputPropertiesList.ToArray().ForEach(x => AudioFileOutputPropertiesRefresh(x));
            viewModel.CurveDetailsList.ToArray().ForEach(x => CurveDetailsRefresh(x));
            viewModel.CurvePropertiesList.ToArray().ForEach(x => CurvePropertiesRefresh(x));
            viewModel.NodePropertiesList.ToArray().ForEach(x => NodePropertiesRefresh(x));
            viewModel.PatchGridList.ToArray().ForEach(x => PatchGridRefresh(x));
            viewModel.SamplePropertiesList.ToArray().ForEach(x => SamplePropertiesRefresh(x));
            viewModel.ScalePropertiesList.ToArray().ForEach(x => ScalePropertiesRefresh(x));
            viewModel.ToneGridEditList.ToArray().ForEach(x => ToneGridEditRefresh(x));
            UnderylingPatchLookupRefresh();

            // Note that AutoPatchDetails cannot be refreshed.

            foreach (PatchDocumentViewModel patchDocumentViewModel in viewModel.PatchDocumentList)
            {
                CurveGridRefresh(patchDocumentViewModel.CurveGrid);
                CurveLookupRefresh(patchDocumentViewModel);
                PatchDetailsRefresh(patchDocumentViewModel.PatchDetails);
                patchDocumentViewModel.CurveDetailsList.ToArray().ForEach(x => CurveDetailsRefresh(x));
                patchDocumentViewModel.CurvePropertiesList.ToArray().ForEach(x => CurvePropertiesRefresh(x));
                patchDocumentViewModel.NodePropertiesList.ToArray().ForEach(x => NodePropertiesRefresh(x));
                patchDocumentViewModel.OperatorPropertiesList.ToArray().ForEach(x => OperatorPropertiesRefresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForAggregates.ToArray().ForEach(x => OperatorProperties_ForAggregate_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForBundles.ToArray().ForEach(x => OperatorProperties_ForBundle_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForCurves.ToArray().ForEach(x =>  OperatorProperties_ForCurve_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForCustomOperators.ToArray().ForEach(x => OperatorProperties_ForCustomOperator_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForNumbers.ToArray().ForEach(x =>  OperatorProperties_ForNumber_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForPatchInlets.ToArray().ForEach(x => OperatorProperties_ForPatchInlet_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForPatchOutlets.ToArray().ForEach(x => OperatorProperties_ForPatchOutletRefresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForRandoms.ToArray().ForEach(x =>  OperatorProperties_ForRandom_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForResamples.ToArray().ForEach(x => OperatorProperties_ForResample_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForSamples.ToArray().ForEach(x =>  OperatorProperties_ForSample_Refresh(x));
                patchDocumentViewModel.OperatorPropertiesList_ForUnbundles.ToArray().ForEach(x => OperatorProperties_ForUnbundle_Refresh(x));
                patchDocumentViewModel.SamplePropertiesList.ToArray().ForEach(x => SamplePropertiesRefresh(x));
                PatchPropertiesRefresh(patchDocumentViewModel.PatchProperties);
                SampleGridRefresh(patchDocumentViewModel.SampleGrid);
                SampleLookupRefresh(patchDocumentViewModel);
            }
        }

        private void CurveLookupRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            patchDocumentViewModel.CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(rootDocument, childDocument);
        }

        private void NodePropertiesRefresh(NodePropertiesViewModel userInput)
        {
            NodePropertiesViewModel viewModel = _nodePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForAggregate_Refresh(OperatorPropertiesViewModel_ForAggregate userInput)
        {
            OperatorPropertiesViewModel_ForAggregate viewModel = _operatorPropertiesPresenter_ForAggregate.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForBundle_Refresh(OperatorPropertiesViewModel_ForBundle userInput)
        {
            OperatorPropertiesViewModel_ForBundle viewModel = _operatorPropertiesPresenter_ForBundle.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCurve_Refresh(OperatorPropertiesViewModel_ForCurve userInput)
        {
            OperatorPropertiesViewModel_ForCurve viewModel = _operatorPropertiesPresenter_ForCurve.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCustomOperator_Refresh(OperatorPropertiesViewModel_ForCustomOperator userInput)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = _operatorPropertiesPresenter_ForCustomOperator.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCustomOperatorViewModel_Refresh(OperatorPropertiesViewModel_ForCustomOperator userInput)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = _operatorPropertiesPresenter_ForCustomOperator.Refresh(userInput);

            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCustomOperatorViewModels_Refresh(int underlyingPatchID)
        {
            IList<OperatorPropertiesViewModel_ForCustomOperator> propertiesViewModelList = 
                MainViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators).ToArray();

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in propertiesViewModelList)
            {
                OperatorProperties_ForCustomOperatorViewModel_Refresh(propertiesViewModel);
            }
        }

        private void OperatorProperties_ForNumber_Refresh(OperatorPropertiesViewModel_ForNumber userInput)
        {
            OperatorPropertiesViewModel_ForNumber viewModel = _operatorPropertiesPresenter_ForNumber.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForPatchInlet_Refresh(OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            OperatorPropertiesViewModel_ForPatchInlet viewModel = _operatorPropertiesPresenter_ForPatchInlet.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForPatchOutletRefresh(OperatorPropertiesViewModel_ForPatchOutlet userInput)
        {
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = _operatorPropertiesPresenter_ForPatchOutlet.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForRandom_Refresh(OperatorPropertiesViewModel_ForRandom userInput)
        {
            OperatorPropertiesViewModel_ForRandom viewModel = _operatorPropertiesPresenter_ForRandom.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForResample_Refresh(OperatorPropertiesViewModel_ForResample userInput)
        {
            OperatorPropertiesViewModel_ForResample viewModel = _operatorPropertiesPresenter_ForResample.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForSample_Refresh(OperatorPropertiesViewModel_ForSample userInput)
        {
            OperatorPropertiesViewModel_ForSample viewModel = _operatorPropertiesPresenter_ForSample.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForUnbundle_Refresh(OperatorPropertiesViewModel_ForUnbundle userInput)
        {
            OperatorPropertiesViewModel_ForUnbundle viewModel = _operatorPropertiesPresenter_ForUnbundle.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorPropertiesRefresh(OperatorPropertiesViewModel userInput)
        {
            OperatorPropertiesViewModel viewModel = _operatorPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        /// <summary>
        /// When an underlying document of a custom operator is changed,
        /// we do not know which PatchDetails OperatorViewModels are affected,
        /// because no OperatorViewModel has as property saying what UnderlyingPatch it is. 
        /// Therefore we refresh all CustomOperators.
        /// 
        /// But also, a custom operator would need to be updated if something connected to it is deleted,
        /// because then the obsolete inlets and outlets might be cleaned up.
        /// </summary>
        private void OperatorViewModels_OfType_Refresh(OperatorTypeEnum operatorTypeEnum)
        {
            IList<PatchDetailsViewModel> patchDetailsViewModels = MainViewModel.Document.PatchDocumentList.Select(x => x.PatchDetails).ToArray();

            IList<OperatorViewModel> operatorViewModels =
                patchDetailsViewModels.SelectMany(x => x.Entity.Operators)
                                      .Where(x => x.OperatorType.ID == (int)operatorTypeEnum)
                                      .ToArray();

            foreach (OperatorViewModel operatorViewModel in operatorViewModels)
            {
                PatchDetails_RefreshOperator(operatorViewModel);
            }
        }

        private void PatchDetails_RefreshOperator(int operatorID)
        {
            OperatorViewModel viewModel = DocumentViewModelHelper.GetOperatorViewModel(MainViewModel.Document, operatorID);
            PatchDetails_RefreshOperator(viewModel);
        }

        private void PatchDetails_RefreshOperator(OperatorViewModel viewModel)
        {
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);
            PatchDetails_RefreshOperator(entity, viewModel);
        }

        private void PatchDetails_RefreshOperator(Operator entity, OperatorViewModel operatorViewModel)
        {
            // TODO: Not sure if I should also have a variation in which I call UpdateViewModel_WithoutEntityPosition instead.
            ViewModelHelper.RefreshViewModel_WithInletsAndOutlets_WithoutEntityPosition(
                entity, operatorViewModel,
                _repositories.SampleRepository, _repositories.CurveRepository, _repositories.PatchRepository);
        }

        private void PatchDetailsRefresh(PatchDetailsViewModel userInput)
        {
            PatchDetailsViewModel viewModel = _patchDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchGridRefresh(string group)
        {
            PatchGridViewModel viewModel2 = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(MainViewModel.Document, group);

            PatchGridRefresh(viewModel2);
        }

        private void PatchGridRefresh(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            PatchGridViewModel viewModel = _patchGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchGridsRefresh()
        {
            // Patch grids can be updated, created and deleted as group names are changed.
            // All the logic in ToPatchGridViewModelList is required for this.
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            MainViewModel.Document.PatchGridList = document.ToPatchGridViewModelList();

            foreach (PatchGridViewModel gridViewModel in MainViewModel.Document.PatchGridList.ToArray())
            {
                DispatchViewModel(gridViewModel);
            }
        }

        private void PatchPropertiesRefresh(PatchPropertiesViewModel userInput)
        {
            PatchPropertiesViewModel viewModel = _patchPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void SampleGridRefresh(SampleGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            SampleGridViewModel viewModel = _sampleGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        /// <summary> Will update the SampleGrid that the sample with sampleID is part of. </summary>
        private void SampleGridRefresh(int sampleID)
        {
            SampleGridViewModel userInput = DocumentViewModelHelper.GetSampleGridViewModel_BySampleID(MainViewModel.Document, sampleID);
            SampleGridViewModel viewModel = _sampleGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void SampleLookupsRefresh(int sampleID)
        {
            Sample sample = _repositories.SampleRepository.Get(sampleID);

            foreach (PatchDocumentViewModel patchDocumentViewModel in MainViewModel.Document.PatchDocumentList)
            {
                IDAndName idAndName2 = patchDocumentViewModel.SampleLookup.Where(x => x.ID == sample.ID).FirstOrDefault();
                if (idAndName2 != null)
                {
                    idAndName2.Name = sample.Name;
                    patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
                }
            }
        }

        private void SampleLookupRefresh(PatchDocumentViewModel patchDocumentViewModel)
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            Document childDocument = _repositories.DocumentRepository.Get(patchDocumentViewModel.ChildDocumentID);
            patchDocumentViewModel.SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(rootDocument, childDocument);
        }

        private void SamplePropertiesRefresh(SamplePropertiesViewModel userInput)
        {
            SamplePropertiesViewModel viewModel = _samplePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ScaleGridRefresh()
        {
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;
            ScaleGridViewModel viewModel = _scaleGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ScalePropertiesRefresh(ScalePropertiesViewModel userInput)
        {
            ScalePropertiesViewModel viewModel = _scalePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ToneGridEditRefresh(int scaleID)
        {
            ToneGridEditViewModel viewModel = DocumentViewModelHelper.GetToneGridEditViewModel(MainViewModel.Document, scaleID);
            ToneGridEditRefresh(viewModel);
        }

        private void ToneGridEditRefresh(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = _toneGridEditPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void UnderylingPatchLookupRefresh()
        {
            Document rootDocument = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> patches = rootDocument.ChildDocuments.SelectMany(x => x.Patches).ToArray();
            MainViewModel.Document.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(patches);
        }
    }
}
