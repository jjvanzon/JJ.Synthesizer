using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.WinForms.Extensions;
using JJ.Presentation.Synthesizer.NAudio;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms
{
	internal partial class MainForm : Form
	{
		private const int MIN_TOP_BAR_HEIGHT = 16;

		private static readonly double _patchPlayDuration = CustomConfigurationManager.GetSection<ConfigurationSection>().PlayActionDurationInSeconds;
		private static readonly string _patchPlayOutputFilePath = CustomConfigurationManager.GetSection<ConfigurationSection>().PlayActionOutputFilePath;
		private static readonly bool _mustHandleMainFormActivated = CustomConfigurationManager.GetSection<ConfigurationSection>().MustHandleMainFormActivated;

		private readonly IContext _context;
		private readonly RepositoryWrapper _repositories;
		private readonly MainPresenter _mainPresenter;
		private readonly InfrastructureFacade _infrastructureFacade;
		private readonly AutoPatcher _autoPatcher;
		private readonly SystemFacade _systemFacade;

		private readonly AutoPatchPopupForm _autoPatchPopupForm = new AutoPatchPopupForm();
		private readonly DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
		private readonly LibrarySelectionPopupForm _librarySelectionPopupForm = new LibrarySelectionPopupForm();

		private readonly DelayedControlInvoker _infrastructureFacade_MidiDimensionValuesChanged_DelayedInvoker;
		private readonly DelayedControlInvoker _infrastructureFacade_MidiNoteOnOccurred_DelayedInvoker;
		private readonly DelayedControlInvoker _infrastructureFacade_ExceptionOnMidiThreadOcurred_DelayedInvoker;
		private readonly DelayedControlInvoker _infrastructureFacade_MidiControllerValueChanged_DelayedInvoker;

		public MainForm()
		{
			InitializeComponent();

			_userControls = CreateUserControlsCollection();
			_infrastructureFacade_MidiDimensionValuesChanged_DelayedInvoker = new DelayedControlInvoker(this);
			_infrastructureFacade_MidiNoteOnOccurred_DelayedInvoker = new DelayedControlInvoker(this);
			_infrastructureFacade_ExceptionOnMidiThreadOcurred_DelayedInvoker = new DelayedControlInvoker(this);
			_infrastructureFacade_MidiControllerValueChanged_DelayedInvoker = new DelayedControlInvoker(this);

			_context = PersistenceHelper.CreateContext();
			_repositories = PersistenceHelper.CreateRepositoryWrapper(_context);
			_mainPresenter = new MainPresenter(_repositories);

			_autoPatcher = new AutoPatcher(_repositories);

			var audioOutputFacade = new AudioOutputFacade(_repositories.AudioOutputRepository, _repositories.SpeakerSetupRepository, _repositories.IDRepository);
			_systemFacade = new SystemFacade(_repositories.DocumentRepository);
			_infrastructureFacade = new InfrastructureFacade(audioOutputFacade, _systemFacade, _repositories);

			curveDetailsListUserControl.SetCurveFacade(new CurveFacade(new CurveRepositories(_repositories)));

			BindEvents();
			ApplyStyling();
			PositionControls();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				components?.Dispose();
				_infrastructureFacade_MidiDimensionValuesChanged_DelayedInvoker?.Dispose();
				_infrastructureFacade_MidiNoteOnOccurred_DelayedInvoker?.Dispose();
				_infrastructureFacade_ExceptionOnMidiThreadOcurred_DelayedInvoker?.Dispose();
				_infrastructureFacade_MidiControllerValueChanged_DelayedInvoker?.Dispose();
				_infrastructureFacade?.Dispose();
				_context?.Dispose();
			}

			base.Dispose(disposing);
		}
		
		/// <param name="documentName">nullable</param>
		/// <param name="patchName">nullable</param>
		public void Show(string documentName, string patchName)
		{
			base.Show();

			TemplateActionHandler(
				() =>
				{
					_mainPresenter.Show(documentName, patchName);
					RecreatePatchCalculatorIfSuccessful();
				});
		}

		public void DocumentOpen(string name) => TemplateActionHandler(() => _mainPresenter.Document_Open(name));

		public void PatchShow(string patchName)
		{
			// TODO: Delegate to presenter layer.
			TemplateActionHandler(
				() =>
				{
					Document document = _repositories.DocumentRepository.Get(_mainPresenter.MainViewModel.Document.ID);
					Patch patch = document.Patches.Where(x => string.Equals(x.Name, patchName)).SingleWithClearException(new { patchName });
					_mainPresenter.PatchDetails_Show(patch.ID);
				});
		}

		// Helpers

		private void SetTreePanelVisible(bool visible)
		{
			splitContainerTreeAndRightSide.Panel1Collapsed = !visible;
		}

		private void SetPropertiesPanelVisible(bool visible)
		{
			splitContainerCenterAndProperties.Panel2Collapsed = !visible;
		}

		private void SetCurveDetailsPanelVisible(bool visible)
		{
			splitContainerCurvesAndTopSide.Panel2Collapsed = !visible;
		}

		private void ApplyStyling()
		{
			splitContainerCenterAndProperties.SplitterWidth = StyleHelper.SplitterWidth;
			splitContainerTreeAndRightSide.SplitterWidth = StyleHelper.SplitterWidth;
			splitContainerCurvesAndTopSide.SplitterWidth = StyleHelper.SplitterWidth;
		}

		private void ForceLoseFocus()
		{
			// Quite a hack, and making the method name lie,
			// but the ForceLoseFocus is there to make LoseFocus go off
			// when doing actions on controls that do not trigger a LoseFocus themselves (such as the Menu control).
			// The control that has to loose focus, is one in which you enter data (e.g. a Properties view).
			// No data is entered in the DocumentTree view at the moment,
			// so if another control is focused, focusing the DocumentTree control would do the trick.
			documentTreeUserControl.Focus();
		}

		private readonly IList<UserControlBase> _userControls;

		private IList<UserControlBase> CreateUserControlsCollection()
		{
			IList<UserControlBase> userControls = this.GetDescendantsOfType<UserControlBase>();
			return userControls;
		}

		// Audio

		private void SetAudioOutputIfNeeded()
		{
			if (!_mainPresenter.MainViewModel.Successful)
			{
				return;
			}

			AudioOutputPropertiesViewModel viewModel = _mainPresenter.MainViewModel.Document.AudioOutputProperties;

			if (viewModel.Successful)
			{
				UpdateInfrastructureIfSuccessful();
			}
		}

		private void RecreatePatchCalculatorIfSuccessful()
		{
			if (!_mainPresenter.MainViewModel.Successful)
			{
				return;
			}

			Patch patch = GetInstrumentPatch();

			_infrastructureFacade.RecreatePatchCalculator(patch);
		}

		private void UpdateInfrastructureIfSuccessful()
		{
			if (_mainPresenter.MainViewModel.Successful)
			{
				int audioOutputID = _mainPresenter.MainViewModel.Document.AudioOutputProperties.Entity.ID;

				AudioOutput audioOutput = _repositories.AudioOutputRepository.Get(audioOutputID);
				Patch patch = GetInstrumentPatch();

				Scale scale = _repositories.ScaleRepository.TryGet(_mainPresenter.MainViewModel.Document.InstrumentBar.Scale?.ID ?? default) ??
				              _systemFacade.GetDefaultScale();

				IList<MidiMapping> midiMappings = _mainPresenter.MainViewModel.Document.InstrumentBar.MidiMappingGroups
				                                                               .Select(x => _repositories.MidiMappingGroupRepository.Get(x.EntityID))
				                                                               .SelectMany(x => x.MidiMappings)
				                                                               .ToArray();
				if (midiMappings.Count == 0)
				{
					midiMappings = _systemFacade.GetDefaultMidiMappings();
				}

				_infrastructureFacade.UpdateInfrastructure(audioOutput, patch, scale, midiMappings);
			}
		}

		private Patch GetInstrumentPatch()
		{
			IList<Patch> patches = _mainPresenter.MainViewModel.Document.InstrumentBar.Patches
			                                     .Select(x => _repositories.PatchRepository.Get(x.EntityID))
			                                     .ToArray();
			if (patches.Count == 0)
			{
				patches = new[] { CreateDefaultSinePatch() };
			}

			Patch patch = _autoPatcher.AutoPatch(patches);

			return patch;
		}

		private Patch CreateDefaultSinePatch()
		{
			var patchFacade = new PatchFacade(_repositories);
			Patch patch = patchFacade.CreatePatch();

			var x = new OperatorFactory(patch, _repositories);

			x.PatchOutlet
			(
				DimensionEnum.Signal,
				x.Multiply
				(
					x.Sine
					(
						x.PatchInlet(DimensionEnum.Frequency)
					),
					x.PatchInlet(DimensionEnum.Volume)
				)
			);

			// This makes side-effects go off.
			VoidResult result = patchFacade.SavePatch(patch);
			// ReSharper disable once InvertIf
			if (!result.Successful)
			{
				string formattedMessages = ResultHelper.FormatMessages(result);
				throw new Exception(formattedMessages);
			}

			return patch;
		}

		private void PlayOutletIfNeeded()
		{
			if (!_mainPresenter.MainViewModel.Successful)
			{
				return;
			}

			int? outletIDToPlay = _mainPresenter.MainViewModel.Document.OutletIDToPlay;
			if (!outletIDToPlay.HasValue)
			{
				return;
			}

			// ToViewModel
			_mainPresenter.MainViewModel.Document.OutletIDToPlay = null;

			// GetEntities
			Outlet outlet = _repositories.OutletRepository.Get(outletIDToPlay.Value);

			// Determine AudioOutput
			Document document;
			if (_mainPresenter.MainViewModel.Document.IsOpen)
			{
				// Get AudioOutput from open document.
				document = _repositories.DocumentRepository.TryGet(_mainPresenter.MainViewModel.Document.ID);
			}
			else
			{
				// Otherwise take the AudioOutput from the document the outlet is part of.
				if (outlet.Operator.Patch == null) throw new NullException(() => outlet.Operator.Patch);
				// ReSharper disable once JoinNullCheckWithUsage
				if (outlet.Operator.Patch.Document == null) throw new NullException(() => outlet.Operator.Patch.Document);
				document = outlet.Operator.Patch.Document;
			}
			AudioOutput audioOutput = document.AudioOutput;

			// Calculate
			var patchFacade = new PatchFacade(_repositories);
			var calculatorCache = new CalculatorCache();
			int channelCount = audioOutput.GetChannelCount();
			var patchCalculators = new IPatchCalculator[channelCount];
			for (int i = 0; i < channelCount; i++)
			{
				patchCalculators[i] = patchFacade.CreateCalculator(
					outlet,
					audioOutput.SamplingRate,
					channelCount,
					i,
					calculatorCache);
			}

			// Write Output File
			var audioFileOutputFacade = new AudioFileOutputFacade(new AudioFileOutputRepositories(_repositories));
			AudioFileOutput audioFileOutput = audioFileOutputFacade.Create();
			audioFileOutput.LinkTo(audioOutput.SpeakerSetup);
			audioFileOutput.SamplingRate = audioOutput.SamplingRate;
			audioFileOutput.FilePath = _patchPlayOutputFilePath;
			audioFileOutput.Duration = _patchPlayDuration;
			audioFileOutput.LinkTo(outlet);

			// Infrastructure
			audioFileOutputFacade.WriteFile(audioFileOutput, patchCalculators);
			var soundPlayer = new SoundPlayer(_patchPlayOutputFilePath);
			soundPlayer.Play();
		}

		private void OpenDocumentExternallyAndOptionallyPatchIfNeeded()
		{
			IDAndName documentToOpenExternally = _mainPresenter.MainViewModel.Document.DocumentToOpenExternally;
			IDAndName patchToOpenExternally = _mainPresenter.MainViewModel.Document.PatchToOpenExternally;

			// Infrastructure
			string documentName = documentToOpenExternally?.Name;
			string patchName = patchToOpenExternally?.Name;

			if (!string.IsNullOrEmpty(documentName))
			{
				Program.ShowMainWindow(documentName, patchName);

				//string arguments = $@"""{documentName}""";

				//if (!string.IsNullOrEmpty(patchName))
				//{
				//	arguments += $@" ""{patchName}""";
				//}

				//Process.Start(Assembly.GetExecutingAssembly().Location, arguments);
			}

			// ToViewModel
			_mainPresenter.MainViewModel.Document.DocumentToOpenExternally = null;
			_mainPresenter.MainViewModel.Document.PatchToOpenExternally = null;
		}

		private void MainForm_SizeChanged(object sender, EventArgs e) => PositionControls();

		private void PositionControls()
		{
			instrumentBarUserControl.Width = ClientSize.Width - instrumentBarUserControl.Location.X;
			instrumentBarUserControl.PositionControls();

			monitoringBarUserControl.Width = ClientSize.Width;
			monitoringBarUserControl.PositionControls();
			monitoringBarUserControl.Left = 0;
			monitoringBarUserControl.Top = ClientSize.Height - monitoringBarUserControl.Height;

			int topBarHeight = Math.Max(instrumentBarUserControl.Height, MIN_TOP_BAR_HEIGHT);

			splitContainerCurvesAndTopSide.Left = 0;
			splitContainerCurvesAndTopSide.Top = topBarHeight;
			splitContainerCurvesAndTopSide.Height = ClientSize.Height - topBarHeight - monitoringBarUserControl.Height;
			splitContainerCurvesAndTopSide.Width = ClientSize.Width;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{

		}
	}
}
