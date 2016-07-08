using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;

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

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelDefaultValue, textBoxDefaultValue);
            AddProperty(labelName, textBoxName);
            AddProperty(labelNumber, numericUpDownNumber);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelOperatorTypeValue.Text = PropertyDisplayNames.PatchInlet;
            labelNumber.Text = Titles.Number;
            labelDimension.Text = PropertyDisplayNames.Dimension;
            labelDefaultValue.Text = PropertyDisplayNames.DefaultValue;
        }

        // Binding

        private new OperatorPropertiesViewModel_ForPatchInlet ViewModel => (OperatorPropertiesViewModel_ForPatchInlet)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            numericUpDownNumber.Value = ViewModel.Number;
            textBoxDefaultValue.Text = ViewModel.DefaultValue;

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
            ViewModel.Number = (int)numericUpDownNumber.Value;
            ViewModel.DefaultValue = textBoxDefaultValue.Text;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }
}
