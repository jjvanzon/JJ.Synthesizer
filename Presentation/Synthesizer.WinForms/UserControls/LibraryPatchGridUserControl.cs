using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Properties;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class LibraryPatchGridUserControl : GridUserControlBase
    {
        private DataGridViewColumn _playColumn;

        public LibraryPatchGridUserControl()
        {
            InitializeComponent();

            IDPropertyName = nameof(IDAndName.ID);
            Title = ResourceFormatter.PatchesInLibrary;
            ColumnTitlesVisible = false;
            PlayButtonVisible = true;
            AddButtonVisible = false;
            RemoveButtonVisible = false;

            KeyDown += base_KeyDown;
            CellClick += base_CellClick;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddHiddenColumn(nameof(IDAndName.ID));
            _playColumn = AddImageColumn(Resources.PlayIcon);
            AddAutoSizeColumn(nameof(IDAndName.Name), CommonResourceFormatter.Name);
        }

        public new LibraryPatchGridViewModel ViewModel
        {
            get => (LibraryPatchGridViewModel)base.ViewModel;
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
