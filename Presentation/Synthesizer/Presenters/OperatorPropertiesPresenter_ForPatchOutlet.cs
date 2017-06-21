using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchOutlet
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForPatchOutlet>
    {
        public OperatorPropertiesPresenter_ForPatchOutlet(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override Operator GetEntity(OperatorPropertiesViewModel_ForPatchOutlet userInput)
        {
            return _repositories.OperatorRepository.Get(userInput.ID);
        }

        protected override OperatorPropertiesViewModel_ForPatchOutlet ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForPatchOutlet();
        }

        protected override IResult Save(Operator entity)
        {
            return _patchManager.SaveOperator(entity);
        }
    }
}