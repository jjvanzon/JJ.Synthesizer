using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForFilter 
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForFilter>
    {
        public OperatorPropertiesPresenter_ForFilter(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForFilter ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForFilter();
        }
    }
}