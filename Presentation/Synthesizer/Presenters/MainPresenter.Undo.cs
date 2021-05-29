using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Enums;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.InvalidValues;
using JJ.Framework.Exceptions.TypeChecking;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        private void Undo()
        {
            UndoItemViewModelBase undoItemViewModel = MainViewModel.Document.UndoHistory.PopOrDefault();

            if (undoItemViewModel == null)
            {
                return;
            }

            MainViewModel.Document.RedoFuture.Push(undoItemViewModel);

            switch (undoItemViewModel)
            {
                case UndoCreateViewModel undoInsertViewModel:
                    foreach (EntityTypeAndIDViewModel entityTypeAndIDViewModel in undoInsertViewModel.EntityTypesAndIDs)
                    {
                        ExecuteUndoRedoDeletion(entityTypeAndIDViewModel.EntityTypeEnum, entityTypeAndIDViewModel.EntityID);
                    }

                    break;

                case UndoUpdateViewModel undoUpdateViewModel:
                    ExecuteUndoRedoInsertionOrUpdate(undoUpdateViewModel.OldStates);
                    break;

                case UndoDeleteViewModel undoDeleteViewModel:
                    ExecuteUndoRedoInsertionOrUpdate(undoDeleteViewModel.States);
                    break;

                default:
                    throw new UnexpectedTypeException(() => undoItemViewModel);
            }
        }

        private void Redo()
        {
            UndoItemViewModelBase undoItemViewModel = MainViewModel.Document.RedoFuture.PopOrDefault();

            if (undoItemViewModel == null)
            {
                return;
            }

            MainViewModel.Document.UndoHistory.Push(undoItemViewModel);

            // ToEntity
            if (MainViewModel.Document.IsOpen)
            {
                MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }

            switch (undoItemViewModel)
            {
                case UndoCreateViewModel undoInsertViewModel:
                    ExecuteUndoRedoInsertionOrUpdate(undoInsertViewModel.States);
                    break;

                case UndoUpdateViewModel undoUpdateViewModel:
                    ExecuteUndoRedoInsertionOrUpdate(undoUpdateViewModel.NewStates);
                    break;

                case UndoDeleteViewModel undoDeleteViewModel:
                    foreach (EntityTypeAndIDViewModel entityTypeAndIDViewModel in undoDeleteViewModel.EntityTypesAndIDs)
                    {
                        ExecuteUndoRedoDeletion(entityTypeAndIDViewModel.EntityTypeEnum, entityTypeAndIDViewModel.EntityID);
                    }

                    break;

                default:
                    throw new UnexpectedTypeException(() => undoItemViewModel);
            }
        }

        private void ExecuteUndoRedoInsertionOrUpdate(IList<ScreenViewModelBase> states)
        {
            // Action
            foreach (ScreenViewModelBase viewModel in states)
            {
                RestoreUndoState(viewModel);
            }

            // ToEntity
            if (MainViewModel.Document.IsOpen)
            {
                MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }

            // Side-Effects
            if (MustRefreshDocument(states))
            {
                _documentFacade.Refresh(MainViewModel.Document.ID);
            }

            // Refresh
            DocumentViewModel_Refresh();
        }

        private bool MustRefreshDocument(IList<ScreenViewModelBase> states)
        {
            foreach (ScreenViewModelBase state in states)
            {
                switch (state)
                {
                    case OperatorPropertiesViewModel_ForPatchInlet _:
                    case OperatorPropertiesViewModel_ForPatchOutlet _:
                        return true;
                }
            }

            return false;
        }

        private void RestoreUndoState(ScreenViewModelBase viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));

            viewModel.Successful = true;
            viewModel.Visible = true;
            viewModel.RefreshID = RefreshIDProvider.GetRefreshID();

            DispatchViewModel(viewModel);
        }

        private void ExecuteUndoRedoDeletion(EntityTypeEnum entityTypeEnum, int id)
        {
            // ToEntity
            if (MainViewModel.Document.IsOpen)
            {
                MainViewModel.ToEntityWithRelatedEntities(_repositories);
            }

            // Business
            switch (entityTypeEnum)
            {
                case EntityTypeEnum.AudioFileOutput:
                    _audioFileOutputFacade.Delete(id);
                    break;

                case EntityTypeEnum.Node:
                    _curveFacade.DeleteNode(id);
                    break;

                case EntityTypeEnum.Operator:
                    _patchFacade.DeleteOperatorWithRelatedEntities(id);
                    break;

                case EntityTypeEnum.Patch:
                {
                    IResult result = _patchFacade.DeletePatchWithRelatedEntities(id);
                    result.Assert();
                    break;
                }

                case EntityTypeEnum.Scale:
                {
                    IResult result = _scaleFacade.DeleteWithRelatedEntities(id);
                    result.Assert();
                    break;
                }

                case EntityTypeEnum.Tone:
                    _scaleFacade.DeleteTone(id);
                    break;

                case EntityTypeEnum.DocumentReference:
                {
                    IResult result = _documentFacade.DeleteDocumentReference(id);
                    result.Assert();
                    break;
                }

                case EntityTypeEnum.MidiMappingGroup:
                    _midiMappingFacade.DeleteMidiMappingGroup(id);
                    break;

                case EntityTypeEnum.MidiMapping:
                    _midiMappingFacade.DeleteMidiMapping(id);
                    break;

                default:
                    throw new ValueNotSupportedException(entityTypeEnum);
            }

            // Refresh
            DocumentViewModel_Refresh();
        }

        private IList<ScreenViewModelBase> GetAudioFileOutputStates(int id) => new List<ScreenViewModelBase>
        {
            ViewModelSelector.GetAudioFileOutputPropertiesViewModel(MainViewModel.Document, id)
        };

        private IList<ScreenViewModelBase> GetLibraryStates(int documentReferenceID) => new List<ScreenViewModelBase>
        {
            ViewModelSelector.GetLibraryPropertiesViewModel(MainViewModel.Document, documentReferenceID)
        };

        private IList<ScreenViewModelBase> GetMidiMappingStates(int id)
        {
            MidiMappingPropertiesViewModel mappingElementPropertiesViewModel =
                ViewModelSelector.GetMidiMappingPropertiesViewModel(MainViewModel.Document, id);
            int midiMappingGroupID = mappingElementPropertiesViewModel.MidiMappingGroupID;
            MidiMappingGroupDetailsViewModel midiMappingDetailsViewModel =
                ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document, midiMappingGroupID);

            var states = new List<ScreenViewModelBase> { midiMappingDetailsViewModel, mappingElementPropertiesViewModel };

            return states;
        }

        private IList<ScreenViewModelBase> GetMidiMappingGroupStates(int midiMappingGroupID) => ViewModelSelector
                                                                                                .EnumerateMidiMappingPropertiesViewModel_ByMidiMappingGroupID(MainViewModel.Document,midiMappingGroupID)
                                                                                                .Union<ScreenViewModelBase>(ViewModelSelector.GetMidiMappingGroupDetailsViewModel(MainViewModel.Document,midiMappingGroupID))
                                                                                                .ToArray();

        private IList<ScreenViewModelBase> GetNodeStates(int id)
        {
            NodePropertiesViewModel nodePropertiesViewModel = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, id);
            CurveDetailsViewModel curveDetailsViewModel =
                ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, nodePropertiesViewModel.CurveID);

            return new List<ScreenViewModelBase>
            {
                nodePropertiesViewModel,
                curveDetailsViewModel
            };
        }

        /// <summary>
        /// NOTE: OperatorViewModel cannot be used with DispatchViewModel, so cannot be part of the undo state,
        /// so instead PatchDetailsViewModel is part of the undo state.
        /// Also including PatchDetailsViewModel in the OperatorState will make sure the cleaning up of obsolete inlets/outlets is taken care of.
        /// </summary>
        private IList<ScreenViewModelBase> GetOperatorStates(int id)
        {
            OperatorPropertiesViewModelBase operatorPropertiesViewModel =
                ViewModelSelector.GetOperatorPropertiesViewModelPolymorphic(MainViewModel.Document, id);
            PatchDetailsViewModel patchDetailsViewModel =
                ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, operatorPropertiesViewModel.PatchID);

            var states = new List<ScreenViewModelBase> { patchDetailsViewModel, operatorPropertiesViewModel };

            if (operatorPropertiesViewModel is OperatorPropertiesViewModel_ForCurve castedViewModel)
            {
                CurveDetailsViewModel curveDetailsViewModel = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, castedViewModel.CurveID);
                states.Add(curveDetailsViewModel);

                IEnumerable<NodePropertiesViewModel> nodePropertiesViewModels = MainViewModel.Document.NodePropertiesDictionary.Select(x => x.Value).Where(x => x.CurveID == curveDetailsViewModel.Curve.ID);
                states.AddRange(nodePropertiesViewModels);
            }

            return states;
        }

        private IList<ScreenViewModelBase> GetPatchStates(int id)
        {
            // NOTE: 'By accident' the GetOperatorStates already includes the PatchDetailsViewModel, but to not apply the unwritten agreement anti-pattern,
            // it is included here again. When GetOperatorStates changes, this should not break this code. Also it would look like something is wrong if it weren't included here.
            PatchDetailsViewModel patchDetailsViewModel = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, id);
            PatchPropertiesViewModel patchPropertiesViewModel = ViewModelSelector.GetPatchPropertiesViewModel(MainViewModel.Document, id);

            IList<ScreenViewModelBase> states = ViewModelSelector.EnumerateAllOperatorPropertiesViewModels(MainViewModel.Document)
                                                                 .Where(x => x.PatchID == id)
                                                                 .SelectMany(x => GetOperatorStates(x.ID))
                                                                 .Concat(patchDetailsViewModel)
                                                                 .Concat(patchPropertiesViewModel)
                                                                 .Distinct() // Removes duplicate entries of PatchDetailsViewModel.
                                                                 .ToArray();
            return states;
        }

        private IList<ScreenViewModelBase> GetScaleStates(int id) => new List<ScreenViewModelBase>
        {
            ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, id),
            ViewModelSelector.GetScalePropertiesViewModel(MainViewModel.Document, id)
        };

        private IList<ScreenViewModelBase> GetToneStates(int scaleID) =>
            new List<ScreenViewModelBase> { ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID) };
    }
}