using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Reflection.Exceptions;
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

        public event EventHandler DocumentPropertiesRequested;
        public event EventHandler<Int32EventArgs> ShowChildDocumentPropertiesRequested;
        public event EventHandler<Int32EventArgs> ExpandNodeRequested;
        public event EventHandler<Int32EventArgs> CollapseNodeRequested;
        public event EventHandler ShowInstrumentsRequested;
        public event EventHandler ShowEffectsRequested;
        public event EventHandler<Int32EventArgs> ShowSamplesRequested;
        public event EventHandler<Int32EventArgs> ShowCurvesRequested;
        public event EventHandler<Int32EventArgs> ShowPatchesRequested;
        public event EventHandler ShowAudioFileOutputsRequested;

        private DocumentTreeViewModel _viewModel;

        private TreeNode _documentTreeNode;
        private TreeNode _instrumentsTreeNode;
        private TreeNode _effectsTreeNode;
        private HashSet<TreeNode> _samplesTreeNodes;
        private HashSet<TreeNode> _curvesTreeNodes;
        private HashSet<TreeNode> _patchesTreeNodes;
        private HashSet<TreeNode> _childDocumentTreeNodes;
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

                _samplesTreeNodes = new HashSet<TreeNode>();
                _curvesTreeNodes = new HashSet<TreeNode>();
                _patchesTreeNodes = new HashSet<TreeNode>();
                _childDocumentTreeNodes = new HashSet<TreeNode>();

                treeView.SuspendLayout();

                treeView.Nodes.Clear();

                if (_viewModel == null)
                {
                    treeView.ResumeLayout();
                    return;
                }

                _documentTreeNode = new TreeNode(_viewModel.Name);
                treeView.Nodes.Add(_documentTreeNode);

                AddChildNodesRecursive(_documentTreeNode, _viewModel);

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

                treeView.ResumeLayout();
            }
            finally
            {
                _applyViewModelIsBusy = false;
            }
        }

        private void AddChildNodesRecursive(TreeNode parentNode, DocumentTreeViewModel parentViewModel)
        {
            _instrumentsTreeNode = new TreeNode(PropertyDisplayNames.Instruments);
            parentNode.Nodes.Add(_instrumentsTreeNode);

            foreach (ChildDocumentTreeNodeViewModel instrumentViewModel in parentViewModel.Instruments)
            {
                var instrumentTreeNode = new TreeNode(instrumentViewModel.Name);
                instrumentTreeNode.Tag = instrumentViewModel.ChildDocumentID;
                _instrumentsTreeNode.Nodes.Add(instrumentTreeNode);
                _childDocumentTreeNodes.Add(instrumentTreeNode);

                AddChildDocumentChildNodesRecursive(instrumentTreeNode, instrumentViewModel);

                if (instrumentViewModel.IsExpanded)
                {
                    instrumentTreeNode.Expand();
                }
                else
                {
                    instrumentTreeNode.Collapse();
                }
            }

            _instrumentsTreeNode.Expand();

            _effectsTreeNode = new TreeNode(PropertyDisplayNames.Effects);
            parentNode.Nodes.Add(_effectsTreeNode);

            foreach (ChildDocumentTreeNodeViewModel effectViewModel in parentViewModel.Effects)
            {
                var effectTreeNode = new TreeNode(effectViewModel.Name);
                effectTreeNode.Tag = effectViewModel.ChildDocumentID;
                _effectsTreeNode.Nodes.Add(effectTreeNode);
                _childDocumentTreeNodes.Add(effectTreeNode);

                AddChildDocumentChildNodesRecursive(effectTreeNode, effectViewModel);

                if (effectViewModel.IsExpanded)
                {
                    effectTreeNode.Expand();
                }
                else
                {
                    effectTreeNode.Collapse();
                }
            }

            _effectsTreeNode.Expand();

            var samplesTreeNode = new TreeNode(PropertyDisplayNames.Samples);
            parentNode.Nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
            parentNode.Nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);

            var patchesTreeNode = new TreeNode(PropertyDisplayNames.Patches);
            parentNode.Nodes.Add(patchesTreeNode);
            _patchesTreeNodes.Add(patchesTreeNode);

            _audioFileOutputsTreeNode = new TreeNode(PropertyDisplayNames.AudioFileOutputs);
            parentNode.Nodes.Add(_audioFileOutputsTreeNode);
        }

        private void AddChildDocumentChildNodesRecursive(TreeNode childDocumentNode, ChildDocumentTreeNodeViewModel childDocumentViewModel)
        {
            object childDocumentTag = TagHelper.GetChildDocumentTag(childDocumentViewModel.ChildDocumentID);

            var samplesTreeNode = new TreeNode(PropertyDisplayNames.Samples);
            samplesTreeNode.Tag = childDocumentTag;
            childDocumentNode.Nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
            curvesTreeNode.Tag = childDocumentTag;
            childDocumentNode.Nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);

            var patchesTreeNode = new TreeNode(PropertyDisplayNames.Patches);
            patchesTreeNode.Tag = childDocumentTag;
            childDocumentNode.Nodes.Add(patchesTreeNode);
            _patchesTreeNodes.Add(patchesTreeNode);
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
                DocumentPropertiesRequested(this, EventArgs.Empty);
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

            if (_childDocumentTreeNodes.Contains(e.Node))
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

            if (_childDocumentTreeNodes.Contains(e.Node))
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
                ShowDocumentProperties();
            }

            if (node == _audioFileOutputsTreeNode)
            {
                if (ShowAudioFileOutputsRequested != null)
                {
                    ShowAudioFileOutputsRequested(this, EventArgs.Empty);
                }
            }

            if (_childDocumentTreeNodes.Contains(node))
            {
                if (ShowChildDocumentPropertiesRequested != null)
                {
                    int id = (int)node.Tag;
                    Int32EventArgs e2 = new Int32EventArgs(id);
                    ShowChildDocumentPropertiesRequested(this, e2);
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

            if (node == _effectsTreeNode)
            {
                if (ShowEffectsRequested != null)
                {
                    ShowEffectsRequested(this, EventArgs.Empty);
                }
            }

            if (node == _instrumentsTreeNode)
            {
                if (ShowInstrumentsRequested != null)
                {
                    ShowInstrumentsRequested(this, EventArgs.Empty);
                }
            }

            if (_patchesTreeNodes.Contains(node))
            {
                if (ShowPatchesRequested != null)
                {
                    Int32EventArgs e2 = GetChildDocumentEventArgs(node.Tag);
                    ShowPatchesRequested(this, e2);
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
        }

        private Int32EventArgs GetChildDocumentEventArgs(object childDocumentTag)
        {
            int? childDocumentID = TagHelper.TryGetChildDocumentID(childDocumentTag);
            int documentID = childDocumentID ?? ViewModel.ID;
            return new Int32EventArgs(documentID);
        }
    }
}