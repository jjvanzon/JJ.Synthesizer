﻿using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class Number_OperatorValidator : OperatorValidator_Base
    {
        public Number_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.Number, 
                  expectedInletCount: 0, 
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.Number })
        { }

        protected override void Execute()
        {
            base.Execute();

            string numberString = DataPropertyParser.TryGetString(Object, PropertyNames.Number);

            For(() => numberString, PropertyDisplayNames.Number, DataPropertyParser.FormattingCulture)
                .NotNullOrEmpty()
                .IsDouble()
                .NotInfinity()
                .NotNaN();
        }
    }
}