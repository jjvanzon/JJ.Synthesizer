using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
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

            // Put these last, so we can make them invisible dynamically.
            // Trying to close the gap where the invisible controls are, proved a disaster.
            // TableLayoutPanel cannot help us with it.
            // Trying to recreate the rows dynamically made a total mess of my base class.
            AddProperty(labelStandardDimension, comboBoxStandardDimension);
            AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
        }
    }
}
