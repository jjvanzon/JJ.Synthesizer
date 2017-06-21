using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal abstract class OperatorPropertiesPresenterBase<TViewModel> 
        : PropertiesPresenterBase<TViewModel>, IOperatorPropertiesPresenter
        where TViewModel : OperatorPropertiesViewModelBase
    {
        protected readonly RepositoryWrapper _repositories;
        protected readonly PatchManager _patchManager;
        private readonly AutoPatcher _autoPatcher;

        public OperatorPropertiesPresenterBase(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _patchManager = new PatchManager(_repositories);
            _autoPatcher = new AutoPatcher(_repositories);
        }

        protected abstract TViewModel ToViewModel(Operator op);

        protected sealed override TViewModel CreateViewModel(TViewModel userInput)
        {
            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            TViewModel viewModel = ToViewModel(entity);

            return viewModel;
        }

        protected override void UpdateEntity(TViewModel viewModel)
        {
            // ToEntity: was already done by the MainPresenter.

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);

            if (viewModel.CanEditInletCount)
            {
                // Business
                VoidResult result = _patchManager.SetOperatorInletCount(entity, viewModel.InletCount);

                // ToViewModel
                viewModel.InletCount = entity.Inlets.Count;

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

                if (!result.Successful)
                {
                    // Successful?
                    viewModel.Successful = false;
                    return;
                }
            }

            if (viewModel.CanEditOutletCount)
            {
                // Business
                VoidResult result = _patchManager.SetOperatorOutletCount(entity, viewModel.OutletCount);

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

                // ToViewModel
                viewModel.OutletCount = entity.Outlets.Count;

                if (!result.Successful)
                {
                    // Successful?
                    viewModel.Successful = false;
                    return;
                }
            }

            {
                // Business
                VoidResult result = _patchManager.SaveOperator(entity);

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());

                // ReSharper disable once InvertIf
                if (!result.Successful)
                {
                    // Successful?
                    viewModel.Successful = false;
                    // ReSharper disable once RedundantJumpStatement
                    return;
                }
            }

            // Successful?
            viewModel.Successful = true;
        }

        public TViewModel Play(TViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // ToEntity: was already done by the MainPresenter.

                    // GetEntity
                    Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

                    // Business
                    Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(entity.Patch, entity.ID);
                    Outlet outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }

                    // Non-Persisted
                    viewModel.OutletIDToPlay = outlet?.ID;
                    viewModel.ValidationMessages.AddRange(result.Messages.ToCanonical());
                    viewModel.Successful = result.Successful;
                });
        }

        public TViewModel Delete(TViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

                    // Business
                    _patchManager.DeleteOwnedNumberOperators(entity.ID);
                    _patchManager.DeleteOperatorWithRelatedEntities(entity.ID);
                });
        }

        public OperatorPropertiesViewModelBase Play(OperatorPropertiesViewModelBase userInput) => Play((TViewModel)userInput);

        public OperatorPropertiesViewModelBase Delete(OperatorPropertiesViewModelBase userInput) => Delete((TViewModel)userInput);
    }
}