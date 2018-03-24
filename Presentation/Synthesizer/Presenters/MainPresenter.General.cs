using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.Presenters.Partials;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.Presenters
{
	/// <summary>
	/// Panels in the application are so intricately coordinated
	/// that one action in one part of the screen can
	/// affect several other panels on the screen.
	/// So you cannot manage each part of the screen individually.
	/// 
	/// That is why all panels are managed in a single presenter and view model.
	/// Otherwise you would get difficult coordination of application navigation 
	/// in the platform-specific application code, where it does not belong.
	/// 
	/// Also: most non-visible parts of the view model must be kept alive inside the view model,
	/// because the whole document will not be persistent until you hit save,
	/// and until that time, all the data must be kept inside the view model.
	/// </summary>
	public partial class MainPresenter
	{
		private const int DEFAULT_ADD_INLET_COUNT = 2;
		private const int DEFAULT_AGGREGATE_OVER_INLETS_INLET_COUNT = 3;
		private const int DEFAULT_RANGE_OVER_OUTLETS_OUTLET_COUNT = 16;
		private const int DEFAULT_MULTIPLY_INLET_COUNT = 2;
		private const int DEFAULT_SORT_OVER_INLETS_INLET_COUNT = 8;
		private const int DEFAULT_VARIABLE_INLET_OR_OUTLET_COUNT = 16;

		// TODO: These two constants do not belong here, because they should be determined by the vector graphics.
		private const float ESTIMATED_OPERATOR_WIDTH = 50f;
		private const float OPERATOR_HEIGHT = 30f;

		private readonly RepositoryWrapper _repositories;
		private readonly CurveRepositories _curveRepositories;

		private readonly AudioFileOutputGridPresenter _audioFileOutputGridPresenter;
		private readonly AudioFileOutputPropertiesPresenter _audioFileOutputPropertiesPresenter;
		private readonly AudioOutputPropertiesPresenter _audioOutputPropertiesPresenter;
		private readonly CurrentInstrumentBarPresenter _currentInstrumentBarPresenter;
		private readonly CurveDetailsPresenter _curveDetailsPresenter;
		private readonly DocumentCannotDeletePresenter _documentCannotDeletePresenter;
		private readonly DocumentDeletedPresenter _documentDeletedPresenter;
		private readonly DocumentDeletePresenter _documentDeletePresenter;
		private readonly DocumentDetailsPresenter _documentDetailsPresenter;
		private readonly DocumentGridPresenter _documentGridPresenter;
		private readonly DocumentOrPatchNotFoundPopupPresenter _documentOrPatchNotFoundPresenter;
		private readonly DocumentPropertiesPresenter _documentPropertiesPresenter;
		private readonly DocumentTreePresenter _documentTreePresenter;
		private readonly LibraryPropertiesPresenter _libraryPropertiesPresenter;
		private readonly LibrarySelectionPopupPresenter _librarySelectionPopupPresenter;
		private readonly MenuPresenter _menuPresenter;
		private readonly MidiMappingDetailsPresenter _midiMappingDetailsPresenter;
		private readonly MidiMappingElementPropertiesPresenter _midiMappingElementPropertiesPresenter;
		private readonly NodePropertiesPresenter _nodePropertiesPresenter;
		private readonly OperatorPropertiesPresenter _operatorPropertiesPresenter;
		private readonly OperatorPropertiesPresenter_ForCache _operatorPropertiesPresenter_ForCache;
		private readonly OperatorPropertiesPresenter_ForCurve _operatorPropertiesPresenter_ForCurve;
		private readonly OperatorPropertiesPresenter_ForInletsToDimension _operatorPropertiesPresenter_ForInletsToDimension;
		private readonly OperatorPropertiesPresenter_ForNumber _operatorPropertiesPresenter_ForNumber;
		private readonly OperatorPropertiesPresenter_ForPatchInlet _operatorPropertiesPresenter_ForPatchInlet;
		private readonly OperatorPropertiesPresenter_ForPatchOutlet _operatorPropertiesPresenter_ForPatchOutlet;
		private readonly OperatorPropertiesPresenter_ForSample _operatorPropertiesPresenter_ForSample;
		private readonly OperatorPropertiesPresenter_WithInterpolation _operatorPropertiesPresenter_WithInterpolation;
		private readonly OperatorPropertiesPresenter_WithCollectionRecalculation _operatorPropertiesPresenter_WithCollectionRecalculation;
		private readonly PatchDetailsPresenter _patchDetailsPresenter;
		private readonly PatchPropertiesPresenter _patchPropertiesPresenter;
		private readonly SampleFileBrowserPresenter _sampleFileBrowserPresenter;
		private readonly SaveChangesPopupPresenter _saveChangesPopupPresenter;
		private readonly ScaleGridPresenter _scaleGridPresenter;
		private readonly ScalePropertiesPresenter _scalePropertiesPresenter;
		private readonly ToneGridEditPresenter _toneGridEditPresenter;
		private readonly TitleBarPresenter _titleBarPresenter;

		private readonly AutoPatcher _autoPatcher;
		private readonly AudioFileOutputFacade _audioFileOutputFacade;
		private readonly CurveFacade _curveFacade;
		private readonly DocumentFacade _documentFacade;
		private readonly MidiMappingFacade _midiMappingFacade;
		private readonly PatchFacade _patchFacade;
		private readonly ScaleFacade _scaleFacade;
		private readonly SystemFacade _systemFacade;

		public MainViewModel MainViewModel { get; private set; }

		public MainPresenter(RepositoryWrapper repositories)
		{
			// Create Repositories
			_repositories = repositories ?? throw new NullException(() => repositories);
			var audioFileOutputRepositories = new AudioFileOutputRepositories(_repositories);
			_curveRepositories = new CurveRepositories(_repositories);
			var midiMappingRepositories = new MidiMappingRepositories(_repositories);
			var scaleRepositories = new ScaleRepositories(_repositories);

			// Create Facades
			_autoPatcher = new AutoPatcher(_repositories);
			_audioFileOutputFacade = new AudioFileOutputFacade(audioFileOutputRepositories);
			_curveFacade = new CurveFacade(_curveRepositories);
			_documentFacade = new DocumentFacade(_repositories);
			_midiMappingFacade = new MidiMappingFacade(midiMappingRepositories);
			_patchFacade = new PatchFacade(_repositories);
			_scaleFacade = new ScaleFacade(scaleRepositories);
			_systemFacade = new SystemFacade(_repositories.DocumentRepository);

			// Create Presenters
			_audioFileOutputGridPresenter = new AudioFileOutputGridPresenter(_audioFileOutputFacade, _repositories.DocumentRepository);
			_audioFileOutputPropertiesPresenter = new AudioFileOutputPropertiesPresenter(_audioFileOutputFacade, _repositories.AudioFileOutputRepository);
			_audioOutputPropertiesPresenter = new AudioOutputPropertiesPresenter(
				_repositories.AudioOutputRepository,
				_repositories.SpeakerSetupRepository,
				_repositories.IDRepository);
			_currentInstrumentBarPresenter = new CurrentInstrumentBarPresenter(_autoPatcher, _systemFacade, _repositories.DocumentRepository, _repositories.PatchRepository);
			_curveDetailsPresenter = new CurveDetailsPresenter(_repositories.CurveRepository, _repositories.NodeRepository, _curveFacade);
			_documentCannotDeletePresenter = new DocumentCannotDeletePresenter(_repositories.DocumentRepository);
			_documentDeletedPresenter = new DocumentDeletedPresenter();
			_documentDeletePresenter = new DocumentDeletePresenter(_repositories);
			_documentDetailsPresenter = new DocumentDetailsPresenter(_repositories);
			_documentGridPresenter = new DocumentGridPresenter(_repositories);
			_documentOrPatchNotFoundPresenter = new DocumentOrPatchNotFoundPopupPresenter(_repositories.DocumentRepository);
			_documentPropertiesPresenter = new DocumentPropertiesPresenter(_repositories);
			_documentTreePresenter = new DocumentTreePresenter(_documentFacade, _patchFacade, _midiMappingFacade, _repositories);
			_libraryPropertiesPresenter = new LibraryPropertiesPresenter(_repositories);
			_librarySelectionPopupPresenter = new LibrarySelectionPopupPresenter(_repositories);
			_menuPresenter = new MenuPresenter();
			_midiMappingDetailsPresenter = new MidiMappingDetailsPresenter(midiMappingRepositories, _midiMappingFacade);
			_midiMappingElementPropertiesPresenter = new MidiMappingElementPropertiesPresenter(midiMappingRepositories, _midiMappingFacade);
			_nodePropertiesPresenter = new NodePropertiesPresenter(_repositories.NodeRepository, _curveFacade);
			_operatorPropertiesPresenter = new OperatorPropertiesPresenter(_repositories);
			_operatorPropertiesPresenter_ForCache = new OperatorPropertiesPresenter_ForCache(_repositories);
			_operatorPropertiesPresenter_ForCurve = new OperatorPropertiesPresenter_ForCurve(_repositories);
			_operatorPropertiesPresenter_ForInletsToDimension = new OperatorPropertiesPresenter_ForInletsToDimension(_repositories);
			_operatorPropertiesPresenter_ForNumber = new OperatorPropertiesPresenter_ForNumber(_repositories);
			_operatorPropertiesPresenter_ForPatchInlet = new OperatorPropertiesPresenter_ForPatchInlet(_repositories);
			_operatorPropertiesPresenter_ForPatchOutlet = new OperatorPropertiesPresenter_ForPatchOutlet(_repositories);
			_operatorPropertiesPresenter_ForSample = new OperatorPropertiesPresenter_ForSample(_repositories);
			_operatorPropertiesPresenter_WithInterpolation = new OperatorPropertiesPresenter_WithInterpolation(_repositories);
			_operatorPropertiesPresenter_WithCollectionRecalculation = new OperatorPropertiesPresenter_WithCollectionRecalculation(_repositories);
			_patchDetailsPresenter = new PatchDetailsPresenter(_repositories);
			_patchPropertiesPresenter = new PatchPropertiesPresenter(_repositories);
			_sampleFileBrowserPresenter = new SampleFileBrowserPresenter(_autoPatcher, _repositories);
			_saveChangesPopupPresenter = new SaveChangesPopupPresenter();
			_scaleGridPresenter = new ScaleGridPresenter(_repositories.DocumentRepository, _scaleFacade);
			_scalePropertiesPresenter = new ScalePropertiesPresenter(_repositories.ScaleRepository, _scaleFacade);
			_toneGridEditPresenter = new ToneGridEditPresenter(_repositories.ScaleRepository, _scaleFacade);
			_titleBarPresenter = new TitleBarPresenter();

			_dispatchDelegateDictionary = CreateDispatchDelegateDictionary();
		}

		// Helpers

		private void HideAllGridAndDetailsViewModels()
		{
			MainViewModel.Document.AudioFileOutputGrid.Visible = false;
			MainViewModel.Document.VisibleMidiMappingDetails = null;
			MainViewModel.Document.MidiMappingDetailsDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisiblePatchDetails = null;
			MainViewModel.Document.PatchDetailsDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.ScaleGrid.Visible = false;
			MainViewModel.Document.VisibleToneGridEdit = null;
			MainViewModel.Document.ToneGridEditDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.DocumentDetails.Visible = false;
			MainViewModel.DocumentGrid.Visible = false;
		}

		private void HideAllPropertiesViewModels()
		{
			MainViewModel.DocumentDetails.Visible = false;
			MainViewModel.Document.AudioOutputProperties.Visible = false;
			MainViewModel.Document.VisibleAudioFileOutputProperties = null;
			MainViewModel.Document.AudioFileOutputPropertiesDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.DocumentProperties.Visible = false;
			MainViewModel.Document.VisibleNodeProperties = null;
			MainViewModel.Document.NodePropertiesDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleLibraryProperties = null;
			MainViewModel.Document.LibraryPropertiesDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleMidiMappingElementProperties = null;
			MainViewModel.Document.MidiMappingElementPropertiesDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties = null;
			MainViewModel.Document.OperatorPropertiesDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_ForCache = null;
			MainViewModel.Document.OperatorPropertiesDictionary_ForCaches.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_ForCurve = null;
			MainViewModel.Document.OperatorPropertiesDictionary_ForCurves.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_ForInletsToDimension = null;
			MainViewModel.Document.OperatorPropertiesDictionary_ForInletsToDimension.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_ForNumber = null;
			MainViewModel.Document.OperatorPropertiesDictionary_ForNumbers.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_ForPatchInlet = null;
			MainViewModel.Document.OperatorPropertiesDictionary_ForPatchInlets.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_ForPatchOutlet = null;
			MainViewModel.Document.OperatorPropertiesDictionary_ForPatchOutlets.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_ForSample = null;
			MainViewModel.Document.OperatorPropertiesDictionary_ForSamples.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_WithCollectionRecalculation = null;
			MainViewModel.Document.OperatorPropertiesDictionary_WithCollectionRecalculation.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleOperatorProperties_WithInterpolation = null;
			MainViewModel.Document.OperatorPropertiesDictionary_WithInterpolation.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisiblePatchProperties = null;
			MainViewModel.Document.PatchPropertiesDictionary.Values.ForEach(x => x.Visible = false);
			MainViewModel.Document.VisibleScaleProperties = null;
			MainViewModel.Document.ScalePropertiesDictionary.Values.ForEach(x => x.Visible = false);
		}

		private IOperatorPropertiesPresenter GetOperatorPropertiesPresenter(int id)
		{
			Operator entity = _repositories.OperatorRepository.Get(id);
			OperatorTypeEnum operatorTypeEnum = entity.GetOperatorTypeEnum();

			switch (operatorTypeEnum)
			{
				case OperatorTypeEnum.Cache:
					return _operatorPropertiesPresenter_ForCache;

				case OperatorTypeEnum.Curve:
					return _operatorPropertiesPresenter_ForCurve;

				case OperatorTypeEnum.InletsToDimension:
					return _operatorPropertiesPresenter_ForInletsToDimension;

				case OperatorTypeEnum.Number:
					return _operatorPropertiesPresenter_ForNumber;

				case OperatorTypeEnum.PatchInlet:
					return _operatorPropertiesPresenter_ForPatchInlet;

				case OperatorTypeEnum.PatchOutlet:
					return _operatorPropertiesPresenter_ForPatchOutlet;

				case OperatorTypeEnum.Sample:
					return _operatorPropertiesPresenter_ForSample;
			}

			if (ToViewModelHelper.OperatorTypeEnums_WithCollectionRecalculationPropertyViews.Contains(operatorTypeEnum))
			{
				return _operatorPropertiesPresenter_WithCollectionRecalculation;
			}

			if (ToViewModelHelper.OperatorTypeEnums_WithInterpolationPropertyViews.Contains(operatorTypeEnum))
			{
				return _operatorPropertiesPresenter_WithInterpolation;
			}

			return _operatorPropertiesPresenter;
		}

		private int GetVariableInletOrOutletCount(Patch patch)
		{
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.Add))) return DEFAULT_ADD_INLET_COUNT;

			// Temporarily changed (2017-07-09), because of bug in InletOutletMatcher / assumption in PatchFacade.SetOperatorInletCount,
			// which results in the message that inlets and inputs do not have the same count.
			//if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.ClosestOverInlets))) return DEFAULT_CLOSEST_OVER_INLETS_ITEM_COUNT;
			//if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.ClosestOverInletsExp))) return DEFAULT_CLOSEST_OVER_INLETS_ITEM_COUNT;
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.ClosestOverInlets))) return 1;
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.ClosestOverInletsExp))) return 1;
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.RangeOverOutlets))) return DEFAULT_RANGE_OVER_OUTLETS_OUTLET_COUNT;
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.Multiply))) return DEFAULT_MULTIPLY_INLET_COUNT;
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.MaxOverInlets))) return DEFAULT_AGGREGATE_OVER_INLETS_INLET_COUNT;
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.MinOverInlets))) return DEFAULT_AGGREGATE_OVER_INLETS_INLET_COUNT;
			if (NameHelper.AreEqual(patch.Name, nameof(SystemPatchNames.SortOverInlets))) return DEFAULT_SORT_OVER_INLETS_INLET_COUNT;

			return DEFAULT_VARIABLE_INLET_OR_OUTLET_COUNT;
		}

		private int GetOperatorIDByCurveID(int curveID)
		{
			// Get operator ID using view model, because you cannot reliably use the entity model to get an Operator by CurveID.
			// (Long explanation:
			//  It would require an ORM query, which only works for flushed entities.
			//  And you require an ORM query, because it Operator.Curve does not have an inverse property Curve.Operator.
			//  And the inverse property is not there, because inverse properties are hacky for 1-to-1 relationships with ORM.
			//  And an intermediate flush would not work, if the there are integrity problems, that cannot be persisted to the database.)
			OperatorPropertiesViewModel_ForCurve propertiesViewModel = ViewModelSelector.GetOperatorPropertiesViewModel_ForCurve_ByCurveID(MainViewModel.Document, curveID);
			int operatorID = propertiesViewModel.ID;
			return operatorID;
		}

		/// <summary> Includes the owned operators' IDs. </summary>
		private IList<int> GetOperatorIDsToDelete(int patchID, int? operatorID)
		{
			if (!operatorID.HasValue)
			{
				return new int[0];
			}

			if (operatorID == 0)
			{
				return new int[0];
			}

			OperatorViewModel operatorViewModel = ViewModelSelector.GetOperatorViewModel(MainViewModel.Document, patchID, operatorID.Value);

			IEnumerable<int> ownedOperatorIDs = operatorViewModel.Inlets
			                                                     .Where(x => x.InputOutlet != null)
			                                                     .Select(x => x.InputOutlet.Operator)
			                                                     .Where(x => x.IsOwned)
			                                                     .Select(x => x.ID);

			// Put main operator last so it is dispatched last upon undo and put on top.
			// Yes, this is the non-written agreement anti-pattern, but whatever.
			IList<int> operatorIDsToDelete = ownedOperatorIDs.Union(operatorID.Value).ToArray();

			return operatorIDsToDelete;
		}
	}
}