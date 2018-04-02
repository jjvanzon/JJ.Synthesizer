using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Common;
using JJ.Framework.Conversion;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
// ReSharper disable LocalizableElement
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class MidiMappingPropertiesUserControl
		: PropertiesUserControlBase
	{
		private bool _applyViewModelToControlsIsBusy;

		public event EventHandler<EventArgs<int>> MidiMappingTypeChanged;

		public MidiMappingPropertiesUserControl() => InitializeComponent();

		// Gui

		protected override void AddProperties()
		{
			AddProperty(labelMidiMappingType, comboBoxMidiMappingType);
			AddProperty(labelMidiValues, fromTillMidiValues);
			AddProperty(labelMidiControllerCode, maskedTextBoxMidiControllerCode);
			AddSpacing();
			AddProperty(labelDimension, comboBoxDimension);
			AddProperty(labelName, textBoxName);
			AddProperty(labelPosition, textBoxPosition);
			AddSpacing();
			AddProperty(labelDimensionValues, fromTillDimensionValues);
			AddProperty(labelMinMaxDimensionValues, fromTillMinMaxDimensionValues);
			AddSpacing();
			AddProperty(labelIsRelative, checkBoxIsRelative);
			AddProperty(labelIsActive, checkBoxIsActive);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.MidiMapping);

			labelMidiMappingType.Text = CommonResourceFormatter.Type;
			labelMidiValues.Text = ResourceFormatter.MidiValues;
			labelMidiControllerCode.Text = ResourceFormatter.MidiControllerCode;

			labelDimension.Text = ResourceFormatter.Dimension;
			labelName.Text = CommonResourceFormatter.Name;
			labelPosition.Text = ResourceFormatter.Position;

			labelDimensionValues.Text = ResourceFormatter.DimensionValues;
			labelMinMaxDimensionValues.Text = ResourceFormatter.MinMaxDimensionValues;

			labelIsActive.Text = CommonResourceFormatter.IsActive;
			labelIsRelative.Text = ResourceFormatter.IsRelative;
			checkBoxIsActive.Text = "";
			checkBoxIsRelative.Text = "";
		}

		// Binding

		public new MidiMappingPropertiesViewModel ViewModel
		{
			get => (MidiMappingPropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel?.ID ?? default;

		protected override void ApplyViewModelToControls()
		{
			_applyViewModelToControlsIsBusy = true;
			try
			{
				if (comboBoxMidiMappingType.DataSource == null)
				{
					comboBoxMidiMappingType.DataSource = null; // Do this or WinForms will not refresh the list.
					comboBoxMidiMappingType.ValueMember = nameof(IDAndName.ID);
					comboBoxMidiMappingType.DisplayMember = nameof(IDAndName.Name);
					comboBoxMidiMappingType.DataSource = ViewModel.MidiMappingTypeLookup;
				}

				comboBoxMidiMappingType.SelectedValue = ViewModel.MidiMappingType?.ID ?? 0;
				fromTillMidiValues.From = $"{ViewModel.FromMidiValue}";
				fromTillMidiValues.Till = $"{ViewModel.TillMidiValue}";
				maskedTextBoxMidiControllerCode.Text = $"{ViewModel.MidiControllerCode}";
				maskedTextBoxMidiControllerCode.Visible = ViewModel.CanEditMidiControllerCode;
				labelMidiControllerCode.Visible = ViewModel.CanEditMidiControllerCode;

				if (comboBoxDimension.DataSource == null)
				{
					comboBoxDimension.ValueMember = nameof(IDAndName.ID);
					comboBoxDimension.DisplayMember = nameof(IDAndName.Name);
					comboBoxDimension.DataSource = ViewModel.DimensionLookup;
				}

				comboBoxDimension.SelectedValue = ViewModel.Dimension?.ID ?? 0;
				textBoxName.Text = ViewModel.Name;
				textBoxPosition.Text = ViewModel.Position;

				fromTillDimensionValues.From = ViewModel.FromDimensionValue;
				fromTillDimensionValues.Till = ViewModel.TillDimensionValue;
				fromTillMinMaxDimensionValues.From = ViewModel.MinDimensionValue;
				fromTillMinMaxDimensionValues.Till = ViewModel.MaxDimensionValue;

				checkBoxIsActive.Checked = ViewModel.IsActive;
				checkBoxIsRelative.Checked = ViewModel.IsRelative;
			}
			finally
			{
				_applyViewModelToControlsIsBusy = false;
			}
		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.MidiMappingType = (IDAndName)comboBoxMidiMappingType.SelectedItem;
			ViewModel.FromMidiValue = Int32Parser.ParseNullable(fromTillMidiValues.From) ?? 0;
			ViewModel.TillMidiValue = Int32Parser.ParseNullable(fromTillMidiValues.Till) ?? 0;
			ViewModel.MidiControllerCode = Int32Parser.ParseNullable(maskedTextBoxMidiControllerCode.Text);

			ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
			ViewModel.Name = textBoxName.Text;
			ViewModel.Position = textBoxPosition.Text;

			ViewModel.FromDimensionValue = fromTillDimensionValues.From;
			ViewModel.TillDimensionValue = fromTillDimensionValues.Till;
			ViewModel.MinDimensionValue = fromTillMinMaxDimensionValues.From;
			ViewModel.MaxDimensionValue = fromTillMinMaxDimensionValues.Till;

			ViewModel.IsActive = checkBoxIsActive.Checked;
			ViewModel.IsRelative = checkBoxIsRelative.Checked;
		}

		private void comboBoxMidiMappingType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_applyViewModelToControlsIsBusy) return;
			if (ViewModel == null) return;

			ApplyControlsToViewModel();

			MidiMappingTypeChanged.Invoke(this, new EventArgs<int>(ViewModel.ID));
		}
	}
}