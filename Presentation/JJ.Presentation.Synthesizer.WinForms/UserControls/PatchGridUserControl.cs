using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchGridUserControl : UserControlBase
    {
        private const string CHILD_DOCUMENT_ID_COLUMN_NAME = "ChildDocumentIDColumn";

        public event EventHandler<EventArgs<string>> CreateRequested;
        public event EventHandler<GroupAndChildDocumentIDEventArgs> DeleteRequested;
        public event EventHandler<EventArgs<string>> CloseRequested;
        public event EventHandler<EventArgs<int>> ShowDetailsRequested;

        public PatchGridUserControl()
        {
            InitializeComponent();

            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Patches;
        }

        // Binding

        private new PatchGridViewModel ViewModel => (PatchGridViewModel)base.ViewModel;

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            specializedDataGridView.DataSource = ViewModel.List;
        }

        // Actions

        private void Create()
        {
            CreateRequested?.Invoke(this, new EventArgs<string>(ViewModel.Group));
        }

        private void Delete()
        {
            if (ViewModel == null) return;

            int? id = TryGetSelectedChildDocumentID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new GroupAndChildDocumentIDEventArgs(ViewModel.Group, id.Value));
            }
        }

        private void Close()
        {
            CloseRequested?.Invoke(this, new EventArgs<string>(ViewModel.Group));
        }

        private void ShowProperties()
        {
            int? id = TryGetSelectedChildDocumentID();
            if (id.HasValue)
            {
                var e = new EventArgs<int>(id.Value);
                ShowDetailsRequested?.Invoke(this, e);
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
                    ShowProperties();
                    break;
            }
        }

        private void specializedDataGridView_DoubleClick(object sender, EventArgs e)
        {
            ShowProperties();
        }

        // Helpers

        private int? TryGetSelectedChildDocumentID()
        {
            if (specializedDataGridView.CurrentRow != null)
            {
                DataGridViewCell cell = specializedDataGridView.CurrentRow.Cells[CHILD_DOCUMENT_ID_COLUMN_NAME];
                int childDocumentID = (int)cell.Value;
                return childDocumentID;
            }

            return null;
        }
    }
}
