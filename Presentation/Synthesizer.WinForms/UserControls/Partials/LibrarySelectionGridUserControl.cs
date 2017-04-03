using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal class LibrarySelectionGridUserControl : GridUserControlBase
    {
        public new int? TryGetSelectedID() => base.TryGetSelectedID();

        public LibrarySelectionGridUserControl()
        {
            Title = CommonResourceFormatter.Select_WithName(ResourceFormatter.LowerDocument);
            IDPropertyName = nameof(IDAndName.ID);
            ColumnTitlesVisible = true;
            AddButtonVisible = false;
            RemoveButtonVisible = false;
            CloseButtonVisible = false;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddAutoSizeColumn(nameof(IDAndName.Name), CommonResourceFormatter.Name);

            // NOTE: Add ID column last. If the ID column is the first column, WinForms will make the column visible,
            // when the DataGrid becomes invisible and then visible again, 
            // even when it is due to its parent becoming visible and then invisible again.
            // Thanks, WinForms.
            // Source of solution:
            // http://stackoverflow.com/questions/6359234/datagridview-id-column-will-not-hide 
            AddHiddenColumn(nameof(IDAndName.ID));
        }

        public new LibrarySelectionPopupViewModel ViewModel
        {
            get { return (LibrarySelectionPopupViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
