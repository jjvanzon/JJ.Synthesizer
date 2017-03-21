using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Items;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class LibraryGridUserControl : GridUserControlBase
    {
        public LibraryGridUserControl()
        {
            InitializeComponent();

            Title = ResourceFormatter.LowerDocuments;
            IDPropertyName = PropertyNames.ID;
            ColumnTitlesVisible = true;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddColumn(nameof(LibraryListItemViewModel.DocumentReferenceID), null, visible: false);
            AddColumn(nameof(LibraryListItemViewModel.ReferencedDocumentName), CommonResourceFormatter.Name, autoSize: true);
            AddColumn(nameof(LibraryListItemViewModel.Alias), ResourceFormatter.Alias, widthInPixels: 180);
        }

        public new LibraryGridViewModel ViewModel
        {
            get { return (LibraryGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
