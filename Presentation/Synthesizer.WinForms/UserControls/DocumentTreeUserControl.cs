using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
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

        public event EventHandler SaveRequested;
        public event EventHandler RefreshRequested;
        public event EventHandler PlayRequested;
        public event EventHandler OpenItemExternallyRequested;
        public event EventHandler CloseRequested;
        public event EventHandler<EventArgs<string>> ShowPatchGridRequested;
        public event EventHandler<EventArgs<int>> ShowPatchDetailsRequested;
        public event EventHandler<EventArgs<int>> ShowLibraryPropertiesRequested;
        public event EventHandler<EventArgs<int>> ShowLibraryPatchPropertiesRequested;
        public event EventHandler<LibraryPatchGroupEventArgs> ShowLibraryPatchGridRequested;
        public event EventHandler ShowCurvesRequested;
        public event EventHandler ShowSamplesRequested;
        public event EventHandler ShowAudioOutputRequested;
        public event EventHandler ShowAudioFileOutputsRequested;
        public event EventHandler ShowScalesRequested;
        public event EventHandler ShowLibrariesRequested;
        public event EventHandler<EventArgs<string>> PatchGroupNodeSelected;
        public event EventHandler<EventArgs<int>> PatchNodeSelected;
        public event EventHandler<EventArgs<int>> LibraryNodeSelected;
        public event EventHandler<LibraryPatchGroupEventArgs> LibraryPatchGroupNodeSelected;
        public event EventHandler<EventArgs<int>> LibraryPatchNodeSelected;
        public event EventHandler CurvesNodeSelected;
        public event EventHandler SamplesNodeSelected;
        public event EventHandler AudioOutputNodeSelected;
        public event EventHandler AudioFileOutputsNodeSelected;
        public event EventHandler ScalesNodeSelected;
        public event EventHandler LibrariesNodeSelected;

        private HashSet<TreeNode> _patchGroupTreeNodes;
        private HashSet<TreeNode> _patchTreeNodes;
        private HashSet<TreeNode> _libraryTreeNodes;
        private HashSet<TreeNode> _libraryPatchTreeNodes;
        private HashSet<TreeNode> _libraryPatchGroupTreeNodes;
        private TreeNode _samplesTreeNode;
        private TreeNode _curvesTreeNode;
        private TreeNode _scalesTreeNode;
        private TreeNode _audioOutputNode;
        private TreeNode _audioFileOutputListTreeNode;
        private TreeNode _librariesTreeNode;
        private TreeNode _patchesTreeNode;

        public DocumentTreeUserControl()
        {
            InitializeComponent();
            SetTitles();
            ApplyStyling();
            AddInvariantNodes();
        }

        // Gui

        private void SetTitles() => titleBarUserControl.Text = ResourceFormatter.DocumentTree;

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
            titleBarUserControl.PlayButtonVisible = ViewModel.CanPlay;
            titleBarUserControl.OpenButtonVisible = ViewModel.CanOpenExternally;

            _patchGroupTreeNodes = new HashSet<TreeNode>();
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
        }

        private void AddInvariantNodes()
        {
            _patchesTreeNode = new TreeNode();
            treeView.Nodes.Add(_patchesTreeNode);

            _samplesTreeNode = new TreeNode();
            treeView.Nodes.Add(_samplesTreeNode);

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
        }

        private void ConvertNodes(DocumentTreeViewModel viewModel)
        {
            if (_samplesTreeNode.Text != viewModel.SamplesNode.Text)
            {
                _samplesTreeNode.Text = viewModel.SamplesNode.Text;
            }

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
            //_patchesTreeNode.Expand();

            if (_librariesTreeNode.Text != viewModel.LibrariesNode.Text)
            {
                _librariesTreeNode.Text = viewModel.LibrariesNode.Text;
            }
            ConvertLibrariesDescendants(viewModel.LibrariesNode.List, _librariesTreeNode.Nodes);
            //_librariesTreeNode.Expand();
        }

        private void ConvertPatchesDescendants(PatchesTreeNodeViewModel viewModel, TreeNode treeNode)
        {
            var treeNodesToKeep = new HashSet<TreeNode>();

            bool mustSort = false;

            // Groupless
            foreach (PatchTreeNodeViewModel patchViewModel in viewModel.PatchNodes)
            {
                TreeNode patchTreeNode = ConvertPatchNode(patchViewModel, treeNode.Nodes, out bool isNewOrNameIsDirty);
                _patchTreeNodes.Add(patchTreeNode);
                treeNodesToKeep.Add(patchTreeNode);

                mustSort |= isNewOrNameIsDirty;
            }

            // PatchGroups
            foreach (PatchGroupTreeNodeViewModel patchGroupViewModel in viewModel.PatchGroupNodes)
            {
                TreeNode patchGroupTreeNode = ConvertPatchGroupAndDescendants(patchGroupViewModel, treeNode.Nodes, out bool isNewOrNameIsDirty);
                _patchGroupTreeNodes.Add(patchGroupTreeNode);
                treeNodesToKeep.Add(patchGroupTreeNode);

                //patchGroupTreeNode.Expand();

                mustSort |= isNewOrNameIsDirty;
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
            out bool isNewOrNameIsDirty)
        {
            TreeNode patchGroupTreeNode = ConvertPatchGroup(patchGroupViewModel, patchesTreeNodes, out isNewOrNameIsDirty);

            ConvertPatches(patchGroupViewModel.PatchNodes, patchGroupTreeNode.Nodes);

            return patchGroupTreeNode;
        }

        private void ConvertPatches(IList<PatchTreeNodeViewModel> viewModels, TreeNodeCollection treeNodes)
        {
            var treeNodesToKeep = new HashSet<TreeNode>();

            bool mustSort = false;

            foreach (PatchTreeNodeViewModel viewModel in viewModels)
            {
                TreeNode treeNode = ConvertPatchNode(viewModel, treeNodes, out bool isNewOrNameIsDirty);
                treeNodesToKeep.Add(treeNode);

                _patchTreeNodes.Add(treeNode);

                mustSort |= isNewOrNameIsDirty;
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
                TreeNode treeNode = ConvertLibraryAndDescendants(viewModel, treeNodes, out bool isNewOrNameIsDirty);
                treeNodesToKeep.Add(treeNode);

                _libraryTreeNodes.Add(treeNode);

                mustSort |= isNewOrNameIsDirty;
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

        private TreeNode ConvertLibraryAndDescendants(LibraryTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrNameIsDirty)
        {
            TreeNode libraryTreeNode = ConvertLibrary(viewModel, treeNodes, out isNewOrNameIsDirty);

            ConvertLibraryDescendants(viewModel, libraryTreeNode.Nodes);

            //libraryTreeNode.Expand();

            return libraryTreeNode;
        }

        private static TreeNode ConvertLibrary(LibraryTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrNameIsDirty)
        {
            TreeNode treeNode = treeNodes.Cast<TreeNode>()
                                         .Where(x => (int)x.Tag == viewModel.LowerDocumentReferenceID)
                                         .SingleOrDefault();
            isNewOrNameIsDirty = false;
            if (treeNode == null)
            {
                isNewOrNameIsDirty = true;
                treeNode = new TreeNode { Tag = viewModel.LowerDocumentReferenceID };
                treeNodes.Add(treeNode);
            }

            if (treeNode.Text != viewModel.Caption)
            {
                isNewOrNameIsDirty = true;
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
                TreeNode patchTreeNode = ConvertPatchNode(patchViewModel, treeNodes, out bool isNewOrNameIsDirty);
                treeNodesToKeep.Add(patchTreeNode);

                _libraryPatchTreeNodes.Add(patchTreeNode);

                mustSort |= isNewOrNameIsDirty;
            }

            // Library PatchGroups
            foreach (PatchGroupTreeNodeViewModel patchGroupViewModel in viewModel.PatchGroupNodes)
            {
                TreeNode patchGroupTreeNode = ConvertLibraryPatchGroupAndDescendants(
                    patchGroupViewModel,
                    treeNodes,
                    viewModel.LowerDocumentReferenceID,
                    out bool isNewOrNameIsDirty);

                treeNodesToKeep.Add(patchGroupTreeNode);

                _libraryPatchGroupTreeNodes.Add(patchGroupTreeNode);

                //patchGroupTreeNode.Expand();

                mustSort |= isNewOrNameIsDirty;
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
            out bool isNewOrNameIsDirty)
        {
            TreeNode treeNode = ConvertLibraryPatchGroup(viewModel, treeNodes, lowerDocumentReferenceID, out isNewOrNameIsDirty);

            ConvertLibraryPatches(viewModel.PatchNodes, treeNode.Nodes);

            return treeNode;
        }

        private TreeNode ConvertLibraryPatchGroup(
            PatchGroupTreeNodeViewModel viewModel,
            TreeNodeCollection treeNodes,
            int lowerDocumentReferenceID,
            out bool isNewOrNameIsDirty)
        {
            string tag = FormatLibraryPatchGroupTag(lowerDocumentReferenceID, viewModel.CanonicalGroupName);

            TreeNode treeNode = treeNodes.Cast<TreeNode>()
                                         .Where(x => string.Equals(x.Tag, tag))
                                         .SingleOrDefault();
            isNewOrNameIsDirty = false;
            if (treeNode == null)
            {
                isNewOrNameIsDirty = true;
                treeNode = new TreeNode { Tag = tag };
                treeNodes.Add(treeNode);
            }

            if (treeNode.Text != viewModel.Caption)
            {
                isNewOrNameIsDirty = true;
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
                TreeNode treeNode = ConvertPatchNode(viewModel, treeNodes, out bool isNewOrNameIsDirty);
                treeNodesToKeep.Add(treeNode);

                _libraryPatchTreeNodes.Add(treeNode);

                mustSort |= isNewOrNameIsDirty;
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

        private TreeNode ConvertPatchGroup(PatchGroupTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrNameIsDirty)
        {
            TreeNode treeNode = treeNodes.Cast<TreeNode>()
                                         .Where(x => string.Equals(x.Tag, viewModel.CanonicalGroupName))
                                         .SingleOrDefault();
            isNewOrNameIsDirty = false;
            if (treeNode == null)
            {
                isNewOrNameIsDirty = true;
                treeNode = new TreeNode { Tag = viewModel.CanonicalGroupName };
                treeNodes.Add(treeNode);
            }

            if (treeNode.Text != viewModel.Caption)
            {
                isNewOrNameIsDirty = true;
                treeNode.Text = viewModel.Caption;
            }

            return treeNode;
        }

        private TreeNode ConvertPatchNode(PatchTreeNodeViewModel viewModel, TreeNodeCollection treeNodes, out bool isNewOrNameIsDirty)
        {
            TreeNode treeNode = treeNodes.Cast<TreeNode>().Where(x => Equals(x.Tag, viewModel.ID)).SingleOrDefault();

            isNewOrNameIsDirty = false;
            if (treeNode == null)
            {
                isNewOrNameIsDirty = true;
                treeNode = new TreeNode { Tag = viewModel.ID };
                treeNodes.Add(treeNode);
            }

            if (treeNode.Text != viewModel.Name)
            {
                isNewOrNameIsDirty = true;
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

                    string tag = FormatLibraryPatchGroupTag(ViewModel.SelectedPatchGroupLowerDocumentReferenceID.Value, ViewModel.SelectedPatchGroup);
                    treeView.SelectedNode = _libraryPatchGroupTreeNodes.Where(x => string.Equals((string)x.Tag, tag)).First();
                    break;

                case DocumentTreeNodeTypeEnum.Samples:
                    treeView.SelectedNode = _samplesTreeNode;
                    break;

                case DocumentTreeNodeTypeEnum.Scales:
                    treeView.SelectedNode = _scalesTreeNode;
                    break;

                case DocumentTreeNodeTypeEnum.Patch:
                    treeView.SelectedNode = _patchTreeNodes.Where(x => (int)x.Tag == ViewModel.SelectedItemID).First();
                    break;

                case DocumentTreeNodeTypeEnum.PatchGroup:
                    treeView.SelectedNode = _patchGroupTreeNodes.Where(x => string.Equals((string)x.Tag, ViewModel.SelectedPatchGroup)).First();
                    break;
            }
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e) => CloseRequested?.Invoke(this, EventArgs.Empty);

        private void titleBarUserControl_SaveClicked(object sender, EventArgs e) => SaveRequested?.Invoke(sender, EventArgs.Empty);

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

        private void titleBarUserControl_OpenClicked(object sender, EventArgs e) => OpenItemExternallyRequested?.Invoke(sender, EventArgs.Empty);

        private void titleBarUserControl_PlayClicked(object sender, EventArgs e) => PlayRequested?.Invoke(sender, EventArgs.Empty);

        private void titleBarUserControl_RefreshClicked(object sender, EventArgs e) => RefreshRequested?.Invoke(sender, EventArgs.Empty);

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
                AudioFileOutputsNodeSelected?.Invoke(this, EventArgs.Empty);
            }

            if (node == _audioOutputNode)
            {
                AudioOutputNodeSelected?.Invoke(this, EventArgs.Empty);
            }

            if (node == _curvesTreeNode)
            {
                CurvesNodeSelected?.Invoke(this, EventArgs.Empty);
            }

            if (node == _librariesTreeNode)
            {
                LibrariesNodeSelected?.Invoke(this, EventArgs.Empty);
            }

            if (_patchGroupTreeNodes.Contains(node))
            {
                PatchGroupNodeSelected?.Invoke(this, new EventArgs<string>((string)node.Tag));
            }

            if (_patchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                PatchNodeSelected?.Invoke(this, new EventArgs<int>(id));
            }

            if (node == _samplesTreeNode)
            {
                SamplesNodeSelected?.Invoke(this, EventArgs.Empty);
            }

            if (node == _scalesTreeNode)
            {
                ScalesNodeSelected?.Invoke(this, EventArgs.Empty);
            }

            if (_libraryTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                LibraryNodeSelected?.Invoke(this, new EventArgs<int>(id));
            }

            if (_libraryPatchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                LibraryPatchNodeSelected?.Invoke(this, new EventArgs<int>(id));
            }

            // ReSharper disable once InvertIf
            if (_libraryPatchGroupTreeNodes.Contains(node))
            {
                var e2 = ParseLibraryPatchGroupTag(node.Tag);
                LibraryPatchGroupNodeSelected?.Invoke(this, e2);
            }
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

            if (node == _curvesTreeNode)
            {
                ShowCurvesRequested?.Invoke(this, EventArgs.Empty);
            }

            if (node == _librariesTreeNode)
            {
                ShowLibrariesRequested?.Invoke(this, EventArgs.Empty);
            }

            if (_patchGroupTreeNodes.Contains(node))
            {
                ShowPatchGridRequested?.Invoke(this, new EventArgs<string>((string)node.Tag));
            }

            if (_patchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                ShowPatchDetailsRequested?.Invoke(this, new EventArgs<int>(id));
            }

            if (node == _samplesTreeNode)
            {
                ShowSamplesRequested?.Invoke(this, EventArgs.Empty);
            }

            if (node == _scalesTreeNode)
            {
                ShowScalesRequested?.Invoke(this, EventArgs.Empty);
            }

            if (_libraryTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                ShowLibraryPropertiesRequested?.Invoke(this, new EventArgs<int>(id));
            }

            if (_libraryPatchTreeNodes.Contains(node))
            {
                int id = (int)node.Tag;
                ShowLibraryPatchPropertiesRequested?.Invoke(this, new EventArgs<int>(id));
            }

            // ReSharper disable once InvertIf
            if (_libraryPatchGroupTreeNodes.Contains(node))
            {
                LibraryPatchGroupEventArgs e2 = ParseLibraryPatchGroupTag(node.Tag);
                ShowLibraryPatchGridRequested?.Invoke(this, e2);
            }
        }

        private string FormatLibraryPatchGroupTag(int lowerDocumentReferenceID, string patchGroupID)
        {
            return $"{lowerDocumentReferenceID}{_separator}{patchGroupID}";
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