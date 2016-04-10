using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForDimension 
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForDimension>
    {
        public OperatorPropertiesPresenter_ForDimension(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForDimension ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForDimension();
        }
    }
}