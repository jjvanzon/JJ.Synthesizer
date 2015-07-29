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
using JJ.Presentation.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentTreeUserControl : UserControl
    {
        public event EventHandler CloseRequested;
        public event EventHandler<Int32EventArgs> DocumentPropertiesRequested;
        public event EventHandler<Int32EventArgs> ExpandNodeRequested;
        public event EventHandler<Int32EventArgs> CollapseNodeRequested;
        public event EventHandler ShowInstrumentsRequested;
        public event EventHandler ShowEffectsRequested;
        public event EventHandler<Int32EventArgs> ShowSamplesRequested;
        public event EventHandler<Int32EventArgs> ShowCurvesRequested;
        public event EventHandler<Int32EventArgs> ShowPatchesRequested;
        public event EventHandler ShowAudioFileOutputsRequested;

        /// <summary> virtually not nullable </summary>
        private DocumentTreeViewModel _viewModel;

        private TreeNode _instrumentsTreeNode;
        private TreeNode _effectsTreeNode;
        private HashSet<TreeNode> _samplesTreeNodes;
        private HashSet<TreeNode> _curvesTreeNodes;
        private HashSet<TreeNode> _patchesTreeNodes;
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

        private bool applyViewModelIsBusy = false;

        private void ApplyViewModel()
        {
            applyViewModelIsBusy = true;

            _samplesTreeNodes = new HashSet<TreeNode>();
            _curvesTreeNodes = new HashSet<TreeNode>();
            _patchesTreeNodes = new HashSet<TreeNode>();

            treeView.SuspendLayout();

            treeView.Nodes.Clear();

            if (_viewModel == null)
            {
                treeView.ResumeLayout();
                return;
            }

            var documentTreeNode = new TreeNode(_viewModel.Name);
            documentTreeNode.ContextMenuStrip = contextMenuStripDocument;
            treeView.Nodes.Add(documentTreeNode);

            AddChildNodesRecursive(documentTreeNode, _viewModel);

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

            documentTreeNode.Expand();

            treeView.ResumeLayout();

            applyViewModelIsBusy = false;
        }

        private void AddChildNodesRecursive(TreeNode parentNode, DocumentTreeViewModel parentViewModel)
        {
            _instrumentsTreeNode = new TreeNode(PropertyDisplayNames.Instruments);
            parentNode.Nodes.Add(_instrumentsTreeNode);

            foreach (ChildDocumentTreeNodeViewModel instrumentViewModel in parentViewModel.Instruments)
            {
                var instrumentTreeNode = new TreeNode(instrumentViewModel.Name);
                instrumentTreeNode.Tag = TagHelper.GetChildDocumentNodeIndexTag(instrumentViewModel.NodeIndex);
                _instrumentsTreeNode.Nodes.Add(instrumentTreeNode);

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
                effectTreeNode.Tag = TagHelper.GetChildDocumentNodeIndexTag(effectViewModel.NodeIndex);
                _effectsTreeNode.Nodes.Add(effectTreeNode);

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

        private void AddChildDocumentChildNodesRecursive(TreeNode parentNode, ChildDocumentTreeNodeViewModel parentViewModel)
        {
            object childDocumentTag = TagHelper.GetChildDocumentTag(parentViewModel.ChildDocumentID);

            var samplesTreeNode = new TreeNode(PropertyDisplayNames.Samples);
            samplesTreeNode.Tag = childDocumentTag;
            parentNode.Nodes.Add(samplesTreeNode);
            _samplesTreeNodes.Add(samplesTreeNode);

            var curvesTreeNode = new TreeNode(PropertyDisplayNames.Curves);
            curvesTreeNode.Tag = childDocumentTag;
            parentNode.Nodes.Add(curvesTreeNode);
            _curvesTreeNodes.Add(curvesTreeNode);

            var patchesTreeNode = new TreeNode(PropertyDisplayNames.Patches);
            patchesTreeNode.Tag = childDocumentTag;
            parentNode.Nodes.Add(patchesTreeNode);
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
                DocumentPropertiesRequested(this, new Int32EventArgs(_viewModel.ID)); // TODO: At some point I am going to have to get it from the TreeNode.Tag, instead of the ViewModel.
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

        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (applyViewModelIsBusy)
            {
                return;
            }

            int? nodeIndex = TagHelper.TryGetChildDocumentNodeIndex(e.Node.Tag);
            if (nodeIndex.HasValue)
            {
                if (ExpandNodeRequested != null)
                {
                    ExpandNodeRequested(this, new Int32EventArgs(nodeIndex.Value));
                }
            }
        }

        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (applyViewModelIsBusy)
            {
                return;
            }

            int? nodeIndex = TagHelper.TryGetChildDocumentNodeIndex(e.Node.Tag);
            if (nodeIndex.HasValue)
            {
                if (CollapseNodeRequested != null)
                {
                    CollapseNodeRequested(this, new Int32EventArgs(nodeIndex.Value));
                }
            }
        }

        // NOTE: WinForms does not allow giving event handlers to specific nodes, so you need the if's.

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == _instrumentsTreeNode)
            {
                if (ShowInstrumentsRequested != null)
                {
                    ShowInstrumentsRequested(this, EventArgs.Empty);
                }
            }

            if (e.Node == _effectsTreeNode)
            {
                if (ShowEffectsRequested != null)
                {
                    ShowEffectsRequested(this, EventArgs.Empty);
                }
            }

            if (e.Node == _audioFileOutputsTreeNode)
            {
                if (ShowAudioFileOutputsRequested != null)
                {
                    ShowAudioFileOutputsRequested(this, EventArgs.Empty);
                }
            }

            if (_samplesTreeNodes.Contains(e.Node))
            {
                if (ShowSamplesRequested != null)
                {
                    Int32EventArgs e2 = GetChildDocumentEventArgs(e.Node.Tag);
                    ShowSamplesRequested(this, e2);
                }
            }

            if (_curvesTreeNodes.Contains(e.Node))
            {
                if (ShowCurvesRequested != null)
                {
                    Int32EventArgs e2 = GetChildDocumentEventArgs(e.Node.Tag);
                    ShowCurvesRequested(this, e2);
                }
            }

            if (_patchesTreeNodes.Contains(e.Node))
            {
                if (ShowPatchesRequested != null)
                {
                    Int32EventArgs e2 = GetChildDocumentEventArgs(e.Node.Tag);
                    ShowPatchesRequested(this, e2);
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