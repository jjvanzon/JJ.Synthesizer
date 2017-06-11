using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForPatchInlet 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForPatchInlet() => InitializeComponent();

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelNumber.Text = ResourceFormatter.Number;
            labelDimension.Text = ResourceFormatter.Dimension;
            labelDefaultValue.Text = ResourceFormatter.DefaultValue;
            labelWarnIfEmpty.Text = ResourceFormatter.WarnIfEmpty;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(labelDefaultValue, textBoxDefaultValue);
            AddProperty(_labelName, _textBoxName);
            AddProperty(labelNumber, numericUpDownNumber);
            AddProperty(labelWarnIfEmpty, checkBoxWarnIfEmpty);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForPatchInlet ViewModel
        {
            get => (OperatorPropertiesViewModel_ForPatchInlet)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownNumber.Value = ViewModel.Number;
            textBoxDefaultValue.Text = ViewModel.DefaultValue;
            checkBoxWarnIfEmpty.Checked = ViewModel.WarnIfEmpty;

            if (comboBoxDimension.DataSource == null)
            {
                comboBoxDimension.ValueMember = nameof(IDAndName.ID);
                comboBoxDimension.DisplayMember = nameof(IDAndName.Name);
                comboBoxDimension.DataSource = ViewModel.DimensionLookup;
            }

            comboBoxDimension.SelectedValue = ViewModel.Dimension?.ID ?? 0;
        }

        protected override void ApplyControlsToViewModel()
        {
            base.ApplyControlsToViewModel();

            ViewModel.Number = (int)numericUpDownNumber.Value;
            ViewModel.DefaultValue = textBoxDefaultValue.Text;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
            ViewModel.WarnIfEmpty = checkBoxWarnIfEmpty.Checked;
        }
    }
}
