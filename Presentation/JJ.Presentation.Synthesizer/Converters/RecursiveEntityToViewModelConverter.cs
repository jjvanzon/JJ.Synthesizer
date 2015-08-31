using JJ.Business.CanonicalModel;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Converters
{
    internal class RecursiveEntityToViewModelConverter
    {
        private readonly IOperatorTypeRepository _operatorTypeRepository;
        private readonly ISampleRepository _sampleRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly EntityPositionManager _entityPositionManager;
        private Dictionary<Operator, OperatorViewModel> _dictionary;

        public RecursiveEntityToViewModelConverter(
            IOperatorTypeRepository operatorTypeRepository, ISampleRepository sampleRepository, IDocumentRepository documentRepository,
            EntityPositionManager entityPositionManager)
        {
            if (operatorTypeRepository == null) throw new NullException(() => operatorTypeRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);
            if (documentRepository == null) throw new NullException(() => documentRepository);
            if (entityPositionManager == null) throw new NullException(() => entityPositionManager);

            _operatorTypeRepository = operatorTypeRepository;
            _sampleRepository = sampleRepository;
            _documentRepository = documentRepository;
            _entityPositionManager = entityPositionManager;
        }

        public PatchDetailsViewModel ConvertToDetailsViewModel(Patch patch)
        {
            if (patch == null) throw new NullException(() => patch);

            _dictionary = new Dictionary<Operator, OperatorViewModel>();

            var viewModel = new PatchDetailsViewModel
            {
                Entity = ConvertToViewModelRecursive(patch),
                ValidationMessages = new List<Message>()
            };

            viewModel.OperatorToolboxItems = ViewModelHelper.CreateOperatorTypesViewModel(_operatorTypeRepository);

            foreach (OperatorViewModel operatorViewModel in viewModel.Entity.Operators)
            {
                SetViewModelPosition(operatorViewModel);
            }

            return viewModel;
        }

        private PatchViewModel ConvertToViewModelRecursive(Patch patch)
        {
            PatchViewModel viewModel = patch.ToViewModel();

            viewModel.Operators = ConvertToViewModelsRecursive(patch.Operators);

            return viewModel;
        }

        private IList<OperatorViewModel> ConvertToViewModelsRecursive(IList<Operator> operators)
        {
            var list = new List<OperatorViewModel>(operators.Count);

            foreach (Operator op in operators)
            {
                OperatorViewModel operatorViewModel = ConvertToViewModelRecursive(op);

                list.Add(operatorViewModel);
            }

            return list;
        }

        private OperatorViewModel ConvertToViewModelRecursive(Operator op)
        {
            OperatorViewModel viewModel;
            if (_dictionary.TryGetValue(op, out viewModel))
            {
                return viewModel;
            }

            viewModel = op.ToViewModel(_sampleRepository, _documentRepository, _entityPositionManager);

            _dictionary.Add(op, viewModel);

            // TODO: Low priority: When PatchInlets do not have inlets and PatchOutlets do not have PatchOutlets (in the future) these if's are probably not necessary anymore.

            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.PatchInlet)
            {
                viewModel.Inlets = ConvertToViewModelsRecursive(op.Inlets);
            }
            else
            {
                viewModel.Inlets = new List<InletViewModel>();
            }

            if (op.GetOperatorTypeEnum() != OperatorTypeEnum.PatchOutlet)
            {
                viewModel.Outlets = ConvertToViewModelsRecursive(op.Outlets);
            }
            else
            {
                viewModel.Outlets = new List<OutletViewModel>();
            }

            return viewModel;
        }

        private IList<InletViewModel> ConvertToViewModelsRecursive(IList<Inlet> entities)
        {
            IList<InletViewModel> viewModels = entities.OrderBy(x => x.SortOrder)
                                                       .Select(x => ConvertToViewModelRecursive(x))
                                                       .ToList();
            return viewModels;
        }

        private InletViewModel ConvertToViewModelRecursive(Inlet inlet)
        {
            InletViewModel viewModel = inlet.ToViewModel();

            if (inlet.InputOutlet != null)
            {
                viewModel.InputOutlet = ConvertToViewModelRecursive(inlet.InputOutlet);
            }

            return viewModel;
        }

        private IList<OutletViewModel> ConvertToViewModelsRecursive(IList<Outlet> entities)
        {
            IList<OutletViewModel> viewModels = entities.OrderBy(x => x.SortOrder)
                                                        .Select(x => ConvertToViewModelRecursive(x))
                                                        .ToList();
            return viewModels;
        }

        private OutletViewModel ConvertToViewModelRecursive(Outlet outlet)
        {
            OutletViewModel viewModel = outlet.ToViewModel();

            _entityPositionManager.GetOrCreateOperatorPosition(outlet.Operator);

            // Recursive call
            viewModel.Operator = ConvertToViewModelRecursive(outlet.Operator);

            return viewModel;
        }

        private void SetViewModelPosition(OperatorViewModel operatorViewModel)
        {
            EntityPosition entityPosition = _entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.EntityPositionID = entityPosition.ID;
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }
    }
}