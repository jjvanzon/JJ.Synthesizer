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
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCustomOperator
        : OperatorPropertiesUserControl_ForCustomOperator_NotDesignable
    {
        public event EventHandler<Int32EventArgs> CloseRequested;
        public event EventHandler<Int32EventArgs> LoseFocusRequested;

        public OperatorPropertiesUserControl_ForCustomOperator()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        private void OperatorPropertiesUserControl_ForCustomOperator_Load(object sender, EventArgs e)
        {
            ApplyStyling();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);

            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelUnderlyingPatch.Text = Titles.UnderlyingPatch;

            labelOperatorTypeValue.Text = PropertyDisplayNames.CustomOperator;
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

            if (ViewModel.UnderlyingPatch != null)
            {
                comboBoxUnderlyingPatch.SelectedValue = ViewModel.UnderlyingPatch.ChildDocumentID;
            }
            else
            {
                comboBoxUnderlyingPatch.SelectedValue = 0;
            }
        }

        public void SetUnderlyingPatchLookup(IList<ChildDocumentIDAndNameViewModel> underlyingPatchLookup)
        {
            // Always refill the document lookup, so changes to the document collection are reflected.

            int? selectedID = TryGetSelectedChildDocumentID();
            comboBoxUnderlyingPatch.DataSource = null; // Do this or WinForms will not refresh the list.
            comboBoxUnderlyingPatch.ValueMember = PropertyNames.ChildDocumentID;
            comboBoxUnderlyingPatch.DisplayMember = PropertyNames.Name;
            comboBoxUnderlyingPatch.DataSource = underlyingPatchLookup;
            if (selectedID != null)
            {
                comboBoxUnderlyingPatch.SelectedValue = selectedID;
            }
        }

        private int? TryGetSelectedChildDocumentID()
        {
            if (comboBoxUnderlyingPatch.DataSource == null) return null;
            ChildDocumentIDAndNameViewModel childDocumentIDAndNameViewModel = (ChildDocumentIDAndNameViewModel)comboBoxUnderlyingPatch.SelectedItem;
            if (childDocumentIDAndNameViewModel == null) return null;
            return childDocumentIDAndNameViewModel.ChildDocumentID;
        }

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Name = textBoxName.Text;
            ViewModel.UnderlyingPatch = (ChildDocumentIDAndNameViewModel)comboBoxUnderlyingPatch.SelectedItem;
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
        private void OperatorPropertiesUserControl_ForCustomOperator_Leave(object sender, EventArgs e)
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
    internal class OperatorPropertiesUserControl_ForCustomOperator_NotDesignable
        : UserControlBase<OperatorPropertiesViewModel_ForCustomOperator>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
