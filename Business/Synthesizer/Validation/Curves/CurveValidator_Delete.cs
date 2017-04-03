using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Framework.Exceptions;
using JJ.Framework.Validation;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;
using JJ.Framework.Presentation.Resources;

namespace JJ.Business.Synthesizer.Validation.Curves
{
    internal class CurveValidator_Delete : VersatileValidator<Curve>
    {
        private readonly ICurveRepository _curveRepository;

        public CurveValidator_Delete([NotNull] Curve curve, [NotNull] ICurveRepository curveRepository)
            : base(curve, postponeExecute: true)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            _curveRepository = curveRepository;

            Execute();
        }

        protected sealed override void Execute()
        {
            Curve curve = Obj;

            string curveIdentifier = ResourceFormatter.Curve + " " + ValidationHelper.GetUserFriendlyIdentifier(curve);

            foreach (Operator op in EnumerateCurveOperators(curve))
            {
                string patchPrefix = ValidationHelper.GetMessagePrefix(op.Patch);
                string operatorIdentifier = ResourceFormatter.Operator + " " + ValidationHelper.GetUserFriendlyIdentifier_ForCurveOperator(op, _curveRepository);

                ValidationMessages.Add(
                    PropertyNames.Sample,
                    CommonResourceFormatter.CannotDelete_WithName_AndDependentItem(curveIdentifier, patchPrefix + operatorIdentifier));
            }
        }

        private IEnumerable<Operator> EnumerateCurveOperators([NotNull] Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);
            if (curve.Document == null)
            {
                yield break;    
            }

            foreach (Operator op in curve.Document.Patches.SelectMany(x => x.Operators))
            {
                if (op.GetOperatorTypeEnum() != OperatorTypeEnum.Curve)
                {
                    continue;
                }

                var wrapper = new Curve_OperatorWrapper(op, _curveRepository);

                if (wrapper.Curve == curve ||
                    wrapper.CurveID == curve.ID)
                {
                    yield return op;
                }
            }
        }
    }
}
