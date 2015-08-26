using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Presentation.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentDetailsUserControl : UserControl
    {
        public event EventHandler SaveRequested;
        public event EventHandler<Int32EventArgs> DeleteRequested;
        public event EventHandler CloseRequested;

        private DocumentDetailsViewModel _viewModel;

        public DocumentDetailsUserControl()
        {
            InitializeComponent();

            SetTitles();

            this.AutomaticallyAssignTabIndexes();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentDetailsViewModel ViewModel
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
            titleBarUserControl.Text = PropertyDisplayNames.Document;
            labelIDTitle.Text = CommonTitles.ID;
            labelName.Text = CommonTitles.Name;
            buttonSave.Text = CommonTitles.Save;
            buttonDelete.Text = CommonTitles.Delete;
        }

        private void ApplyViewModelToControls()
        {
            if (_viewModel == null) return;

            labelIDValue.Text = _viewModel.Document.ID.ToString();
            textBoxName.Text = _viewModel.Document.Name;

            labelIDTitle.Visible = _viewModel.IDVisible;
            labelIDValue.Visible = _viewModel.IDVisible;

            buttonDelete.Visible = _viewModel.CanDelete;
        }

        private void ApplyControlsToViewModel()
        {
            if (_viewModel == null) return;

            _viewModel.Document.Name = textBoxName.Text;
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
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void Delete()
        {
            if (DeleteRequested != null)
            {
                if (_viewModel == null) return;

                var e = new Int32EventArgs(_viewModel.Document.ID);
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
