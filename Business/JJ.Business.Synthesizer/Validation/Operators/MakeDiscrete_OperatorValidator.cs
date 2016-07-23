using System;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class MakeDiscrete_OperatorValidator : OperatorValidator_Base
    {
        public MakeDiscrete_OperatorValidator(Operator obj)
            : base(
                  obj,
                  OperatorTypeEnum.MakeDiscrete,
                  expectedInletCount: 1,
                  expectedOutletCount: obj?.Outlets?.Count ?? 0,
                  expectedDataKeys: new string[0])
        { }

        protected override void Execute()
        {
            base.Execute();

            Operator op = Object;

            For(() => op.Outlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets)).GreaterThan(0);
        }
    }
}