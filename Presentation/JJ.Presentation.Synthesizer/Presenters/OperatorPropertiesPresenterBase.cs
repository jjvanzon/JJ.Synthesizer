using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Common;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    /// <summary> Contains all code shared by operator properties presenters. </summary>
    public abstract class OperatorPropertiesPresenterBase<TViewModel>
        where TViewModel : OperatorPropertiesViewModelBase
    {
        protected PatchRepositories _repositories;

        public OperatorPropertiesPresenterBase(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        // Protected Members

        protected abstract TViewModel ToViewModel(Operator op);

        protected virtual TViewModel Update(TViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            PatchManager patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);

            // ToViewModel
            TViewModel viewModel = ToViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.ValidationMessages.AddRange(result.Messages);

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }

        protected virtual void CopyNonPersistedProperties(TViewModel sourceViewModel, TViewModel destViewModel)
        {
            if (sourceViewModel == null) throw new NullException(() => sourceViewModel);
            if (destViewModel == null) throw new NullException(() => destViewModel);

            destViewModel.ValidationMessages = sourceViewModel.ValidationMessages;
            destViewModel.Visible = sourceViewModel.Visible;
            destViewModel.Successful = sourceViewModel.Successful;
        }

        // Action Methods

        public TViewModel Show(TViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            TViewModel viewModel = ToViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);
            viewModel.Visible = true;

            return viewModel;
        }

        public TViewModel Refresh(TViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // ToViewModel
            TViewModel viewModel = ToViewModel(entity);

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            return viewModel;
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
    }
}
