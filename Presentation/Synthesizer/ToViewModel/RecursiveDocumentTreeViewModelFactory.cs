using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions.Basic;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
	internal class RecursiveDocumentTreeViewModelFactory
	{
		public DocumentTreeViewModel CreateEmptyDocumentTreeViewModel()
		{
			var viewModel = new DocumentTreeViewModel
			{
				PatchesNode = new PatchesTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Patches, count: 0),
					PatchNodes = new List<PatchTreeNodeViewModel>(),
					PatchGroupNodes = new List<PatchGroupTreeNodeViewModel>()
				},
				MidiNode = new EntityWithListTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Midi, count: 0),
					Visible = true,
					List = new List<IDAndName>()
				},
				ScalesNode = new EntityWithListTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Scales, count: 0),
					Visible = true,
					List = new List<IDAndName>()
				},
				AudioOutputNode = CreateTreeLeafViewModel(ResourceFormatter.AudioOutput),
				AudioFileOutputListNode = CreateTreeLeafViewModel(ResourceFormatter.AudioFileOutput, count: 0),
				ValidationMessages = new List<string>(),
				LibrariesNode = new LibrariesTreeNodeViewModel
				{
					List = new List<LibraryTreeNodeViewModel>()
				}
			};

			return viewModel;
		}

		public DocumentTreeViewModel ToTreeViewModel(Document document)
		{
			if (document == null) throw new NullException(() => document);

			// ToViewModel
			var viewModel = new DocumentTreeViewModel
			{
				ID = document.ID,
				PatchesNode = new PatchesTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Patches, document.Patches.Count)
				},
				MidiNode = new EntityWithListTreeNodeViewModel
				{
					EntityID = document.ID,
					Text = GetTreeNodeText(ResourceFormatter.Midi, document.MidiMappingGroups.Count),
					Visible = true,
					List = document.MidiMappingGroups
					               .Select(x => x.ToIDAndName())
					               .OrderBy(x => x.Name)
					               .ToList()
				},
				ScalesNode = new EntityWithListTreeNodeViewModel
				{
					EntityID = document.ID,
					Text = GetTreeNodeText(ResourceFormatter.Scales, document.Scales.Count),
					Visible = true,
					List = document.Scales
					               .Select(x => x.ToIDAndName())
					               .OrderBy(x => x.Name)
					               .ToList()
				},
				AudioOutputNode = CreateTreeLeafViewModel(ResourceFormatter.AudioOutput),
				AudioFileOutputListNode = CreateTreeLeafViewModel(ResourceFormatter.AudioFileOutput, document.AudioFileOutputs.Count),
				ValidationMessages = new List<string>(),
				LibrariesNode = new LibrariesTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Libraries, document.LowerDocumentReferences.Count)
				}
			};

			viewModel.LibrariesNode.List = document.LowerDocumentReferences
			                                       .Select(ConvertTo_LibraryTreeNodeViewModel_WithRelatedEntities)
			                                       .OrderBy(x => x.Caption)
			                                       .ToList();

			// Business
			IList<Patch> grouplessPatches = PatchGrouper.GetGrouplessPatches(document.Patches, mustIncludeHidden: true);
			IList<PatchGroupDto> patchGroupDtos = PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(document.Patches, mustIncludeHidden: true);

			// ToViewModel
			viewModel.PatchesNode.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
			                                                   .Select(ConvertToTreeNodeViewModel)
			                                                   .ToList();

			viewModel.PatchesNode.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.FriendlyGroupName)
			                                                      .Select(ConvertToTreeNodeViewModel)
			                                                      .ToList();
			return viewModel;
		}

		private LibraryTreeNodeViewModel ConvertTo_LibraryTreeNodeViewModel_WithRelatedEntities(DocumentReference lowerDocumentReference)
		{
			Document lowerDocument = lowerDocumentReference.LowerDocument;

			var viewModel = new LibraryTreeNodeViewModel
			{
				LowerDocumentReferenceID = lowerDocumentReference.ID,
				MidiNode = new EntityWithListTreeNodeViewModel
				{
					EntityID = lowerDocument.ID,
					Text = GetTreeNodeText(ResourceFormatter.Midi, lowerDocument.MidiMappingGroups.Count),
					List = lowerDocument.MidiMappingGroups.Select(x => x.ToIDAndName()).OrderBy(x => x.Name).ToArray(),
					Visible = lowerDocument.MidiMappingGroups.Any()
				},
				ScalesNode = new EntityWithListTreeNodeViewModel
				{
					EntityID = lowerDocument.ID,
					Text = GetTreeNodeText(ResourceFormatter.Scales, lowerDocument.Scales.Count),
					List = lowerDocument.Scales.Select(x => x.ToIDAndName()).OrderBy(x => x.Name).ToArray(),
					Visible = lowerDocument.Scales.Any()
				}
			};

			// Business
			IList<Patch> grouplessPatches = PatchGrouper.GetGrouplessPatches(lowerDocument.Patches, mustIncludeHidden: false);
			IList<PatchGroupDto> patchGroupDtos = PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(lowerDocument.Patches, mustIncludeHidden: false);

			// ToViewModel
			viewModel.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
			                                       .Select(ConvertToTreeNodeViewModel)
			                                       .ToList();

			viewModel.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.FriendlyGroupName)
			                                          .Select(ConvertToTreeNodeViewModel)
			                                          .ToList();

			string aliasOrName = lowerDocumentReference.GetAliasOrName();
			int visiblePatchCount = lowerDocument.Patches.Where(x => !x.Hidden).Count();
			viewModel.Caption = GetTreeNodeText(aliasOrName, visiblePatchCount);

			return viewModel;
		}

		private TreeLeafViewModel CreateTreeLeafViewModel(string displayName) => new TreeLeafViewModel { Text = displayName };

		private TreeLeafViewModel CreateTreeLeafViewModel(string displayName, int count)
		{
			var viewModel = new TreeLeafViewModel
			{
				Text = GetTreeNodeText(displayName, count)
			};

			return viewModel;
		}

		private PatchGroupTreeNodeViewModel ConvertToTreeNodeViewModel(PatchGroupDto patchGroupDto)
		{
			var viewModel = new PatchGroupTreeNodeViewModel
			{
				CanonicalGroupName = patchGroupDto.CanonicalGroupName,
				Caption = GetTreeNodeText(patchGroupDto.FriendlyGroupName, patchGroupDto.Patches.Count),
				PatchNodes = patchGroupDto.Patches
				                          .OrderBy(x => x.Name)
				                          .Select(ConvertToTreeNodeViewModel)
				                          .ToList()
			};

			return viewModel;
		}

		private PatchTreeNodeViewModel ConvertToTreeNodeViewModel(Patch entity)
		{
			var viewModel = new PatchTreeNodeViewModel
			{
				ID = entity.ID,
				HasLighterStyle = entity.Hidden,
				Name = ResourceFormatter.GetDisplayName(entity)
			};

			return viewModel;
		}

		private string GetTreeNodeText(string displayName, int count)
		{
			string text = displayName;

			if (count != 0)
			{
				text += $" ({count})";
			}

			return text;
		}
	}
}