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
        public event EventHandler OpenRequested;
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

        public DocumentTreeUserControl()
        {
            InitializeComponent();
            SetTitles();
        }

        // Gui

        private void SetTitles() => titleBarUserControl.Text = ResourceFormatter.DocumentTree;

        // Binding

        public new DocumentTreeViewModel ViewModel
        {
            get => (DocumentTreeViewModel)base.ViewModel;
            set => base.ViewModel = value;
        }

        protected override void ApplyViewModelToControls()
        {
            try
            {
                titleBarUserControl.PlayButtonVisible = ViewModel.CanPlay;
                titleBarUserControl.OpenButtonVisible = ViewModel.CanOpen;

                treeView.SuspendLayout();
                treeView.BeginUpdate();

                _patchGroupTreeNodes = new HashSet<TreeNode>();
                _patchTreeNodes = new HashSet<TreeNode>();
                _libraryTreeNodes = new HashSet<TreeNode>();
                _libraryPatchTreeNodes = new HashSet<TreeNode>();
                _libraryPatchGroupTreeNodes = new HashSet<TreeNode>();

                treeView.Nodes.Clear();

                if (ViewModel == null)
                {
                    treeView.ResumeLayout();
                    return;
                }

                AddTopLevelNodesAndDescendants(treeView.Nodes, ViewModel);
                SetSelectedNode();
            }
            finally
            {
                treeView.EndUpdate();
                treeView.ResumeLayout();
            }
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

        private void AddTopLevelNodesAndDescendants(TreeNodeCollection nodes, DocumentTreeViewModel documentTreeViewModel)
        {
            {
                // Patches
                var patchesTreeNode = new TreeNode(documentTreeViewModel.PatchesNode.Text);
                nodes.Add(patchesTreeNode);
                _patchGroupTreeNodes.Add(patchesTreeNode);

                // Patches (Groupless)
                foreach (PatchTreeNodeViewModel patchTreeNodeViewModel in documentTreeViewModel.PatchesNode.PatchNodes)
                {
                    TreeNode patchTreeNode = ConvertPatchNode(patchTreeNodeViewModel);
                    patchesTreeNode.Nodes.Add(patchTreeNode);
                    _patchTreeNodes.Add(patchTreeNode);
                }

                // PatchGroups
                foreach (PatchGroupTreeNodeViewModel patchGroupViewModel in documentTreeViewModel.PatchesNode.PatchGroupNodes)
                {
                    var patchGroupTreeNode = new TreeNode(patchGroupViewModel.Caption)
                    {
                        Tag = patchGroupViewModel.GroupName
                    };
                    patchesTreeNode.Nodes.Add(patchGroupTreeNode);
                    _patchGroupTreeNodes.Add(patchGroupTreeNode);

                    foreach (PatchTreeNodeViewModel patchTreeNodeViewModel in patchGroupViewModel.PatchNodes)
                    {
                        TreeNode patchTreeNode = ConvertPatchNode(patchTreeNodeViewModel);
                        patchGroupTreeNode.Nodes.Add(patchTreeNode);
                        _patchTreeNodes.Add(patchTreeNode);
                    }

                    patchGroupTreeNode.Expand();
                }

                patchesTreeNode.Expand();
            }

            // Other Nodes
            var samplesTreeNode = new TreeNode(documentTreeViewModel.SamplesNode.Text);
            nodes.Add(samplesTreeNode);
            _samplesTreeNode = samplesTreeNode;

            var curvesTreeNode = new TreeNode(documentTreeViewModel.CurvesNode.Text);
            nodes.Add(curvesTreeNode);
            _curvesTreeNode = curvesTreeNode;

            _scalesTreeNode = new TreeNode(documentTreeViewModel.ScalesNode.Text);
            nodes.Add(_scalesTreeNode);

            _audioOutputNode = new TreeNode(documentTreeViewModel.AudioOutputNode.Text);
            nodes.Add(_audioOutputNode);

            _audioFileOutputListTreeNode = new TreeNode(documentTreeViewModel.AudioFileOutputListNode.Text);
            nodes.Add(_audioFileOutputListTreeNode);

            _librariesTreeNode = new TreeNode(documentTreeViewModel.LibrariesNode.Text);
            treeView.Nodes.Add(_librariesTreeNode);

            // Library Nodes
            foreach (LibraryTreeNodeViewModel libraryViewModel in documentTreeViewModel.LibrariesNode.List)
            {
                var libraryTreeNode = new TreeNode(libraryViewModel.Caption)
                {
                    Tag = libraryViewModel.LowerDocumentReferenceID
                };
                _librariesTreeNode.Nodes.Add(libraryTreeNode);
                _libraryTreeNodes.Add(libraryTreeNode);

                // Library Patches (Groupless)
                foreach (PatchTreeNodeViewModel libraryPatchTreeViewModel in libraryViewModel.PatchNodes)
                {
                    TreeNode libraryPatchTreeNode = ConvertPatchNode(libraryPatchTreeViewModel);
                    libraryTreeNode.Nodes.Add(libraryPatchTreeNode);
                    _libraryPatchTreeNodes.Add(libraryPatchTreeNode);
                }

                // Library PatchGroups
                foreach (PatchGroupTreeNodeViewModel libraryPatchGroupViewModel in libraryViewModel.PatchGroupNodes)
                {
                    var libraryPatchGroupTreeNode = new TreeNode(libraryPatchGroupViewModel.Caption)
                    {
                        Tag = FormatLibraryPatchGroupTag(libraryViewModel.LowerDocumentReferenceID, libraryPatchGroupViewModel.GroupName)
                    };
                    libraryTreeNode.Nodes.Add(libraryPatchGroupTreeNode);
                    _libraryPatchGroupTreeNodes.Add(libraryPatchGroupTreeNode);

                    foreach (PatchTreeNodeViewModel libraryPatchTreeNodeViewModel in libraryPatchGroupViewModel.PatchNodes)
                    {
                        TreeNode libraryPatchTreeNode = ConvertPatchNode(libraryPatchTreeNodeViewModel);
                        libraryPatchGroupTreeNode.Nodes.Add(libraryPatchTreeNode);
                        _libraryPatchTreeNodes.Add(libraryPatchTreeNode);
                    }

                    libraryPatchGroupTreeNode.Expand();
                }
                
                libraryTreeNode.Expand();
            }

            _librariesTreeNode.Expand();
        }

        private TreeNode ConvertPatchNode(PatchTreeNodeViewModel patchViewModel)
        {
            var patchTreeNode = new TreeNode(patchViewModel.Name)
            {
                Tag = patchViewModel.ID
            };

            if (patchViewModel.HasLighterStyle)
            {
                patchTreeNode.ForeColor = StyleHelper.LightGray;
            }

            return patchTreeNode;
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

        private void titleBarUserControl_OpenClicked(object sender, EventArgs e) => OpenRequested?.Invoke(sender, EventArgs.Empty);

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