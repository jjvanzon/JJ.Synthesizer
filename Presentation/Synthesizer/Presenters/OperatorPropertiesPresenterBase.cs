using System;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Collections;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal abstract class OperatorPropertiesPresenterBase<TViewModel> : PropertiesPresenterBase<TViewModel>
        where TViewModel : OperatorPropertiesViewModelBase
    {
        protected readonly PatchRepositories _repositories;

        public OperatorPropertiesPresenterBase(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        // Action Methods

        public TViewModel Show(TViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel => viewModel.Visible = true);
        }

        public TViewModel Refresh(TViewModel userInput)
        {
            return TemplateMethod(userInput, x => { });
        }

        public TViewModel Close(TViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            TViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public TViewModel LoseFocus(TViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            TViewModel viewModel = Update(userInput);

            return viewModel;
        }

        // Non-Public Members

        protected abstract TViewModel ToViewModel(Operator op);

        protected virtual TViewModel Update(TViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

                // Business
                var patchManager = new PatchManager(entity.Patch, _repositories);
                VoidResult result = patchManager.SaveOperator(entity);

                // Non-Persisted
                viewModel.ValidationMessages.AddRange(result.Messages);
            });
        }

        private TViewModel TemplateMethod(TViewModel userInput, Action<TViewModel> action)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // CreateViewModel
            TViewModel viewModel = CreateViewModel(userInput);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Action
            action(viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        private TViewModel CreateViewModel(TViewModel userInput)
        {
            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            TViewModel viewModel = ToViewModel(entity);

            return viewModel;
        }
    }
}
