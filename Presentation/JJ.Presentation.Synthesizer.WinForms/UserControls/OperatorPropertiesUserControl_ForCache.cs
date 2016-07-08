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
    internal partial class OperatorPropertiesUserControl_ForCache 
        : OperatorPropertiesUserControl_ForCache_NotDesignable
    {
        public OperatorPropertiesUserControl_ForCache()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Operator);
            labelName.Text = CommonTitles.Name;
            labelOperatorTypeTitle.Text = Titles.Type + ":";
            labelInterpolation.Text = PropertyDisplayNames.Interpolation;
            labelSpeakerSetup.Text = PropertyDisplayNames.SpeakerSetup;
            labelDimension.Text = PropertyDisplayNames.Dimension;
            labelOperatorTypeValue.Text = PropertyDisplayNames.Cache;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelName, textBoxName);
            AddProperty(labelInterpolation, comboBoxInterpolation);
            AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
            AddProperty(labelDimension, comboBoxDimension);
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;

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

            // SpeakerSetup
            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
                comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
            }
            if (ViewModel.SpeakerSetup != null)
            {
                comboBoxSpeakerSetup.SelectedValue = ViewModel.SpeakerSetup.ID;
            }
            else
            {
                comboBoxSpeakerSetup.SelectedValue = 0;
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
            ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
            ViewModel.SpeakerSetup = (IDAndName)comboBoxSpeakerSetup.SelectedItem;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
        }
    }

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class OperatorPropertiesUserControl_ForCache_NotDesignable
        : OperatorPropertiesUserControlBase<OperatorPropertiesViewModel_ForCache>
    { }
}
