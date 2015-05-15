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
                Visible = true,
                ID = document.ID,
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                AudioFileOutputsNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel(),
                Instruments = new List<ChildDocumentTreeViewModel>(),
                Effects = new List<ChildDocumentTreeViewModel>(),
                ReferencedDocuments = new ReferencedDocumentsNodeViewModel
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

            foreach (Document instrument in document.Instruments)
            {
                ChildDocumentTreeViewModel instrumentTreeViewModel = instrument.ToChildDocumentTreeViewModel();
                viewModel.Instruments.Add(instrumentTreeViewModel);
            }

            foreach (Document effect in document.Effects)
            {
                ChildDocumentTreeViewModel effectTreeViewModel = effect.ToChildDocumentTreeViewModel();
                viewModel.Effects.Add(effectTreeViewModel);
            }

            return viewModel;
        }

        private static ChildDocumentTreeViewModel ToChildDocumentTreeViewModel(this Document document)
        {
            if (document == null) throw new NullException(() => document);

            var viewModel = new ChildDocumentTreeViewModel
            {
                ID = document.ID,
                Name = document.Name,
                CurvesNode = new DummyViewModel(),
                SamplesNode = new DummyViewModel(),
                PatchesNode = new DummyViewModel(),
            };

            return viewModel;
        }
    }
}
