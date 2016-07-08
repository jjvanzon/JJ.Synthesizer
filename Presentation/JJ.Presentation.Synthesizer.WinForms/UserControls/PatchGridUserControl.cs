using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchGridUserControl : PatchGridUserControl_NotDesignable
    {
        private const string CHILD_DOCUMENT_ID_COLUMN_NAME = "ChildDocumentIDColumn";

        public event EventHandler<StringEventArgs> CreateRequested;
        public event EventHandler<Int32EventArgs> DeleteRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<Int32EventArgs> ShowDetailsRequested;

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

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            specializedDataGridView.DataSource = ViewModel.List;
        }

        // Actions

        private void Create()
        {
            if (CreateRequested != null)
            {
                var e = new StringEventArgs(ViewModel.Group);
                CreateRequested(this, e);
            }
        }

        private void Delete()
        {
            if (DeleteRequested != null)
            {
                int? id = TryGetSelectedChildDocumentID();
                if (id.HasValue)
                {
                    DeleteRequested(this, new Int32EventArgs(id.Value));
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

        private void ShowProperties()
        {
            if (ShowDetailsRequested != null)
            {
                int? id = TryGetSelectedChildDocumentID();
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

    /// <summary> 
    /// The WinForms designer does not work when deriving directly from a generic class.
    /// And also not when you make this class abstract.
    /// </summary>
    internal class PatchGridUserControl_NotDesignable : UserControlBase<PatchGridViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}
