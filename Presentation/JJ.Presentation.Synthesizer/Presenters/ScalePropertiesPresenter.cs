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
    internal class ScalePropertiesPresenter
    {
        private ScaleRepositories _repositories;
        private ScaleManager _scaleManager;

        public ScalePropertiesViewModel ViewModel { get; set; }

        public ScalePropertiesPresenter(ScaleRepositories repositories)
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

            Scale entity = _repositories.ScaleRepository.Get(ViewModel.Entity.ID);

            bool visible = ViewModel.Visible;

            ViewModel = entity.ToPropertiesViewModel(_repositories.ScaleTypeRepository);

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

            Scale scale = ViewModel.ToEntity(_repositories.ScaleRepository, _repositories.ScaleTypeRepository);

            VoidResult result = _scaleManager.ValidateWithoutTones(scale);

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
