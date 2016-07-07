using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForBundle 
        : OperatorPropertiesUserControl_ForBundle_NotDesignable
    {
        public OperatorPropertiesUserControl_ForBundle()
        {
            InitializeComponent();

            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelOperatorTypeValue.Text = PropertyDisplayNames.Bundle;
            labelInletCount.Text = CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
            labelDimension.Text = PropertyDisplayNames.Dimension;
        }

        protected override void PositionControls()
        {
            base.PositionControls();

            tableLayoutPanelProperties.Left = 0;
            tableLayoutPanelProperties.Top = TitleBarHeight;
            tableLayoutPanelProperties.Width = Width;
            tableLayoutPanelProperties.Height = Height - TitleBarHeight;
        }

        protected override void ApplyStyling()
        {
            StyleHelper.SetPropertyLabelColumnSize(tableLayoutPanelProperties);
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            textBoxName.Text = ViewModel.Name;
            numericUpDownInletCount.Value = ViewModel.InletCount;

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
            if (ViewModel == null) return;

            ViewModel.Name = textBoxName.Text;
            ViewModel.InletCount = (int)numericUpDownInletCount.Value;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class OperatorPropertiesUserControl_ForBundle_NotDesignable
        : OperatorPropertiesUserControlBase<OperatorPropertiesViewModel_ForBundle>
    {
        protected override void ApplyControlsToViewModel()
        {
            throw new NotImplementedException();
        }

        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
