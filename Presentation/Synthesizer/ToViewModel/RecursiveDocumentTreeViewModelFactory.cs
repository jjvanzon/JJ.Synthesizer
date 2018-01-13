using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
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
				MidiNode = new MidiTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Midi, count: 0),
					List = new List<IDAndName>()
				},
				ScalesNode = CreateTreeLeafViewModel(ResourceFormatter.Scales, count: 0),
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
				MidiNode = new MidiTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Midi, document.MidiMappings.Count)
				},
				ScalesNode = CreateTreeLeafViewModel(ResourceFormatter.Scales, document.Scales.Count),
				AudioOutputNode = CreateTreeLeafViewModel(ResourceFormatter.AudioOutput),
				AudioFileOutputListNode = CreateTreeLeafViewModel(ResourceFormatter.AudioFileOutput, document.AudioFileOutputs.Count),
				ValidationMessages = new List<string>(),
				LibrariesNode = new LibrariesTreeNodeViewModel
				{
					Text = GetTreeNodeText(ResourceFormatter.Libraries, document.LowerDocumentReferences.Count),
					List = new List<LibraryTreeNodeViewModel>()
				}
			};

			viewModel.LibrariesNode.List = document.LowerDocumentReferences
			                                       .Select(ConvertTo_LibraryTreeNodeViewModel_WithRelatedEntities)
			                                       .OrderBy(x => x.Caption)
			                                       .ToList();

			viewModel.MidiNode.List = document.MidiMappings.
			                                           OrderBy(x => x.Name)
			                                          .Select(ConvertToTreeNodeViewModel)
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

		private IDAndName ConvertToTreeNodeViewModel(MidiMapping midiMapping) => new IDAndName { ID = midiMapping.ID, Name = midiMapping.Name };

		private LibraryTreeNodeViewModel ConvertTo_LibraryTreeNodeViewModel_WithRelatedEntities(DocumentReference lowerDocumentReference)
		{
			Document document = lowerDocumentReference.LowerDocument;

			var viewModel = new LibraryTreeNodeViewModel
			{
				LowerDocumentReferenceID = lowerDocumentReference.ID
			};

			// Business
			IList<Patch> grouplessPatches = PatchGrouper.GetGrouplessPatches(document.Patches, mustIncludeHidden: false);
			IList<PatchGroupDto> patchGroupDtos = PatchGrouper.GetPatchGroupDtos_ExcludingGroupless(document.Patches, mustIncludeHidden: false);

			// ToViewModel
			viewModel.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
			                                       .Select(ConvertToTreeNodeViewModel)
			                                       .ToList();

			viewModel.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.FriendlyGroupName)
			                                          .Select(ConvertToTreeNodeViewModel)
			                                          .ToList();

			string aliasOrName = lowerDocumentReference.GetAliasOrName();
			int visiblePatchCount = document.Patches.Where(x => !x.Hidden).Count();
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