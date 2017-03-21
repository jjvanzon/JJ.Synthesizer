using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchGridUserControl : GridUserControlBase
    {
        public PatchGridUserControl()
        {
            InitializeComponent();

            IDPropertyName = PropertyNames.ID;
            Title = ResourceFormatter.Patches;
            ColumnTitlesVisible = true;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddColumn(PropertyNames.ID, null, visible: false);
            AddColumn(PropertyNames.Name, CommonResourceFormatter.Name, autoSize: true);
            AddColumn(PropertyNames.UsedIn, ResourceFormatter.UsedIn, widthInPixels: 180);
        }

        public new PatchGridViewModel ViewModel
        {
            get { return (PatchGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
