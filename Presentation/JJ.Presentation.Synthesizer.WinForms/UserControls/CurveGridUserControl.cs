using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveGridUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler<EventArgs<int>> CreateRequested;
        public event EventHandler<DocumentAndChildEntityEventArgs> DeleteRequested;
        public event EventHandler<EventArgs<int>> CloseRequested;
        public event EventHandler<EventArgs<int>> ShowDetailsRequested;

        public CurveGridUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Curves;
        }

        // Binding

        private new CurveGridViewModel ViewModel => (CurveGridViewModel)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            specializedDataGridView.DataSource = ViewModel.List;
        }

        // Actions

        private void Create()
        {
            CreateRequested?.Invoke(this, new EventArgs<int>(ViewModel.DocumentID));
        }

        private void Delete()
        {
            if (ViewModel == null) return;

            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new DocumentAndChildEntityEventArgs(ViewModel.DocumentID, id.Value));
            }
        }

        private void Close()
        {
            if (ViewModel == null) return;
            CloseRequested?.Invoke(this, new EventArgs<int>(ViewModel.DocumentID));
        }

        private void ShowDetails()
        {
            if (ShowDetailsRequested != null)
            {
                int? id = TryGetSelectedID();
                if (id.HasValue)
                {
                    var e = new EventArgs<int>(id.Value);
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
