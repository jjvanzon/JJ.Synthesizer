using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    public partial class PatchDetailsForm : Form
    {
        public event EventHandler CloseRequested;

        public PatchDetailsViewModel ViewModel
        {
            get { return (PatchDetailsViewModel) patchDetailsUserControl.ViewModel; }
            set { patchDetailsUserControl.ViewModel = value; }
        }

        public PatchDetailsForm()
        {
            InitializeComponent();
        }

        private void PatchDetailsForm_Load(object sender, EventArgs e)
        {
            SetTitles();
        }

        private void SetTitles()
        {
            Text = Titles.ApplicationName;
        }

        private void patchDetailsUserControl_CloseRequested(object sender, EventArgs e)
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
