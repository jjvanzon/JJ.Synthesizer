using System;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
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
			AddProperty(labelIsActive, checkBoxIsActive);
			AddProperty(labelIsRelative, checkBoxIsRelative);
			AddProperty(labelControllerCode,maskedTextBoxControllerCode);
			//AddProperty(labelFromControllerValue, maskedTextBoxFromControllerValue);
			//AddProperty(labelTillControllerValue,maskedTextBoxTillControllerValue);
			//AddProperty(labelFromNoteNumber, maskedTextBoxFromNoteNumber);
			//AddProperty(labelTillNoteNumber,maskedTextBoxTillNoteNumber);
			//AddProperty(labelStandardDimension, comboBoxStandardDimension);
			//AddProperty(labelCustomDimensionName,textBoxCustomDimensionName);
			//AddProperty(labelFromDimensionValue, textBoxFromDimensionValue);
			//AddProperty(labelTillDimensionValue,textBoxTillDimensionValue);
			//AddProperty(labelFromPosition, textBoxFromPosition);
			//AddProperty(labelTillPosition,textBoxTillPosition);
			//AddProperty(labelScale, comboBoxScale);
			//AddProperty(labelFromToneNumber,maskedTextBoxFromToneNumber);
			//AddProperty(labelTillToneNumber, maskedTextBoxTillToneNumber);
		}

		protected override void SetTitles()
		{
			TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.MidiMappingElement);
			labelIsActive.Text = CommonResourceFormatter.IsActive;
			labelIsRelative.Text = ResourceFormatter.IsRelative;
			labelControllerCode.Text = ResourceFormatter.ControllerCode;
			//labelFromControllerValue.Text = ResourceFormatter.FromControllerValue;
			//labelTillControllerValue.Text = ResourceFormatter.TillControllerValue;
			//labelFromNoteNumber.Text = ResourceFormatter.FromNoteNumber;
			//labelTillNoteNumber.Text = ResourceFormatter.TillNoteNumber;
			//labelStandardDimension.Text = ResourceFormatter.StandardDimension;
			//labelCustomDimensionName.Text = ResourceFormatter.CustomDimensionName;
			//labelFromDimensionValue.Text = ResourceFormatter.FromDimensionValue;
			//labelTillDimensionValue.Text = ResourceFormatter.TillDimensionValue;
			//labelFromPosition.Text = ResourceFormatter.FromPosition;
			//labelTillPosition.Text = ResourceFormatter.TillPosition;
			//labelScale.Text = ResourceFormatter.Scale;
			//labelFromToneNumber.Text = ResourceFormatter.FromToneNumber;
			//labelTillToneNumber.Text = ResourceFormatter.TillToneNumber;
		}

		// Binding

		public new MidiMappingElementPropertiesViewModel ViewModel
		{
			get => (MidiMappingElementPropertiesViewModel)base.ViewModel;
			set => base.ViewModel = value;
		}

		protected override void ApplyViewModelToControls()
		{
			checkBoxIsActive.Checked = ViewModel.IsActive;
			checkBoxIsRelative.Checked = ViewModel.IsRelative;
			maskedTextBoxControllerCode.Text = $"{ViewModel.ControllerCode}";
			//maskedTextBoxFromControllerValue.Text = $"{ViewModel.FromControllerValue}";
			//maskedTextBoxTillControllerValue.Text = $"{ViewModel.TillControllerValue}";
			//maskedTextBoxFromNoteNumber.Text = $"{ViewModel.FromNoteNumber}";
			//maskedTextBoxTillNoteNumber.Text = $"{ViewModel.TillNoteNumber}";

			//if (comboBoxStandardDimension.DataSource == null)
			//{
			//	comboBoxStandardDimension.ValueMember = nameof(IDAndName.ID);
			//	comboBoxStandardDimension.DisplayMember = nameof(IDAndName.Name);
			//	comboBoxStandardDimension.DataSource = ViewModel.StandardDimensionLookup;
			//}
			//comboBoxStandardDimension.SelectedValue = ViewModel.StandardDimension?.ID ?? 0;

			//textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;
			//textBoxFromDimensionValue.Text = $"{ViewModel.FromDimensionValue}";
			//textBoxTillDimensionValue.Text = $"{ViewModel.TillDimensionValue}";
			//textBoxFromPosition.Text = $"{ViewModel.FromPosition}";
			//textBoxTillPosition.Text = $"{ViewModel.TillPosition}";

			//comboBoxScale.ValueMember = nameof(IDAndName.ID);
			//comboBoxScale.DisplayMember = nameof(IDAndName.Name);
			//comboBoxScale.DataSource = ViewModel.ScaleLookup;
			//comboBoxScale.SelectedValue = ViewModel.Scale?.ID ?? 0;

			//maskedTextBoxFromToneNumber.Text = $"{ViewModel.FromToneNumber}";
			//maskedTextBoxTillToneNumber.Text = $"{ViewModel.TillToneNumber}";
		}

		protected override void ApplyControlsToViewModel()
		{
			ViewModel.IsActive = checkBoxIsActive.Checked;
			ViewModel.IsRelative = checkBoxIsRelative.Checked;
			//ViewModel.ControllerCode = GetNullableInt32FromMaskedTextBox(maskedTextBoxControllerCode);
			//ViewModel.FromControllerValue = GetNullableInt32FromMaskedTextBox(maskedTextBoxFromControllerValue);
			//ViewModel.TillControllerValue = GetNullableInt32FromMaskedTextBox(maskedTextBoxTillControllerValue);
			//ViewModel.FromNoteNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxFromNoteNumber);
			//ViewModel.TillNoteNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxTillNoteNumber);
			//ViewModel.StandardDimension = (IDAndName)comboBoxStandardDimension.SelectedItem;
			//ViewModel.CustomDimensionName = textBoxCustomDimensionName.Text;
			//ViewModel.FromDimensionValue = GetNullableDoubleFromTextBox(textBoxFromDimensionValue);
			//ViewModel.TillDimensionValue = GetNullableDoubleFromTextBox(textBoxTillDimensionValue);
			//ViewModel.FromPosition = GetNullableInt32FromMaskedTextBox(textBoxFromPosition);
			//ViewModel.TillPosition = GetNullableInt32FromMaskedTextBox(textBoxTillPosition);
			//ViewModel.Scale = (IDAndName)comboBoxScale.SelectedItem;
			//ViewModel.FromToneNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxFromToneNumber);
			//ViewModel.TillToneNumber = GetNullableInt32FromMaskedTextBox(maskedTextBoxTillToneNumber);
		}

		private int? GetNullableInt32FromMaskedTextBox(MaskedTextBox maskedTextBox)
		{
			throw new NotImplementedException();
		}

		private double GetNullableDoubleFromTextBox(MaskedTextBox maskedTextBox)
		{
			throw new NotImplementedException();
		}
	}
}
