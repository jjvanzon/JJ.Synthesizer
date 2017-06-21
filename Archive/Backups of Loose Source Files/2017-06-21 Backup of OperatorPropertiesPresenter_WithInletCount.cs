//using JJ.Business.Canonical;
//using JJ.Data.Canonical;
//using JJ.Presentation.Synthesizer.ViewModels;
//using JJ.Business.Synthesizer;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Business;
//using JJ.Presentation.Synthesizer.ToViewModel;
//using JJ.Framework.Collections;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    internal class OperatorPropertiesPresenter_WithInletCount
//        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithInletCount>
//    {
//        public OperatorPropertiesPresenter_WithInletCount(RepositoryWrapper repositories)
//            : base(repositories)
//        { }

//        protected override OperatorPropertiesViewModel_WithInletCount ToViewModel(Operator op)
//        {
//            return op.ToPropertiesViewModel_WithInletCount();
//        }

//        protected override void UpdateEntity(OperatorPropertiesViewModel_WithInletCount viewModel)
//        {
//            // GetEntity
//            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);

//            if (viewModel.CanEditInletCount)
//            {
//                // Business
//                VoidResult result1 = _patchManager.SetOperatorInletCount(entity, viewModel.InletCount);

//                // Non-Persisted
//                viewModel.ValidationMessages.AddRange(result1.Messages.ToCanonical());

//                if (!result1.Successful)
//                {
//                    // Successful?
//                    viewModel.Successful = false;
//                    return;
//                }
//            }

//            {
//                // Business
//                VoidResult result2 = _patchManager.SaveOperator(entity);

//                // Non-Persisted
//                viewModel.ValidationMessages.AddRange(result2.Messages.ToCanonical());

//                // ReSharper disable once InvertIf
//                if (!result2.Successful)
//                {
//                    // Successful?
//                    viewModel.Successful = false;
//                    // ReSharper disable once RedundantJumpStatement
//                    return;
//                }
//            }
//        }
//    }
//}