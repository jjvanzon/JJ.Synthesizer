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
    public class CurveDetailsPresenter
    {
        private ICurveRepository _curveRepository;
        private INodeRepository _nodeRepository;
        private INodeTypeRepository _nodeTypeRepository;
        
        public CurveDetailsViewModel ViewModel { get; set; }

        public CurveDetailsPresenter(
            ICurveRepository curveRepository,
            INodeRepository nodeRepository,
            INodeTypeRepository nodeTypeRepository)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (nodeRepository == null) throw new NullException(() => nodeRepository);
            if (nodeTypeRepository == null) throw new NullException(() => nodeTypeRepository);

            _curveRepository = curveRepository;
            _nodeRepository = nodeRepository;
            _nodeTypeRepository = nodeTypeRepository;
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

        public CurveDetailsViewModel LooseFocus(CurveDetailsViewModel userInput)
        {
            ViewModel = Update(userInput);

            return ViewModel;
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
                ViewModel.ValidationMessages = new Message[0];
                ViewModel.Successful = false;
            }

            return ViewModel;
        }

        private bool MustCreateViewModel(CurveDetailsViewModel existingViewModel, CurveDetailsViewModel userInput)
        {
            return existingViewModel == null ||
                   existingViewModel.Curve.Keys.RootDocumentID != userInput.Curve.Keys.RootDocumentID ||
                   existingViewModel.Curve.Keys.ListIndex != userInput.Curve.Keys.ListIndex ||
                   existingViewModel.Curve.Keys.ChildDocumentTypeEnum != userInput.Curve.Keys.ChildDocumentTypeEnum ||
                   existingViewModel.Curve.Keys.ChildDocumentListIndex != userInput.Curve.Keys.ChildDocumentListIndex;
        }

        private CurveDetailsViewModel CreateViewModel(Curve entity, CurveDetailsViewModel userInput)
        {
            CurveDetailsViewModel viewModel = entity.ToDetailsViewModel(
                userInput.Curve.Keys.ListIndex,
                userInput.Curve.Keys.ChildDocumentListIndex,
                _nodeTypeRepository);

            return viewModel;
        }
    }
}
