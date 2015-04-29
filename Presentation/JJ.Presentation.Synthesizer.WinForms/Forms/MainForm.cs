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

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    public partial class MainForm : Form
    {
        private IContext _context;

        public MainForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();

            BuildMenu();

            ShowAudioFileOutputList();
            ShowCurveList();
            ShowDocumentList();
            ShowPatchList();
            ShowSampleList();

            ShowAudioFileOutputDetails();
            ShowPatchDetails();
        }

        private MenuStrip menuStrip1;
        private ToolStripMenuItem viewToolStripMenuItem;
        private ToolStripMenuItem documentsToolStripMenuItem;

        private void BuildMenu()
        {
            // TODO: Derive this dynamically from the view model that comes out of the MenuPresenter.
            // Perhaps put the whole code in the presentation framework,
            // as well as the MenuViewModel and MenuItemViewModel.

            menuStrip1 = new System.Windows.Forms.MenuStrip();

            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {viewToolStripMenuItem});
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(750, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            documentsToolStripMenuItem});
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // documentsToolStripMenuItem
            // 
            documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
            documentsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            documentsToolStripMenuItem.Text = "&Documents";
            documentsToolStripMenuItem.Click += new System.EventHandler(documentsToolStripMenuItem_Click);

            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
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

        private void ShowAudioFileOutputList()
        {
            var form = new AudioFileOutputListForm();
            form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowCurveList()
        {
            var form = new CurveListForm();
            form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowDocumentList()
        {
            var form = new DocumentListForm();
            form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchList()
        {
            var form = new PatchListForm();
            form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowSampleList()
        {
            var form = new SampleListForm();
            form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowAudioFileOutputDetails()
        {
            var form = new AudioFileOutputDetailsForm();
            form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void ShowPatchDetails()
        {
            var form = new PatchDetailsForm();
            form.MdiParent = this;
            form.Context = _context;
            form.Show();
        }

        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
