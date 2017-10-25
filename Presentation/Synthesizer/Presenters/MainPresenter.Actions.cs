using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable InvertIf
// ReSharper disable RedundantCaseLabel

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        // General Actions

        /// <param name="documentName">nullable</param>
        /// <param name="patchName">nullable</param>
        public void Show(string documentName, string patchName)
        {
            // Redirect
            if (string.IsNullOrEmpty(documentName) && string.IsNullOrEmpty(patchName))
            {
                ShowWithoutDocumentNameOrPatchName();
            }
            else if (!string.IsNullOrEmpty(documentName) && string.IsNullOrEmpty(patchName))
            {
                ShowWithDocumentName(documentName);
            }
            else if (string.IsNullOrEmpty(documentName) && !string.IsNullOrEmpty(patchName))
            {
                throw new Exception($"if {nameof(documentName)} is empty, {nameof(patchName)} cannot be filled in.");
            }
            else if (!string.IsNullOrEmpty(documentName) && !string.IsNullOrEmpty(patchName))
            {
                ShowWithDocumentNameAndPatchName(documentName, patchName);
            }
        }

        private void ShowWithoutDocumentNameOrPatchName()
        {
            // Create ViewModel
            MainViewModel = ToViewModelHelper.CreateEmptyMainViewModel();

            // Partial Actions
            MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
            DocumentGridViewModel documentGridViewModel = MainViewModel.DocumentGrid;
            documentGridViewModel = _documentGridPresenter.Load(documentGridViewModel);
            string titleBar = _titleBarPresenter.Show();

            // DispatchViewModel
            MainViewModel.TitleBar = titleBar;
            DispatchViewModel(menuViewModel);
            DispatchViewModel(documentGridViewModel);
        }

        private void ShowWithDocumentName(string documentName)
        {
            // Create ViewModel
            MainViewModel = ToViewModelHelper.CreateEmptyMainViewModel();

            // Businesss
            Document document = _repositories.DocumentRepository.TryGetByName(documentName);
            if (document == null)
            {
                // GetUserInput
                DocumentOrPatchNotFoundPopupViewModel userInput = MainViewModel.DocumentOrPatchNotFound;

                // Template Method
                ExecuteReadAction(null, () => _documentOrPatchNotFoundPresenter.Show(userInput, documentName));
            }
            else
            {
                // Redirect
                DocumentOpen(document);
            }
        }

        private void ShowWithDocumentNameAndPatchName(string documentName, string patchName)
        {
            // Create ViewModel
            MainViewModel = ToViewModelHelper.CreateEmptyMainViewModel();

            // Businesss
            Document document = _repositories.DocumentRepository.TryGetByName(documentName);
            string canonicalPatchName = NameHelper.ToCanonical(patchName);
            Patch patch = document?.Patches
                                   .Where(x => string.Equals(NameHelper.ToCanonical(x.Name), canonicalPatchName))
                                   .SingleWithClearException(new { canonicalPatchName });

            if (document == null || patch == null)
            {
                // GetUserInput
                DocumentOrPatchNotFoundPopupViewModel userInput = MainViewModel.DocumentOrPatchNotFound;

                // Template Method
                ExecuteReadAction(userInput, () => _documentOrPatchNotFoundPresenter.Show(userInput, documentName, patchName));
            }
            else
            {
                // Redirect
                DocumentOpen(document);
                PatchDetailsShow(patch.ID);
            }
        }

        public void PopupMessagesOK() => MainViewModel.PopupMessages = new List<string>();

        // AudioFileOutput

        public void AudioFileOutputGridShow()
        {
            // GetViewModel
            AudioFileOutputGridViewModel viewModel = MainViewModel.Document.AudioFileOutputGrid;

            // TemplateMethod
            ExecuteNonPersistedAction(viewModel, () => _audioFileOutputGridPresenter.Show(viewModel));
        }

        public void AudioFileOutputGridClose()
        {
            // GetViewModel
            AudioFileOutputGridViewModel viewModel = MainViewModel.Document.AudioFileOutputGrid;

            // TemplateMethod
            ExecuteNonPersistedAction(viewModel, () => _audioFileOutputGridPresenter.Close(viewModel));
        }

        public void AudioFileOutputGridDelete(int id)
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Template Method
            AudioFileOutputGridViewModel viewModel = ExecuteWriteAction(userInput, () => _audioFileOutputGridPresenter.Delete(userInput, id));

            if (viewModel.Successful)
            {
                // ToViewModel
                MainViewModel.Document.AudioFileOutputPropertiesDictionary.Remove(id);

                if (MainViewModel.Document.VisibleAudioFileOutputProperties?.Entity.ID == id)
                {
                    MainViewModel.Document.VisibleAudioFileOutputProperties = null;
                }

                // Refresh
                DocumentTreeRefresh();
                AudioFileOutputGridRefresh();
            }
        }

        public void AudioFileOutputCreate()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Template Method
            AudioFileOutput audioFileOutput = null;
            AudioFileOutputGridViewModel gridViewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

                    // Business
                    audioFileOutput = _audioFileOutputManager.Create(document);

                    // Successful
                    userInput.Successful = true;

                    return userInput;
                });

            if (gridViewModel.Successful)
            {
                // ToViewModel
                AudioFileOutputPropertiesViewModel propertiesViewModel = audioFileOutput.ToPropertiesViewModel();

                // DispatchViewModel
                DispatchViewModel(propertiesViewModel);

                // Refresh
                DocumentTreeRefresh();
                AudioFileOutputGridRefresh();
            }
        }

        public void AudioFileOutputPropertiesShow(int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel viewModel = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteNonPersistedAction(viewModel, () => _audioFileOutputPropertiesPresenter.Show(viewModel));
        }

        public void AudioFileOutputPropertiesClose(int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            AudioFileOutputPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _audioFileOutputPropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleAudioFileOutputProperties = null;

                // Refresh
                AudioFileOutputGridRefresh();
            }
        }

        public void AudioFileOutputPropertiesDelete(int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            AudioFileOutputPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _audioFileOutputPropertiesPresenter.Delete(userInput));

            if (viewModel.Successful)
            {
                // ToViewModel
                MainViewModel.Document.AudioFileOutputPropertiesDictionary.Remove(id);

                if (MainViewModel.Document.VisibleAudioFileOutputProperties?.Entity.ID == id)
                {
                    MainViewModel.Document.VisibleAudioFileOutputProperties = null;
                }

                // Refresh
                DocumentTreeRefresh();
                AudioFileOutputGridRefresh();
            }
        }

        public void AudioFileOutputPropertiesLoseFocus(int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            AudioFileOutputPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _audioFileOutputPropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                AudioFileOutputGridRefresh();
            }
        }

        // AudioOutput

        public void AudioOutputPropertiesShow()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel viewModel = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ExecuteNonPersistedAction(viewModel, () => _audioOutputPropertiesPresenter.Show(viewModel));
        }

        public void AudioOutputPropertiesClose()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _audioOutputPropertiesPresenter.Close(userInput));
        }

        public void AudioOutputPropertiesLoseFocus()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _audioOutputPropertiesPresenter.LoseFocus(userInput));
        }

        public void AudioOutputPropertiesPlay()
        {
            // NOTE:
            // Cannot use partial presenter, because this action uses both
            // AudioOutputProperties and CurrentInstrument view model.

            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ExecuteWriteAction(
                userInput,
                () =>
                {
                    // GetEntities
                    AudioOutput audioOutput = _repositories.AudioOutputRepository.Get(userInput.Entity.ID);
                    IList<Patch> entities = MainViewModel.Document.CurrentInstrument.List.Select(x => _repositories.PatchRepository.Get(x.ID)).ToArray();

                    // Business
                    Patch autoPatch = _autoPatcher.AutoPatch(entities);
                    _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(autoPatch);
                    Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(autoPatch);
                    Outlet outlet = result.Data;

                    // ToViewModel
                    AudioOutputPropertiesViewModel viewModel = audioOutput.ToPropertiesViewModel();

                    // Non-Persisted
                    viewModel.Visible = userInput.Visible;
                    viewModel.ValidationMessages = result.Messages;
                    viewModel.Successful = result.Successful;
                    viewModel.OutletIDToPlay = outlet?.ID;

                    return viewModel;
                });
        }

        // AutoPatch

        public void AutoPatchPopupShow()
        {
            // GetViewModel
            CurrentInstrumentViewModel currentInstrumentUserInput = MainViewModel.Document.CurrentInstrument;
            AutoPatchPopupViewModel autoPatchPopupUserInput = MainViewModel.Document.AutoPatchPopup;

            // RefreshCounter
            currentInstrumentUserInput.RefreshCounter++;
            autoPatchPopupUserInput.RefreshCounter++;
            autoPatchPopupUserInput.PatchDetails.RefreshCounter++;

            // Set !Successful
            currentInstrumentUserInput.Successful = false;

            // ToEntity
            Document document = MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Get Entities
            IList<Patch> underlyingPatches = currentInstrumentUserInput.List.Select(x => _repositories.PatchRepository.Get(x.ID)).ToArray();

            // Business
            Patch autoPatch = _autoPatcher.AutoPatch(underlyingPatches);

            // Business
            IResult validationResult = _documentManager.Save(document);
            if (!validationResult.Successful)
            {
                // Non-Persisted
                currentInstrumentUserInput.ValidationMessages.AddRange(validationResult.Messages);

                // DispatchViewModel
                DispatchViewModel(currentInstrumentUserInput);

                return;
            }

            // ToViewModel
            AutoPatchPopupViewModel autoPatchPopupViewModel = autoPatch.ToAutoPatchViewModel(
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.InterpolationTypeRepository,
                _entityPositionManager);

            // Non-Persisted
            autoPatchPopupViewModel.Visible = true;
            autoPatchPopupViewModel.RefreshCounter = autoPatchPopupUserInput.RefreshCounter;
            autoPatchPopupViewModel.PatchDetails.RefreshCounter = autoPatchPopupUserInput.PatchDetails.RefreshCounter;

            // Successful
            currentInstrumentUserInput.Successful = true;
            autoPatchPopupViewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(autoPatchPopupViewModel);
        }

        public void AutoPatchPopupClose()
        {
            AutoPatchPopupViewModel userInput = MainViewModel.Document.AutoPatchPopup;

            ExecuteReadAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;
                    userInput.PatchDetails.RefreshCounter++;

                    // Action
                    AutoPatchPopupViewModel viewModel = ToViewModelHelper.CreateEmptyAutoPatchViewModel();

                    // Non-Persisted
                    viewModel.RefreshCounter = userInput.RefreshCounter;
                    viewModel.PatchDetails.RefreshCounter = userInput.PatchDetails.RefreshCounter;

                    return viewModel;
                });
        }

        public void AutoPatchPopupSave()
        {
            AutoPatchPopupViewModel userInput = MainViewModel.Document.AutoPatchPopup;

            AutoPatchPopupViewModel viewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;
                    userInput.PatchDetails.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // Get Entities
                    Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

                    // ToEntity
                    Patch patch = userInput.ToEntityWithRelatedEntities(_repositories);

                    // Business
                    patch.LinkTo(document);
                    IResult result = _patchManager.SavePatch(patch);

                    AutoPatchPopupViewModel viewModel2;
                    if (result.Successful)
                    {
                        // ToViewModel
                        viewModel2 = ToViewModelHelper.CreateEmptyAutoPatchViewModel();
                    }
                    else
                    {
                        // ToViewModel
                        viewModel2 = patch.ToAutoPatchViewModel(
                            _repositories.SampleRepository,
                            _repositories.CurveRepository,
                            _repositories.InterpolationTypeRepository,
                            _entityPositionManager);

                        viewModel2.Visible = userInput.Visible;
                    }

                    // Non-Persisted
                    viewModel2.ValidationMessages.AddRange(result.Messages);
                    viewModel2.RefreshCounter = userInput.RefreshCounter;
                    viewModel2.PatchDetails.RefreshCounter = userInput.PatchDetails.RefreshCounter;

                    // Successful?
                    viewModel2.Successful = result.Successful;

                    return viewModel2;
                });

            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
                PatchDetailsShow(userInput.PatchDetails.Entity.ID);
                PatchPropertiesShow(userInput.PatchDetails.Entity.ID);
            }
        }

        // CurrentInstrument

        private void AddToInstrument(int patchID)
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _currentInstrumentPresenter.Add(userInput, patchID));
        }

        public void CurrentInstrumentClose()
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _currentInstrumentPresenter.Close(userInput));
        }

        public void CurrentInstrumentMovePatch(int patchID, int newPosition)
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _currentInstrumentPresenter.Move(userInput, patchID, newPosition));
        }

        public void CurrentInstrumentShow()
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _currentInstrumentPresenter.Load(userInput));
        }

        public void CurrentInstrumentPlay()
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _currentInstrumentPresenter.Play(userInput));
        }

        public void RemoveFromInstrument(int patchID)
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _currentInstrumentPresenter.Remove(userInput, patchID));
        }

        // Curve

        private void CurveShow(int curveID)
        {
            // Get operator ID using view model, because you cannot reliably use the entity model to get an Operator by CurveID.
            // (Long explanation:
            //  It would require an ORM query, which only works for flushed entities.
            //  And you require an ORM query, because it Operator.Curve does not have an inverse property Curve.Operator.
            //  And the inverse property is not there, because inverse properties are hacky for 1-to-1 relationships with ORM.
            //  And an intermediate flush would not work, if the there are integrity problems, that cannot be persisted to the database.)
            OperatorPropertiesViewModel_ForCurve propertiesViewModel = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve_ByCurveID(MainViewModel.Document, curveID);

            ExecuteNonPersistedAction(propertiesViewModel, () =>
            {
                int operatorID = propertiesViewModel.ID;

                // GetEntity
                Operator op = _repositories.OperatorRepository.Get(operatorID);
                int patchID = op.Patch.ID;
                
                // Redirect
                OperatorPropertiesShow(operatorID);
                CurveDetailsShow(curveID);
                PatchDetailsShow(patchID);
                OperatorSelect(patchID, operatorID);
            });
        }

        private void CurveDetailsShow(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteNonPersistedAction(userInput, () => _curveDetailsPresenter.Show(userInput));
        }

        public void CurveDetailsClose(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _curveDetailsPresenter.Close(userInput));
        }

        public void CurveDetailsLoseFocus(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _curveDetailsPresenter.LoseFocus(userInput));
        }

        public void CurveDetailsExpand(int curveID)
        {
            // Redirect
            CurveShow(curveID);
        }

        // Document Grid

        public void DocumentCannotDeleteOK()
        {
            // GetViewModel
            DocumentCannotDeleteViewModel userInput = MainViewModel.DocumentCannotDelete;

            // Partial Action
            ExecuteNonPersistedAction(userInput, () => _documentCannotDeletePresenter.OK(userInput));
        }

        public void DocumentDeleteCancel()
        {
            // GetViewModel
            DocumentDeleteViewModel viewModel = MainViewModel.DocumentDelete;

            // Partial Action
            ExecuteNonPersistedAction(viewModel, () => _documentDeletePresenter.Cancel(viewModel));
        }

        public void DocumentDeleteConfirm()
        {
            // GetViewModel
            DocumentDeleteViewModel viewModel = MainViewModel.DocumentDelete;

            // Partial Action
            ViewModelBase viewModel2 = _documentDeletePresenter.Confirm(viewModel);

            // RefreshCounter
            viewModel.RefreshCounter++;

            // Set !Successful
            viewModel.Successful = false;

            if (viewModel2 is DocumentDeletedViewModel)
            {
                _repositories.Commit();
            }

            // Successful
            viewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel2);
        }

        public void DocumentDeletedOK()
        {
            // GetViewModel
            DocumentDeletedViewModel viewModel = MainViewModel.DocumentDeleted;

            // Partial Action
            ExecuteNonPersistedAction(viewModel, () =>  _documentDeletedPresenter.OK(viewModel));
        }

        public void DocumentDeleteShow(int id)
        {
            // GetViewModel
            DocumentGridViewModel gridViewModel = MainViewModel.DocumentGrid;

            // RefreshCounter
            gridViewModel.RefreshCounter++;

            // Set !Successful
            gridViewModel.Successful = false;

            // Partial Action
            ViewModelBase viewModel2 = _documentDeletePresenter.Show(id);

            // Successful
            gridViewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel2);
        }

        public void DocumentDetailsClose()
        {
            // GetViewModel
            DocumentDetailsViewModel viewModel = MainViewModel.DocumentDetails;

            // Partial Action
            _documentDetailsPresenter.Close(viewModel);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsCreate()
        {
            // GetViewModel
            DocumentGridViewModel gridViewModel = MainViewModel.DocumentGrid;

            // RefreshCounter
            gridViewModel.RefreshCounter++;

            // Set !Successful
            gridViewModel.Successful = false;

            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Create();

            // Successful
            viewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDetailsSave()
        {
            // GetViewModel
            DocumentDetailsViewModel userInput = MainViewModel.DocumentDetails;

            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Save(userInput);
            
            // Commit
            // (do it before opening the document, which does a big query, which requires at least a flush.)
            if (viewModel.Successful)
            {
                _repositories.DocumentRepository.Commit();
            }

            // DispatchViewModel
            DispatchViewModel(viewModel);

            if (viewModel.Successful)
            {
                // Refresh
                DocumentGridRefresh();
     
                // Redirect
                DocumentOpen(viewModel.Document.ID);
            }
        }

        public void DocumentGridClose()
        {
            // GetViewModel
            DocumentGridViewModel viewModel = MainViewModel.DocumentGrid;

            // Partial Action
            ExecuteNonPersistedAction(viewModel, () => _documentGridPresenter.Close(viewModel));
        }

        public void DocumentGridPlay(int id)
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _documentGridPresenter.Play(userInput, id));
        }

        public void DocumentGridShow()
        {
            // GetViewModel
            DocumentGridViewModel viewModel = MainViewModel.DocumentGrid;

            // Partial Action
            ExecuteNonPersistedAction(viewModel, () => _documentGridPresenter.Show(viewModel));
        }

        // Document

        public void DocumentActivate()
        {
            DocumentRefresh();
        }

        public void DocumentClose()
        {
            if (MainViewModel.Document.IsOpen)
            {
                // Partial Actions
                string titleBar = _titleBarPresenter.Show();
                MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: false);
                DocumentViewModel documentViewModel = ToViewModelHelper.CreateEmptyDocumentViewModel();

                // DispatchViewModel
                MainViewModel.TitleBar = titleBar;
                MainViewModel.Menu = menuViewModel;
                MainViewModel.Document = documentViewModel;

                // Redirections
                DocumentGridShow();
            }
        }

        public void DocumentOpen(string name)
        {
            Document document = _documentManager.Get(name);

            DocumentOpen(document);
        }

        public void DocumentOpen(int id)
        {
            Document document = _documentManager.Get(id);

            DocumentOpen(document);
        }

        private void DocumentOpen(Document document)
        {
            // ToViewModel
            DocumentViewModel viewModel = document.ToViewModel(_repositories, _entityPositionManager);

            // Non-Persisted
            viewModel.DocumentTree.Visible = true;
            viewModel.IsOpen = true;

            // Set everything to successful.
            viewModel.AudioFileOutputGrid.Successful = true;
            viewModel.AudioFileOutputPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.AudioOutputProperties.Successful = true;
            viewModel.AutoPatchPopup.PatchDetails.Successful = true;
            viewModel.AutoPatchPopup.PatchProperties.Successful = true;
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForCaches.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForCurves.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForInletsToDimension.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForNumbers.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForPatchInlets.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForPatchOutlets.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_ForSamples.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_WithCollectionRecalculation.Values.ForEach(x => x.Successful = true);
            viewModel.AutoPatchPopup.OperatorPropertiesDictionary_WithInterpolation.Values.ForEach(x => x.Successful = true);
            viewModel.CurrentInstrument.Successful = true;
            viewModel.CurveDetailsDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.DocumentProperties.Successful = true;
            viewModel.DocumentTree.Successful = true;
            viewModel.NodePropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForCaches.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForCurves.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForInletsToDimension.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForNumbers.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForPatchInlets.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForPatchOutlets.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_ForSamples.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_WithCollectionRecalculation.Values.ForEach(x => x.Successful = true);
            viewModel.OperatorPropertiesDictionary_WithInterpolation.Values.ForEach(x => x.Successful = true);
            viewModel.PatchDetailsDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.PatchPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.ScaleGrid.Successful = true;
            viewModel.ScalePropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.ToneGridEditDictionary.Values.ForEach(x => x.Successful = true);

            string titleBar = _titleBarPresenter.Show(document);
            MenuViewModel menuViewModel = _menuPresenter.Show(documentIsOpen: true);

            // DispatchViewModel
            MainViewModel.Document = viewModel;
            MainViewModel.TitleBar = titleBar;
            MainViewModel.Menu = menuViewModel;

            // Redirections
            MainViewModel.DocumentGrid.Visible = false;
            CurrentInstrumentShow();
            if (document.Patches.Count == 1)
            {
                PatchDetailsShow(document.Patches[0].ID);
            }
        }

        public void DocumentOrPatchNotFoundOK()
        {
            // GetViewModel
            DocumentOrPatchNotFoundPopupViewModel userInput = MainViewModel.DocumentOrPatchNotFound;

            // Template Method
            ExecuteNonPersistedAction(userInput, () => _documentOrPatchNotFoundPresenter.OK(userInput));
        }

        public void DocumentPropertiesShow()
        {
            // GetViewModel
            DocumentPropertiesViewModel viewModel = MainViewModel.Document.DocumentProperties;

            // Template Method
            ExecuteNonPersistedAction(viewModel, () => _documentPropertiesPresenter.Show(viewModel));
        }

        public void DocumentPropertiesClose() => DocumentPropertiesCloseOrLoseFocus(_documentPropertiesPresenter.Close);

        public void DocumentPropertiesLoseFocus() => DocumentPropertiesCloseOrLoseFocus(_documentPropertiesPresenter.LoseFocus);

        private void DocumentPropertiesCloseOrLoseFocus(Func<DocumentPropertiesViewModel, DocumentPropertiesViewModel> partialAction)
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Template Method
            DocumentPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => partialAction(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                TitleBarRefresh();
                DocumentGridRefresh();
                DocumentTreeRefresh();
            }
        }

        public void DocumentPropertiesPlay()
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _documentPropertiesPresenter.Play(userInput));
        }

        /// <summary>
        /// Will do a a ViewModel to Entity conversion.
        /// (The private Refresh methods do not.)
        /// Will also ApplyExternalUnderlyingPatches.
        /// </summary>
        public void DocumentRefresh()
        {
            if (MainViewModel.Document.IsOpen)
            {
                // ToEntity
                MainViewModel.ToEntityWithRelatedEntities(_repositories);

                // Business
                _documentManager.Refresh(MainViewModel.Document.ID);

                // ToViewModel
                DocumentViewModelRefresh();
            }
        }

        public void DocumentSave()
        {
            // ToEntity
            // Get rid of AutoPatch view model temporarily from the DocumentViewModel.
            // It should not be saved and this is the only action upon which it should not be converted to Entity.
            Document document;
            AutoPatchPopupViewModel originalAutoPatchPopup = null;
            try
            {
                originalAutoPatchPopup = MainViewModel.Document.AutoPatchPopup;
                MainViewModel.Document.AutoPatchPopup = null;

                document = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }
            finally
            {
                if (originalAutoPatchPopup != null)
                {
                    MainViewModel.Document.AutoPatchPopup = originalAutoPatchPopup;
                }
            }

            // Business
            IResult validationResult = _documentManager.Save(document);
            IResult warningsResult = _documentManager.GetWarningsRecursive(document);

            // Commit
            if (validationResult.Successful)
            {
                _repositories.Commit();
                _documentManager.RefreshSystemDocumentIfNeeded(document);
            }

            // ToViewModel
            MainViewModel.ValidationMessages = validationResult.Messages;
            MainViewModel.WarningMessages = warningsResult.Messages;
        }

        public void DocumentTreeAdd()
        {
            // Involves both DocumentTree and LibrarySelectionPopup,
            // so cannot be handled by a single sub-presenter.

            DocumentTreeNodeTypeEnum documentTreeNodeTypeEnum = MainViewModel.Document.DocumentTree.SelectedNodeType;

            switch (documentTreeNodeTypeEnum)
            {
                case DocumentTreeNodeTypeEnum.Libraries:
                    DocumentTreeAddLibrary();
                    break;

                case DocumentTreeNodeTypeEnum.PatchGroup:
                    DocumentTreeCreatePatch(MainViewModel.Document.DocumentTree.SelectedCanonicalPatchGroup);
                    break;

                default:
                    throw new ValueNotSupportedException(documentTreeNodeTypeEnum);
            }
        }

        private void DocumentTreeAddLibrary()
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // Template Method
            ExecuteWriteAction(userInput, () => _librarySelectionPopupPresenter.Show(userInput));
        }

        /// <param name="group">nullable</param>
        private void DocumentTreeCreatePatch(string group)
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            int patchID = 0;

            // Template Method
            DocumentTreeViewModel viewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

                    // Business
                    Patch patch = _patchManager.CreatePatch(document);
                    patch.GroupName = group;
                    patchID = patch.ID;

                    // Successful
                    userInput.Successful = true;

                    return userInput;
                });

            if (viewModel.Successful)
            {
                // Refresh
                DocumentViewModelRefresh();

                // Redirect
                PatchDetailsShow(patchID);
                PatchPropertiesShow(patchID);
            }
        }

        public void DocumentTreeAddToInstrument()
        {
            // Inolves both DocumentTree and CurrentInstrument view,
            // so cannot be handled by a single sub-presenter.

            if (!MainViewModel.Document.DocumentTree.SelectedItemID.HasValue)
            {
                throw new NotHasValueException(() => MainViewModel.Document.DocumentTree.SelectedItemID);
            }
            int patchID = MainViewModel.Document.DocumentTree.SelectedItemID.Value;

            AddToInstrument(patchID);
        }

        public void DocumentTreeClose() => ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.Close);

        public void DocumentTreeHoverPatch(int id)
        {
            // GetViewModel
            DocumentTreeViewModel viewModel = MainViewModel.Document.DocumentTree;

            // TemplateMethod
            ExecuteReadAction(viewModel, () => _documentTreePresenter.HoverPatch(viewModel, id));
        }

        public void DocumentTreeNew()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // TemplateMethod
            Operator op = null;
            DocumentTreeViewModel viewModel = ExecuteWriteAction(userInput, () =>
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                switch (userInput.SelectedNodeType)
                {
                    case DocumentTreeNodeTypeEnum.Patch:
                    case DocumentTreeNodeTypeEnum.LibraryPatch:
                        if (!userInput.SelectedItemID.HasValue)
                        {
                            throw new NullException(() => userInput.SelectedItemID);
                        }

                        if (MainViewModel.Document.VisiblePatchDetails == null)
                        {
                            throw new NullException(() => MainViewModel.Document.VisiblePatchDetails);
                        }

                        // GetEntities
                        Patch underlyingPatch = _repositories.PatchRepository.Get(userInput.SelectedItemID.Value);
                        Patch patch = _repositories.PatchRepository.Get(MainViewModel.Document.VisiblePatchDetails.Entity.ID);

                        bool isSamplePatch = underlyingPatch.IsSamplePatch();
                        if (isSamplePatch)
                        {
                            // ToViewModel
                            MainViewModel.Document.SampleFileBrowser.Visible = true;
                            MainViewModel.Document.SampleFileBrowser.DestPatchID = patch.ID;
                        }
                        else
                        {
                            // Business
                            var operatorFactory = new OperatorFactory(patch, _repositories);
                            op = operatorFactory.New(underlyingPatch, GetVariableInletOrOutletCount(underlyingPatch));
                            _autoPatcher.CreateNumbersForEmptyInletsWithDefaultValues(op, ESTIMATED_OPERATOR_WIDTH, OPERATOR_HEIGHT, _entityPositionManager);
                        }

                        // Successful
                        userInput.Successful = true;

                        // Do not do bother with ToViewModel. We will do a full Refresh later.
                        return userInput;

                    default:
                        throw new ValueNotSupportedException(userInput.SelectedNodeType);
                }
            });

            if (viewModel.Successful)
            {
                // Refresh
                DocumentViewModelRefresh();

                // Redirect
                if (op != null)
                {
                    OperatorExpand(op.ID);
                }
            }
        }

        public void DocumentTreeOpenItemExternally()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _documentTreePresenter.OpenItemExternally(userInput));
        }

        public void DocumentTreePlay()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // TemplateMethod
            ExecuteWriteAction(userInput, func);

            DocumentTreeViewModel func()
            {
                // RefreshCounter
                userInput.RefreshCounter++;

                // Set !Successful
                userInput.Successful = false;

                // GetEntities
                Document document = _repositories.DocumentRepository.Get(userInput.ID);

                Result<Outlet> result;
                switch (userInput.SelectedNodeType)
                {
                    case DocumentTreeNodeTypeEnum.AudioOutput:
                    {
                        // GetEntities
                        IList<Patch> entities = MainViewModel.Document.CurrentInstrument.List
                                                             .Select(x => _repositories.PatchRepository.Get(x.ID))
                                                             .ToArray();
                        // Business
                        Patch autoPatch = _autoPatcher.AutoPatch(entities);
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(autoPatch);
                        result = _autoPatcher.AutoPatch_TryCombineSounds(autoPatch);

                        break;
                    }

                    case DocumentTreeNodeTypeEnum.Library:
                    {
                        if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

                        // GetEntity
                        DocumentReference documentReference = _repositories.DocumentReferenceRepository.Get(userInput.SelectedItemID.Value);

                        // Business
                        result = _autoPatcher.TryAutoPatchFromDocumentRandomly(documentReference.LowerDocument, mustIncludeHidden: false);
                        if (result.Data != null)
                        {
                            _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
                        }

                        break;
                    }

                    case DocumentTreeNodeTypeEnum.Patch:
                    case DocumentTreeNodeTypeEnum.LibraryPatch:
                    {
                        if (!userInput.SelectedItemID.HasValue) throw new NullException(() => userInput.SelectedItemID);

                        // GetEntities
                        Patch patch = _repositories.PatchRepository.Get(userInput.SelectedItemID.Value);

                        // Business
                        result = _autoPatcher.AutoPatch_TryCombineSounds(patch);
                        if (result.Data != null)
                        {
                            _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
                        }

                        break;
                    }

                    case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
                    {
                        if (!userInput.SelectedPatchGroupLowerDocumentReferenceID.HasValue) throw new NullException(() => userInput.SelectedPatchGroupLowerDocumentReferenceID);

                        // GetEntities
                        DocumentReference lowerDocumentReference = _repositories.DocumentReferenceRepository.Get(userInput.SelectedPatchGroupLowerDocumentReferenceID.Value);

                        // Business
                        result = _autoPatcher.TryAutoPatchFromPatchGroupRandomly(
                            lowerDocumentReference.LowerDocument,
                            userInput.SelectedCanonicalPatchGroup,
                            mustIncludeHidden: false);
                        if (result.Data != null)
                        {
                            _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
                        }

                        break;
                    }

                    case DocumentTreeNodeTypeEnum.PatchGroup:
                    {
                        // Business
                        result = _autoPatcher.TryAutoPatchFromPatchGroupRandomly(document, userInput.SelectedCanonicalPatchGroup, mustIncludeHidden: false);
                        
                        break;
                    }

                    case DocumentTreeNodeTypeEnum.Libraries:
                    {
                        // Business
                        IList<Document> lowerDocuments = document.LowerDocumentReferences.Select(x => x.LowerDocument).ToArray();
                        result = _autoPatcher.TryAutoPatchFromDocumentsRandomly(lowerDocuments, mustIncludeHidden: false);
                        if (result.Data != null)
                        {
                            _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(result.Data.Operator.Patch);
                        }

                        break;
                    }

                    case DocumentTreeNodeTypeEnum.AudioFileOutputList:
                    case DocumentTreeNodeTypeEnum.Scales:
                    default:
                    {
                        // Successful
                        userInput.Successful = true;

                        return userInput;
                    }
                }

                // Business
                Outlet outlet = result.Data;

                // ToViewModel
                var converter = new RecursiveDocumentTreeViewModelFactory();
                DocumentTreeViewModel viewModel = converter.ToTreeViewModel(document);

                // Non-Persisted
                _documentTreePresenter.CopyNonPersistedProperties(userInput, viewModel);
                viewModel.OutletIDToPlay = outlet?.ID;
                viewModel.Successful = result.Successful;
                viewModel.ValidationMessages.AddRange(result.Messages);

                return viewModel;
            }
        }

        public void DocumentTreeRemove()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // Template Method
            DocumentTreeViewModel viewModel = ExecuteWriteAction(userInput, () => _documentTreePresenter.Remove(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void DocumentTreeSelectAudioFileOutputs() => ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectAudioFileOutputs);

        public void DocumentTreeSelectAudioOutput() => ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectAudioOutput);

        public void DocumentTreeSelectLibraries() => ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectLibraries);

        public void DocumentTreeSelectLibrary(int id) => ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibrary(x, id));

        public void DocumentTreeSelectLibraryPatch(int id)
        {
            ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryPatch(x, id));
        }

        public void DocumentTreeSelectLibraryPatchGroup(int lowerDocumentReferenceID, string patchGroup)
        {
            ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectLibraryPatchGroup(x, lowerDocumentReferenceID, patchGroup));
        }

        public void DocumentTreeSelectScales() => ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.SelectScales);

        public void DocumentTreeSelectPatch(int id) => ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectPatch(x, id));

        public void DocumentTreeSelectPatchGroup(string group)
        {
            ExecuteNonPersistedDocumentTreeAction(x => _documentTreePresenter.SelectPatchGroup(x, group));
        }

        public void DocumentTreeShow() => ExecuteNonPersistedDocumentTreeAction(_documentTreePresenter.Show);

        /// <summary>
        /// On top of the regular ExecuteNonPersistedAction,
        /// will set CanCreateNew, which cannot be determined by entities or DocumentTreeViewModel alone.
        /// </summary>
        private void ExecuteNonPersistedDocumentTreeAction(Action<DocumentTreeViewModel> partialAction)
        {
            // GetViewModel
            DocumentTreeViewModel viewModel = MainViewModel.Document.DocumentTree;

            // Action
            ExecuteNonPersistedAction(viewModel, () =>
            {
                partialAction(viewModel);
                SetCanCreateNew(viewModel);
            });
        }

        private void SetCanCreateNew(DocumentTreeViewModel viewModel)
        {
            bool patchDetailsVisible = MainViewModel.Document.VisiblePatchDetails != null;
            viewModel.CanCreateNew = ToViewModelHelper.GetCanCreateNew(viewModel.SelectedNodeType, patchDetailsVisible);
        }

        // Library

        public void LibraryPropertiesClose(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // Template Method
            LibraryPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _libraryPropertiesPresenter.Close(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleLibraryProperties = null;

                DocumentViewModelRefresh();
            }
        }

        public void LibraryPropertiesLoseFocus(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // Template Method
            LibraryPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _libraryPropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void LibraryPropertiesOpenExternally(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // TemplateMethod
            ExecuteReadAction(userInput, () => _libraryPropertiesPresenter.OpenExternally(userInput));
        }

        public void LibraryPropertiesPlay(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _libraryPropertiesPresenter.Play(userInput));
        }

        public void LibraryPropertiesRemove(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // Template Method
            LibraryPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _libraryPropertiesPresenter.Remove(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void LibraryPropertiesShow(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel viewModel = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // Template Method
            ExecuteNonPersistedAction(viewModel, () => _libraryPropertiesPresenter.Show(viewModel));
        }

        public void LibrarySelectionPopupCancel()
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // Template Method
            ExecuteNonPersistedAction(userInput, () => _librarySelectionPopupPresenter.Cancel(userInput));
        }

        public void LibrarySelectionPopupOK(int? lowerDocumentID)
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // Template Method
            LibrarySelectionPopupViewModel viewModel = ExecuteWriteAction(userInput, () => _librarySelectionPopupPresenter.OK(userInput, lowerDocumentID));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void LibrarySelectionPopupOpenItemExternally(int lowerDocumentID)
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _librarySelectionPopupPresenter.OpenItemExternally(userInput, lowerDocumentID));
        }

        public void LibrarySelectionPopupPlay(int lowerDocumentID)
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _librarySelectionPopupPresenter.Play(userInput, lowerDocumentID));
        }

        // Node

        /// <summary>
        /// Rotates between node types for the selected node.
        /// If no node is selected, nothing happens.
        /// </summary>
        public void NodeChangeSelectedNodeType(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validation
            if (!userInput.SelectedNodeID.HasValue)
            {
                return;
            }
            int nodeID = userInput.SelectedNodeID.Value;

            // Template Method
            CurveDetailsViewModel viewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // GetEntity
                    Node node = _repositories.NodeRepository.Get(nodeID);

                    // Business
                    _curveManager.RotateNodeType(node);

                    // Successful
                    userInput.Successful = true;

                    return userInput;
                });

            // Refresh
            if (viewModel.Successful)
            {
                CurveDetailsNodeRefresh(curveID, nodeID);
                NodePropertiesRefresh(nodeID);
            }
        }

        public void NodeCreate(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // Template Method
            CurveDetailsViewModel viewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // GetEntity
                    Curve curve = _repositories.CurveRepository.Get(userInput.Curve.ID);

                    // Business
                    // TODO: This kind of stuff belongs in the business layer.
                    Node afterNode;
                    if (userInput.SelectedNodeID.HasValue)
                    {
                        afterNode = _repositories.NodeRepository.Get(userInput.SelectedNodeID.Value);
                    }
                    else
                    {
                        // Insert after last node if none selected.
                        afterNode = curve.Nodes.OrderBy(x => x.X).Last();
                    }

                    // Business
                    // ReSharper disable once UnusedVariable
                    Node node = _curveManager.CreateNode(curve, afterNode);

                    // Successful
                    userInput.Successful = true;

                    return userInput;
                });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void NodeDeleteSelected(int curveID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validation
            if (!userInput.SelectedNodeID.HasValue)
            {
                MainViewModel.ValidationMessages.Add(ResourceFormatter.SelectANodeFirst);
                return;
            }

            // Template Method
            CurveDetailsViewModel viewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // GetEntity
                    int nodeID = userInput.SelectedNodeID.Value;
                    Node node = _repositories.NodeRepository.Get(nodeID);

                    // Business
                    IResult result = _curveManager.DeleteNode(node);

                    // Non-Persisted
                    userInput.ValidationMessages = result.Messages;
                    userInput.SelectedNodeID = null;

                    // Successful?
                    userInput.Successful = result.Successful;

                    return userInput;
                });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void NodeMoving(int curveID, int nodeID, double x, double y)
        {
            // Opted to not use the TemplateActionMethod
            // (which would do a complete DocumentViewModel to Entity conversion),
            // because this is faster but less robust.
            // Because it is not nice when moving nodes is slow.
            // When you work in-memory backed with zipped XML,
            // you might use the more robust method again.
            // The overhead is mainly in the database queries.

            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // Partial ToEntity
            userInput.ToEntityWithNodes(_curveRepositories);

            // Partial Action
            CurveDetailsViewModel viewModel = _curveDetailsPresenter.NodeMoving(userInput, nodeID, x, y);
            
            // Refresh
            if (viewModel.Successful)
            {
                CurveDetailsNodeRefresh(curveID, nodeID);
                NodePropertiesRefresh(nodeID);
            }
        }

        public void NodeMoved(int curveID, int nodeID, double x, double y)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // TemplateMethod
            CurveDetailsViewModel viewModel = ExecuteWriteAction(userInput, () => _curveDetailsPresenter.NodeMoved(userInput, nodeID, x, y));

            // Refresh
            if (viewModel.Successful)
            {
                CurveDetailsNodeRefresh(curveID, nodeID);
                NodePropertiesRefresh(nodeID);
            }
        }

        public void NodePropertiesShow(int id)
        {
            // GetViewModel
            NodePropertiesViewModel viewModel = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ExecuteNonPersistedAction(viewModel, () => _nodePropertiesPresenter.Show(viewModel));
        }

        public void NodePropertiesClose(int id)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            NodePropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _nodePropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleNodeProperties = null;

                // Refresh
                Node node = _repositories.NodeRepository.Get(id);
                CurveDetailsNodeRefresh(node.Curve.ID, id);
            }
        }

        public void NodePropertiesDelete(int nodeID)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);

            // Template Method
            NodePropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _nodePropertiesPresenter.Delete(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void NodePropertiesLoseFocus(int id)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            NodePropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _nodePropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                Node node = _repositories.NodeRepository.Get(id);
                CurveDetailsNodeRefresh(node.Curve.ID, id);
            }
        }

        public void NodeSelect(int curveID, int nodeID)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // TemplateMethod
            ExecuteNonPersistedAction(userInput, () => _curveDetailsPresenter.SelectNode(userInput, nodeID));
        }

        // Operator

        public void OperatorChangeInputOutlet(int patchID, int inletID, int inputOutletID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _patchDetailsPresenter.ChangeInputOutlet(userInput, inletID, inputOutletID));
        }

        public void OperatorDeleteSelected(int patchID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // Template Method
            PatchDetailsViewModel viewModel = ExecuteWriteAction(userInput, () => _patchDetailsPresenter.DeleteOperator(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorMove(int patchID, int operatorID, float centerX, float centerY)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _patchDetailsPresenter.MoveOperator(userInput, operatorID, centerX, centerY));
        }

        public void OperatorPropertiesClose(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel userInput = ViewModelSelector.GetOperatorPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesClose_ForCache(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCache userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCache viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForCache.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_ForCache = null;

                // Refresh
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesClose_ForCurve(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve_ByOperatorID(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCurve viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForCurve.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_ForCurve = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesClose_ForInletsToDimension(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForInletsToDimension userInput =
                ViewModelSelector.GetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForInletsToDimension viewModel = ExecuteWriteAction(
                userInput,
                () => _operatorPropertiesPresenter_ForInletsToDimension.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension = null;

                // Refresh
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesClose_ForNumber(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForNumber userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForNumber viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForNumber.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_ForNumber = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesClose_ForPatchInlet(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchInlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchInlet viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesClose_ForPatchOutlet(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchOutlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesClose_ForSample(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForSample userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForSample viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForSample.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_ForSample = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesClose_WithInterpolation(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithInterpolation userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithInterpolation viewModel =
                ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_WithInterpolation.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = null;

                // Refresh
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesClose_WithCollectionRecalculation(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithCollectionRecalculation userInput =
                ViewModelSelector.GetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ExecuteWriteAction(
                userInput,
                () => _operatorPropertiesPresenter_WithCollectionRecalculation.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = null;

                // Refresh
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesDelete(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModelBase userInput = ViewModelSelector.GetOperatorPropertiesViewModelPolymorphic(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModelBase viewModel = ExecuteWriteAction(userInput, () => GetOperatorPropertiesPresenter(id).Delete(userInput)); 

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesExpand(int id)
        {
            // Redirect
            OperatorExpand(id);
        }

        public void OperatorPropertiesLoseFocus(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel userInput = ViewModelSelector.GetOperatorPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesLoseFocus_ForCache(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCache userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCache viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForCache.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesLoseFocus_ForCurve(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve_ByOperatorID(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCurve viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForCurve.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesLoseFocus_ForInletsToDimension(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForInletsToDimension userInput =
                ViewModelSelector.GetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForInletsToDimension viewModel = ExecuteWriteAction(
                userInput,
                () => _operatorPropertiesPresenter_ForInletsToDimension.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesLoseFocus_ForNumber(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForNumber userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForNumber viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForNumber.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesLoseFocus_ForPatchInlet(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchInlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchInlet viewModel =
                ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesLoseFocus_ForPatchOutlet(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForPatchOutlet userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForPatchOutlet viewModel =
                ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesLoseFocus_ForSample(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForSample userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForSample viewModel = ExecuteWriteAction(userInput, () => _operatorPropertiesPresenter_ForSample.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesLoseFocus_WithInterpolation(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithInterpolation userInput = ViewModelSelector.GetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithInterpolation viewModel = ExecuteWriteAction(
                userInput,
                () => _operatorPropertiesPresenter_WithInterpolation.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesLoseFocus_WithCollectionRecalculation(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_WithCollectionRecalculation userInput =
                ViewModelSelector.GetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ExecuteWriteAction(
                userInput,
                () => _operatorPropertiesPresenter_WithCollectionRecalculation.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesPlay(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModelBase userInput = ViewModelSelector.GetOperatorPropertiesViewModelPolymorphic(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => GetOperatorPropertiesPresenter(id).Play(userInput));
        }

        private void OperatorPropertiesShow(int id)
        {
            // GetViewModel & Partial Action
            {
                OperatorPropertiesViewModel viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCache viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForCache.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCurve viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve_ByOperatorID(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForCurve.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForInletsToDimension viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForInletsToDimension.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForNumber viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForNumber.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchInlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForPatchInlet.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchOutlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForPatchOutlet.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForSample viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_ForSample.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithInterpolation viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_WithInterpolation.Show(viewModel));
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, id);
                if (viewModel != null)
                {
                    ExecuteNonPersistedAction(viewModel, () => _operatorPropertiesPresenter_WithCollectionRecalculation.Show(viewModel));
                    return;
                }
            }

            throw new NotFoundException<OperatorPropertiesViewModelBase>(new { OperatorID = id });
        }

        public void OperatorSelect(int patchID, int operatorID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // Partial Action
            ExecuteNonPersistedAction(userInput, () => _patchDetailsPresenter.SelectOperator(userInput, operatorID));
        }

        public void OperatorExpand(int operatorID)
        {
            ExecuteReadAction(null, () =>
            {
                // GetEntities
                Operator op = _repositories.OperatorRepository.Get(operatorID);
                Curve curve = op.Curve;

                // Redirect
                if (curve != null)
                {
                    CurveShow(curve.ID);
                }
                else
                {
                    int patchID = op.Patch.ID;
                    OperatorPropertiesShow(operatorID);
                    PatchDetailsShow(patchID);
                    OperatorSelect(patchID, operatorID);
                }
            });
        }

        // Patch

        public void PatchDetailsAddToInstrument(int id)
        {
            AddToInstrument(id);
        }

        public void PatchDetailsClose(int id)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            PatchDetailsViewModel viewModel = ExecuteWriteAction(userInput, () => _patchDetailsPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisiblePatchDetails = null;
            }
        }

        public void PatchDetailsLoseFocus(int id)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _patchDetailsPresenter.LoseFocus(userInput));
        }

        public void PatchDetailsPlay(int id)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

            // Template Method
            ExecuteWriteAction(userInput, () => _patchDetailsPresenter.Play(userInput));
        }

        public void PatchDetailsShow(int id)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteNonPersistedAction(userInput, () => _patchDetailsPresenter.Show(userInput));
        }

        public void PatchPropertiesAddToInstrument(int id)
        {
            AddToInstrument(id);
        }

        public void PatchPropertiesClose(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            PatchPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _patchPropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisiblePatchProperties = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void PatchPropertiesDelete(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            PatchPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _patchPropertiesPresenter.Delete(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void PatchPropertiesChangeHasDimension(int id)
        {         
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ExecuteWriteAction(userInput, () => _patchPropertiesPresenter.ChangeHasDimension(userInput));
        }

        public void PatchPropertiesLoseFocus(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            PatchPropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _patchPropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void PatchPropertiesPlay(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ExecuteWriteAction(userInput, () => _patchPropertiesPresenter.Play(userInput));
        }

        public void PatchPropertiesShow(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel viewModel = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ExecuteNonPersistedAction(viewModel, () => _patchPropertiesPresenter.Show(viewModel));
        }

        // Sample
    
        public void SampleFileBrowserCancel()
        {
            SampleFileBrowserViewModel userInput = MainViewModel.Document.SampleFileBrowser;

            ExecuteNonPersistedAction(userInput, () => _sampleFileBrowserPresenter.Cancel(userInput));
        }

        public void SampleFileBrowserOK()
        {
            // GetViewModel
            SampleFileBrowserViewModel userInput = MainViewModel.Document.SampleFileBrowser;

            // TemplateMethod
            SampleFileBrowserViewModel viewModel = ExecuteWriteAction(userInput, () => _sampleFileBrowserPresenter.OK(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        // Scale

        public void ScaleGridShow()
        {
            // GetViewModel
            ScaleGridViewModel viewModel = MainViewModel.Document.ScaleGrid;

            // Template Method
            ExecuteNonPersistedAction(viewModel, () => _scaleGridPresenter.Show(viewModel));
        }

        public void ScaleGridClose()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ExecuteNonPersistedAction(userInput, () => _scaleGridPresenter.Close(userInput));
        }

        public void ScaleCreate()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ScaleGridViewModel viewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // GetEntity
                    Document document = _repositories.DocumentRepository.Get(userInput.DocumentID);

                    // Business
                    // ReSharper disable once UnusedVariable
                    Scale scale = _scaleManager.Create(document, mustSetDefaults: true);

                    // Successful
                    userInput.Successful = true;

                    return userInput;
                });

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void ScaleGridDelete(int id)
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ScaleGridViewModel viewModel = ExecuteWriteAction(userInput, () => _scaleGridPresenter.Delete(userInput, id));

            if (viewModel.Successful)
            {
                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void ScaleShow(int id)
        {
            // GetViewModel
            ScalePropertiesViewModel viewModel1 = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);
            ToneGridEditViewModel viewModel2 = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID: id);

            // Partial Actions
            _scalePropertiesPresenter.Show(viewModel1);
            _toneGridEditPresenter.Show(viewModel2);
        
            // DispatchViewModel
            DispatchViewModel(viewModel1);
            DispatchViewModel(viewModel2);
        }

        public void ScalePropertiesClose(int id)
        {
            // Get ViewModel
            ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ScalePropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _scalePropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleScaleProperties = null;

                // Refresh
                ToneGridEditRefresh(userInput.Entity.ID);
                ScaleGridRefresh();
            }
        }

        public void ScalePropertiesDelete(int id)
        {
            // GetViewModel
            ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ScalePropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _scalePropertiesPresenter.Delete(userInput));

            if (viewModel.Successful)
            {
                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void ScalePropertiesLoseFocus(int id)
        {
            // Get ViewModel
            ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ScalePropertiesViewModel viewModel = ExecuteWriteAction(userInput, () => _scalePropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                ToneGridEditRefresh(userInput.Entity.ID);
                ScaleGridRefresh();
            }
        }

        // Tone

        public void ToneCreate(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModelValidator
            IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
            if (!viewModelValidator.IsValid)
            {
                userInput.ValidationMessages = viewModelValidator.Messages;
                DispatchViewModel(userInput);
                return;
            }

            // TemplateMethod
            Tone tone = null;
            ToneGridEditViewModel viewModel = ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // Get Entity
                    Scale scale = _repositories.ScaleRepository.Get(scaleID);

                    // Business
                    tone = _scaleManager.CreateTone(scale);

                    // Successful
                    userInput.Successful = true;

                    return userInput;
                });

            if (viewModel.Successful)
            {
                // ToViewModel
                ToneViewModel toneViewModel = tone.ToViewModel();
                userInput.Tones.Add(toneViewModel);
                // Do not sort grid, so that the new item appears at the bottom.
            }
        }

        public void ToneDelete(int scaleID, int toneID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModelValidator
            IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
            if (!viewModelValidator.IsValid)
            {
                userInput.ValidationMessages = viewModelValidator.Messages;
                DispatchViewModel(userInput);
                return;
            }

            // Template Method
            ExecuteWriteAction(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // GetEntity
                    Tone tone = _repositories.ToneRepository.Get(toneID);
                    Scale scale = tone.Scale;

                    // Business
                    _scaleManager.DeleteTone(tone);

                    // ToViewModel
                    ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();

                    // Non-Persisted
                    viewModel.Visible = userInput.Visible;

                    // Successful
                    viewModel.Successful = true;

                    return viewModel;
                });
        }

        public void ToneGridEditClose(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            ToneGridEditViewModel viewModel = ExecuteWriteAction(userInput, () => _toneGridEditPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleToneGridEdit = null;
            }
        }

        public void ToneGridEditLoseFocus(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            ExecuteWriteAction(userInput, () => _toneGridEditPresenter.LoseFocus(userInput));
        }

        public void ToneGridEditEdit(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            ExecuteWriteAction(userInput, () => _toneGridEditPresenter.Edit(userInput));
        }

        public void TonePlay(int scaleID, int toneID)
        {
            // NOTE:
            // Cannot use partial presenter, because this action uses both
            // ToneGridEditViewModel and CurrentInstrument view model.

            // GetEntity
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            ExecuteWriteAction(
                userInput,
                () =>
                {
                    // ViewModel Validator
                    IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
                    if (!viewModelValidator.IsValid)
                    {
                        userInput.ValidationMessages = viewModelValidator.Messages;
                        DispatchViewModel(userInput);
                        return null;
                    }

                    // GetEntities
                    Tone tone = _repositories.ToneRepository.Get(toneID);

                    var underlyingPatches = new List<Patch>(MainViewModel.Document.CurrentInstrument.List.Count);
                    foreach (IDAndName itemViewModel in MainViewModel.Document.CurrentInstrument.List)
                    {
                        Patch underlyingPatch = _repositories.PatchRepository.Get(itemViewModel.ID);
                        underlyingPatches.Add(underlyingPatch);
                    }

                    // Business
                    Outlet outlet = null;
                    if (underlyingPatches.Count != 0)
                    {
                        outlet = _autoPatcher.TryAutoPatchWithTone(tone, underlyingPatches);
                    }

                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }

                    if (outlet == null) // Fallback to Sine
                    {
                        Patch patch = _patchManager.CreatePatch();

                        var operatorFactory = new OperatorFactory(patch, _repositories);
                        double frequency = tone.GetFrequency();
                        outlet = operatorFactory.Sine(operatorFactory.PatchInlet(DimensionEnum.Frequency, frequency));
                    }

                    // ToViewModel
                    ToneGridEditViewModel viewModel = tone.Scale.ToToneGridEditViewModel();

                    // Non-Persisted
                    viewModel.OutletIDToPlay = outlet.ID;
                    viewModel.Visible = userInput.Visible;
                    viewModel.Successful = true;

                    return viewModel;
                });
        }

        // Helpers

        /// <summary>
        /// A template method for a MainPresenter action method,
        /// that will write to the entity model.
        /// 
        /// Works for most write actions. Less suitable for specialized cases:
        /// In particular the ones that are not about the open document.
        ///
        /// Executes a sub-presenter's action and surrounds it with:
        /// a) Converting the full document view model to entity.
        /// b) Doing a full document validation.
        /// c) Managing view model transactionality.
        /// d) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
        /// 
        /// All you need to do is provide the right sub-viewmodel,
        /// provide a delegate to the sub-presenter's action method
        /// and possibly do some refreshing of other view models afterwards.
        /// </summary>
        private TViewModel ExecuteWriteAction<TViewModel>(TViewModel userInput, Func<TViewModel> partialAction)
            where TViewModel : ViewModelBase
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            Document document = null;
            if (MainViewModel.Document.IsOpen)
            {
                document = MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }

            // Partial Action
            TViewModel viewModel = partialAction();
            if (!viewModel.Successful)
            {
                // DispatchViewModel
                DispatchViewModel(viewModel);
                return viewModel;
            }

            // Set !Successful
            viewModel.Successful = false;

            // Business
            if (MainViewModel.Document.IsOpen)
            {
                IResult validationResult = _documentManager.Save(document);
                if (!validationResult.Successful)
                {
                    // Non-Persisted
                    viewModel.ValidationMessages.AddRange(validationResult.Messages);

                    // DispatchViewModel
                    DispatchViewModel(viewModel);
                    return viewModel;
                }
            }

            // Successful
            viewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(viewModel);

            return viewModel;
        }

        /// <summary>
        /// A template method for a MainPresenter action method,
        /// that will read from the entity model, but not write to it.
        ///
        /// This version omits the full document validation and successful flags.
        /// 
        /// Executes a sub-presenter's action and surrounds it with:
        /// a) Converting the full document view model to entity.
        /// b) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
        /// 
        /// All you need to do is provide the right sub-viewmodel,
        /// provide a delegate to the sub-presenter's action method.
        /// </summary>
        /// <param name="viewModelToDispatch">
        /// Can be null if no view model is relevant. But if you have a relevant view model, please pass it along.
        /// </param>
        private void ExecuteReadAction(ViewModelBase viewModelToDispatch, Action partialAction)
        {
            // ToEntity
            if (MainViewModel.Document.IsOpen)
            {
                MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }

            // Partial Action
            partialAction();

            // DispatchViewModel
            if (viewModelToDispatch != null)
            {
                DispatchViewModel(viewModelToDispatch);
            }
        }

        /// <summary>
        /// A template method for a MainPresenter action method,
        /// that will read from the entity model, but not write to it.
        ///
        /// This version omits the full document validation and successful flags
        /// but allows the partial action to return a new view model.
        /// 
        /// Executes a sub-presenter's action and surrounds it with:
        /// a) Converting the full document view model to entity.
        /// b) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
        /// 
        /// All you need to do is provide the right sub-viewmodel,
        /// provide a delegate to the sub-presenter's action method.
        /// </summary>
        /// <param name="viewModelToDoNothingWith">
        /// You can pass null to it if you want.
        /// Or a specific view model that your action is about.
        /// This parameter is not used in this method.
        /// However, it is there for a very important reason.
        /// If it were not, you could by accident call the other overload
        /// of ExecuteReadAction and not use the return value of your delegate.
        /// By having this parameter, the overload that is called, is always
        /// only dependent on partialAction returning or not returning a view model.
        /// </param>
        // ReSharper disable once UnusedParameter.Local
        private void ExecuteReadAction<TViewModel>(TViewModel viewModelToDoNothingWith, Func<TViewModel> partialAction)
            where TViewModel : ViewModelBase
        {
            // ToEntity
            if (MainViewModel.Document.IsOpen)
            {
                MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }

            // Partial Action
            TViewModel viewModel = partialAction();

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        /// <summary>
        /// A template method for a MainPresenter action method,
        /// that will not read or write the entity model,
        /// but works with non-entity model data only.
        ///
        /// Most steps otherwise needed in for instance write actions are not needed.
        /// 
        /// Executes a sub-presenter's action and surrounds it with:
        /// a) Dispatching the view model (for instance needed to hide other view models if a new view model is displayed over it).
        /// 
        /// All you need to do is provide the right sub-viewmodel,
        /// provide a delegate to the sub-presenter's action method.
        /// </summary>
        private void ExecuteNonPersistedAction<TViewModel>(TViewModel viewModelToDispatch, Action partialAction)
            where TViewModel : ViewModelBase
        {
            if (viewModelToDispatch == null) throw new ArgumentNullException(nameof(viewModelToDispatch));

            // Partial Action
            partialAction();

            // DispatchViewModel
            DispatchViewModel(viewModelToDispatch);
        }
    }
}