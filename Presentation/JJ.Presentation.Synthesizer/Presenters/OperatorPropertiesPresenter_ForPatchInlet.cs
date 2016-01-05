using System.Collections.Generic;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.Helpers;
using JJ.Business.Canonical;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchInlet
    {
        private PatchRepositories _repositories;

        public OperatorPropertiesViewModel_ForPatchInlet ViewModel { get; set; }

        public OperatorPropertiesPresenter_ForPatchInlet(PatchRepositories repositories)
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
            ViewModel = entity.ToPropertiesViewModel_ForPatchInlet(_repositories.InletTypeRepository);
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
            IValidator presentationValidator = new OperatorPropertiesViewModel_ForPatchInlet_Validator(ViewModel);
            if (!presentationValidator.IsValid)
            {
                ViewModel.Successful = presentationValidator.IsValid;
                ViewModel.ValidationMessages = presentationValidator.ValidationMessages.ToCanonical();
                return;
            }

            Operator op = ViewModel.ToOperatorWithInlet(_repositories);

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