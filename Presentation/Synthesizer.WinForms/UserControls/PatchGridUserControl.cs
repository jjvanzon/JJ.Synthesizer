using System;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class PatchGridUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler<EventArgs<string>> CreateRequested;
        public event EventHandler<GroupAndPatchIDEventArgs> DeleteRequested;
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
            NameColumn.HeaderText = CommonTitles.Name;
            UsedInColumn.HeaderText = Titles.UsedIn;
        }

        // Binding

        public new PatchGridViewModel ViewModel
        {
            get { return (PatchGridViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

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

            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new GroupAndPatchIDEventArgs(ViewModel.Group, id.Value));
            }
        }

        private void Close()
        {
            CloseRequested?.Invoke(this, new EventArgs<string>(ViewModel.Group));
        }

        private void ShowProperties()
        {
            int? id = TryGetSelectedID();
            if (!id.HasValue)
            {
                return;
            }

            var e = new EventArgs<int>(id.Value);
            ShowDetailsRequested?.Invoke(this, e);
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

        private int? TryGetSelectedID()
        {
            if (specializedDataGridView.CurrentRow == null)
            {
                return null;
            }

            DataGridViewCell cell = specializedDataGridView.CurrentRow.Cells[ID_COLUMN_NAME];
            int id = (int)cell.Value;
            return id;
        }
    }
}
