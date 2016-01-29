using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Common;
using JJ.Framework.Presentation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
                { typeof(NotFoundViewModel), DispatchNotFoundViewModel },
                { typeof(OperatorPropertiesViewModel), DispatchOperatorPropertiesViewModel },
                { typeof(OperatorPropertiesViewModel_ForBundle), DispatchOperatorPropertiesViewModel_ForBundle },
                { typeof(OperatorPropertiesViewModel_ForCurve), DispatchOperatorPropertiesViewModel_ForCurve },
                { typeof(OperatorPropertiesViewModel_ForCustomOperator), DispatchOperatorPropertiesViewModel_ForCustomOperator },
                { typeof(OperatorPropertiesViewModel_ForNumber), DispatchOperatorPropertiesViewModel_ForNumber },
                { typeof(OperatorPropertiesViewModel_ForPatchInlet), DispatchOperatorPropertiesViewModel_ForPatchInlet },
                { typeof(OperatorPropertiesViewModel_ForPatchOutlet), DispatchOperatorPropertiesViewModel_ForPatchOutlet },
                { typeof(OperatorPropertiesViewModel_ForSample), DispatchOperatorPropertiesViewModel_ForSample },
                { typeof(OperatorPropertiesViewModel_ForSpectrum), DispatchOperatorPropertiesViewModel_ForSpectrum },
                { typeof(OperatorPropertiesViewModel_ForUnbundle), DispatchOperatorPropertiesViewModel_ForUnbundle },
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
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchAudioFileOutputPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (AudioFileOutputPropertiesViewModel)viewModel2;

            var list = MainViewModel.Document.AudioFileOutputPropertiesList;
            int? listIndex = list.TryGetIndexOf(x => x.Entity.ID == castedViewModel.Entity.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurrentPatchesViewModel(object viewModel2)
        {
            // Currently does nothing. But all view models have a dispatch method, so I think I need a placeholder here.
        }

        private void DispatchCurveDetailsViewModel(object viewModel2)
        {
            var castedViewModel = (CurveDetailsViewModel)viewModel2;

            var list = DocumentViewModelHelper.GetCurveDetailsViewModelList_ByCurveID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.ValidationMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchCurveGridViewModel(object viewModel2)
        {
            var castedViewModel = (CurveGridViewModel)viewModel2;

            bool isRootDocument = MainViewModel.Document.ID == castedViewModel.DocumentID;
            if (isRootDocument)
            {
                MainViewModel.Document.CurveGrid = castedViewModel;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.GetPatchDocumentViewModel(MainViewModel.Document, castedViewModel.DocumentID);
                patchDocumentViewModel.CurveGrid = castedViewModel;
            }

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchCurvePropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (CurvePropertiesViewModel)viewModel2;

            var list = DocumentViewModelHelper.GetCurvePropertiesViewModelList_ByCurveID(MainViewModel.Document, castedViewModel.Entity.ID);
            int? listIndex = list.TryGetIndexOf(x => x.Entity.ID == castedViewModel.Entity.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
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
                HideAllListAndDetailViewModels();
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
                HideAllListAndDetailViewModels();
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

            var list = DocumentViewModelHelper.GetNodePropertiesViewModelList_ByNodeID(MainViewModel.Document, castedViewModel.Entity.ID);
            int? listIndex = list.TryGetIndexOf(x => x.Entity.ID == castedViewModel.Entity.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchNotFoundViewModel(object viewModel2)
        {
            var castedViewModel = (NotFoundViewModel)viewModel2;

            MainViewModel.NotFound = castedViewModel;

            // HACK: Checking visibility of the NotFound view model
            // prevents refreshing the DocumentList twice:
            // once when showing the NotFound view model,
            // a second time when clicking OK on it.

            // TODO: Low priority: Eventually the NotFoundViewModel will create even more ambiguity,
            // when it is reused for multiple entity types.

            if (castedViewModel.Visible)
            {
                DocumentGridRefresh();
            }
        }

        private void DispatchOperatorPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForBundle(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForBundle)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForBundles_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForCurve(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCurve)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForCurves_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForCustomOperator(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForCustomOperator)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForCustomOperators_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchInlet(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchInlet)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForPatchInlets_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForPatchOutlet(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForPatchOutlet)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForPatchOutlets_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForSample(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForSample)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForSamples_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForNumber(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForNumber)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForNumbers_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForSpectrum(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForSpectrum)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForSpectrums_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchOperatorPropertiesViewModel_ForUnbundle(object viewModel2)
        {
            var castedViewModel = (OperatorPropertiesViewModel_ForUnbundle)viewModel2;

            var list = DocumentViewModelHelper.GetOperatorPropertiesViewModelList_ForUnbundles_ByOperatorID(MainViewModel.Document, castedViewModel.ID);
            int? listIndex = list.TryGetIndexOf(x => x.ID == castedViewModel.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchDetailsViewModel(object viewModel2)
        {
            var castedViewModel = (PatchDetailsViewModel)viewModel2;
            
            var list = MainViewModel.Document.PatchDocumentList;
            int listIndex = MainViewModel.Document.PatchDocumentList.IndexOf(x => x.PatchDetails.Entity.PatchID == castedViewModel.Entity.PatchID);
            list[listIndex].PatchDetails = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.ValidationMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchPatchGridViewModel(object viewModel2)
        {
            var castedViewModel = (PatchGridViewModel)viewModel2;

            MainViewModel.Document.PatchGridList.TryRemoveFirst(x => String.Equals(x.Group, castedViewModel.Group));
            MainViewModel.Document.PatchGridList.Add(castedViewModel);

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchPatchPropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (PatchPropertiesViewModel)viewModel2;

            var list = MainViewModel.Document.PatchDocumentList;
            int listIndex = MainViewModel.Document.PatchDocumentList.IndexOf(x => x.PatchProperties.PatchID == castedViewModel.PatchID);
            list[listIndex].PatchProperties = castedViewModel;

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchSampleGridViewModel(object viewModel2)
        {
            var castedViewModel = (SampleGridViewModel)viewModel2;

            bool isRootDocument = MainViewModel.Document.ID == castedViewModel.DocumentID;
            if (isRootDocument)
            {
                MainViewModel.Document.SampleGrid = castedViewModel;
            }
            else
            {
                PatchDocumentViewModel patchDocumentViewModel = DocumentViewModelHelper.GetPatchDocumentViewModel(MainViewModel.Document, castedViewModel.DocumentID);
                patchDocumentViewModel.SampleGrid = castedViewModel;
            }

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchSamplePropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (SamplePropertiesViewModel)viewModel2;

            var list = DocumentViewModelHelper.GetSamplePropertiesViewModelList_BySampleID(MainViewModel.Document, castedViewModel.Entity.ID);
            int? listIndex = list.TryGetIndexOf(x => x.Entity.ID == castedViewModel.Entity.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
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
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }
        }

        private void DispatchScalePropertiesViewModel(object viewModel2)
        {
            var castedViewModel = (ScalePropertiesViewModel)viewModel2;

            var list = MainViewModel.Document.ScalePropertiesList;
            int? listIndex = list.TryGetIndexOf(x => x.Entity.ID == castedViewModel.Entity.ID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllPropertiesViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }

        private void DispatchToneGridEditViewModel(object viewModel2)
        {
            var castedViewModel = (ToneGridEditViewModel)viewModel2;

            var list = MainViewModel.Document.ToneGridEditList;
            int? listIndex = list.TryGetIndexOf(x => x.ScaleID == castedViewModel.ScaleID);
            if (listIndex.HasValue)
            {
                list[listIndex.Value] = castedViewModel;
            }
            else
            {
                list.Add(castedViewModel);
            }

            if (castedViewModel.Visible)
            {
                HideAllListAndDetailViewModels();
                castedViewModel.Visible = true;
            }

            MainViewModel.PopupMessages.AddRange(castedViewModel.ValidationMessages);
            castedViewModel.ValidationMessages.Clear();
        }
    }
}
