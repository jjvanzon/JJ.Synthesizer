using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;

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