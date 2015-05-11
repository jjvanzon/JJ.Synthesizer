using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
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
using JJ.Business.Synthesizer.Extensions;
using JJ.Framework.Configuration;

namespace JJ.Presentation.Synthesizer.Presenters
{
    public class PatchDetailsPresenter
    {
        private IPatchRepository _patchRepository;
        private IOperatorRepository _operatorRepository;
        private IInletRepository _inletRepository;
        private IOutletRepository _outletRepository;
        private ICurveRepository _curveRepository;
        private ISampleRepository _sampleRepository;
        private IEntityPositionRepository _entityPositionRepository;

        private OperatorFactory _operatorFactory;
        private EntityPositionManager _entityPositionManager;

        private PatchDetailsViewModel _viewModel;

        public PatchDetailsPresenter(
            IPatchRepository patchRepository,
            IOperatorRepository operatorRepository,
            IInletRepository inletRepository,
            IOutletRepository outletRepository,
            IEntityPositionRepository entityPositionRepository, 
            ICurveRepository curveRepository,
            ISampleRepository sampleRepository)
        {
            if (patchRepository == null) throw new NullException(() => patchRepository);
            if (operatorRepository == null) throw new NullException(() => operatorRepository);
            if (inletRepository == null) throw new NullException(() => inletRepository);
            if (outletRepository == null) throw new NullException(() => outletRepository);
            if (entityPositionRepository == null) throw new NullException(() => entityPositionRepository);
            if (curveRepository == null) throw new NullException(() => curveRepository);
            if (sampleRepository == null) throw new NullException(() => sampleRepository);

            _patchRepository = patchRepository;
            _operatorRepository = operatorRepository;
            _inletRepository = inletRepository;
            _outletRepository = outletRepository;
            _entityPositionRepository = entityPositionRepository;
            _curveRepository = curveRepository;
            _sampleRepository = sampleRepository;

            _entityPositionManager = new EntityPositionManager(_entityPositionRepository);
            _operatorFactory = new OperatorFactory(_operatorRepository, _inletRepository, _outletRepository, _curveRepository, _sampleRepository);
        }

        public PatchDetailsViewModel Create()
        {
            Patch patch = _patchRepository.Create();

            _viewModel = patch.ToDetailsViewModel(_entityPositionManager);

            return _viewModel;
        }

        public PatchDetailsViewModel Edit(int patchID)
        {
            Patch patch = _patchRepository.Get(patchID);

            _viewModel = patch.ToDetailsViewModel(_entityPositionManager);

            return _viewModel;
        }

        public PatchDetailsViewModel AddOperator(PatchDetailsViewModel viewModel, string operatorTypeName)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

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

            op.LinkTo(patch);

            // You need the ID in the MoveOperator action methods.
            // TODO: I never used to need it. Why do I need that now? Am I doing it right?
            _operatorRepository.Flush();

            if (_viewModel == null)
            {
                _viewModel = patch.ToDetailsViewModel(_entityPositionManager);
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

        public PatchDetailsViewModel MoveOperator(PatchDetailsViewModel viewModel, int operatorID, float centerX, float centerY)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            Operator op = _operatorRepository.Get(operatorID); // This is just to check that the entity exists. TODO: But that's weird. You should be doing that in the entity position manager if anywhere.
            EntityPosition entityPosition = _entityPositionManager.SetOrCreateOperatorPosition(operatorID, centerX, centerY);

            if (_viewModel == null)
            {
                _viewModel = patch.ToDetailsViewModel(_entityPositionManager);
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

        public PatchDetailsViewModel ChangeInputOutlet(PatchDetailsViewModel viewModel, int inletID, int inputOutletID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            Inlet inlet = _inletRepository.Get(inletID);
            Outlet outlet = _outletRepository.Get(inputOutletID);
            inlet.LinkTo(outlet);

            // TODO: In a stateful situation you might just adjust a small part of the view model.
            _viewModel = patch.ToDetailsViewModel(_entityPositionManager);

            return _viewModel;
        }

        public PatchDetailsViewModel Save(PatchDetailsViewModel viewModel)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            
            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            if (_viewModel == null)
            {
                _viewModel = patch.ToDetailsViewModel(_entityPositionManager);
            }

            IValidator validator = new PatchValidator(patch, _curveRepository, _sampleRepository);
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

        public PatchDetailsViewModel SelectOperator(PatchDetailsViewModel viewModel, int operatorID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);

            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            if (_viewModel == null)
            {
                _viewModel = patch.ToDetailsViewModel(_entityPositionManager);
            }

            SetSelectedOperator(_viewModel, operatorID);

            return _viewModel;
        }

        public PatchDetailsViewModel DeleteOperator(PatchDetailsViewModel viewModel, int operatorID)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            
            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            Operator op = patch.Operators.Where(x => x.ID == operatorID).SingleOrDefault();
            if (op != null)
            {
                op.UnlinkRelatedEntities();
                op.DeleteRelatedEntities(_inletRepository, _outletRepository, _entityPositionRepository);
                _operatorRepository.Delete(op);
            }

            //if (_viewModel == null || FORCE_STATELESS)
            //{
                _viewModel = patch.ToDetailsViewModel(_entityPositionManager);
            //}
            //else
            //{
            //    // TODO: This is not enough because the connected inlets and outlets keep the operator viewModel alive.
            //    OperatorViewModel operatorViewModel = _viewModel.Patch.Operators.Where(x => x.ID == operatorID).Single();
            //    _viewModel.Patch.Operators.Remove(operatorViewModel);
            //    _viewModel.SelectedOperator = null;
            //}

            return _viewModel;
        }

        public PatchDetailsViewModel SetValue(PatchDetailsViewModel viewModel, string value)
        {
            if (viewModel == null) throw new NullException(() => viewModel);
            
            Patch patch = viewModel.ToEntity(_patchRepository, _operatorRepository, _inletRepository, _outletRepository, _entityPositionRepository);

            // TODO: Validation messages for incorrect situations.
            double d;
            if (Double.TryParse(value, out d))
            {
                if (viewModel.SelectedOperator != null)
                {
                    Operator op = patch.Operators.Where(x => x.ID == viewModel.SelectedOperator.ID).Single();
                    if (String.Equals(op.OperatorTypeName, PropertyNames.ValueOperator))
                    {
                        var wrapper = new ValueOperatorWrapper(op);
                        wrapper.Value = d;
                    }
                }
            }

            // TODO: See if you can do it more efficiently for stateful situations.
            _viewModel = patch.ToDetailsViewModel(_entityPositionManager);

            // TODO: You are not supposed to transform the view model based on information in that viewmodel.
            if (viewModel.SelectedOperator != null)
            {
                SetSelectedOperator(_viewModel, viewModel.SelectedOperator.ID);
            }

            return _viewModel;
        }

        private void SetSelectedOperator(PatchDetailsViewModel viewModel, int operatorID)
        {
            // The non-persisted operator selection data.
            foreach (OperatorViewModel operatorViewModel in viewModel.Patch.Operators)
            {
                if (operatorViewModel.ID == operatorID)
                {
                    operatorViewModel.IsSelected = true;
                    viewModel.SelectedOperator = operatorViewModel;
                    if (String.Equals(operatorViewModel.OperatorTypeName, PropertyNames.ValueOperator))
                    {
                        // Kind of dirty: this depends on the value being filled in as the name for value operators.
                        _viewModel.SelectedValue = operatorViewModel.Name;
                    }
                }
                else
                {
                    operatorViewModel.IsSelected = false;
                }
            }
        }

        public object Close()
        {
            PatchDetailsViewModel viewModel = ViewModelHelper.CreateEmptyPatchDetailsViewModel();
            viewModel.Visible = false;
            return viewModel;
        }
    }
}
