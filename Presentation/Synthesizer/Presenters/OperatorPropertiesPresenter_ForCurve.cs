using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
	internal class OperatorPropertiesPresenter_ForCurve
		: OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForCurve>
	{
		public OperatorPropertiesPresenter_ForCurve(RepositoryWrapper repositories)
			: base(repositories)
		{ }

		protected override OperatorPropertiesViewModel_ForCurve ToViewModel(Operator entity)
		{
			return entity.ToPropertiesViewModel_ForCurve();
		}
	}
}