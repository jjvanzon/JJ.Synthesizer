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
                DocumentPropertiesViewModel viewModel = document.ToPropertiesViewModel();

                _documentRepository.Rollback();

                return viewModel;
            }
        }

        /// <summary>
        /// Can return DocumentPropertiesViewModel or NoViewModel.
        /// </summary>
        public object Close(DocumentPropertiesViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            Document document = viewModel.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator(document);
            if (!validator.IsValid)
            {
                // TODO: Be more stateful.
                DocumentPropertiesViewModel viewModel2 = document.ToPropertiesViewModel();
                viewModel2.Messages = validator.ValidationMessages.ToCanonical();

                _documentRepository.Rollback();

                return viewModel2;
            }

            // For now close is save. In the future a giant view model will retain state, until the user says 'save'.
            _documentRepository.Commit();

            return new NoViewModel();
        }

        public DocumentPropertiesViewModel LooseFocus(DocumentPropertiesViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            Document document = viewModel.ToEntity(_documentRepository);

            IValidator validator = new DocumentValidator(document);
            if (!validator.IsValid)
            {
                // TODO: Be more stateful.
                DocumentPropertiesViewModel viewModel2 = document.ToPropertiesViewModel();
                viewModel2.Messages = validator.ValidationMessages.ToCanonical();

                _documentRepository.Rollback();

                return viewModel2;
            }
            else
            {
                // For now loose focus is save. In the future a giant view model will retain state, until the user says 'save'.
                _documentRepository.Commit();

                DocumentPropertiesViewModel viewModel2 = document.ToPropertiesViewModel();
                return viewModel2;
            }
        }
    }
}