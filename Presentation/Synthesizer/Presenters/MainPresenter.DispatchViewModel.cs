using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Presentation;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Framework.Collections;

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
        private void DispatchViewModel(ViewModelBase viewModel2)
        {
            if (viewModel2 == null) throw new NullException(() => viewModel2);

            Type viewModelType = viewModel2.GetType();

            if (!_dispatchDelegateDictionary.TryGetValue(viewModelType, out Action<ViewModelBase> dispatchDelegate))
            {
                throw new UnexpectedViewModelTypeException(viewModel2);
            }

            dispatchDelegate(viewModel2);
        }

        private void DispatchAudioFileOutputGridViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (AudioFileOutputGridViewModel)viewModel2;

            MainViewModel.Document.AudioFileOutputGrid = (AudioFileOutputGridViewModel)viewModel2;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchAudioFileOutputPropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (AudioFileOutputPropertiesViewModel)viewModel2;

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

        private void DispatchAudioOutputPropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (AudioOutputPropertiesViewModel)viewModel2;

            MainViewModel.Document.AudioOutputProperties = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        /// <summary> Not called through DispatchViewModel. Can only be called explicitly. </summary>
        private void DispatchAutoPatchDetailsViewModel(PatchDetailsViewModel detailsViewModel)
        {
            MainViewModel.Document.AutoPatchDetails = detailsViewModel;

            MainViewModel.ValidationMessages.AddRange(detailsViewModel.ValidationMessages);
            detailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurrentInstrumentViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (CurrentInstrumentViewModel)viewModel2;

            MainViewModel.Document.CurrentInstrument = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchCurveDetailsViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (CurveDetailsViewModel)viewModel2;

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

        private void DispatchCurveGridViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (CurveGridViewModel)viewModel2;

            MainViewModel.Document.CurveGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchCurvePropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (CurvePropertiesViewModel)viewModel2;

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

        private void DispatchDocumentCannotDeleteViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (DocumentCannotDeleteViewModel)viewModel2;

            MainViewModel.DocumentCannotDelete = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentDeletedViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (DocumentDeletedViewModel)viewModel2;

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

        private void DispatchDocumentDeleteViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (DocumentDeleteViewModel)viewModel2;
            MainViewModel.DocumentDelete = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentDetailsViewModel(ViewModelBase viewModel2)
        {
            var documentDetailsViewModel = (DocumentDetailsViewModel)viewModel2;

            MainViewModel.DocumentDetails = documentDetailsViewModel;

            if (documentDetailsViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                documentDetailsViewModel.Visible = true;
            }

            DispatchViewModelBase(documentDetailsViewModel);
        }

        private void DispatchDocumentGridViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (DocumentGridViewModel)viewModel2;

            MainViewModel.DocumentGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentPropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (DocumentPropertiesViewModel)viewModel2;

            MainViewModel.Document.DocumentProperties = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchDocumentTreeViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (DocumentTreeViewModel)viewModel2;
            MainViewModel.Document.DocumentTree = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibraryGridViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (LibraryGridViewModel)viewModel2;

            MainViewModel.Document.LibraryGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibraryPatchPropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (LibraryPatchPropertiesViewModel)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.LibraryPatchPropertiesDictionary;
            dictionary[castedViewModel.PatchID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleLibraryPatchProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibraryPropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (LibraryPropertiesViewModel)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.LibraryPropertiesDictionary;
            dictionary[castedViewModel.DocumentReferenceID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleLibraryProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchLibrarySelectionPopupViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (LibrarySelectionPopupViewModel)viewModel2;

            MainViewModel.Document.LibrarySelectionPopup = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchMenuViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (MenuViewModel)viewModel2;
            MainViewModel.Menu = castedViewModel;

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchNodePropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (NodePropertiesViewModel)viewModel2;

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

        private void DispatchOperatorPropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForCache(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCache)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCaches;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCache = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForCurve(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCurve)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCurves;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCurve = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForCustomOperator(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCustomOperator)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCustomOperator = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForInletsToDimension(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForInletsToDimension)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForInletsToDimension;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForNumber(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForNumber)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForNumber = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchInlet(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchInlet)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchOutlet(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchOutlet)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_ForSample(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForSample)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForSamples;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForSample = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithInterpolation(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithInterpolation)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInterpolation;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithCollectionRecalculation(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithCollectionRecalculation)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithCollectionRecalculation;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithOutletCount(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithOutletCount)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithOutletCount;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithOutletCount = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchOperatorPropertiesViewModel_WithInletCount(ViewModelBase viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithInletCount)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInletCount;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithInletCount = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchPatchDetailsViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (PatchDetailsViewModel)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.PatchDetailsDictionary;
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisiblePatchDetails = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchPatchGridViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (PatchGridViewModel)viewModel2;

            string key = NameHelper.ToCanonical(castedViewModel.Group);

            MainViewModel.Document.PatchGridDictionary[key] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisiblePatchGrid = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchPatchPropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (PatchPropertiesViewModel)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.PatchPropertiesDictionary;
            dictionary[castedViewModel.ID]= castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisiblePatchProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchSampleGridViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (SampleGridViewModel)viewModel2;

            MainViewModel.Document.SampleGrid = castedViewModel;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchSamplePropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (SamplePropertiesViewModel)viewModel2;

            // ReSharper disable once SuggestVarOrType_Elsewhere
            var dictionary = MainViewModel.Document.SamplePropertiesDictionary;
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleSampleProperties = castedViewModel;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchScaleGridViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (ScaleGridViewModel)viewModel2;

            MainViewModel.Document.ScaleGrid = (ScaleGridViewModel)viewModel2;

            // ReSharper disable once InvertIf
            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            DispatchViewModelBase(castedViewModel);
        }

        private void DispatchScalePropertiesViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (ScalePropertiesViewModel)viewModel2;

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

        private void DispatchToneGridEditViewModel(ViewModelBase viewModel2)
        {
            var castedViewModel = (ToneGridEditViewModel)viewModel2;

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
