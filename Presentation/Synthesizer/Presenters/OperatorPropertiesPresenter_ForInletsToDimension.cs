using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForInletsToDimension
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForInletsToDimension>
    {
        public OperatorPropertiesPresenter_ForInletsToDimension(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override Operator GetEntity(OperatorPropertiesViewModel_ForInletsToDimension userInput)
        {
            return _repositories.OperatorRepository.Get(userInput.ID);
        }

        protected override OperatorPropertiesViewModel_ForInletsToDimension ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForInletsToDimension();
        }

        protected override IResult SaveWithUserInput(Operator entity, OperatorPropertiesViewModel_ForInletsToDimension userInput)
        {
            VoidResult result1 = _patchManager.SetOperatorInletCount(entity, userInput.InletCount);
            if (!result1.Successful)
            {
                return result1;
            }

            VoidResult result2 = _patchManager.SaveOperator(entity);
            return result2;
        }
    }
}