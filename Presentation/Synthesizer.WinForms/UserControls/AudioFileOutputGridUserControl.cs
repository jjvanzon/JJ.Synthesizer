using System;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class AudioFileOutputGridUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler CreateRequested;
        public event EventHandler<EventArgs<int>> DeleteRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<EventArgs<int>> ShowPropertiesRequested;

        public AudioFileOutputGridUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.AudioFileOutputList;
            NameColumn.HeaderText = CommonTitles.Name;
            AudioFileFormatColumn.HeaderText = PropertyDisplayNames.AudioFileFormat;
            SampleDataTypeColumn.HeaderText = PropertyDisplayNames.SampleDataType;
            SpeakerSetupColumn.HeaderText = PropertyDisplayNames.SpeakerSetup;
            SamplingRateColumn.HeaderText = PropertyDisplayNames.SamplingRate;
        }

        // Binding

        protected override void ApplyViewModelToControls()
        {
            specializedDataGridView.DataSource = ViewModel.List;
        }

        public new AudioFileOutputGridViewModel ViewModel
        {
            get { return (AudioFileOutputGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        // Events

        private void titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            CreateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            Delete();
        }

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void specializedDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    Delete();
                    break;

                case Keys.Enter:
                    ShowProperties();
                    break;
            }
        }

        private void specializedDataGridView_DoubleClick(object sender, EventArgs e)
        {
            ShowProperties();
        }

        // Actions

        private void Delete()
        {
            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new EventArgs<int>(id.Value));
            }
        }

        private void ShowProperties()
        {
            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                ShowPropertiesRequested?.Invoke(this, new EventArgs<int>(id.Value));
            }
        }

        // Helpers

        private int? TryGetSelectedID()
        {
            if (specializedDataGridView.CurrentRow != null)
            {
                DataGridViewCell cell = specializedDataGridView.CurrentRow.Cells[ID_COLUMN_NAME];
                int id = (int)cell.Value;
                return id;
            }

            return null;
        }
    }
}
