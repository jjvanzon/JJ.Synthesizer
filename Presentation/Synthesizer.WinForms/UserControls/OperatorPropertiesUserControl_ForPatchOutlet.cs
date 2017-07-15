using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class OperatorPropertiesUserControl_ForPatchOutlet 
        : OperatorPropertiesUserControlBase
    {
        public OperatorPropertiesUserControl_ForPatchOutlet() => InitializeComponent();

        // Gui

        protected override void SetTitles()
        {
            base.SetTitles();

            labelPosition.Text = ResourceFormatter.Position;
            labelDimension.Text = ResourceFormatter.Dimension;
            labelNameOrDimensionHidden.Text = ResourceFormatter.NameOrDimensionHidden;
            labelIsRepeating.Text = ResourceFormatter.IsRepeating;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelOperatorTypeTitle, _labelOperatorTypeValue);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(_labelName, _textBoxName);
            AddProperty(labelPosition, numericUpDownPosition);
            AddProperty(labelNameOrDimensionHidden, checkBoxNameOrDimensionHidden);
            AddProperty(labelIsRepeating, checkBoxIsRepeating);
            AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForPatchOutlet ViewModel
        {
            get => (OperatorPropertiesViewModel_ForPatchOutlet)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownPosition.Value = ViewModel.Position;
            checkBoxNameOrDimensionHidden.Checked = ViewModel.NameOrDimensionHidden;
            checkBoxIsRepeating.Checked = ViewModel.IsRepeating;

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
            
            ViewModel.Position = (int)numericUpDownPosition.Value;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
            ViewModel.NameOrDimensionHidden = checkBoxNameOrDimensionHidden.Checked;
            ViewModel.IsRepeating = checkBoxIsRepeating.Checked;
        }
    }
}
