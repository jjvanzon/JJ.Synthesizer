using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.Properties;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class SampleGridUserControl : GridUserControlBase
    {
        private DataGridViewColumn _playColumn;

        public SampleGridUserControl()
        {
            InitializeComponent();

            IDPropertyName = nameof(SampleListItemViewModel.ID);
            Title = ResourceFormatter.Samples;
            ColumnTitlesVisible = true;

            KeyDown += base_KeyDown;
            CellClick += base_CellClick;
        }

        protected override object GetDataSource() => ViewModel?.List;

        protected override void AddColumns()
        {
            AddHiddenColumn(nameof(SampleListItemViewModel.ID));
            _playColumn = AddImageColumn(Resources.PlayIcon);
            AddAutoSizeColumn(nameof(SampleListItemViewModel.Name), CommonResourceFormatter.Name);
            AddColumn(nameof(SampleListItemViewModel.SampleDataType), ResourceFormatter.SampleDataType);
            AddColumn(nameof(SampleListItemViewModel.SpeakerSetup), ResourceFormatter.SpeakerSetup);
            AddColumn(nameof(SampleListItemViewModel.SamplingRate), ResourceFormatter.SamplingRate);
            AddColumn(nameof(SampleListItemViewModel.IsActiveText), ResourceFormatter.IsActive);
            AddColumnWithWidth(nameof(SampleListItemViewModel.UsedIn), ResourceFormatter.UsedIn, 180);
        }

        public new SampleGridViewModel ViewModel
        {
            get => (SampleGridViewModel)base.ViewModel;
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
