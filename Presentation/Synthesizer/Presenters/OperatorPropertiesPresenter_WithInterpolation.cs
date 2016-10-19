using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithInterpolation
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithInterpolation>
    {
        public OperatorPropertiesPresenter_WithInterpolation(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithInterpolation ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithInterpolation();
        }
    }
}