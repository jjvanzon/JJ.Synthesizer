﻿using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal class RangeOverOutlets_OperatorValidator : OperatorValidator_Base_WithoutData
    {
        public RangeOverOutlets_OperatorValidator(Operator obj)
            : base(
                obj,
                OperatorTypeEnum.RangeOverOutlets,
                new DimensionEnum[] { DimensionEnum.Undefined, DimensionEnum.Undefined },
                Enumerable.Repeat(DimensionEnum.Undefined, obj?.Outlets.Count ?? 0).ToArray())
        { }

        protected override void Execute()
        {
            base.Execute();

            For(() => Object.Outlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets)).GreaterThan(0);
        }
    }
}