using System;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class CurveGridUserControl : UserControlBase
    {
        private const string ID_COLUMN_NAME = "IDColumn";

        public event EventHandler CreateRequested;
        public event EventHandler<EventArgs<int>> DeleteRequested;
        public event EventHandler CloseRequested;
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
            NameColumn.HeaderText = CommonTitles.Name;
            UsedInColumn.HeaderText = Titles.UsedIn;
        }

        // Binding

        public new CurveGridViewModel ViewModel
        {
            get { return (CurveGridViewModel)base.ViewModel; }
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
            CreateRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Delete()
        {
            if (ViewModel == null) return;

            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                DeleteRequested?.Invoke(this, new EventArgs<int>(id.Value));
            }
        }

        private void Close()
        {
            if (ViewModel == null) return;
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void ShowDetails()
        {
            int? id = TryGetSelectedID();
            if (id.HasValue)
            {
                ShowDetailsRequested?.Invoke(this, new EventArgs<int>(id.Value));
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
