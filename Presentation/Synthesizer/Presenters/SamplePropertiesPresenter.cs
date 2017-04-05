using JJ.Framework.Exceptions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class SamplePropertiesPresenter : PropertiesPresenterBase<SamplePropertiesViewModel>
    {
        private readonly SampleRepositories _repositories;
        private readonly SampleManager _sampleManager;

        public SamplePropertiesPresenter(SampleRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);

            _sampleManager = new SampleManager(repositories);
        }

        protected override SamplePropertiesViewModel CreateViewModel(SamplePropertiesViewModel userInput)
        {
            // GetEntity
            Sample sample = _repositories.SampleRepository.Get(userInput.Entity.ID);

            // ToViewModel
            SamplePropertiesViewModel viewModel = sample.ToPropertiesViewModel(_repositories);

            return viewModel;
        }

        protected override SamplePropertiesViewModel UpdateEntity(SamplePropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Sample entity = _repositories.SampleRepository.Get(userInput.Entity.ID);

                // Business
                VoidResult result = _sampleManager.Save(entity);

                // Non-Persisted
                viewModel.ValidationMessages = result.Messages;

                // Successful?
                viewModel.Successful = result.Successful;
            });
        }
    }
}
