using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel>
    {
        public OperatorPropertiesPresenter(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel();
        }
    }
}