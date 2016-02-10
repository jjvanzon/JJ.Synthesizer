using JJ.Data.Synthesizer;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using System;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForCurve
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForCurve>
    {
        public OperatorPropertiesPresenter_ForCurve(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForCurve ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForCurve(_repositories.CurveRepository);
        }
    }
}