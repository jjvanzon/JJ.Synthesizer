//using System;
//using System.Windows.Forms;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Presentation.Synthesizer.Resources;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Presentation.Resources;

//namespace JJ.Presentation.Synthesizer.WinForms.UserControls.Partials
//{
//    internal partial class MenuUserControl : UserControlBase<MenuViewModel>
//    {
//        public event EventHandler ShowDocumentGridRequested;
//        public event EventHandler ShowDocumentTreeRequested;
//        public event EventHandler DocumentCloseRequested;
//        public event EventHandler DocumentSaveRequested;
//        public event EventHandler ShowCurrentPatchesRequested;

//        public MenuUserControl()
//        {
//            InitializeComponent();
//        }

//        // Overrides

//        private bool _resizeBusy;

//        /// <summary>
//        /// Fix the height to the height of the menu.
//        /// </summary>
//        protected override void OnResize(EventArgs e)
//        {
//            if (_resizeBusy) return;
//            _resizeBusy = true;

//            base.OnResize(e);

//            Height = menuStrip.Height;

//            _resizeBusy = false;
//        }

//        protected override void ApplyViewModelToControls()
//        {
//            if (ViewModel == null) throw new NullException(() => ViewModel);

//            menuStrip.Items.Clear();

//            ToolStripMenuItem menuToolStripMenuItem = CreateMenuToolStripMenuItem();
//            menuStrip.Items.Add(menuToolStripMenuItem);

//            ToolStripMenuItem toolStripMenuItem;

//            // DocumentTree
//            if (ViewModel.DocumentTreeMenuItem.Visible)
//            {
//                toolStripMenuItem = CreateDocumentTreeToolStripMenuItem();
//                toolStripMenuItem.Click += documentTreeToolStripMenuItem_Click;
//                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
//            }

//            // CurrentPatches
//            if (ViewModel.CurrentPatches.Visible)
//            {
//                toolStripMenuItem = CreateCurrentPatchesToolStripMenuItem();
//                toolStripMenuItem.Click += currentPatchesToolStripMenuItem_Click;
//                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
//            }

//            // DocumentSave
//            if (ViewModel.DocumentSaveMenuItem.Visible)
//            {
//                toolStripMenuItem = CreateDocumentSaveToolStripMenuItem();
//                toolStripMenuItem.Click += documentSaveToolStripMenuItem_Click;
//                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
//            }

//            // DocumentClose
//            if (ViewModel.DocumentCloseMenuItem.Visible)
//            {
//                toolStripMenuItem = CreateDocumentCloseToolStripMenuItem();
//                toolStripMenuItem.Click += documentCloseToolStripMenuItem_Click;
//                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
//            }

//            // Documents
//            if (ViewModel.DocumentsMenuItem.Visible)
//            {
//                toolStripMenuItem = CreateDocumentsToolStripMenuItem();
//                toolStripMenuItem.Click += documentsToolStripMenuItem_Click;
//                menuToolStripMenuItem.DropDownItems.Add(toolStripMenuItem);
//            }
//        }

//        private ToolStripMenuItem CreateMenuToolStripMenuItem()
//        {
//            var toolStripMenuItem = new ToolStripMenuItem
//            {
//                Name = "menuToolStripMenuItem",
//                Text = "&" + CommonTitles.Menu
//            };

//            return toolStripMenuItem;
//        }

//        private ToolStripMenuItem CreateDocumentTreeToolStripMenuItem()
//        {
//            var toolStripMenuItem = new ToolStripMenuItem
//            {
//                Name = "documentTreeToolStripMenuItem",
//                Text = "&" + Titles.DocumentTree
//            };

//            return toolStripMenuItem;
//        }

//        private ToolStripMenuItem CreateCurrentPatchesToolStripMenuItem()
//        {
//            var toolStripMenuItem = new ToolStripMenuItem
//            {
//                Name = "currentPatchesToolStripMenuItem",
//                Text = "&" + Titles.CurrentPatches
//            };

//            return toolStripMenuItem;
//        }

//        private ToolStripMenuItem CreateDocumentSaveToolStripMenuItem()
//        {
//            var toolStripMenuItem = new ToolStripMenuItem
//            {
//                Name = "documentSaveToolStripMenuItem",
//                Text = "&" + CommonTitleFormatter.SaveObject(PropertyDisplayNames.Document)
//            };

//            return toolStripMenuItem;
//        }

//        private ToolStripMenuItem CreateDocumentCloseToolStripMenuItem()
//        {
//            var toolStripMenuItem = new ToolStripMenuItem
//            {
//                Name = "documentCloseToolStripMenuItem",
//                Text = "&" + CommonTitleFormatter.CloseObject(PropertyDisplayNames.Document)
//            };

//            return toolStripMenuItem;
//        }

//        private ToolStripMenuItem CreateDocumentsToolStripMenuItem()
//        {
//            var toolStripMenuItem = new ToolStripMenuItem
//            {
//                Name = "documentsToolStripMenuItem",
//                Text = "&" + PropertyDisplayNames.Documents
//            };

//            return toolStripMenuItem;
//        }

//        // Events

//        private void documentTreeToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (ShowDocumentTreeRequested != null)
//            {
//                ShowDocumentTreeRequested(sender, EventArgs.Empty);
//            }
//        }

//        private void currentPatchesToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (ShowCurrentPatchesRequested != null)
//            {
//                ShowCurrentPatchesRequested(sender, EventArgs.Empty);
//            }
//        }

//        private void documentSaveToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (DocumentSaveRequested != null)
//            {
//                DocumentSaveRequested(sender, EventArgs.Empty);
//            }
//        }

//        private void documentCloseToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (DocumentCloseRequested != null)
//            {
//                DocumentCloseRequested(sender, EventArgs.Empty);
//            }
//        }

//        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
//        {
//            if (ShowDocumentGridRequested != null)
//            {
//                ShowDocumentGridRequested(sender, EventArgs.Empty);
//            }
//        }
//    }
//}
