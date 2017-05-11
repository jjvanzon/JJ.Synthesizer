using System;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Data;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Reflection;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Configuration;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private static readonly double _patchPlayDuration = CustomConfigurationManager.GetSection<ConfigurationSection>().PlayActionDurationInSeconds;
        private static readonly string _patchPlayOutputFilePath = CustomConfigurationManager.GetSection<ConfigurationSection>().PlayActionOutputFilePath;

        private readonly IContext _context;
        private readonly RepositoryWrapper _repositories;
        private readonly MainPresenter _presenter;

        private readonly DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
        private readonly AutoPatchPopupForm _autoPatchPopupForm = new AutoPatchPopupForm();
        private readonly LibrarySelectionPopupForm _librarySelectionPopupForm = new LibrarySelectionPopupForm();

        public MainForm()
        {
            InitializeComponent();

            _userControls = CreateUserControlsCollection();

            _context = PersistenceHelper.CreateContext();
            _repositories = PersistenceHelper.CreateRepositoryWrapper(_context);
            _presenter = new MainPresenter(_repositories);

            BindEvents();
            ApplyStyling();
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
                    _presenter.Show(documentName, patchName);
                    RecreatePatchCalculator();
                });
        }

        public void DocumentOpen(string name) => TemplateActionHandler(() => _presenter.DocumentOpen(name));

        // Helpers

        private void SetTreePanelVisible(bool visible)
        {
            splitContainerTree.Panel1Collapsed = !visible;
        }

        private void SetPropertiesPanelVisible(bool visible)
        {
            splitContainerProperties.Panel2Collapsed = !visible;
        }

        private void ApplyStyling()
        {
            splitContainerProperties.SplitterWidth = StyleHelper.DefaultSpacing;
            splitContainerTree.SplitterWidth = StyleHelper.DefaultSpacing;
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
            AudioOutputPropertiesViewModel viewModel = _presenter.MainViewModel.Document.AudioOutputProperties;

            // ReSharper disable once InvertIf
            if (viewModel.Successful)
            {
                AudioOutput audioOutput = _repositories.AudioOutputRepository.Get(viewModel.Entity.ID);
                Program.SetAudioOutput(audioOutput);

                // Right the patch calculator must be recreated too,
                // because the PatchCalculatorContainer has a reference to a NoteRecycler,
                // that has to be replaced. Therefore the Program.SetAudioOutput() replaced the
                // PatchCalculatorContainer and now the patch calculator in it is lost.
                RecreatePatchCalculator();
            }
        }

        private void RecreatePatchCalculator()
        {
            IList<Patch> patches = _presenter.MainViewModel.Document.CurrentInstrument.List
                                             .Select(x => _repositories.PatchRepository.Get(x.ID))
                                             .ToArray();
            if (patches.Count == 0)
            {
                patches = new[] { CreateDefaultSinePatch() };
            }

            Program.PatchCalculatorContainer.RecreateCalculator(patches, Program.AudioOutput, _repositories);
        }

        private Patch CreateDefaultSinePatch()
        {
            var x = new PatchManager(new PatchRepositories(_repositories));
            x.CreatePatch();

            x.PatchOutlet
            (
                DimensionEnum.Signal,
                x.MultiplyWithOrigin
                (
                    x.Sine
                    (
                        x.PatchInlet(DimensionEnum.Frequency)
                    ),
                    x.PatchInlet(DimensionEnum.Volume)
                )
            );

            // This makes side-effects go off.
            VoidResultDto result = x.SavePatch();
            // ReSharper disable once InvertIf
            if (!result.Successful)
            {
                // TODO: Make a distinction between Data.Canonical and Business.Canonical, so that you have a place to put helpers for this.
                string formattedMessages = string.Join(Environment.NewLine, result.Messages.Select(y => y.Text));
                throw new Exception(formattedMessages);
            }

            return x.Patch;
        }

        private void PlayOutletIfNeeded()
        {
            int? outletIDToPlay = _presenter.MainViewModel.Document.OutletIDToPlay;
            if (!outletIDToPlay.HasValue)
            {
                return;
            }

            // Get Entities
            Outlet outlet = _repositories.OutletRepository.Get(outletIDToPlay.Value);

            // Determine AudioOutput
            Document document;
            if (_presenter.MainViewModel.Document.IsOpen)
            {
                // Get AudioOutput from open document.
                document = _repositories.DocumentRepository.TryGet(_presenter.MainViewModel.Document.ID);
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
            var patchManager = new PatchManager(outlet.Operator.Patch, new PatchRepositories(_repositories));
            var calculatorCache = new CalculatorCache();
            int channelCount = audioOutput.GetChannelCount();
            var patchCalculators = new IPatchCalculator[channelCount];
            for (int i = 0; i < channelCount; i++)
            {
                patchCalculators[i] = patchManager.CreateCalculator(
                    outlet,
                    audioOutput.SamplingRate,
                    channelCount,
                    i,
                    calculatorCache);
            }

            // Write Output File
            var audioFileOutputManager = new AudioFileOutputManager(new AudioFileOutputRepositories(_repositories));
            AudioFileOutput audioFileOutput = audioFileOutputManager.Create();
            audioFileOutput.LinkTo(audioOutput.SpeakerSetup);
            audioFileOutput.SamplingRate = audioOutput.SamplingRate;
            audioFileOutput.FilePath = _patchPlayOutputFilePath;
            audioFileOutput.Duration = _patchPlayDuration;
            audioFileOutput.LinkTo(outlet);

            // Infrastructure
            audioFileOutputManager.WriteFile(audioFileOutput, patchCalculators);
            var soundPlayer = new SoundPlayer(_patchPlayOutputFilePath);
            soundPlayer.Play();

            _presenter.MainViewModel.Document.OutletIDToPlay = null;
        }

        private void OpenDocumentExternallyAndOptionallyPatchIfNeeded()
        {
            IDAndName documentToOpenExternally = _presenter.MainViewModel.Document.DocumentToOpenExternally;
            IDAndName patchToOpenExternally = _presenter.MainViewModel.Document.PatchToOpenExternally;

            // Infrastructure
            string documentName = documentToOpenExternally?.Name;
            string patchName = patchToOpenExternally?.Name;

            // ReSharper disable once InvertIf
            if (!string.IsNullOrEmpty(documentName))
            {
                string arguments = $@"""{documentName}""";

                if (!string.IsNullOrEmpty(patchName))
                {
                    arguments += $@" ""{patchName}""";
                }

                Process.Start(Assembly.GetExecutingAssembly().Location, arguments);

                // ToViewModel
                _presenter.MainViewModel.Document.DocumentToOpenExternally = null;
                _presenter.MainViewModel.Document.PatchToOpenExternally = null;
            }
        }
    }
}
