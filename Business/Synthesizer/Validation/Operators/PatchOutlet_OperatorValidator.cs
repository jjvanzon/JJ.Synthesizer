﻿using JJ.Business.Synthesizer.Helpers;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Validation.DataProperty;
using System.Linq;
using JJ.Business.Synthesizer.Extensions;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class PatchOutlet_OperatorValidator : OperatorValidator_Base
    {
        public PatchOutlet_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.PatchOutlet,
                new DimensionEnum[] { DimensionEnum.Undefined },
                new DimensionEnum[] { GetOutletDimensionEnum(obj) },
                expectedDataKeys: new string[] { PropertyNames.ListIndex })
        { }

        protected override void Execute()
        {
            ExecuteValidator(new ListIndex_DataProperty_Validator(Object.Data));

            base.Execute();
        }

        // Helpers

        private static DimensionEnum GetOutletDimensionEnum(Operator obj)
        {
            return obj?.Outlets.FirstOrDefault()?.GetDimensionEnum() ?? DimensionEnum.Undefined;
        }
    }
}