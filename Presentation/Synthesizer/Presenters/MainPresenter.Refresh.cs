using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public partial class MainPresenter
    {
        private void AudioFileOutputGridRefresh()
        {
            // GetViewModel
            AudioFileOutputGridViewModel userInput = MainViewModel.Document.AudioFileOutputGrid;

            // Partial Action
            AudioFileOutputGridViewModel viewModel = _audioFileOutputGridPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void AudioFileOutputPropertiesDictionaryRefresh()
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
                    AudioFileOutputPropertiesRefresh(viewModel);
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

        private void AudioFileOutputPropertiesRefresh(AudioFileOutputPropertiesViewModel userInput)
        {
            // Partial Action
            AudioFileOutputPropertiesViewModel viewModel = _audioFileOutputPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void AudioOutputPropertiesRefresh()
        {
            // GetViewModel
            AudioOutputPropertiesViewModel userInput = MainViewModel.Document.AudioOutputProperties;

            // Partial Action
            AudioOutputPropertiesViewModel viewModel = _audioOutputPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void CurrentInstrumentRefresh()
        {
            // GetViewModel
            CurrentInstrumentViewModel userInput = MainViewModel.Document.CurrentInstrument;

            // Partial Action
            CurrentInstrumentViewModel viewModel = _currentInstrumentPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void CurveDetailsDictionaryRefresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.CurveDetailsDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Curve> entities = document.Curves;

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
                    CurveDetailsRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleCurveDetails?.CurveID == idToDelete)
                {
                    MainViewModel.Document.VisibleCurveDetails = null;
                }
            }
        }
    
        private void CurveDetailsNodeRefresh(int curveID, int nodeID)
        {
            CurveDetailsViewModel detailsViewModel = ViewModelSelector.GetCurveDetailsViewModel(MainViewModel.Document, curveID);

            // Remove original node
            detailsViewModel.Nodes.Remove(nodeID);

            // Add new version of the node
            Node node = _repositories.NodeRepository.Get(nodeID);
            NodeViewModel nodeViewModel = node.ToViewModel();
            detailsViewModel.Nodes[nodeID] = nodeViewModel;

            detailsViewModel.RefreshCounter++;
        }

        private void CurveDetailsRefresh(CurveDetailsViewModel userInput)
        {
            CurveDetailsViewModel viewModel = _curveDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void CurveGridRefresh()
        {
            CurveGridViewModel userInput = MainViewModel.Document.CurveGrid;
            CurveGridViewModel viewModel = _curveGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void CurveLookupRefresh()
        {
            // GetEntity
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            // Business
            IList<UsedInDto<Curve>> curveUsedInDtos = _documentManager.GetUsedIn(document.Curves);

            // ToViewModel
            MainViewModel.Document.CurveLookup = ViewModelHelper.CreateCurveLookupViewModel(curveUsedInDtos);
        }

        private void CurvePropertiesDictionaryRefresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.CurvePropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Curve> entities = document.Curves;

            foreach (Curve entity in entities)
            {
                CurvePropertiesViewModel viewModel = ViewModelSelector.TryGetCurvePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    CurvePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);
                
                if (MainViewModel.Document.VisibleCurveProperties?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleCurveProperties = null;
                }
            }
        }

        private void CurvePropertiesRefresh(CurvePropertiesViewModel userInput)
        {
            CurvePropertiesViewModel viewModel = _curvePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void DocumentGridRefresh()
        {
            // GetViewModel
            DocumentGridViewModel userInput = MainViewModel.DocumentGrid;

            // TemplateMethod
            TemplateActionMethod(userInput, () => _documentGridPresenter.Refresh(userInput));
        }

        private void DocumentPropertiesRefresh()
        {
            // GetViewModel
            DocumentPropertiesViewModel userInput = MainViewModel.Document.DocumentProperties;

            // Partial Action
            DocumentPropertiesViewModel viewModel = _documentPropertiesPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void DocumentTreeRefresh()
        {
            // Partial Action
            DocumentTreeViewModel viewModel = _documentTreePresenter.Refresh(MainViewModel.Document.DocumentTree);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void DocumentViewModelRefresh()
        {
            AudioFileOutputGridRefresh();
            AudioFileOutputPropertiesDictionaryRefresh();
            AudioOutputPropertiesRefresh();
            AudioOutputPropertiesRefresh();
            CurrentInstrumentRefresh();
            CurveDetailsDictionaryRefresh();
            CurveGridRefresh();
            CurveLookupRefresh();
            CurvePropertiesDictionaryRefresh();
            DocumentPropertiesRefresh();
            DocumentTreeRefresh();
            LibraryGridRefresh();
            LibraryPatchPropertiesDictionaryRefresh();
            LibraryPropertiesDictionaryRefresh();
            LibrarySelectionPopupRefresh();
            NodePropertiesDictionaryRefresh();
            OperatorPropertiesDictionary_ForCaches_Refresh();
            OperatorPropertiesDictionary_ForCurves_Refresh();
            OperatorPropertiesDictionary_ForCustomOperators_Refresh();
            OperatorPropertiesDictionary_ForInletsToDimension_Refresh();
            OperatorPropertiesDictionary_ForNumbers_Refresh();
            OperatorPropertiesDictionary_ForPatchInlets_Refresh();
            OperatorPropertiesDictionary_ForPatchOutlets_Refresh();
            OperatorPropertiesDictionary_ForSamples_Refresh();
            OperatorPropertiesDictionary_WithCollectionRecalculation_Refresh();
            OperatorPropertiesDictionary_WithInterpolation_Refresh();
            OperatorPropertiesDictionary_WithOutletCount_Refresh();
            OperatorPropertiesDictionary_WithInletCount_Refresh();
            OperatorPropertiesDictionaryRefresh();
            PatchDetailsDictionaryRefresh();
            PatchGridDictionaryRefresh();
            PatchPropertiesDictionaryRefresh();
            SampleGridRefresh();
            SampleLookupRefresh();
            SamplePropertiesDictionaryRefresh();
            ScaleGridRefresh();
            ScalePropertiesDictionaryRefresh();
            ToneGridEditDictionaryRefresh();
            UnderylingPatchLookupRefresh();

            // Note that AutoPatchDetails cannot be refreshed.
        }

        private void LibraryGridRefresh()
        {
            LibraryGridViewModel userInput = MainViewModel.Document.LibraryGrid;
            LibraryGridViewModel viewModel = _libraryGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void LibraryPatchPropertiesDictionaryRefresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.LibraryPatchPropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            foreach (DocumentReference lowerDocumentReference in document.LowerDocumentReferences)
            {
                // ReSharper disable once InvertIf
                if (lowerDocumentReference.LowerDocument != null)
                {
                    foreach (Patch patch in lowerDocumentReference.LowerDocument.Patches)
                    {
                        LibraryPatchPropertiesViewModel viewModel = ViewModelSelector.TryGetLibraryPatchPropertiesViewModel(MainViewModel.Document, patch.ID);
                        if (viewModel == null)
                        {
                            viewModel = patch.ToLibraryPatchPropertiesViewModel(lowerDocumentReference);

                            viewModel.Successful = true;
                            viewModelDictionary[patch.ID] = viewModel;
                        }
                        else
                        {
                            LibraryPatchPropertiesRefresh(viewModel);
                        }
                    }
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = document.LowerDocumentReferences.SelectMany(x => x.LowerDocument.Patches).Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleLibraryPatchProperties?.PatchID == idToDelete)
                {
                    MainViewModel.Document.VisibleLibraryPatchProperties = null;
                }
            }
        }

        private void LibraryPatchPropertiesRefresh(LibraryPatchPropertiesViewModel userInput)
        {
            LibraryPatchPropertiesViewModel viewModel = _libraryPatchPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void LibraryPropertiesDictionaryRefresh()
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
                    LibraryPropertiesRefresh(viewModel);
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

        private void LibraryPropertiesRefresh(LibraryPropertiesViewModel userInput)
        {
            LibraryPropertiesViewModel viewModel = _libraryPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void LibrarySelectionPopupRefresh()
        {
            LibrarySelectionPopupViewModel userInput = MainViewModel.Document.LibrarySelectionPopup;
            LibrarySelectionPopupViewModel viewModel = _librarySelectionPopupPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void NodePropertiesDictionaryRefresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.NodePropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Node> entities = document.Curves.SelectMany(x => x.Nodes).ToArray();

            foreach (Node entity in entities)
            {
                NodePropertiesViewModel viewModel = ViewModelSelector.TryGetNodePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel();
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    NodePropertiesRefresh(viewModel);
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

        private void NodePropertiesRefresh(int nodeID)
        {
            NodePropertiesViewModel userInput = ViewModelSelector.GetNodePropertiesViewModel(MainViewModel.Document, nodeID);
            NodePropertiesRefresh(userInput);
        }

        private void NodePropertiesRefresh(NodePropertiesViewModel userInput)
        {
            NodePropertiesViewModel viewModel = _nodePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorPropertiesRefresh(OperatorPropertiesViewModel userInput)
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

        private void OperatorProperties_ForCustomOperator_Refresh(OperatorPropertiesViewModel_ForCustomOperator userInput)
        {
            OperatorPropertiesViewModel_ForCustomOperator viewModel = _operatorPropertiesPresenter_ForCustomOperator.Refresh(userInput);
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

        private void OperatorProperties_WithOutletCount_Refresh(OperatorPropertiesViewModel_WithOutletCount userInput)
        {
            OperatorPropertiesViewModel_WithOutletCount viewModel = _operatorPropertiesPresenter_WithOutletCount.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorProperties_WithInletCount_Refresh(OperatorPropertiesViewModel_WithInletCount userInput)
        {
            OperatorPropertiesViewModel_WithInletCount viewModel = _operatorPropertiesPresenter_WithInletCount.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void OperatorPropertiesDictionaryRefresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithoutAlternativePropertiesView.Contains(x.GetOperatorTypeEnum()))
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
                    OperatorPropertiesRefresh(viewModel);
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
                OperatorPropertiesViewModel_ForCurve viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCurve(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
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

        private void OperatorPropertiesDictionary_ForCustomOperators_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_ForCustomOperators;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.GetOperatorsOfType(OperatorTypeEnum.CustomOperator))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_ForCustomOperator viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_ForCustomOperator(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_ForCustomOperator_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_ForCustomOperator?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_ForCustomOperator = null;
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
                    viewModel = op.ToPropertiesViewModel_ForInletsToDimension();
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
                    viewModel = op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
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
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithInterpolationPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();
            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithInterpolation viewModel = 
                    ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInterpolation(MainViewModel.Document, op.ID);

                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithInterpolation();
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
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithCollectionRecalculationPropertyViews.Contains(x.GetOperatorTypeEnum()))
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

        private void OperatorPropertiesDictionary_WithInletCount_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithInletCount;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithInletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithInletCount viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithInletCount(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithInletCount();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithInletCount_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_WithInletCount?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_WithInletCount = null;
                }
            }
        }

        private void OperatorPropertiesDictionary_WithOutletCount_Refresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.OperatorPropertiesDictionary_WithOutletCount;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Operator> operators = document.Patches
                                                .SelectMany(x => x.Operators)
                                                .Where(x => ViewModelHelper.OperatorTypeEnums_WithOutletCountPropertyViews.Contains(x.GetOperatorTypeEnum()))
                                                .ToArray();

            foreach (Operator op in operators)
            {
                OperatorPropertiesViewModel_WithOutletCount viewModel = ViewModelSelector.TryGetOperatorPropertiesViewModel_WithOutletCount(MainViewModel.Document, op.ID);
                if (viewModel == null)
                {
                    viewModel = op.ToPropertiesViewModel_WithOutletCount();
                    viewModel.Successful = true;
                    viewModelDictionary[op.ID] = viewModel;
                }
                else
                {
                    OperatorProperties_WithOutletCount_Refresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = operators.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleOperatorProperties_WithOutletCount?.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleOperatorProperties_WithOutletCount = null;
                }
            }
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
            IList<OperatorViewModel> operatorViewModels =
                MainViewModel.Document.PatchDetailsDictionary.Values
                                      .SelectMany(x => x.Entity.OperatorDictionary.Values)
                                      .Where(x => x.OperatorType.ID == (int)operatorTypeEnum)
                                      .ToArray();

            foreach (OperatorViewModel operatorViewModel in operatorViewModels)
            {
                PatchDetails_RefreshOperator(operatorViewModel);
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
            detailsViewModel.RefreshCounter++;
        }

        private void PatchDetails_RefreshOperator(Operator entity, OperatorViewModel operatorViewModel)
        {
            ViewModelHelper.RefreshViewModel_WithInletsAndOutlets(
                entity,
                operatorViewModel,
                _repositories.SampleRepository,
                _repositories.CurveRepository,
                _repositories.PatchRepository,
                _entityPositionManager);
        }

        private void PatchDetailsDictionaryRefresh()
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
                    viewModel = entity.ToDetailsViewModel(
                        _repositories.SampleRepository, 
                        _repositories.CurveRepository, 
                        _repositories.PatchRepository,
                        _entityPositionManager);

                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    PatchDetailsRefresh(viewModel);
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

        private void PatchDetailsRefresh(PatchDetailsViewModel userInput)
        {
            PatchDetailsViewModel viewModel = _patchDetailsPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchGridDictionaryRefresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.PatchGridDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);

            var patchManager = new PatchManager(_patchRepositories);
            IList<string> groups = patchManager.GetPatchGroupNames(document.Patches);

            // Always add nameless group even when there are no child documents in it.
            groups.Add(null);

            foreach (string group in groups)
            {
                PatchGridViewModel viewModel = ViewModelSelector.TryGetPatchGridViewModel_ByGroup(MainViewModel.Document, group);
                if (viewModel == null)
                {
                    IList<Patch> patchesInGroup = patchManager.GetPatchesInGroup_IncludingGroupless(document.Patches, group);
                    IList<UsedInDto<Patch>> usedInDtos = _documentManager.GetUsedIn(patchesInGroup);

                    viewModel = usedInDtos.ToPatchGridViewModel(document.ID, group);
                    viewModel.Successful = true;

                    string canonicalGroup = NameHelper.ToCanonical(group);

                    viewModelDictionary[canonicalGroup] = viewModel;
                }
                else
                {
                    PatchGridRefresh(viewModel);
                }
            }

            // Delete operations
            string canonicalVisiblePatchGridGroup = NameHelper.ToCanonical(MainViewModel.Document.VisiblePatchGrid?.Group);

            IEnumerable<string> canonicalExistingGroups = viewModelDictionary.Keys.Select(x => NameHelper.ToCanonical(x));
            IEnumerable<string> canonicalGroupsToDelete = groups.Select(x => NameHelper.ToCanonical(x)).Except(canonicalExistingGroups);

            foreach (string canonicalGroupToDelete in canonicalGroupsToDelete)
            {
                viewModelDictionary.Remove(canonicalGroupToDelete);

                if (string.Equals(canonicalVisiblePatchGridGroup, canonicalGroupToDelete))
                {
                    MainViewModel.Document.VisiblePatchGrid = null;
                }
            }
        }

        private void PatchGridRefresh(PatchGridViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            PatchGridViewModel viewModel = _patchGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void PatchPropertiesDictionaryRefresh()
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
                    PatchPropertiesRefresh(viewModel);
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

        private void PatchPropertiesRefresh(PatchPropertiesViewModel userInput)
        {
            PatchPropertiesViewModel viewModel = _patchPropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void SampleGridRefresh()
        {
            SampleGridViewModel userInput = MainViewModel.Document.SampleGrid;
            SampleGridViewModel viewModel = _sampleGridPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void SampleLookupRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            MainViewModel.Document.SampleLookup = ViewModelHelper.CreateSampleLookupViewModel(document);
        }

        private void SamplePropertiesDictionaryRefresh()
        {
            // ReSharper disable once SuggestVarOrType_Elsewhere
            var viewModelDictionary = MainViewModel.Document.SamplePropertiesDictionary;

            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Sample> entities = document.Samples;

            foreach (Sample entity in entities)
            {
                SamplePropertiesViewModel viewModel = ViewModelSelector.TryGetSamplePropertiesViewModel(MainViewModel.Document, entity.ID);
                if (viewModel == null)
                {
                    viewModel = entity.ToPropertiesViewModel(_sampleRepositories);
                    viewModel.Successful = true;
                    viewModelDictionary[entity.ID] = viewModel;
                }
                else
                {
                    SamplePropertiesRefresh(viewModel);
                }
            }

            IEnumerable<int> existingIDs = viewModelDictionary.Keys;
            IEnumerable<int> idsToKeep = entities.Select(x => x.ID);
            IEnumerable<int> idsToDelete = existingIDs.Except(idsToKeep);

            foreach (int idToDelete in idsToDelete.ToArray())
            {
                viewModelDictionary.Remove(idToDelete);

                if (MainViewModel.Document.VisibleSampleProperties?.Entity.ID == idToDelete)
                {
                    MainViewModel.Document.VisibleSampleProperties = null;
                }
            }
        }

        private void SamplePropertiesRefresh(SamplePropertiesViewModel userInput)
        {
            SamplePropertiesViewModel viewModel = _samplePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ScaleGridRefresh()
        {
            // GetViewModel
            ScaleGridViewModel userInput = MainViewModel.Document.ScaleGrid;

            // Partial Action
            ScaleGridViewModel viewModel = _scaleGridPresenter.Refresh(userInput);

            // DispatchViewModel
            DispatchViewModel(viewModel);
        }

        private void ScalePropertiesDictionaryRefresh()
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
                    ScalePropertiesRefresh(viewModel);
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

        private void ScalePropertiesRefresh(ScalePropertiesViewModel userInput)
        {
            ScalePropertiesViewModel viewModel = _scalePropertiesPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void TitleBarRefresh()
        {
            int documentID = MainViewModel.Document.ID;
            Document document = _repositories.DocumentRepository.TryGet(documentID);
            string windowTitle = _titleBarPresenter.Show(document);
            MainViewModel.TitleBar = windowTitle;
        }

        private void ToneGridEditDictionaryRefresh()
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
                    ToneGridEditRefresh(viewModel);
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

        private void ToneGridEditRefresh(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = _toneGridEditPresenter.Refresh(userInput);
            DispatchViewModel(viewModel);
        }

        private void ToneGridEditRefresh(int scaleID)
        {
            ToneGridEditViewModel viewModel = ViewModelSelector.GetToneGridEditViewModel(MainViewModel.Document, scaleID);
            ToneGridEditRefresh(viewModel);
        }

        private void UnderylingPatchLookupRefresh()
        {
            Document document = _repositories.DocumentRepository.Get(MainViewModel.Document.ID);
            IList<Patch> patches = document.GetPatchesAndLowerDocumentPatches();
            MainViewModel.Document.UnderlyingPatchLookup = ViewModelHelper.CreateUnderlyingPatchLookupViewModel(patches);
        }
    }
}
