using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentPropertiesUserControl : PropertiesUserControlBase
    {
        public DocumentPropertiesUserControl()
        {
            InitializeComponent();
        }

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Document);
            labelName.Text = CommonTitles.Name;
        }

        // Binding

        public new DocumentPropertiesViewModel ViewModel
        {
            get { return (DocumentPropertiesViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override int GetID()
        {
            return ViewModel.Entity.ID;
        }

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Entity.Name;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Entity.Name = textBoxName.Text;
        }
    }
}
