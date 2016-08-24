using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Resources;
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
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Patch);
            labelName.Text = CommonTitles.Name;
            labelGroup.Text = Titles.Group;
            buttonAddToCurentPatches.Text = Titles.AddToCurrentPatches;
        }

        // Binding

        private new PatchPropertiesViewModel ViewModel => (PatchPropertiesViewModel)base.ViewModel;

        protected override int GetKey()
        {
            return ViewModel.ChildDocumentID;
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
            AddCurrentPatchRequested?.Invoke(this, new EventArgs<int>(ViewModel.ChildDocumentID));
        }
    }
}
