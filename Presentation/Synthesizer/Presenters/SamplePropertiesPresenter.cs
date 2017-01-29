using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SamplePropertiesPresenter : PresenterBase<SamplePropertiesViewModel>
    {
        private readonly SampleRepositories _repositories;
        private readonly SampleManager _sampleManager;

        public SamplePropertiesPresenter(SampleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _sampleManager = new SampleManager(repositories);
        }

        public SamplePropertiesViewModel Show(SamplePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Sample sample = _repositories.SampleRepository.Get(userInput.Entity.ID);

            // ToViewModel
            SamplePropertiesViewModel viewModel = sample.ToPropertiesViewModel(_repositories);

            // Non-Persisted
            viewModel.Visible = true;

            // Successful
            viewModel.Successful = true;

            return viewModel;
        }

        public SamplePropertiesViewModel Refresh(SamplePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // !Successful
            userInput.Successful = false;

            // GetEntity
            Sample entity = _repositories.SampleRepository.Get(userInput.Entity.ID);

            // ToViewModel
            SamplePropertiesViewModel viewModel = entity.ToPropertiesViewModel(_repositories);

            // Non-Persisted
            viewModel.Visible = userInput.Visible;

            // Successfule
            viewModel.Successful = true;

            return viewModel;
        }

        public SamplePropertiesViewModel Close(SamplePropertiesViewModel userInput)
        {
            SamplePropertiesViewModel viewModel = Update(userInput);

            if (viewModel.Successful)
            {
                viewModel.Visible = false;
            }

            return viewModel;
        }

        public SamplePropertiesViewModel LoseFocus(SamplePropertiesViewModel userInput)
        {
            SamplePropertiesViewModel viewModel = Update(userInput);
            return viewModel;
        }

        private SamplePropertiesViewModel Update(SamplePropertiesViewModel userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Sample entity = _repositories.SampleRepository.Get(userInput.Entity.ID);

            // Business
            VoidResult result = _sampleManager.Save(entity);

            // ToViewModel
            SamplePropertiesViewModel viewModel = entity.ToPropertiesViewModel(_repositories);

            // Non-Persisted
            viewModel.Visible = userInput.Visible;
            viewModel.ValidationMessages = result.Messages;

            // Successful?
            viewModel.Successful = result.Successful;

            return viewModel;
        }
    }
}
