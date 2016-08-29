using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl()
        {
            InitializeComponent();
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelName, textBoxName);
        }
    }
}
