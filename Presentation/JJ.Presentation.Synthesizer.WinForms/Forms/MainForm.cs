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

namespace JJ.Presentation.Synthesizer.WinForms
{
    public partial class MainForm : Form
    {
        private IContext _context;

        public MainForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();

            ShowPatchList();
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

        private void ShowPatchList()
        {
            var patchListForm = new PatchListForm();
            patchListForm.MdiParent = this;
            patchListForm.Context = _context;
            patchListForm.Show();
        }

        private void ShowPatchDetails()
        {
            var patchDetailsForm = new PatchDetailsForm();
            patchDetailsForm.MdiParent = this;
            patchDetailsForm.Context = _context;
            patchDetailsForm.Show();
        }
    }
}
