using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class PatchPropertiesUserControl : PropertiesUserControlBase
	{
		private bool _applyViewModelToControlsIsBusy;

		public event EventHandler<EventArgs<int>> HasDimensionChanged;

		public PatchPropertiesUserControl()
		{
			InitializeComponent();

			AddToInstrumentButtonVisible = true;
			ExpandButtonVisible = true;
		}

		// Gui

		protected override void AddProperties()
		{
			AddProperty(labelName, textBoxName);
			AddProperty(labelGroup, textBoxGroup);
			AddProperty(labelHidden, checkBoxHidden);
			AddProperty(labelHasDimension, checkBoxHasDimension);
			AddProperty(labelStandardDimension, comboBoxStandardDimension);
			AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Patch);
			labelName.Text = CommonResourceFormatter.Name;
			labelGroup.Text = ResourceFormatter.Group;
			labelHasDimension.Text = ResourceFormatter.HasDimension;
			labelStandardDimension.Text = ResourceFormatter.StandardDimension;
			labelCustomDimensionName.Text = ResourceFormatter.CustomDimension;
			labelHidden.Text = ResourceFormatter.Hidden;
			checkBoxHasDimension.Text = null;
			checkBoxHidden.Text = null;
		}

		// Binding

		public new PatchPropertiesViewModel ViewModel
		{
			get => (PatchPropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel.ID;

		protected override void ApplyViewModelToControls()
		{
			_applyViewModelToControlsIsBusy = true;
			try
			{
				textBoxName.Text = ViewModel.Name;
				textBoxGroup.Text = ViewModel.Group;
				checkBoxHasDimension.Checked = ViewModel.HasDimension;
				textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;
				textBoxCustomDimensionName.Enabled = ViewModel.CustomDimensionNameEnabled;
				labelCustomDimensionName.Enabled = ViewModel.CustomDimensionNameEnabled;
				checkBoxHidden.Checked = ViewModel.Hidden;

				if (comboBoxStandardDimension.DataSource == null)
				{
					comboBoxStandardDimension.ValueMember = nameof(IDAndName.ID);
					comboBoxStandardDimension.DisplayMember = nameof(IDAndName.Name);
					comboBoxStandardDimension.DataSource = ViewModel.StandardDimensionLookup;
				}
				comboBoxStandardDimension.SelectedValue = ViewModel.StandardDimension?.ID ?? 0;
				comboBoxStandardDimension.Enabled = ViewModel.CustomDimensionNameEnabled;
				labelStandardDimension.Enabled = ViewModel.CustomDimensionNameEnabled;
			}
			finally
			{
				_applyViewModelToControlsIsBusy = false;
			}
		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.Name = textBoxName.Text;
			ViewModel.Group = textBoxGroup.Text;
			ViewModel.HasDimension = checkBoxHasDimension.Checked;
			ViewModel.StandardDimension = (IDAndName)comboBoxStandardDimension.SelectedItem;
			ViewModel.CustomDimensionName = textBoxCustomDimensionName.Text;
			ViewModel.Hidden = checkBoxHidden.Checked;
		}

		// Events

		private void checkBoxHasDimension_CheckedChanged(object sender, EventArgs e)
		{
			if (_applyViewModelToControlsIsBusy) return;

			if (ViewModel == null) return;

			ApplyControlsToViewModel();

			HasDimensionChanged.Invoke(this, new EventArgs<int>(ViewModel.ID));
		}
	}
}
