using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Exceptions;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    internal abstract class OperatorValidator_Base_WithOperatorType : OperatorValidator_Base
    {
        public OperatorValidator_Base_WithOperatorType(
            Operator op,
            OperatorTypeEnum expectedOperatorTypeEnum,
            IList<DimensionEnum> expectedInletDimensionEnums,
            IList<DimensionEnum> expectedOutletDimensionEnums,
            IList<string> expectedDataKeys)
            : base(op)
        {
            if (expectedDataKeys == null) throw new NullException(() => expectedDataKeys);
            if (expectedInletDimensionEnums == null) throw new NullException(() => expectedInletDimensionEnums);
            if (expectedOutletDimensionEnums == null) throw new NullException(() => expectedOutletDimensionEnums);
            int uniqueExpectedDataPropertyKeyCount = expectedDataKeys.Distinct().Count();
            if (uniqueExpectedDataPropertyKeyCount != expectedDataKeys.Count) throw new NotUniqueException(() => expectedDataKeys);

            For(() => op.GetOperatorTypeEnum(), ResourceFormatter.OperatorType).Is(expectedOperatorTypeEnum);

            ExecuteValidator(new DimensionInfoValidator(op.OperatorType?.HasDimension ?? false, op.StandardDimension, op.CustomDimensionName));
            ExecuteValidator(new DataPropertyValidator(op.Data, expectedDataKeys));

            int expectedInletCount = expectedInletDimensionEnums.Count;
            int expectedOutletCount = expectedOutletDimensionEnums.Count;

            // Inlets
            string inletCountPropertyDisplayName = CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets);
            For(() => op.Inlets.Count, inletCountPropertyDisplayName).Is(expectedInletCount);

            if (op.Inlets.Count == expectedInletCount)
            {
                IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.Position).ToArray();
                for (int i = 0; i < sortedInlets.Count; i++)
                {
                    Inlet inlet = sortedInlets[i];
                    DimensionEnum expectedDimensionEnum = expectedInletDimensionEnums[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(inlet);
                    ExecuteValidator(new InletValidator_NotForCustomOperator(inlet, expectedDimensionEnum), messagePrefix);
                }
            }

            // Outlets
            string outletCountPropertyDisplayName = CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets);
            For(() => op.Outlets.Count, outletCountPropertyDisplayName).Is(expectedOutletCount);

            // ReSharper disable once InvertIf
            if (op.Outlets.Count == expectedOutletCount)
            {
                IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.Position).ToArray();
                for (int i = 0; i < sortedOutlets.Count; i++)
                {
                    Outlet outlet = sortedOutlets[i];
                    DimensionEnum expectedDimensionEnum = expectedOutletDimensionEnums[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(outlet);
                    ExecuteValidator(new OutletValidator_NotForCustomOperator(outlet, expectedDimensionEnum), messagePrefix);
                }
            }
        }

    }
}
