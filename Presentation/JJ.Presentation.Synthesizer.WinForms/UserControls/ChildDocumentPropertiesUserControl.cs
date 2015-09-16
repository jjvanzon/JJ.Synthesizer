using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ChildDocumentPropertiesUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private ChildDocumentPropertiesViewModel _viewModel;

        public ChildDocumentPropertiesUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void ChildDocumentPropertiesUserControl_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ChildDocumentPropertiesViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.ChildDocument);
            labelName.Text = CommonTitles.Name;
            labelChildDocumentType.Text = Titles.Type;
            labelMainPatch.Text = PropertyDisplayNames.MainPatch;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            textBoxName.Text = _viewModel.Name;

            if (comboBoxChildDocumentType.DataSource == null)
            {
                comboBoxChildDocumentType.DataSource = _viewModel.ChildDocumentTypeLookup;
                comboBoxChildDocumentType.ValueMember = PropertyNames.ID;
                comboBoxChildDocumentType.DisplayMember = PropertyNames.Name;
            }
            comboBoxChildDocumentType.SelectedValue = _viewModel.ChildDocumentType.ID;

            // Always refill the MainPatch lookup, so changes to the patch collection are reflected.
            comboBoxMainPatch.DataSource = null;
            comboBoxMainPatch.DataSource = _viewModel.MainPatchLookup;
            comboBoxMainPatch.ValueMember = PropertyNames.ID;
            comboBoxMainPatch.DisplayMember = PropertyNames.Name;
            if (_viewModel.MainPatch != null)
            {
                comboBoxMainPatch.SelectedValue = _viewModel.MainPatch.ID;
            }
            else
            {
                comboBoxMainPatch.SelectedValue = 0;
            }
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Name = textBoxName.Text;
            _viewModel.ChildDocumentType = (IDAndName)comboBoxChildDocumentType.SelectedItem;
            _viewModel.MainPatch = (IDAndName)comboBoxMainPatch.SelectedItem;
        }

        // Actions

        private void Close()
        {
            if (CloseRequested != null)
            {
                ApplyControlsToViewModel();
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void LoseFocus()
        {
            if (LoseFocusRequested != null)
            {
                ApplyControlsToViewModel();
                LoseFocusRequested(this, EventArgs.Empty);
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void ChildDocumentPropertiesUserControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                textBoxName.Focus();
                textBoxName.Select(0, 0);
            }
        }

        // This event goes off when I call DocumentPropertiesUserControl.SetFocus after clicking on a DataGridView,
        // but does not go off when I call DocumentPropertiesUserControl.SetFocus after clicking on a TreeView.
        // Thanks, WinForms...
        private void ChildDocumentPropertiesUserControl_Enter(object sender, EventArgs e)
        {
            textBoxName.Focus();
            textBoxName.Select(0, 0);
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void ChildDocumentPropertiesUserControl_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible) 
            {
                LoseFocus();
            }
        }
    }
}
