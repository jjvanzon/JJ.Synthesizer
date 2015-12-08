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

namespace JJ.Presentation.Synthesizer.WinForms
{
    internal partial class MainForm : Form
    {
        private IContext _context;
        private RepositoryWrapper _repositoryWrapper;
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
            _repositoryWrapper = PersistenceHelper.CreateRepositoryWrapper(_context);
            _presenter = new MainPresenter(_repositoryWrapper);

            BindEvents();
            ApplyStyling();

            _presenter.Show();

            ApplyViewModel();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            if (_context != null)
            {
                _context.Dispose();
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
    }
}
