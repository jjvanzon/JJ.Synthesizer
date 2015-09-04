using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchGridUserControl : UserControl
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler<Int32EventArgs> CreateRequested;
        public event EventHandler<Int32EventArgs> DeleteRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<Int32EventArgs> ShowDetailsRequested;

        private PatchGridViewModel _viewModel;

        public PatchGridUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public PatchGridViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Patches;
        }
        
        private void ApplyViewModel()
        {
            if (_viewModel == null)
            {
                return;
            }

            specializedDataGridView.DataSource = _viewModel.List.Select(x => new { x.Name, x.ID }).ToArray();
        }

        // Actions

        private void Create()
        {
            if (CreateRequested != null)
            {
                var e = new Int32EventArgs(ViewModel.DocumentID);
                CreateRequested(this, e);
            }
        }

        private void Delete()
        {
            if (DeleteRequested != null)
            {
                int? id = TryGetSelectedID();
                if (id.HasValue)
                {
                    var e = new Int32EventArgs(id.Value);
                    DeleteRequested(this, e);
                }
            }
        }

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void ShowDetails()
        {
            if (ShowDetailsRequested != null)
            {
                int? id = TryGetSelectedID();
                if (id.HasValue)
                {
                    var e = new Int32EventArgs(id.Value);
                    ShowDetailsRequested(this, e);
                }
            }
        }

        // Events

        private void titleBarUserControl_AddClicked(object sender, EventArgs e)
        {
            Create();
        }

        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e)
        {
            Delete();
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
                    Delete();
                    break;

                case Keys.Enter:
                    ShowDetails();
                    break;
            }
        }

        private void specializedDataGridView_DoubleClick(object sender, EventArgs e)
        {
            ShowDetails();
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
