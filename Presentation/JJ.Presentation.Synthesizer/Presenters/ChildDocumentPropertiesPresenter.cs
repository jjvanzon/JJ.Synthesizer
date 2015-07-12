using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ChildDocumentPropertiesPresenter
    {
        private IDocumentRepository _documentRepository;
        private IIDRepository _idRepository;

        public ChildDocumentPropertiesViewModel ViewModel { get; set; }

        public ChildDocumentPropertiesPresenter(IDocumentRepository documentRepository, IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _documentRepository = documentRepository;
            _idRepository = idRepository;
        }

        public ChildDocumentPropertiesViewModel Show(ChildDocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                Document entity = userInput.ToEntity(_documentRepository);

                ViewModel = entity.ToChildDocumentPropertiesViewModel();
            }

            ViewModel.Visible = true;

            return ViewModel;
        }

        public ChildDocumentPropertiesViewModel Close(ChildDocumentPropertiesViewModel userInput)
        {
            ViewModel = Update(userInput);

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }

            return ViewModel;
        }

        public ChildDocumentPropertiesViewModel LoseFocus(ChildDocumentPropertiesViewModel userInput)
        {
            ViewModel = Update(userInput);

            return ViewModel;
        }

        private ChildDocumentPropertiesViewModel Update(ChildDocumentPropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);
            
            Document entity = userInput.ToEntity(_documentRepository);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                ViewModel = entity.ToChildDocumentPropertiesViewModel();
            }

            IValidator validator = new ChildDocumentValidator(entity);
            if (!validator.IsValid)
            {
                ViewModel.Successful = true;
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                ViewModel.ValidationMessages = new List<Message>();
                ViewModel.Successful = false;
            }

            return ViewModel;
        }

        private bool MustCreateViewModel(ChildDocumentPropertiesViewModel existingViewModel, ChildDocumentPropertiesViewModel userInput)
        {
            return existingViewModel == null ||
                   existingViewModel.ID != userInput.ID;
        }
    }
}
