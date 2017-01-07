using System;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ToneGridEditUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";
        private const int PLAY_COLUMN_INDEX = 1;

        public event EventHandler<EventArgs<int>> CreateToneRequested;
        public event EventHandler<ScaleAndToneEventArgs> DeleteToneRequested;
        public event EventHandler<ScaleAndToneEventArgs> PlayToneRequested;
        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> LoseFocusRequested;
        public event EventHandler<EventArgs<int>> Edited;

        public ToneGridEditUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Tones;
            PlayColumn.HeaderText = Titles.Play;
            PlayColumn.Text = Titles.Play;
            PlayColumn.UseColumnTextForButtonValue = true;
            OctaveColumn.HeaderText = PropertyDisplayNames.Octave;
            FrequencyColumn.HeaderText = PropertyDisplayNames.Frequency;
        }

        // Binding

        public new ToneGridEditViewModel ViewModel
        {
            get { return (ToneGridEditViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            NumberColumn.HeaderText = ViewModel.NumberTitle;
            FrequencyColumn.Visible = ViewModel.FrequencyVisible;

            specializedDataGridView.DataSource = ViewModel.Tones;
        }

        // Actions

        private void CreateTone()
        {
            CreateToneRequested?.Invoke(this, new EventArgs<int>(ViewModel.ScaleID));
        }

        private void DeleteTone()
        {
            if (ViewModel == null) return;

            int? toneID = TryGetSelectedID();
            if (toneID.HasValue)
            {
                DeleteToneRequested?.Invoke(this, new ScaleAndToneEventArgs(ViewModel.ScaleID, toneID.Value));
            }
        }

        private void Close()
        {
            if (ViewModel == null) return;

            specializedDataGridView.EndEdit();

            CloseRequested?.Invoke(this, new EventArgs<int>(ViewModel.ScaleID));
        }

        private void LoseFocus()
        {
            if (ViewModel == null) return;

            specializedDataGridView.EndEdit();

            LoseFocusRequested?.Invoke(this, new EventArgs<int>(ViewModel.ScaleID));
        }

        // Events

        private void titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            CreateTone();
        }

        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            DeleteTone();
        }

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void specializedDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            // Without an Invoke, WinForms will complain with
            // 'Operation is not valid because it results in a reentrant call to the SetCurrentCellAddressCore function.'
            // when we try to reassign the data source.

            BeginInvoke(new Action(() => Edited?.Invoke(this, new EventArgs<int>(ViewModel.ScaleID))));
        }

        private void specializedDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (ViewModel == null) return;

            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex != PLAY_COLUMN_INDEX)
            {
                return;
            }

            int toneID = (int)specializedDataGridView.Rows[e.RowIndex].Cells[ID_COLUMN_NAME].Value;
            PlayToneRequested?.Invoke(this, new ScaleAndToneEventArgs(ViewModel.ScaleID, toneID));
        }

        private void specializedDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteTone();
                    break;

                case Keys.Insert:
                    CreateTone();
                    break;
            }
        }

        // This event does not go off, if not clicked on a control that according to WinForms can get focus.
        private void ToneGridEditUserControl_Leave(object sender, EventArgs e)
        {
            // This Visible check is there because the leave event (lose focus) goes off after I closed, 
            // making it want to save again, even though view model is empty
            // which makes it say that now clear fields are required.
            if (Visible)
            {
                LoseFocus();
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
