﻿using JJ.Business.Canonical;
using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer.Entities;
using JJ.Framework.Business;
using JJ.Framework.Validation;
using JJ.Presentation.Synthesizer.Presenters.Bases;
using JJ.Presentation.Synthesizer.ToViewModel;
using JJ.Presentation.Synthesizer.Validators;
using JJ.Presentation.Synthesizer.ViewModels;

namespace JJ.Presentation.Synthesizer.Presenters
{
    internal class OperatorPropertiesPresenter_ForPatchInlet
        : OperatorPropertiesPresenterBase<OperatorPropertiesViewModel_ForPatchInlet>
    {
        public OperatorPropertiesPresenter_ForPatchInlet(RepositoryWrapper repositories)
            : base(repositories)
        { }

        protected override OperatorPropertiesViewModel_ForPatchInlet ToViewModel(Operator op) => op.ToPropertiesViewModel_ForPatchInlet();

        protected override IResult Save(Operator entity, OperatorPropertiesViewModel_ForPatchInlet userInput)
        {
            IValidator validator = new OperatorPropertiesViewModel_ForPatchInlet_Validator(userInput);
            if (!validator.IsValid)
            {
                return validator.ToResult();
            }
            
            return base.Save(entity, userInput);
        }
    }
}