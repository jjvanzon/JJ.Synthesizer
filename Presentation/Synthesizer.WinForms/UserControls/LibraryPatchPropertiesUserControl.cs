using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class LibraryPatchPropertiesUserControl : PropertiesUserControlBase
    {
        public event EventHandler<EventArgs<int>> AddCurrentPatchRequested;

        public LibraryPatchPropertiesUserControl() => InitializeComponent();

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelNameTitle, labelNameValue);
            AddProperty(labelGroupTitle, textBoxGroupValue);
            AddProperty(labelLibraryNameTitle, labelLibraryNameValue);
            AddProperty(null, buttonAddToCurrentPatches);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Patch);
            labelNameTitle.Text = CommonResourceFormatter.Name + ":";
            labelGroupTitle.Text = ResourceFormatter.Group + ":";
            labelLibraryNameTitle.Text = ResourceFormatter.LowerDocument + ":";
            buttonAddToCurrentPatches.Text = ResourceFormatter.AddToCurrentPatches;
        }

        // Binding

        public new LibraryPatchPropertiesViewModel ViewModel
        {
            get => (LibraryPatchPropertiesViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override int GetID() => ViewModel.PatchID;

        protected override void ApplyViewModelToControls()
        {
            labelNameValue.Text = ViewModel.Name;
            textBoxGroupValue.Text = ViewModel.Group;
            labelLibraryNameValue.Text = ViewModel.Library;
        }

        // Events

        private void buttonAddToCurrentPatches_Click(object sender, EventArgs e) => AddCurrentPatchRequested?.Invoke(this, new EventArgs<int>(ViewModel.PatchID));
    }
}
