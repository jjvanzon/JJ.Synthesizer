using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Configuration;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentListPresenter
    {
        private IDocumentRepository _documentRepository;

        private static int _pageSize;
        private static int _maxVisiblePageNumbers;

        public DocumentListPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;

            ConfigurationSection config = ConfigurationHelper.GetSection<ConfigurationSection>();
            _pageSize = config.PageSize;
            _maxVisiblePageNumbers = config.MaxVisiblePageNumbers;
        }

        public DocumentListViewModel Show(int pageNumber)
        {
            int pageIndex = pageNumber - 1;
            IList<Document> documents = _documentRepository.GetPageOfRootDocuments(pageIndex * _pageSize, _pageSize);

            int count = _documentRepository.CountRootDocuments();

            DocumentListViewModel viewModel = new DocumentListViewModel
            {
                List = documents.Select(x => x.ToIDName()).ToArray(),
                Pager = PagerViewModelFactory.Create(pageIndex, _pageSize, count, _maxVisiblePageNumbers)
            };

            _documentRepository.Rollback();

            return viewModel;
        }

        public DocumentDetailsViewModel Create()
        {
            DocumentDetailsPresenter presenter2 = new DocumentDetailsPresenter(_documentRepository);
            DocumentDetailsViewModel viewModel2 = presenter2.Create();
            return viewModel2;
        }

        /// <summary>
        /// Can return DocumentDetailsViewModel or NotFoundViewModel.
        /// </summary>
        public object Edit(int id)
        {
            DocumentDetailsPresenter presenter2 = new DocumentDetailsPresenter(_documentRepository);
            object viewModel2 = presenter2.Edit(id);
            return viewModel2;
        }

        /// <summary>
        /// Can return DocumentConfirmDeleteViewModel, NotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Delete(
            int id,
            ICurveRepository curveRepository,
            IPatchRepository patchRepository,
            ISampleRepository sampleRepository,
            IAudioFileOutputRepository audioFileOutputRepository,
            IDocumentReferenceRepository documentReferenceRepository,
            INodeRepository nodeRepository,
            IAudioFileOutputChannelRepository audioFileOutputChannelRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository)
        {
            DocumentConfirmDeletePresenter presenter2 = new DocumentConfirmDeletePresenter(
                _documentRepository,
                curveRepository,
                patchRepository,
                sampleRepository,
                audioFileOutputRepository,
                documentReferenceRepository,
                nodeRepository,
                audioFileOutputChannelRepository,
                operatorRepository,
                inletRepository,
                outletRepository,
                entityPositionRepository);

            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }
    }
}
