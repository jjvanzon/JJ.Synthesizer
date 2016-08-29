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
using JJ.Data.Canonical;

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
            base.SetTitles();

            labelUnderlyingPatch.Text = PropertyDisplayNames.UnderlyingPatch;
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
            base.ApplyViewModelToControls();

            comboBoxUnderlyingPatch.SelectedValue = ViewModel.UnderlyingPatch?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.UnderlyingPatch = (IDAndName)comboBoxUnderlyingPatch.SelectedItem;
        }

        public void SetUnderlyingPatchLookup(IList<IDAndName> underlyingPatchLookup)
        {
            // Always refill the document lookup, so changes to the document collection are reflected.

            int? selectedID = TryGetSelectedUnderlyingPatchID();
            comboBoxUnderlyingPatch.DataSource = null; // Do this or WinForms will not refresh the list.
            comboBoxUnderlyingPatch.ValueMember = PropertyNames.ID;
            comboBoxUnderlyingPatch.DisplayMember = PropertyNames.Name;
            comboBoxUnderlyingPatch.DataSource = underlyingPatchLookup;
            if (selectedID != null)
            {
                comboBoxUnderlyingPatch.SelectedValue = selectedID;
            }
        }

        private int? TryGetSelectedUnderlyingPatchID()
        {
            if (comboBoxUnderlyingPatch.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxUnderlyingPatch.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }
    }
}
