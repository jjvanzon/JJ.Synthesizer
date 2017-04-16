using System;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchPropertiesUserControl : PropertiesUserControlBase
    {
        public event EventHandler<EventArgs<int>> AddToInstrumentRequested;

        public PatchPropertiesUserControl() => InitializeComponent();

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelName, textBoxName);
            AddProperty(labelGroup, textBoxGroup);
            AddProperty(labelHidden, checkBoxHidden);
            AddProperty(null, buttonAddToInstrument);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Patch);
            labelName.Text = CommonResourceFormatter.Name;
            labelGroup.Text = ResourceFormatter.Group;
            labelHidden.Text = ResourceFormatter.Hidden;
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
            textBoxName.Text = ViewModel.Name;
            textBoxGroup.Text = ViewModel.Group;
            checkBoxHidden.Checked = ViewModel.Hidden;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.Group = textBoxGroup.Text;
            ViewModel.Hidden = checkBoxHidden.Checked;
        }

        // Events

        private void buttonAddToInstrument_Click(object sender, EventArgs e) => AddToInstrumentRequested?.Invoke(this, new EventArgs<int>(ViewModel.ID));
    }
}
