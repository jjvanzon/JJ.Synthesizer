using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.DataProperty;
using JJ.Data.Synthesizer.Entities;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary> Validates the inlet and outlet ListIndexes. </summary>
    public abstract class OperatorValidator_Base : VersatileValidator<Operator>
    {
        public OperatorValidator_Base(
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
            if (uniqueExpectedDataPropertyKeyCount != expectedDataKeys.Count)
            {
                throw new NotUniqueException(() => expectedDataKeys);
            }

            int expectedInletCount = expectedInletDimensionEnums.Count;
            int expectedOutletCount = expectedOutletDimensionEnums.Count;

            ExecuteValidator(new NameValidator(op.Name, required: false));

            For(() => op.GetOperatorTypeEnum(), ResourceFormatter.OperatorType).Is(expectedOperatorTypeEnum);

            ExecuteValidator(new OperatorValidator_Dimension(op));
            ExecuteValidator(new DataPropertyValidator(op.Data, expectedDataKeys));

            // Inlets
            For(() => op.Inlets.Count, GetPropertyDisplayName_ForInletCount()).Is(expectedInletCount);

            if (op.Inlets.Count == expectedInletCount)
            {
                IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedInlets.Count; i++)
                {
                    Inlet inlet = sortedInlets[i];
                    DimensionEnum expectedDimensionEnum = expectedInletDimensionEnums[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(inlet);
                    ExecuteValidator(new InletValidator_NotForCustomOperator(inlet, expectedDimensionEnum), messagePrefix);
                }
            }

            // Outlets
            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount()).Is(expectedOutletCount);

            // ReSharper disable once InvertIf
            if (op.Outlets.Count == expectedOutletCount)
            {
                IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedOutlets.Count; i++)
                {
                    Outlet outlet = sortedOutlets[i];
                    DimensionEnum expectedDimensionEnum = expectedOutletDimensionEnums[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(outlet);
                    ExecuteValidator(new OutletValidator_NotForCustomOperator(outlet, expectedDimensionEnum), messagePrefix);
                }
            }
        }

        private string GetPropertyDisplayName_ForInletCount()
        {
            return CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets);
        }

        private string GetPropertyDisplayName_ForOutletCount()
        {
            return CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Outlets);
        }
    }
}
