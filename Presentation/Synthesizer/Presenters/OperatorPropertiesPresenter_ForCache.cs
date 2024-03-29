﻿using JJ.Presentation.Synthesizer.ViewModels;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForCache
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForCache>
    {
        public OperatorPropertiesPresenter_ForCache(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForCache ToViewModel(Operator op) => op.ToPropertiesViewModel_ForCache(_repositories.InterpolationTypeRepository);
    }
}