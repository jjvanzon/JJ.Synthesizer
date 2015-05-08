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
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Forms;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentTreeUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler<IDEventArgs> DocumentPropertiesRequested;

        /// <summary> virtually not nullable </summary>
        private DocumentTreeViewModel _viewModel;

        public DocumentTreeUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DocumentTreeViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                if (value == null) throw new NullException(() => value);
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = Titles.DocumentTree;
            propertiesToolStripMenuItemDocumentProperties.Text = "&" + CommonTitles.Properties;
        }

        private void ApplyViewModel()
        {
            // TODO: I will probably run into the problem that
            // I cannot recreated the tree view every time,
            // but need to update it, because otherwise
            // all nodes are collapsed again after I update.
            // Another alternative might be to let the view model
            // decide if a node is open or not.

            treeView.SuspendLayout();

            treeView.Nodes.Clear();

            if (_viewModel == null)
            {
                treeView.ResumeLayout();
                return;
            }

            TreeNode documentTreeNode = CreateDocumentTreeNode();
            treeView.Nodes.Add(documentTreeNode);

            AddChildNodesRecursive(documentTreeNode, _viewModel);

            var referencedDocumentsTreeNode = new TreeNode(PropertyDisplayNames.ReferencedDocuments);
            documentTreeNode.Nodes.Add(referencedDocumentsTreeNode);

            foreach (ReferencedDocumentViewModel referencedDocumentViewModel in _viewModel.ReferencedDocuments.List)
            {
                var referencedDocumentTreeNode = new TreeNode(referencedDocumentViewModel.Name);
                referencedDocumentTreeNode.Tag = referencedDocumentViewModel.ID;
                referencedDocumentsTreeNode.Nodes.Add(referencedDocumentTreeNode);

                foreach (IDAndName instrumentViewModel in referencedDocumentViewModel.Instruments)
                {
                    var instrumentTreeNode = new TreeNode(instrumentViewModel.Name);
                    instrumentTreeNode.Tag = instrumentViewModel.ID;
                    referencedDocumentTreeNode.Nodes.Add(instrumentTreeNode);
                }

                foreach (IDAndName effectViewModel in referencedDocumentViewModel.Effects)
                {
                    var effectTreeNode = new TreeNode(effectViewModel.Name);
                    effectTreeNode.Tag = effectViewModel.ID;
                    referencedDocumentTreeNode.Nodes.Add(effectTreeNode);
                }
            }

            documentTreeNode.ExpandAll();

            treeView.ResumeLayout();
        }

        private TreeNode CreateDocumentTreeNode()
        {
            var treeNode = new TreeNode(_viewModel.Name);
            treeNode.Tag = _viewModel.ID;
            treeNode.ContextMenuStrip = contextMenuStripDocument;
            return treeNode;
        }

        private void AddChildNodesRecursive(TreeNode parentNode, DocumentTreeViewModel parentViewModel)
        {
            var instrumentsTreeNode = new TreeNode(PropertyDisplayNames.Instruments);
            parentNode.Nodes.Add(instrumentsTreeNode);

            foreach (DocumentTreeViewModel instrumentViewModel in parentViewModel.Instruments)
            {
                var instrumentTreeNode = new TreeNode(instrumentViewModel.Name);
                instrumentsTreeNode.Tag = instrumentViewModel.ID;
                instrumentsTreeNode.Nodes.Add(instrumentTreeNode);

                AddChildNodesRecursive(instrumentTreeNode, instrumentViewModel);
            }

            var effectsTreeNode = new TreeNode(PropertyDisplayNames.Effects);
            parentNode.Nodes.Add(effectsTreeNode);

            foreach (DocumentTreeViewModel effectViewModel in parentViewModel.Effects)
            {
                var effectTreeNode = new TreeNode(effectViewModel.Name);
                effectTreeNode.Tag = effectViewModel.ID;
                effectsTreeNode.Nodes.Add(effectTreeNode);

                AddChildNodesRecursive(effectTreeNode, effectViewModel);
            }

            var samplesTreeNode = new TreeNode(PropertyDisplayNames.Samples);
            parentNode.Nodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
            parentNode.Nodes.Add(curvesTreeNode);

            var patchesTreeNode = new TreeNode(PropertyDisplayNames.Patches);
            parentNode.Nodes.Add(patchesTreeNode);

            var audioFileOutputsTreeNode = new TreeNode(PropertyDisplayNames.AudioFileOutputs);
            parentNode.Nodes.Add(audioFileOutputsTreeNode);
        }

        // Actions

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        private void ShowDocumentProperties()
        {
            if (DocumentPropertiesRequested != null)
            {
                DocumentPropertiesRequested(this, new IDEventArgs(_viewModel.ID)); // TODO: At some point I am going to have to get it from the TreeNode.Tag, instead of the ViewModel.
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowDocumentProperties();
        }
    }
}
