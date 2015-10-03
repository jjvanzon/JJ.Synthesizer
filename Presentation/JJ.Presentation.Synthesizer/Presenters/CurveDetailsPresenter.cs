using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.CanonicalModel;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurveDetailsPresenter
    {
        public CurveDetailsViewModel ViewModel { get; set; }

        private CurveRepositories _repositories;

        public CurveDetailsPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
        }

        public void Show()
        {
            AssertViewModel();

            ViewModel.Visible = true;
        }

        public void Refresh()
        {
            AssertViewModel();

            Curve entity = _repositories.CurveRepository.Get(ViewModel.Entity.ID);

            bool visible = ViewModel.Visible;
            ViewModel = entity.ToDetailsViewModel(_repositories.NodeTypeRepository);
            ViewModel.Visible = true;
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

            Curve entity = ViewModel.ToEntityWithRelatedEntities(_repositories);

            IValidator validator = new CurveValidator(entity);

            ViewModel.Successful = validator.IsValid;
            ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}