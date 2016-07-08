using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_WithInletCount 
        : OperatorPropertiesUserControl_WithInletCount_NotDesignable
    {
        public OperatorPropertiesUserControl_WithInletCount()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelInletCount, numericUpDownInletCount);
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelInletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            numericUpDownInletCount.Value = ViewModel.InletCount;
            labelOperatorTypeValue.Text = ViewModel.OperatorType.Name;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.InletCount = (int)numericUpDownInletCount.Value;
        }
    }

    /// <summary> 
    /// The WinForms designer does not work when deriving directly from a generic class.
    /// And also not when you make this class abstract.
    /// </summary>
    internal class OperatorPropertiesUserControl_WithInletCount_NotDesignable
        : OperatorPropertiesUserControlBase<OperatorPropertiesViewModel_WithInletCount>
    { }
}
