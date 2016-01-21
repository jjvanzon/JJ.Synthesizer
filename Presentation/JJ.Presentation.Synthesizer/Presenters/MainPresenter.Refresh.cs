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
            object viewModel2 = _audioFileOutputGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void CurrentPatchesRefresh()
        {
            _currentPatchesPresenter.Refresh();
            DispatchViewModel(_currentPatchesPresenter.ViewModel);
        }

        private void CurveGridRefresh(CurveGridViewModel curveGridViewModel)
        {
            _curveGridPresenter.ViewModel = curveGridViewModel;
            object viewModel2 = _curveGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void CurveGridItemRefresh(int curveID)
        {
            CurveGridViewModel gridViewModel = DocumentViewModelHelper.GetCurveGridViewModel_ByCurveID(ViewModel.Document, curveID);
            _curveGridPresenter.ViewModel = gridViewModel;
            _curveGridPresenter.RefreshListItem(curveID);
        }

        private void CurveLookupsItemsRefresh(int curveID)
        {
            Curve curve = _repositories.CurveRepository.Get(curveID);

            foreach (PatchDocumentViewModel patchDocumentViewModel in ViewModel.Document.PatchDocumentList)
            {
                IDAndName idAndName2 = patchDocumentViewModel.CurveLookup.Where(x => x.ID == curve.ID).FirstOrDefault();
                if (idAndName2 != null)
                {
                    idAndName2.Name = curve.Name;
                    patchDocumentViewModel.CurveLookup = patchDocumentViewModel.CurveLookup.OrderBy(x => x.Name).ToList();
                }
            }
        }

        private void CurveDetailsNodeRefresh(int nodeID)
        {
            // TODO: This is not very fast.
            CurveDetailsViewModel detailsViewModel = DocumentViewModelHelper.GetCurveDetailsViewModel_ByNodeID(ViewModel.Document, nodeID);

            // Remove original node
            detailsViewModel.Nodes.RemoveFirst(x => x.ID == nodeID);

            // Add new version of the node
            Node node = _repositories.NodeRepository.Get(nodeID);
            NodeViewModel nodeViewModel = node.ToViewModel();
            detailsViewModel.Nodes.Add(nodeViewModel);
        }

        private void DocumentGridRefresh()
        {
            _documentGridPresenter.Refresh();
            DispatchViewModel(_documentGridPresenter.ViewModel);
        }

        private void DocumentTreeRefresh()
        {
            object viewModel2 = _documentTreePresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void OperatorProperties_ForCustomOperatorViewModels_Refresh(int underlyingPatchID)
        {
            IList<OperatorPropertiesViewModel_ForCustomOperator> propertiesViewModelList = 
                ViewModel.Document.PatchDocumentList.SelectMany(x => x.OperatorPropertiesList_ForCustomOperators).ToArray();

            foreach (OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel in propertiesViewModelList)
            {
                OperatorProperties_ForCustomOperatorViewModel_Refresh(propertiesViewModel);
            }
        }

        private void OperatorProperties_ForCustomOperatorViewModel_Refresh(OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel)
        {
            _operatorPropertiesPresenter_ForCustomOperator.ViewModel = propertiesViewModel;
            _operatorPropertiesPresenter_ForCustomOperator.Refresh();

            DispatchViewModel(_operatorPropertiesPresenter_ForCustomOperator.ViewModel);
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
            IList<PatchDetailsViewModel> patchDetailsViewModels = ViewModel.Document.PatchDocumentList.Select(x => x.PatchDetails).ToArray();

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
            OperatorViewModel viewModel = DocumentViewModelHelper.GetOperatorViewModel(ViewModel.Document, operatorID);
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

        private void PatchGridRefresh(string group)
        {
            PatchGridViewModel viewModel2 = DocumentViewModelHelper.GetPatchGridViewModel_ByGroup(ViewModel.Document, group);

            PatchGridRefresh(viewModel2);
        }

        private void PatchGridsRefresh()
        {
            // Patch grids can be updated, created and deleted as group names are changed.
            // All the logic in ToPatchGridViewModelList is required for this.
            Document document = _repositories.DocumentRepository.Get(ViewModel.Document.ID);
            ViewModel.Document.PatchGridList = document.ToPatchGridViewModelList();

            foreach (PatchGridViewModel gridViewModel in ViewModel.Document.PatchGridList.ToArray())
            {
                DispatchViewModel(gridViewModel);
            }
        }

        private void PatchGridRefresh(PatchGridViewModel patchGridViewModel)
        {
            if (patchGridViewModel == null) throw new NullException(() => patchGridViewModel);
            _patchGridPresenter.ViewModel = patchGridViewModel;
            object viewModel2 = _patchGridPresenter.Refresh();

            DispatchViewModel(viewModel2);
        }

        private void SampleGridRefresh(SampleGridViewModel sampleGridViewModel)
        {
            _sampleGridPresenter.ViewModel = sampleGridViewModel;
            object viewModel2 = _sampleGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void SampleGridRefreshItem(int sampleID)
        {
            SampleGridViewModel gridViewModel = DocumentViewModelHelper.GetSampleGridViewModel_BySampleID(ViewModel.Document, sampleID);
            _sampleGridPresenter.ViewModel = gridViewModel;
            _sampleGridPresenter.RefreshListItem(sampleID);
        }

        private void SampleLookupsRefresh(int sampleID)
        {
            Sample sample = _repositories.SampleRepository.Get(sampleID);

            foreach (PatchDocumentViewModel patchDocumentViewModel in ViewModel.Document.PatchDocumentList)
            {
                IDAndName idAndName2 = patchDocumentViewModel.SampleLookup.Where(x => x.ID == sample.ID).FirstOrDefault();
                if (idAndName2 != null)
                {
                    idAndName2.Name = sample.Name;
                    patchDocumentViewModel.SampleLookup = patchDocumentViewModel.SampleLookup.OrderBy(x => x.Name).ToList();
                }
            }
        }

        private void ScaleGridRefresh()
        {
            object viewModel2 = _scaleGridPresenter.Refresh();
            DispatchViewModel(viewModel2);
        }

        private void ToneGridEditRefresh(int scaleID)
        {
            ToneGridEditViewModel viewModel = DocumentViewModelHelper.GetToneGridEditViewModel(ViewModel.Document, scaleID);
            ToneGridEditRefresh(viewModel);
        }

        private void ToneGridEditRefresh(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = _toneGridEditPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void UnderylingDocumentLookupRefresh()
        {
            Document rootDocument = _repositories.DocumentRepository.Get(ViewModel.Document.ID);
            IList<Patch> patches = rootDocument.ChildDocuments.SelectMany(x => x.Patches).ToArray();
            ViewModel.Document.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(patches);
        }
    }
}
