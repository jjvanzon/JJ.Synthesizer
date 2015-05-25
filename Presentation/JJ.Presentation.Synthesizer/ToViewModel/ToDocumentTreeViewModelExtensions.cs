using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ViewModels.Partials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Presentation.Synthesizer.ToViewModel
{
    internal static class ToDocumentTreeViewModelExtensions
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
                Instruments = new List<ChildDocumentTreeViewModel>(),
                Effects = new List<ChildDocumentTreeViewModel>(),
                ReferencedDocuments = new ReferencedDocumentsTreeNodeViewModel
                {
                    List = new List<ReferencedDocumentViewModel>()
                }
            };

            IList<Document> dependentOnDocuments = document.DependentOnDocuments.Select(x => x.DependentOnDocument).ToArray();
            foreach (Document dependentOnDocument in dependentOnDocuments)
            {
                ReferencedDocumentViewModel referencedDocumentViewModel = dependentOnDocument.ToReferencedDocumentViewModelWithRelatedEntities();
                viewModel.ReferencedDocuments.List.Add(referencedDocumentViewModel);
            }

            int nodeIndex = 0;

            for (int i = 0; i < document.Instruments.Count; i++)
            {
                Document instrument = document.Instruments[i];

                ChildDocumentTreeViewModel instrumentTreeViewModel = instrument.ToChildDocumentTreeViewModel();
                instrumentTreeViewModel.ListIndex = i;
                instrumentTreeViewModel.NodeIndex = nodeIndex++;

                viewModel.Instruments.Add(instrumentTreeViewModel);
            }

            for (int i = 0; i < document.Effects.Count; i++)
            {
                Document effect = document.Effects[i];

                ChildDocumentTreeViewModel effectTreeViewModel = effect.ToChildDocumentTreeViewModel();
                effectTreeViewModel.ListIndex = i;
                effectTreeViewModel.NodeIndex = nodeIndex++;

                viewModel.Effects.Add(effectTreeViewModel);
            }

            return viewModel;
        }

        /// <summary>
        /// ListIndex and NodeIndex are not assigned.
        /// </summary>
        private static ChildDocumentTreeViewModel ToChildDocumentTreeViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentTreeViewModel
            {
                ID = document.ID,
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel()
            };

            return viewModel;
        }
    }
}
