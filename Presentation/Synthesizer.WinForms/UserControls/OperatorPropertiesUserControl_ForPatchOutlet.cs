using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForPatchOutlet 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForPatchOutlet()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelNumber.Text = Titles.Number;
            labelDimension.Text = PropertyDisplayNames.Dimension;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(_labelName, _textBoxName);
            AddProperty(labelNumber, numericUpDownNumber);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForPatchOutlet ViewModel
        {
            get { return (OperatorPropertiesViewModel_ForPatchOutlet)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownNumber.Value = ViewModel.Number;

            if (comboBoxDimension.DataSource == null)
            {
                comboBoxDimension.ValueMember = PropertyNames.ID;
                comboBoxDimension.DisplayMember = PropertyNames.Name;
                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
            }

            comboBoxDimension.SelectedValue = ViewModel.Dimension?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();
            
            ViewModel.Number = (int)numericUpDownNumber.Value;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }
}
