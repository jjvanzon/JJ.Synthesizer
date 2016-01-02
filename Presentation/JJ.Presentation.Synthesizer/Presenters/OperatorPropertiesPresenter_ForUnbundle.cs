using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForUnbundle
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesViewModel_ForUnbundle ViewModel { get; set; }

        public OperatorPropertiesPresenter_ForUnbundle(PatchRepositories repositories)
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
            int outletCount = ViewModel.OutletCount;

            ViewModel = entity.ToPropertiesViewModel_ForUnbundle();

            ViewModel.Visible = visible;
            ViewModel.OutletCount = outletCount;
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

            Operator entity = ViewModel.ToEntity(_repositories.OperatorRepository, _repositories.OperatorTypeRepository);

            var patchManager = new PatchManager(entity.Patch, _repositories);

            VoidResult result1 = patchManager.SetOperatorOutletCount(entity, ViewModel.OutletCount);
            if (!result1.Successful)
            {
                ViewModel.Successful = result1.Successful;
                ViewModel.ValidationMessages = result1.Messages;
                return;
            }

            VoidResult result2 = patchManager.SaveOperator(entity);
            ViewModel.Successful = result2.Successful;
            ViewModel.ValidationMessages = result2.Messages;
            return;
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}