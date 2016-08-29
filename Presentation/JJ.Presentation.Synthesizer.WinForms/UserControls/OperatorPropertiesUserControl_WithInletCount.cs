using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithInletCount 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_WithInletCount()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelInletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelInletCount, numericUpDownInletCount);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_WithInletCount ViewModel => 
                   (OperatorPropertiesViewModel_WithInletCount)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownInletCount.Value = ViewModel.InletCount;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.InletCount = (int)numericUpDownInletCount.Value;
        }
    }
}
