using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToTreeViewModelExtensions
    {
        public static DocumentTreeViewModel ToTreeViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new DocumentTreeViewModel
            {
                ID = document.ID,
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                AudioFileOutputsNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel(),
                Instruments = new List<ChildDocumentTreeNodeViewModel>(),
                Effects = new List<ChildDocumentTreeNodeViewModel>(),
                ReferencedDocuments = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                }
            };

            viewModel.ReferencedDocuments.List = document.DependentOnDocuments.Select(x => x.DependentOnDocument)
                                                                              .Select(x => x.ToReferencedDocumentViewModelWithRelatedEntities())
                                                                              .OrderBy(x => x.Name)
                                                                              .ToList();
            int nodeIndex = 0;

            viewModel.Instruments = document.ChildDocuments.Where(x => x.GetChildDocumentTypeEnum() == ChildDocumentTypeEnum.Instrument)
                                                           .OrderBy(x => x.Name)
                                                           .Select(x => x.ToChildDocumentTreeNodeViewModel(nodeIndex++))
                                                           .ToList();

            viewModel.Effects = document.ChildDocuments.Where(x => x.GetChildDocumentTypeEnum() == ChildDocumentTypeEnum.Effect)
                                                       .OrderBy(x => x.Name)
                                                       .Select(x => x.ToChildDocumentTreeNodeViewModel(nodeIndex++))
                                                       .ToList();
            return viewModel;
        }

        public static ChildDocumentTreeNodeViewModel ToChildDocumentTreeNodeViewModel(this Document document, int nodeIndex)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentTreeNodeViewModel
            {
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel(),
                ChildDocumentID = document.ID,
                NodeIndex = nodeIndex
            };

            return viewModel;
        }
    }
}
