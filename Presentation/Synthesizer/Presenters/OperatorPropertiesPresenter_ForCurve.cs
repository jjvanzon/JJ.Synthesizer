using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForCurve
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForCurve>
    {
        public OperatorPropertiesPresenter_ForCurve(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForCurve ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
        }
    }
}