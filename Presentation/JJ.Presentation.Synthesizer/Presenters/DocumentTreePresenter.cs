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
    public class DocumentTreePresenter
    {
        private IDocumentRepository _documentRepository;

        private DocumentTreeViewModel _viewModel;

        public DocumentTreePresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Can return DocumentTreeViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int id)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.ID != id;

            if (mustCreateViewModel)
            {
                Document document = _documentRepository.TryGet(id);
                if (document == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                _viewModel = document.ToTreeViewModel();
            }

            _viewModel.Visible = true;

            return _viewModel;
        }

        public object Refresh(DocumentTreeViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Document document = _documentRepository.TryGet(viewModel.ID);
            if (document == null)
            {
                return CreateDocumentNotFoundViewModel();
            }

            _viewModel = document.ToTreeViewModel();

            CopyNonPersistedProperties(viewModel, _viewModel);

            return _viewModel;
        }

        public object ExpandNode(DocumentTreeViewModel viewModel, Guid temporaryID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // 'Business'
            ChildDocumentTreeViewModel nodeViewModel = 
                viewModel.Instruments.Where(x => x.TemporaryID == temporaryID).SingleOrDefault() ??
                viewModel.Effects.Where(x => x.TemporaryID == temporaryID).SingleOrDefault();

            if (nodeViewModel == null)
            {
                throw new Exception(String.Format(
                    "temporaryID '{0}' does not match with any ChildDocumentTreeViewModel in viewModel.Instruments not viewModel.Effects.",
                    temporaryID));
            }

            nodeViewModel.IsExpanded = true;

            if (_viewModel == null)
            {
                // Get entity
                Document document = _documentRepository.TryGet(viewModel.ID);
                if (document == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                // ToViewModel
                _viewModel = document.ToTreeViewModel();

                // Non-Persisted
                CopyNonPersistedProperties(viewModel, _viewModel);
            }

            return _viewModel;
        }

        public object CollapseNode(DocumentTreeViewModel viewModel, Guid temporaryID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            // 'Business'
            ChildDocumentTreeViewModel nodeViewModel =
                viewModel.Instruments.Where(x => x.TemporaryID == temporaryID).SingleOrDefault() ??
                viewModel.Effects.Where(x => x.TemporaryID == temporaryID).SingleOrDefault();

            if (nodeViewModel == null)
            {
                throw new Exception(String.Format(
                    "temporaryID '{0}' does not match with any ChildDocumentTreeViewModel in viewModel.Instruments not viewModel.Effects.",
                    temporaryID));
            }

            nodeViewModel.IsExpanded = false;

            if (_viewModel == null)
            {
                // Get entity
                Document document = _documentRepository.TryGet(viewModel.ID);
                if (document == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                // ToViewModel
                _viewModel = document.ToTreeViewModel();

                // Non-Persisted
                CopyNonPersistedProperties(viewModel, _viewModel);
            }

            return _viewModel;
        }

        public object Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyDocumentTreeViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }

        /// <summary>
        /// Can return DocumentPropertiesViewModel or NotFoundViewModel.
        /// </summary>
        public object Properties(int id)
        {
            var presenter2 = new DocumentPropertiesPresenter(_documentRepository);
            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }

        // Helpers

        private object CreateDocumentNotFoundViewModel()
        {
            var notFoundPresenter = new NotFoundPresenter();
            NotFoundViewModel viewModel = notFoundPresenter.Show(PropertyDisplayNames.Document);
            return viewModel;
        }

        /// <summary>
        /// Copies the Visible and the IsExpanded properties.
        /// TODO: Copying the IsExpanded properties does not work,
        /// because it tries to match by TemporaryID, which
        /// is so temporary it is not the same in the source and dest
        /// view models. A solution is yet to be found.
        /// </summary>
        private void CopyNonPersistedProperties(DocumentTreeViewModel sourceViewModel, DocumentTreeViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.Visible = sourceViewModel.Visible;

            var join1 = from sourceInstrumentViewModel in sourceViewModel.Instruments
                        join destInstrumentViewModel in destViewModel.Instruments 
                        on sourceInstrumentViewModel.TemporaryID equals destInstrumentViewModel.TemporaryID
                        select new { sourceInstrumentViewModel, destInstrumentViewModel };

            foreach (var tuple in join1)
            {
                tuple.destInstrumentViewModel.IsExpanded = tuple.sourceInstrumentViewModel.IsExpanded;
            }

            var join2 = from sourceEffectViewModel in sourceViewModel.Effects
                        join destEffectViewModel in destViewModel.Effects 
                        on sourceEffectViewModel.TemporaryID equals destEffectViewModel.TemporaryID
                        select new { sourceEffectViewModel, destEffectViewModel };

            foreach (var tuple in join2)
            {
                tuple.destEffectViewModel.IsExpanded = tuple.sourceEffectViewModel.IsExpanded;
            }
        }
    }
}
