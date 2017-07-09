//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    internal class OperatorValidator_SetInletCount_WithOperatorType_ExceptCustomOperator : VersatileValidator
//    {
//        public OperatorValidator_SetInletCount_WithOperatorType_ExceptCustomOperator(Operator op)
//        {
//            if (op == null) throw new NullException(() => op);

//            bool canSetInletCount = op.CanSetInletCount();
//            if (!canSetInletCount)
//            {
//                string message = ResourceFormatter.CannotSetInletCountForOperatorType(ResourceFormatter.GetUnderlyingPatchDisplayName_OrOperatorTypeDisplayName(op));
//                ValidationMessages.Add(() => canSetInletCount, message);
//            }
//        }
//    }
//}