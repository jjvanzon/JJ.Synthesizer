using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        // General Actions

        public void Show()
        {
            try
            {
                ViewModel = ViewModelHelper.CreateEmptyMainViewModel();

                MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
                DispatchViewModel(menuViewModel);

                _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
                _documentGridPresenter.Show();
                DispatchViewModel(_documentGridPresenter.ViewModel);

                ViewModel.WindowTitle = Titles.ApplicationName;
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void NotFoundOK()
        {
            try
            {
                _notFoundPresenter.OK();
                DispatchViewModel(_notFoundPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PopupMessagesOK()
        {
            try
            {
                ViewModel.PopupMessages = new List<Message> { };
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // AudioFileOutput Actions

        public void AudioFileOutputGridShow()
        {
            try
            {
                _audioFileOutputGridPresenter.Show();
                DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputGridClose()
        {
            try
            {
                _audioFileOutputGridPresenter.Close();
                DispatchViewModel(_audioFileOutputGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputCreate()
        {
            try
            {
                // ToEntity
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                AudioFileOutput audioFileOutput = _audioFileOutputManager.CreateWithRelatedEntities(document, mustGenerateName: true);

                // ToViewModel
                AudioFileOutputListItemViewModel listItemViewModel = audioFileOutput.ToListItemViewModel();
                ViewModel.Document.AudioFileOutputGrid.List.Add(listItemViewModel);
                ViewModel.Document.AudioFileOutputGrid.List = ViewModel.Document.AudioFileOutputGrid.List.OrderBy(x => x.Name).ToList();

                AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel(_repositories.AudioFileFormatRepository, _repositories.SampleDataTypeRepository, _repositories.SpeakerSetupRepository);
                ViewModel.Document.AudioFileOutputPropertiesList.Add(propertiesViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputDelete(int id)
        {
            try
            {
                // 'Business' / ToViewModel
                ViewModel.Document.AudioFileOutputPropertiesList.RemoveFirst(x => x.Entity.ID == id);
                ViewModel.Document.AudioFileOutputGrid.List.RemoveFirst(x => x.ID == id);

                // No need to do ToEntity, 
                // because we are not executing any additional business logic or refreshing 
                // that uses the entity models.
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            try
            {
                _audioFileOutputPropertiesPresenter.ViewModel = ViewModel.Document.AudioFileOutputPropertiesList
                                                                                  .First(x => x.Entity.ID == id);
                _audioFileOutputPropertiesPresenter.Show();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void AudioFileOutputPropertiesClose()
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(() => _audioFileOutputPropertiesPresenter.Close());
        }

        public void AudioFileOutputPropertiesLoseFocus()
        {
            AudioFileOutputPropertiesCloseOrLoseFocus(() => _audioFileOutputPropertiesPresenter.LoseFocus());
        }

        private void AudioFileOutputPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                DispatchViewModel(_audioFileOutputPropertiesPresenter.ViewModel);

                if (_audioFileOutputPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshAudioFileOutputGrid();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Child Document Actions

        public void ChildDocumentPropertiesShow(int id)
        {
            try
            {
                _childDocumentPropertiesPresenter.ViewModel = ViewModel.Document.ChildDocumentPropertiesList.First(x => x.ID == id);
                _childDocumentPropertiesPresenter.Show();

                DispatchViewModel(_childDocumentPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ChildDocumentPropertiesClose()
        {
            ChildDocumentPropertiesCloseOrLoseFocus(() => _childDocumentPropertiesPresenter.Close());
        }

        public void ChildDocumentPropertiesLoseFocus()
        {
            ChildDocumentPropertiesCloseOrLoseFocus(() => _childDocumentPropertiesPresenter.LoseFocus());
        }

        private void ChildDocumentPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // ToEntity  (Most of the entity model is needed for Document_SideEffect_UpdateDependentCustomOperators.)
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                int childDocumentID = _childDocumentPropertiesPresenter.ViewModel.ID;

                partialAction();

                DispatchViewModel(_childDocumentPropertiesPresenter.ViewModel);

                if (_childDocumentPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshDocumentTree();
                    RefreshInstrumentGrid(); // Refresh both efect and instrument grids, because ChildDocumentType can be changed.
                    RefreshEffectGrid();
                    RefreshUnderylingDocumentLookup();
                    RefreshOperatorViewModels_OfTypeCustomOperators();
                    Refresh_OperatorProperties_ForCustomOperatorViewModels(underlyingDocumentID: childDocumentID);
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Curve Actions

        public void CurveGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    ViewModel.ToEntityWithRelatedEntities(_repositories);
                }

                CurveGridViewModel curveGridViewModel = ChildDocumentHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _curveGridPresenter.ViewModel = curveGridViewModel;
                _curveGridPresenter.Show();
                DispatchViewModel(_curveGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveGridClose()
        {
            try
            {
                _curveGridPresenter.Close();
                DispatchViewModel(_curveGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveCreate(int documentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                Document document = _repositories.DocumentRepository.TryGet(documentID);
                Curve curve = _curveManager.Create(document, mustGenerateName: true);

                // ToViewModel
                CurveGridViewModel curveGridViewModel = ChildDocumentHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                IDAndName listItemViewModel = curve.ToIDAndName();
                curveGridViewModel.List.Add(listItemViewModel);
                curveGridViewModel.List = curveGridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<CurveDetailsViewModel> curveDetailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, document.ID);
                CurveDetailsViewModel curveDetailsViewModel = curve.ToDetailsViewModel(_repositories.NodeTypeRepository);
                curveDetailsViewModels.Add(curveDetailsViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveDelete(int curveID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Curve curve = _repositories.CurveRepository.TryGet(curveID);
                if (curve == null)
                {
                    NotFoundViewModel notFoundViewModel = ViewModelHelper.CreateNotFoundViewModel<Curve>();
                    DispatchViewModel(notFoundViewModel);
                    return;
                }
                int documentID = curve.Document.ID;

                // Business
                VoidResult result = _curveManager.DeleteWithRelatedEntities(curve);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<CurveDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetCurveDetailsViewModels_ByDocumentID(ViewModel.Document, documentID);
                    detailsViewModels.RemoveFirst(x => x.Entity.ID == curveID);

                    CurveGridViewModel gridViewModel = ChildDocumentHelper.GetCurveGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == curveID);
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveDetailsShow(int curveID)
        {
            try
            {
                CurveDetailsViewModel detailsViewModel = ChildDocumentHelper.GetCurveDetailsViewModel(ViewModel.Document, curveID);
                _curveDetailsPresenter.ViewModel = detailsViewModel;
                _curveDetailsPresenter.Show();

                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveDetailsClose()
        {
            try
            {
                _curveDetailsPresenter.Close();

                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void CurveDetailsLoseFocus()
        {
            try
            {
                _curveDetailsPresenter.LoseFocus();

                DispatchViewModel(_curveDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Document List Actions

        public void DocumentGridShow(int pageNumber)
        {
            try
            {
                _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
                DocumentGridViewModel viewModel = _documentGridPresenter.Show(pageNumber);
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentGridClose()
        {
            try
            {
                _documentGridPresenter.ViewModel = ViewModel.DocumentGrid;
                _documentGridPresenter.Close();
                DispatchViewModel(_documentGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentDetailsCreate()
        {
            try
            {
                DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentDetailsClose()
        {
            try
            {
                _documentDetailsPresenter.Close();
                DispatchViewModel(_documentDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentDetailsSave()
        {
            try
            {
                _documentDetailsPresenter.Save();
                DispatchViewModel(_documentDetailsPresenter.ViewModel);

                RefreshDocumentGrid();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentDelete(int id)
        {
            try
            {
                object viewModel = _documentDeletePresenter.Show(id);
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentCannotDeleteOK()
        {
            try
            {
                _documentCannotDeletePresenter.OK();
                DispatchViewModel(_documentCannotDeletePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentConfirmDelete(int id)
        {
            try
            {
                object viewModel = _documentDeletePresenter.Confirm(id);

                if (viewModel is DocumentDeletedViewModel)
                {
                    _repositories.Commit();
                }

                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentCancelDelete()
        {
            try
            {
                _documentDeletePresenter.Cancel();
                DispatchViewModel(_documentDeletePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentDeletedOK()
        {
            try
            {
                DocumentDeletedViewModel viewModel = _documentDeletedPresenter.OK();
                DispatchViewModel(viewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Document Actions

        public void DocumentOpen(int documentID)
        {
            try
            {
                Document document = _repositories.DocumentRepository.Get(documentID);

                ViewModel.Document = document.ToViewModel(_repositories, _entityPositionManager);

                // Here only the view models are assigned that cannot vary.
                // E.g. PatchGrid can be show for both chile documents and root document.
                _audioFileOutputGridPresenter.ViewModel = ViewModel.Document.AudioFileOutputGrid;
                _effectGridPresenter.ViewModel = ViewModel.Document.EffectGrid;
                _instrumentGridPresenter.ViewModel = ViewModel.Document.InstrumentGrid;
                _documentTreePresenter.ViewModel = ViewModel.Document.DocumentTree;
                _scaleGridPresenter.ViewModel = ViewModel.Document.ScaleGrid;

                ViewModel.WindowTitle = String.Format("{0} - {1}", document.Name, Titles.ApplicationName);

                _menuPresenter.Show(documentIsOpen: true);
                ViewModel.Menu = _menuPresenter.ViewModel;

                ViewModel.DocumentGrid.Visible = false;
                ViewModel.Document.DocumentTree.Visible = true;

                ViewModel.Document.IsOpen = true;
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentSave()
        {
            if (ViewModel.Document == null) throw new NullException(() => ViewModel.Document);

            try
            {
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);

                IValidator validator = new DocumentValidator_Recursive(document, _repositories, alreadyDone: new HashSet<object>());
                IValidator warningsValidator = new DocumentWarningValidator_Recursive(document, _repositories.SampleRepository, new HashSet<object>());

                if (!validator.IsValid)
                {
                    _repositories.Rollback();
                }
                else
                {
                    _repositories.Commit();
                }

                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                ViewModel.WarningMessages = warningsValidator.ValidationMessages.ToCanonical();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentClose()
        {
            try
            {
                if (ViewModel.Document.IsOpen)
                {
                    ViewModel.Document = ViewModelHelper.CreateEmptyDocumentViewModel();
                    ViewModel.WindowTitle = Titles.ApplicationName;

                    _menuPresenter.Show(documentIsOpen: false);

                    ViewModel.Menu = _menuPresenter.ViewModel;
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentPropertiesShow()
        {
            try
            {
                _documentPropertiesPresenter.ViewModel = ViewModel.Document.DocumentProperties;
                _documentPropertiesPresenter.Show();
                DispatchViewModel(_documentPropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentPropertiesClose()
        {
            DocumentPropertiesCloseOrLoseFocus(() => _documentPropertiesPresenter.Close());
        }

        public void DocumentPropertiesLoseFocus()
        {
            DocumentPropertiesCloseOrLoseFocus(() => _documentPropertiesPresenter.LoseFocus());
        }

        private void DocumentPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // ToEntity: This should be just enought to correctly refresh the grids and tree furtheron.
                Document document = ViewModel.Document.ToEntity(_repositories.DocumentRepository);
                ViewModel.Document.ChildDocumentList.SelectMany(x => x.PatchDetailsList)
                                                    .Select(x => x.Entity)
                                                    .ForEach(x => x.ToEntity(_repositories.PatchRepository));
                ToEntityHelper.ToChildDocuments(ViewModel.Document.ChildDocumentPropertiesList, document, _repositories);


                _documentPropertiesPresenter.ViewModel = ViewModel.Document.DocumentProperties;

                partialAction();

                DispatchViewModel(_documentPropertiesPresenter.ViewModel);

                if (_documentPropertiesPresenter.ViewModel.Successful)
                {
                    RefreshDocumentGrid();
                    RefreshDocumentTree();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeShow()
        {
            try
            {
                _documentTreePresenter.Show();
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeExpandNode(int nodeIndex)
        {
            try
            {
                _documentTreePresenter.ExpandNode(nodeIndex);
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeCollapseNode(int nodeIndex)
        {
            try
            {
                _documentTreePresenter.CollapseNode(nodeIndex);
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void DocumentTreeClose()
        {
            try
            {
                _documentTreePresenter.Close();
                DispatchViewModel(_documentTreePresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Effect Actions

        public void EffectGridShow()
        {
            try
            {
                _effectGridPresenter.Show();
                DispatchViewModel(_effectGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void EffectGridClose()
        {
            try
            {
                _effectGridPresenter.Close();
                DispatchViewModel(_effectGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void EffectCreate()
        {
            try
            {
                // ToEntity
                Document parentDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                Document effect = _documentManager.CreateChildDocument(parentDocument, ChildDocumentTypeEnum.Effect, mustGenerateName: true);

                // ToViewModel
                IDAndName listItemViewModel = effect.ToIDAndName();
                ViewModel.Document.EffectGrid.List.Add(listItemViewModel);
                ViewModel.Document.EffectGrid.List = ViewModel.Document.EffectGrid.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = effect.ToChildDocumentPropertiesViewModel(_repositories.ChildDocumentTypeRepository);
                ViewModel.Document.ChildDocumentPropertiesList.Add(propertiesViewModel);

                ChildDocumentViewModel documentViewModel = effect.ToChildDocumentViewModel(_repositories, _entityPositionManager);
                ViewModel.Document.ChildDocumentList.Add(documentViewModel);

                IDAndName idAndName = effect.ToIDAndName();
                ViewModel.Document.UnderlyingDocumentLookup.Add(idAndName);
                ViewModel.Document.UnderlyingDocumentLookup = ViewModel.Document.UnderlyingDocumentLookup.OrderBy(x => x.Name).ToList();

                RefreshDocumentTree();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void EffectDelete(int effectDocumentID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Document childDocument = _repositories.DocumentRepository.Get(effectDocumentID);

                // Businesss
                VoidResult result = _documentManager.DeleteWithRelatedEntities(childDocument);
                if (!result.Successful)
                {
                    // ToViewModel
                    ViewModel.PopupMessages.AddRange(result.Messages);
                }
                else
                {
                    // ToViewModel
                    ViewModel.Document.EffectGrid.List.RemoveFirst(x => x.ID == effectDocumentID);
                    ViewModel.Document.ChildDocumentPropertiesList.RemoveFirst(x => x.ID == effectDocumentID);
                    ViewModel.Document.ChildDocumentList.RemoveFirst(x => x.ID == effectDocumentID);
                    ViewModel.Document.DocumentTree.Effects.RemoveFirst(x => x.ChildDocumentID == effectDocumentID);
                    ViewModel.Document.UnderlyingDocumentLookup.RemoveFirst(x => x.ID == effectDocumentID);
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Instrument Actions

        public void InstrumentGridShow()
        {
            try
            {
                _instrumentGridPresenter.Show();
                DispatchViewModel(_instrumentGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void InstrumentGridClose()
        {
            try
            {
                _instrumentGridPresenter.Close();
                DispatchViewModel(_instrumentGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void InstrumentCreate()
        {
            try
            {
                // ToEntity
                Document parentDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                Document childDocument = _documentManager.CreateChildDocument(parentDocument, ChildDocumentTypeEnum.Instrument, mustGenerateName: true);

                // ToViewModel
                IDAndName listItemViewModel = childDocument.ToIDAndName();
                ViewModel.Document.InstrumentGrid.List.Add(listItemViewModel);
                ViewModel.Document.InstrumentGrid.List = ViewModel.Document.InstrumentGrid.List.OrderBy(x => x.Name).ToList();

                ChildDocumentPropertiesViewModel propertiesViewModel = childDocument.ToChildDocumentPropertiesViewModel(_repositories.ChildDocumentTypeRepository);
                ViewModel.Document.ChildDocumentPropertiesList.Add(propertiesViewModel);

                ChildDocumentViewModel documentViewModel = childDocument.ToChildDocumentViewModel(_repositories, _entityPositionManager);
                ViewModel.Document.ChildDocumentList.Add(documentViewModel);

                IDAndName idAndName = childDocument.ToIDAndName();
                ViewModel.Document.UnderlyingDocumentLookup.Add(idAndName);
                ViewModel.Document.UnderlyingDocumentLookup = ViewModel.Document.UnderlyingDocumentLookup.OrderBy(x => x.Name).ToList();

                RefreshDocumentTree();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void InstrumentDelete(int instrumentDocumentID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Document childDocument = _repositories.DocumentRepository.Get(instrumentDocumentID);

                // Businesss
                VoidResult result = _documentManager.DeleteWithRelatedEntities(childDocument);
                if (!result.Successful)
                {
                    // ToViewModel
                    ViewModel.PopupMessages.AddRange(result.Messages);
                }
                else
                {
                    // ToViewModel
                    ViewModel.Document.InstrumentGrid.List.RemoveFirst(x => x.ID == instrumentDocumentID);
                    ViewModel.Document.ChildDocumentPropertiesList.RemoveFirst(x => x.ID == instrumentDocumentID);
                    ViewModel.Document.ChildDocumentList.RemoveFirst(x => x.ID == instrumentDocumentID);
                    ViewModel.Document.DocumentTree.Instruments.RemoveFirst(x => x.ChildDocumentID == instrumentDocumentID);
                    ViewModel.Document.UnderlyingDocumentLookup.RemoveFirst(x => x.ID == instrumentDocumentID);
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Operator Actions

        public void OperatorPropertiesShow(int id)
        {
            try
            {
                {
                    OperatorPropertiesViewModel viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter partialPresenter = _operatorPropertiesPresenter;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForCustomOperator viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForCustomOperator(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForCustomOperator partialPresenter = _operatorPropertiesPresenter_ForCustomOperator;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForPatchInlet viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForPatchInlet(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForPatchInlet partialPresenter = _operatorPropertiesPresenter_ForPatchInlet;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForPatchOutlet viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForPatchOutlet(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForPatchOutlet partialPresenter = _operatorPropertiesPresenter_ForPatchOutlet;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForSample viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForSample(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForSample partialPresenter = _operatorPropertiesPresenter_ForSample;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }
                {
                    OperatorPropertiesViewModel_ForNumber viewModel = ChildDocumentHelper.TryGetOperatorPropertiesViewModel_ForNumber(ViewModel.Document, id);
                    if (viewModel != null)
                    {
                        OperatorPropertiesPresenter_ForNumber partialPresenter = _operatorPropertiesPresenter_ForNumber;
                        partialPresenter.ViewModel = viewModel;
                        partialPresenter.Show();
                        DispatchViewModel(partialPresenter.ViewModel);
                        return;
                    }
                }

                throw new Exception(String.Format("Properties ViewModel not found for Operator with ID '{0}'.", id));
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorPropertiesClose()
        {
            OperatorPropertiesCloseOrLoseFocus(() => _operatorPropertiesPresenter.Close());
        }

        public void OperatorPropertiesClose_ForCustomOperator()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(() => _operatorPropertiesPresenter_ForCustomOperator.Close());
        }

        public void OperatorPropertiesClose_ForPatchInlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(() => _operatorPropertiesPresenter_ForPatchInlet.Close());
        }

        public void OperatorPropertiesClose_ForPatchOutlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(() => _operatorPropertiesPresenter_ForPatchOutlet.Close());
        }

        public void OperatorPropertiesClose_ForSample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(() => _operatorPropertiesPresenter_ForSample.Close());
        }

        public void OperatorPropertiesClose_ForNumber()
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(() => _operatorPropertiesPresenter_ForNumber.Close());
        }

        public void OperatorPropertiesLoseFocus()
        {
            OperatorPropertiesCloseOrLoseFocus(() => _operatorPropertiesPresenter.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForCustomOperator()
        {
            OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(() => _operatorPropertiesPresenter_ForCustomOperator.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForPatchInlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(() => _operatorPropertiesPresenter_ForPatchInlet.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForPatchOutlet()
        {
            OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(() => _operatorPropertiesPresenter_ForPatchOutlet.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForSample()
        {
            OperatorPropertiesCloseOrLoseFocus_ForSample(() => _operatorPropertiesPresenter_ForSample.LoseFocus());
        }

        public void OperatorPropertiesLoseFocus_ForNumber()
        {
            OperatorPropertiesCloseOrLoseFocus_ForNumber(() => _operatorPropertiesPresenter_ForNumber.LoseFocus());
        }

        private void OperatorPropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter partialPresenter = _operatorPropertiesPresenter;
                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    RefreshPatchDetailsOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForSample(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForSample partialPresenter = _operatorPropertiesPresenter_ForSample;

                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                // Convert the document, child documents + samples
                // because we are about to validate a sample operator's reference to its sample.
                ViewModel.Document.ToHollowDocumentWithHollowChildDocumentsWithHollowSamplesWithName(
                    _repositories.DocumentRepository,
                    _repositories.ChildDocumentTypeRepository,
                    _repositories.SampleRepository);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    RefreshPatchDetailsOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForNumber(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForNumber partialPresenter = _operatorPropertiesPresenter_ForNumber;

                OperatorEntityAndViewModel operatorEntityAndViewModel = ToEntityHelper.ToOperatorWithInletsAndOutletsAndPatch(ViewModel.Document, partialPresenter.ViewModel.ID, _patchRepositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    RefreshPatchDetailsOperator(operatorEntityAndViewModel.Operator, operatorEntityAndViewModel.OperatorViewModel);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForCustomOperator(Action partialAction)
        {
            try
            {
                // Convert whole document, because we need:
                // - OperatorViewModel (from PatchDetail), because we are about to validate
                //   the inlets and outlets too, which are not defined in the OperatorPropertiesViewModel.
                // - The child documents + main patches
                //   because we are about to validate a custom operator's reference to an underlying document.
                // - The chosen Document's MainPatch's PatchInlets and PatchOutlets,
                //   because we are about to convert those to the custom operator.
                // We could convert only those things, to be a little bit faster,
                // but perhaps now it is better to choose being agnostic over being efficient.
                // (If you ever decie to go the other way, other OperatorProperties actions have code that almost converts all of that already,
                // except for the last bullet point.)
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                OperatorPropertiesPresenter_ForCustomOperator partialPresenter = _operatorPropertiesPresenter_ForCustomOperator;
                if (partialPresenter.ViewModel.Successful)
                {
                    RefreshPatchDetailsOperator(partialPresenter.ViewModel.ID);
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchInlet(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchInlet partialPresenter = _operatorPropertiesPresenter_ForPatchInlet;

                // ToEntity  (Most of the entity model is needed for Document_SideEffect_UpdateDependentCustomOperators.)
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    RefreshPatchDetailsOperator(partialPresenter.ViewModel.ID);

                    // Refresh Dependencies
                    RefreshOperatorViewModels_OfTypeCustomOperators();
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorPropertiesCloseOrLoseFocus_ForPatchOutlet(Action partialAction)
        {
            try
            {
                OperatorPropertiesPresenter_ForPatchOutlet partialPresenter = _operatorPropertiesPresenter_ForPatchOutlet;

                // ToEntity  (Most of the entity model is needed for Document_SideEffect_UpdateDependentCustomOperators.)
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                if (partialPresenter.ViewModel.Successful)
                {
                    RefreshPatchDetailsOperator(partialPresenter.ViewModel.ID);

                    // Refresh Dependent Things
                    RefreshOperatorViewModels_OfTypeCustomOperators();
                }

                DispatchViewModel(partialPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorCreate(int operatorTypeID)
        {
            bool isPatchInletOrPatchOutlet = operatorTypeID == (int)OperatorTypeEnum.PatchInlet ||
                                             operatorTypeID == (int)OperatorTypeEnum.PatchOutlet;
            if (isPatchInletOrPatchOutlet)
            {
                OperatorCreate_ForPatchInletOrPatchOutlet(operatorTypeID);
            }
            else
            {
                OperatorCreate_ForOtherOperatorTypes(operatorTypeID);
            }
        }

        // TODO: Now that executing the side-effects is moved to the PatchManager, do I still need this specialized method?
        private void OperatorCreate_ForPatchInletOrPatchOutlet(int operatorTypeID)
        {
            try
            {
                // ToEntity  (Full entity model needed for Document_SideEffect_UpdateDependentCustomOperators.)
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.ID);

                // Business
                var patchManager = new PatchManager(patch, _patchRepositories);
                Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

                // ToViewModel

                // OperatorViewModel
                OperatorViewModel operatorViewModel = op.ToViewModelWithRelatedEntitiesAndInverseProperties(
                    _repositories.SampleRepository,
                    _repositories.CurveRepository,
                    _repositories.DocumentRepository,
                    _entityPositionManager);
                _patchDetailsPresenter.ViewModel.Entity.Operators.Add(operatorViewModel);

                // OperatorPropertiesViewModel
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
                switch (operatorTypeEnum)
                {
                    case OperatorTypeEnum.PatchInlet:
                        {
                            OperatorPropertiesViewModel_ForPatchInlet propertiesViewModel = op.ToPropertiesViewModel_ForPatchInlet();
                            IList<OperatorPropertiesViewModel_ForPatchInlet> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForPatchInlets_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.PatchOutlet:
                        {
                            OperatorPropertiesViewModel_ForPatchOutlet propertiesViewModel = op.ToPropertiesViewModel_ForPatchOutlet();
                            IList<OperatorPropertiesViewModel_ForPatchOutlet> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForPatchOutlets_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    default:
                        throw new ValueNotSupportedException(operatorTypeEnum);
                }

                // Refresh Dependent Things
                RefreshOperatorViewModels_OfTypeCustomOperators();
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        private void OperatorCreate_ForOtherOperatorTypes(int operatorTypeID)
        {
            try
            {
                // ToEntity
                Patch patch = _patchDetailsPresenter.ViewModel.ToEntityWithRelatedEntities(_patchRepositories);

                // Business
                var patchManager = new PatchManager(patch, new PatchRepositories(_repositories));
                Operator op = patchManager.CreateOperator((OperatorTypeEnum)operatorTypeID);

                // ToViewModel

                // PatchDetails OperatorViewModel
                OperatorViewModel operatorViewModel = op.ToViewModelWithRelatedEntitiesAndInverseProperties(
                    _repositories.SampleRepository,
                    _repositories.CurveRepository,
                    _repositories.DocumentRepository,
                    _entityPositionManager);
                _patchDetailsPresenter.ViewModel.Entity.Operators.Add(operatorViewModel);

                // Operator Properties
                OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();
                switch (operatorTypeEnum)
                {
                    case OperatorTypeEnum.CustomOperator:
                        {
                            OperatorPropertiesViewModel_ForCustomOperator propertiesViewModel = op.ToPropertiesViewModel_ForCustomOperator(_repositories.DocumentRepository);
                            IList<OperatorPropertiesViewModel_ForCustomOperator> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForCustomOperators_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.Sample:
                        {
                            OperatorPropertiesViewModel_ForSample propertiesViewModel = op.ToOperatorPropertiesViewModel_ForSample(_repositories.SampleRepository);
                            IList<OperatorPropertiesViewModel_ForSample> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForSamples_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    case OperatorTypeEnum.Number:
                        {
                            OperatorPropertiesViewModel_ForNumber propertiesViewModel = op.ToPropertiesViewModel_ForNumber();
                            IList<OperatorPropertiesViewModel_ForNumber> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ForNumbers_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }

                    default:
                        {
                            OperatorPropertiesViewModel propertiesViewModel = op.ToPropertiesViewModel();
                            IList<OperatorPropertiesViewModel> propertiesViewModelList = ChildDocumentHelper.GetOperatorPropertiesViewModelList_ByPatchID(ViewModel.Document, patch.ID);
                            propertiesViewModelList.Add(propertiesViewModel);
                            break;
                        }
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        /// <summary> Deletes the operator selected in PatchDetails. Does not delete anything if no operator is selected. </summary>
        public void OperatorDelete()
        {
            try
            {
                if (_patchDetailsPresenter.ViewModel.SelectedOperator != null)
                {
                    // ToEntity
                    // (Full entity model needed for Document_SideEffect_UpdateDependentCustomOperators and 
                    //  Operator_SideEffect_ApplyUnderlyingDocument)
                    Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                    Patch patch = _repositories.PatchRepository.Get(_patchDetailsPresenter.ViewModel.Entity.ID);
                    Document document = patch.Document;
                    Operator op = _repositories.OperatorRepository.Get(_patchDetailsPresenter.ViewModel.SelectedOperator.ID);
                    OperatorTypeEnum operatorTypeEnum = op.GetOperatorTypeEnum();

                    // Business
                    var patchManager = new PatchManager(patch, _patchRepositories);
                    patchManager.DeleteOperator(op);

                    // Partial Action
                    _patchDetailsPresenter.DeleteOperator();

                    // ToViewModel
                    if (_patchDetailsPresenter.ViewModel.Successful)
                    {
                        // Do a lot of if'ing and switching to be a little faster in removing the item a specific place in the view model,
                        bool isRootDocument = rootDocument.ID == document.ID;
                        if (isRootDocument)
                        {
                            switch (operatorTypeEnum)
                            {
                                case OperatorTypeEnum.CustomOperator:
                                    ViewModel.Document.OperatorPropertiesList_ForCustomOperators.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.PatchInlet:
                                    ViewModel.Document.OperatorPropertiesList_ForPatchInlets.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.PatchOutlet:
                                    ViewModel.Document.OperatorPropertiesList_ForPatchOutlets.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.Sample:
                                    ViewModel.Document.OperatorPropertiesList_ForSamples.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.Number:
                                    ViewModel.Document.OperatorPropertiesList_ForNumbers.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.Undefined:
                                    throw new ValueNotSupportedException(operatorTypeEnum);

                                default:
                                    ViewModel.Document.OperatorPropertiesList.RemoveFirst(x => x.ID == op.ID);
                                    break;
                            }
                        }
                        else
                        {
                            ChildDocumentViewModel childDocumentViewModel = ViewModel.Document.ChildDocumentList.Where(x => x.ID == document.ID).First();
                            switch (operatorTypeEnum)
                            {
                                case OperatorTypeEnum.CustomOperator:
                                    childDocumentViewModel.OperatorPropertiesList_ForCustomOperators.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.PatchInlet:
                                    childDocumentViewModel.OperatorPropertiesList_ForPatchInlets.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.PatchOutlet:
                                    childDocumentViewModel.OperatorPropertiesList_ForPatchOutlets.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.Sample:
                                    childDocumentViewModel.OperatorPropertiesList_ForSamples.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.Number:
                                    childDocumentViewModel.OperatorPropertiesList_ForNumbers.RemoveFirst(x => x.ID == op.ID);
                                    break;

                                case OperatorTypeEnum.Undefined:
                                    throw new ValueNotSupportedException(operatorTypeEnum);

                                default:
                                    childDocumentViewModel.OperatorPropertiesList.RemoveFirst(x => x.ID == op.ID);
                                    break;
                            }
                        }
                    }

                    // Refresh Dependent Things
                    RefreshOperatorViewModels_OfTypeCustomOperators();
                }

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorMove(int operatorID, float centerX, float centerY)
        {
            try
            {
                _patchDetailsPresenter.MoveOperator(operatorID, centerX, centerY);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorChangeInputOutlet(int inletID, int inputOutletID)
        {
            try
            {
                _patchDetailsPresenter.ChangeInputOutlet(inletID, inputOutletID);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void OperatorSelect(int operatorID)
        {
            try
            {
                _patchDetailsPresenter.SelectOperator(operatorID);
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Patch Actions

        public void PatchGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (!isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    ViewModel.ToEntityWithRelatedEntities(_repositories);
                }

                PatchGridViewModel patchGridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _patchGridPresenter.ViewModel = patchGridViewModel;
                _patchGridPresenter.Show();
                DispatchViewModel(_patchGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchGridClose()
        {
            try
            {
                _patchGridPresenter.Close();
                DispatchViewModel(_patchGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchCreate(int documentID)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                Document document = _repositories.DocumentRepository.TryGet(documentID);

                // Business
                var patchManager = new PatchManager(_patchRepositories);
                Patch patch = patchManager.Create(document, mustGenerateName: true);

                // ToViewModel
                PatchGridViewModel gridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                IDAndName listItemViewModel = patch.ToIDAndName();
                gridViewModel.List.Add(listItemViewModel);
                gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(ViewModel.Document, document.ID);
                PatchDetailsViewModel detailsViewModel = patch.ToDetailsViewModel(
                    _repositories.OperatorTypeRepository,
                    _repositories.SampleRepository,
                    _repositories.CurveRepository,
                    _repositories.DocumentRepository,
                    _entityPositionManager);
                detailsViewModels.Add(detailsViewModel);

                ChildDocumentPropertiesViewModel childDocumentPropertiesViewModel = ChildDocumentHelper.TryGetChildDocumentPropertiesViewModel(ViewModel.Document, document.ID);
                if (childDocumentPropertiesViewModel != null)
                {
                    IDAndName idAndName = ToIDAndNameExtensions.ToIDAndName(patch);
                    childDocumentPropertiesViewModel.MainPatchLookup.Add(idAndName);
                    childDocumentPropertiesViewModel.MainPatchLookup = childDocumentPropertiesViewModel.MainPatchLookup.OrderBy(x => x.Name).ToList();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchDelete(int patchID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Patch patch = _repositories.PatchRepository.TryGet(patchID);
                if (patch == null)
                {
                    NotFoundViewModel notFoundViewModel = ViewModelHelper.CreateNotFoundViewModel<Patch>();
                    DispatchViewModel(notFoundViewModel);
                    return;
                }
                int documentID = patch.Document.ID;

                // Business
                var patchManager = new PatchManager(patch, new PatchRepositories(_repositories));

                VoidResult result = patchManager.DeleteWithRelatedEntities();
                if (result.Successful)
                {
                    // ToViewModel
                    IList<PatchDetailsViewModel> detailsViewModels = ChildDocumentHelper.GetPatchDetailsViewModels_ByDocumentID(ViewModel.Document, documentID);
                    detailsViewModels.RemoveFirst(x => x.Entity.ID == patchID);

                    PatchGridViewModel gridViewModel = ChildDocumentHelper.GetPatchGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == patchID);

                    ChildDocumentPropertiesViewModel childDocumentPropertiesViewModel = ChildDocumentHelper.TryGetChildDocumentPropertiesViewModel(ViewModel.Document, documentID);
                    if (childDocumentPropertiesViewModel != null)
                    {
                        childDocumentPropertiesViewModel.MainPatchLookup.RemoveFirst(x => x.ID == patchID);
                    }
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchDetailsShow(int patchID)
        {
            try
            {
                PatchDetailsViewModel detailsViewModel = ChildDocumentHelper.GetPatchDetailsViewModel(ViewModel.Document, patchID);
                _patchDetailsPresenter.ViewModel = detailsViewModel;
                _patchDetailsPresenter.Show();
                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void PatchDetailsClose()
        {
            PatchDetailsCloseOrLoseFocus(() => _patchDetailsPresenter.Close());
        }

        public void PatchDetailsLoseFocus()
        {
            PatchDetailsCloseOrLoseFocus(() => _patchDetailsPresenter.LoseFocus());
        }

        private void PatchDetailsCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // ToEntity
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);
                int patchID = _patchDetailsPresenter.ViewModel.Entity.ID;
                Patch patch = _repositories.PatchRepository.Get(patchID);
                int documentID = patch.Document.ID;

                // Partial Action
                partialAction();

                if (_patchDetailsPresenter.ViewModel.Successful)
                {
                    RefreshPatchGrid(documentID);
                }

                DispatchViewModel(_patchDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        /// <summary> Returns output file path if ViewModel.Successful. summary>
        public string PatchPlay()
        {
            try
            {
                Document rootDocument = ViewModel.ToEntityWithRelatedEntities(_repositories);

                HACK_CreateCurves(rootDocument);

                string outputFilePath = _patchDetailsPresenter.Play(_repositories);

                DispatchViewModel(_patchDetailsPresenter.ViewModel);

                // Move messages to popup messages, because the default dispatching for PatchDetailsViewModel moves it to the ValidationMessages.
                ViewModel.PopupMessages.AddRange(_patchDetailsPresenter.ViewModel.ValidationMessages);
                _patchDetailsPresenter.ViewModel.ValidationMessages.Clear();

                ViewModel.Successful = _patchDetailsPresenter.ViewModel.Successful;

                DispatchViewModel(_patchDetailsPresenter.ViewModel);

                return outputFilePath;
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Sample Actions

        public void SampleGridShow(int documentID)
        {
            try
            {
                bool isRootDocument = documentID == ViewModel.Document.ID;
                if (!isRootDocument)
                {
                    // Needed to create uncommitted child documents.
                    ViewModel.ToEntityWithRelatedEntities(_repositories);
                }

                SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                _sampleGridPresenter.ViewModel = gridViewModel;
                _sampleGridPresenter.Show();
                DispatchViewModel(_sampleGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SampleGridClose()
        {
            try
            {
                _sampleGridPresenter.Close();
                DispatchViewModel(_sampleGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SampleCreate(int documentID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Document document = _repositories.DocumentRepository.Get(documentID);

                // Business
                Sample sample = _sampleManager.CreateSample(document, mustGenerateName: true);

                // ToViewModel
                SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, document.ID);
                SampleListItemViewModel listItemViewModel = sample.ToListItemViewModel();
                gridViewModel.List.Add(listItemViewModel);
                gridViewModel.List = gridViewModel.List.OrderBy(x => x.Name).ToList();

                IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, document.ID);
                SamplePropertiesViewModel propertiesViewModel = sample.ToPropertiesViewModel(_sampleRepositories);
                propertiesViewModels.Add(propertiesViewModel);

                // NOTE: Samples in a child document are only added to the sample lookup of that child document,
                // while sample in the root document are added to both root and child documents.
                bool isRootDocument = document.ParentDocument == null;
                if (isRootDocument)
                {
                    IDAndName idAndName = sample.ToIDAndName();
                    ViewModel.Document.SampleLookup.Add(idAndName);
                    ViewModel.Document.ChildDocumentList.ForEach(x => x.SampleLookup.Add(idAndName));
                }
                else
                {
                    IDAndName idAndName = sample.ToIDAndName();
                    IList<IDAndName> lookup = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, documentID).SampleLookup;
                    lookup.Add(idAndName);
                    // TODO: Forgot to sort here !!!
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SampleDelete(int sampleID)
        {
            try
            {
                // ToEntity
                ViewModel.ToEntityWithRelatedEntities(_repositories);
                Sample sample = _repositories.SampleRepository.Get(sampleID);
                int documentID = sample.Document.ID;
                bool isRootDocument = sample.Document.ParentDocument == null;

                // Business
                VoidResult result = _sampleManager.Delete(sample);
                if (result.Successful)
                {
                    // ToViewModel
                    IList<SamplePropertiesViewModel> propertiesViewModels = ChildDocumentHelper.GetSamplePropertiesViewModels_ByDocumentID(ViewModel.Document, documentID);
                    propertiesViewModels.RemoveFirst(x => x.Entity.ID == sampleID);

                    SampleGridViewModel gridViewModel = ChildDocumentHelper.GetSampleGridViewModel_ByDocumentID(ViewModel.Document, documentID);
                    gridViewModel.List.RemoveFirst(x => x.ID == sampleID);

                    // NOTE: 
                    // If it is a sample in the root document, it is present in both root document and child document's sample lookups.
                    // If it is a sample in a child document, it will only be present in the child document's sample lookup and we have to do less work.
                    if (isRootDocument)
                    {
                        ViewModel.Document.SampleLookup.RemoveFirst(x => x.ID == sampleID);
                        ViewModel.Document.ChildDocumentList.ForEach(x => x.SampleLookup.RemoveFirst(y => y.ID == sampleID));
                    }
                    else
                    {
                        IList<IDAndName> lookup = ChildDocumentHelper.GetChildDocumentViewModel(ViewModel.Document, documentID).SampleLookup;
                        lookup.RemoveFirst(x => x.ID == sampleID);
                    }
                }
                else
                {
                    // ToViewModel
                    ViewModel.PopupMessages = result.Messages;
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SamplePropertiesShow(int sampleID)
        {
            try
            {
                SamplePropertiesViewModel propertiesViewModel = ChildDocumentHelper.GetSamplePropertiesViewModel(ViewModel.Document, sampleID);
                _samplePropertiesPresenter.ViewModel = propertiesViewModel;
                _samplePropertiesPresenter.Show();

                DispatchViewModel(_samplePropertiesPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void SamplePropertiesClose()
        {
            SamplePropertiesCloseOrLoseFocus(() => _samplePropertiesPresenter.Close());
        }

        public void SamplePropertiesLoseFocus()
        {
            SamplePropertiesCloseOrLoseFocus(() => _samplePropertiesPresenter.LoseFocus());
        }

        private void SamplePropertiesCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                partialAction();

                DispatchViewModel(_samplePropertiesPresenter.ViewModel);

                if (_samplePropertiesPresenter.ViewModel.Successful)
                {
                    int sampleID = _samplePropertiesPresenter.ViewModel.Entity.ID;

                    RefreshSampleGridItem(sampleID);
                    RefreshSampleLookupsItems(sampleID);
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        // Scale Actions

        public void ScaleGridShow()
        {
            try
            {
                _scaleGridPresenter.Show();
                DispatchViewModel(_scaleGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleGridClose()
        {
            try
            {
                _scaleGridPresenter.Close();
                DispatchViewModel(_scaleGridPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleCreate()
        {
            try
            {
                // ToEntity
                Document document = ViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                Scale scale = _scaleManager.Create(document, mustGenerateName: true);

                // ToViewModel
                IDAndName listItemViewModel = scale.ToIDAndName();
                ViewModel.Document.ScaleGrid.List.Add(listItemViewModel);
                ViewModel.Document.ScaleGrid.List = ViewModel.Document.ScaleGrid.List.OrderBy(x => x.Name).ToList();

                ScaleDetailsViewModel detailsViewModel = scale.ToDetailsViewModel(_repositories.ScaleTypeRepository);
                ViewModel.Document.ScaleDetailsList.Add(detailsViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleDelete(int id)
        {
            try
            {
                // TODO: It is not very clean to assume business logic will also not in the future have any delete constraints.

                // 'Business' / ToViewModel
                ViewModel.Document.ScaleDetailsList.RemoveFirst(x => x.Entity.ID == id);
                ViewModel.Document.ScaleGrid.List.RemoveFirst(x => x.ID == id);

                // No need to do ToEntity, 
                // because we are not executing any additional business logic or refreshing 
                // that uses the entity models.
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleDetailsShow(int id)
        {
            try
            {
                _scaleDetailsPresenter.ViewModel = ViewModel.Document.ScaleDetailsList.First(x => x.Entity.ID == id);

                _scaleDetailsPresenter.Show();

                DispatchViewModel(_scaleDetailsPresenter.ViewModel);
            }
            finally
            {
                _repositories.Rollback();
            }
        }

        public void ScaleDetailsClose()
        {
            ScaleDetailsCloseOrLoseFocus(() => _scaleDetailsPresenter.Close());
        }

        public void ScaleDetailsLoseFocus()
        {
            ScaleDetailsCloseOrLoseFocus(() => _scaleDetailsPresenter.LoseFocus());
        }

        private void ScaleDetailsCloseOrLoseFocus(Action partialAction)
        {
            try
            {
                // TODO: Can I get away with converting only part of the user input to entities?
                // Do consider that channels reference patch outlets.
                ViewModel.ToEntityWithRelatedEntities(_repositories);

                partialAction();

                DispatchViewModel(_scaleDetailsPresenter.ViewModel);

                if (_scaleDetailsPresenter.ViewModel.Successful)
                {
                    RefreshScaleGrid();
                }
            }
            finally
            {
                _repositories.Rollback();
            }
        }
    }
}