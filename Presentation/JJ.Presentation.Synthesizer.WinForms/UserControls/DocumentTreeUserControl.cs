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
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentTreeUserControl : UserControlBase
    {
        public event EventHandler CloseRequested;

        public event EventHandler<EventArgs<int>> ExpandNodeRequested;
        public event EventHandler<EventArgs<int>> CollapseNodeRequested;

        public event EventHandler<EventArgs<string>> ShowPatchGridRequested;
        public event EventHandler<EventArgs<int>> ShowPatchDetailsRequested;
        public event EventHandler<EventArgs<int>> ShowCurvesRequested;
        public event EventHandler<EventArgs<int>> ShowSamplesRequested;
        public event EventHandler ShowAudioOutputRequested;
        public event EventHandler ShowAudioFileOutputsRequested;
        public event EventHandler ShowScalesRequested;

        private HashSet<TreeNode> _patchesTreeNodes;
        private HashSet<TreeNode> _patchTreeNodes;
        private HashSet<TreeNode> _samplesTreeNodes;
        private HashSet<TreeNode> _curvesTreeNodes;
        private TreeNode _scalesTreeNode;
        private TreeNode _audioOutputNode;
        private TreeNode _audioFileOutputListTreeNode;

        public DocumentTreeUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles()
        {
            titleBarUserControl.Text = Titles.DocumentTree;
        }

        // Binding

        private new DocumentTreeViewModel ViewModel => (DocumentTreeViewModel)base.ViewModel;

        private bool _applyViewModelIsBusy = false;

        protected override void ApplyViewModelToControls()
        {
            try
            {
                _applyViewModelIsBusy = true;
                treeView.SuspendLayout();
                treeView.BeginUpdate();

                _patchesTreeNodes = new HashSet<TreeNode>();
                _patchTreeNodes = new HashSet<TreeNode>();
                _samplesTreeNodes = new HashSet<TreeNode>();
                _curvesTreeNodes = new HashSet<TreeNode>();

                treeView.Nodes.Clear();

                if (ViewModel == null)
                {
                    treeView.ResumeLayout();
                    return;
                }

                AddDocumentDescendantNodes(treeView.Nodes, ViewModel);

                // TODO: Uncomment when the referenced documents functionality is programmed.
                //var referencedDocumentsTreeNode = new TreeNode(PropertyDisplayNames.ReferencedDocuments);
                //treeView.Nodes.Add(referencedDocumentsTreeNode);

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

            }
            finally
            {
                _applyViewModelIsBusy = false;
                treeView.EndUpdate();
                treeView.ResumeLayout();
            }
        }

        private void AddDocumentDescendantNodes(TreeNodeCollection nodes, DocumentTreeViewModel documentTreeViewModel)
        {
            // Patches
            var patchesTreeNode = new TreeNode(documentTreeViewModel.PatchesNode.Text);
            nodes.Add(patchesTreeNode);
            _patchesTreeNodes.Add(patchesTreeNode);

            // PatchGroups
            foreach (PatchGroupTreeNodeViewModel patchGroupViewModel in documentTreeViewModel.PatchesNode.PatchGroupNodes)
            {
                var patchGroupTreeNode = new TreeNode(patchGroupViewModel.Text);
                patchGroupTreeNode.Tag = patchGroupViewModel.GroupName;
                patchesTreeNode.Nodes.Add(patchGroupTreeNode);
                _patchesTreeNodes.Add(patchGroupTreeNode);

                foreach (PatchTreeNodeViewModel patchViewModel in patchGroupViewModel.PatchNodes)
                {
                    TreeNode patchTreeNode = ConvertPatchTreeNode(patchViewModel);
                    patchGroupTreeNode.Nodes.Add(patchTreeNode);
                    _patchTreeNodes.Add(patchTreeNode);
                }

                patchGroupTreeNode.Expand();
            }

            // Patches (Groupless)
            foreach (PatchTreeNodeViewModel patchViewModel in documentTreeViewModel.PatchesNode.PatchNodes)
            {
                TreeNode patchTreeNode = ConvertPatchTreeNode(patchViewModel);
                patchesTreeNode.Nodes.Add(patchTreeNode);
                _patchTreeNodes.Add(patchTreeNode);
            }

            patchesTreeNode.Expand();

            // Other Nodes
            var samplesTreeNode = new TreeNode(documentTreeViewModel.SamplesNode.Text);
            nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(documentTreeViewModel.CurvesNode.Text);
            nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);

            _scalesTreeNode = new TreeNode(documentTreeViewModel.ScalesNode.Text);
            nodes.Add(_scalesTreeNode);

            _audioOutputNode = new TreeNode(documentTreeViewModel.AudioOutputNode.Text);
            nodes.Add(_audioOutputNode);

            _audioFileOutputListTreeNode = new TreeNode(documentTreeViewModel.AudioFileOutputListNode.Text);
            nodes.Add(_audioFileOutputListTreeNode);
        }

        private TreeNode ConvertPatchTreeNode(PatchTreeNodeViewModel patchTreeNodeViewModel)
        {
            var patchTreeNode = new TreeNode(patchTreeNodeViewModel.Text);
            patchTreeNode.Tag = patchTreeNodeViewModel.ChildDocumentID;

            AddPatchDescendantNodes(patchTreeNode.Nodes, patchTreeNodeViewModel);

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

        private void AddPatchDescendantNodes(TreeNodeCollection nodes, PatchTreeNodeViewModel patchTreeNodeViewModel)
        {
            object childDocumentTag = TagHelper.GetChildDocumentTag(patchTreeNodeViewModel.ChildDocumentID);

            var samplesTreeNode = new TreeNode(patchTreeNodeViewModel.SamplesNode.Text);
            samplesTreeNode.Tag = childDocumentTag;
            nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(patchTreeNodeViewModel.CurvesNode.Text);
            curvesTreeNode.Tag = childDocumentTag;
            nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);
        }

        // Actions

        private void Close()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
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
                ExpandNodeRequested?.Invoke(this, new EventArgs<int>(id));
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
                CollapseNodeRequested?.Invoke(this, new EventArgs<int>(id));
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

            if (node == _audioFileOutputListTreeNode)
            {
                ShowAudioFileOutputsRequested?.Invoke(this, EventArgs.Empty);
            }

            if (node == _audioOutputNode)
            {
                ShowAudioOutputRequested?.Invoke(this, EventArgs.Empty);
            }

            if (_curvesTreeNodes.Contains(node))
            {
                ShowCurvesRequested?.Invoke(this, GetChildDocumentEventArgs(node.Tag));
            }

            if (_patchesTreeNodes.Contains(node))
            {
                ShowPatchGridRequested?.Invoke(this, new EventArgs<string>((string)node.Tag));
            }

            if (_patchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                ShowPatchDetailsRequested?.Invoke(this, new EventArgs<int>(id));
            }

            if (_samplesTreeNodes.Contains(node))
            {
                ShowSamplesRequested?.Invoke(this, GetChildDocumentEventArgs(node.Tag));
            }

            if (node == _scalesTreeNode)
            {
                ShowScalesRequested?.Invoke(this, EventArgs.Empty);
            }
        }

        private EventArgs<int> GetChildDocumentEventArgs(object childDocumentTag)
        {
            int? childDocumentID = TagHelper.TryGetChildDocumentID(childDocumentTag);
            int documentID = childDocumentID ?? ViewModel.ID;
            return new EventArgs<int>(documentID);
        }
    }
}