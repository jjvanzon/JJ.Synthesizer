using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class DocumentPropertiesPresenter
    {
        private IDocumentRepository _documentRepository;

        private DocumentPropertiesViewModel _viewModel;

        public DocumentPropertiesPresenter(IDocumentRepository documentRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);

            _documentRepository = documentRepository;
        }

        /// <summary>
        /// Can return DocumentPropertiesViewModel or NotFoundViewModel.
        /// </summary>
        public object Show(int id)
        {
            bool mustCreateViewModel = _viewModel == null ||
                                       _viewModel.Document.ID != id;
            if (mustCreateViewModel)
            {
                Document document = _documentRepository.TryGet(id);
                if (document == null)
                {
                    return CreateDocumentNotFoundViewModel();
                }

                _viewModel = document.ToPropertiesViewModel();
            }

            _viewModel.Visible = true;

            return _viewModel;
        }

        public DocumentPropertiesViewModel Close(DocumentPropertiesViewModel userInput)
        {
            _viewModel = Update(userInput);

            if (_viewModel.Successful)
            {
                _viewModel.Visible = false;
            }

            return _viewModel;
        }

        public DocumentPropertiesViewModel LooseFocus(DocumentPropertiesViewModel userInput)
        {
            _viewModel = Update(userInput);

            return _viewModel;
        }

        private DocumentPropertiesViewModel Update(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            Document document = userInput.ToEntity(_documentRepository);

            if (_viewModel == null)
            {
                _viewModel = document.ToPropertiesViewModel();
            }

            IValidator validator = new DocumentValidator_Basic(document);
            if (!validator.IsValid)
            {
                _viewModel.Successful = false;
                _viewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                _viewModel.ValidationMessages = new Message[0];
                _viewModel.Successful = true;
            }

            return _viewModel;
        }

        // Helpers

        private NotFoundViewModel CreateDocumentNotFoundViewModel()
        {
            NotFoundViewModel viewModel = new NotFoundPresenter().Show(PropertyDisplayNames.Document);
            return viewModel;
        }
    }
}
