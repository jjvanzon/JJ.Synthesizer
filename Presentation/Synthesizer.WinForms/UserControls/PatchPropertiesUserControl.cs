using System;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchPropertiesUserControl : PropertiesUserControlBase
    {
        private bool _applyViewModelToControlsIsBusy = false;

        public event EventHandler<EventArgs<int>> AddToInstrumentRequested;
        public event EventHandler<EventArgs<int>> HasDimensionChanged;

        public PatchPropertiesUserControl() => InitializeComponent();

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelName, textBoxName);
            AddProperty(labelGroup, textBoxGroup);
            AddProperty(labelHidden, checkBoxHidden);
            AddProperty(labelHasDimension, checkBoxHasDimension);
            AddProperty(labelDefaultStandardDimension, comboBoxDefaultStandardDimension);
            AddProperty(labelDefaultCustomDimensionName, textBoxDefaultCustomDimensionName);
            AddProperty(buttonAddToInstrument, null);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Patch);
            labelName.Text = CommonResourceFormatter.Name;
            labelGroup.Text = ResourceFormatter.Group;
            labelHasDimension.Text = ResourceFormatter.HasDimension;
            labelDefaultStandardDimension.Text = ResourceFormatter.DefaultStandardDimension;
            labelDefaultCustomDimensionName.Text = ResourceFormatter.DefaultCustomDimension;
            labelHidden.Text = ResourceFormatter.Hidden;
            checkBoxHasDimension.Text = null;
            checkBoxHidden.Text = null;
            buttonAddToInstrument.Text = ResourceFormatter.AddToInstrument;
        }

        // Binding

        public new PatchPropertiesViewModel ViewModel
        {
            get => (PatchPropertiesViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override int GetID() => ViewModel.ID;

        protected override void ApplyViewModelToControls()
        {
            _applyViewModelToControlsIsBusy = true;
            try
            {
                textBoxName.Text = ViewModel.Name;
                textBoxGroup.Text = ViewModel.Group;
                checkBoxHasDimension.Checked = ViewModel.HasDimension;
                textBoxDefaultCustomDimensionName.Text = ViewModel.DefaultCustomDimensionName;
                textBoxDefaultCustomDimensionName.Enabled = ViewModel.DefaultCustomDimensionNameEnabled;
                labelDefaultCustomDimensionName.Enabled = ViewModel.DefaultCustomDimensionNameEnabled;
                checkBoxHidden.Checked = ViewModel.Hidden;

                if (comboBoxDefaultStandardDimension.DataSource == null)
                {
                    comboBoxDefaultStandardDimension.ValueMember = nameof(IDAndName.ID);
                    comboBoxDefaultStandardDimension.DisplayMember = nameof(IDAndName.Name);
                    comboBoxDefaultStandardDimension.DataSource = ViewModel.DefaultStandardDimensionLookup;
                }
                comboBoxDefaultStandardDimension.SelectedValue = ViewModel.DefaultStandardDimension?.ID ?? 0;
                comboBoxDefaultStandardDimension.Enabled = ViewModel.DefaultCustomDimensionNameEnabled;
                labelDefaultStandardDimension.Enabled = ViewModel.DefaultCustomDimensionNameEnabled;
            }
            finally
            {
                _applyViewModelToControlsIsBusy = false;
            }
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.Group = textBoxGroup.Text;
            ViewModel.HasDimension = checkBoxHasDimension.Checked;
            ViewModel.DefaultStandardDimension = (IDAndName)comboBoxDefaultStandardDimension.SelectedItem;
            ViewModel.DefaultCustomDimensionName = textBoxDefaultCustomDimensionName.Text;
            ViewModel.Hidden = checkBoxHidden.Checked;
        }

        // Events

        private void buttonAddToInstrument_Click(object sender, EventArgs e) => AddToInstrumentRequested.Invoke(this, new EventArgs<int>(ViewModel.ID));

        private void checkBoxHasDimension_CheckedChanged(object sender, EventArgs e)
        {
            if (_applyViewModelToControlsIsBusy) return;

            if (ViewModel == null) return;

            ApplyControlsToViewModel();

            HasDimensionChanged.Invoke(this, new EventArgs<int>(ViewModel.ID));
        }
    }
}
