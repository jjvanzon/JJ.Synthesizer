using System;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Data;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private RepositoryWrapper _repositories;
        private MainPresenter _presenter;

        private DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
        private PatchDetailsForm _autoPatchDetailsForm = new PatchDetailsForm();

        public MainForm()
        {
            InitializeComponent();

            _userControls = CreateUserControlsCollection();

            _context = PersistenceHelper.CreateContext();
            _repositories = PersistenceHelper.CreateRepositoryWrapper(_context);
            _presenter = new MainPresenter(_repositories);

            BindEvents();
            ApplyStyling();

            try
            {
                _presenter.Show();
                ApplyViewModel();

                RecreatePatchCalculator();
            }
            finally
            {
                _context.Rollback();
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (_context != null) _context.Dispose();
            }

            base.Dispose(disposing);
        }

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

        // Audio

        private void SetAudioOutputIfNeeded()
        {
            AudioOutputPropertiesViewModel viewModel = _presenter.MainViewModel.Document.AudioOutputProperties;

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
            IList<Patch> patches = _presenter.MainViewModel.Document.CurrentPatches.List
                                             .Select(x => _repositories.PatchRepository.Get(x.ID))
                                             .ToArray();
            if (patches.Count == 0)
            {
                patches = new Patch[] { CreateDefaultSinePatch() };
            }

            Program.PatchCalculatorContainer.RecreateCalculator(patches, Program.AudioOutput, _repositories);
        }

        private Patch CreateDefaultSinePatch()
        {
            var x = new PatchManager(new PatchRepositories(_repositories));
            x.CreatePatch();

            var signalOutlet = x.PatchOutlet
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
            VoidResult result = x.SavePatch();
            if (!result.Successful)
            {
                // TODO: Make a distinction between Data.Canonical and Business.Canonical, so that you have a place to put helpers for this.
                string formattedMessages = String.Join(Environment.NewLine, result.Messages.Select(y => y.Text));
                throw new Exception(formattedMessages);
            }

            return x.Patch;
        }

        private IList<UserControlBase> _userControls;

        private IList<UserControlBase> CreateUserControlsCollection()
        {
            IList<UserControlBase> userControls = this.GetDescendantsOfType<UserControlBase>();
            return userControls;
        }
    }
}
