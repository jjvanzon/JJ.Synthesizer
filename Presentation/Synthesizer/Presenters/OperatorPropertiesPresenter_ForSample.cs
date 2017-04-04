using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForSample
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForSample>
    {
        public OperatorPropertiesPresenter_ForSample(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForSample ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
        }
    }
}