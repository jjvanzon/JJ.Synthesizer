using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForCache 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForCache()
        {
            InitializeComponent();
        }

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelInterpolation.Text = PropertyDisplayNames.Interpolation;
            labelSpeakerSetup.Text = PropertyDisplayNames.SpeakerSetup;
        }

        protected override void AddProperties()
        {
            AddProperty(labelOperatorTypeTitle, labelOperatorTypeValue);
            AddProperty(labelInterpolation, comboBoxInterpolation);
            AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
            AddProperty(labelName, textBoxName);
        }

        // Binding

        private new OperatorPropertiesViewModel_ForCache ViewModel => (OperatorPropertiesViewModel_ForCache)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            // Interpolation
            if (comboBoxInterpolation.DataSource == null)
            {
                comboBoxInterpolation.ValueMember = PropertyNames.ID;
                comboBoxInterpolation.DisplayMember = PropertyNames.Name;
                comboBoxInterpolation.DataSource = ViewModel.InterpolationLookup;
            }
            comboBoxInterpolation.SelectedValue = ViewModel.Interpolation?.ID ?? 0;

            // SpeakerSetup
            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
                comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
            }
            comboBoxSpeakerSetup.SelectedValue = ViewModel.SpeakerSetup?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Interpolation = (IDAndName)comboBoxInterpolation.SelectedItem;
            ViewModel.SpeakerSetup = (IDAndName)comboBoxSpeakerSetup.SelectedItem;
        }
    }
}
