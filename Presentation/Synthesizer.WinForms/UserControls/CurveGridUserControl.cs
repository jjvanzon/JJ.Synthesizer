using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Framework.Presentation.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveGridUserControl : GridUserControlBase
    {
        public CurveGridUserControl()
        {
            InitializeComponent();
        }

        protected override string IDPropertyName => PropertyNames.ID;
        protected override string Title => ResourceFormatter.Curves;
        protected override bool ColumnHeadersVisible => true;
        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddColumn(PropertyNames.ID, CommonResourceFormatter.ID, visible: false);
            AddColumn(PropertyNames.Name, CommonResourceFormatter.Name, autoSize: true);
            AddColumn(PropertyNames.UsedIn, ResourceFormatter.UsedIn, widthInPixels: 180);
        }

        public new CurveGridViewModel ViewModel
        {
            get { return (CurveGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
