using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForSample 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForSample()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelSample.Text = PropertyDisplayNames.Sample;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelSample, comboBoxSample);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForSample ViewModel => (OperatorPropertiesViewModel_ForSample)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            comboBoxSample.SelectedValue = ViewModel.Sample?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Sample = (IDAndName)comboBoxSample.SelectedItem;
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

        // Helpers

        private int? TryGetSelectedSampleID()
        {
            if (comboBoxSample.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxSample.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }
    }
}
