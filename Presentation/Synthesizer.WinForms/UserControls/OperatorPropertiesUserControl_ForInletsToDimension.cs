using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForInletsToDimension 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForInletsToDimension()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelInletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
            labelInterpolation.Text = PropertyDisplayNames.Interpolation;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelInletCount, numericUpDownInletCount);
            AddProperty(labelInterpolation, comboBoxInterpolation);
            AddProperty(_labelStandardDimension, _comboBoxStandardDimension);
            AddProperty(_labelCustomDimensionName, _textBoxCustomDimensionName);
            AddProperty(_labelName, _textBoxName);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForInletsToDimension ViewModel
        {
            get { return (OperatorPropertiesViewModel_ForInletsToDimension)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownInletCount.Value = ViewModel.InletCount;

            // Interpolation
            if (comboBoxInterpolation.DataSource == null)
            {
                comboBoxInterpolation.ValueMember = PropertyNames.ID;
                comboBoxInterpolation.DisplayMember = PropertyNames.Name;
                comboBoxInterpolation.DataSource = ViewModel.InterpolationLookup;
            }
            comboBoxInterpolation.SelectedValue = ViewModel.Interpolation?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.InletCount = (int)numericUpDownInletCount.Value;
            ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
        }
    }
}
