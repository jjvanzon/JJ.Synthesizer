using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentGridUserControl : GridUserControlBase
    {
        public DocumentGridUserControl()
        {
            InitializeComponent();

            IDPropertyName = PropertyNames.ID;
            Title = ResourceFormatter.Documents;
            ColumnTitlesVisible = false;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddColumn(PropertyNames.ID, null, visible: false);
            AddColumn(PropertyNames.Name, CommonResourceFormatter.Name, autoSize: true);
        }

        public new DocumentGridViewModel ViewModel
        {
            get { return (DocumentGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
