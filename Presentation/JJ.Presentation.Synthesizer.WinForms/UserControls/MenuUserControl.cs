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
using JJ.Framework.Presentation.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class MenuUserControl : UserControl
    {
        public event EventHandler ShowDocumentListRequested;
        public event EventHandler ShowDocumentTreeRequested;
        public event EventHandler AudioFileOutputEditRequested;
        public event EventHandler PatchDetailsRequested;

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

            ToolStripMenuItem viewToolStripMenuItem = CreateViewToolStripMenuItem();
            menuStrip.Items.Add(viewToolStripMenuItem);

            ToolStripMenuItem toolStripMenuItem;

            // Documents
            toolStripMenuItem = CreateDocumentsToolStripMenuItem();
            toolStripMenuItem.Click += documentsToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);

            // DocumentTree
            toolStripMenuItem = CreateDocumentTreeToolStripMenuItem();
            toolStripMenuItem.Click += documentTreeToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);

            // AudioFileOutputProperties
            toolStripMenuItem = CreateAudioFileOutputEditToolStripMenuItem();
            toolStripMenuItem.Click += audioFileOutputEditToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);

            // PatchDetails
            toolStripMenuItem = CreatePatchDetailsToolStripMenuItem();
            toolStripMenuItem.Click += patchDetailsToolStripMenuItem_Click;
            viewToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
        }

        private ToolStripMenuItem CreateViewToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "viewToolStripMenuItem",
                Text = "&" + Titles.ViewMenuItem
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentsToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentsToolStripMenuItem",
                Text = "&" + PropertyDisplayNames.Documents
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentTreeToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentTreeToolStripMenuItem",
                Text = "&" + Titles.DocumentTree
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateAudioFileOutputEditToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "audioFileOutputPropertiesToolStripMenuItem",
                Text = "&" + CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.AudioFileOutput)
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreatePatchDetailsToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "patchDetailsToolStripMenuItem",
                Text = "&" + CommonTitleFormatter.ObjectDetails(PropertyDisplayNames.Patch)
            };

            return toolStripMenuItem;
        }

        // Events

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

        private void audioFileOutputEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AudioFileOutputEditRequested != null)
            {
                AudioFileOutputEditRequested(sender, EventArgs.Empty);
            }
        }

        private void patchDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (PatchDetailsRequested != null)
            {
                PatchDetailsRequested(sender, EventArgs.Empty);
            }
        }
    }
}
