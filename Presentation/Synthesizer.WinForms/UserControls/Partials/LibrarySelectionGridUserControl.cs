using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal class LibrarySelectionGridUserControl : GridUserControlBase
    {
        public new int? TryGetSelectedID() => base.TryGetSelectedID();

        public LibrarySelectionGridUserControl()
        {
            Title = CommonResourceFormatter.Select_WithName(ResourceFormatter.LowerDocument);
            IDPropertyName = PropertyNames.ID;
            ColumnTitlesVisible = true;
            AddButtonVisible = false;
            RemoveButtonVisible = false;
            CloseButtonVisible = false;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddHiddenColumn(nameof(IDAndName.ID));
            AddAutoSizeColumn(nameof(IDAndName.Name), CommonResourceFormatter.Name);
        }

        public new LibrarySelectionPopupViewModel ViewModel
        {
            get { return (LibrarySelectionPopupViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
