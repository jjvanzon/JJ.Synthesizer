using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters.Bases
{
    internal abstract class OperatorPropertiesPresenterBase<TViewModel> 
        : DetailsOrPropertiesPresenterBase<Operator, TViewModel>, 
          IOperatorPropertiesPresenter
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

        protected override Operator GetEntity(TViewModel userInput) => _repositories.OperatorRepository.Get(userInput.ID);

        protected override IResult SaveWithUserInput(Operator entity, TViewModel userInput)
        {
            if (entity.CanSetInletCount())
            {
                VoidResult result = _patchManager.SetOperatorInletCount(entity, userInput.InletCount);
                if (!result.Successful)
                {
                    return result;
                }
            }

            // ReSharper disable once InvertIf
            if (entity.CanSetOutletCount())
            {
                VoidResult result = _patchManager.SetOperatorOutletCount(entity, userInput.OutletCount);
                if (!result.Successful)
                {
                    return result;
                }
            }

            return _patchManager.SaveOperator(entity);
        }

        public TViewModel Play(TViewModel userInput)
        {
            Outlet outlet = null;

            return ExecuteAction(
                userInput,
                entity =>
                {
                    Result<Outlet> result = _autoPatcher.AutoPatch_TryCombineSounds(entity.Patch, entity.ID);
                    outlet = result.Data;
                    if (outlet != null)
                    {
                        _autoPatcher.SubstituteSineForUnfilledInSoundPatchInlets(outlet.Operator.Patch);
                    }
                    return result;
                },
                viewModel =>
                {
                    viewModel.OutletIDToPlay = outlet?.ID;
                });
        }

        public TViewModel Delete(TViewModel userInput)
        {
            return ExecuteAction(
                userInput,
                entity =>
                {
                    _patchManager.DeleteOwnedNumberOperators(entity.ID);
                    _patchManager.DeleteOperatorWithRelatedEntities(entity.ID);
                    return null;
                });
        }

        // IOperatorPropertiesPresenter

        public OperatorPropertiesViewModelBase Play(OperatorPropertiesViewModelBase userInput) => Play((TViewModel)userInput);
        public OperatorPropertiesViewModelBase Delete(OperatorPropertiesViewModelBase userInput) => Delete((TViewModel)userInput);
    }
}