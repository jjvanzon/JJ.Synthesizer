using System.Collections.Generic;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Common;
using JJ.Framework.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

// ReSharper disable LocalizableElement

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class MidiMappingElementPropertiesUserControl
		: PropertiesUserControlBase
	{
		public MidiMappingElementPropertiesUserControl()
		{
			InitializeComponent();
		}

		// Gui

		protected override void AddProperties()
		{
			AddProperty(labelStandardDimension, comboBoxStandardDimension);
			AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
			AddProperty(labelMidiControllerCode, maskedTextBoxMidiControllerCode);
			AddSpacing();
			AddProperty(labelDimensionValues, fromTillUserControlDimensionValues);
			AddProperty(labelMinMaxDimensionValues, fromTillUserControlMinMaxDimensionValues);
			AddSpacing();
			AddProperty(labelMidiControllerValues, fromTillUserControlMidiControllerValues);
			AddProperty(labelMidiVelocities, fromTillUserControlMidiVelocities);
			AddProperty(labelPositions, fromTillUserControlPositions);
			AddProperty(labelScale, comboBoxScale);
			AddProperty(labelMidiNoteNumbers, fromTillUserControlMidiNoteNumbers);
			AddProperty(labelToneNumbers, fromTillUserControlToneNumbers);
			AddSpacing();
			AddProperty(labelIsRelative, checkBoxIsRelative);
			AddProperty(labelIsActive, checkBoxIsActive);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.MidiMappingElement);
			labelMidiControllerCode.Text = ResourceFormatter.MidiControllerCode;
			labelCustomDimensionName.Text = ResourceFormatter.CustomDimension;
			labelMidiControllerValues.Text = ResourceFormatter.MidiControllerValues;
			labelDimensionValues.Text = ResourceFormatter.DimensionValues;
			labelMidiNoteNumbers.Text = ResourceFormatter.MidiNoteNumbers;
			labelPositions.Text = ResourceFormatter.Positions;
			labelToneNumbers.Text = ResourceFormatter.ToneNumbers;
			labelMidiVelocities.Text = ResourceFormatter.MidiVelocities;
			labelIsActive.Text = CommonResourceFormatter.IsActive;
			labelIsRelative.Text = ResourceFormatter.IsRelative;
			labelMinMaxDimensionValues.Text = ResourceFormatter.MinMaxDimensionValues;
			labelScale.Text = ResourceFormatter.Scale;
			labelStandardDimension.Text = ResourceFormatter.StandardDimension;
			checkBoxIsActive.Text = "";
			checkBoxIsRelative.Text = "";
		}

		// Binding

		public new MidiMappingElementPropertiesViewModel ViewModel
		{
			get => (MidiMappingElementPropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID()
		{
			return ViewModel?.ID ?? default;
		}

		protected override void ApplyViewModelToControls()
		{
			checkBoxIsActive.Checked = ViewModel.IsActive;
			checkBoxIsRelative.Checked = ViewModel.IsRelative;
			comboBoxScale.SelectedValue = ViewModel.Scale?.ID ?? 0;
			fromTillUserControlMidiControllerValues.From = $"{ViewModel.FromMidiControllerValue}";
			fromTillUserControlMidiControllerValues.From = $"{ViewModel.FromMidiControllerValue}";
			fromTillUserControlMidiControllerValues.Till = $"{ViewModel.TillMidiControllerValue}";
			fromTillUserControlMidiControllerValues.Till = $"{ViewModel.TillMidiControllerValue}m";
			fromTillUserControlDimensionValues.From = ViewModel.FromDimensionValue;
			fromTillUserControlDimensionValues.From = ViewModel.FromDimensionValue;
			fromTillUserControlDimensionValues.Till = ViewModel.TillDimensionValue;
			fromTillUserControlDimensionValues.Till = ViewModel.TillDimensionValue;
			fromTillUserControlMinMaxDimensionValues.From = ViewModel.MinDimensionValue;
			fromTillUserControlMinMaxDimensionValues.Till = ViewModel.MaxDimensionValue;
			fromTillUserControlMidiNoteNumbers.From = $"{ViewModel.FromMidiNoteNumber}";
			fromTillUserControlMidiNoteNumbers.Till = $"{ViewModel.TillMidiNoteNumber}";
			fromTillUserControlPositions.From = ViewModel.FromPosition;
			fromTillUserControlPositions.Till = ViewModel.TillPosition;
			fromTillUserControlToneNumbers.From = $"{ViewModel.FromToneNumber}";
			fromTillUserControlToneNumbers.Till = $"{ViewModel.TillToneNumber}";
			fromTillUserControlMidiVelocities.From = $"{ViewModel.FromMidiVelocity}";
			fromTillUserControlMidiVelocities.Till = $"{ViewModel.TillMidiVelocity}";
			maskedTextBoxMidiControllerCode.Text = $"{ViewModel.MidiControllerCode}";
			textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;

			if (comboBoxStandardDimension.DataSource == null)
			{
				comboBoxStandardDimension.ValueMember = nameof(IDAndName.ID);
				comboBoxStandardDimension.DisplayMember = nameof(IDAndName.Name);
				comboBoxStandardDimension.DataSource = ViewModel.StandardDimensionLookup;
			}

			comboBoxStandardDimension.SelectedValue = ViewModel.StandardDimension?.ID ?? 0;

		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.MidiControllerCode = Int32Helper.ParseNullable(maskedTextBoxMidiControllerCode.Text);
			ViewModel.CustomDimensionName = textBoxCustomDimensionName.Text;
			ViewModel.FromMidiControllerValue = Int32Helper.ParseNullable(fromTillUserControlMidiControllerValues.From);
			ViewModel.FromDimensionValue = fromTillUserControlDimensionValues.From;
			ViewModel.FromMidiNoteNumber = Int32Helper.ParseNullable(fromTillUserControlMidiNoteNumbers.From);
			ViewModel.FromPosition =  fromTillUserControlPositions.From;
			ViewModel.FromToneNumber = Int32Helper.ParseNullable(fromTillUserControlToneNumbers.From);
			ViewModel.FromMidiVelocity = Int32Helper.ParseNullable(fromTillUserControlMidiVelocities.From);
			ViewModel.IsActive = checkBoxIsActive.Checked;
			ViewModel.IsRelative = checkBoxIsRelative.Checked;
			ViewModel.MaxDimensionValue = fromTillUserControlMinMaxDimensionValues.Till;
			ViewModel.MinDimensionValue = fromTillUserControlMinMaxDimensionValues.From;
			ViewModel.Scale = (IDAndName)comboBoxScale.SelectedItem;
			ViewModel.StandardDimension = (IDAndName)comboBoxStandardDimension.SelectedItem;
			ViewModel.TillMidiControllerValue = Int32Helper.ParseNullable(fromTillUserControlMidiControllerValues.Till);
			ViewModel.TillDimensionValue = fromTillUserControlDimensionValues.Till;
			ViewModel.TillMidiNoteNumber = Int32Helper.ParseNullable(fromTillUserControlMidiNoteNumbers.Till);
			ViewModel.TillPosition = fromTillUserControlPositions.Till;
			ViewModel.TillToneNumber = Int32Helper.ParseNullable(fromTillUserControlToneNumbers.Till);
			ViewModel.TillMidiVelocity = Int32Helper.ParseNullable(fromTillUserControlMidiVelocities.Till);
		}

		public void SetScaleLookup(IList<IDAndName> scaleLookup)
		{
			if (ViewModel == null) return;

			comboBoxScale.DataSource = null; // Do this or WinForms will not refresh the list.
			comboBoxScale.ValueMember = nameof(IDAndName.ID);
			comboBoxScale.DisplayMember = nameof(IDAndName.Name);
			comboBoxScale.DataSource = scaleLookup;
		}
	}
}