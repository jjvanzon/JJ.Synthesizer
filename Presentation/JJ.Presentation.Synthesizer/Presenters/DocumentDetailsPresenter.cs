using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Extensions;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentDetailsPresenter
    {
        private IDocumentRepository _documentRepository;

        public DocumentDetailsPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public DocumentDetailsViewModel Create()
        {
            Document document = _documentRepository.Create();
            DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();
            viewModel.IDVisible = false;
            viewModel.CanDelete = false;

            _documentRepository.Rollback();

            return viewModel;
        }

        /// <summary>
        /// Can return DocumentDetailsViewModel or NotFoundViewModel.
        /// </summary>
        public object Edit(int id)
        {
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                var presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel = presenter2.Show(PropertyDisplayNames.Document);
                return viewModel;
            }
            else
            {
                DocumentDetailsViewModel viewModel = document.ToDetailsViewModel();
                viewModel.IDVisible = true;
                viewModel.CanDelete = true;

                _documentRepository.Rollback();

                return viewModel;
            }
        }

        /// <summary>
        /// Can return DocumentDetailsViewModel or PreviousViewModel.
        /// </summary>
        public object Save(DocumentDetailsViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            Document document = viewModel.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator(document);
            if (!validator.IsValid)
            {
                // TODO: Be more stateful.
                DocumentDetailsViewModel viewModel2 = document.ToDetailsViewModel();
                viewModel2.Messages = validator.ValidationMessages.ToCanonical();

                viewModel2.IDVisible = viewModel.IDVisible;
                viewModel2.CanDelete = viewModel.CanDelete;

                _documentRepository.Rollback();

                return viewModel2;
            }

            _documentRepository.Commit();

            return new PreviousViewModel();
        }

        /// <summary>
        /// Can return DocumentConfirmDeleteViewModel, NotFoundViewModel or DocumentCannotDeleteViewModel.
        /// </summary>
        public object Delete(int id, RepositoryWrapper repositoryWrapper)
        {
            var presenter2 = new DocumentDeletePresenter(repositoryWrapper);
            object viewModel2 = presenter2.Show(id);
            return viewModel2;
        }
    }
}
