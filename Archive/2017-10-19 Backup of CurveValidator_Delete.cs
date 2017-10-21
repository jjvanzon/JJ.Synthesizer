//using System.Collections.Generic;
//using System.Linq;
//using JJ.Business.Synthesizer.EntityWrappers;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Extensions;
//using JJ.Business.Synthesizer.Resources;
//using JJ.Data.Synthesizer.Entities;
//using JJ.Data.Synthesizer.RepositoryInterfaces;
//using JJ.Framework.Exceptions;
//using JJ.Framework.Presentation.Resources;
//using JJ.Framework.Validation;

//namespace JJ.Business.Synthesizer.Validation.Curves
//{
//    internal class CurveValidator_Delete : VersatileValidator
//    {
//        public CurveValidator_Delete(Curve curve, ICurveRepository curveRepository)
//        {
//            if (curve == null) throw new NullException(() => curve);
//            if (curveRepository == null) throw new NullException(() => curveRepository);

//            string curveIdentifier = ResourceFormatter.Curve + " " + ValidationHelper.GetUserFriendlyIdentifier(curve);

//            foreach (Operator op in EnumerateCurveOperators(curve, curveRepository))
//            {
//                string patchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
//                string operatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForCurveOperator(op, curveRepository);

//                Messages.Add(CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(curveIdentifier, patchPrefix + operatorIdentifier));
//            }
//        }

//        private IEnumerable<Operator> EnumerateCurveOperators(Curve curve, ICurveRepository curveRepository)
//        {
//            if (curve == null) throw new NullException(() => curve);
//            if (curve.Document == null)
//            {
//                yield break;    
//            }

//            foreach (Operator op in curve.Document.Patches.SelectMany(x => x.Operators))
//            {
//                if (op.GetOperatorTypeEnum() != OperatorTypeEnum.Curve)
//                {
//                    continue;
//                }

//                var wrapper = new Curve_OperatorWrapper(op, curveRepository);

//                if (wrapper.Curve == curve ||
//                    wrapper.CurveID == curve.ID)
//                {
//                    yield return op;
//                }
//            }
//        }
//    }
//}
