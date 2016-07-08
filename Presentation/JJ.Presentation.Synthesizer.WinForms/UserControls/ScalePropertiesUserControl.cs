using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.WinForms.Extensions;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ScalePropertiesUserControl : PropertiesUserControlBase
    {
        public ScalePropertiesUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelName, textBoxName);
            AddProperty(labelScaleType, comboBoxScaleType);
            AddProperty(labelBaseFrequency, numericUpDownBaseFrequency);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Scale);
            labelName.Text = CommonTitles.Name;
            labelScaleType.Text = Titles.Type;
            labelBaseFrequency.Text = PropertyDisplayNames.BaseFrequency;
        }

        // Binding

        private new ScalePropertiesViewModel ViewModel => (ScalePropertiesViewModel)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Entity.Name;
            if (ViewModel.Entity.BaseFrequency.HasValue)
            {
                numericUpDownBaseFrequency.Value = (decimal)ViewModel.Entity.BaseFrequency.Value;
            }

            if (comboBoxScaleType.DataSource == null)
            {
                comboBoxScaleType.ValueMember = PropertyNames.ID;
                comboBoxScaleType.DisplayMember = PropertyNames.Name;
                comboBoxScaleType.DataSource = ViewModel.ScaleTypeLookup;
            }
            comboBoxScaleType.SelectedValue = ViewModel.Entity.ScaleType.ID;
        }

        private int? TryGetSelectedSampleID()
        {
            if (comboBoxScaleType.DataSource == null) return null;
            IDAndName idAndName = (IDAndName)comboBoxScaleType.SelectedItem;
            if (idAndName == null) return null;
            return idAndName.ID;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Entity.Name = textBoxName.Text;
            ViewModel.Entity.BaseFrequency = (double)numericUpDownBaseFrequency.Value;
            ViewModel.Entity.ScaleType = (IDAndName)comboBoxScaleType.SelectedItem;
        }
    }
}
