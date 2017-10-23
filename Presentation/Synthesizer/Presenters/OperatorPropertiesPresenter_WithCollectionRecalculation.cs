using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_WithCollectionRecalculation
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithCollectionRecalculation>
    {
        public OperatorPropertiesPresenter_WithCollectionRecalculation(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_WithCollectionRecalculation ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_WithCollectionRecalculation();
        }
    }
}