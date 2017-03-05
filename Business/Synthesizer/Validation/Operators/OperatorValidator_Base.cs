using JJ.Business.Synthesizer.Resources;
using JJ.Framework.Presentation.Resources;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using JJ.Data.Synthesizer;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Validation.DataProperty;

namespace JJ.Business.Synthesizer.Validation.Operators
{
    /// <summary> Validates the inlet and outlet ListIndexes and that the inlet names are NOT filled in. </summary>
    public abstract class OperatorValidator_Base : VersatileValidator<Operator>
    {
        private readonly OperatorTypeEnum _expectedOperatorTypeEnum;
        private readonly int _expectedInletCount;
        private readonly int _expectedOutletCount;
        private readonly IList<string> _expectedDataKeys;
        private readonly IList<DimensionEnum> _expectedInletDimensionEnums;
        private readonly IList<DimensionEnum> _expectedOutletDimensionEnums;

        public OperatorValidator_Base(
            Operator obj,
            OperatorTypeEnum expectedOperatorTypeEnum,
            IList<DimensionEnum> expectedInletDimensionEnums,
            IList<DimensionEnum> expectedOutletDimensionEnums,
            IList<string> expectedDataKeys)
            : base(obj, postponeExecute: true)
        {
            if (expectedDataKeys == null) throw new NullException(() => expectedDataKeys);
            if (expectedInletDimensionEnums == null) throw new NullException(() => expectedInletDimensionEnums);
            if (expectedOutletDimensionEnums == null) throw new NullException(() => expectedOutletDimensionEnums);

            int uniqueExpectedDataPropertyKeyCount = expectedDataKeys.Distinct().Count();
            if (uniqueExpectedDataPropertyKeyCount != expectedDataKeys.Count)
            {
                throw new NotUniqueException(() => expectedDataKeys);
            }

            _expectedOperatorTypeEnum = expectedOperatorTypeEnum;
            _expectedInletDimensionEnums = expectedInletDimensionEnums;
            _expectedOutletDimensionEnums = expectedOutletDimensionEnums;
            _expectedDataKeys = expectedDataKeys;
            _expectedInletCount = _expectedInletDimensionEnums.Count;
            _expectedOutletCount = _expectedOutletDimensionEnums.Count;

            // ReSharper disable once VirtualMemberCallInConstructor
            Execute();
        }

        protected override void Execute()
        {
            Operator op = Obj;

            For(() => op.GetOperatorTypeEnum(), ResourceFormatter.OperatorType).Is(_expectedOperatorTypeEnum);

            ExecuteValidator(new OperatorValidator_Dimension(op));
            ExecuteValidator(new DataPropertyValidator(op.Data, _expectedDataKeys));

            // Inlets
            For(() => op.Inlets.Count, GetPropertyDisplayName_ForInletCount()).Is(_expectedInletCount);

            if (op.Inlets.Count == _expectedInletCount)
            {
                IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedInlets.Count; i++)
                {
                    Inlet inlet = sortedInlets[i];
                    DimensionEnum expectedDimensionEnum = _expectedInletDimensionEnums[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(inlet);
                    ExecuteValidator(new InletValidator_NotForCustomOperator(inlet, i, expectedDimensionEnum), messagePrefix);
                }
            }

            // Outlets
            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount()).Is(_expectedOutletCount);

            // ReSharper disable once InvertIf
            if (op.Outlets.Count == _expectedOutletCount)
            {
                IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
                for (int i = 0; i < sortedOutlets.Count; i++)
                {
                    Outlet outlet = sortedOutlets[i];
                    DimensionEnum expectedDimensionEnum = _expectedOutletDimensionEnums[i];

                    string messagePrefix = ValidationHelper.GetMessagePrefix(outlet, i + 1);
                    ExecuteValidator(new OutletValidator_NotForCustomOperator(outlet, i, expectedDimensionEnum), messagePrefix);
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
