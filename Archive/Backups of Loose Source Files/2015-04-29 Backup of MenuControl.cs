using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.Presenters;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal class MenuControl : MenuStrip
    {
        public event EventHandler ShowDocumentListRequested;

        public new void Show()
        {
            var presenter = new MenuPresenter();
            MenuViewModel viewModel = presenter.Show();

            ToolStripMenuItem viewToolStripMenuItem = CreateViewToolStripMenuItem(viewModel.ViewMenu);
            Items.Add(viewToolStripMenuItem);

            ToolStripMenuItem documentsToolStripMenuItem = CreateDocumentsToolStripMenuItem(viewModel.ViewMenu.DocumentMenuItem);
            documentsToolStripMenuItem.Click += documentsToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(documentsToolStripMenuItem);

            // Just to make it possible to use it the classic way too: to set Visible = true.
            base.Show();
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


        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ShowDocumentListRequested != null)
            {
                ShowDocumentListRequested(sender, EventArgs.Empty);
            }
        }
    }
}
