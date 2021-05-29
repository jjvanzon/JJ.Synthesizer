using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForInletsToDimension
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForInletsToDimension>
    {
        public OperatorPropertiesPresenter_ForInletsToDimension(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForInletsToDimension ToViewModel(Operator op) => op.ToPropertiesViewModel_ForInletsToDimension(_repositories.InterpolationTypeRepository);
    }
}