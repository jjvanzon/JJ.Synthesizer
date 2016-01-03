using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Business.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchOutlet
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesViewModel_ForPatchOutlet ViewModel { get; set; }

        public OperatorPropertiesPresenter_ForPatchOutlet(PatchRepositories repositories)
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
            ViewModel = entity.ToPropertiesViewModel_ForPatchOutlet(_repositories.OutletTypeRepository);
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
            IValidator validator = new OperatorPropertiesViewModel_ForPatchOutlet_Validator(ViewModel);
            if (!validator.IsValid)
            {
                ViewModel.Successful = validator.IsValid;
                ViewModel.ValidationMessages = validator.ValidationMessages.ToCanonical();
                return;
            }

            Operator op = ViewModel.ToOperatorWithOutlet(_repositories);

            PatchManager patchManager = new PatchManager(op.Patch, _repositories);
            VoidResult result = patchManager.SaveOperator(op);

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