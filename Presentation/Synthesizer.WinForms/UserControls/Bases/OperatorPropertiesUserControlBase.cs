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
        protected readonly Label _labelUnderlyingPatch;
        protected readonly ComboBox _comboBoxUnderlyingPatch;
        protected readonly Label _labelStandardDimension;
        protected readonly ComboBox _comboBoxStandardDimension;
        protected readonly Label _labelCustomDimensionName;
        protected readonly TextBox _textBoxCustomDimensionName;
        protected readonly Label _labelInletCount;
        protected readonly NumericUpDown _numericUpDownInletCount;
        protected readonly Label _labelOutletCount;
        protected readonly NumericUpDown _numericUpDownOutletCount;

        public OperatorPropertiesUserControlBase()
        {
            Name = GetType().Name;

            PlayButtonVisible = true;
            ExpandButtonVisible = true;

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

            _labelInletCount = CreateLabelInletCount();
            _numericUpDownInletCount = CreateNumericUpDownInletCount();

            _labelOutletCount = CreateLabelOutletCount();
            _numericUpDownOutletCount = CreateNumericUpDownOutletCount();
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
            _labelUnderlyingPatch.Text = CommonResourceFormatter.Type;
            _labelStandardDimension.Text = ResourceFormatter.StandardDimension;
            _labelCustomDimensionName.Text = ResourceFormatter.CustomDimension;
            _labelInletCount.Text = CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets);
            _labelOutletCount.Text = CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets);
            _labelName.Text = CommonResourceFormatter.Name;
        }

        protected override void ApplyViewModelToControls()
        {
            _textBoxName.Text = ViewModel.Name;

            _comboBoxUnderlyingPatch.SelectedValue = ViewModel.UnderlyingPatch?.ID ?? 0;

            _textBoxCustomDimensionName.Text = ViewModel.CustomDimensionName;
            _textBoxCustomDimensionName.Visible = ViewModel.CanEditCustomDimensionName;
            _labelCustomDimensionName.Visible = ViewModel.CanEditCustomDimensionName;

            if (_comboBoxStandardDimension.DataSource == null)
            {
                _comboBoxStandardDimension.ValueMember = nameof(IDAndName.ID);
                _comboBoxStandardDimension.DisplayMember = nameof(IDAndName.Name);
                _comboBoxStandardDimension.DataSource = ViewModel.StandardDimensionLookup;
            }
            _comboBoxStandardDimension.SelectedValue = ViewModel.StandardDimension?.ID ?? 0;
            _comboBoxStandardDimension.Visible = ViewModel.CanSelectStandardDimension;
            _labelStandardDimension.Visible = ViewModel.CanSelectStandardDimension;

            if (ViewModel.CanEditInletCount)
            {
                _numericUpDownInletCount.Value = ViewModel.InletCount;
            }
            _numericUpDownInletCount.Visible = ViewModel.CanEditInletCount;
            _labelInletCount.Visible = ViewModel.CanEditInletCount;

            if (ViewModel.CanEditOutletCount)
            {
                _numericUpDownOutletCount.Value = ViewModel.OutletCount;
            }
            _numericUpDownOutletCount.Visible = ViewModel.CanEditOutletCount;
            _labelOutletCount.Visible = ViewModel.CanEditOutletCount;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = _textBoxName.Text;
            ViewModel.UnderlyingPatch = (IDAndName)_comboBoxUnderlyingPatch.SelectedItem;
            ViewModel.StandardDimension = (IDAndName)_comboBoxStandardDimension.SelectedItem;
            ViewModel.CustomDimensionName = _textBoxCustomDimensionName.Text;
            ViewModel.InletCount = (int)_numericUpDownInletCount.Value;
            ViewModel.OutletCount = (int)_numericUpDownOutletCount.Value;
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

        private Label CreateLabelUnderlyingPatch() => CreateLabel(nameof(_labelUnderlyingPatch));
        private Label CreateLabelStandardDimension() => CreateLabel(nameof(_labelStandardDimension));
        private Label CreateLabelCustomDimension() => CreateLabel(nameof(_labelCustomDimensionName));
        private TextBox CreateTextBoxCustomDimensionName() => CreateTextBox(nameof(_textBoxCustomDimensionName));
        private Label CreateLabelName() => CreateLabel(nameof(_labelName));
        private TextBox CreateTextBoxName() => CreateTextBox(nameof(_textBoxName));
        private Label CreateLabelInletCount() => CreateLabel(nameof(_labelInletCount));
        private NumericUpDown CreateNumericUpDownInletCount() => CreateNumericUpDown(nameof(_numericUpDownInletCount));
        private Label CreateLabelOutletCount() => CreateLabel(nameof(_labelOutletCount));
        private NumericUpDown CreateNumericUpDownOutletCount() => CreateNumericUpDown(nameof(_numericUpDownOutletCount));

        private NumericUpDown CreateNumericUpDown(string name)
        {
            var control = new NumericUpDown
            {
                Margin = new Padding(0),
                Name = name,
                Maximum = new decimal(new[] { 128, 0, 0, 0 }),
                Minimum = new decimal(new[] { 1, 0, 0, 0 })
            };

            return control;
        }

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
