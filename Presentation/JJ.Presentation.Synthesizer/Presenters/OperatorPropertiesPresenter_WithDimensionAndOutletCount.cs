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
    internal class OperatorPropertiesPresenter_WithDimensionAndOutletCount
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithDimensionAndOutletCount>
    {
        public OperatorPropertiesPresenter_WithDimensionAndOutletCount(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithDimensionAndOutletCount ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithDimensionAndOutletCount();
        }

        protected override OperatorPropertiesViewModel_WithDimensionAndOutletCount Update(OperatorPropertiesViewModel_WithDimensionAndOutletCount userInput)
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
                OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = entity.ToPropertiesViewModel_WithDimensionAndOutletCount();

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
                OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel = entity.ToPropertiesViewModel_WithDimensionAndOutletCount();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result2.Messages);

                return viewModel;
            }

            // ToViewModel
            OperatorPropertiesViewModel_WithDimensionAndOutletCount viewModel2 = entity.ToPropertiesViewModel_WithDimensionAndOutletCount();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel2);

            // Successful
            viewModel2.Successful = true;

            return viewModel2;
        }
    }
}