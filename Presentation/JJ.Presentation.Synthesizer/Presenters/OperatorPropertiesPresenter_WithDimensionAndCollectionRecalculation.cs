using System;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithDimensionAndCollectionRecalculation
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation>
    {
        public OperatorPropertiesPresenter_WithDimensionAndCollectionRecalculation(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithDimensionAndCollectionRecalculation ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithDimensionAndCollectionRecalculation();
        }
    }
}