using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Presentation;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;

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
            Document document = _documentRepository.TryGet(id);
            if (document == null)
            {
                var presenter2 = new NotFoundPresenter();
                NotFoundViewModel viewModel = presenter2.Show(PropertyDisplayNames.Document);
                return viewModel;
            }
            else
            {
                _viewModel = document.ToPropertiesViewModel();

                _documentRepository.Rollback();

                return _viewModel;
            }
        }

        // TODO: In both Close and LoseFocus you might want to check if the ID is filled,
        // because you might not be supposed to create a new entity with a Properties box.

        public DocumentPropertiesViewModel Close(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            Document document = userInput.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator(document);
            if (!validator.IsValid)
            {
                if (_viewModel == null)
                {
                    _viewModel = document.ToPropertiesViewModel();
                }

                _viewModel.Messages = validator.ValidationMessages.ToCanonical();

                _documentRepository.Rollback();

                return _viewModel;
            }
            else
            {
                // For now close is save. In the future a giant view model will retain state, until the user says 'save'.
                _documentRepository.Commit();

                if (_viewModel == null)
                {
                    _viewModel = ViewModelHelper.CreateEmptyDocumentPropertiesViewModel();
                }

                _viewModel.Visible = false;

                return _viewModel;
            }
        }

        public DocumentPropertiesViewModel LooseFocus(DocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            Document document = userInput.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator(document);
            if (!validator.IsValid)
            {
                if (_viewModel == null)
                {
                    _viewModel = document.ToPropertiesViewModel();
                }

                _viewModel.Messages = validator.ValidationMessages.ToCanonical();

                _documentRepository.Rollback();

                return _viewModel;
            }
            else
            {
                // For now loose focus is save. In the future a giant view model will retain state, until the user says 'save'.
                _documentRepository.Commit();

                if (_viewModel == null)
                {
                    _viewModel = document.ToPropertiesViewModel();
                }

                return _viewModel;
            }
        }
    }
}
