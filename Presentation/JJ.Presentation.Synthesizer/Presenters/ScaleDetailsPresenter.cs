using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Managers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class ScaleDetailsPresenter
    {
        private ScaleRepositories _repositories;
        private ScaleManager _scaleManager;

        public ScaleDetailsViewModel ViewModel { get; set; }

        public ScaleDetailsPresenter(ScaleRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _scaleManager = new ScaleManager(_repositories);
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Scale entity = _repositories.ScaleRepository.Get(ViewModel.ScaleID);

            bool visible = ViewModel.Visible;

            ViewModel = entity.ToDetailsViewModel();

            ViewModel.Visible = visible;
        }

        public void Close()
        {
            AssertViewModel();

            Update();

            if (ViewModel.Successful)
            {
                ViewModel.Visible = false;
            }
        }

        public void LoseFocus()
        {
            Update();
        }

        private void Update()
        {
            AssertViewModel();

            Scale scale = ViewModel.ToEntityWithRelatedEntities(_repositories);

            VoidResult result = _scaleManager.Save(scale);

            ViewModel.Successful = result.Successful;
            ViewModel.ValidationMessages = result.Messages;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
