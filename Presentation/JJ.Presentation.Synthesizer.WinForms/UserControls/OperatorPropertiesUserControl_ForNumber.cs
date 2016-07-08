using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;

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

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelNumber, textBoxNumber);
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelNumber.Text = PropertyDisplayNames.Number;
            labelOperatorTypeValue.Text = PropertyDisplayNames.Number;
        }

        // Binding

        private new OperatorPropertiesViewModel_ForNumber ViewModel => (OperatorPropertiesViewModel_ForNumber)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            textBoxNumber.Text = ViewModel.Number;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.Number = textBoxNumber.Text;
        }
    }
}
