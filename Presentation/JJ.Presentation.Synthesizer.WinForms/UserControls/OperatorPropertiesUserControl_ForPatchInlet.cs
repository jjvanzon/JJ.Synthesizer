using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForPatchInlet 
        : OperatorPropertiesUserControl_ForPatchInlet_NotDesignable
    {
        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> LoseFocusRequested;

        public OperatorPropertiesUserControl_ForPatchInlet()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForPatchInlet_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelOperatorTypeValue.Text = PropertyDisplayNames.PatchInlet;
            labelNumber.Text = Titles.Number;
            labelDimension.Text = PropertyDisplayNames.Dimension;
            labelDefaultValue.Text = PropertyDisplayNames.DefaultValue;
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
            numericUpDownNumber.Value = ViewModel.Number;
            textBoxDefaultValue.Text = ViewModel.DefaultValue;

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

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Name = textBoxName.Text;
            ViewModel.Number = (int)numericUpDownNumber.Value;
            ViewModel.DefaultValue = textBoxDefaultValue.Text;
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
        private void OperatorPropertiesUserControl_ForPatchInlet_Leave(object sender, EventArgs e)
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
    internal class OperatorPropertiesUserControl_ForPatchInlet_NotDesignable
        : UserControlBase<OperatorPropertiesViewModel_ForPatchInlet>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
