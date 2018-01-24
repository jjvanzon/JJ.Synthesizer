using System.Collections.Generic;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

// ReSharper disable LocalizableElement

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class MidiMappingElementPropertiesUserControl
		: PropertiesUserControlBase
	{
		public MidiMappingElementPropertiesUserControl() => InitializeComponent();

		// Gui

		protected override void AddProperties()
		{
			AddProperty(labelStandardDimension, comboBoxStandardDimension);
			AddProperty(labelCustomDimensionName, textBoxCustomDimensionName);
			AddProperty(labelControllerCode, maskedTextBoxControllerCode);
			AddProperty(new Label(), null);
			AddProperty(labelFromDimensionValue, textBoxFromDimensionValue);
			AddProperty(labelTillDimensionValue, textBoxTillDimensionValue);
			AddProperty(labelMinDimensionValue, textBoxMinDimensionValue);
			AddProperty(labelMaxDimensionValue, textBoxMaxDimensionValue);
			AddProperty(new Label(), null);
			AddProperty(labelFromControllerValue, maskedTextBoxFromControllerValue);
			AddProperty(labelTillControllerValue, maskedTextBoxTillControllerValue);
			AddProperty(new Label(), null);
			AddProperty(labelFromPosition, textBoxFromPosition);
			AddProperty(labelTillPosition, textBoxTillPosition);
			AddProperty(new Label(), null);
			AddProperty(labelScale, comboBoxScale);
			AddProperty(labelFromNoteNumber, maskedTextBoxFromNoteNumber);
			AddProperty(labelTillNoteNumber, maskedTextBoxTillNoteNumber);
			AddProperty(labelFromToneNumber, maskedTextBoxFromToneNumber);
			AddProperty(labelTillToneNumber, maskedTextBoxTillToneNumber);
			AddProperty(new Label(), null);
			AddProperty(labelIsRelative, checkBoxIsRelative);
			AddProperty(labelIsActive, checkBoxIsActive);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.MidiMappingElement);
			labelControllerCode.Text = ResourceFormatter.ControllerCode;
			labelFromControllerValue.Text = ResourceFormatter.FromControllerValue;
			labelTillControllerValue.Text = ResourceFormatter.TillControllerValue;
			labelFromNoteNumber.Text = ResourceFormatter.FromNoteNumber;
			labelTillNoteNumber.Text = ResourceFormatter.TillNoteNumber;
			labelStandardDimension.Text = ResourceFormatter.StandardDimension;
			labelCustomDimensionName.Text = ResourceFormatter.CustomDimensionName;
			labelFromDimensionValue.Text = ResourceFormatter.FromDimensionValue;
			labelTillDimensionValue.Text = ResourceFormatter.TillDimensionValue;
			labelMinDimensionValue.Text = ResourceFormatter.MinDimensionValue;
			labelMaxDimensionValue.Text = ResourceFormatter.MaxDimensionValue;
			labelFromPosition.Text = ResourceFormatter.FromPosition;
			labelTillPosition.Text = ResourceFormatter.TillPosition;
			labelScale.Text = ResourceFormatter.Scale;
			labelFromToneNumber.Text = ResourceFormatter.FromToneNumber;
			labelTillToneNumber.Text = ResourceFormatter.TillToneNumber;
			labelIsRelative.Text = ResourceFormatter.IsRelative;
			labelIsActive.Text = CommonResourceFormatter.IsActive;
			checkBoxIsActive.Text = "";
			checkBoxIsRelative.Text = "";
		}

		// Binding

		public new MidiMappingElementPropertiesViewModel ViewModel
		{
			get => (MidiMappingElementPropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override int GetID() => ViewModel?.ID ?? default;

		protected override void ApplyViewModelToControls()
		{
			maskedTextBoxControllerCode.Text = $"{ViewModel.ControllerCode}";
			maskedTextBoxFromControllerValue.Text = $"{ViewModel.FromControllerValue}";
			maskedTextBoxTillControllerValue.Text = $"{ViewModel.TillControllerValue}";
			maskedTextBoxFromNoteNumber.Text = $"{ViewModel.FromNoteNumber}";
			maskedTextBoxTillNoteNumber.Text = $"{ViewModel.TillNoteNumber}";

			if (comboBoxStandardDimension.DataSource == null)
			{
				comboBoxStandardDimension.ValueMember = nameof(IDAndName.ID);
				comboBoxStandardDimension.DisplayMember = nameof(IDAndName.Name);
				comboBoxStandardDimension.DataSource = ViewModel.StandardDimensionLookup;
			}

			comboBoxStandardDimension.SelectedValue = ViewModel.StandardDimension?.ID ?? 0;

			textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;
			textBoxFromDimensionValue.Text = ViewModel.FromDimensionValue;
			textBoxTillDimensionValue.Text = ViewModel.TillDimensionValue;
			textBoxMinDimensionValue.Text = ViewModel.MinDimensionValue;
			textBoxMaxDimensionValue.Text = ViewModel.MaxDimensionValue;
			textBoxFromPosition.Text = ViewModel.FromPosition;
			textBoxTillPosition.Text = ViewModel.TillPosition;

			maskedTextBoxFromToneNumber.Text = $"{ViewModel.FromToneNumber}";
			maskedTextBoxTillToneNumber.Text = $"{ViewModel.TillToneNumber}";
			checkBoxIsRelative.Checked = ViewModel.IsRelative;
			checkBoxIsActive.Checked = ViewModel.IsActive;
		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.ControllerCode = GetNullableInt32FromMaskedTextBox(maskedTextBoxControllerCode);
			ViewModel.FromControllerValue = GetNullableInt32FromMaskedTextBox(maskedTextBoxFromControllerValue);
			ViewModel.TillControllerValue = GetNullableInt32FromMaskedTextBox(maskedTextBoxTillControllerValue);
			ViewModel.FromNoteNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxFromNoteNumber);
			ViewModel.TillNoteNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxTillNoteNumber);
			ViewModel.StandardDimension = (IDAndName)comboBoxStandardDimension.SelectedItem;
			ViewModel.CustomDimensionName = textBoxCustomDimensionName.Text;
			ViewModel.FromDimensionValue = textBoxFromDimensionValue.Text;
			ViewModel.TillDimensionValue = textBoxTillDimensionValue.Text;
			ViewModel.MinDimensionValue = textBoxMinDimensionValue.Text;
			ViewModel.MaxDimensionValue = textBoxMaxDimensionValue.Text;
			ViewModel.FromPosition = textBoxFromPosition.Text;
			ViewModel.TillPosition = textBoxTillPosition.Text;
			ViewModel.Scale = (IDAndName)comboBoxScale.SelectedItem;
			ViewModel.FromToneNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxFromToneNumber);
			ViewModel.TillToneNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxTillToneNumber);
			ViewModel.IsRelative = checkBoxIsRelative.Checked;
			ViewModel.IsActive = checkBoxIsActive.Checked;
		}

		private int? GetNullableInt32FromMaskedTextBox(MaskedTextBox maskedTextBox)
		{
			string text = maskedTextBox.Text;
			if (string.IsNullOrWhiteSpace(text))
			{
				return null;
			}
			return int.Parse(text);
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