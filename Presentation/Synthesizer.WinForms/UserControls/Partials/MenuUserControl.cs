using System;
using System.Windows.Forms;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.Resources;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
{
    internal partial class MenuUserControl : UserControl
    {
        public event EventHandler ShowDocumentGridRequested;
        public event EventHandler ShowDocumentTreeRequested;
        public event EventHandler DocumentCloseRequested;
        public event EventHandler DocumentSaveRequested;
        public event EventHandler ShowCurrentPatchesRequested;
        public event EventHandler ShowDocumentPropertiesRequested;

        public MenuUserControl()
        {
            InitializeComponent();
        }

        // Overrides

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

        private void ApplyViewModel(MenuViewModel viewModel)
        {
            menuStrip.Items.Clear();

            ToolStripMenuItem menuToolStripMenuItem = CreateMenuToolStripMenuItem();
            menuStrip.Items.Add(menuToolStripMenuItem);

            ToolStripMenuItem toolStripMenuItem;

            // DocumentTree
            if (viewModel.DocumentTree.Visible)
            {
                toolStripMenuItem = CreateDocumentTreeToolStripMenuItem();
                toolStripMenuItem.Click += documentTreeToolStripMenuItem_Click;
                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }

            // CurrentPatches
            if (viewModel.CurrentPatches.Visible)
            {
                toolStripMenuItem = CreateCurrentPatchesToolStripMenuItem();
                toolStripMenuItem.Click += currentPatchesToolStripMenuItem_Click;
                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }

            // DocumentSave
            if (viewModel.DocumentSave.Visible)
            {
                toolStripMenuItem = CreateDocumentSaveToolStripMenuItem();
                toolStripMenuItem.Click += documentSaveToolStripMenuItem_Click;
                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }

            // DocumentClose
            if (viewModel.DocumentClose.Visible)
            {
                toolStripMenuItem = CreateDocumentCloseToolStripMenuItem();
                toolStripMenuItem.Click += documentCloseToolStripMenuItem_Click;
                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }

            // DocumentList
            if (viewModel.DocumentList.Visible)
            {
                toolStripMenuItem = CreateDocumentListToolStripMenuItem();
                toolStripMenuItem.Click += documentListToolStripMenuItem_Click;
                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }

            // DocumentProperties
            // ReSharper disable once InvertIf
            if (viewModel.DocumentProperties.Visible)
            {
                toolStripMenuItem = CreateDocumentPropertiesToolStripMenuItem();
                toolStripMenuItem.Click += documentPropertiesToolStripMenuItem_Click;
                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
            }
        }

        private ToolStripMenuItem CreateMenuToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "menuToolStripMenuItem",
                // ReSharper disable once LocalizableElement
                Text = "&" + CommonTitles.Menu
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentTreeToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentTreeToolStripMenuItem",
                // ReSharper disable once LocalizableElement
                Text = "&" + Titles.DocumentTree
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateCurrentPatchesToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "currentPatchesToolStripMenuItem",
                // ReSharper disable once LocalizableElement
                Text = "&" + Titles.CurrentPatches
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentSaveToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentSaveToolStripMenuItem",
                // ReSharper disable once LocalizableElement
                Text = "&" + CommonTitleFormatter.SaveObject(PropertyDisplayNames.Document)
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentCloseToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentCloseToolStripMenuItem",
                // ReSharper disable once LocalizableElement
                Text = "&" + CommonTitleFormatter.CloseObject(PropertyDisplayNames.Document)
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentListToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentListToolStripMenuItem",
                // ReSharper disable once LocalizableElement
                Text = "&" + Titles.DocumentList
            };

            return toolStripMenuItem;
        }

        private ToolStripMenuItem CreateDocumentPropertiesToolStripMenuItem()
        {
            var toolStripMenuItem = new ToolStripMenuItem
            {
                Name = "documentPropertiesToolStripMenuItem",
                // ReSharper disable once LocalizableElement
                Text = "&" + CommonTitleFormatter.ObjectProperties(PropertyDisplayNames.Document)
            };

            return toolStripMenuItem;
        }

        // Events

        private void documentTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDocumentTreeRequested?.Invoke(sender, EventArgs.Empty);
        }

        private void currentPatchesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowCurrentPatchesRequested?.Invoke(sender, EventArgs.Empty);
        }

        private void documentSaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentSaveRequested?.Invoke(sender, EventArgs.Empty);
        }

        private void documentCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DocumentCloseRequested?.Invoke(sender, EventArgs.Empty);
        }

        private void documentListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDocumentGridRequested?.Invoke(sender, EventArgs.Empty);
        }

        private void documentPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDocumentPropertiesRequested?.Invoke(sender, EventArgs.Empty);
        }
    }
}
