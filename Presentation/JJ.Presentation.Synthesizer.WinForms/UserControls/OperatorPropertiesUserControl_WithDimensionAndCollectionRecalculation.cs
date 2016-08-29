using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithDimensionAndCollectionRecalculation 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_WithDimensionAndCollectionRecalculation()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelRecalculation.Text = PropertyDisplayNames.CollectionRecalculation;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
            AddProperty(labelRecalculation, comboBoxCollectionRecalculation);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation ViewModel =>
                   (OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            if (comboBoxCollectionRecalculation.DataSource == null)
            {
                comboBoxCollectionRecalculation.ValueMember = PropertyNames.ID;
                comboBoxCollectionRecalculation.DisplayMember = PropertyNames.Name;
                comboBoxCollectionRecalculation.DataSource = ViewModel.CollectionRecalculationLookup;
            }
            comboBoxCollectionRecalculation.SelectedValue = ViewModel.CollectionRecalculation?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.CollectionRecalculation = (IDAndName)comboBoxCollectionRecalculation.SelectedItem;
        }
    }
}
