using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.CanonicalModel;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ChildDocumentPropertiesPresenter
    {
        private IDocumentRepository _documentRepository;
        private IChildDocumentTypeRepository _childDocumentTypeRepository;
        private IPatchRepository _patchRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private IOperatorTypeRepository _operatorTypeRepository;
        private IIDRepository _idRepository;

        public ChildDocumentPropertiesViewModel ViewModel { get; set; }

        public ChildDocumentPropertiesPresenter(
            IDocumentRepository documentRepository, 
            IChildDocumentTypeRepository childDocumentTypeRepository, 
            IPatchRepository patchRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IOperatorTypeRepository operatorTypeRepository,
            IIDRepository idRepository)
        {
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (childDocumentTypeRepository == null) throw new NullException(() => childDocumentTypeRepository);
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _documentRepository = documentRepository;
            _childDocumentTypeRepository = childDocumentTypeRepository;
            _patchRepository = patchRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _operatorTypeRepository = operatorTypeRepository;
            _idRepository = idRepository;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Document entity = _documentRepository.Get(ViewModel.ID);
            bool visible = ViewModel.Visible;
            ViewModel = entity.ToChildDocumentPropertiesViewModel(_childDocumentTypeRepository);
            ViewModel.Visible = visible;
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

            Document entity = ViewModel.ToEntityWithMainPatchReference(_documentRepository, _childDocumentTypeRepository, _patchRepository);

            IValidator validator = new ChildDocumentValidator(entity);
            if (!validator.IsValid)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
            }
            else
            {
                ISideEffect sideEffect = new Document_SideEffect_UpdateDependentCustomOperators(entity, _inletRepository, _outletRepository, _documentRepository, _operatorTypeRepository, _idRepository);
                sideEffect.Execute();

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
