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
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForSample 
        : OperatorPropertiesUserControl_ForSample_NotDesignable
    {
        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> LoseFocusRequested;

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

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelOperatorTypeValue.Text = PropertyDisplayNames.Sample;
            labelSample.Text = PropertyDisplayNames.Sample;
            labelDimension.Text = PropertyDisplayNames.Dimension;
        }

        private void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            textBoxName.Text = ViewModel.Name;

            // Sample
            if (ViewModel.Sample != null)
            {
                comboBoxSample.SelectedValue = ViewModel.Sample.ID;
            }
            else
            {
                comboBoxSample.SelectedValue = 0;
            }

            // Dimension
            if (comboBoxDimension.DataSource == null)
            {
                comboBoxDimension.ValueMember = PropertyNames.ID;
                comboBoxDimension.DisplayMember = PropertyNames.Name;
                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
            }
            if (ViewModel.Dimension != null)
            {
                comboBoxDimension.SelectedValue = ViewModel.Dimension.ID;
            }
            else
            {
                comboBoxDimension.SelectedValue = 0;
            }
        }

        public void SetSampleLookup(IList<IDAndName> sampleLookup)
        {
            // Always refill the lookup, so changes to the sample collection are reflected.
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

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Name = textBoxName.Text;
            ViewModel.Sample = (IDAndName)comboBoxSample.SelectedItem;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }

        // Actions

        private void Close()
        {
            if (ViewModel == null) return;
            ApplyControlsToViewModel();
            CloseRequested?.Invoke(this, new Int32EventArgs(ViewModel.ID));
        }

        private void LoseFocus()
        {
            if (ViewModel == null) return;
            ApplyControlsToViewModel();
            LoseFocusRequested?.Invoke(this, new Int32EventArgs(ViewModel.ID));
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

        // Helpers

        private int? TryGetSelectedSampleID()
        {
            if (comboBoxSample.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxSample.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }
    }

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class OperatorPropertiesUserControl_ForSample_NotDesignable
        : UserControlBase<OperatorPropertiesViewModel_ForSample>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
