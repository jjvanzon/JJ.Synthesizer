using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal class RecursiveToDocumentTreeViewModelConverter
    {
        public DocumentTreeViewModel ToTreeViewModel(Document document, PatchRepositories repositories)
        {
            if (document == null) throw new NullException(() => document);

            // ToViewModel
            var viewModel = new DocumentTreeViewModel
            {
                ID = document.ID,
                CurvesNode = ViewModelHelper.CreateTreeLeafViewModel(ResourceFormatter.Curves, document.Curves.Count),
                SamplesNode = ViewModelHelper.CreateTreeLeafViewModel(ResourceFormatter.Samples, document.Samples.Count),
                ScalesNode = ViewModelHelper.CreateTreeLeafViewModel(ResourceFormatter.Scales, document.Scales.Count),
                AudioOutputNode = ViewModelHelper.CreateTreeLeafViewModel(ResourceFormatter.AudioOutput),
                AudioFileOutputListNode = ViewModelHelper.CreateTreeLeafViewModel(ResourceFormatter.AudioFileOutput, document.AudioFileOutputs.Count),
                ValidationMessages = new List<Message>(),
                PatchesNode = new PatchesTreeNodeViewModel
                {
                    Text = ViewModelHelper.GetTreeNodeText(ResourceFormatter.Patches, document.Patches.Count),
                    PatchGroupNodes = new List<PatchGroupTreeNodeViewModel>()
                },
                LibrariesNode = new LibrariesTreeNodeViewModel
                {
                    Text = ViewModelHelper.GetTreeNodeText(ResourceFormatter.LowerDocuments, document.LowerDocumentReferences.Count),
                    List = new List<LibraryTreeNodeViewModel>()
                }
            };

            viewModel.LibrariesNode.List = document.LowerDocumentReferences
                                                   .Select(x => ConvertToTreeNodeViewModelWithRelatedEntities(x, repositories))
                                                   .OrderBy(x => x.Caption)
                                                   .ToList();


            // Business
            var patchManager = new PatchManager(repositories);
            IList<Patch> grouplessPatches = patchManager.GetGrouplessPatches(document.Patches);
            IList<PatchGroupDto> patchGroupDtos = patchManager.GetPatchGroupDtos(document.Patches);

            // ToViewModel
            viewModel.PatchesNode.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
                                                               .Select(x => x.ToIDAndName())
                                                               .ToList();

            viewModel.PatchesNode.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.GroupName)
                                                                  .Select(x => x.ToTreeNodeViewModel())
                                                                  .ToList();
            return viewModel;
        }

        private static LibraryTreeNodeViewModel ConvertToTreeNodeViewModelWithRelatedEntities(DocumentReference lowerDocumentReference, PatchRepositories repositories)
        {
            Document document = lowerDocumentReference.LowerDocument;

            var viewModel = new LibraryTreeNodeViewModel
            {
                LowerDocumentReferenceID = lowerDocumentReference.ID,
                Caption = ViewModelHelper.GetLibraryDescription(lowerDocumentReference),
            };

            // Business
            var patchManager = new PatchManager(repositories);
            IList<Patch> grouplessPatches = patchManager.GetGrouplessPatches(document.Patches);
            IList<PatchGroupDto> patchGroupDtos = patchManager.GetPatchGroupDtos(document.Patches);

            // ToViewModel
            viewModel.PatchNodes = grouplessPatches.OrderBy(x => x.Name)
                                                   .Select(x => x.ToIDAndName())
                                                   .ToList();

            viewModel.PatchGroupNodes = patchGroupDtos.OrderBy(x => x.GroupName)
                                                      .Select(x => x.ToTreeNodeViewModel())
                                                      .ToList();

            return viewModel;
        }
    }
}
