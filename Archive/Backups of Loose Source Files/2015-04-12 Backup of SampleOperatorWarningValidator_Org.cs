//using JJ.Business.Synthesizer.Resources;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Framework.Validation;
//using JJ.Persistence.Synthesizer;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace JJ.Business.Synthesizer.Warnings.Entities
//{
//    public class SampleOperatorWarningValidator : OperatorWarningValidatorBase
//    {
//        public SampleOperatorWarningValidator(Operator obj)
//            : base(obj)
//        { }

//        protected override void Execute()
//        {
//            if (Object == null) throw new NullException(() => Object);

//            if (Object.AsSampleOperator != null) // For warnings I need null-tollerance.
//            {
//                For(() => Object.AsSampleOperator.Sample, PropertyDisplayNames.Sample)
//                    .NotNull();
//            }
//        }
//    }
//}
