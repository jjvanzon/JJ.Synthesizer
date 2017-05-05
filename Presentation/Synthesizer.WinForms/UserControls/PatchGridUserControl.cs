using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.Properties;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchGridUserControl : GridUserControlBase
    {
        private DataGridViewColumn _playColumn;

        public PatchGridUserControl()
        {
            InitializeComponent();

            IDPropertyName = nameof(PatchListItemViewModel.ID);
            Title = ResourceFormatter.Patches;
            ColumnTitlesVisible = true;
            PlayButtonVisible = true;

            KeyDown += base_KeyDown;
            CellClick += base_CellClick;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddHiddenColumn(nameof(PatchListItemViewModel.ID));
            _playColumn = AddImageColumn(Resources.PlayIconThinner);
            AddAutoSizeColumn(nameof(PatchListItemViewModel.Name), CommonResourceFormatter.Name);
            AddColumnWithWidth(nameof(PatchListItemViewModel.UsedIn), ResourceFormatter.UsedIn, 180);
        }

        public new PatchGridViewModel ViewModel
        {
            get => (PatchGridViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        private void base_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    Play();
                    e.Handled = true;
                    break;
            }
        }

        private void base_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ViewModel == null) return;

            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex == _playColumn.Index)
            {
                Play();
            }
        }
    }
}
