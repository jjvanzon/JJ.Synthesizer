//using JetBrains.Annotations;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.Operators
//{
//    internal abstract class OperatorValidator_Base : VersatileValidator
//    {
//        public OperatorValidator_Base([NotNull] Operator op)
//        {
//            if (op == null) throw new NullException(() => op);

//            ExecuteValidator(new NameValidator(op.Name, required: false));
//        }
//    }
//}
