using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        private Dictionary<Type, Action<object>> _dispatchDelegateDictionary;

        private Dictionary<Type, Action<object>> CreateDispatchDelegateDictionary()
        {
            var dictionary = new Dictionary<Type, Action<object>>
            {
                { typeof(AudioFileOutputGridViewModel), DispatchAudioFileOutputGridViewModel },
                { typeof(AudioFileOutputPropertiesViewModel), DispatchAudioFileOutputPropertiesViewModel },
                { typeof(AudioOutputPropertiesViewModel), DispatchAudioOutputPropertiesViewModel },
                { typeof(CurrentPatchesViewModel), DispatchCurrentPatchesViewModel },
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
                { typeof(MenuViewModel), DispatchMenuViewModel },
                { typeof(NodePropertiesViewModel), DispatchNodePropertiesViewModel },
                { typeof(OperatorPropertiesViewModel), DispatchOperatorPropertiesViewModel },
                { typeof(OperatorPropertiesViewModel_ForBundle), DispatchOperatorPropertiesViewModel_ForBundle },
                { typeof(OperatorPropertiesViewModel_ForCache), DispatchOperatorPropertiesViewModel_ForCache },
                { typeof(OperatorPropertiesViewModel_ForCurve), DispatchOperatorPropertiesViewModel_ForCurve },
                { typeof(OperatorPropertiesViewModel_ForCustomOperator), DispatchOperatorPropertiesViewModel_ForCustomOperator },
                { typeof(OperatorPropertiesViewModel_ForMakeContinuous), DispatchOperatorPropertiesViewModel_ForMakeContinuous },
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
        private void DispatchViewModel(object viewModel2)
        {
            if (viewModel2 == null) throw new NullException(() => viewModel2);

            Type viewModelType = viewModel2.GetType();

            Action<object> dispatchDelegate;
            if (!_dispatchDelegateDictionary.TryGetValue(viewModelType, out dispatchDelegate))
            {
                throw new UnexpectedViewModelTypeException(viewModel2);
            }

            dispatchDelegate(viewModel2);
        }

        private void DispatchAudioFileOutputGridViewModel(object viewModel2)
        {
            var castedViewModel = (AudioFileOutputGridViewModel)viewModel2;

            MainViewModel.Document.AudioFileOutputGrid = (AudioFileOutputGridViewModel)viewModel2;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchAudioFileOutputPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (AudioFileOutputPropertiesViewModel)viewModel2;

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

        private void DispatchAudioOutputPropertiesViewModel(object viewModel2)
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

        private void DispatchCurrentPatchesViewModel(object viewModel2)
        {
            var castedViewModel = (CurrentPatchesViewModel)viewModel2;

            MainViewModel.Document.CurrentPatches = castedViewModel;

            MainViewModel.ValidationMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveDetailsViewModel(object viewModel2)
        {
            var castedViewModel = (CurveDetailsViewModel)viewModel2;

            var dictionary = MainViewModel.Document.CurveDetailsDictionary;
            dictionary[castedViewModel.CurveID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleCurveDetails = castedViewModel;
            }

            MainViewModel.ValidationMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveGridViewModel(object viewModel2)
        {
            var castedViewModel = (CurveGridViewModel)viewModel2;

            MainViewModel.Document.CurveGrid = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchCurvePropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (CurvePropertiesViewModel)viewModel2;

            var dictionary = MainViewModel.Document.CurvePropertiesDictionary;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleCurveProperties = castedViewModel;
            }

            MainViewModel.ValidationMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchDocumentCannotDeleteViewModel(object viewModel2)
        {
            var castedViewModel = (DocumentCannotDeleteViewModel)viewModel2;

            MainViewModel.DocumentCannotDelete = castedViewModel;
        }

        private void DispatchDocumentDeletedViewModel(object viewModel2)
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
        }

        private void DispatchDocumentDeleteViewModel(object viewModel2)
        {
            var castedViewModel = (DocumentDeleteViewModel)viewModel2;
            MainViewModel.DocumentDelete = castedViewModel;
        }

        private void DispatchDocumentDetailsViewModel(object viewModel2)
        {
            var documentDetailsViewModel = (DocumentDetailsViewModel)viewModel2;

            MainViewModel.DocumentDetails = documentDetailsViewModel;

            if (documentDetailsViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                documentDetailsViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(documentDetailsViewModel.ValidationMessages);
            documentDetailsViewModel.ValidationMessages.Clear();
        }

        private void DispatchDocumentGridViewModel(object viewModel2)
        {
            var castedViewModel = (DocumentGridViewModel)viewModel2;

            MainViewModel.DocumentGrid = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchDocumentPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (DocumentPropertiesViewModel)viewModel2;

            MainViewModel.Document.DocumentProperties = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchDocumentTreeViewModel(object viewModel2)
        {
            var castedViewModel = (DocumentTreeViewModel)viewModel2;
            MainViewModel.Document.DocumentTree = castedViewModel;
        }

        private void DispatchMenuViewModel(object viewModel2)
        {
            var castedViewModel = (MenuViewModel)viewModel2;
            MainViewModel.Menu = castedViewModel;
        }

        private void DispatchNodePropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (NodePropertiesViewModel)viewModel2;

            var list = ViewModelSelector.GetNodePropertiesViewModelDictionary_ByCurveID(MainViewModel.Document, castedViewModel.CurveID);
            list[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleNodeProperties = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForBundle(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForBundle)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForBundles;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForBundle = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForCache(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCache)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCaches;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCache = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForCurve(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCurve)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCurves;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCurve = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForCustomOperator(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCustomOperator)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForCustomOperator = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForMakeContinuous(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForMakeContinuous)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForMakeContinuous;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForMakeContinuous = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForNumber(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForNumber)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForNumber = castedViewModel;
            }
            
            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchInlet(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchInlet)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchOutlet(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchOutlet)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForSample(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForSample)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForSamples;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_ForSample = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_WithInterpolation(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithInterpolation)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInterpolation;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_WithCollectionRecalculation(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithCollectionRecalculation)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithCollectionRecalculation;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_WithOutletCount(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithOutletCount)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithOutletCount;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithOutletCount = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_WithInletCount(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_WithInletCount)viewModel2;

            var dictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInletCount;
            dictionary[castedViewModel.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleOperatorProperties_WithInletCount = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchDetailsViewModel(object viewModel2)
        {
            var castedViewModel = (PatchDetailsViewModel)viewModel2;

            var dictionary = MainViewModel.Document.PatchDetailsDictionary;
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisiblePatchDetails = castedViewModel;
            }

            MainViewModel.ValidationMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchGridViewModel(object viewModel2)
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

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (PatchPropertiesViewModel)viewModel2;

            var dictionary = MainViewModel.Document.PatchPropertiesDictionary;
            dictionary[castedViewModel.ID]= castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisiblePatchProperties = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchSampleGridViewModel(object viewModel2)
        {
            var castedViewModel = (SampleGridViewModel)viewModel2;

            MainViewModel.Document.SampleGrid = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchSamplePropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (SamplePropertiesViewModel)viewModel2;

            var dictionary = MainViewModel.Document.SamplePropertiesDictionary;
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleSampleProperties = castedViewModel;
            }

            MainViewModel.ValidationMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchScaleGridViewModel(object viewModel2)
        {
            var castedViewModel = (ScaleGridViewModel)viewModel2;

            MainViewModel.Document.ScaleGrid = (ScaleGridViewModel)viewModel2;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchScalePropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (ScalePropertiesViewModel)viewModel2;

            var dictionary = MainViewModel.Document.ScalePropertiesDictionary;
            dictionary[castedViewModel.Entity.ID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleScaleProperties = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchToneGridEditViewModel(object viewModel2)
        {
            var castedViewModel = (ToneGridEditViewModel)viewModel2;

            var dictionary = MainViewModel.Document.ToneGridEditDictionary;
            dictionary[castedViewModel.ScaleID] = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllGridAndDetailViewModels();
                castedViewModel.Visible = true;
                MainViewModel.Document.VisibleToneGridEdit = castedViewModel;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }
    }
}
