using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForCustomOperator
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForCustomOperator>
    {
        public OperatorPropertiesPresenter_ForCustomOperator(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForCustomOperator ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForCustomOperator(_repositories.PatchRepository);
        }
    }
}