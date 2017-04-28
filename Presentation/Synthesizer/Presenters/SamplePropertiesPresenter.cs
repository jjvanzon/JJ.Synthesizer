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
        private readonly RepositoryWrapper _repositories;
        private readonly SampleRepositories _sampleRepositories;
        private readonly SampleManager _sampleManager;

        public SamplePropertiesPresenter(RepositoryWrapper repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _sampleRepositories = new SampleRepositories(repositories);
            _sampleManager = new SampleManager(_sampleRepositories);
        }

        protected override SamplePropertiesViewModel CreateViewModel(SamplePropertiesViewModel userInput)
        {
            // GetEntity
            Sample sample = _repositories.SampleRepository.Get(userInput.Entity.ID);

            // ToViewModel
            SamplePropertiesViewModel viewModel = sample.ToPropertiesViewModel(_sampleRepositories);

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

        public SamplePropertiesViewModel Play(SamplePropertiesViewModel userInput)
        {
            return TemplateMethod(
                userInput,
                viewModel =>
                {
                    // GetEntity
                    Sample entity = _repositories.SampleRepository.Get(userInput.Entity.ID);

                    // Business
                    var x = new PatchManager(new PatchRepositories(_repositories));
                    x.CreatePatch();
                    Outlet outlet = x.Sample(entity);
                    VoidResult result = x.SavePatch();

                    // Non-Persisted
                    viewModel.OutletIDToPlay = outlet?.ID;
                    viewModel.ValidationMessages = result.Messages;

                    // Successful?
                    viewModel.Successful = result.Successful;
                });
        }
    }
}
