using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForNumber
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesViewModel_ForNumber ViewModel { get; set; }

        public OperatorPropertiesPresenter_ForNumber(PatchRepositories repositories)
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

            Operator entity = _repositories.OperatorRepository.Get(ViewModel.ID);
            bool visible = ViewModel.Visible;
            ViewModel = entity.ToPropertiesViewModel_ForNumber();
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
            AssertViewModel();

            // ToEntity
            Operator entity = ViewModel.ToEntity(_repositories.OperatorRepository, _repositories.OperatorTypeRepository);

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);

            // ToViewModel
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