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
//    internal class OperatorPropertiesPresenter_WithOutletCount
//        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithOutletCount>
//    {
//        public OperatorPropertiesPresenter_WithOutletCount(RepositoryWrapper repositories)
//            : base(repositories)
//        { }

//        protected override OperatorPropertiesViewModel_WithOutletCount ToViewModel(Operator op)
//        {
//            return op.ToPropertiesViewModel_WithOutletCount();
//        }

//        protected override void UpdateEntity(OperatorPropertiesViewModel_WithOutletCount viewModel)
//        {
//            // GetEntity
//            Operator entity = _repositories.OperatorRepository.Get(viewModel.ID);

//            if (viewModel.CanEditOutletCount)
//            {
//                // Business
//                VoidResult result1 = _patchManager.SetOperatorOutletCount(entity, viewModel.OutletCount);

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
//                VoidResult result2 = _patchManager.SaveOperator(entity);
//                // ReSharper disable once InvertIf
//                if (!result2.Successful)
//                {
//                    // Non-Persisted
//                    viewModel.ValidationMessages.AddRange(result2.Messages.ToCanonical());

//                    // Successful?
//                    viewModel.Successful = result2.Successful;
//                }
//            }
//        }
//    }
//}