using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
    internal partial class DocumentTreeUserControl : UserControlBase
    {
        public event EventHandler CloseRequested;

        public event EventHandler<EventArgs<string>> ShowPatchGridRequested;
        public event EventHandler<EventArgs<int>> ShowPatchDetailsRequested;
        public event EventHandler<EventArgs<int>> ShowLibraryPropertiesRequested;
        public event EventHandler<EventArgs<int>> ShowLibraryPatchPropertiesRequested;
        public event EventHandler ShowCurvesRequested;
        public event EventHandler ShowSamplesRequested;
        public event EventHandler ShowAudioOutputRequested;
        public event EventHandler ShowAudioFileOutputsRequested;
        public event EventHandler ShowScalesRequested;
        public event EventHandler ShowLibrariesRequested;

        private HashSet<TreeNode> _patchesTreeNodes;
        private HashSet<TreeNode> _patchTreeNodes;
        private HashSet<TreeNode> _libraryTreeNodes;
        private HashSet<TreeNode> _libraryPatchTreeNodes;
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

        private void SetTitles()
        {
            titleBarUserControl.Text = ResourceFormatter.DocumentTree;
        }

        // Binding

        public new DocumentTreeViewModel ViewModel
        {
            get { return (DocumentTreeViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        protected override void ApplyViewModelToControls()
        {
            try
            {
                treeView.SuspendLayout();
                treeView.BeginUpdate();

                _patchesTreeNodes = new HashSet<TreeNode>();
                _patchTreeNodes = new HashSet<TreeNode>();
                _libraryTreeNodes = new HashSet<TreeNode>();
                _libraryPatchTreeNodes = new HashSet<TreeNode>();

                treeView.Nodes.Clear();

                if (ViewModel == null)
                {
                    treeView.ResumeLayout();
                    return;
                }

                AddTopLevelNodesAndDescendants(treeView.Nodes, ViewModel);
            }
            finally
            {
                treeView.EndUpdate();
                treeView.ResumeLayout();
            }
        }

        private void AddTopLevelNodesAndDescendants(TreeNodeCollection nodes, DocumentTreeViewModel documentTreeViewModel)
        {
            {
                // Patches
                var patchesTreeNode = new TreeNode(documentTreeViewModel.PatchesNode.Text);
                nodes.Add(patchesTreeNode);
                _patchesTreeNodes.Add(patchesTreeNode);

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
                    _patchesTreeNodes.Add(patchGroupTreeNode);

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

                // Patches (Groupless)
                foreach (PatchTreeNodeViewModel libraryPatchTreeViewModel in libraryViewModel.PatchNodes)
                {
                    TreeNode libraryPatchTreeNode = ConvertPatchNode(libraryPatchTreeViewModel);
                    libraryTreeNode.Nodes.Add(libraryPatchTreeNode);
                    _libraryPatchTreeNodes.Add(libraryPatchTreeNode);
                }

                // PatchGroups
                foreach (PatchGroupTreeNodeViewModel libraryPatchGroupViewModel in libraryViewModel.PatchGroupNodes)
                {
                    var libraryPatchGroupTreeNode = new TreeNode(libraryPatchGroupViewModel.Caption)
                    {
                        Tag = libraryPatchGroupViewModel.GroupName
                    };
                    libraryTreeNode.Nodes.Add(libraryPatchGroupTreeNode);

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

        // Actions

        private void Close()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        // Events

        private void titleBarUserControl_CloseClicked(object sender, EventArgs e) => Close();

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            HandleNodeKeyEnterOrDoubleClick(e.Node);
        }

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

            if (_patchesTreeNodes.Contains(node))
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
        }
    }
}