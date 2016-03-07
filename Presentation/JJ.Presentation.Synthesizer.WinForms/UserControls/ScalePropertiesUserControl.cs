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
    internal partial class ScalePropertiesUserControl : ScalePropertiesUserControl_NotDesignable
    {
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

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

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            textBoxName.Text = ViewModel.Entity.Name;
            if (ViewModel.Entity.BaseFrequency.HasValue)
            {
                numericUpDownBaseFrequency.Value = (decimal)ViewModel.Entity.BaseFrequency.Value;
            }

            if (comboBoxScaleType.DataSource == null)
            {
                comboBoxScaleType.ValueMember = PropertyNames.ID;
                comboBoxScaleType.DisplayMember = PropertyNames.Name;
                comboBoxScaleType.DataSource = ViewModel.ScaleTypeLookup;
            }
            comboBoxScaleType.SelectedValue = ViewModel.Entity.ScaleType.ID;
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
            if (ViewModel == null) return;

            ViewModel.Entity.Name = textBoxName.Text;
            ViewModel.Entity.BaseFrequency = (double)numericUpDownBaseFrequency.Value;
            ViewModel.Entity.ScaleType = (IDAndName)comboBoxScaleType.SelectedItem;
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

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class ScalePropertiesUserControl_NotDesignable : UserControlBase<ScalePropertiesViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
