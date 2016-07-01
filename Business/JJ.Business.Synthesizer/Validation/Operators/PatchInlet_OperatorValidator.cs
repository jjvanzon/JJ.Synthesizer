using JJ.Business.Synthesizer.Resources;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using System;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Validation.OperatorData;
using System.Linq;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PatchInlet_OperatorValidator : OperatorValidator_Base
    {
        public PatchInlet_OperatorValidator(Operator obj)
            : base(
                  obj, 
                  OperatorTypeEnum.PatchInlet, 
                  expectedInletCount: 1, 
                  expectedOutletCount: 1,
                  allowedDataKeys: new string[] { PropertyNames.ListIndex })
        { }

        protected override void Execute()
        {
            base.Execute();

            Execute(new ListIndex_OperatorData_Validator(Object.Data));
        }
    }
}
