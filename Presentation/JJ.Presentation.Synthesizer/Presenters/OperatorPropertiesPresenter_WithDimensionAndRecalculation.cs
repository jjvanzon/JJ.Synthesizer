using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithDimensionAndRecalculation
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithDimensionAndRecalculation>
    {
        public OperatorPropertiesPresenter_WithDimensionAndRecalculation(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithDimensionAndRecalculation ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithDimensionAndRecalculation();
        }
    }
}