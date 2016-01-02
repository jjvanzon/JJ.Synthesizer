using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ScalePropertiesUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private ScalePropertiesViewModel _viewModel;

        public ScalePropertiesUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void ScalePropertiesUserControl_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScalePropertiesViewModel ViewModel
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
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Scale);

            labelName.Text = CommonTitles.Name;
            labelScaleType.Text = Titles.Type;
            labelBaseFrequency.Text = PropertyDisplayNames.BaseFrequency;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            textBoxName.Text = _viewModel.Entity.Name;
            if (_viewModel.Entity.BaseFrequency.HasValue)
            {
                numericUpDownBaseFrequency.Value = (decimal)_viewModel.Entity.BaseFrequency.Value;
            }

            if (comboBoxScaleType.DataSource == null)
            {
                comboBoxScaleType.ValueMember = PropertyNames.ID;
                comboBoxScaleType.DisplayMember = PropertyNames.Name;
                comboBoxScaleType.DataSource = _viewModel.ScaleTypeLookup;
            }
            comboBoxScaleType.SelectedValue = _viewModel.Entity.ScaleType.ID;
        }

        private int? TryGetSelectedSampleID()
        {
            if (comboBoxScaleType.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxScaleType.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Entity.Name = textBoxName.Text;
            _viewModel.Entity.BaseFrequency = (double)numericUpDownBaseFrequency.Value;
            _viewModel.Entity.ScaleType = (IDAndName)comboBoxScaleType.SelectedItem;
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
        private void ScalePropertiesUserControl_Leave(object sender, EventArgs e)
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
