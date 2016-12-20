//using System;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Presentation.Resources;
//using JJ.Data.Synthesizer;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Helpers;
//using System.Linq;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    internal class Unbundle_OperatorValidator : OperatorValidator_Base_WithoutData
//    {
//        public Unbundle_OperatorValidator(Operator obj)
//            : base(
//                obj,
//                OperatorTypeEnum.Unbundle,
//                new DimensionEnum[] { DimensionEnum.Undefined },
//                Enumerable.Repeat(DimensionEnum.Undefined, obj?.Outlets.Count ?? 0).ToArray())
//        { }

//        protected override void Execute()
//        {
//            base.Execute();

//            For(() => Object.Outlets.Count, CommonTitleFormatter.ObjectCount(PropertyDisplayNames.Outlets)).GreaterThan(0);
//        }
//    }
//}