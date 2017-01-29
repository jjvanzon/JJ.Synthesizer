using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Business.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ToneGridEditPresenter : PresenterBase<ToneGridEditViewModel>
    {
        private readonly ScaleRepositories _repositories;
        private readonly ScaleManager _scaleManager;

        public ToneGridEditPresenter(ScaleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _scaleManager = new ScaleManager(_repositories);
        }

        public ToneGridEditViewModel Show(ToneGridEditViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Scale scale = _repositories.ScaleRepository.Get(userInput.ScaleID);

            // ToViewModel
            ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public ToneGridEditViewModel Refresh(ToneGridEditViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Scale scale = _repositories.ScaleRepository.Get(userInput.ScaleID);

            // ToViewModel
            ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

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
                userInput.ValidationMessages = viewModelValidator.ValidationMessages.ToCanonical();
                return userInput;
            }

            // GetEntity
            Scale scale = _repositories.ScaleRepository.Get(userInput.ScaleID);

            // Business
            VoidResult result = _scaleManager.Save(scale);

            // ToViewModel
            ToneGridEditViewModel viewModel = scale.ToToneGridEditViewModel();
            viewModel.ValidationMessages = result.Messages;

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            // Successful
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}
