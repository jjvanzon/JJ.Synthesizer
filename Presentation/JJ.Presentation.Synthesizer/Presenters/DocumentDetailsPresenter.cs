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

        DocumentDetailsViewModel _viewModel;

        public DocumentDetailsPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        public DocumentDetailsViewModel Create()
        {
            Document document = _documentRepository.Create();

            _viewModel = document.ToDetailsViewModel();
            _viewModel.IDVisible = false;
            _viewModel.CanDelete = false;

            _documentRepository.Rollback();

            return _viewModel;
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
                NotFoundViewModel viewModel2 = presenter2.Show(PropertyDisplayNames.Document);
                return viewModel2;
            }
            else
            {
                _viewModel = document.ToDetailsViewModel();
                _viewModel.IDVisible = true;
                _viewModel.CanDelete = true;

                _documentRepository.Rollback();

                return _viewModel;
            }
        }

        public DocumentDetailsViewModel Save(DocumentDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            Document document = userInput.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator(document);
            if (!validator.IsValid)
            {
                if (_viewModel == null)
                {
                    _viewModel = document.ToDetailsViewModel();
                    _viewModel.IDVisible = userInput.IDVisible;
                    _viewModel.CanDelete = userInput.CanDelete;
                }

                _viewModel.Messages = validator.ValidationMessages.ToCanonical();

                _documentRepository.Rollback();

                return _viewModel;
            }
            else
            {
                _documentRepository.Commit();

                if (_viewModel == null)
                {
                    _viewModel = ViewModelHelper.CreateEmptyDocumentDetailsViewModel();
                }

                _viewModel.Visible = false;

                return _viewModel;
            }
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

        public object Close()
        {
            if (_viewModel == null)
            {
                _viewModel = ViewModelHelper.CreateEmptyDocumentDetailsViewModel();
            }

            _viewModel.Visible = false;

            return _viewModel;
        }
    }
}
