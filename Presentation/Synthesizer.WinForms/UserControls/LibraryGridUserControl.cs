using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class LibraryGridUserControl : GridUserControlBase
    {
        public LibraryGridUserControl()
        {
            InitializeComponent();

            Title = ResourceFormatter.LowerDocuments;
            IDPropertyName = nameof(LibraryListItemViewModel.DocumentReferenceID);
            ColumnTitlesVisible = true;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddHiddenColumn(nameof(LibraryListItemViewModel.DocumentReferenceID));
            AddAutoSizeColumn(nameof(LibraryListItemViewModel.ReferencedDocumentName), CommonResourceFormatter.Name);
            AddColumnWithWidth(nameof(LibraryListItemViewModel.Alias), ResourceFormatter.Alias, 180);
        }

        public new LibraryGridViewModel ViewModel
        {
            get { return (LibraryGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
