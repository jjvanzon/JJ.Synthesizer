using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
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
        private IChildDocumentTypeRepository _childDocumentTypeRepository;
        private IPatchRepository _patchRepository;
        private IIDRepository _idRepository;

        public ChildDocumentPropertiesViewModel ViewModel { get; set; }

        public ChildDocumentPropertiesPresenter(
            IDocumentRepository documentRepository, 
            IChildDocumentTypeRepository childDocumentTypeRepository, 
            IPatchRepository patchRepository,
            IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _documentRepository = documentRepository;
            _childDocumentTypeRepository = childDocumentTypeRepository;
            _patchRepository = patchRepository;
            _idRepository = idRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }
        }

        public void LoseFocus()
        {
            AssertViewModel();

            Update();
        }

        private void Update()
        {
            AssertViewModel();

            Document entity = ViewModel.ToEntity(_documentRepository, _childDocumentTypeRepository, _patchRepository);

            IValidator validator = new ChildDocumentValidator(entity);
            if (!validator.IsValid)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                ViewModel.ValidationMessages = new List<Message>();
                ViewModel.Successful = true;
            }
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
