using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

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
            TitleBarText = CommonTitlesFormatter.ObjectProperties(PropertyDisplayNames.Scale);
            labelName.Text = CommonTitlesFormatter.Name;
            labelScaleType.Text = Titles.Type;
            labelBaseFrequency.Text = PropertyDisplayNames.BaseFrequency;
        }

        // Binding

        public new ScalePropertiesViewModel ViewModel
        {
            get { return (ScalePropertiesViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override int GetID()
        {
            return ViewModel.Entity.ID;
        }

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

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Entity.Name = textBoxName.Text;
            ViewModel.Entity.BaseFrequency = (double)numericUpDownBaseFrequency.Value;
            ViewModel.Entity.ScaleType = (IDAndName)comboBoxScaleType.SelectedItem;
        }
    }
}
