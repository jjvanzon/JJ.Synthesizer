using JJ.Business.Canonical;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using Canonicals = JJ.Data.Canonical;
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
        protected readonly PatchRepositories _repositories;

        public OperatorPropertiesPresenterBase(PatchRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
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

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);
            Canonicals.VoidResultDto result = patchManager.SaveOperator(entity);

            // Non-Persisted
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;
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
                    var patchManager = new PatchManager(_repositories);
                    Result<Outlet> result = patchManager.AutoPatch_TryCombineSounds(entity.Patch, entity.ID);
                    Outlet outlet = result.Data;

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
                    var patchManager = new PatchManager(entity.Patch, _repositories);
                    patchManager.DeleteOwnedNumberOperators(entity.ID);
                    patchManager.DeleteOperatorWithRelatedEntities(entity.ID);
                });
        }

        public OperatorPropertiesViewModelBase Play(OperatorPropertiesViewModelBase userInput) => Play((TViewModel)userInput);

        public OperatorPropertiesViewModelBase Delete(OperatorPropertiesViewModelBase userInput) => Delete((TViewModel)userInput);
    }
}