using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.CanonicalModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveDetailsPresenter
    {
        private ICurveRepository _curveRepository;
        private INodeRepository _nodeRepository;
        private INodeTypeRepository _nodeTypeRepository;
        private IIDRepository _idRepository;
        
        public CurveDetailsViewModel ViewModel { get; set; }

        public CurveDetailsPresenter(
            ICurveRepository curveRepository,
            INodeRepository nodeRepository,
            INodeTypeRepository nodeTypeRepository,
            IIDRepository idRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);
            if (idRepository == null) throw new NullException(() => idRepository);

            _curveRepository = curveRepository;
            _nodeRepository = nodeRepository;
            _nodeTypeRepository = nodeTypeRepository;
            _idRepository = idRepository;
        }

        public CurveDetailsViewModel Show(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                Curve entity = userInput.ToEntityWithRelatedEntities(_curveRepository, _nodeRepository, _nodeTypeRepository);

                ViewModel = CreateViewModel(entity, userInput);
            }

            ViewModel.Visible = true;

            return ViewModel;
        }

        public CurveDetailsViewModel Close(CurveDetailsViewModel userInput)
        {
            ViewModel = Update(userInput);

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }

            return ViewModel;
        }

        public CurveDetailsViewModel LoseFocus(CurveDetailsViewModel userInput)
        {
            ViewModel = Update(userInput);

            return ViewModel;
        }

        public void Clear()
        {
            ViewModel = null;
        }

        private CurveDetailsViewModel Update(CurveDetailsViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            Curve entity = userInput.ToEntityWithRelatedEntities(_curveRepository, _nodeRepository, _nodeTypeRepository);

            if (MustCreateViewModel(ViewModel, userInput))
            {
                ViewModel = CreateViewModel(entity, userInput);
            }

            IValidator validator = new CurveValidator(entity, new HashSet<object>());
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

        private bool MustCreateViewModel(CurveDetailsViewModel existingViewModel, CurveDetailsViewModel userInput)
        {
            return existingViewModel == null ||
                   existingViewModel.Entity.ID != userInput.Entity.ID;
        }

        private CurveDetailsViewModel CreateViewModel(Curve entity, CurveDetailsViewModel userInput)
        {
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel(_nodeTypeRepository);
            return viewModel;
        }
    }
}
