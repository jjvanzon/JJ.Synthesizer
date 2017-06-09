using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Bases
{
    internal class OperatorPropertiesUserControlBase : PropertiesUserControlBase
    {
        protected readonly Label _labelName;
        protected readonly TextBox _textBoxName;
        protected readonly Label _labelOperatorTypeTitle;
        protected readonly Label _labelOperatorTypeValue;
        protected readonly Label _labelUnderlyingPatch;
        protected readonly ComboBox _comboBoxUnderlyingPatch;
        protected readonly Label _labelStandardDimension;
        protected readonly ComboBox _comboBoxStandardDimension;
        protected readonly TextBox _textBoxCustomDimensionName;
        protected readonly Label _labelCustomDimensionName;

        public OperatorPropertiesUserControlBase()
        {
            Name = GetType().Name;

            PlayButtonVisible = true;

            _labelOperatorTypeTitle = CreateLabelOperatorTypeTitle();
            _labelOperatorTypeValue = CreateLabelOperatorTypeValue();
            Controls.Add(_labelOperatorTypeTitle);
            Controls.Add(_labelOperatorTypeValue);

            _labelUnderlyingPatch = CreateLabelUnderlyingPatch();
            _comboBoxUnderlyingPatch = CreateComboBoxUnderlyingPatch();
            Controls.Add(_labelUnderlyingPatch);
            Controls.Add(_comboBoxUnderlyingPatch);

            _labelStandardDimension = CreateLabelStandardDimension();
            _comboBoxStandardDimension = CreateComboBoxStandardDimension();
            Controls.Add(_labelStandardDimension);
            Controls.Add(_comboBoxStandardDimension);

            _labelCustomDimensionName = CreateLabelCustomDimension();
            _textBoxCustomDimensionName = CreateTextBoxCustomDimensionName();
            Controls.Add(_labelCustomDimensionName);
            Controls.Add(_textBoxCustomDimensionName);

            _labelName = CreateLabelName();
            _textBoxName = CreateTextBoxName();
            Controls.Add(_labelName);
            Controls.Add(_textBoxName);
        }

        public new OperatorPropertiesViewModelBase ViewModel
        {
            get => (OperatorPropertiesViewModelBase)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override int GetID() => ViewModel.ID;

        protected override void SetTitles()
        {
            TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Operator);
            _labelName.Text = CommonResourceFormatter.Name;
            // ReSharper disable once LocalizableElement
            _labelOperatorTypeTitle.Text = CommonResourceFormatter.Type + ":";
            _labelUnderlyingPatch.Text = ResourceFormatter.UnderlyingPatch;
            _labelStandardDimension.Text = ResourceFormatter.StandardDimension;
            _labelCustomDimensionName.Text = ResourceFormatter.CustomDimension;
        }

        protected override void ApplyViewModelToControls()
        {
            _textBoxName.Text = ViewModel.Name;
            _labelOperatorTypeValue.Text = ViewModel.OperatorType.Name;

            _labelUnderlyingPatch.Visible = ViewModel.UnderlyingPatchVisible;
            _comboBoxUnderlyingPatch.SelectedValue = ViewModel.UnderlyingPatch?.ID ?? 0;
            _comboBoxUnderlyingPatch.Visible = ViewModel.UnderlyingPatchVisible;

            _comboBoxStandardDimension.Visible = ViewModel.StandardDimensionVisible;
            _labelStandardDimension.Visible = ViewModel.StandardDimensionVisible;
            _labelCustomDimensionName.Visible = ViewModel.CustomDimensionNameVisible;
            _textBoxCustomDimensionName.Visible = ViewModel.CustomDimensionNameVisible;

            _textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;

            if (_comboBoxStandardDimension.DataSource == null)
            {
                _comboBoxStandardDimension.ValueMember = nameof(IDAndName.ID);
                _comboBoxStandardDimension.DisplayMember = nameof(IDAndName.Name);
                _comboBoxStandardDimension.DataSource = ViewModel.StandardDimensionLookup;
            }

            _comboBoxStandardDimension.SelectedValue = ViewModel.StandardDimension?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = _textBoxName.Text;
            ViewModel.UnderlyingPatch = (IDAndName)_comboBoxUnderlyingPatch.SelectedItem;
            ViewModel.StandardDimension = (IDAndName)_comboBoxStandardDimension.SelectedItem;
            ViewModel.CustomDimensionName = _textBoxCustomDimensionName.Text;
        }

        public void SetUnderlyingPatchLookup(IList<IDAndName> underlyingPatchLookup)
        {
            // Always refill the document lookup, so changes to the document collection are reflected.

            int? selectedID = TryGetSelectedUnderlyingPatchID();
            _comboBoxUnderlyingPatch.DataSource = null; // Do this or WinForms will not refresh the list.
            _comboBoxUnderlyingPatch.ValueMember = nameof(IDAndName.ID);
            _comboBoxUnderlyingPatch.DisplayMember = nameof(IDAndName.Name);
            _comboBoxUnderlyingPatch.DataSource = underlyingPatchLookup;
            if (selectedID != null)
            {
                _comboBoxUnderlyingPatch.SelectedValue = selectedID;
            }
        }

        private int? TryGetSelectedUnderlyingPatchID()
        {
            if (_comboBoxUnderlyingPatch.DataSource == null) return null;
            var idAndName = (IDAndName)_comboBoxUnderlyingPatch.SelectedItem;
            return idAndName?.ID;
        }

        // Creating Controls

        private Label CreateLabelOperatorTypeTitle() => CreateLabel(nameof(_labelOperatorTypeTitle));
        private Label CreateLabelOperatorTypeValue() => CreateLabel(nameof(_labelOperatorTypeValue), ContentAlignment.MiddleLeft);
        private Label CreateLabelUnderlyingPatch() => CreateLabel(nameof(_labelUnderlyingPatch));
        private Label CreateLabelStandardDimension() => CreateLabel(nameof(_labelStandardDimension));
        private Label CreateLabelCustomDimension() => CreateLabel(nameof(_labelCustomDimensionName));
        private TextBox CreateTextBoxCustomDimensionName() => CreateTextBox(nameof(_textBoxCustomDimensionName));
        private Label CreateLabelName() => CreateLabel(nameof(_labelName));
        private TextBox CreateTextBoxName() => CreateTextBox(nameof(_textBoxName));

        private ComboBox CreateComboBoxUnderlyingPatch()
        {
            var comboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FormattingEnabled = true,
                Margin = new Padding(0),
                Name = nameof(_comboBoxUnderlyingPatch)
            };

            return comboBox;
        }

        private ComboBox CreateComboBoxStandardDimension()
        {
            var comboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                FormattingEnabled = true,
                Margin = new Padding(0),
                Name = nameof(_comboBoxStandardDimension)
            };

            return comboBox;
        }

        private Label CreateLabel(string name, ContentAlignment textAlign = ContentAlignment.MiddleRight)
        {
            var label = new Label
            {
                Margin = new Padding(0),
                TextAlign = textAlign,
                Name = name
            };
            return label;
        }

        private TextBox CreateTextBox(string name)
        {
            var textBox = new TextBox
            {
                Margin = new Padding(0),
                Name = name
            };

            return textBox;
        }
    }
}
