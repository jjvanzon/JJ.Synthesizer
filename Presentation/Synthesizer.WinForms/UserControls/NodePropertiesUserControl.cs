using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class NodePropertiesUserControl : PropertiesUserControlBase
	{
		public NodePropertiesUserControl()
		{
			InitializeComponent();

			ExpandButtonVisible = true;
		}

		// Gui

		protected override void AddProperties()
		{
			AddProperty(labelX, numericUpDownX);
			AddProperty(labelY, numericUpDownY);
			AddProperty(labelInterpolationType, comboBoxInterpolationType);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Node);
			labelX.Text = ResourceFormatter.X;
			labelY.Text = ResourceFormatter.Y;
			labelInterpolationType.Text = ResourceFormatter.Interpolation;
		}

		// Binding

		public new NodePropertiesViewModel ViewModel
		{
			// ReSharper disable once MemberCanBePrivate.Global
			get => (NodePropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.Entity.ID;

		protected override void ApplyViewModelToControls()
		{
			numericUpDownX.Value = (decimal)ViewModel.Entity.X;
			numericUpDownY.Value = (decimal)ViewModel.Entity.Y;

			if (comboBoxInterpolationType.DataSource == null)
			{
				comboBoxInterpolationType.ValueMember = nameof(IDAndName.ID);
				comboBoxInterpolationType.DisplayMember = nameof(IDAndName.Name);
				comboBoxInterpolationType.DataSource = ViewModel.InterpolationTypeLookup;
			}

			if (ViewModel.Entity.Interpolation != null)
			{
				comboBoxInterpolationType.SelectedValue = ViewModel.Entity.Interpolation.ID;
			}
			else
			{
				comboBoxInterpolationType.SelectedValue = 0;
			}
		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.Entity.X = (double)numericUpDownX.Value;
			ViewModel.Entity.Y = (double)numericUpDownY.Value;
			ViewModel.Entity.Interpolation = (IDAndName)comboBoxInterpolationType.SelectedItem;
		}
	}
}
