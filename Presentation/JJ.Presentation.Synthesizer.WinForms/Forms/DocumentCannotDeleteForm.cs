using JJ.Presentation.Synthesizer.ViewModels;
using System;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    internal partial class DocumentCannotDeleteForm : Form
    {
        public event EventHandler OKClicked;

        public DocumentCannotDeleteForm()
        {
            InitializeComponent();

            Text = Titles.ApplicationName;
        }

        public void ShowDialog(DocumentCannotDeleteViewModel viewModel)
        {
            documentCannotDeleteUserControl.ViewModel = viewModel;
            documentCannotDeleteUserControl.Show();

            base.ShowDialog();
        }

        private void documentCannotDeleteUserControl_CloseRequested(object sender, EventArgs e)
        {
            Close();

            if (OKClicked != null)
            {
                OKClicked(this, EventArgs.Empty);
            }
        }

        private void DocumentCannotDeleteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }
    }
}
