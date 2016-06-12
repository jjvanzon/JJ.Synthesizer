using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithDimensionAndInterpolation
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithDimensionAndInterpolation>
    {
        public OperatorPropertiesPresenter_WithDimensionAndInterpolation(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithDimensionAndInterpolation ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithDimensionAndInterpolation();
        }
    }
}