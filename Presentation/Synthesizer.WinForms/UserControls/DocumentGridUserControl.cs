using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
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
            AddHiddenColumn(nameof(IDAndName.ID));
            AddAutoSizeColumn(nameof(IDAndName.Name), CommonResourceFormatter.Name);
        }

        public new DocumentGridViewModel ViewModel
        {
            get { return (DocumentGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
