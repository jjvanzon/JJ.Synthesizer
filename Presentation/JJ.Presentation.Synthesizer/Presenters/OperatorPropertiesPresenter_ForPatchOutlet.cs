using JJ.Business.CanonicalModel;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToEntity;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Business;
using JJ.Business.Synthesizer.SideEffects;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchOutlet
    {
        private PatchRepositories _repositories;
        private PatchManager _patchManager;

        public OperatorPropertiesViewModel_ForPatchOutlet ViewModel { get; set; }

        public OperatorPropertiesPresenter_ForPatchOutlet(PatchRepositories repositories)
        {
            if (repositories == null) throw new NullException(() => repositories);

            _repositories = repositories;

            _patchManager = new PatchManager(_repositories);
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
            ViewModel = entity.ToPropertiesViewModel_ForPatchOutlet();
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

            Operator op = ViewModel.ToEntity(_repositories.OperatorRepository, _repositories.OperatorTypeRepository);

            // TODO: The decision that the whole Patch must be validated instead of just the operator, should be part of the Manager.
            VoidResult result = _patchManager.Validate(op.Patch);
            if (!result.Successful)
            {
                ViewModel.Successful = false;
                ViewModel.ValidationMessages = result.Messages;
            }
            else
            {
                ISideEffect sideEffect = new Document_SideEffect_UpdateDependentCustomOperators(
                    op.Patch.Document,
                    _repositories.InletRepository,
                    _repositories.OutletRepository,
                    _repositories.DocumentRepository,
                    _repositories.OperatorTypeRepository,
                    _repositories.IDRepository);

                sideEffect.Execute();

                ViewModel.Successful = true;
                ViewModel.ValidationMessages = new List<Message>();
            }
        }

        // Helpers

        private void AssertViewModel()
        {
            if (ViewModel == null) throw new NullException(() => ViewModel);
        }
    }
}