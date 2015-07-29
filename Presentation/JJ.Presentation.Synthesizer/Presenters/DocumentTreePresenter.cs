using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Presentation.Synthesizer.ViewModels.Partials;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentTreePresenter
    {
        private IDocumentRepository _documentRepository;

        public DocumentTreeViewModel ViewModel { get; set; }

        public DocumentTreePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public object Refresh()
        {
            AssertViewModel();

            Document document = _documentRepository.TryGet(ViewModel.ID);
            if (document == null)
            {
                ViewModelHelper.CreateDocumentNotFoundViewModel();
            }

            DocumentTreeViewModel viewModel2 = document.ToTreeViewModel();

            CopyNonPersistedProperties(ViewModel, viewModel2);

            ViewModel = viewModel2;

            return ViewModel;
        }

        public void ExpandNode(int nodeIndex)
        {
            AssertViewModel();

            // 'Business'
            ChildDocumentTreeNodeViewModel nodeViewModel =
                ViewModel.Instruments.Where(x => x.NodeIndex == nodeIndex).SingleOrDefault() ??
                ViewModel.Effects.Where(x => x.NodeIndex == nodeIndex).SingleOrDefault();

            if (nodeViewModel == null)
            {
                throw new Exception(String.Format(
                    "nodeIndex '{0}' does not match with any ChildDocumentTreeViewModel in viewModel.Instruments not viewModel.Effects.",
                    nodeIndex));
            }

            nodeViewModel.IsExpanded = true;
        }

        public void CollapseNode(int nodeIndex)
        {
            AssertViewModel();

            // 'Business'
            ChildDocumentTreeNodeViewModel nodeViewModel =
                ViewModel.Instruments.Where(x => x.NodeIndex == nodeIndex).SingleOrDefault() ??
                ViewModel.Effects.Where(x => x.NodeIndex == nodeIndex).SingleOrDefault();

            if (nodeViewModel == null)
            {
                throw new Exception(String.Format(
                    "nodeIndex '{0}' does not match with any ChildDocumentTreeViewModel in viewModel.Instruments not viewModel.Effects.",
                    nodeIndex));
            }

            nodeViewModel.IsExpanded = false;
        }

        public void Close()
        {
            AssertViewModel();

            ViewModel.Visible = false;
        }

        // Helpers

        /// <summary>
        /// Copies the Visible and the IsExpanded properties.
        /// It matches source and dest nodex by the NodeIndex properties,
        /// so it is important to keep those unique and (relatively) constant.
        /// </summary>
        private void CopyNonPersistedProperties(DocumentTreeViewModel sourceViewModel, DocumentTreeViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.Visible = sourceViewModel.Visible;

            var join1 = from sourceInstrumentViewModel in sourceViewModel.Instruments
                        join destInstrumentViewModel in destViewModel.Instruments
                        on sourceInstrumentViewModel.NodeIndex equals destInstrumentViewModel.NodeIndex
                        select new { sourceInstrumentViewModel, destInstrumentViewModel };

            foreach (var tuple in join1)
            {
                tuple.destInstrumentViewModel.IsExpanded = tuple.sourceInstrumentViewModel.IsExpanded;
            }

            var join2 = from sourceEffectViewModel in sourceViewModel.Effects
                        join destEffectViewModel in destViewModel.Effects
                        on sourceEffectViewModel.NodeIndex equals destEffectViewModel.NodeIndex
                        select new { sourceEffectViewModel, destEffectViewModel };

            foreach (var tuple in join2)
            {
                tuple.destEffectViewModel.IsExpanded = tuple.sourceEffectViewModel.IsExpanded;
            }
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
