using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.Helpers;
using System.Collections.Generic;
using JJ.Framework.Validation;
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

            Scale entity = _repositories.ScaleRepository.Get(ViewModel.Entity.ID);

            bool visible = ViewModel.Visible;

            ViewModel = entity.ToDetailsViewModel(_repositories.ScaleTypeRepository);

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

            if (!result.Successful)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages = result.Messages;
            }
            else
            {
                ViewModel.ValidationMessages = new List<Message>();
                ViewModel.Successful = true;
            }
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}
