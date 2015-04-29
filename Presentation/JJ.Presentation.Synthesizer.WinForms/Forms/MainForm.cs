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

namespace JJ.Presentation.Synthesizer.WinForms.Forms
{
    internal partial class MainForm : Form
    {
        private IContext _context;

        public MainForm()
        {
            InitializeComponent();

            _context = PersistenceHelper.CreateContext();

            BuildMenu();

            ShowAudioFileOutputList();
            ShowCurveList();
            ShowPatchList();
            ShowSampleList();

            ShowAudioFileOutputDetails();
            ShowPatchDetails();
        }

        private void BuildMenu()
        {
            var presenter = new MenuPresenter();
            MenuViewModel viewModel = presenter.Show();

            MenuStrip menuStrip = CreateMenuStrip();
            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);

            ToolStripMenuItem viewToolStripMenuItem = CreateViewToolStripMenuItem(viewModel.ViewMenu);
            menuStrip.Items.Add(viewToolStripMenuItem);

            ToolStripMenuItem documentsToolStripMenuItem = CreateDocumentsToolStripMenuItem(viewModel.ViewMenu.DocumentMenuItem);
            documentsToolStripMenuItem.Click += documentsToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(documentsToolStripMenuItem);
        }

        private MenuStrip CreateMenuStrip()
        {
            var menuStrip = new MenuStrip
            {
                Location = new Point(0, 0),
                Name = "menuStrip",
                Size = new Size(750, 24) // TODO: Do I need this? Does it not resize automatically?
            };

            return menuStrip;
        }

        private ToolStripMenuItem CreateViewToolStripMenuItem(ViewMenuViewModel viewModel)
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "viewToolStripMenuItem",
                Size = new Size(44, 20), // TODO: Do I need this? Does it not resize automatically?
                Text = "&" + Titles.View
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentsToolStripMenuItem(MenuItemViewModel viewModel)
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentsToolStripMenuItem",
                Size = new Size(152, 22), // TODO: Do I need this? Does it not resize automatically?
                Text = "&" + PropertyDisplayNames.Documents
            };

            return toolStripMenuItem;
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
            ShowDocumentList();
        }
    }
}
