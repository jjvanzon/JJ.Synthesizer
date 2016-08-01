//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Presentation.Resources;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Framework.Validation;
//using JJ.Data.Synthesizer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Framework.Common;
//using JJ.Business.Synthesizer.Validation.DataProperty;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    /// <summary> Validates the inlet and outlet ListIndexes and that the inlet names are NOT filled in. </summary>
//    public abstract class OperatorValidator_Base : FluentValidator<Operator>
//    {
//        private readonly OperatorTypeEnum _expectedOperatorTypeEnum;
//        private readonly int _expectedInletCount;
//        private readonly int _expectedOutletCount;
//        private readonly IList<string> _expectedDataKeys;
//        private readonly IList<DimensionEnum> _expectedInletDimensionEnums;
//        private readonly IList<DimensionEnum> _expectedOutletDimensionEnums;

//        public OperatorValidator_Base(
//            Operator obj,
//            OperatorTypeEnum expectedOperatorTypeEnum,
//            IList<string> expectedDataKeys,
//            int expectedInletCount,
//            params DimensionEnum[] expectedInletAndOutletDimensionEnums)
//            : base(obj, postponeExecute: true)
//        {
//            if (expectedInletCount < 0) throw new LessThanException(() => expectedInletCount, 0);
//            if (expectedDataKeys == null) throw new NullException(() => expectedDataKeys);

//            int expectedOutletCount = expectedInletAndOutletDimensionEnums.Length - expectedInletCount;
//            if (expectedOutletCount < 0)
//            {
//                throw new LessThanException(() => expectedOutletCount, 0);
//            }

//            int uniqueExpectedDataPropertyKeyCount = expectedDataKeys.Distinct().Count();
//            if (uniqueExpectedDataPropertyKeyCount != expectedDataKeys.Count)
//            {
//                throw new NotUniqueException(() => expectedDataKeys);
//            }

//            _expectedOperatorTypeEnum = expectedOperatorTypeEnum;
//            _expectedInletCount = expectedInletCount;
//            _expectedOutletCount = expectedOutletCount;
//            _expectedDataKeys = expectedDataKeys;

//            _expectedInletDimensionEnums = expectedInletAndOutletDimensionEnums.Take(expectedInletCount).ToArray();
//            _expectedOutletDimensionEnums = expectedInletAndOutletDimensionEnums.Skip(expectedInletCount).ToArray();

//            Execute();
//        }

//        protected override void Execute()
//        {
//            Operator op = Object;

//            For(() => op.GetOperatorTypeEnum(), PropertyDisplayNames.OperatorType).Is(_expectedOperatorTypeEnum);

//            For(() => op.Inlets.Count, GetPropertyDisplayName_ForInletCount())
//                .Is(_expectedInletCount);

//            if (op.Inlets.Count == _expectedInletCount)
//            {
//                IList<Inlet> sortedInlets = op.Inlets.OrderBy(x => x.ListIndex).ToArray();
//                for (int i = 0; i < sortedInlets.Count; i++)
//                {
//                    Inlet inlet = sortedInlets[i];
//                    DimensionEnum expectedDimensionEnum = _expectedInletDimensionEnums[i];

//                    string messagePrefix = ValidationHelper.GetMessagePrefix(inlet, i + 1);
//                    ExecuteValidator(new InletValidator_NotForCustomOperator(inlet, i, expectedDimensionEnum), messagePrefix);
//                }
//            }

//            For(() => op.Outlets.Count, GetPropertyDisplayName_ForOutletCount())
//                .Is(_expectedOutletCount);

//            if (op.Outlets.Count == _expectedOutletCount)
//            {
//                IList<Outlet> sortedOutlets = op.Outlets.OrderBy(x => x.ListIndex).ToArray();
//                for (int i = 0; i < sortedOutlets.Count; i++)
//                {
//                    Outlet outlet = sortedOutlets[i];
//                    DimensionEnum expectedDimensionEnum = _expectedOutletDimensionEnums[i];

//                    string messagePrefix = ValidationHelper.GetMessagePrefix(outlet, i + 1);
//                    ExecuteValidator(new OutletValidator_NotForCustomOperator(outlet, i, expectedDimensionEnum), messagePrefix);
//                }
//            }

//            ExecuteValidator(new DataPropertyValidator(op.Data, _expectedDataKeys));
//        }

//        private string GetPropertyDisplayName_ForInletCount()
//        {
//            return CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Inlets);
//        }

//        private string GetPropertyDisplayName_ForOutletCount()
//        {
//            return CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets);
//        }
//    }
//}
