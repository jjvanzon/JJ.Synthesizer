using System;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Framework.Presentation.WinForms.Extensions;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentDetailsUserControl : UserControlBase
    {
        public event EventHandler SaveRequested;
        public event EventHandler<Int32EventArgs> DeleteRequested;
        public event EventHandler CloseRequested;

        public DocumentDetailsUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        // Gui

        private new DocumentDetailsViewModel ViewModel => (DocumentDetailsViewModel)base.ViewModel;

        private void SetTitles()
        {
            titleBarUserControl.Text = PropertyDisplayNames.Document;
            labelIDTitle.Text = CommonTitles.ID;
            labelName.Text = CommonTitles.Name;
            buttonSave.Text = CommonTitles.Save;
            buttonDelete.Text = CommonTitles.Delete;
        }

        protected override void ApplyViewModelToControls()
        {
            if (ViewModel == null) return;

            labelIDValue.Text = ViewModel.Document.ID.ToString();
            textBoxName.Text = ViewModel.Document.Name;

            labelIDTitle.Visible = ViewModel.IDVisible;
            labelIDValue.Visible = ViewModel.IDVisible;

            buttonDelete.Visible = ViewModel.CanDelete;
        }

        private void ApplyControlsToViewModel()
        {
            if (ViewModel == null) return;

            ViewModel.Document.Name = textBoxName.Text;
        }

        // Actions

        private void Save()
        {
            if (SaveRequested != null)
            {
                ApplyControlsToViewModel();
                SaveRequested(this, EventArgs.Empty);
            }
        }

        private void Close()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void Delete()
        {
            if (DeleteRequested != null)
            {
                if (ViewModel == null) return;

                var e = new Int32EventArgs(ViewModel.Document.ID);
                DeleteRequested(this, e);
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void DocumentDetailsUserControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                textBoxName.Focus();
                textBoxName.Select(0, 0);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonSave.PerformClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
