using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForSample : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private OperatorPropertiesViewModel_ForSample _viewModel;

        public OperatorPropertiesUserControl_ForSample()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForSample_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperatorPropertiesViewModel_ForSample ViewModel
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
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = PropertyDisplayNames.OperatorType + ":";
            labelSample.Text = PropertyDisplayNames.Sample;

            labelOperatorTypeValue.Text = PropertyDisplayNames.Sample;
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

            if (_viewModel.Sample != null)
            {
                comboBoxSample.SelectedValue = _viewModel.Sample.ID;
            }
            else
            {
                comboBoxSample.SelectedValue = 0;
            }
        }

        public void SetSampleLookup(IList<IDAndName> sampleLookup)
        {
            // Always refill the lookup, so changes to the patch collection are reflected.
            int? selectedID = TryGetSelectedSampleID();
            comboBoxSample.DataSource = null; // Do this or WinForms will not refresh the list.
            comboBoxSample.ValueMember = PropertyNames.ID;
            comboBoxSample.DisplayMember = PropertyNames.Name;
            comboBoxSample.DataSource = sampleLookup;
            if (selectedID != null)
            {
                comboBoxSample.SelectedValue = selectedID;
            }
        }

        private int? TryGetSelectedSampleID()
        {
            if (comboBoxSample.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxSample.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Name = textBoxName.Text;
            _viewModel.Sample = (IDAndName)comboBoxSample.SelectedItem;
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

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void OperatorPropertiesUserControl_ForSample_Leave(object sender, EventArgs e)
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
