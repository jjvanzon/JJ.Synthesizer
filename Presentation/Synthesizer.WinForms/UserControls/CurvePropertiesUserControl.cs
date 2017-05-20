using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurvePropertiesUserControl : PropertiesUserControlBase
    {
        public CurvePropertiesUserControl() => InitializeComponent();

        // Gui

        protected override void AddProperties()
        {
            AddProperty(labelName, textBoxName);
        }

        protected override void SetTitles()
        {
            TitleBarText = CommonResourceFormatter.Properties_WithName(ResourceFormatter.Curve);
            labelName.Text = CommonResourceFormatter.Name;
        }

        // Binding

        public new CurvePropertiesViewModel ViewModel
        {
            get => (CurvePropertiesViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override int GetID() => ViewModel.ID;

        protected override void ApplyViewModelToControls()
        {
            textBoxName.Text = ViewModel.Name;
        }

        protected override void ApplyControlsToViewModel()
        {
            ViewModel.Name = textBoxName.Text;
        }
    }
}
