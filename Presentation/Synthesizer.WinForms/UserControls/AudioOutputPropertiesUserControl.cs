﻿using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioOutputPropertiesUserControl : PropertiesUserControlBase
    {
        public AudioOutputPropertiesUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelSamplingRate, numericUpDownSamplingRate);
            AddProperty(labelSpeakerSetup, comboBoxSpeakerSetup);
            AddProperty(labelMaxConcurrentNotes, numericUpDownMaxConcurrentNotes);
            AddProperty(labelDesiredBufferDuration, numericUpDownDesiredBufferDuration);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.AudioOutput);
            labelSamplingRate.Text = PropertyDisplayNames.SamplingRate;
            labelSpeakerSetup.Text = PropertyDisplayNames.SpeakerSetup;
            labelMaxConcurrentNotes.Text = PropertyDisplayNames.MaxConcurrentNotes;
            labelDesiredBufferDuration.Text = PropertyDisplayNames.DesiredBufferDuration;
        }

        // Binding

        public new AudioOutputPropertiesViewModel ViewModel
        {
            get { return (AudioOutputPropertiesViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void ApplyViewModelToControls()
        {
            numericUpDownSamplingRate.Value = ViewModel.Entity.SamplingRate;

            if (comboBoxSpeakerSetup.DataSource == null)
            {
                comboBoxSpeakerSetup.ValueMember = PropertyNames.ID;
                comboBoxSpeakerSetup.DisplayMember = PropertyNames.Name;
                comboBoxSpeakerSetup.DataSource = ViewModel.SpeakerSetupLookup;
            }
            comboBoxSpeakerSetup.SelectedValue = ViewModel.Entity.SpeakerSetup.ID;

            numericUpDownMaxConcurrentNotes.Value = ViewModel.Entity.MaxConcurrentNotes;
            numericUpDownDesiredBufferDuration.Value = (decimal)ViewModel.Entity.DesiredBufferDuration;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Entity.SamplingRate = (int)numericUpDownSamplingRate.Value;
            ViewModel.Entity.SpeakerSetup = (IDAndName)comboBoxSpeakerSetup.SelectedItem;
            ViewModel.Entity.MaxConcurrentNotes = (int)numericUpDownMaxConcurrentNotes.Value;
            ViewModel.Entity.DesiredBufferDuration = (double)numericUpDownDesiredBufferDuration.Value;
        }
    }
}