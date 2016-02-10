using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForRandom 
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForRandom>
    {
        public OperatorPropertiesPresenter_ForRandom(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForRandom ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForRandom();
        }
    }
}