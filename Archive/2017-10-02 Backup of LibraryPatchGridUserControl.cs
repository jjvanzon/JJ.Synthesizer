//using System.Windows.Forms;
//using JJ.Data.Canonical;
//using JJ.Framework.Presentation.Resources;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Presentation.Synthesizer.WinForms.Properties;
//using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

//namespace JJ.Presentation.Synthesizer.WinForms.UserControls
//{
//    internal partial class LibraryPatchGridUserControl : GridUserControlBase
//    {
//        private DataGridViewColumn _playColumn;
//        private DataGridViewColumn _openExternallyColumn;

//        public LibraryPatchGridUserControl()
//        {
//            InitializeComponent();

//            IDPropertyName = nameof(IDAndName.ID);
//            ColumnTitlesVisible = false;
//            PlayButtonVisible = true;
//            AddButtonVisible = false;
//            RemoveButtonVisible = false;
//            OpenItemExternallyButtonVisible = true;

//            KeyDown += base_KeyDown;
//            CellClick += base_CellClick;
//        }

//        protected override object GetDataSource() => ViewModel?.List;

//        protected override void ApplyViewModelToControls()
//        {
//            Title = ViewModel?.Title;

//            base.ApplyViewModelToControls();
//        }

//        protected override void AddColumns()
//        {
//            AddHiddenColumn(nameof(IDAndName.ID));
//            _playColumn = AddImageColumn(Resources.PlayIconThinner);
//            AddAutoSizeColumn(nameof(IDAndName.Name), CommonResourceFormatter.Name);
//            _openExternallyColumn = AddImageColumn(Resources.OpenWindowIconThinner);
//        }

//        public new LibraryPatchGridViewModel ViewModel
//        {
//            get => (LibraryPatchGridViewModel)base.ViewModel;
//            set => base.ViewModel = value;
//        }

//        private void base_KeyDown(object sender, KeyEventArgs e)
//        {
//            int? columnIndex = TryGetColumnIndex();
//            if (!columnIndex.HasValue)
//            {
//                return;
//            }

//            switch (e.KeyCode)
//            {
//                case Keys.Space when columnIndex.Value == _playColumn.Index:
//                    Play();
//                    e.Handled = true;
//                    break;

//                case Keys.Space when columnIndex.Value == _openExternallyColumn.Index:
//                    OpenItemExternally();
//                    e.Handled = true;
//                    break;
//            }
//        }

//        private void base_CellClick(object sender, DataGridViewCellEventArgs e)
//        {
//            if (ViewModel == null) return;

//            if (e.RowIndex == -1)
//            {
//                return;
//            }

//            if (e.ColumnIndex == _playColumn.Index)
//            {
//                Play();
//                return;
//            }

//            // ReSharper disable once InvertIf
//            if (e.ColumnIndex == _openExternallyColumn.Index)
//            {
//                OpenItemExternally();
//                // ReSharper disable once RedundantJumpStatement
//                return;
//            }
//        }
//    }
//}
