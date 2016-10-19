using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithInletCount
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithInletCount>
    {
        public OperatorPropertiesPresenter_WithInletCount(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithInletCount ToViewModel(Operator op)
        {
            OperatorPropertiesViewModel_WithInletCount viewModel = op.ToPropertiesViewModel_WithInletCount();
            return viewModel;
        }

        protected override OperatorPropertiesViewModel_WithInletCount Update(OperatorPropertiesViewModel_WithInletCount userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            var patchManager = new PatchManager(entity.Patch, _repositories);

            VoidResult result1 = patchManager.SetOperatorInletCount(entity, userInput.InletCount);
            if (!result1.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_WithInletCount viewModel = entity.ToPropertiesViewModel_WithInletCount();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result1.Messages);

                return viewModel;
            }

            VoidResult result2 = patchManager.SaveOperator(entity);
            if (!result2.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_WithInletCount viewModel = entity.ToPropertiesViewModel_WithInletCount();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result2.Messages);

                return viewModel;
            }

            // ToViewModel
            OperatorPropertiesViewModel_WithInletCount viewModel2 = entity.ToPropertiesViewModel_WithInletCount();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel2);

            // Successful
            viewModel2.Successful = true;

            return viewModel2;
        }
    }
}