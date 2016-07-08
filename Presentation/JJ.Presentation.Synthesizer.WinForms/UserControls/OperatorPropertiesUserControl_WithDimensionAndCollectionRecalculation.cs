using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Resources;

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

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelRecalculation, comboBoxCollectionRecalculation);
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelRecalculation.Text = PropertyDisplayNames.CollectionRecalculation;
            labelDimension.Text = PropertyDisplayNames.Dimension;
        }

        // Binding

        private new OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation ViewModel =>
                   (OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            labelOperatorTypeValue.Text = ViewModel.OperatorType.Name;

            // Recalculation
            if (comboBoxCollectionRecalculation.DataSource == null)
            {
                comboBoxCollectionRecalculation.ValueMember = PropertyNames.ID;
                comboBoxCollectionRecalculation.DisplayMember = PropertyNames.Name;
                comboBoxCollectionRecalculation.DataSource = ViewModel.CollectionRecalculationLookup;
            }
            if (ViewModel.CollectionRecalculation != null)
            {
                comboBoxCollectionRecalculation.SelectedValue = ViewModel.CollectionRecalculation.ID;
            }
            else
            {
                comboBoxCollectionRecalculation.SelectedValue = 0;
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

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.CollectionRecalculation = (IDAndName)comboBoxCollectionRecalculation.SelectedItem;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }
}
