using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForPatchInlet 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForPatchInlet()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelNumber.Text = Titles.Number;
            labelDefaultValue.Text = PropertyDisplayNames.DefaultValue;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelDefaultValue, textBoxDefaultValue);
            AddProperty(labelName, textBoxName);
            AddProperty(labelNumber, numericUpDownNumber);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForPatchInlet ViewModel => 
                   (OperatorPropertiesViewModel_ForPatchInlet)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownNumber.Value = ViewModel.Number;
            textBoxDefaultValue.Text = ViewModel.DefaultValue;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Number = (int)numericUpDownNumber.Value;
            ViewModel.DefaultValue = textBoxDefaultValue.Text;
        }
    }
}
