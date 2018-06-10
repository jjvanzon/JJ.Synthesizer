using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class OperatorPropertiesPresenter_WithInterpolation
		: OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_WithInterpolation>
	{
		public OperatorPropertiesPresenter_WithInterpolation(RepositoryWrapper repositories)
			: base(repositories)
		{ }

		protected override OperatorPropertiesViewModel_WithInterpolation ToViewModel(Operator op) => op.ToPropertiesViewModel_WithInterpolation(_repositories.InterpolationTypeRepository);
	}
}