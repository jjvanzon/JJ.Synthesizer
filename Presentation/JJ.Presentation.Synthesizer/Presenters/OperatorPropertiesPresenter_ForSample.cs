using JJ.Data.Canonical;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;
using JJ.Presentation.Synthesizer.ViewModels;
using System.Collections.Generic;
using JJ.Business.Synthesizer;
using JJ.Business.Synthesizer.Helpers;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Framework.Common;
using System;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForSample
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForSample>
    {
        public OperatorPropertiesPresenter_ForSample(PatchRepositories repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForSample ToViewModel(Operator op)
        {
            return op.ToPropertiesViewModel_ForSample(_repositories.SampleRepository);
        }
    }
}