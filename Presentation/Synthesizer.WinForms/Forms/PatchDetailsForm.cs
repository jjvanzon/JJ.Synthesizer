using System;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    public partial class PatchDetailsForm : Form
    {
        public event EventHandler CloseRequested;

        public PatchDetailsViewModel ViewModel
        {
            get { return patchDetailsUserControl.ViewModel; }
            set { patchDetailsUserControl.ViewModel = value; }
        }

        public PatchDetailsForm()
        {
            InitializeComponent();

            patchDetailsUserControl.CloseRequested += patchDetailsUserControl_CloseRequested;
        }

        private void PatchDetailsForm_Load(object sender, EventArgs e)
        {
            SetTitles();
        }

        private void SetTitles()
        {
            Text = ResourceFormatter.ApplicationName;
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs<int> e)
        {
            Close();
        }

        private void PatchDetailsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
