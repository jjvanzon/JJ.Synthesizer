using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForSample 
        : OperatorPropertiesUserControl_ForSample_NotDesignable
    {
        public OperatorPropertiesUserControl_ForSample()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelSample, comboBoxSample);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelOperatorTypeValue.Text = PropertyDisplayNames.Sample;
            labelSample.Text = PropertyDisplayNames.Sample;
            labelDimension.Text = PropertyDisplayNames.Dimension;
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;

            // Sample
            if (ViewModel.Sample != null)
            {
                comboBoxSample.SelectedValue = ViewModel.Sample.ID;
            }
            else
            {
                comboBoxSample.SelectedValue = 0;
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

        public void SetSampleLookup(IList<IDAndName> sampleLookup)
        {
            // Always refill the lookup, so changes to the sample collection are reflected.
            int? selectedID = TryGetSelectedSampleID();
            comboBoxSample.DataSource = null; // Do this or WinForms will not refresh the list.
            comboBoxSample.ValueMember = PropertyNames.ID;
            comboBoxSample.DisplayMember = PropertyNames.Name;
            comboBoxSample.DataSource = sampleLookup;
            if (selectedID != null)
            {
                comboBoxSample.SelectedValue = selectedID;
            }
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.Sample = (IDAndName)comboBoxSample.SelectedItem;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }

        // Helpers

        private int? TryGetSelectedSampleID()
        {
            if (comboBoxSample.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxSample.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }
    }

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class OperatorPropertiesUserControl_ForSample_NotDesignable
        : OperatorPropertiesUserControlBase<OperatorPropertiesViewModel_ForSample>
    { }
}
