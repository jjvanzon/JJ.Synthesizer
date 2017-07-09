//using System.Linq;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Presentation.Resources;
//using JJ.Business.Synthesizer.Enums;
//using System.Collections.Generic;
//using JJ.Data.Synthesizer.Entities;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    internal abstract class OperatorValidator_Base_VariableInletCountOneOutlet : OperatorValidator_Base_WithOperatorType
//    {
//        public OperatorValidator_Base_VariableInletCountOneOutlet(
//            Operator op,
//            OperatorTypeEnum expectedOperatorTypeEnum,
//            DimensionEnum expectedOutletDimensionEnum,
//            IList<string> expectedDataKeys)
//            : base(
//                op,
//                expectedOperatorTypeEnum,
//                Enumerable.Repeat(DimensionEnum.Item, op?.Inlets.Count ?? 0).ToArray(),
//                new[] { expectedOutletDimensionEnum },
//                expectedDataKeys)
//        {
//            For(() => op.Inlets.Count, CommonResourceFormatter.Count_WithNamePlural(ResourceFormatter.Inlets)).GreaterThan(0);
//        }
//    }
//}