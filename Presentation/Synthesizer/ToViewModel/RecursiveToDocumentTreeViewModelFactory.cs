using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Items;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal class RecursiveToDocumentTreeViewModelFactory
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
                CurvesNode = CreateTreeLeafViewModel(ResourceFormatter.Curves, count: 0),
                SamplesNode = CreateTreeLeafViewModel(ResourceFormatter.Samples, count: 0),
                ScalesNode = CreateTreeLeafViewModel(ResourceFormatter.Scales, count: 0),
                AudioOutputNode = CreateTreeLeafViewModel(ResourceFormatter.AudioOutput),
                AudioFileOutputListNode = CreateTreeLeafViewModel(ResourceFormatter.AudioFileOutput, count: 0),
                ValidationMessages = new List<MessageDto>(),
                LibrariesNode = new LibrariesTreeNodeViewModel
                {
                    List = new List<LibraryTreeNodeViewModel>()
                }
            };

            return viewModel;
        }

        public DocumentTreeViewModel ToTreeViewModel(Document document, PatchRepositories repositories)
        {
            if (document == null) throw new NullException(() => document);

            // ToViewModel
            var viewModel = new DocumentTreeViewModel
            {
                ID = document.ID,
                CurvesNode = CreateTreeLeafViewModel(ResourceFormatter.Curves, document.Curves.Count),
                SamplesNode = CreateTreeLeafViewModel(ResourceFormatter.Samples, document.Samples.Count),
                ScalesNode = CreateTreeLeafViewModel(ResourceFormatter.Scales, document.Scales.Count),
                AudioOutputNode = CreateTreeLeafViewModel(ResourceFormatter.AudioOutput),
                AudioFileOutputListNode = CreateTreeLeafViewModel(ResourceFormatter.AudioFileOutput, document.AudioFileOutputs.Count),
                ValidationMessages = new List<MessageDto>(),
                PatchesNode = new PatchesTreeNodeViewModel
                {
                    Text = GetTreeNodeText(ResourceFormatter.Patches, document.Patches.Count),
                    PatchGroupNodes = new List<PatchGroupTreeNodeViewModel>()
                },
                LibrariesNode = new LibrariesTreeNodeViewModel
                {
                    Text = GetTreeNodeText(ResourceFormatter.Libraries, document.LowerDocumentReferences.Count),
                    List = new List<LibraryTreeNodeViewModel>()
                }
            };

            viewModel.LibrariesNode.List = document.LowerDocumentReferences
                                                   .Select(x => ConvertTo_LibraryTreeNodeViewModel_WithRelatedEntities(x, repositories))
                                                   .OrderBy(x => x.Caption)
                                                   .ToList();

            // Business
            var patchManager = new PatchManager(repositories);
            IList<Patch> grouplessPatches = patchManager.GetGrouplessPatches(document.Patches, hidden: null);
            IList<PatchGroupDto> patchGroupDtos = patchManager.GetPatchGroupDtos(document.Patches, hidden: null);

            // ToViewModel
            viewModel.PatchesNode.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
                                                               .Select(x => ToTreeNodeViewModel(x))
                                                               .ToList();

            viewModel.PatchesNode.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.GroupName)
                                                                  .Select(x => ToTreeNodeViewModel(x))
                                                                  .ToList();
            return viewModel;
        }

        private LibraryTreeNodeViewModel ConvertTo_LibraryTreeNodeViewModel_WithRelatedEntities(DocumentReference lowerDocumentReference, PatchRepositories repositories)
        {
            Document document = lowerDocumentReference.LowerDocument;

            var viewModel = new LibraryTreeNodeViewModel
            {
                LowerDocumentReferenceID = lowerDocumentReference.ID,
            };



            // Business
            var patchManager = new PatchManager(repositories);
            IList<Patch> grouplessPatches = patchManager.GetGrouplessPatches(document.Patches, hidden: false);
            IList<PatchGroupDto> patchGroupDtos = patchManager.GetPatchGroupDtos(document.Patches, hidden: false);

            // ToViewModel
            viewModel.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
                                                   .Select(x => ToTreeNodeViewModel(x))
                                                   .ToList();

            viewModel.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.GroupName)
                                                      .Select(x => ToTreeNodeViewModel(x))
                                                      .ToList();

            string aliasOrName = lowerDocumentReference.GetAliasOrName();
            int visiblePatchCount = document.Patches.Where(x => !x.Hidden).Count();
            viewModel.Caption = GetTreeNodeText(aliasOrName, visiblePatchCount);

            return viewModel;
        }

        private TreeLeafViewModel CreateTreeLeafViewModel(string displayName)
        {
            var viewModel = new TreeLeafViewModel
            {
                Text = displayName
            };

            return viewModel;
        }

        private TreeLeafViewModel CreateTreeLeafViewModel(string displayName, int count)
        {
            var viewModel = new TreeLeafViewModel
            {
                Text = GetTreeNodeText(displayName, count)
            };

            return viewModel;
        }

        private PatchGroupTreeNodeViewModel ToTreeNodeViewModel(PatchGroupDto patchGroupDto)
        {
            var viewModel = new PatchGroupTreeNodeViewModel
            {
                GroupName = patchGroupDto.GroupName,
                Caption = GetTreeNodeText(patchGroupDto.GroupName, patchGroupDto.Patches.Count),
                PatchNodes = patchGroupDto.Patches
                                          .OrderBy(x => x.Name)
                                          .Select(x => ToTreeNodeViewModel(x))
                                          .ToList()
            };

            return viewModel;
        }

        private PatchTreeNodeViewModel ToTreeNodeViewModel(Patch entity)
        {
            var viewModel = new PatchTreeNodeViewModel
            {
                ID = entity.ID,
                Name = entity.Name,
                HasLighterStyle = entity.Hidden
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
