using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Configuration;
using JJ.Framework.Data;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Presentation.Svg.EventArg;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Svg;
using JJ.Presentation.Synthesizer.Svg.EventArg;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.WinForms.Configuration;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.Svg.Helpers;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    internal partial class MainForm : Form
    {
        private IContext _context;

        public MainForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();

            var config = CustomConfigurationManager.GetSection<ConfigurationSection>();
            Text = Titles.ApplicationName + config.General.TitleBarExtraText;

            ShowDocumentList();

            ShowAudioFileOutputList();
            ShowCurveList();
            ShowPatchList();
            ShowSampleList();
            ShowAudioFileOutputDetails();

            ShowPatchDetails();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            if (_context != null)
            {
                _context.Dispose();
            }

            base.Dispose(disposing);
        }

        // Actions

        private void ShowDocumentList()
        {
            documentListUserControl.Context = _context;
            documentListUserControl.Show();
        }

        private void CloseDocumentList()
        {
            documentListUserControl.Hide();
        }

        private void ShowDocumentDetails(DocumentDetailsViewModel documentDetailsViewModel)
        {
            documentDetailsUserControl1.Context = _context;
            documentDetailsUserControl1.Show(documentDetailsViewModel);
            documentDetailsUserControl1.BringToFront();
        }

        private void CloseDocumentDetails()
        {
            documentDetailsUserControl1.Hide();
            documentListUserControl.Show();
        }

        private void ShowAudioFileOutputList()
        {
            var form = new AudioFileOutputListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowCurveList()
        {
            var form = new CurveListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchList()
        {
            var form = new PatchListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowSampleList()
        {
            var form = new SampleListForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowAudioFileOutputDetails()
        {
            var form = new AudioFileOutputDetailsForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchDetails()
        {
            var form = new PatchDetailsForm();
            //form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        // Events

        private void menuUserControl_ShowDocumentListRequested(object sender, EventArgs e)
        {
            ShowDocumentList();
        }

        private void documentListUserControl_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentList();
        }

        private void documentListUserControl_DetailsViewRequested(object sender, DocumentDetailsViewEventArgs e)
        {
            ShowDocumentDetails(e.ViewModel);
        }

        private void documentDetailsUserControl1_CloseRequested(object sender, EventArgs e)
        {
            CloseDocumentDetails();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            // For debugging
        }
    }
}
