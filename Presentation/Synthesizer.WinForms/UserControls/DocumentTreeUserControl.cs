using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Framework.Collections;
using JJ.Framework.Common;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Exceptions.TypeChecking;
using JJ.Framework.Text;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using JJ.Presentation.Synthesizer.WinForms.EventArg;
using JJ.Presentation.Synthesizer.WinForms.Helpers;
using JJ.Presentation.Synthesizer.WinForms.UserControls.Bases;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Presentation.Synthesizer.WinForms.UserControls
{
	internal partial class DocumentTreeUserControl : UserControlBase
	{
		private const string LIBRARY_MIDI_NODE_TAG = "LibraryMidiNode";
		private const string LIBRARY_SCALES_NODE_TAG = "LibraryScalesNode";

		private static readonly string _separator = Guid.NewGuid().ToString();

		// Button Events
		public event EventHandler AddToInstrumentRequested;
		public event EventHandler CloseRequested;
		public event EventHandler NewRequested;
		public event EventHandler OpenItemExternallyRequested;
		public event EventHandler<EventArgs<int>> PatchHovered;
		public event EventHandler PlayRequested;
		public event EventHandler RefreshRequested;
		public event EventHandler DeleteRequested;
		public event EventHandler RedoRequested;
		public event EventHandler SaveRequested;
		public event EventHandler UndoRequested;

		// Show Events
		public event EventHandler ShowAudioOutputRequested;
		public event EventHandler ShowAudioFileOutputsRequested;
		public event EventHandler<EventArgs<int>> ShowLibraryRequested;
		public event EventHandler<EventArgs<int>> ShowPatchRequested;
		public event EventHandler<EventArgs<int>> ShowMidiMappingRequested;
		public event EventHandler ShowScalesRequested;

		// Selected Events
		public event EventHandler AudioOutputNodeSelected;
		public event EventHandler AudioFileOutputsNodeSelected;
		public event EventHandler MidiNodeSelected;
		public event EventHandler<EventArgs<int>> MidiMappingNodeSelected;
		public event EventHandler LibrariesNodeSelected;
		public event EventHandler<EventArgs<int>> LibraryMidiNodeSelected;
		public event EventHandler<EventArgs<int>> LibraryMidiMappingNodeSelected;
		public event EventHandler<EventArgs<int>> LibraryNodeSelected;
		public event EventHandler<LibraryPatchGroupEventArgs> LibraryPatchGroupNodeSelected;
		public event EventHandler<EventArgs<int>> LibraryPatchNodeSelected;
		public event EventHandler<EventArgs<int>> LibraryScaleNodeSelected;
		public event EventHandler<EventArgs<int>> LibraryScalesNodeSelected;
		public event EventHandler<EventArgs<string>> PatchGroupNodeSelected;
		public event EventHandler<EventArgs<int>> PatchNodeSelected;
		public event EventHandler ScalesNodeSelected;

		// TreeNodes
		private TreeNode _audioFileOutputListTreeNode;
		private TreeNode _audioOutputNode;
		private TreeNode _librariesTreeNode;
		private HashSet<TreeNode> _libraryMidiMappingTreeNodes;
		private HashSet<TreeNode> _libraryMidiTreeNodes;
		private HashSet<TreeNode> _libraryPatchGroupTreeNodes;
		private HashSet<TreeNode> _libraryPatchTreeNodes;
		private HashSet<TreeNode> _libraryScaleTreeNodes;
		private HashSet<TreeNode> _libraryScalesTreeNodes;
		private HashSet<TreeNode> _libraryTreeNodes;
		private TreeNode _midiTreeNode;
		private HashSet<TreeNode> _midiMappingTreeNodes;
		private TreeNode _mouseHoverNode;
		private TreeNode _patchesTreeNode;
		private HashSet<TreeNode> _patchGroupTreeNodes;
		private HashSet<TreeNode> _patchTreeNodes;
		private TreeNode _scalesTreeNode;

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
			titleBarUserControl.AddToInstrumentButtonVisible = ViewModel.CanAddToInstrument;
			titleBarUserControl.NewButtonVisible = ViewModel.CanCreate;
			titleBarUserControl.ExpandButtonVisible = ViewModel.CanOpenExternally;
			titleBarUserControl.PlayButtonVisible = ViewModel.CanPlay;
			titleBarUserControl.DeleteButtonVisible = ViewModel.CanDelete;

			_libraryMidiTreeNodes = new HashSet<TreeNode>();
			_libraryMidiMappingTreeNodes = new HashSet<TreeNode>();
			_libraryPatchTreeNodes = new HashSet<TreeNode>();
			_libraryPatchGroupTreeNodes = new HashSet<TreeNode>();
			_libraryScaleTreeNodes = new HashSet<TreeNode>();
			_libraryScalesTreeNodes = new HashSet<TreeNode>();
			_libraryTreeNodes = new HashSet<TreeNode>();
			_midiMappingTreeNodes = new HashSet<TreeNode>();
			_patchGroupTreeNodes = new HashSet<TreeNode> { _patchesTreeNode };
			_patchTreeNodes = new HashSet<TreeNode>();

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
			//   (the TreeView.ShowNodeToolTips only controls whether the TreeNode.ToolTipText is used.
			//	  I know: This makes ShowNodeToolTips a really bad property name.)
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

			_midiTreeNode = new TreeNode();
			treeView.Nodes.Add(_midiTreeNode);
			_midiTreeNode.Expand();

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
			// References comparisons of strings is faster.
			// False negatives are rare and not a problem here.

			if (_patchesTreeNode.Text != viewModel.PatchesNode.Text)
			{
				_patchesTreeNode.Text = viewModel.PatchesNode.Text;
			}
			ConvertPatchesDescendants(viewModel.PatchesNode, _patchesTreeNode);

			if (_midiTreeNode.Text != viewModel.MidiNode.Text)
			{
				_midiTreeNode.Text = viewModel.MidiNode.Text;
			}
			ConvertMidiChildren(viewModel.MidiNode.List, _midiTreeNode.Nodes);

			_midiMappingTreeNodes.AddRange(_midiTreeNode.Nodes.Cast<TreeNode>());

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

		private void ConvertMidiChildren(IList<IDAndName> viewModels, TreeNodeCollection treeNodes)
		{
			var treeNodesToKeep = new HashSet<TreeNode>();

			bool mustSort = false;

			foreach (IDAndName viewModel in viewModels)
			{
				TreeNode treeNode = ConvertMidiMapping(viewModel, treeNodes, out bool isNewOrIsDirtyName);
				treeNodesToKeep.Add(treeNode);

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

		private TreeNode ConvertMidiMapping(IDAndName viewModel, TreeNodeCollection treeNodes, out bool isNewOrIsDirtyName)
		{
			TreeNode treeNode = treeNodes.Cast<TreeNode>().SingleOrDefault(x => Equals(x.Tag, viewModel.ID));

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

			return treeNode;
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
				TreeNode patchGroupTreeNode = Convert_LibraryPatchGroup_AndDescendants(
					patchGroupViewModel,
					treeNodes,
					viewModel.LowerDocumentReferenceID,
					out bool isNewOrIsDirtyName);

				treeNodesToKeep.Add(patchGroupTreeNode);

				_libraryPatchGroupTreeNodes.Add(patchGroupTreeNode);

				mustSort |= isNewOrIsDirtyName;
			}

			// Sort
			// ReSharper disable once InvertIf
			if (mustSort)
			{
				SortTreeNodes(treeNodes);
			}

			// MidiMappings
			TreeNode libraryMidiTreeNode = TryConvert_LibraryMidi_WithDescendants(viewModel.MidiNode, treeNodes);
			treeNodesToKeep.Add(libraryMidiTreeNode);

			// Scales
			TreeNode scalesTreeNode = TryConvert_LibraryScalesNode_WithDescendants(viewModel.ScalesNode, treeNodes);
			treeNodesToKeep.Add(scalesTreeNode);

			// Deletions
			IEnumerable<TreeNode> existingTreeNodes = treeNodes.Cast<TreeNode>();
			IEnumerable<TreeNode> treeNodesToDelete = existingTreeNodes.Except(treeNodesToKeep);
			foreach (TreeNode treeNodeToDelete in treeNodesToDelete.ToArray())
			{
				treeNodes.Remove(treeNodeToDelete);
			}
		}

		private TreeNode TryConvert_LibraryScalesNode_WithDescendants(SimpleTreeNodeViewModel viewModel, TreeNodeCollection treeNodes)
		{
			if (!viewModel.Visible)
			{
				return null;
			}

			TreeNode scalesTreeNode = treeNodes.Cast<TreeNode>().SingleOrDefault(x => Equals(x.Tag, LIBRARY_SCALES_NODE_TAG));
			if (scalesTreeNode == null)
			{
				scalesTreeNode = new TreeNode
				{
					Tag = LIBRARY_SCALES_NODE_TAG
				};
				treeNodes.Add(scalesTreeNode);
			}

			if (scalesTreeNode.Text != viewModel.Text)
			{
				scalesTreeNode.Text = viewModel.Text;
			}

			_libraryScalesTreeNodes.Add(scalesTreeNode);

			ConvertScales(viewModel.List, scalesTreeNode.Nodes);

			_libraryScaleTreeNodes.AddRange(scalesTreeNode.Nodes.Cast<TreeNode>());

			return scalesTreeNode;
		}

		private TreeNode TryConvert_LibraryMidi_WithDescendants(SimpleTreeNodeViewModel viewModel, TreeNodeCollection treeNodes)
		{
			if (!viewModel.Visible)
			{
				return null;
			}

			TreeNode midiTreeNode = treeNodes.Cast<TreeNode>().SingleOrDefault(x => Equals(x.Tag, LIBRARY_MIDI_NODE_TAG));
			if (midiTreeNode == null)
			{
				midiTreeNode = new TreeNode
				{
					Tag = LIBRARY_MIDI_NODE_TAG
				};
				treeNodes.Add(midiTreeNode);
			}

			if (midiTreeNode.Text != viewModel.Text)
			{
				midiTreeNode.Text = viewModel.Text;
			}

			_libraryMidiTreeNodes.Add(midiTreeNode);

			ConvertMidiChildren(viewModel.List, midiTreeNode.Nodes);

			_libraryMidiMappingTreeNodes.AddRange(midiTreeNode.Nodes.Cast<TreeNode>());

			return midiTreeNode;
		}

		private TreeNode Convert_LibraryPatchGroup_AndDescendants(
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
			                             .SingleOrDefault(x => NameHelper.AreEqual((string)x.Tag, viewModel.CanonicalGroupName));
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
			TreeNode treeNode = treeNodes.Cast<TreeNode>().SingleOrDefault(x => Equals(x.Tag, viewModel.ID));

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

		private void ConvertScales(IList<IDAndName> viewModels, TreeNodeCollection treeNodes)
		{
			var treeNodesToKeep = new HashSet<TreeNode>();

			bool mustSort = false;

			foreach (IDAndName viewModel in viewModels)
			{
				TreeNode treeNode = ConvertScale(viewModel, treeNodes, out bool isNewOrIsDirtyName);
				treeNodesToKeep.Add(treeNode);

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

		private TreeNode ConvertScale(IDAndName viewModel, TreeNodeCollection treeNodes, out bool isNewOrIsDirtyName)
		{
			TreeNode treeNode = treeNodes.Cast<TreeNode>().SingleOrDefault(x => Equals(x.Tag, viewModel.ID));

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

		private void titleBarUserControl_AddToInstrumentClicked(object sender, EventArgs e) => AddToInstrumentRequested(this, EventArgs.Empty);
		private void titleBarUserControl_CloseClicked(object sender, EventArgs e) => CloseRequested(this, EventArgs.Empty);
		private void titleBarUserControl_NewClicked(object sender, EventArgs e) => NewRequested(sender, EventArgs.Empty);
		private void titleBarUserControl_OpenClicked(object sender, EventArgs e) => OpenItemExternallyRequested(sender, EventArgs.Empty);
		private void titleBarUserControl_PlayClicked(object sender, EventArgs e) => PlayRequested(sender, EventArgs.Empty);
		private void titleBarUserControl_RefreshClicked(object sender, EventArgs e) => RefreshRequested(sender, EventArgs.Empty);
		private void titleBarUserControl_RedoClicked(object sender, EventArgs e) => RedoRequested(sender, EventArgs.Empty);
		private void titleBarUserControl_DeleteClicked(object sender, EventArgs e) => DeleteRequested(this, EventArgs.Empty);
		private void titleBarUserControl_SaveClicked(object sender, EventArgs e) => SaveRequested(sender, EventArgs.Empty);
		private void titleBarUserControl_UndoClicked(object sender, EventArgs e) => UndoRequested(sender, EventArgs.Empty);

		private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) => HandleNodeKeyEnterOrDoubleClick(e.Node);

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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

			if (node == _librariesTreeNode)
			{
				LibrariesNodeSelected(this, EventArgs.Empty);
			}

			if (_libraryTreeNodes.Contains(node))
			{
				LibraryNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (_libraryMidiTreeNodes.Contains(node))
			{
				LibraryMidiNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (_libraryMidiMappingTreeNodes.Contains(node))
			{
				LibraryMidiMappingNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (_libraryPatchTreeNodes.Contains(node))
			{
				LibraryPatchNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (_libraryPatchGroupTreeNodes.Contains(node))
			{
				LibraryPatchGroupEventArgs e2 = ParseLibraryPatchGroupTag(node.Tag);
				LibraryPatchGroupNodeSelected(this, e2);
			}

			if (_libraryScalesTreeNodes.Contains(node))
			{
				LibraryScalesNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (_libraryScaleTreeNodes.Contains(node))
			{
				LibraryScaleNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (node == _midiTreeNode)
			{
				MidiNodeSelected(this, EventArgs.Empty);
			}

			if (_midiMappingTreeNodes.Contains(node))
			{
				MidiMappingNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (_patchGroupTreeNodes.Contains(node))
			{
				PatchGroupNodeSelected(this, new EventArgs<string>((string)node.Tag));
			}

			if (_patchTreeNodes.Contains(node))
			{
				PatchNodeSelected(this, new EventArgs<int>((int)node.Tag));
			}

			if (node == _scalesTreeNode)
			{
				ScalesNodeSelected(this, EventArgs.Empty);
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

			if (_libraryTreeNodes.Contains(node))
			{
				int id = (int)node.Tag;
				ShowLibraryRequested(this, new EventArgs<int>(id));
			}

			if (_libraryPatchTreeNodes.Contains(node))
			{
				NewRequested(this, EventArgs.Empty);
			}

			if (_patchTreeNodes.Contains(node))
			{
				int id = (int)node.Tag;
				ShowPatchRequested(this, new EventArgs<int>(id));
			}

			if (_midiMappingTreeNodes.Contains(node))
			{
				int id = (int)node.Tag;
				ShowMidiMappingRequested(this, new EventArgs<int>(id));
			}

			if (node == _scalesTreeNode)
			{
				ShowScalesRequested(this, EventArgs.Empty);
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
				throw new IsNotTypeException<string>(() => tag);
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