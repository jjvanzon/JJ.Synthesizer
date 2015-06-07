using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Keys;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            IList<Document> dependentOnDocuments = document.DependentOnDocuments.Select(x => x.DependentOnDocument).OrderBy(x => x.Name).ToArray();
            for (int i = 0; i < dependentOnDocuments.Count; i++)
            {
                Document dependentOnDocument = dependentOnDocuments[i];

                ReferencedDocumentViewModel referencedDocumentViewModel = dependentOnDocument.ToReferencedDocumentViewModelWithRelatedEntities(i);
                viewModel.ReferencedDocuments.List.Add(referencedDocumentViewModel);
            }

            int nodeIndex = 0;

            IList<Document> sortedInstruments = document.Instruments.OrderBy(x => x.Name).ToArray();
            for (int i = 0; i < sortedInstruments.Count; i++)
            {
                Document instrument = sortedInstruments[i];

                ChildDocumentTreeNodeViewModel instrumentTreeViewModel = instrument.ToChildDocumentTreeViewModel(i, nodeIndex++);

                viewModel.Instruments.Add(instrumentTreeViewModel);
            }

            IList<Document> sortedEffects = document.Effects.OrderBy(x => x.Name).ToArray();
            for (int i = 0; i < sortedEffects.Count; i++)
            {
                Document effect = sortedEffects[i];

                ChildDocumentTreeNodeViewModel effectTreeViewModel = effect.ToChildDocumentTreeViewModel(i, nodeIndex++);

                viewModel.Effects.Add(effectTreeViewModel);
            }

            return viewModel;
        }

        private static ChildDocumentTreeNodeViewModel ToChildDocumentTreeViewModel(this Document document, int listIndex, int nodeIndex)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentTreeNodeViewModel
            {
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel(),
                Keys = new ChildDocumentTreeNodeKeysViewModel
                {
                    ID = document.ID,
                    ListIndex = listIndex,
                    NodeIndex = nodeIndex
                }
            };

            return viewModel;
        }
    }
}
