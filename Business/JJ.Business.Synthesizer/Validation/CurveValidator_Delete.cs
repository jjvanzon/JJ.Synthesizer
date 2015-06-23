using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Names;
using JJ.Business.Synthesizer.Resources;
using JJ.Business.Synthesizer.Extensions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using JJ.Framework.Reflection.Exceptions;
using JJ.Framework.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.Validation
{
    internal class CurveValidator_Delete : FluentValidator<Curve>
    {
        private ICurveRepository _curveRepository;

        public CurveValidator_Delete(Curve curve, ICurveRepository curveRepository)
            : base(curve)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);

            _curveRepository = curveRepository;
        }

        protected override void Execute()
        {
            Curve curve = Object;

            bool hasOperators = EnumerateCurveInOperators(curve).Any();
            if (hasOperators)
            {
                // TODO: It might be handy to know what patch and possibly what operator still uses it.
                ValidationMessages.Add(PropertyNames.Curve, MessageFormatter.CannotDeleteCurveBecauseHasOperators(curve.Name));
            }
        }

        private IEnumerable<Operator> EnumerateCurveInOperators(Curve curve)
        {
            if (curve == null) throw new NullException(() => curve);

            foreach (Operator op in curve.Document.Patches.SelectMany(x => x.Operators))
            {
                if (op.GetOperatorTypeEnum() != OperatorTypeEnum.CurveIn)
                {
                    continue;
                }

                var wrapper = new CurveIn_OperatorWrapper(op, _curveRepository);

                if (wrapper.Curve == curve ||
                    wrapper.CurveID == curve.ID)
                {
                    yield return op;
                }
            }
        }
    }
}
