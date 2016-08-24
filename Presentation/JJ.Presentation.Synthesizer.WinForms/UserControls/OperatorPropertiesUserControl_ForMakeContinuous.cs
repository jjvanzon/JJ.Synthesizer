using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForMakeContinuous 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForMakeContinuous()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelInletCount, numericUpDownInletCount);
            AddProperty(labelInterpolation, comboBoxInterpolation);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelOperatorTypeValue.Text = PropertyDisplayNames.MakeContinuous;
            labelInletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
            labelInterpolation.Text = PropertyDisplayNames.Interpolation;
            labelDimension.Text = PropertyDisplayNames.Dimension;
        }

        // Binding

        private new OperatorPropertiesViewModel_ForMakeContinuous ViewModel => (OperatorPropertiesViewModel_ForMakeContinuous)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            numericUpDownInletCount.Value = ViewModel.InletCount;

            // Interpolation
            if (comboBoxInterpolation.DataSource == null)
            {
                comboBoxInterpolation.ValueMember = PropertyNames.ID;
                comboBoxInterpolation.DisplayMember = PropertyNames.Name;
                comboBoxInterpolation.DataSource = ViewModel.InterpolationLookup;
            }
            if (ViewModel.Interpolation != null)
            {
                comboBoxInterpolation.SelectedValue = ViewModel.Interpolation.ID;
            }
            else
            {
                comboBoxInterpolation.SelectedValue = 0;
            }

            // Dimension
            if (comboBoxDimension.DataSource == null)
            {
                comboBoxDimension.ValueMember = PropertyNames.ID;
                comboBoxDimension.DisplayMember = PropertyNames.Name;
                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
            }
            if (ViewModel.Dimension != null)
            {
                comboBoxDimension.SelectedValue = ViewModel.Dimension.ID;
            }
            else
            {
                comboBoxDimension.SelectedValue = 0;
            }
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.InletCount = (int)numericUpDownInletCount.Value;
            ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }
}
