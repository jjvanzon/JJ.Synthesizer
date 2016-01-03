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
using JJ.Infrastructure.Synthesizer;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Enums;
using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private RepositoryWrapper _repositories;
        private MainPresenter _presenter;

        private DocumentCannotDeleteForm _documentCannotDeleteForm = new DocumentCannotDeleteForm();
        private PatchDetailsForm _autoPatchDetailsForm = new PatchDetailsForm();
        private static string _titleBarExtraText;

        static MainForm()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                var config = CustomConfigurationManager.GetSection<ConfigurationSection>();
                _titleBarExtraText = config.TitleBarExtraText;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();
            _repositories = PersistenceHelper.CreateRepositoryWrapper(_context);
            _presenter = new MainPresenter(_repositories);

            BindEvents();
            ApplyStyling();

            // TODO: Re-enable rollback, after MidiProcessor can do without querying
            //try
            //{
                _presenter.Show();
                ApplyViewModel();

                RecreateMidiInputProcessor();
            //}
            //finally
            //{
            //    _context.Rollback();
            //}
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
                if (_midiInputProcessor != null) _midiInputProcessor.Dispose();
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

        // Infrastructure

        private MidiInputProcessor _midiInputProcessor;

        private void RecreateMidiInputProcessor()
        {
            if (_midiInputProcessor != null)
            {
                _midiInputProcessor.Dispose();
            }

            Scale dummyScale = CreateMockScale();

            IList<Patch> patches = _presenter.ViewModel.Document.CurrentPatches.List
                                                                .Select(x => _repositories.DocumentRepository.Get(x.ChildDocumentID))
                                                                .Select(x => x.Patches.Single())
                                                                .ToArray();
            if (patches.Count == 0)
            {
                patches = new Patch[] { CreateDefaultSinePatch() };
            }

            _midiInputProcessor = new MidiInputProcessor(dummyScale, patches, new PatchRepositories(_repositories));
        }

        private Scale CreateMockScale()
        {
            var dummyScale = new Scale();
            return dummyScale;
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
                        x.PatchInlet(InletTypeEnum.Frequency)
                    ),
                    x.PatchInlet(InletTypeEnum.Volume)
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
    }
}
