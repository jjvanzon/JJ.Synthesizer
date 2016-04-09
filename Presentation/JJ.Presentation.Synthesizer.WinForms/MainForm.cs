using System;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using ConfigurationSection = JJ.Presentation.Synthesizer.WinForms.Configuration.ConfigurationSection;
using System.ComponentModel;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private RepositoryWrapper _repositories;
        private MainPresenter _presenter;

        private DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
        private PatchDetailsForm _autoPatchDetailsForm = new PatchDetailsForm();
        private readonly static int _maxConcurrentNotes;

        static MainForm()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                var config = CustomConfigurationManager.GetSection<ConfigurationSection>();
                _maxConcurrentNotes = config.MaxConcurrentNotes;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            _userControlTuples = CreateUserControlTuples();

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

        private void RecreatePatchCalculator()
        {
            IList<Patch> patches = _presenter.MainViewModel.Document.CurrentPatches.List
                                                                .Select(x => _repositories.DocumentRepository.Get(x.ChildDocumentID))
                                                                .Select(x => x.Patches.Single())
                                                                .ToArray();
            if (patches.Count == 0)
            {
                patches = new Patch[] { CreateDefaultSinePatch() };
            }

            Program.PatchCalculatorContainer.RecreateCalculator(patches, _maxConcurrentNotes, new PatchRepositories(_repositories));
        }

        private Patch CreateDefaultSinePatch()
        {
            var x = new PatchManager(new PatchRepositories(_repositories));
            x.CreatePatch();

            var signalOutlet = x.PatchOutlet
            (
                OutletTypeEnum.Signal,
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
            VoidResult result = x.SavePatch();
            if (!result.Successful)
            {
                // TODO: Make a distinction between Data.Canonical and Business.Canonical, so that you have a place to put helpers for this.
                string formattedMessages = String.Join(Environment.NewLine, result.Messages.Select(y => y.Text));
                throw new Exception(formattedMessages);
            }

            return x.Patch;
        }

        private UserControlTuple[] _userControlTuples;

        private class UserControlTuple
        {
            public UserControlTuple(UserControlBase userControl, bool isPropertiesView = false)
            {
                UserControl = userControl;
                IsPropertiesView = isPropertiesView;
            }

            public UserControlBase UserControl { get; private set; }
            public bool IsPropertiesView { get; private set; }
        }

        // TODO: Refactor out these tuples, after making a PropertiesViewModel base class.
        private UserControlTuple[] CreateUserControlTuples()
        {
            return new UserControlTuple[]
            {
                new UserControlTuple(audioFileOutputGridUserControl),
                new UserControlTuple(audioFileOutputPropertiesUserControl, isPropertiesView: true),
                new UserControlTuple(curveDetailsUserControl),
                new UserControlTuple(curveGridUserControl),
                new UserControlTuple(curvePropertiesUserControl, isPropertiesView: true),
                new UserControlTuple(documentDetailsUserControl),
                new UserControlTuple(documentGridUserControl),
                new UserControlTuple(documentPropertiesUserControl, isPropertiesView: true),
                new UserControlTuple(documentTreeUserControl),
                new UserControlTuple(nodePropertiesUserControl, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForBundle, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForCache, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForCurve, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForCustomOperator, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForFilter, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForNumber, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForPatchInlet, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForPatchOutlet, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForRandom, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForResample, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForSample, isPropertiesView: true),
                new UserControlTuple(operatorPropertiesUserControl_ForUnbundle, isPropertiesView: true),
                new UserControlTuple(patchGridUserControl),
                new UserControlTuple(patchDetailsUserControl),
                new UserControlTuple(patchPropertiesUserControl, isPropertiesView: true),
                new UserControlTuple(sampleGridUserControl),
                new UserControlTuple(samplePropertiesUserControl, isPropertiesView: true),
                new UserControlTuple(scaleGridUserControl),
                new UserControlTuple(toneGridEditUserControl),
                new UserControlTuple(scalePropertiesUserControl, isPropertiesView: true)
            };
        }
    }
}
