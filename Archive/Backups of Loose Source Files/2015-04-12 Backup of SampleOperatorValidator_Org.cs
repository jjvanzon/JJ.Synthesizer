﻿//using JJ.Business.Synthesizer.Names;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Business.Synthesizer.Validation;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Framework.Validation;
//using JJ.Persistence.Synthesizer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Business.Synthesizer.Validation.Entities
//{
//    public class SampleOperatorValidator : OperatorValidatorBase
//    {
//        public SampleOperatorValidator(Operator op)
//            : base(op, PropertyNames.SampleOperator, 0, PropertyNames.Result)
//        { }

//        protected override void Execute()
//        {
//            base.Execute();

//            For(() => Object.AsCurveIn, PropertyDisplayNames.AsCurveIn)
//                .IsNull();

//            For(() => Object.AsSampleOperator, PropertyDisplayNames.AsSampleOperator)
//                .NotNull();

//            For(() => Object.AsValueOperator, PropertyDisplayNames.AsValueOperator)
//                .IsNull();
//        }
//    }
//}
