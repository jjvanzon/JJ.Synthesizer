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
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class DocumentDetailsPresenter
    {
        private IDocumentRepository _documentRepository;
        private IIDRepository _idRepository;

        private DocumentDetailsViewModel _viewModel;

        public DocumentDetailsPresenter(IDocumentRepository documentRepository, IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _documentRepository = documentRepository;
            _idRepository = idRepository;
        }

        public DocumentDetailsViewModel Create()
        {
            Document document = _documentRepository.Create();
            document.ID = _idRepository.GetID();

            _viewModel = document.ToDetailsViewModel();
            _viewModel.IDVisible = false;
            _viewModel.CanDelete = false;
            _viewModel.Visible = true;

            return _viewModel;
        }

        public DocumentDetailsViewModel Save(DocumentDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            Document document = userInput.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator_Basic(document);
            if (!validator.IsValid)
            {
                if (_viewModel == null)
                {
                    _viewModel = document.ToDetailsViewModel();
                    _viewModel.IDVisible = userInput.IDVisible;
                    _viewModel.CanDelete = userInput.CanDelete;
                }

                _viewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();

                return _viewModel;
            }
            else
            {
                // TODO: Perhaps report success and leave Committing to the MainPresenter.
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
