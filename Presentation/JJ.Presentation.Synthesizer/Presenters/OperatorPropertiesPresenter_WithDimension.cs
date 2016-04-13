using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithDimension 
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithDimension>
    {
        public OperatorPropertiesPresenter_WithDimension(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithDimension ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithDimension();
        }
    }
}