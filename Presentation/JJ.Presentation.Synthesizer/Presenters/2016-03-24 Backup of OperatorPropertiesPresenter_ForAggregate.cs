//using JJ.Data.Synthesizer;
//using JJ.Presentation.Synthesizer.ViewModels;
//using System.Collections.Generic;
//using JJ.Business.Synthesizer.Helpers;
//using JJ.Presentation.Synthesizer.ToViewModel;
//using System;

//namespace JJ.Presentation.Synthesizer.Presenters
//{
//    internal class OperatorPropertiesPresenter_ForAggregate
//        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForAggregate>
//    {
//        public OperatorPropertiesPresenter_ForAggregate(PatchRepositories repositories)
//            : base(repositories)
//        { }

//        protected override OperatorPropertiesViewModel_ForAggregate ToViewModel(Operator op)
//        {
//            return op.ToPropertiesViewModel_ForAggregate();
//        }
//    }
//}