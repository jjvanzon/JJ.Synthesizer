using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchGridUserControl : GridUserControlBase
    {
        public PatchGridUserControl()
        {
            InitializeComponent();

            IDPropertyName = nameof(PatchListItemViewModel.ID);
            Title = ResourceFormatter.Patches;
            ColumnTitlesVisible = true;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddHiddenColumn(nameof(PatchListItemViewModel.ID));
            AddAutoSizeColumn(nameof(PatchListItemViewModel.Name), CommonResourceFormatter.Name);
            AddColumnWithWidth(nameof(PatchListItemViewModel.UsedIn), ResourceFormatter.UsedIn, 180);
        }

        public new PatchGridViewModel ViewModel
        {
            get => (PatchGridViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }
    }
}
