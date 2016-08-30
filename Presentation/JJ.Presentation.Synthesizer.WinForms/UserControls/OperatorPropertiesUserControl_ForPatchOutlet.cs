using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

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
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelName, textBoxName);
            AddProperty(labelNumber, numericUpDownNumber);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForPatchOutlet ViewModel => (OperatorPropertiesViewModel_ForPatchOutlet)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownNumber.Value = ViewModel.Number;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();
            
            ViewModel.Number = (int)numericUpDownNumber.Value;
        }
    }
}
