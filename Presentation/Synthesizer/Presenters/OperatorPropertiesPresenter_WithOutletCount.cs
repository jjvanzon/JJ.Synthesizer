using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Common;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithOutletCount
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithOutletCount>
    {
        public OperatorPropertiesPresenter_WithOutletCount(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithOutletCount ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithOutletCount();
        }

        protected override OperatorPropertiesViewModel_WithOutletCount Update(OperatorPropertiesViewModel_WithOutletCount userInput)
        {
            if (userInput == null) throw new NullException(() => userInput);

            // RefreshCounter
            userInput.RefreshCounter++;

            // Set !Successful
            userInput.Successful = false;

            // GetEntity
            Operator entity = _repositories.OperatorRepository.Get(userInput.ID);

            // Business
            PatchManager patchManager = new PatchManager(entity.Patch, _repositories);
            VoidResult result1 = patchManager.SetOperatorOutletCount(entity, userInput.OutletCount);
            if (!result1.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_WithOutletCount viewModel = entity.ToPropertiesViewModel_WithOutletCount();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result1.Messages);

                return viewModel;
            }

            // Business
            VoidResult result2 = patchManager.SaveOperator(entity);
            if (!result2.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_WithOutletCount viewModel = entity.ToPropertiesViewModel_WithOutletCount();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result2.Messages);

                return viewModel;
            }

            // ToViewModel
            OperatorPropertiesViewModel_WithOutletCount viewModel2 = entity.ToPropertiesViewModel_WithOutletCount();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel2);

            // Successful
            viewModel2.Successful = true;

            return viewModel2;
        }
    }
}