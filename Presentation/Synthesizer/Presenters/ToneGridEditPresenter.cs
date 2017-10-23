using System;
using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.Presenters.Bases;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ToneGridEditPresenter : PresenterBase<ToneGridEditViewModel>
    {
        private readonly IScaleRepository _scaleRepository;
        private readonly ScaleManager _scaleManager;

        public ToneGridEditPresenter(IScaleRepository scaleRepository, ScaleManager scaleManager)
        {
            _scaleRepository = scaleRepository ?? throw new ArgumentNullException(nameof(scaleRepository));
            _scaleManager = scaleManager ?? throw new ArgumentNullException(nameof(scaleManager));
        }

        public void Show(ToneGridEditViewModel viewModel)
        {
            ExecuteNonPersistedAction(viewModel, () => viewModel.Visible = true);
        }

        public ToneGridEditViewModel Refresh(ToneGridEditViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Scale scale = _scaleRepository.Get(userInput.ScaleID);

            // ToViewModel
            ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public ToneGridEditViewModel Close(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public ToneGridEditViewModel LoseFocus(ToneGridEditViewModel userInput)
        {
            ToneGridEditViewModel viewModel = Update(userInput);
            return viewModel;
        }

        public ToneGridEditViewModel Edit(ToneGridEditViewModel userInput)
        {
            // Refreshing upon edit is required to update the Frequency values.
            ToneGridEditViewModel viewModel = Refresh(userInput);

            return viewModel;
        }

        private ToneGridEditViewModel Update(ToneGridEditViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // ViewModel Validator
            IValidator viewModelValidator = new ToneGridEditViewModelValidator(userInput);
            if (!viewModelValidator.IsValid)
            {
                userInput.ValidationMessages = viewModelValidator.Messages;
                return userInput;
            }

            // GetEntity
            Scale scale = _scaleRepository.Get(userInput.ScaleID);

            // Business
            VoidResult result = _scaleManager.Save(scale);

            // ToViewModel
            ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();
            viewModel.ValidationMessages = result.Messages;

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel);

            // Successful
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}
