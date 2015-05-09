using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JJ.Framework.Presentation.WinForms;
using JJ.Framework.Data;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Presentation;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class MenuUserControl : UserControl
    {
        public event EventHandler ShowDocumentListRequested;
        public event EventHandler ShowDocumentTreeRequested;

        public MenuUserControl()
        {
            InitializeComponent();
        }

        // Overrides

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Show();
        }

        private bool _resizeBusy;

        /// <summary>
        /// Fix the height to the height of the menu.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            if (_resizeBusy) return;
            _resizeBusy = true;

            base.OnResize(e);

            Height = menuStrip.Height;

            _resizeBusy = false;
        }

        public void Show(MenuViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            ApplyViewModel(viewModel);

            base.Show();
        }

        private new void Show()
        {
            var presenter = new MenuPresenter();
            MenuViewModel viewModel = presenter.Show();

            ApplyViewModel(viewModel);

            base.Show();
        }

        private void ApplyViewModel(MenuViewModel viewModel)
        {
            menuStrip.Items.Clear();

            ToolStripMenuItem viewToolStripMenuItem = CreateViewToolStripMenuItem(viewModel.ViewMenu);
            menuStrip.Items.Add(viewToolStripMenuItem);

            ToolStripMenuItem documentsToolStripMenuItem = CreateDocumentsToolStripMenuItem(viewModel.ViewMenu.DocumentsMenuItem);
            documentsToolStripMenuItem.Click += documentsToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(documentsToolStripMenuItem);

            ToolStripMenuItem documentTreeToolStripMenuItem = CreateDocumentTreeToolStripMenuItem(viewModel.ViewMenu.DocumentTreeMenuItem);
            documentTreeToolStripMenuItem.Click += documentTreeToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(documentTreeToolStripMenuItem);
        }

        private ToolStripMenuItem CreateViewToolStripMenuItem(ViewMenuViewModel viewModel)
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "viewToolStripMenuItem",
                Size = new Size(44, 20), // TODO: Do I need this? Does it not resize automatically?
                Text = "&" + Titles.ViewMenuItem
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

        private ToolStripMenuItem CreateDocumentTreeToolStripMenuItem(MenuItemViewModel menuItemViewModel)
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentTreeToolStripMenuItem",
                //Size = new Size(152, 22), // TODO: Do I need this? Does it not resize automatically?
                Text = "&" + Titles.DocumentTree
            };

            return toolStripMenuItem;
        }

        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowDocumentListRequested != null)
            {
                ShowDocumentListRequested(sender, EventArgs.Empty);
            }
        }

        private void documentTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowDocumentTreeRequested != null)
            {
                ShowDocumentTreeRequested(sender, EventArgs.Empty);
            }
        }
    }
}
