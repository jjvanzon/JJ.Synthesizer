using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ToneGridEditUserControl : ToneGridEditUserControl_NotDesignable
    {
        private const string ID_COLUMN_NAME = "IDColumn";
        private const int PLAY_COLUMN_INDEX = 3;

        public event EventHandler<Int32EventArgs> CreateToneRequested;
        public event EventHandler<Int32EventArgs> DeleteToneRequested;
        public event EventHandler<Int32EventArgs> PlayToneRequested;
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        public ToneGridEditUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Tones;
            OctaveColumn.HeaderText = PropertyDisplayNames.Octave;
            PlayColumn.HeaderText = Titles.Play;
            PlayColumn.Text = Titles.Play;
            PlayColumn.UseColumnTextForButtonValue = true;
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            NumberColumn.HeaderText = ViewModel.NumberTitle;

            specializedDataGridView.DataSource = ViewModel.Tones;
        }

        // Actions

        private void CreateTone()
        {
            if (CreateToneRequested != null)
            {
                var e = new Int32EventArgs(ViewModel.ScaleID);
                CreateToneRequested(this, e);
            }
        }

        private void DeleteTone()
        {
            if (DeleteToneRequested != null)
            {
                int? id = TryGetSelectedID();
                if (id.HasValue)
                {
                    var e = new Int32EventArgs(id.Value);
                    DeleteToneRequested(this, e);
                }
            }
        }

        private void Close()
        {
            specializedDataGridView.EndEdit();

            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void LoseFocus()
        {
            specializedDataGridView.EndEdit();

            if (LoseFocusRequested != null)
            {
                LoseFocusRequested(this, EventArgs.Empty);
            }
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

        private void specializedDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (PlayToneRequested == null)
            {
                return;
            }

            if (e.RowIndex == -1)
            {
                return;
            }

            if (e.ColumnIndex != PLAY_COLUMN_INDEX)
            {
                return;
            }

            int toneID = (int)specializedDataGridView.Rows[e.RowIndex].Cells[ID_COLUMN_NAME].Value;
            var e2 = new Int32EventArgs(toneID);
            PlayToneRequested(this, e2);
        }

        private void specializedDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    DeleteTone();
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

    /// <summary> The WinForms designer does not work when deriving directly from a generic class. </summary>
    internal class ToneGridEditUserControl_NotDesignable : UserControlBase<ToneGridEditViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
