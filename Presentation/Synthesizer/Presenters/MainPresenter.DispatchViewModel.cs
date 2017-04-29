using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Framework.Collections;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        private readonly Dictionary<Type, Action<ViewModelBase>> _dispatchDelegateDictionary;

        private Dictionary<Type, Action<ViewModelBase>> CreateDispatchDelegateDictionary()
        {
            var dictionary = new Dictionary<Type, Action<ViewModelBase>>
            {
                { typeof(AudioFileOutputGridViewModel), DispatchAudioFileOutputGridViewModel },
                { typeof(AudioFileOutputPropertiesViewModel), DispatchAudioFileOutputPropertiesViewModel },
                { typeof(AudioOutputPropertiesViewModel), DispatchAudioOutputPropertiesViewModel },
                { typeof(AutoPatchPopupViewModel), DispatchAutoPatchViewModel },
                { typeof(CurrentInstrumentViewModel), DispatchCurrentInstrumentViewModel },
                { typeof(CurveDetailsViewModel), DispatchCurveDetailsViewModel },
                { typeof(CurveGridViewModel), DispatchCurveGridViewModel },
                { typeof(CurvePropertiesViewModel), DispatchCurvePropertiesViewModel },
                { typeof(DocumentCannotDeleteViewModel), DispatchDocumentCannotDeleteViewModel },
                { typeof(DocumentDeletedViewModel), DispatchDocumentDeletedViewModel },
                { typeof(DocumentDeleteViewModel), DispatchDocumentDeleteViewModel },
                { typeof(DocumentDetailsViewModel), DispatchDocumentDetailsViewModel },
                { typeof(DocumentGridViewModel), DispatchDocumentGridViewModel },
                { typeof(DocumentPropertiesViewModel), DispatchDocumentPropertiesViewModel },
                { typeof(DocumentTreeViewModel), DispatchDocumentTreeViewModel },
                { typeof(LibraryGridViewModel), DispatchLibraryGridViewModel },
                { typeof(LibraryPatchPropertiesViewModel), DispatchLibraryPatchPropertiesViewModel },
                { typeof(LibraryPropertiesViewModel), DispatchLibraryPropertiesViewModel },
                { typeof(LibrarySelectionPopupViewModel), DispatchLibrarySelectionPopupViewModel },
                { typeof(MenuViewModel), DispatchMenuViewModel },
                { typeof(NodePropertiesViewModel), DispatchNodePropertiesViewModel },
                { typeof(OperatorPropertiesViewModel), DispatchOperatorPropertiesViewModel },
                { typeof(OperatorPropertiesViewModel_ForCache), DispatchOperatorPropertiesViewModel_ForCache },
                { typeof(OperatorPropertiesViewModel_ForCurve), DispatchOperatorPropertiesViewModel_ForCurve },
                { typeof(OperatorPropertiesViewModel_ForCustomOperator), DispatchOperatorPropertiesViewModel_ForCustomOperator },
                { typeof(OperatorPropertiesViewModel_ForInletsToDimension), DispatchOperatorPropertiesViewModel_ForInletsToDimension },
                { typeof(OperatorPropertiesViewModel_ForNumber), DispatchOperatorPropertiesViewModel_ForNumber },
                { typeof(OperatorPropertiesViewModel_ForPatchInlet), DispatchOperatorPropertiesViewModel_ForPatchInlet },
                { typeof(OperatorPropertiesViewModel_ForPatchOutlet), DispatchOperatorPropertiesViewModel_ForPatchOutlet },
                { typeof(OperatorPropertiesViewModel_ForSample), DispatchOperatorPropertiesViewModel_ForSample },
                { typeof(OperatorPropertiesViewModel_WithInterpolation), DispatchOperatorPropertiesViewModel_WithInterpolation },
                { typeof(OperatorPropertiesViewModel_WithCollectionRecalculation), DispatchOperatorPropertiesViewModel_WithCollectionRecalculation },
                { typeof(OperatorPropertiesViewModel_WithOutletCount), DispatchOperatorPropertiesViewModel_WithOutletCount },
                { typeof(OperatorPropertiesViewModel_WithInletCount), DispatchOperatorPropertiesViewModel_WithInletCount },
                { typeof(PatchDetailsViewModel), DispatchPatchDetailsViewModel },
                { typeof(PatchGridViewModel), DispatchPatchGridViewModel },
                { typeof(PatchPropertiesViewModel), DispatchPatchPropertiesViewModel },
                { typeof(SampleGridViewModel), DispatchSampleGridViewModel },
                { typeof(SamplePropertiesViewModel), DispatchSamplePropertiesViewModel },
                { typeof(ScaleGridViewModel), DispatchScaleGridViewModel },
                { typeof(ScalePropertiesViewModel), DispatchScalePropertiesViewModel },
                { typeof(ToneGridEditViewModel), DispatchToneGridEditViewModel },
            };

            return dictionary;
        }

        /// <summary>
        /// Closes the deal with regards to the action method,
        /// combining a temporary partial view model with the MainViewModel.
        /// Applies it in the right way to the MainViewModel. 
        /// This means assigning a partial ViewModel to a property of the MainViewModel,
        /// but also for instance yielding over the validation message from a partial
        /// ViewModel to the MainViewModel, and showing and hiding views currently not on the foreground.
        /// </summary>
        private void DispatchViewModel(ViewModelBase viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Type viewModelType = viewModel.GetType();

            if (!_dispatchDelegateDictionary.TryGetValue(viewModelType, out Action<ViewModelBase> dispatchDelegate))
            {
                throw new UnexpectedViewModelTypeException(viewModel);
            }

            dispatchDelegate(viewModel);
        }

        private void DispatchAudioFileOutputGridViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (AudioFileOutputGridViewModel)viewModel;

            MainViewModel.Document.AudioFileOutputGrid = (AudioFileOutputGridViewModel)viewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchAudioFileOutputPropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (AudioFileOutputPropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.AudioFileOutputPropertiesDictionary;
            int id = castedViewModel.Entity.ID;

            dictionary[id] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleAudioFileOutputProperties = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchAudioOutputPropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (AudioOutputPropertiesViewModel)viewModel;

            MainViewModel.Document.AudioOutputProperties = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchAutoPatchViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (AutoPatchPopupViewModel)viewModel;

            MainViewModel.Document.AutoPatchPopup = castedViewModel;

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurrentInstrumentViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (CurrentInstrumentViewModel)viewModel;

            MainViewModel.Document.CurrentInstrument = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchCurveDetailsViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (CurveDetailsViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.CurveDetailsDictionary;
            dictionary[castedViewModel.CurveID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleCurveDetails = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchCurveGridViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (CurveGridViewModel)viewModel;

            MainViewModel.Document.CurveGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchCurvePropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (CurvePropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.CurvePropertiesDictionary;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleCurveProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentCannotDeleteViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (DocumentCannotDeleteViewModel)viewModel;

            MainViewModel.DocumentCannotDelete = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentDeletedViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (DocumentDeletedViewModel)viewModel;

            MainViewModel.DocumentDeleted = castedViewModel;

            // TODO: This is quite an assumption.
            MainViewModel.DocumentDelete.Visible = false;
            MainViewModel.DocumentDetails.Visible = false;

            if (!castedViewModel.Visible)
            {
                // Also: this might better be done in the action method.
                DocumentGridRefresh();
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentDeleteViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (DocumentDeleteViewModel)viewModel;
            MainViewModel.DocumentDelete = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentDetailsViewModel(ViewModelBase viewModel)
        {
            var documentDetailsViewModel = (DocumentDetailsViewModel)viewModel;

            MainViewModel.DocumentDetails = documentDetailsViewModel;

            if (documentDetailsViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                documentDetailsViewModel.Visible = true;
            }

            DispatchViewModelBase(documentDetailsViewModel);
        }

        private void DispatchDocumentGridViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (DocumentGridViewModel)viewModel;

            MainViewModel.DocumentGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentPropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (DocumentPropertiesViewModel)viewModel;

            MainViewModel.Document.DocumentProperties = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentTreeViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (DocumentTreeViewModel)viewModel;
            MainViewModel.Document.DocumentTree = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibraryGridViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (LibraryGridViewModel)viewModel;

            MainViewModel.Document.LibraryGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibraryPatchPropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (LibraryPatchPropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.LibraryPatchPropertiesDictionary;
            dictionary[castedViewModel.PatchID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleLibraryPatchProperties = castedViewModel;
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibraryPropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (LibraryPropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.LibraryPropertiesDictionary;
            dictionary[castedViewModel.DocumentReferenceID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleLibraryProperties = castedViewModel;
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibrarySelectionPopupViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (LibrarySelectionPopupViewModel)viewModel;

            MainViewModel.Document.LibrarySelectionPopup = castedViewModel;

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchMenuViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (MenuViewModel)viewModel;
            MainViewModel.Menu = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchNodePropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (NodePropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = ViewModelSelector.GetNodePropertiesViewModelDictionary_ByCurveID(MainViewModel.Document, castedViewModel.CurveID);
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleNodeProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForCache(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCache)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCaches;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCache = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForCurve(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCurve)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCurves;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCurve = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForCustomOperator(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCustomOperator)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCustomOperator = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForInletsToDimension(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForInletsToDimension)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForInletsToDimension;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForNumber(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForNumber)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForNumber = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchInlet(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchInlet)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchOutlet(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchOutlet)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForSample(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForSample)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForSamples;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForSample = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithInterpolation(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithInterpolation)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInterpolation;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithCollectionRecalculation(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithCollectionRecalculation)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithCollectionRecalculation;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithOutletCount(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithOutletCount)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithOutletCount;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithOutletCount = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithInletCount(ViewModelBase viewModel)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithInletCount)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInletCount;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithInletCount = castedViewModel;
            }

            DispatchOperatorPropertiesViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModelBase(OperatorPropertiesViewModelBase viewModel)
        {
            if (viewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = viewModel.OutletIDToPlay;
                viewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(viewModel);
        }

        private void DispatchPatchDetailsViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (PatchDetailsViewModel)viewModel;

            // Try Dispatch to AutoPatchPopupViewModel
            if (MainViewModel.Document.AutoPatchPopup?.PatchDetails.Entity.ID == castedViewModel.Entity.ID)
            {
                MainViewModel.Document.AutoPatchPopup.PatchDetails = castedViewModel;
            }
            else
            {
                // Otherwise dispatch to DocumentViewModel
                // ReSharper disable once SuggestVarOrType_Elsewhere
                var dictionary = MainViewModel.Document.PatchDetailsDictionary;
                dictionary[castedViewModel.Entity.ID] = castedViewModel;

                if (castedViewModel.Visible)
                {
                    HideAllGridAndDetailViewModels();
                    castedViewModel.Visible = true;
                    MainViewModel.Document.VisiblePatchDetails = castedViewModel;
                }
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchPatchGridViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (PatchGridViewModel)viewModel;

            string key = NameHelper.ToCanonical(castedViewModel.Group);

            MainViewModel.Document.PatchGridDictionary[key] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisiblePatchGrid = castedViewModel;
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchPatchPropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (PatchPropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.PatchPropertiesDictionary;
            dictionary[castedViewModel.ID]= castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisiblePatchProperties = castedViewModel;
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchSampleGridViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (SampleGridViewModel)viewModel;

            MainViewModel.Document.SampleGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchSamplePropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (SamplePropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.SamplePropertiesDictionary;
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleSampleProperties = castedViewModel;
            }

            if (castedViewModel.OutletIDToPlay.HasValue)
            {
                MainViewModel.Document.OutletIDToPlay = castedViewModel.OutletIDToPlay;
                castedViewModel.OutletIDToPlay = null;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchScaleGridViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (ScaleGridViewModel)viewModel;

            MainViewModel.Document.ScaleGrid = (ScaleGridViewModel)viewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchScalePropertiesViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (ScalePropertiesViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.ScalePropertiesDictionary;
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleScaleProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchToneGridEditViewModel(ViewModelBase viewModel)
        {
            var castedViewModel = (ToneGridEditViewModel)viewModel;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.ToneGridEditDictionary;
            dictionary[castedViewModel.ScaleID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleToneGridEdit = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchViewModelBase(ViewModelBase castedViewModel)
        {
            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }
    }
}
