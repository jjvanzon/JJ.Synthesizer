using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class ScalePropertiesUserControl : PropertiesUserControlBase
	{
		public ScalePropertiesUserControl() => InitializeComponent();

		// Gui

		protected override void AddProperties()
		{
			AddProperty(labelName, textBoxName);
			AddProperty(labelScaleType, comboBoxScaleType);
			AddProperty(labelBaseFrequency, numericUpDownBaseFrequency);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Scale);
			labelName.Text = CommonResourceFormatter.Name;
			labelScaleType.Text = CommonResourceFormatter.Type;
			labelBaseFrequency.Text = ResourceFormatter.BaseFrequency;
		}

		// Binding

		public new ScalePropertiesViewModel ViewModel
		{
			get => (ScalePropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.Entity.ID;

		protected override void ApplyViewModelToControls()
		{
			textBoxName.Text = ViewModel.Entity.Name;
			if (ViewModel.Entity.BaseFrequency.HasValue)
			{
				numericUpDownBaseFrequency.Value = (decimal)ViewModel.Entity.BaseFrequency.Value;
			}

			if (comboBoxScaleType.DataSource == null)
			{
				comboBoxScaleType.ValueMember = nameof(IDAndName.ID);
				comboBoxScaleType.DisplayMember = nameof(IDAndName.Name);
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
