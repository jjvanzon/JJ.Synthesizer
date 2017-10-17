using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentTreeUserControl : UserControlBase
    {
        private static readonly string _separator = Guid.NewGuid().ToString();

        public event EventHandler AddRequested;
        public event EventHandler AddToInstrumentRequested;
        public event EventHandler CloseRequested;
        public event EventHandler NewRequested;
        public event EventHandler OpenItemExternallyRequested;
        public event EventHandler<EventArgs<int>> PatchHovered;
        public event EventHandler PlayRequested;
        public event EventHandler RefreshRequested;
        public event EventHandler RemoveRequested;
        public event EventHandler SaveRequested;

        public event EventHandler<EventArgs<int>> ShowPatchDetailsRequested;
        public event EventHandler<EventArgs<int>> ShowLibraryPropertiesRequested;
        public event EventHandler ShowCurvesRequested;
        public event EventHandler ShowAudioOutputRequested;
        public event EventHandler ShowAudioFileOutputsRequested;
        public event EventHandler ShowScalesRequested;

        public event EventHandler<EventArgs<string>> PatchGroupNodeSelected;
        public event EventHandler<EventArgs<int>> PatchNodeSelected;
        public event EventHandler<EventArgs<int>> LibraryNodeSelected;
        public event EventHandler<LibraryPatchGroupEventArgs> LibraryPatchGroupNodeSelected;
        public event EventHandler<EventArgs<int>> LibraryPatchNodeSelected;
        public event EventHandler CurvesNodeSelected;
        public event EventHandler AudioOutputNodeSelected;
        public event EventHandler AudioFileOutputsNodeSelected;
        public event EventHandler ScalesNodeSelected;
        public event EventHandler LibrariesNodeSelected;

        private HashSet<TreeNode> _patchGroupTreeNodes;
        private HashSet<TreeNode> _patchTreeNodes;
        private HashSet<TreeNode> _libraryTreeNodes;
        private HashSet<TreeNode> _libraryPatchTreeNodes;
        private HashSet<TreeNode> _libraryPatchGroupTreeNodes;
        private TreeNode _curvesTreeNode;
        private TreeNode _scalesTreeNode;
        private TreeNode _audioOutputNode;
        private TreeNode _audioFileOutputListTreeNode;
        private TreeNode _librariesTreeNode;
        private TreeNode _patchesTreeNode;
        private TreeNode _mouseHoverNode;

        public DocumentTreeUserControl()
        {
            InitializeComponent();

            ApplyStyling();
            AddInvariantNodes();
        }

        // Gui

        public void ApplyStyling()
        {
            tableLayoutPanel.RowStyles[0].Height = StyleHelper.TitleBarHeight;
        }

        // Binding

        public new DocumentTreeViewModel ViewModel
        {
            get => (DocumentTreeViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            titleBarUserControl.AddButtonVisible = ViewModel.CanAdd;
            titleBarUserControl.AddToInstrumentButtonVisible = ViewModel.CanAddToInstrument;
            titleBarUserControl.NewButtonVisible = ViewModel.CanCreateNew;
            titleBarUserControl.OpenButtonVisible = ViewModel.CanOpenExternally;
            titleBarUserControl.PlayButtonVisible = ViewModel.CanPlay;
            titleBarUserControl.RemoveButtonVisible = ViewModel.CanRemove;

            _patchGroupTreeNodes = new HashSet<TreeNode> { _patchesTreeNode };
            _patchTreeNodes = new HashSet<TreeNode>();
            _libraryTreeNodes = new HashSet<TreeNode>();
            _libraryPatchTreeNodes = new HashSet<TreeNode>();
            _libraryPatchGroupTreeNodes = new HashSet<TreeNode>();

            if (ViewModel == null)
            {
                return;
            }

            ConvertNodes(ViewModel);
            SetSelectedNode();

            // The following code is for working around wonky WinForms behavior.
            // You really would not want to know this information, but it does explain the code.
            // - Whether or not the TreeView.ShowNodeToolTips is set,
            //   TreeView control will show the node's text as a tool tip if it the text does not fit on screen.
            //   (the TreeView.ShowNodeToolTips only controls whether the TreNode.ToolTipText is used.
            //    I know: This makes ShowNodeToolTips a really bad property name.)
            // - We use a ToolTip component for better control over the timers around showing the tooltip.
            //   TreeView does not offer that control. For instance we want to keep the ToolTip not to auto-hide after x amount of time.
            // - But then we still need to let TreeView and ToolTip play along toghether,
            //   otherwise they will both be showing tooltips at the same time.
            string treeViewsOwnToolTipText = _mouseHoverNode?.Text;
            if (!string.Equals(ViewModel.PatchToolTipText ?? "", treeViewsOwnToolTipText ?? ""))
            {
                toolTip.SetToolTip(treeView, ViewModel.PatchToolTipText);
            }
            else
            {
                toolTip.SetToolTip(treeView, "");
            }
        }

        private void AddInvariantNodes()
        {
            _patchesTreeNode = new TreeNode();
            treeView.Nodes.Add(_patchesTreeNode);
            _patchesTreeNode.Expand();

            _curvesTreeNode = new TreeNode();
            treeView.Nodes.Add(_curvesTreeNode);

            _scalesTreeNode = new TreeNode();
            treeView.Nodes.Add(_scalesTreeNode);

            _audioOutputNode = new TreeNode();
            treeView.Nodes.Add(_audioOutputNode);

            _audioFileOutputListTreeNode = new TreeNode();
            treeView.Nodes.Add(_audioFileOutputListTreeNode);

            _librariesTreeNode = new TreeNode();
            treeView.Nodes.Add(_librariesTreeNode);
            _librariesTreeNode.Expand();
        }

        private void ConvertNodes(DocumentTreeViewModel viewModel)
        {
            if (_curvesTreeNode.Text != viewModel.CurvesNode.Text)
            {
                _curvesTreeNode.Text = viewModel.CurvesNode.Text;
            }

            if (_scalesTreeNode.Text != viewModel.ScalesNode.Text)
            {
                _scalesTreeNode.Text = viewModel.ScalesNode.Text;
            }

            if (_audioOutputNode.Text != viewModel.AudioOutputNode.Text)
            {
                _audioOutputNode.Text = viewModel.AudioOutputNode.Text;
            }

            if (_audioFileOutputListTreeNode.Text != viewModel.AudioFileOutputListNode.Text)
            {
                _audioFileOutputListTreeNode.Text = viewModel.AudioFileOutputListNode.Text;
            }

            if (_patchesTreeNode.Text != viewModel.PatchesNode.Text)
            {
                _patchesTreeNode.Text = viewModel.PatchesNode.Text;
            }
            ConvertPatchesDescendants(viewModel.PatchesNode, _patchesTreeNode);

            if (_librariesTreeNode.Text != viewModel.LibrariesNode.Text)
            {
                _librariesTreeNode.Text = viewModel.LibrariesNode.Text;
            }
            ConvertLibrariesDescendants(viewModel.LibrariesNode.List, _librariesTreeNode.Nodes);
        }

        private void ConvertPatchesDescendants(PatchesTreeNodeViewModel viewModel, TreeNode treeNode)
        {
            var treeNodesToKeep = new HashSet<TreeNode>();

            bool mustSort = false;

            // Groupless
            foreach (PatchTreeNodeViewModel patchViewModel in viewModel.PatchNodes)
            {
                TreeNode patchTreeNode = ConvertPatchNode(patchViewModel, treeNode.Nodes, out bool isNewOrIsDirtyName);
                _patchTreeNodes.Add(patchTreeNode);
                treeNodesToKeep.Add(patchTreeNode);

                mustSort |= isNewOrIsDirtyName;
            }

            // PatchGroups
            foreach (PatchGroupTreeNodeViewModel patchGroupViewModel in viewModel.PatchGroupNodes)
            {
                TreeNode patchGroupTreeNode = ConvertPatchGroupAndDescendants(patchGroupViewModel, treeNode.Nodes, out bool isNewOrIsDirtyName);
                _patchGroupTreeNodes.Add(patchGroupTreeNode);
                treeNodesToKeep.Add(patchGroupTreeNode);

                mustSort |= isNewOrIsDirtyName;
            }

            // Deletions
            IEnumerable<TreeNode> existingTreeNodes = treeNode.Nodes.Cast<TreeNode>();
            IEnumerable<TreeNode> treeNodesToDelete = existingTreeNodes.Except(treeNodesToKeep);
            foreach (TreeNode treeNodeToDelete in treeNodesToDelete.ToArray())
            {
                treeNode.Nodes.Remove(treeNodeToDelete);
            }

            // Sort
            // ReSharper disable once InvertIf
            if (mustSort)
            {
                SortTreeNodes(treeNode.Nodes);
            }
        }

        private TreeNode ConvertPatchGroupAndDescendants(
            PatchGroupTreeNodeViewModel patchGroupViewModel,
            TreeNodeCollection patchesTreeNodes,
            out bool isNewOrIsDirtyName)
        {
            TreeNode patchGroupTreeNode = ConvertPatchGroup(patchGroupViewModel, patchesTreeNodes, out isNewOrIsDirtyName);

            ConvertPatches(patchGroupViewModel.PatchNodes, patchGroupTreeNode.Nodes);

            return patchGroupTreeNode;
        }

        private void ConvertPatches(IList<PatchTreeNodeViewModel> viewModels, TreeNodeCollection treeNodes)
        {
            var treeNodesToKeep = new HashSet<TreeNode>();

            bool mustSort = false;

            foreach (PatchTreeNodeViewModel viewModel in viewModels)
            {
                TreeNode treeNode = ConvertPatchNode(viewModel, treeNodes, out bool isNewOrIsDirtyName);
                treeNodesToKeep.Add(treeNode);

                _patchTreeNodes.Add(treeNode);

                mustSort |= isNewOrIsDirtyName;
            }

            IEnumerable<TreeNode> existingTreeNodes = treeNodes.Cast<TreeNode>();
            IEnumerable<TreeNode> treeNodesToDelete = existingTreeNodes.Except(treeNodesToKeep);
            foreach (TreeNode treeNodeToDelete in treeNodesToDelete.ToArray())
            {
                treeNodes.Remove(treeNodeToDelete);
            }

            // Sort
            // ReSharper disable once InvertIf
            if (mustSort)
            {
                SortTreeNodes(treeNodes);
            }
        }

        private void ConvertLibrariesDescendants(IList<LibraryTreeNodeViewModel> viewModels, TreeNodeCollection treeNodes)
        {
            var treeNodesToKeep = new HashSet<TreeNode>();

            bool mustSort = false;

            foreach (LibraryTreeNodeViewModel viewModel in viewModels)
            {
                TreeNode treeNode = ConvertLibraryAndDescendants(viewModel, treeNodes, out bool isNewOrIsDirtyName);
                treeNodesToKeep.Add(treeNode);

                _libraryTreeNodes.Add(treeNode);

                mustSort |= isNewOrIsDirtyName;
            }

            // Deletions
            IEnumerable<TreeNode> existingTreeNodes = treeNodes.Cast<TreeNode>();
            IEnumerable<TreeNode> treeNodesToDelete = existingTreeNodes.Except(treeNodesToKeep);
            foreach (TreeNode treeNodeToDelete in treeNodesToDelete.ToArray())
            {
                treeNodes.Remove(treeNodeToDelete);
            }

            // Sort
            // ReSharper disable once InvertIf
            if (mustSort)
            {
                SortTreeNodes(treeNodes);
            }
        }

        private TreeNode ConvertLibraryAndDescendants(LibraryTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrIsDirtyName)
        {
            TreeNode libraryTreeNode = ConvertLibrary(viewModel, treeNodes, out isNewOrIsDirtyName);

            ConvertLibraryDescendants(viewModel, libraryTreeNode.Nodes);

            return libraryTreeNode;
        }

        private static TreeNode ConvertLibrary(LibraryTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrIsDirtyName)
        {
            TreeNode treeNode = treeNodes.Cast<TreeNode>()
                                         .Where(x => x.Tag is int)
                                         .Where(x => (int)x.Tag == viewModel.LowerDocumentReferenceID)
                                         .SingleOrDefault();
            isNewOrIsDirtyName = false;
            if (treeNode == null)
            {
                isNewOrIsDirtyName = true;
                treeNode = new TreeNode { Tag = viewModel.LowerDocumentReferenceID };
                treeNodes.Add(treeNode);
                treeNode.Expand();
            }

            // ReSharper disable once InvertIf
            if (treeNode.Text != viewModel.Caption)
            {
                isNewOrIsDirtyName = true;
                treeNode.Text = viewModel.Caption;
            }

            return treeNode;
        }

        private void ConvertLibraryDescendants(LibraryTreeNodeViewModel viewModel, TreeNodeCollection treeNodes)
        {
            var treeNodesToKeep = new HashSet<TreeNode>();

            bool mustSort = false;

            // Library Patches (Groupless)
            foreach (PatchTreeNodeViewModel patchViewModel in viewModel.PatchNodes)
            {
                TreeNode patchTreeNode = ConvertPatchNode(patchViewModel, treeNodes, out bool isNewOrIsDirtyName);
                treeNodesToKeep.Add(patchTreeNode);

                _libraryPatchTreeNodes.Add(patchTreeNode);

                mustSort |= isNewOrIsDirtyName;
            }

            // Library PatchGroups
            foreach (PatchGroupTreeNodeViewModel patchGroupViewModel in viewModel.PatchGroupNodes)
            {
                TreeNode patchGroupTreeNode = ConvertLibraryPatchGroupAndDescendants(
                    patchGroupViewModel,
                    treeNodes,
                    viewModel.LowerDocumentReferenceID,
                    out bool isNewOrIsDirtyName);

                treeNodesToKeep.Add(patchGroupTreeNode);

                _libraryPatchGroupTreeNodes.Add(patchGroupTreeNode);

                mustSort |= isNewOrIsDirtyName;
            }

            // Deletions
            IEnumerable<TreeNode> existingTreeNodes = treeNodes.Cast<TreeNode>();
            IEnumerable<TreeNode> treeNodesToDelete = existingTreeNodes.Except(treeNodesToKeep);
            foreach (TreeNode treeNodeToDelete in treeNodesToDelete.ToArray())
            {
                treeNodes.Remove(treeNodeToDelete);
            }

            // Sort
            // ReSharper disable once InvertIf
            if (mustSort)
            {
                SortTreeNodes(treeNodes);
            }
        }

        private TreeNode ConvertLibraryPatchGroupAndDescendants(
            PatchGroupTreeNodeViewModel viewModel,
            TreeNodeCollection treeNodes,
            int lowerDocumentReferenceID,
            out bool isNewOrIsDirtyName)
        {
            TreeNode treeNode = ConvertLibraryPatchGroup(viewModel, treeNodes, lowerDocumentReferenceID, out isNewOrIsDirtyName);

            ConvertLibraryPatches(viewModel.PatchNodes, treeNode.Nodes);

            return treeNode;
        }

        private TreeNode ConvertLibraryPatchGroup(
            PatchGroupTreeNodeViewModel viewModel,
            // ReSharper disable once SuggestBaseTypeForParameter
            TreeNodeCollection treeNodes,
            int lowerDocumentReferenceID,
            out bool isNewOrIsDirtyName)
        {
            string tag = FormatLibraryPatchGroupTag(lowerDocumentReferenceID, viewModel.CanonicalGroupName);

            TreeNode treeNode = treeNodes.Cast<TreeNode>()
                                         .Where(x => x.Tag is string)
                                         .Where(x => NameHelper.AreEqual((string)x.Tag, tag))
                                         .SingleOrDefault();
            if (treeNode == null)
            {
                isNewOrIsDirtyName = true;
                treeNode = new TreeNode { Tag = tag };
                treeNodes.Add(treeNode);
                treeNode.Expand();
            }

            isNewOrIsDirtyName = false;

            // ReSharper disable once InvertIf
            if (treeNode.Text != viewModel.Caption)
            {
                isNewOrIsDirtyName = true;
                treeNode.Text = viewModel.Caption;
            }

            return treeNode;
        }

        private void ConvertLibraryPatches(IList<PatchTreeNodeViewModel> viewModels, TreeNodeCollection treeNodes)
        {
            var treeNodesToKeep = new HashSet<TreeNode>();

            bool mustSort = false;

            foreach (PatchTreeNodeViewModel viewModel in viewModels)
            {
                TreeNode treeNode = ConvertPatchNode(viewModel, treeNodes, out bool isNewOrIsDirtyName);
                treeNodesToKeep.Add(treeNode);

                _libraryPatchTreeNodes.Add(treeNode);

                mustSort |= isNewOrIsDirtyName;
            }

            IEnumerable<TreeNode> existingTreeNodes = treeNodes.Cast<TreeNode>();
            IEnumerable<TreeNode> treeNodesToDelete = existingTreeNodes.Except(treeNodesToKeep);
            foreach (TreeNode treeNodeToDelete in treeNodesToDelete.ToArray())
            {
                treeNodes.Remove(treeNodeToDelete);
            }

            // Sort
            // ReSharper disable once InvertIf
            if (mustSort)
            {
                SortTreeNodes(treeNodes);
            }
        }

        private TreeNode ConvertPatchGroup(PatchGroupTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrIsDirtyName)
        {
            TreeNode treeNode = treeNodes.Cast<TreeNode>()
                                         .Where(x => x.Tag is string)
                                         .Where(x => NameHelper.AreEqual((string)x.Tag, viewModel.CanonicalGroupName))
                                         .SingleOrDefault();
            isNewOrIsDirtyName = false;
            if (treeNode == null)
            {
                isNewOrIsDirtyName = true;
                treeNode = new TreeNode { Tag = viewModel.CanonicalGroupName };
                treeNodes.Add(treeNode);
                treeNode.Expand();
            }

            // ReSharper disable once InvertIf
            if (treeNode.Text != viewModel.Caption)
            {
                isNewOrIsDirtyName = true;
                treeNode.Text = viewModel.Caption;
            }

            return treeNode;
        }

        private TreeNode ConvertPatchNode(PatchTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrIsDirtyName)
        {
            TreeNode treeNode = treeNodes.Cast<TreeNode>().Where(x => Equals(x.Tag, viewModel.ID)).SingleOrDefault();

            isNewOrIsDirtyName = false;
            if (treeNode == null)
            {
                isNewOrIsDirtyName = true;
                treeNode = new TreeNode { Tag = viewModel.ID };
                treeNodes.Add(treeNode);
            }

            if (treeNode.Text != viewModel.Name)
            {
                isNewOrIsDirtyName = true;
                treeNode.Text = viewModel.Name;
            }

            if (viewModel.HasLighterStyle)
            {
                treeNode.ForeColor = StyleHelper.LightGray;
            }

            return treeNode;
        }

        private void SortTreeNodes(TreeNodeCollection treeNodes)
        {
            TreeNode[] sortedTreeNodes = treeNodes.Cast<TreeNode>()
                                                  .OrderBy(x => x.Text)
                                                  .ToArray();
            treeNodes.Clear();
            treeNodes.AddRange(sortedTreeNodes);
        }

        private void SetSelectedNode()
        {
            switch (ViewModel.SelectedNodeType)
            {
                case DocumentTreeNodeTypeEnum.AudioFileOutputList:
                    treeView.SelectedNode = _audioFileOutputListTreeNode;
                    break;

                case DocumentTreeNodeTypeEnum.AudioOutput:
                    treeView.SelectedNode = _audioOutputNode;
                    break;

                case DocumentTreeNodeTypeEnum.Curves:
                    treeView.SelectedNode = _curvesTreeNode;
                    break;

                case DocumentTreeNodeTypeEnum.Libraries:
                    treeView.SelectedNode = _librariesTreeNode;
                    break;

                case DocumentTreeNodeTypeEnum.Library:
                    treeView.SelectedNode = _libraryTreeNodes.Where(x => (int)x.Tag == ViewModel.SelectedItemID).First();
                    break;

                case DocumentTreeNodeTypeEnum.LibraryPatch:
                    treeView.SelectedNode = _libraryPatchTreeNodes.Where(x => (int)x.Tag == ViewModel.SelectedItemID).First();
                    break;

                case DocumentTreeNodeTypeEnum.LibraryPatchGroup:
                    if (!ViewModel.SelectedPatchGroupLowerDocumentReferenceID.HasValue)
                    {
                        throw new NullException(() => ViewModel.SelectedPatchGroupLowerDocumentReferenceID);
                    }

                    string tag = FormatLibraryPatchGroupTag(ViewModel.SelectedPatchGroupLowerDocumentReferenceID.Value, ViewModel.SelectedCanonicalPatchGroup);
                    treeView.SelectedNode = _libraryPatchGroupTreeNodes.Where(x => NameHelper.AreEqual((string)x.Tag, tag)).First();
                    break;

                case DocumentTreeNodeTypeEnum.Scales:
                    treeView.SelectedNode = _scalesTreeNode;
                    break;

                case DocumentTreeNodeTypeEnum.Patch:
                    treeView.SelectedNode = _patchTreeNodes.Where(x => (int)x.Tag == ViewModel.SelectedItemID).First();
                    break;

                case DocumentTreeNodeTypeEnum.PatchGroup:
                    treeView.SelectedNode = _patchGroupTreeNodes.Where(x => NameHelper.AreEqual((string)x.Tag, ViewModel.SelectedCanonicalPatchGroup)).First();
                    break;
            }
        }

        // Events

        private void titleBarUserControl_AddClicked(object sender, EventArgs e) => AddRequested(this, EventArgs.Empty);
        private void titleBarUserControl_AddToInstrumentClicked(object sender, EventArgs e) => AddToInstrumentRequested(this, EventArgs.Empty);
        private void titleBarUserControl_CloseClicked(object sender, EventArgs e) => CloseRequested(this, EventArgs.Empty);
        private void titleBarUserControl_NewClicked(object sender, EventArgs e) => NewRequested(sender, EventArgs.Empty);
        private void titleBarUserControl_OpenClicked(object sender, EventArgs e) => OpenItemExternallyRequested(sender, EventArgs.Empty);
        private void titleBarUserControl_PlayClicked(object sender, EventArgs e) => PlayRequested(sender, EventArgs.Empty);
        private void titleBarUserControl_RefreshClicked(object sender, EventArgs e) => RefreshRequested(sender, EventArgs.Empty);
        private void titleBarUserControl_RemoveClicked(object sender, EventArgs e) => RemoveRequested(this, EventArgs.Empty);
        private void titleBarUserControl_SaveClicked(object sender, EventArgs e) => SaveRequested(sender, EventArgs.Empty);

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) => HandleNodeKeyEnterOrDoubleClick(e.Node);

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, Keys keyData)
        {
            // Use ProcessCmdKey,because OnKeyDown produces an annoying Ding sound.
            // every time you hit enter.

            // ReSharper disable once InvertIf
            if (keyData == Keys.Enter)
            {
                // ReSharper disable once InvertIf
                if (treeView.SelectedNode != null)
                {
                    HandleNodeKeyEnterOrDoubleClick(treeView.SelectedNode);
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool notByUser = e.Action == TreeViewAction.Unknown;
            if (notByUser)
            {
                return;
            }

            // NOTE: WinForms does not allow giving event handlers to specific nodes, so you need the if's.

            TreeNode node = e.Node;

            if (node == _audioFileOutputListTreeNode)
            {
                AudioFileOutputsNodeSelected(this, EventArgs.Empty);
            }

            if (node == _audioOutputNode)
            {
                AudioOutputNodeSelected(this, EventArgs.Empty);
            }

            if (node == _curvesTreeNode)
            {
                CurvesNodeSelected(this, EventArgs.Empty);
            }

            if (node == _librariesTreeNode)
            {
                LibrariesNodeSelected(this, EventArgs.Empty);
            }

            if (_patchGroupTreeNodes.Contains(node))
            {
                PatchGroupNodeSelected(this, new EventArgs<string>((string)node.Tag));
            }

            if (_patchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                PatchNodeSelected(this, new EventArgs<int>(id));
            }

            if (node == _scalesTreeNode)
            {
                ScalesNodeSelected(this, EventArgs.Empty);
            }

            if (_libraryTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                LibraryNodeSelected(this, new EventArgs<int>(id));
            }

            if (_libraryPatchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                LibraryPatchNodeSelected(this, new EventArgs<int>(id));
            }

            // ReSharper disable once InvertIf
            if (_libraryPatchGroupTreeNodes.Contains(node))
            {
                var e2 = ParseLibraryPatchGroupTag(node.Tag);
                LibraryPatchGroupNodeSelected(this, e2);
            }
        }

        private void treeView_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            if (_patchTreeNodes.Contains(e.Node))
            {
                _mouseHoverNode = e.Node;

                int patchID = (int)e.Node.Tag;
                PatchHovered.Invoke(sender, new EventArgs<int>(patchID));
            }
        }

        // Helpers

        private void HandleNodeKeyEnterOrDoubleClick(TreeNode node)
        {
            // NOTE: WinForms does not allow giving event handlers to specific nodes, so you need the if's.

            if (node == _audioFileOutputListTreeNode)
            {
                ShowAudioFileOutputsRequested(this, EventArgs.Empty);
            }

            if (node == _audioOutputNode)
            {
                ShowAudioOutputRequested(this, EventArgs.Empty);
            }

            if (node == _curvesTreeNode)
            {
                ShowCurvesRequested(this, EventArgs.Empty);
            }

            if (_patchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                ShowPatchDetailsRequested(this, new EventArgs<int>(id));
            }

            if (node == _scalesTreeNode)
            {
                ShowScalesRequested(this, EventArgs.Empty);
            }

            if (_libraryTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                ShowLibraryPropertiesRequested(this, new EventArgs<int>(id));
            }
        }

        private string FormatLibraryPatchGroupTag(int lowerDocumentReferenceID, string patchGroupNameCanonical)
        {
            return $"{lowerDocumentReferenceID}{_separator}{patchGroupNameCanonical}";
        }

        private LibraryPatchGroupEventArgs ParseLibraryPatchGroupTag(object tag)
        {
            if (!(tag is string tagString))
            {
                throw new UnexpectedTypeException(() => tag);
            }

            if (string.IsNullOrEmpty(tagString))
            {
                throw new NullOrEmptyException(() => tagString);
            }

            IList<string> values = tagString.Split(_separator);
            if (values.Count != 2)
            {
                throw new Exception($"{nameof(tagString)} does not contain a '{_separator}'.");
            }

            if (!int.TryParse(values[0], out int lowerDocumentReferenceID))
            {
                throw new Exception($"'{values[0]}' cannot be parsed to {nameof(Int32)} {nameof(lowerDocumentReferenceID)}.");
            }

            string patchGroup = values[1];

            return new LibraryPatchGroupEventArgs(lowerDocumentReferenceID, patchGroup);
        }
    }
}