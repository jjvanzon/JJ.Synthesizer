using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForCustomOperator
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesViewModel_ForCustomOperator ViewModel { get; set; }

        public OperatorPropertiesPresenter_ForCustomOperator(PatchRepositories repositories)
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
            ViewModel = entity.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
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
            Operator entity = ViewModel.ToEntity(
                _repositories.OperatorRepository,
                _repositories.OperatorTypeRepository,
                _repositories.PatchRepository,
                _repositories.DocumentRepository);

            var patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(entity);

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