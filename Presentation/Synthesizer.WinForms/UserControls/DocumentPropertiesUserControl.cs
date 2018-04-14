using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class DocumentPropertiesUserControl : PropertiesUserControlBase
	{
		public DocumentPropertiesUserControl()
		{
			InitializeComponent();

			DeleteButtonVisible = false;
		}

		// Gui

		protected override void AddProperties() => AddProperty(labelName, textBoxName);

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Document);
			labelName.Text = CommonResourceFormatter.Name;
		}

		// Binding

		public new DocumentPropertiesViewModel ViewModel
		{
			get => (DocumentPropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.Entity.ID;

		protected override void ApplyViewModelToControls() => textBoxName.Text = ViewModel.Entity.Name;

		protected override void ApplyControlsToViewModel() => ViewModel.Entity.Name = textBoxName.Text;
	}
}
