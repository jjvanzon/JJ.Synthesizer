using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.Resources;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentTreeUserControl : UserControl
    {
        public event EventHandler CloseRequested;

        public event EventHandler<Int32EventArgs> ExpandNodeRequested;
        public event EventHandler<Int32EventArgs> CollapseNodeRequested;

        public event EventHandler ShowDocumentPropertiesRequested;
        public event EventHandler ShowAudioFileOutputsRequested;
        public event EventHandler<Int32EventArgs> ShowCurvesRequested;
        public event EventHandler<Int32EventArgs> ShowPatchDetailsRequested;
        public event EventHandler<StringEventArgs> ShowPatchGridRequested;
        public event EventHandler<Int32EventArgs> ShowSamplesRequested;
        public event EventHandler ShowScalesRequested;

        private DocumentTreeViewModel _viewModel;

        private TreeNode _documentTreeNode;
        private HashSet<TreeNode> _samplesTreeNodes;
        private HashSet<TreeNode> _curvesTreeNodes;
        private HashSet<TreeNode> _patchTreeNodes;
        private HashSet<TreeNode> _patchesTreeNodes;
        private TreeNode _scalesTreeNode;
        private TreeNode _audioFileOutputsTreeNode;

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
                _viewModel = value;
                ApplyViewModel();
            }
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = Titles.DocumentTree;
        }

        private bool _applyViewModelIsBusy = false;

        private void ApplyViewModel()
        {
            try
            {
                _applyViewModelIsBusy = true;
                treeView.SuspendLayout();
                treeView.BeginUpdate();

                _samplesTreeNodes = new HashSet<TreeNode>();
                _curvesTreeNodes = new HashSet<TreeNode>();
                _patchTreeNodes = new HashSet<TreeNode>();
                _patchesTreeNodes = new HashSet<TreeNode>();

                treeView.Nodes.Clear();

                if (_viewModel == null)
                {
                    treeView.ResumeLayout();
                    return;
                }

                _documentTreeNode = new TreeNode(_viewModel.Name);
                treeView.Nodes.Add(_documentTreeNode);

                AddDocumentDescendantNodes(_documentTreeNode, _viewModel);

                // TODO: Uncomment when the referenced documents functionality is programmed.
                //var referencedDocumentsTreeNode = new TreeNode(PropertyDisplayNames.ReferencedDocuments);
                //documentTreeNode.Nodes.Add(referencedDocumentsTreeNode);

                //foreach (ReferencedDocumentViewModel referencedDocumentViewModel in _viewModel.ReferencedDocuments.List)
                //{
                //    var referencedDocumentTreeNode = new TreeNode(referencedDocumentViewModel.Name);
                //    referencedDocumentsTreeNode.Nodes.Add(referencedDocumentTreeNode);

                //    foreach (IDAndName instrumentViewModel in referencedDocumentViewModel.Instruments)
                //    {
                //        var instrumentTreeNode = new TreeNode(instrumentViewModel.Name);
                //        referencedDocumentTreeNode.Nodes.Add(instrumentTreeNode);
                //    }

                //    foreach (IDAndName effectViewModel in referencedDocumentViewModel.Effects)
                //    {
                //        var effectTreeNode = new TreeNode(effectViewModel.Name);
                //        referencedDocumentTreeNode.Nodes.Add(effectTreeNode);
                //    }

                //    referencedDocumentTreeNode.Expand();
                //}

                //referencedDocumentsTreeNode.Expand();

                _documentTreeNode.Expand();

            }
            finally
            {
                _applyViewModelIsBusy = false;
                treeView.EndUpdate();
                treeView.ResumeLayout();
            }
        }

        private void AddDocumentDescendantNodes(TreeNode documentNode, DocumentTreeViewModel documentNodeViewModel)
        {
            // Patches
            var patchesTreeNode = new TreeNode(PropertyDisplayNames.Patches);
            documentNode.Nodes.Add(patchesTreeNode);
            _patchesTreeNodes.Add(patchesTreeNode);

            // PatchGroups
            foreach (PatchGroupTreeNodeViewModel patchGroupViewModel in documentNodeViewModel.PatchesNode.PatchGroupNodes)
            {
                var patchGroupTreeNode = new TreeNode(patchGroupViewModel.Name);
                patchGroupTreeNode.Tag = patchGroupViewModel.Name;
                patchesTreeNode.Nodes.Add(patchGroupTreeNode);
                _patchesTreeNodes.Add(patchGroupTreeNode);

                foreach (PatchTreeNodeViewModel patchViewModel in patchGroupViewModel.Patches)
                {
                    TreeNode patchTreeNode = ConvertPatchTreeNode(patchViewModel);
                    patchGroupTreeNode.Nodes.Add(patchTreeNode);
                    _patchTreeNodes.Add(patchTreeNode);
                }

                patchGroupTreeNode.Expand();
            }

            // Patches (Groupless)
            foreach (PatchTreeNodeViewModel patchViewModel in documentNodeViewModel.PatchesNode.PatchNodes)
            {
                TreeNode patchTreeNode = ConvertPatchTreeNode(patchViewModel);
                patchesTreeNode.Nodes.Add(patchTreeNode);
                _patchTreeNodes.Add(patchTreeNode);
            }

            patchesTreeNode.Expand();

            // Other Nodes
            var samplesTreeNode = new TreeNode(PropertyDisplayNames.Samples);
            documentNode.Nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
            documentNode.Nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);

            _scalesTreeNode = new TreeNode(PropertyDisplayNames.Scales);
            documentNode.Nodes.Add(_scalesTreeNode);

            _audioFileOutputsTreeNode = new TreeNode(PropertyDisplayNames.AudioFileOutputs);
            documentNode.Nodes.Add(_audioFileOutputsTreeNode);
        }

        private TreeNode ConvertPatchTreeNode(PatchTreeNodeViewModel patchTreeNodeViewModel)
        {
            var patchTreeNode = new TreeNode(patchTreeNodeViewModel.Name);
            patchTreeNode.Tag = patchTreeNodeViewModel.ChildDocumentID;

            AddPatchDescendantNodes(patchTreeNode, patchTreeNodeViewModel);

            if (patchTreeNodeViewModel.IsExpanded)
            {
                patchTreeNode.Expand();
            }
            else
            {
                patchTreeNode.Collapse();
            }

            return patchTreeNode;
        }

        private void AddPatchDescendantNodes(TreeNode patchNode, PatchTreeNodeViewModel patchTreeNodeViewModel)
        {
            object childDocumentTag = TagHelper.GetChildDocumentTag(patchTreeNodeViewModel.ChildDocumentID);

            var samplesTreeNode = new TreeNode(PropertyDisplayNames.Samples);
            samplesTreeNode.Tag = childDocumentTag;
            patchNode.Nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
            curvesTreeNode.Tag = childDocumentTag;
            patchNode.Nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);
        }

        // Actions

        private void Close()
        {
            if (CloseRequested != null)
            {
                CloseRequested(this, EventArgs.Empty);
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e)
        {
            Close();
        }

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (_applyViewModelIsBusy)
            {
                return;
            }

            if (_patchTreeNodes.Contains(e.Node))
            {
                int id = (int)e.Node.Tag;
                if (ExpandNodeRequested != null)
                {
                    ExpandNodeRequested(this, new Int32EventArgs(id));
                }
            }
        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (_applyViewModelIsBusy)
            {
                return;
            }

            if (_patchTreeNodes.Contains(e.Node))
            {
                int id = (int)e.Node.Tag;
                if (CollapseNodeRequested != null)
                {
                    CollapseNodeRequested(this, new Int32EventArgs(id));
                }
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            HandleNodeKeyEnterOrDoubleClick(e.Node);
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            // Use ProcessCmdKey,because OnKeyDown produces an annoying Ding sound 
            // every time you hit enter.
            if (keyData == Keys.Enter)
            {
                if (treeView.SelectedNode != null)
                {
                    HandleNodeKeyEnterOrDoubleClick(treeView.SelectedNode);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Helpers

        private void HandleNodeKeyEnterOrDoubleClick(TreeNode node)
        {
            // NOTE: WinForms does not allow giving event handlers to specific nodes, so you need the if's.

            if (node == _documentTreeNode)
            {
                if (ShowDocumentPropertiesRequested != null)
                {
                    ShowDocumentPropertiesRequested(this, EventArgs.Empty);
                }
            }

            if (node == _audioFileOutputsTreeNode)
            {
                if (ShowAudioFileOutputsRequested != null)
                {
                    ShowAudioFileOutputsRequested(this, EventArgs.Empty);
                }
            }

            if (_curvesTreeNodes.Contains(node))
            {
                if (ShowCurvesRequested != null)
                {
                    Int32EventArgs e2 = GetChildDocumentEventArgs(node.Tag);
                    ShowCurvesRequested(this, e2);
                }
            }

            if (_patchesTreeNodes.Contains(node))
            {
                if (ShowPatchGridRequested != null)
                {
                    var e2 = new StringEventArgs((string)node.Tag);
                    ShowPatchGridRequested(this, e2);
                }
            }

            if (_patchTreeNodes.Contains(node))
            {
                if (ShowPatchDetailsRequested != null)
                {
                    int id = (int)node.Tag;
                    Int32EventArgs e2 = new Int32EventArgs(id);
                    ShowPatchDetailsRequested(this, e2);
                }
            }

            if (_samplesTreeNodes.Contains(node))
            {
                if (ShowSamplesRequested != null)
                {
                    Int32EventArgs e2 = GetChildDocumentEventArgs(node.Tag);
                    ShowSamplesRequested(this, e2);
                }
            }

            if (node == _scalesTreeNode)
            {
                if (ShowScalesRequested != null)
                {
                    ShowScalesRequested(this, EventArgs.Empty);
                }
            }
        }

        private Int32EventArgs GetChildDocumentEventArgs(object childDocumentTag)
        {
            int? childDocumentID = TagHelper.TryGetChildDocumentID(childDocumentTag);
            int documentID = childDocumentID ?? ViewModel.ID;
            return new Int32EventArgs(documentID);
        }
    }
}