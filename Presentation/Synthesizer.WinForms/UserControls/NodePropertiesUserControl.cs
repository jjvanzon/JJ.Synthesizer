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
			AddProperty(labelNodeType, comboBoxNodeType);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Node);
			labelX.Text = ResourceFormatter.X;
			labelY.Text = ResourceFormatter.Y;
			labelNodeType.Text = CommonResourceFormatter.Type;
		}

		// Binding

		public new NodePropertiesViewModel ViewModel
		{
			get => (NodePropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.Entity.ID;

		protected override void ApplyViewModelToControls()
		{
			numericUpDownX.Value = (decimal)ViewModel.Entity.X;
			numericUpDownY.Value = (decimal)ViewModel.Entity.Y;

			if (comboBoxNodeType.DataSource == null)
			{
				comboBoxNodeType.ValueMember = nameof(IDAndName.ID);
				comboBoxNodeType.DisplayMember = nameof(IDAndName.Name);
				comboBoxNodeType.DataSource = ViewModel.NodeTypeLookup;
			}

			if (ViewModel.Entity.NodeType != null)
			{
				comboBoxNodeType.SelectedValue = ViewModel.Entity.NodeType.ID;
			}
			else
			{
				comboBoxNodeType.SelectedValue = 0;
			}
		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.Entity.X = (double)numericUpDownX.Value;
			ViewModel.Entity.Y = (double)numericUpDownY.Value;
			ViewModel.Entity.NodeType = (IDAndName)comboBoxNodeType.SelectedItem;
		}
	}
}
