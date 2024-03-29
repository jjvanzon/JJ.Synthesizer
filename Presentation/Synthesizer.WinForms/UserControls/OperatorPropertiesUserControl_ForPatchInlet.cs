﻿using JJ.Business.Synthesizer.StringResources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

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

            labelPosition.Text = ResourceFormatter.Position;
            labelDimension.Text = ResourceFormatter.Dimension;
            labelDefaultValue.Text = ResourceFormatter.DefaultValue;
            labelWarnIfEmpty.Text = ResourceFormatter.WarnIfEmpty;
            labelNameOrDimensionHidden.Text = ResourceFormatter.NameOrDimensionHidden;
            labelIsRepeating.Text = ResourceFormatter.IsRepeating;
        }

        protected override void AddProperties()
        {
            AddProperty(_labelUnderlyingPatch, _comboBoxUnderlyingPatch);
            AddProperty(labelDimension, comboBoxDimension);
            AddProperty(_labelName, _textBoxName);
            AddProperty(labelPosition, numericUpDownPosition);
            AddProperty(labelDefaultValue, textBoxDefaultValue);
            AddProperty(labelWarnIfEmpty, checkBoxWarnIfEmpty);
            AddProperty(labelNameOrDimensionHidden, checkBoxNameOrDimensionHidden);
            AddProperty(labelIsRepeating, checkBoxIsRepeating);
        }

        // Binding

        public new OperatorPropertiesViewModel_ForPatchInlet ViewModel
        {
            // ReSharper disable once MemberCanBePrivate.Global
            get => (OperatorPropertiesViewModel_ForPatchInlet)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            base.ApplyViewModelToControls();

            numericUpDownPosition.Value = ViewModel.Position;
            textBoxDefaultValue.Text = ViewModel.DefaultValue;
            checkBoxWarnIfEmpty.Checked = ViewModel.WarnIfEmpty;
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
            ViewModel.DefaultValue = textBoxDefaultValue.Text;
            ViewModel.Dimension = (IDAndName)comboBoxDimension.SelectedItem;
            ViewModel.WarnIfEmpty = checkBoxWarnIfEmpty.Checked;
            ViewModel.NameOrDimensionHidden = checkBoxNameOrDimensionHidden.Checked;
            ViewModel.IsRepeating = checkBoxIsRepeating.Checked;
        }
    }
}
