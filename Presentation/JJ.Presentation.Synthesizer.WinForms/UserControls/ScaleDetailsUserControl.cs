using System;
using System.Linq;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class ScaleDetailsUserControl : UserControl
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler<Int32EventArgs> CreateToneRequested;
        public event EventHandler<Int32EventArgs> DeleteToneRequested;
        public event EventHandler CloseRequested;
        public event EventHandler LoseFocusRequested;

        private ScaleDetailsViewModel _viewModel;

        public ScaleDetailsUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ScaleDetailsViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModelToControls();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Scale;
            OctaveColumn.HeaderText = PropertyDisplayNames.Octave;
        }

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            NumberColumn.HeaderText = _viewModel.NumberTitle;

            specializedDataGridView.DataSource = _viewModel.Tones;
        }

        // TODO: Remove method if it proves it is not necessary.
        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            //throw new NotImplementedException();
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
            if (CloseRequested != null)
            {
                // TODO: Consider if the following code line is necessary at all
                ApplyControlsToViewModel();
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void LoseFocus()
        {
            if (LoseFocusRequested != null)
            {
                // TODO: Consider if the following code line is necessary at all
                ApplyControlsToViewModel();
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
        private void ScaleDetailsUserControl_Leave(object sender, EventArgs e)
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
