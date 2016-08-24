using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCustomOperator
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForCustomOperator()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelUnderlyingPatch.Text = PropertyDisplayNames.UnderlyingPatch;
            labelOperatorTypeValue.Text = PropertyDisplayNames.CustomOperator;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelUnderlyingPatch, comboBoxUnderlyingPatch);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForCustomOperator ViewModel => (OperatorPropertiesViewModel_ForCustomOperator)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
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

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.UnderlyingPatch = (ChildDocumentIDAndNameViewModel)comboBoxUnderlyingPatch.SelectedItem;
        }
    }
}
