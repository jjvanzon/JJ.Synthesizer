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

        //// WinForms designer will not show base event handlers,
        //// so define new ones here.
        //public new event EventHandler<EventArgs<int>> ShowItemRequested
        //{
        //    add { base.ShowItemRequested += value; }
        //    remove { base.ShowItemRequested -= value; }
        //}

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
