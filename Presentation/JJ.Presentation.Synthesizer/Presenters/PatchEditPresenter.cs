using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;
using JJ.Persistence.Synthesizer;
using JJ.Persistence.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ViewModels.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Validation;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Framework.Mathematics;
using JJ.Business.Synthesizer.Names;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class PatchEditPresenter
    {
        private IPatchRepository _patchRepository;
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private ICurveInRepository _curveInRepository;
        private IValueOperatorRepository _valueOperatorRepository;
        private ISampleOperatorRepository _sampleOperatorRepository;
        private IEntityPositionRepository _entityPositionRepository;

        private OperatorFactory _operatorFactory;
        private EntityPositionManager _entityPositionManager;

        private Patch _patch;
        private PatchEditViewModel _viewModel;

        public PatchEditPresenter(
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository, 
            ICurveInRepository curveInRepository,
            IValueOperatorRepository valueOperatorRepository,
            ISampleOperatorRepository sampleOperatorRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);
            if (curveInRepository == null) throw new NullException(() => curveInRepository);
            if (valueOperatorRepository == null) throw new NullException(() => valueOperatorRepository);
            if (sampleOperatorRepository == null) throw new NullException(() => sampleOperatorRepository);

            _patchRepository = patchRepository;
            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _operatorRepository = operatorRepository;
            _outletRepository = outletRepository;
            _entityPositionRepository = entityPositionRepository;
            _curveInRepository = curveInRepository;
            _valueOperatorRepository = valueOperatorRepository;
            _sampleOperatorRepository = sampleOperatorRepository;

            _entityPositionManager = new EntityPositionManager(_entityPositionRepository);

            // TODO: Pass more repositories
            _operatorFactory = new OperatorFactory(_operatorRepository, _inletRepository, _outletRepository, _curveInRepository, _valueOperatorRepository, _sampleOperatorRepository);
        }

        public PatchEditViewModel Create()
        {
            _patch = _patchRepository.Create();

            _viewModel = CreateViewModel(_patch);

            return _viewModel;
        }

        public PatchEditViewModel Edit(int patchID)
        {
            _patch = _patchRepository.Get(patchID);

            _viewModel = CreateViewModel(_patch);

            return _viewModel;
        }

        public PatchEditViewModel AddOperator(PatchEditViewModel viewModel, string operatorTypeName)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            if (_patch == null)
            {
                _patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
            }

            //Type operatorWrapperType;
            //if (!_operatorTypeName_To_WrapperTypeDictionary.TryGetValue(operatorTypeName, out operatorWrapperType))
            //{
            //    throw new Exception(String.Format("Invalid operatorTypeName '{0}'.", operatorTypeName));
            //}

            //IOperatorWrapper wrapper = Activator.CreateInstance(
            //throw new NotImplementedException();

            // TODO: This should be more dynamic in the future. And probably part of a manager.
            // So should a lot more concerning the operators.
            // And I need to use the base class OperatorWrapperBase and have its constructor
            // capable of creating the operator.
            Operator op;
            if (String.Equals(operatorTypeName, PropertyNames.Adder))
            {
                op = _operatorFactory.Adder(new Outlet[16]);
            }
            else if (String.Equals(operatorTypeName, PropertyNames.Add))
            {
                op = _operatorFactory.Add();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.CurveIn))
            {
                op = _operatorFactory.CurveIn();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.Divide))
            {
                op = _operatorFactory.Divide();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.Multiply))
            {
                op = _operatorFactory.Multiply();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.PatchInlet))
            {
                op = _operatorFactory.PatchInlet();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.PatchOutlet))
            {
                op = _operatorFactory.PatchOutlet();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.Power))
            {
                op = _operatorFactory.PatchOutlet();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.SampleOperator))
            {
                op = _operatorFactory.Sample();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.Sine))
            {
                op = _operatorFactory.Sine();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.Substract))
            {
                op = _operatorFactory.Substract();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.TimeAdd))
            {
                op = _operatorFactory.TimeAdd();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.TimeDivide))
            {
                op = _operatorFactory.TimeDivide();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.TimeMultiply))
            {
                op = _operatorFactory.TimeMultiply();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.TimePower))
            {
                op = _operatorFactory.TimePower();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.TimeSubstract))
            {
                op = _operatorFactory.TimeSubstract();
            }
            else if (String.Equals(operatorTypeName, PropertyNames.ValueOperator))
            {
                op = _operatorFactory.Value(0);
            }
            else
            {
                throw new Exception(String.Format("Invalid operatorTypeName '{0}'.", operatorTypeName));
            }

            op.LinkTo(_patch);

            // You need the ID in the MoveOperator action methods.
            // TODO: I never used to need it. Why do I need that now? Am I doing it right?
            _operatorRepository.Flush();

            if (_viewModel == null)
            {
                _viewModel = CreateViewModel(_patch);
            }
            else
            {
                OperatorViewModel operatorViewModel = op.ToViewModelWithRelatedEntities();
                // TODO: Should these coordinates should be set in business logic? And randomized the same way as in other parts of the code?
                operatorViewModel.CenterX = 100;
                operatorViewModel.CenterY = 100;
                _viewModel.Patch.Operators.Add(operatorViewModel);

                _viewModel.SavedMessageVisible = false;
            }

            return _viewModel;
        }

        // TODO: This should be more dynamic in the future.
        //private IDictionary<string, Type> _operatorTypeName_To_WrapperTypeDictionary = new Dictionary<string, Type>
        //{
        //    { "ValueOperator", typeof(ValueOperatorWrapper) },
        //    { "Add", typeof(AddWrapper) },
        //    { "Substract", typeof(SubstractWrapper) }
        //};

        public PatchEditViewModel MoveOperator(PatchEditViewModel viewModel, int operatorID, float centerX, float centerY)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            if (_patch == null)
            {
                _patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
            }

            Operator op = _operatorRepository.Get(operatorID); // This is just to check that the entity exists. TODO: But that's weird. You should be doing that in the entity position manager if anywhere.
            EntityPosition entityPosition = _entityPositionManager.SetOrCreateOperatorPosition(operatorID, centerX, centerY);

            if (_viewModel == null)
            {
                _viewModel = CreateViewModel(_patch);
            }
            else
            {
                OperatorViewModel operatorViewModel = _viewModel.Patch.Operators.Where(x => x.ID == operatorID).Single();
                operatorViewModel.CenterX = centerX;
                operatorViewModel.CenterY = centerY;

                _viewModel.SavedMessageVisible = false;
            }

            return _viewModel;
        }

        public PatchEditViewModel ChangeInputOutlet(PatchEditViewModel viewModel, int inletID, int inputOutletID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            if (_patch == null)
            {
                _patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
            }

            Inlet inlet = _inletRepository.Get(inletID);
            Outlet outlet = _outletRepository.Get(inputOutletID);
            inlet.LinkTo(outlet);

            // TODO: In a stateful situation you might just adjust a small part of the view model.
            _viewModel = CreateViewModel(_patch);

            return _viewModel;
        }

        public PatchEditViewModel Save(PatchEditViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            if (_patch == null)
            {
                _patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);
            }

            if (_viewModel == null)
            {
                _viewModel = CreateViewModel(_patch);
            }

            IValidator validator = new PatchValidator(_patch);
            if (!validator.IsValid)
            {
                _viewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                _viewModel.SavedMessageVisible = false;
                return _viewModel;
            }
            else
            {
                _patchRepository.Commit();
                _viewModel.SavedMessageVisible = true;
                return _viewModel;
            }
        }

        // Helpers

        private PatchEditViewModel CreateViewModel(Patch entity)
        {
            PatchEditViewModel viewModel = entity.ToPatchEditViewModel();

            // TODO: I got very confused about having to do this separately,
            // so it should belong in the main ToViewModel procedure
            foreach (OperatorViewModel operatorViewModel in viewModel.Patch.Operators)
            {
                SetViewModelPosition(operatorViewModel);
            }

            return viewModel;
        }

        private void SetViewModelPosition(OperatorViewModel operatorViewModel)
        {
            EntityPosition entityPosition = _entityPositionManager.GetOrCreateOperatorPosition(operatorViewModel.ID);
            operatorViewModel.CenterX = entityPosition.X;
            operatorViewModel.CenterY = entityPosition.Y;
        }
    }
}
