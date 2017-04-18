using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Data.Canonical;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScalePropertiesPresenter : PropertiesPresenterBase<ScalePropertiesViewModel>
    {
        private readonly ScaleRepositories _repositories;
        private readonly ScaleManager _scaleManager;

        public ScalePropertiesPresenter(ScaleRepositories repositories)
        {
            _repositories = repositories ?? throw new NullException(() => repositories);
            _scaleManager = new ScaleManager(_repositories);
        }

        protected override ScalePropertiesViewModel CreateViewModel(ScalePropertiesViewModel userInput)
        {
            // GetEntity
            Scale scale = _repositories.ScaleRepository.Get(userInput.Entity.ID);

            // ToViewModel
            ScalePropertiesViewModel viewModel = scale.ToPropertiesViewModel();

            return viewModel;
        }

        protected override ScalePropertiesViewModel UpdateEntity(ScalePropertiesViewModel userInput)
        {
            return TemplateMethod(userInput, viewModel =>
            {
                // GetEntity
                Scale entity = _repositories.ScaleRepository.Get(userInput.Entity.ID);

                // Business
                VoidResult result = _scaleManager.SaveWithoutTones(entity);

                // Non-Persisted
                viewModel.ValidationMessages = result.Messages;

                // Successful?
                viewModel.Successful = result.Successful;
            });
        }
    }
}
