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
			AddProperty(labelControllerCode, maskedTextBoxControllerCode);
			AddSpacing();
			AddProperty(labelDimensionValues, fromTillUserControlDimensionValues);
			AddProperty(labelMinMaxDimensionValues, fromTillUserControlMinMaxDimensionValues);
			AddSpacing();
			AddProperty(labelControllerValues, fromTillUserControlControllerValues);
			AddProperty(labelVelocities, fromTillUserControlVelocities);
			AddProperty(labelPositions, fromTillUserControlPositions);
			AddProperty(labelScale, comboBoxScale);
			AddProperty(labelNoteNumbers, fromTillUserControlNoteNumbers);
			AddProperty(labelToneNumbers, fromTillUserControlToneNumbers);
			AddSpacing();
			AddProperty(labelIsRelative, checkBoxIsRelative);
			AddProperty(labelIsActive, checkBoxIsActive);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.MidiMappingElement);
			labelControllerCode.Text = ResourceFormatter.ControllerCode;
			labelCustomDimensionName.Text = ResourceFormatter.CustomDimension;
			labelControllerValues.Text = ResourceFormatter.ControllerValues;
			labelDimensionValues.Text = ResourceFormatter.DimensionValues;
			labelNoteNumbers.Text = ResourceFormatter.NoteNumbers;
			labelPositions.Text = ResourceFormatter.Positions;
			labelToneNumbers.Text = ResourceFormatter.ToneNumbers;
			labelVelocities.Text = ResourceFormatter.Velocities;
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
			fromTillUserControlControllerValues.From = $"{ViewModel.FromControllerValue}";
			fromTillUserControlControllerValues.From = $"{ViewModel.FromControllerValue}";
			fromTillUserControlControllerValues.Till = $"{ViewModel.TillControllerValue}";
			fromTillUserControlControllerValues.Till = $"{ViewModel.TillControllerValue}m";
			fromTillUserControlDimensionValues.From = ViewModel.FromDimensionValue;
			fromTillUserControlDimensionValues.From = ViewModel.FromDimensionValue;
			fromTillUserControlDimensionValues.Till = ViewModel.TillDimensionValue;
			fromTillUserControlDimensionValues.Till = ViewModel.TillDimensionValue;
			fromTillUserControlMinMaxDimensionValues.From = ViewModel.MinDimensionValue;
			fromTillUserControlMinMaxDimensionValues.Till = ViewModel.MaxDimensionValue;
			fromTillUserControlNoteNumbers.From = $"{ViewModel.FromNoteNumber}";
			fromTillUserControlNoteNumbers.Till = $"{ViewModel.TillNoteNumber}";
			fromTillUserControlPositions.From = ViewModel.FromPosition;
			fromTillUserControlPositions.Till = ViewModel.TillPosition;
			fromTillUserControlToneNumbers.From = $"{ViewModel.FromToneNumber}";
			fromTillUserControlToneNumbers.Till = $"{ViewModel.TillToneNumber}";
			fromTillUserControlVelocities.From = $"{ViewModel.FromVelocity}";
			fromTillUserControlVelocities.Till = $"{ViewModel.TillVelocity}";
			maskedTextBoxControllerCode.Text = $"{ViewModel.ControllerCode}";
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
			ViewModel.ControllerCode = Int32Helper.ParseNullable(maskedTextBoxControllerCode.Text);
			ViewModel.CustomDimensionName = textBoxCustomDimensionName.Text;
			ViewModel.FromControllerValue = Int32Helper.ParseNullable(fromTillUserControlControllerValues.From);
			ViewModel.FromDimensionValue = fromTillUserControlDimensionValues.From;
			ViewModel.FromNoteNumber = Int32Helper.ParseNullable(fromTillUserControlNoteNumbers.From);
			ViewModel.FromPosition =  fromTillUserControlPositions.From;
			ViewModel.FromToneNumber = Int32Helper.ParseNullable(fromTillUserControlToneNumbers.From);
			ViewModel.FromVelocity = Int32Helper.ParseNullable(fromTillUserControlVelocities.From);
			ViewModel.IsActive = checkBoxIsActive.Checked;
			ViewModel.IsRelative = checkBoxIsRelative.Checked;
			ViewModel.MaxDimensionValue = fromTillUserControlMinMaxDimensionValues.Till;
			ViewModel.MinDimensionValue = fromTillUserControlMinMaxDimensionValues.From;
			ViewModel.Scale = (IDAndName)comboBoxScale.SelectedItem;
			ViewModel.StandardDimension = (IDAndName)comboBoxStandardDimension.SelectedItem;
			ViewModel.TillControllerValue = Int32Helper.ParseNullable(fromTillUserControlControllerValues.Till);
			ViewModel.TillDimensionValue = fromTillUserControlDimensionValues.Till;
			ViewModel.TillNoteNumber = Int32Helper.ParseNullable(fromTillUserControlNoteNumbers.Till);
			ViewModel.TillPosition = fromTillUserControlPositions.Till;
			ViewModel.TillToneNumber = Int32Helper.ParseNullable(fromTillUserControlToneNumbers.Till);
			ViewModel.TillVelocity = Int32Helper.ParseNullable(fromTillUserControlVelocities.Till);
		}

		public void SetScaleLookup(IList<IDAndName> underlyingPatchLookup)
		{
			if (ViewModel == null) return;

			comboBoxScale.DataSource = null; // Do this or WinForms will not refresh the list.
			comboBoxScale.ValueMember = nameof(IDAndName.ID);
			comboBoxScale.DisplayMember = nameof(IDAndName.Name);
			comboBoxScale.DataSource = underlyingPatchLookup;
			comboBoxScale.SelectedValue = ViewModel.Scale?.ID ?? 0;
		}
	}
}