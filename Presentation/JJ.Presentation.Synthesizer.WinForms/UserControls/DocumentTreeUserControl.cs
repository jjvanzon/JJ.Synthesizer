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
    internal partial class DocumentTreeUserControl : DocumentTreeUserControl_NotDesignable
    {
        public event EventHandler CloseRequested;

        public event EventHandler<Int32EventArgs> ExpandNodeRequested;
        public event EventHandler<Int32EventArgs> CollapseNodeRequested;

        public event EventHandler ShowDocumentPropertiesRequested;
        public event EventHandler<StringEventArgs> ShowPatchGridRequested;
        public event EventHandler<Int32EventArgs> ShowPatchDetailsRequested;
        public event EventHandler<Int32EventArgs> ShowCurvesRequested;
        public event EventHandler<Int32EventArgs> ShowSamplesRequested;
        public event EventHandler ShowAudioOutputRequested;
        public event EventHandler ShowAudioFileOutputsRequested;
        public event EventHandler ShowScalesRequested;

        private TreeNode _documentTreeNode;
        private HashSet<TreeNode> _patchesTreeNodes;
        private HashSet<TreeNode> _patchTreeNodes;
        private HashSet<TreeNode> _samplesTreeNodes;
        private HashSet<TreeNode> _curvesTreeNodes;
        private TreeNode _scalesTreeNode;
        private TreeNode _audioOutputNode;
        private TreeNode _audioFileOutputsTreeNode;

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

        private void AddDocumentDescendantNodes(TreeNodeCollection nodes, DocumentTreeViewModel documentNodeViewModel)
        {
            // Patches
            var patchesTreeNode = new TreeNode(PropertyDisplayNames.Patches);
            nodes.Add(patchesTreeNode);
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
            nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
            nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);

            _scalesTreeNode = new TreeNode(PropertyDisplayNames.Scales);
            nodes.Add(_scalesTreeNode);

            _audioOutputNode = new TreeNode(PropertyDisplayNames.AudioOutput);
            nodes.Add(_audioOutputNode);

            _audioFileOutputsTreeNode = new TreeNode(PropertyDisplayNames.AudioFileOutputs);
            nodes.Add(_audioFileOutputsTreeNode);
        }

        private TreeNode ConvertPatchTreeNode(PatchTreeNodeViewModel patchTreeNodeViewModel)
        {
            var patchTreeNode = new TreeNode(patchTreeNodeViewModel.Name);
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

            var samplesTreeNode = new TreeNode(PropertyDisplayNames.Samples);
            samplesTreeNode.Tag = childDocumentTag;
            nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
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
                ExpandNodeRequested?.Invoke(this, new Int32EventArgs(id));
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
                CollapseNodeRequested?.Invoke(this, new Int32EventArgs(id));
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
                ShowDocumentPropertiesRequested?.Invoke(this, EventArgs.Empty);
            }

            if (node == _audioFileOutputsTreeNode)
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
                ShowPatchGridRequested?.Invoke(this, new StringEventArgs((string)node.Tag));
            }

            if (_patchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                ShowPatchDetailsRequested?.Invoke(this, new Int32EventArgs(id));
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

        private Int32EventArgs GetChildDocumentEventArgs(object childDocumentTag)
        {
            int? childDocumentID = TagHelper.TryGetChildDocumentID(childDocumentTag);
            int documentID = childDocumentID ?? ViewModel.ID;
            return new Int32EventArgs(documentID);
        }
    }

    /// <summary> 
    /// The WinForms designer does not work when deriving directly from a generic class.
    /// And also not when you make this class abstract.
    /// </summary>
    internal class DocumentTreeUserControl_NotDesignable : UserControlBase<DocumentTreeViewModel>
    {
        protected override void ApplyViewModelToControls()
        {
            throw new NotImplementedException();
        }
    }
}