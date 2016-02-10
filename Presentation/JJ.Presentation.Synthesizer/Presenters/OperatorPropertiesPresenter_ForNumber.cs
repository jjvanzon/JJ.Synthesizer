using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForNumber
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForNumber>
    {
        public OperatorPropertiesPresenter_ForNumber(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForNumber ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForNumber();
        }
    }
}