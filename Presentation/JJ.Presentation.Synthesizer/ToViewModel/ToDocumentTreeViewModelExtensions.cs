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
                Instruments = new List<DocumentTreeViewModel>(),
                Effects = new List<DocumentTreeViewModel>(),
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
                DocumentTreeViewModel instrumentTreeViewModel = instrument.ToTreeViewModel();
                viewModel.Instruments.Add(instrumentTreeViewModel);
            }

            foreach (Document effect in document.Effects)
            {
                DocumentTreeViewModel effectTreeViewModel = effect.ToTreeViewModel();
                viewModel.Effects.Add(effectTreeViewModel);
            }

            return viewModel;
        }
    }
}
