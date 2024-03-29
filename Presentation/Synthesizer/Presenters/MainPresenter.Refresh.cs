﻿using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        private void AudioFileOutputGrid_Refresh()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Partial Action
            AudioFileOutputGridViewModel viewModel = _audioFileOutputGridPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void AudioFileOutputPropertiesDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.AudioFileOutputPropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<AudioFileOutput> entities = document.AudioFileOutputs;

            foreach (AudioFileOutput entity in entities)
            {
                AudioFileOutputPropertiesViewModel viewModel = ViewModelSelector.TryGetAudioFileOutputPropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    AudioFileOutputProperties_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleAudioFileOutputProperties?.Entity.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleAudioFileOutputProperties = null;
                }
            }
        }

        private void AudioFileOutputProperties_Refresh(AudioFileOutputPropertiesViewModel userInput)
        {
            // Partial Action
            AudioFileOutputPropertiesViewModel viewModel = _audioFileOutputPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void AudioOutputProperties_Refresh()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // Partial Action
            AudioOutputPropertiesViewModel viewModel = _audioOutputPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void InstrumentBar_Refresh()
        {
            // GetViewModel
            InstrumentBarViewModel userInput = MainViewModel.Document.InstrumentBar;

            // Partial Action
            InstrumentBarViewModel viewModel = _instrumentBarPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void CurveDetailsDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.CurveDetailsDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Curve> entities = document.GetCurves();

            foreach (Curve entity in entities)
            {
                CurveDetailsViewModel viewModel = ViewModelSelector.TryGetCurveDetailsViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToDetailsViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    CurveDetails_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);
            }
        }
    
        private void CurveDetailsNode_Refresh(int curveID, int nodeID)
        {
            CurveDetailsViewModel detailsViewModel = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // Remove original node
            detailsViewModel.Nodes.Remove(nodeID);

            // Add new version of the node
            Node node = _repositories.NodeRepository.Get(nodeID);
            NodeViewModel nodeViewModel = node.ToViewModel();
            detailsViewModel.Nodes[nodeID] = nodeViewModel;

            detailsViewModel.RefreshID = RefreshIDProvider.GetRefreshID();
        }

        private void CurveDetails_Refresh(CurveDetailsViewModel userInput)
        {
            CurveDetailsViewModel viewModel = _curveDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void DocumentGrid_Refresh()
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // TemplateMethod
            ExecuteReadAction(userInput, () => _documentGridPresenter.Refresh(userInput));
        }

        private void DocumentProperties_Refresh()
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Partial Action
            DocumentPropertiesViewModel viewModel = _documentPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void DocumentTree_Refresh()
        {
            // Partial Action
            DocumentTreeViewModel viewModel = _documentTreePresenter.Refresh(MainViewModel.Document.DocumentTree);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        /// <summary> Note that this does not do a ViewModel to Entity conversion. </summary>
        private void DocumentViewModel_Refresh()
        {
            AudioFileOutputGrid_Refresh();
            AudioFileOutputPropertiesDictionary_Refresh();
            AudioOutputProperties_Refresh();
            InstrumentBar_Refresh();
            CurveDetailsDictionary_Refresh();
            DocumentProperties_Refresh();
            DocumentTree_Refresh();
            LibraryPropertiesDictionary_Refresh();
            LibrarySelectionPopup_Refresh();
            NodePropertiesDictionary_Refresh();
            MidiMappingGroupDetailsDictionary_Refresh();
            MidiMappingPropertiesDictionary_Refresh();
            OperatorPropertiesDictionary_ForCaches_Refresh();
            OperatorPropertiesDictionary_ForCurves_Refresh();
            OperatorPropertiesDictionary_ForInletsToDimension_Refresh();
            OperatorPropertiesDictionary_ForNumbers_Refresh();
            OperatorPropertiesDictionary_ForPatchInlets_Refresh();
            OperatorPropertiesDictionary_ForPatchOutlets_Refresh();
            OperatorPropertiesDictionary_ForSamples_Refresh();
            OperatorPropertiesDictionary_WithCollectionRecalculation_Refresh();
            OperatorPropertiesDictionary_WithInterpolation_Refresh();
            OperatorPropertiesDictionary_Refresh();
            PatchDetailsDictionary_Refresh();
            PatchPropertiesDictionary_Refresh();
            ScalePropertiesDictionary_Refresh();
            ToneGridEditDictionary_Refresh();
            UnderlyingPatchLookup_Refresh();

            // Note that AutoPatchDetails cannot be refreshed.
        }

        private void LibraryPropertiesDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.LibraryPropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<DocumentReference> entities = document.LowerDocumentReferences;

            foreach (DocumentReference entity in entities)
            {
                LibraryPropertiesViewModel viewModel = ViewModelSelector.TryGetLibraryPropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    LibraryProperties_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleLibraryProperties?.DocumentReferenceID == idToDelete)
                {
                    MainViewModel.Document.VisibleLibraryProperties = null;
                }
            }
        }

        private void LibraryProperties_Refresh(LibraryPropertiesViewModel userInput)
        {
            LibraryPropertiesViewModel viewModel = _libraryPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void LibrarySelectionPopup_Refresh()
        {
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;
            LibrarySelectionPopupViewModel viewModel = _librarySelectionPopupPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void NodePropertiesDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.NodePropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Node> entities = document.GetCurves().SelectMany(x => x.Nodes).ToArray();

            foreach (Node entity in entities)
            {
                NodePropertiesViewModel viewModel = ViewModelSelector.TryGetNodePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel(_repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    NodeProperties_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleNodeProperties?.Entity.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleNodeProperties = null;
                }
            }
        }

        private void NodeProperties_Refresh(int nodeID)
        {
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);
            NodeProperties_Refresh(userInput);
        }

        private void NodeProperties_Refresh(NodePropertiesViewModel userInput)
        {
            NodePropertiesViewModel viewModel = _nodePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void MidiMappingGroupDetailsDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.MidiMappingGroupDetailsDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<MidiMappingGroup> entities = document.MidiMappingGroups;

            foreach (MidiMappingGroup entity in entities)
            {
                MidiMappingGroupDetailsViewModel viewModel = ViewModelSelector.TryGetMidiMappingGroupDetailsViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToDetailsViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    MidiMappingGroupDetails_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleMidiMappingGroupDetails?.MidiMappingGroup.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleMidiMappingGroupDetails = null;
                }
            }
        }

        private void MidiMappingGroupDetails_Refresh(MidiMappingGroupDetailsViewModel userInput)
        {
            MidiMappingGroupDetailsViewModel viewModel = _midiMappingDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void MidiMappingPropertiesDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.MidiMappingPropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<MidiMapping> entities = document.MidiMappingGroups.SelectMany(x => x.MidiMappings).ToArray();

            foreach (MidiMapping entity in entities)
            {
                MidiMappingPropertiesViewModel viewModel = ViewModelSelector.TryGetMidiMappingPropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    MidiMappingProperties_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleMidiMappingProperties?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleMidiMappingProperties = null;
                }
            }
        }

        private void MidiMappingProperties_Refresh(MidiMappingPropertiesViewModel userInput)
        {
            MidiMappingPropertiesViewModel viewModel = _midiMappingPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_Refresh(OperatorPropertiesViewModel userInput)
        {
            OperatorPropertiesViewModel viewModel = _operatorPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCache_Refresh(OperatorPropertiesViewModel_ForCache userInput)
        {
            OperatorPropertiesViewModel_ForCache viewModel = _operatorPropertiesPresenter_ForCache.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForCurve_Refresh(OperatorPropertiesViewModel_ForCurve userInput)
        {
            OperatorPropertiesViewModel_ForCurve viewModel = _operatorPropertiesPresenter_ForCurve.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForInletsToDimension_Refresh(OperatorPropertiesViewModel_ForInletsToDimension userInput)
        {
            OperatorPropertiesViewModel_ForInletsToDimension viewModel = _operatorPropertiesPresenter_ForInletsToDimension.Refresh(userInput);
            DispatchViewModel(viewModel);
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

        private void OperatorProperties_ForPatchOutlet_Refresh(OperatorPropertiesViewModel_ForPatchOutlet userInput)
        {
            OperatorPropertiesViewModel_ForPatchOutlet viewModel = _operatorPropertiesPresenter_ForPatchOutlet.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_ForSample_Refresh(OperatorPropertiesViewModel_ForSample userInput)
        {
            OperatorPropertiesViewModel_ForSample viewModel = _operatorPropertiesPresenter_ForSample.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithInterpolation_Refresh(OperatorPropertiesViewModel_WithInterpolation userInput)
        {
            OperatorPropertiesViewModel_WithInterpolation viewModel = _operatorPropertiesPresenter_WithInterpolation.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithCollectionRecalculation_Refresh(OperatorPropertiesViewModel_WithCollectionRecalculation userInput)
        {
            OperatorPropertiesViewModel_WithCollectionRecalculation viewModel = _operatorPropertiesPresenter_WithCollectionRecalculation.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorPropertiesDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ToViewModelHelper.OperatorTypeEnums_WithStandardPropertiesView.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_ForCaches_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCaches;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Cache))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCache viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCache(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCache(_repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCache_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForCurve?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForCurve = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_ForCurves_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCurves;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Curve))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCurve viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve_ByOperatorID(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCurve();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCurve_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForCurve?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForCurve = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_ForInletsToDimension_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForInletsToDimension;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.InletsToDimension))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForInletsToDimension viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForInletsToDimension(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForInletsToDimension(_repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForInletsToDimension_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_ForNumbers_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Number))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForNumber viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForNumber(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForNumber();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForNumber_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForNumber?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForNumber = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_ForPatchInlets_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchInlet))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchInlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchInlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchInlet();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForPatchInlet_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_ForPatchOutlets_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.PatchOutlet))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForPatchOutlet viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForPatchOutlet(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForPatchOutlet();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForPatchOutlet_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_ForSamples_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForSamples;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.Sample))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForSample viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForSample(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository, _repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForSample_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForSample?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForSample = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_WithInterpolation_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInterpolation;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ToViewModelHelper.OperatorTypeEnums_WithInterpolationPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithInterpolation viewModel = 
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, op.ID);

                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithInterpolation(_repositories.InterpolationTypeRepository);
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithInterpolation_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_WithInterpolation?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_WithCollectionRecalculation_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithCollectionRecalculation;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ToViewModelHelper.OperatorTypeEnums_WithCollectionRecalculationPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithCollectionRecalculation viewModel =
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_WithCollectionRecalculation(MainViewModel.Document, op.ID);

                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithCollectionRecalculation();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithCollectionRecalculation_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = null;
                }
            }
        }

        private void PatchDetails_RefreshOperator(OperatorViewModel viewModel)
        {
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);
            PatchDetails_RefreshOperator(entity, viewModel);
        }

        private void PatchDetails_RefreshOperator(int operatorID)
        {
            Operator op = _repositories.OperatorRepository.Get(operatorID);
            int patchID = op.Patch.ID;

            OperatorViewModel viewModel = ViewModelSelector.GetOperatorViewModel(MainViewModel.Document, patchID, operatorID);
            PatchDetails_RefreshOperator(viewModel);

            // TODO: Replace this with moving RefreshOperator to the PatchDetail presenter?
            PatchDetailsViewModel detailsViewModel = ViewModelSelector.GetPatchDetailsViewModel(MainViewModel.Document, patchID);
            detailsViewModel.RefreshID = RefreshIDProvider.GetRefreshID();
        }

        private void PatchDetails_RefreshOperator(Operator entity, OperatorViewModel operatorViewModel) => ToViewModelHelper.RefreshViewModel_WithInletsAndOutlets(entity, operatorViewModel);

        private void PatchDetailsDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.PatchDetailsDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> entities = document.Patches;

            foreach (Patch entity in entities)
            {
                PatchDetailsViewModel viewModel = ViewModelSelector.TryGetPatchDetailsViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToDetailsViewModel();

                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    PatchDetails_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisiblePatchDetails?.Entity.ID == idToDelete)
                {
                    MainViewModel.Document.VisiblePatchDetails = null;
                }
            }
        }

        private void PatchDetails_Refresh(PatchDetailsViewModel userInput)
        {
            PatchDetailsViewModel viewModel = _patchDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchPropertiesDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.PatchPropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> entities = document.Patches;

            foreach (Patch entity in entities)
            {
                PatchPropertiesViewModel viewModel = ViewModelSelector.TryGetPatchPropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();

                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    PatchProperties_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisiblePatchProperties?.ID == idToDelete)
                {
                    MainViewModel.Document.VisiblePatchProperties = null;
                }
            }
        }

        private void PatchProperties_Refresh(PatchPropertiesViewModel userInput)
        {
            PatchPropertiesViewModel viewModel = _patchPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ScalePropertiesDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.ScalePropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Scale> entities = document.Scales;

            foreach (Scale entity in entities)
            {
                ScalePropertiesViewModel viewModel = ViewModelSelector.TryGetScalePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    ScaleProperties_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleScaleProperties?.Entity.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleScaleProperties = null;
                }
            }
        }

        private void ScaleProperties_Refresh(ScalePropertiesViewModel userInput)
        {
            ScalePropertiesViewModel viewModel = _scalePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void TitleBar_Refresh()
        {
            int documentID = MainViewModel.Document.ID;
            Document document = _repositories.DocumentRepository.TryGet(documentID);
            string windowTitle = _titleBarPresenter.Show(document);
            MainViewModel.TitleBar = windowTitle;
        }

        private void ToneGridEditDictionary_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.ToneGridEditDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Scale> entities = document.Scales;

            foreach (Scale entity in entities)
            {
                ToneGridEditViewModel viewModel = ViewModelSelector.TryGetToneGridEditViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToToneGridEditViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    ToneGridEdit_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingScaleIDs = viewModelDictionary.Keys;
            IEnumerable<int> scaleIDsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> scaleIDsToDelete = existingScaleIDs.Except(scaleIDsToKeep);

            foreach (int scaleIDToDelete in scaleIDsToDelete.ToArray())
            {
                viewModelDictionary.Remove(scaleIDToDelete);

                if (MainViewModel.Document.VisibleToneGridEdit?.ScaleID == scaleIDToDelete)
                {
                    MainViewModel.Document.VisibleToneGridEdit = null;
                }
            }
        }

        private void ToneGridEdit_Refresh(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = _toneGridEditPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ToneGridEdit_Refresh(int scaleID)
        {
            ToneGridEditViewModel viewModel = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);
            ToneGridEdit_Refresh(viewModel);
        }

        /// <summary> Just refreshes the TopButtonBar partial. Not the Refresh user action, that can be executed from the TopButtonBar. </summary>
        private void TopButtonBar_RefreshPartial()
        {
            TopButtonBarViewModel viewModel = MainViewModel.Document.TopButtonBar;
            DocumentTreeNodeTypeEnum selectedNodeType = MainViewModel.Document.DocumentTree.SelectedNodeType;
            bool documentTreeVisible = MainViewModel.Document.DocumentTree.Visible;
            bool patchDetailsVisible = MainViewModel.Document.VisiblePatchDetails != null;

            _topButtonBarPresenter.Refresh(viewModel, selectedNodeType, documentTreeVisible, patchDetailsVisible);
        }

        private void UnderlyingPatchLookup_Refresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            MainViewModel.Document.UnderlyingPatchLookup = document.ToUnderlyingPatchLookupViewModel();
        }
    }
}
