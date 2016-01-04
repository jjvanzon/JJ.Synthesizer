using System.Collections.Generic;
using JJ.Framework.Validation;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Extensions;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Business.Synthesizer;
using JJ.Data.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class CurvePropertiesPresenter
    {
        private CurveRepositories _repositories;
        private CurveManager _curveManager;

        public CurvePropertiesViewModel ViewModel { get; set; }

        public CurvePropertiesPresenter(CurveRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;
            _curveManager = new CurveManager(_repositories);
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
            ViewModel = entity.ToPropertiesViewModel();
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
            AssertViewModel();

            Update();
        }

        private void Update()
        {
            Curve entity = ViewModel.ToEntity(_repositories.CurveRepository);

            VoidResult result = _curveManager.ValidateWithoutRelatedEntities(entity);

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
