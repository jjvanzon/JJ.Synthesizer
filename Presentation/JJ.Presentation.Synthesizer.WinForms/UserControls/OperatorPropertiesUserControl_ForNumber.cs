using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForNumber 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForNumber()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelNumber.Text = PropertyDisplayNames.Number;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelNumber, textBoxNumber);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForNumber ViewModel => (OperatorPropertiesViewModel_ForNumber)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            textBoxNumber.Text = ViewModel.Number;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Number = textBoxNumber.Text;
        }
    }
}
