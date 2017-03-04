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
        public event EventHandler<EventArgs<int>> AddCurrentPatchRequested;

        public PatchPropertiesUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelName, textBoxName);
            AddProperty(labelGroup, textBoxGroup);
            AddProperty(null, buttonAddToCurentPatches);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonResourceFormatter.ObjectProperties(ResourceFormatter.Patch);
            labelName.Text = CommonResourceFormatter.Name;
            labelGroup.Text = ResourceFormatter.Group;
            buttonAddToCurentPatches.Text = ResourceFormatter.AddToCurrentPatches;
        }

        // Binding

        public new PatchPropertiesViewModel ViewModel
        {
            get { return (PatchPropertiesViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override int GetID()
        {
            return ViewModel.ID;
        }

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
            textBoxGroup.Text = ViewModel.Group;

            buttonAddToCurentPatches.Enabled = ViewModel.CanAddToCurrentPatches;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
            ViewModel.Group = textBoxGroup.Text;
        }

        // Events

        private void buttonAddToCurentPatches_Click(object sender, EventArgs e)
        {
            AddCurrentPatchRequested?.Invoke(this, new EventArgs<int>(ViewModel.ID));
        }
    }
}
