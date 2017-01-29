using System;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentGridUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler CreateRequested;
        public event EventHandler<EventArgs<int>> OpenRequested;
        public event EventHandler<EventArgs<int>> DeleteRequested;
        public event EventHandler CloseRequested;

        public DocumentGridUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Documents;
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            dataGridView.DataSource = ((DocumentGridViewModel)ViewModel).List;
        }

        // Actions

        private void Create()
        {
            CreateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Open()
        {
            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                OpenRequested?.Invoke(this, new EventArgs<int>(id.Value));
            }
        }

        private void Delete()
        {
            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new EventArgs<int>(id.Value));
            }
        }

        private void Close()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
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

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    Delete();
                    break;

                case Keys.Enter:
                    Open();
                    break;
            }
        }

        private void dataGridView_DoubleClick(object sender, EventArgs e)
        {
            Open();
        }

        // Helpers

        private int? TryGetSelectedID()
        {
            if (dataGridView.CurrentRow == null)
            {
                return null;
            }

            DataGridViewCell cell = dataGridView.CurrentRow.Cells[ID_COLUMN_NAME];
            int id = (int)cell.Value;
            return id;
        }
    }
}
