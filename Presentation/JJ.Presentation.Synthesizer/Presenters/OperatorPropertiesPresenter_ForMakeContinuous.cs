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
    internal class OperatorPropertiesPresenter_ForMakeContinuous
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForMakeContinuous>
    {
        public OperatorPropertiesPresenter_ForMakeContinuous(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForMakeContinuous ToViewModel(Operator op)
        {
            OperatorPropertiesViewModel_ForMakeContinuous viewModel = op.ToPropertiesViewModel_ForMakeContinuous();
            return viewModel;
        }

        protected override OperatorPropertiesViewModel_ForMakeContinuous Update(OperatorPropertiesViewModel_ForMakeContinuous userInput)
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
                OperatorPropertiesViewModel_ForMakeContinuous viewModel = entity.ToPropertiesViewModel_ForMakeContinuous();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result1.Messages);

                return viewModel;
            }

            VoidResult result2 = patchManager.SaveOperator(entity);
            if (!result2.Successful)
            {
                // ToViewModel
                OperatorPropertiesViewModel_ForMakeContinuous viewModel = entity.ToPropertiesViewModel_ForMakeContinuous();

                // Non-Persisted
                CopyNonPersistedProperties(userInput, viewModel);
                viewModel.ValidationMessages.AddRange(result2.Messages);

                return viewModel;
            }

            // ToViewModel
            OperatorPropertiesViewModel_ForMakeContinuous viewModel2 = entity.ToPropertiesViewModel_ForMakeContinuous();

            // Non-Persisted
            CopyNonPersistedProperties(userInput, viewModel2);

            // Successful
            viewModel2.Successful = true;

            return viewModel2;
        }
    }
}