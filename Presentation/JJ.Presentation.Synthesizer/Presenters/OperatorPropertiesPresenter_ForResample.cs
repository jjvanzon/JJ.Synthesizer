using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForResample 
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForResample>
    {
        public OperatorPropertiesPresenter_ForResample(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForResample ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForResample();
        }
    }
}