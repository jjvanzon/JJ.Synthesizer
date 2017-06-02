using System.Collections.Generic;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCustomOperator
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForCustomOperator() => InitializeComponent();

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelUnderlyingPatch.Text = ResourceFormatter.UnderlyingPatch;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelUnderlyingPatch, comboBoxUnderlyingPatch);
            AddProperty(_labelName, _textBoxName);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForCustomOperator ViewModel
        {
            get => (OperatorPropertiesViewModel_ForCustomOperator)base.ViewModel;
            set => base.ViewModel = value;
        }

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
            comboBoxUnderlyingPatch.ValueMember = nameof(IDAndName.ID);
            comboBoxUnderlyingPatch.DisplayMember = nameof(IDAndName.Name);
            comboBoxUnderlyingPatch.DataSource = underlyingPatchLookup;
            if (selectedID != null)
            {
                comboBoxUnderlyingPatch.SelectedValue = selectedID;
            }
        }

        private int? TryGetSelectedUnderlyingPatchID()
        {
            if (comboBoxUnderlyingPatch.DataSource == null) return null;
            var idAndName = (IDAndName)comboBoxUnderlyingPatch.SelectedItem;
            return idAndName?.ID;
        }
    }
}
