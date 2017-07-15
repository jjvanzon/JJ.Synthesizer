using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Framework.Mathematics;
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
using JJ.Business.Synthesizer.Helpers;
// ReSharper disable InvertIf

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        // General Actions

        /// <param name="documentName">nullable</param>
        /// <param name="patchName">nullable</param>
        public void Show(string documentName, string patchName)
        {
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
            DocumentGridViewModel documentGridViewModel = _documentGridPresenter.Show(MainViewModel.DocumentGrid);
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
                ActionTemplateMethod(userInput, () => _documentOrPatchNotFoundPresenter.Show(userInput, documentName));
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
            Patch patch = document.Patches
                                  .Where(x => string.Equals(NameHelper.ToCanonical(x.Name), canonicalPatchName))
                                  .SingleWithClearException(new { canonicalPatchName });

            if (document == null || patch == null)
            {
                // GetUserInput
                DocumentOrPatchNotFoundPopupViewModel userInput = MainViewModel.DocumentOrPatchNotFound;

                // Template Method
                ActionTemplateMethod(userInput, () => _documentOrPatchNotFoundPresenter.Show(userInput, documentName, patchName));
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
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _audioFileOutputGridPresenter.Show(userInput));
        }

        public void AudioFileOutputGridClose()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _audioFileOutputGridPresenter.Close(userInput));
        }

        public void AudioFileOutputGridDelete(int id)
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Template Method
            AudioFileOutputGridViewModel viewModel = ActionTemplateMethod(userInput, () => _audioFileOutputGridPresenter.Delete(userInput, id));

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
            AudioFileOutputGridViewModel gridViewModel = ActionTemplateMethod(
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
            AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            AudioFileOutputPropertiesViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _audioFileOutputPropertiesPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleAudioFileOutputProperties = viewModel;
            }
        }

        public void AudioFileOutputPropertiesClose(int id)
        {
            // GetViewModel
            AudioFileOutputPropertiesViewModel userInput = ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            AudioFileOutputPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _audioFileOutputPropertiesPresenter.Close(userInput));

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
            AudioFileOutputPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _audioFileOutputPropertiesPresenter.Delete(userInput));

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
            AudioFileOutputPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _audioFileOutputPropertiesPresenter.LoseFocus(userInput));

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
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _audioOutputPropertiesPresenter.Show(userInput));
        }

        public void AudioOutputPropertiesClose()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _audioOutputPropertiesPresenter.Close(userInput));
        }

        public void AudioOutputPropertiesLoseFocus()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _audioOutputPropertiesPresenter.LoseFocus(userInput));
        }

        public void AudioOutputPropertiesPlay()
        {
            // NOTE:
            // Cannot use partial presenter, because this action uses both
            // AudioOutputProperties and CurrentInstrument view model.

            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // TemplateMethod
            ActionTemplateMethod(
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
                _repositories.DimensionRepository,
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

            ReadOnlyActionTemplateMethod(
                userInput,
                () =>
                {
                    // RefreshCounter
                    userInput.RefreshCounter++;
                    userInput.PatchDetails.RefreshCounter++;

                    // Set !Successful
                    userInput.Successful = false;

                    // Action
                    AutoPatchPopupViewModel viewModel = ToViewModelHelper.CreateEmptyAutoPatchViewModel();

                    // Non-Persisted
                    viewModel.RefreshCounter = userInput.RefreshCounter;
                    viewModel.PatchDetails.RefreshCounter = userInput.PatchDetails.RefreshCounter;

                    // Succesful
                    viewModel.Successful = true;

                    return viewModel;
                });
        }

        public void AutoPatchPopupSave()
        {
            AutoPatchPopupViewModel userInput = MainViewModel.Document.AutoPatchPopup;

            AutoPatchPopupViewModel viewModel = ActionTemplateMethod(
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
                            _repositories.DimensionRepository,
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

        public void AddToInstrument(int patchID)
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _currentInstrumentPresenter.Add(userInput, patchID));
        }

        public void CurrentInstrumentClose()
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _currentInstrumentPresenter.Close(userInput));
        }

        public void CurrentInstrumentMovePatch(int patchID, int newPosition)
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _currentInstrumentPresenter.Move(userInput, patchID, newPosition));
        }

        public void CurrentInstrumentShow()
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _currentInstrumentPresenter.Show(userInput));
        }

        public void CurrentInstrumentPlay()
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _currentInstrumentPresenter.Play(userInput));
        }

        public void RemoveFromInstrument(int patchID)
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _currentInstrumentPresenter.Remove(userInput, patchID));
        }

        // Curve

        public void CurveGridShow()
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _curveGridPresenter.Show(userInput));
        }

        public void CurveGridClose()
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _curveGridPresenter.Close(userInput));
        }

        public void CurveCreate()
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // Template Method
            CurveGridViewModel viewModel = ActionTemplateMethod(
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
                    _curveManager.Create(document);

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

        public void CurveGridDelete(int id)
        {
            // GetViewModel
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;

            // Template Method
            CurveGridViewModel viewModel = ActionTemplateMethod(userInput, () => _curveGridPresenter.Delete(userInput, id));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void CurveDetailsShow(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _curveDetailsPresenter.Show(userInput));
        }

        public void CurveDetailsClose(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _curveDetailsPresenter.Close(userInput));
        }

        public void CurveDetailsLoseFocus(int id)
        {
            // GetViewModel
            CurveDetailsViewModel userInput = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _curveDetailsPresenter.LoseFocus(userInput));
        }

        public void CurvePropertiesShow(int id)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = ViewModelSelector.GetCurvePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            CurvePropertiesViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _curvePropertiesPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleCurveProperties = viewModel;
            }
        }

        public void CurvePropertiesClose(int id)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = ViewModelSelector.GetCurvePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            CurvePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _curvePropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleCurveProperties = null;

                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void CurvePropertiesDelete(int id)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = ViewModelSelector.GetCurvePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            CurvePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _curvePropertiesPresenter.Delete(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void CurvePropertiesLoseFocus(int id)
        {
            // GetViewModel
            CurvePropertiesViewModel userInput = ViewModelSelector.GetCurvePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            CurvePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _curvePropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        // Document Grid

        public void DocumentCannotDeleteOK()
        {
            // GetViewModel
            DocumentCannotDeleteViewModel userInput = MainViewModel.DocumentCannotDelete;

            // Partial Action
            DocumentCannotDeleteViewModel viewModel = _documentCannotDeletePresenter.OK(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDeleteCancel()
        {
            // GetViewModel
            DocumentDeleteViewModel userInput = MainViewModel.DocumentDelete;

            // Partial Action
            DocumentDeleteViewModel viewModel = _documentDeletePresenter.Cancel(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentDeleteConfirm(int id)
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
            DocumentDeletedViewModel userInput = MainViewModel.DocumentDeleted;

            // Partial Action
            DocumentDeletedViewModel viewModel = _documentDeletedPresenter.OK(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
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
            DocumentDetailsViewModel userInput = MainViewModel.DocumentDetails;

            // Partial Action
            DocumentDetailsViewModel viewModel = _documentDetailsPresenter.Close(userInput);

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
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // Partial Action
            DocumentGridViewModel viewModel = _documentGridPresenter.Close(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        public void DocumentGridPlay(int id)
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _documentGridPresenter.Play(userInput, id));
        }

        public void DocumentGridShow()
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // Partial Action
            DocumentGridViewModel viewModel = _documentGridPresenter.Show(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
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
            // Business
            IList<Patch> grouplessPatches = PatchGrouper.GetGrouplessPatches(document.Patches, mustIncludeHidden: true);
            IList<PatchGroupDto> patchGroupDtos = PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(document.Patches, mustIncludeHidden: true);
            IList<UsedInDto<Curve>> curveUsedInDtos = _documentManager.GetUsedIn(document.Curves);
            IList<UsedInDto<Sample>> sampleUsedInDtos = _documentManager.GetUsedIn(document.Samples);
            IList<UsedInDto<Patch>> grouplessPatchUsedInDtos = _documentManager.GetUsedIn(grouplessPatches);
            IList<PatchGroupDto_WithUsedIn> patchGroupDtos_WithUsedIn = patchGroupDtos.Select(
                                                                                          x => new PatchGroupDto_WithUsedIn
                                                                                          {
                                                                                              GroupName = x.FriendlyGroupName,
                                                                                              PatchUsedInDtos = _documentManager.GetUsedIn(x.Patches)
                                                                                          })
                                                                                      .ToArray();
            IList<DocumentReferencePatchGroupDto> documentReferencePatchGroupDtos =
                PatchGrouper.GetDocumentReferencePatchGroupDtos_IncludingGrouplessIfAny(document.LowerDocumentReferences, mustIncludeHidden: false);

            // ToViewModel
            DocumentViewModel viewModel = document.ToViewModel(
                grouplessPatchUsedInDtos,
                patchGroupDtos_WithUsedIn,
                documentReferencePatchGroupDtos,
                curveUsedInDtos,
                sampleUsedInDtos,
                _repositories,
                _entityPositionManager);

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
            viewModel.CurveGrid.Successful = true;
            viewModel.CurvePropertiesDictionary.Values.ForEach(x => x.Successful = true);
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
            viewModel.PatchGridDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.PatchPropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.SampleGrid.Successful = true;
            viewModel.SamplePropertiesDictionary.Values.ForEach(x => x.Successful = true);
            viewModel.SamplePropertiesDictionary.Values.ForEach(x => x.Successful = true);
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
            ReadOnlyActionTemplateMethod(userInput, () => _documentOrPatchNotFoundPresenter.OK(userInput));
        }

        public void DocumentPropertiesShow()
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Template Method
            ReadOnlyActionTemplateMethod(userInput, () => _documentPropertiesPresenter.Show(userInput));
        }

        public void DocumentPropertiesClose() => DocumentPropertiesCloseOrLoseFocus(_documentPropertiesPresenter.Close);

        public void DocumentPropertiesLoseFocus() => DocumentPropertiesCloseOrLoseFocus(_documentPropertiesPresenter.LoseFocus);

        private void DocumentPropertiesCloseOrLoseFocus(Func<DocumentPropertiesViewModel, DocumentPropertiesViewModel> partialAction)
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Template Method
            DocumentPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => partialAction(userInput));

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
            ActionTemplateMethod(userInput, () => _documentPropertiesPresenter.Play(userInput));
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

        public void DocumentTreeClose() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.Close);

        public void DocumentTreeNew()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // TemplateMethod
            DocumentTreeViewModel viewModel = ActionTemplateMethod(userInput, func);

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }

            DocumentTreeViewModel func()
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

                        // Business
                        var operatorFactory = new OperatorFactory(patch, _repositories);
                        Operator op = operatorFactory.New(underlyingPatch, GetVariableInletOrOutletCount(underlyingPatch));
                        _autoPatcher.CreateNumbersForEmptyInletsWithDefaultValues(op, ESTIMATED_OPERATOR_WIDTH, OPERATOR_HEIGHT, _entityPositionManager);

                        // Successful
                        userInput.Successful = true;

                        // Do not do Tbother with ToViewModel. We will do a full Refresh later.
                        return userInput;

                    default:
                        throw new ValueNotSupportedException(userInput.SelectedNodeType);
                }
            }
        }

        public void DocumentTreeOpenItemExternally() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.OpenItemExternally);

        public void DocumentTreePlay()
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // TemplateMethod
            ActionTemplateMethod(userInput, func);

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

                    case DocumentTreeNodeTypeEnum.Samples:
                    {
                        // Business
                        Sample sample = Randomizer.TryGetRandomItem(document.Samples);
                        if (sample != null)
                        {
                            Patch patch = _patchManager.CreatePatch();

                            var x = new OperatorFactory(patch, _repositories);
                            Outlet outlet2 = x.Sample(sample);

                            VoidResult result2 = _patchManager.SavePatch(patch);

                            result = new Result<Outlet>
                            {
                                Successful = result2.Successful,
                                Messages = result2.Messages,
                                Data = outlet2
                            };
                        }
                        else
                        {
                            result = new Result<Outlet>
                            {
                                Successful = false,
                                Messages = new[] { ResourceFormatter.NoSoundFound }
                            };
                        }

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

                    // ReSharper disable once RedundantCaseLabel
                    case DocumentTreeNodeTypeEnum.AudioFileOutputList:
                    // ReSharper disable once RedundantCaseLabel
                    case DocumentTreeNodeTypeEnum.Curves:
                    // ReSharper disable once RedundantCaseLabel
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
                viewModel.Visible = userInput.Visible;
                viewModel.ValidationMessages.AddRange(result.Messages);
                viewModel.Successful = result.Successful;
                viewModel.OutletIDToPlay = outlet?.ID;
                viewModel.SelectedItemID = userInput.SelectedItemID;
                viewModel.SelectedNodeType = userInput.SelectedNodeType;
                viewModel.SelectedCanonicalPatchGroup = userInput.SelectedCanonicalPatchGroup;
                viewModel.SelectedPatchGroupLowerDocumentReferenceID = userInput.SelectedPatchGroupLowerDocumentReferenceID;
                viewModel.CanPlay = userInput.CanPlay;
                viewModel.CanCreateNew = userInput.CanCreateNew;

                return viewModel;

            }
        }

        public void DocumentTreeSelectAudioFileOutputs() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.SelectAudioFileOutputs);

        public void DocumentTreeSelectAudioOutput() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.SelectAudioOutput);

        public void DocumentTreeSelectCurves() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.SelectCurves);

        public void DocumentTreeSelectLibraries() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.SelectLibraries);

        public void DocumentTreeSelectLibrary(int id)
        {
            ReadOnlyDocumentTreeActionTemplate(x => _documentTreePresenter.SelectLibrary(x, id));
        }

        public void DocumentTreeSelectLibraryPatch(int id)
        {
            ReadOnlyDocumentTreeActionTemplate(x => _documentTreePresenter.SelectLibraryPatch(x, id));
        }

        public void DocumentTreeSelectLibraryPatchGroup(int lowerDocumentReferenceID, string patchGroup)
        {
            ReadOnlyDocumentTreeActionTemplate(x => _documentTreePresenter.SelectLibraryPatchGroup(x, lowerDocumentReferenceID, patchGroup));
        }

        public void DocumentTreeSelectSamples() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.SelectSamples);

        public void DocumentTreeSelectScales() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.SelectScales);

        public void DocumentTreeSelectPatch(int id)
        {
            ReadOnlyDocumentTreeActionTemplate(x => _documentTreePresenter.SelectPatch(x, id));
        }

        public void DocumentTreeSelectPatchGroup(string group)
        {
            ReadOnlyDocumentTreeActionTemplate(x => _documentTreePresenter.SelectPatchGroup(x, group));
        }

        public void DocumentTreeShow() => ReadOnlyDocumentTreeActionTemplate(_documentTreePresenter.Show);

        /// <summary>
        /// On top of the regular ReadOnlyActionTemplateMethod,
        /// will set CanCreateNew, which cannot be determined by entities or DocumentTreewieWModel alone.
        /// </summary>
        private void ReadOnlyDocumentTreeActionTemplate(Func<DocumentTreeViewModel, DocumentTreeViewModel> partialAction)
        {
            // GetViewModel
            DocumentTreeViewModel userInput = MainViewModel.Document.DocumentTree;

            // Template Method
            ReadOnlyActionTemplateMethod(
                userInput,
                () =>
                {
                    DocumentTreeViewModel viewModel = partialAction(userInput);
                    SetCanCreateNew(viewModel);
                    return viewModel;
                });
        }

        private void SetCanCreateNew(DocumentTreeViewModel viewModel)
        {
            bool patchDetailsVisible = MainViewModel.Document.VisiblePatchDetails != null;
            viewModel.CanCreateNew = ToViewModelHelper.GetCanCreateNew(viewModel.SelectedNodeType, patchDetailsVisible);
        }

        // Library

        public void LibraryAdd()
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // Template Method
            ActionTemplateMethod(userInput, () => _librarySelectionPopupPresenter.Show(userInput));
        }

        public void LibraryGridClose()
        {
            // GetViewModel
            LibraryGridViewModel userInput = MainViewModel.Document.LibraryGrid;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _libraryGridPresenter.Close(userInput));
        }

        public void LibraryGridRemove(int documentReferenceID)
        {
            // GetViewModel
            LibraryGridViewModel userInput = MainViewModel.Document.LibraryGrid;

            // Template Method
            LibraryGridViewModel viewModel = ActionTemplateMethod(userInput, () => _libraryGridPresenter.Remove(userInput, documentReferenceID));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void LibraryGridOpenItemExternally(int documentReferenceID)
        {
            // GetViewModel
            LibraryGridViewModel userInput = MainViewModel.Document.LibraryGrid;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _libraryGridPresenter.OpenItemExternally(userInput, documentReferenceID));
        }

        public void LibraryGridPlay(int documentReferenceID)
        {
            // GetViewModel
            LibraryGridViewModel userInput = MainViewModel.Document.LibraryGrid;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _libraryGridPresenter.Play(userInput, documentReferenceID));
        }

        public void LibraryGridShow()
        {
            // GetViewModel
            LibraryGridViewModel userInput = MainViewModel.Document.LibraryGrid;

            // TemplateMethod
            ReadOnlyActionTemplateMethod(userInput, () => _libraryGridPresenter.Show(userInput));
        }

        public void LibraryPropertiesClose(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // Template Method
            LibraryPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _libraryPropertiesPresenter.Close(userInput));

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
            LibraryPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _libraryPropertiesPresenter.LoseFocus(userInput));

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
            ActionTemplateMethod(userInput, () => _libraryPropertiesPresenter.OpenExternally(userInput));
        }

        public void LibraryPropertiesPlay(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _libraryPropertiesPresenter.Play(userInput));
        }

        public void LibraryPropertiesRemove(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // Template Method
            LibraryPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _libraryPropertiesPresenter.Remove(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void LibraryPropertiesShow(int documentReferenceID)
        {
            // GetViewModel
            LibraryPropertiesViewModel userInput = ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID);

            // Template Method
            ReadOnlyActionTemplateMethod(userInput, () => _libraryPropertiesPresenter.Show(userInput));
        }

        public void LibrarySelectionPopupCancel()
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // Template Method
            ReadOnlyActionTemplateMethod(userInput, () => _librarySelectionPopupPresenter.Cancel(userInput));
        }

        public void LibrarySelectionPopupOK(int? lowerDocumentID)
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // Template Method
            LibrarySelectionPopupViewModel viewModel = ActionTemplateMethod(userInput, () => _librarySelectionPopupPresenter.OK(userInput, lowerDocumentID));

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
            ActionTemplateMethod(userInput, () => _librarySelectionPopupPresenter.OpenItemExternally(userInput, lowerDocumentID));
        }

        public void LibrarySelectionPopupPlay(int lowerDocumentID)
        {
            // GetViewModel
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _librarySelectionPopupPresenter.Play(userInput, lowerDocumentID));
        }

        // LibraryPatch

        public void LibraryPatchGridClose(int lowerDocumentReferenceID, string group)
        {
            // GetViewModel
            LibraryPatchGridViewModel userInput = ViewModelSelector.GetLibraryPatchGridViewModel(MainViewModel.Document, lowerDocumentReferenceID, group);

            // Template Method
            LibraryPatchGridViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _libraryPatchGridPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleLibraryPatchGrid = null;
            }
        }

        public void LibraryPatchGridOpenItemExternally(int lowerDocumentReferenceID, string group, int patchID)
        {
            // GetViewModel
            LibraryPatchGridViewModel userInput = ViewModelSelector.GetLibraryPatchGridViewModel(MainViewModel.Document, lowerDocumentReferenceID, group);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _libraryPatchGridPresenter.OpenItemExternally(userInput, patchID));
        }

        public void LibraryPatchGridPlay(int lowerDocumentReferenceID, string group, int patchID)
        {
            // GetViewModel
            LibraryPatchGridViewModel userInput = ViewModelSelector.GetLibraryPatchGridViewModel(MainViewModel.Document, lowerDocumentReferenceID, group);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _libraryPatchGridPresenter.Play(userInput, patchID));
        }

        public void LibraryPatchGridShow(int lowerDocumentReferenceID, string group)
        {
            // GetViewModel
            LibraryPatchGridViewModel userInput = ViewModelSelector.GetLibraryPatchGridViewModel(MainViewModel.Document, lowerDocumentReferenceID, group);

            // Template Method
            LibraryPatchGridViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _libraryPatchGridPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleLibraryPatchGrid = viewModel;
            }
        }

        public void LibraryPatchPropertiesClose(int patchID)
        {
            // GetViewModel
            LibraryPatchPropertiesViewModel userInput = ViewModelSelector.GetLibraryPatchPropertiesViewModel(MainViewModel.Document, patchID);

            // Template Method
            LibraryPatchPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _libraryPatchPropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleLibraryPatchProperties = null;
            }
        }

        public void LibraryPatchPropertiesOpenExternally(int patchID)
        {
            // GetViewModel
            LibraryPatchPropertiesViewModel userInput = ViewModelSelector.GetLibraryPatchPropertiesViewModel(MainViewModel.Document, patchID);

            // Template Method
            ActionTemplateMethod(userInput, () => _libraryPatchPropertiesPresenter.OpenExternally(userInput));
        }

        public void LibraryPatchPropertiesPlay(int patchID)
        {
            // GetViewModel
            LibraryPatchPropertiesViewModel userInput = ViewModelSelector.GetLibraryPatchPropertiesViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _libraryPatchPropertiesPresenter.Play(userInput, _repositories));
        }

        public void LibraryPatchPropertiesShow(int patchID)
        {
            // GetViewModel
            LibraryPatchPropertiesViewModel userInput = ViewModelSelector.GetLibraryPatchPropertiesViewModel(MainViewModel.Document, patchID);

            // Template Method
            LibraryPatchPropertiesViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _libraryPatchPropertiesPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleLibraryPatchProperties = viewModel;
            }
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
            CurveDetailsViewModel viewModel = ActionTemplateMethod(
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
            CurveDetailsViewModel viewModel = ActionTemplateMethod(
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
            CurveDetailsViewModel viewModel = ActionTemplateMethod(
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
            CurveDetailsViewModel viewModel = ActionTemplateMethod(userInput, () => _curveDetailsPresenter.NodeMoved(userInput, nodeID, x, y));

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
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            NodePropertiesViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _nodePropertiesPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleNodeProperties = viewModel;
            }
        }

        public void NodePropertiesClose(int id)
        {
            // GetViewModel
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            NodePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _nodePropertiesPresenter.Close(userInput));

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
            NodePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _nodePropertiesPresenter.Delete(userInput));

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
            NodePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _nodePropertiesPresenter.LoseFocus(userInput));

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
            ReadOnlyActionTemplateMethod(userInput, () => _curveDetailsPresenter.SelectNode(userInput, nodeID));
        }

        // Operator

        public void OperatorChangeInputOutlet(int patchID, int inletID, int inputOutletID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _patchDetailsPresenter.ChangeInputOutlet(userInput, inletID, inputOutletID));
        }

        public void OperatorCreate(int patchID, int operatorTypeID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // TemplateMethod
            PatchDetailsViewModel viewModel = ActionTemplateMethod(userInput, () => _patchDetailsPresenter.CreateOperator(userInput, operatorTypeID));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorDeleteSelected(int patchID)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);

            // Template Method
            PatchDetailsViewModel viewModel = ActionTemplateMethod(userInput, () => _patchDetailsPresenter.DeleteOperator(userInput));

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
            ActionTemplateMethod(userInput, () => _patchDetailsPresenter.MoveOperator(userInput, operatorID, centerX, centerY));
        }

        public void OperatorPropertiesClose(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel userInput = ViewModelSelector.GetOperatorPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter.Close(userInput));

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
            OperatorPropertiesViewModel_ForCache viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForCache.Close(userInput));

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
            OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCurve viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForCurve.Close(userInput));

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
            OperatorPropertiesViewModel_ForInletsToDimension viewModel = ActionTemplateMethod(
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
            OperatorPropertiesViewModel_ForNumber viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForNumber.Close(userInput));

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
            OperatorPropertiesViewModel_ForPatchInlet viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.Close(userInput));

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
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.Close(userInput));

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
            OperatorPropertiesViewModel_ForSample viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForSample.Close(userInput));

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
                ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_WithInterpolation.Close(userInput));

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
            OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ActionTemplateMethod(
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
            OperatorPropertiesViewModelBase viewModel = ActionTemplateMethod(userInput, () => GetOperatorPropertiesPresenter(id).Delete(userInput)); 

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void OperatorPropertiesLoseFocus(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel userInput = ViewModelSelector.GetOperatorPropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter.LoseFocus(userInput));

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
            OperatorPropertiesViewModel_ForCache viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForCache.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                PatchDetails_RefreshOperator(userInput.ID);
            }
        }

        public void OperatorPropertiesLoseFocus_ForCurve(int id)
        {
            // GetViewModel
            OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, id);

            // TemplateMethod
            OperatorPropertiesViewModel_ForCurve viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForCurve.LoseFocus(userInput));

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
            OperatorPropertiesViewModel_ForInletsToDimension viewModel = ActionTemplateMethod(
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
            OperatorPropertiesViewModel_ForNumber viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForNumber.LoseFocus(userInput));

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
                ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.LoseFocus(userInput));

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
                ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForPatchOutlet.LoseFocus(userInput));

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
            OperatorPropertiesViewModel_ForSample viewModel = ActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForSample.LoseFocus(userInput));

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
            OperatorPropertiesViewModel_WithInterpolation viewModel = ActionTemplateMethod(
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
            OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ActionTemplateMethod(
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
            ActionTemplateMethod(userInput, () => GetOperatorPropertiesPresenter(id).Play(userInput));
        }

        public void OperatorPropertiesShow(int id)
        {
            // GetViewModel & Partial Action
            {
                OperatorPropertiesViewModel userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _operatorPropertiesPresenter.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCache userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_ForCache viewModel = ReadOnlyActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForCache.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_ForCache = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForCurve userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_ForCurve viewModel = ReadOnlyActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForCurve.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_ForCurve = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForInletsToDimension userInput =
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_ForInletsToDimension viewModel = ReadOnlyActionTemplateMethod(
                        userInput,
                        () => _operatorPropertiesPresenter_ForInletsToDimension.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForNumber userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_ForNumber viewModel = ReadOnlyActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForNumber.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_ForNumber = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchInlet userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_ForPatchInlet viewModel =
                        ReadOnlyActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForPatchInlet.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForPatchOutlet userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_ForPatchOutlet viewModel = ReadOnlyActionTemplateMethod(
                        userInput,
                        () => _operatorPropertiesPresenter_ForPatchOutlet.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_ForSample userInput = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_ForSample viewModel = ReadOnlyActionTemplateMethod(userInput, () => _operatorPropertiesPresenter_ForSample.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_ForSample = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithInterpolation userInput =
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_WithInterpolation viewModel = ReadOnlyActionTemplateMethod(
                        userInput,
                        () => _operatorPropertiesPresenter_WithInterpolation.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = viewModel;
                    }
                    return;
                }
            }
            {
                OperatorPropertiesViewModel_WithCollectionRecalculation userInput =
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, id);
                if (userInput != null)
                {
                    OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = ReadOnlyActionTemplateMethod(
                        userInput,
                        () => _operatorPropertiesPresenter_WithCollectionRecalculation.Show(userInput));
                    if (viewModel.Successful)
                    {
                        MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = viewModel;
                    }
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
            ReadOnlyActionTemplateMethod(userInput, () => _patchDetailsPresenter.SelectOperator(userInput, operatorID));
        }

        // Patch

        /// <param name="group">nullable</param>
        public void PatchCreate(string group)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel(MainViewModel.Document, group);

            int patchID = 0;

            // Template Method
            PatchGridViewModel viewModel = ActionTemplateMethod(
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

        public void PatchGridDelete(string group, int patchID)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel(MainViewModel.Document, group);

            // Template Method
            PatchGridViewModel viewModel = ActionTemplateMethod(userInput, () => _patchGridPresenter.Delete(userInput, patchID));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void PatchDetailsClose(int id)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            PatchDetailsViewModel viewModel = ActionTemplateMethod(userInput, () => _patchDetailsPresenter.Close(userInput));

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
            ActionTemplateMethod(userInput, () => _patchDetailsPresenter.LoseFocus(userInput));
        }

        public void PatchDetailsPlay(int id)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

            // Template Method
            ActionTemplateMethod(userInput, () => _patchDetailsPresenter.Play(userInput));
        }

        public void PatchDetailsShow(int id)
        {
            // GetViewModel
            PatchDetailsViewModel userInput = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);

            // TemplateMethod
            PatchDetailsViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _patchDetailsPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisiblePatchDetails = viewModel;
            }
        }

        public void PatchGridClose(string group)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel(MainViewModel.Document, group);

            // Template Method
            PatchGridViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _patchGridPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisiblePatchGrid = null;
            }
        }

        public void PatchGridPlay(string group, int patchID)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel(MainViewModel.Document, group);

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _patchGridPresenter.Play(userInput, patchID));
        }

        public void PatchGridShow(string group)
        {
            // GetViewModel
            PatchGridViewModel userInput = ViewModelSelector.GetPatchGridViewModel(MainViewModel.Document, group);

            // Template Method
            PatchGridViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _patchGridPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisiblePatchGrid = viewModel;
            }
        }

        public void PatchPropertiesClose(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            PatchPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _patchPropertiesPresenter.Close(userInput));

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
            PatchPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _patchPropertiesPresenter.Delete(userInput));

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
            ActionTemplateMethod(userInput, () => _patchPropertiesPresenter.ChangeHasDimension(userInput));
        }

        public void PatchPropertiesLoseFocus(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            PatchPropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _patchPropertiesPresenter.LoseFocus(userInput));

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
            ActionTemplateMethod(userInput, () => _patchPropertiesPresenter.Play(userInput));
        }

        public void PatchPropertiesShow(int id)
        {
            // GetViewModel
            PatchPropertiesViewModel userInput = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            PatchPropertiesViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _patchPropertiesPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisiblePatchProperties = viewModel;
            }
        }

        // Sample

        public void SampleCreate()
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            SampleGridViewModel viewModel = ActionTemplateMethod(
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
                    // ReSharper disable once UnusedVariable
                    Sample sample = _sampleManager.CreateSample(document);

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

        public void SampleGridDelete(int sampleID)
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            SampleGridViewModel viewModel = ActionTemplateMethod(userInput, () => _sampleGridPresenter.Delete(userInput, sampleID));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void SampleGridClose()
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            ReadOnlyActionTemplateMethod(userInput, () => _sampleGridPresenter.Close(userInput));
        }

        public void SampleGridPlay(int sampleID)
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // TemplateMethod
            ActionTemplateMethod(userInput, () => _sampleGridPresenter.Play(userInput, sampleID));
        }

        public void SampleGridShow()
        {
            // GetViewModel
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;

            // Template Method
            ReadOnlyActionTemplateMethod(userInput, () => _sampleGridPresenter.Show(userInput));
        }

        public void SamplePropertiesClose(int id)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = ViewModelSelector.GetSamplePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            SamplePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _samplePropertiesPresenter.Close(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleSampleProperties = null;

                // Refresh
                SampleGridRefresh();
                SampleLookupRefresh();
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.Sample);
            }
        }

        public void SamplePropertiesLoseFocus(int id)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = ViewModelSelector.GetSamplePropertiesViewModel(MainViewModel.Document, id);

            // TemplateMethod
            SamplePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _samplePropertiesPresenter.LoseFocus(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                SampleGridRefresh();
                SampleLookupRefresh();
                OperatorViewModels_OfType_Refresh(OperatorTypeEnum.Sample);
            }
        }

        public void SamplePropertiesPlay(int id)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = ViewModelSelector.GetSamplePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ActionTemplateMethod(userInput, () => _samplePropertiesPresenter.Play(userInput));
        }

        public void SamplePropertiesDelete(int id)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = ViewModelSelector.GetSamplePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            SamplePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _samplePropertiesPresenter.Delete(userInput));

            // Refresh
            if (viewModel.Successful)
            {
                DocumentViewModelRefresh();
            }
        }

        public void SamplePropertiesShow(int id)
        {
            // GetViewModel
            SamplePropertiesViewModel userInput = ViewModelSelector.GetSamplePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            SamplePropertiesViewModel viewModel = ReadOnlyActionTemplateMethod(userInput, () => _samplePropertiesPresenter.Show(userInput));

            if (viewModel.Successful)
            {
                MainViewModel.Document.VisibleSampleProperties = viewModel;
            }
        }

        // Scale

        public void ScaleGridShow()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ReadOnlyActionTemplateMethod(userInput, () => _scaleGridPresenter.Show(userInput));
        }

        public void ScaleGridClose()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ReadOnlyActionTemplateMethod(userInput, () => _scaleGridPresenter.Close(userInput));
        }

        public void ScaleCreate()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Template Method
            ScaleGridViewModel viewModel = ActionTemplateMethod(
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
            ScaleGridViewModel viewModel = ActionTemplateMethod(userInput, () => _scaleGridPresenter.Delete(userInput, id));

            if (viewModel.Successful)
            {
                // Refresh
                DocumentViewModelRefresh();
            }
        }

        public void ScaleShow(int id)
        {
            // GetViewModel
            ScalePropertiesViewModel userInput1 = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);
            ToneGridEditViewModel userInput2 = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID: id);

            // RefreshCounter
            userInput1.RefreshCounter++;
            userInput2.RefreshCounter++;

            // Set !Successful
            userInput1.Successful = false;
            userInput2.Successful = false;

            // ToEntity
            MainViewModel.ToEntityWithRelatedEntities(_repositories);

            // Partial Actions
            ScalePropertiesViewModel scalePropertiesViewModel = _scalePropertiesPresenter.Show(userInput1);
            if (!scalePropertiesViewModel.Successful)
            {
                DispatchViewModel(scalePropertiesViewModel);
                return;
            }

            ToneGridEditViewModel toneGridEditViewModel = _toneGridEditPresenter.Show(userInput2);
            if (!toneGridEditViewModel.Successful)
            {
                DispatchViewModel(toneGridEditViewModel);
                return;
            }
        
            // Set Successful
            scalePropertiesViewModel.Successful = true;
            toneGridEditViewModel.Successful = true;

            // DispatchViewModel
            DispatchViewModel(scalePropertiesViewModel);
            DispatchViewModel(toneGridEditViewModel);

            MainViewModel.Document.VisibleScaleProperties = scalePropertiesViewModel;
            MainViewModel.Document.VisibleToneGridEdit = toneGridEditViewModel;
        }

        public void ScalePropertiesClose(int id)
        {
            // Get ViewModel
            ScalePropertiesViewModel userInput = ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id);

            // Template Method
            ScalePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _scalePropertiesPresenter.Close(userInput));

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
            ScalePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _scalePropertiesPresenter.Delete(userInput));

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
            ScalePropertiesViewModel viewModel = ActionTemplateMethod(userInput, () => _scalePropertiesPresenter.LoseFocus(userInput));

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
            ToneGridEditViewModel viewModel = ActionTemplateMethod(
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
            ActionTemplateMethod(
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
            ToneGridEditViewModel viewModel = ActionTemplateMethod(userInput, () => _toneGridEditPresenter.Close(userInput));

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
            ActionTemplateMethod(userInput, () => _toneGridEditPresenter.LoseFocus(userInput));
        }

        public void ToneGridEditEdit(int scaleID)
        {
            // GetViewModel
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            ActionTemplateMethod(userInput, () => _toneGridEditPresenter.Edit(userInput));
        }

        public void TonePlay(int scaleID, int toneID)
        {
            // NOTE:
            // Cannot use partial presenter, because this action uses both
            // ToneGridEditViewModel and CurrentInstrument view model.

            // GetEntity
            ToneGridEditViewModel userInput = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);

            // Template Method
            ActionTemplateMethod(
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
        /// A template method for a MainPresenter action method.
        /// Works for most actions. Less suitable for specialized cases:
        /// In particular the ones that are not about the open document.
        ///
        /// Executes a sub-presenter's action and surrounds it with
        /// converting the full document view model to entity,
        /// doing a full document validation and
        /// managing view model transactionality.
        /// All you need to do is provide the right sub-viewmodel,
        /// provide a delegate to the sub-presenter's action method
        /// and possibly do some refreshing of other view models afterwards.
        /// </summary>
        private TViewModel ActionTemplateMethod<TViewModel>(TViewModel userInput, Func<TViewModel> partialAction)
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

        /// <summary> See summary of TemplateActionMethod. This version omits the full document validation. </summary>
        private TViewModel ReadOnlyActionTemplateMethod<TViewModel>(TViewModel userInput, Func<TViewModel> partialAction)
            where TViewModel : ViewModelBase
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // ToEntity
            if (MainViewModel.Document.IsOpen)
            {
                MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }

            // Partial Action
            TViewModel viewModel = partialAction();

            // DispatchViewModel
            DispatchViewModel(viewModel);

            return viewModel;
        }
    }
}