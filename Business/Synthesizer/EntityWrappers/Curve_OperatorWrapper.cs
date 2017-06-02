using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Curve_OperatorWrapper : OperatorWrapperBase_WithResultOutlet
    {
        private readonly ICurveRepository _curveRepository;

        public Curve_OperatorWrapper(Operator op, ICurveRepository curveRepository)
            : base(op)
        {
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
        }

        public int? CurveID
        {
            get => DataPropertyParser.TryGetInt32(WrappedOperator, nameof(CurveID));
            set => DataPropertyParser.SetValue(WrappedOperator, nameof(CurveID), value);
        }

        /// <summary> nullable </summary>
        public Curve Curve
        {
            get
            {
                int? curveID = CurveID;
                if (!curveID.HasValue)
                {
                    return null;
                }

                return _curveRepository.Get(curveID.Value);
            }
            set
            {
                if (value == null)
                {
                    CurveID = null;
                    return;
                }

                CurveID = value.ID;
            }
        }

        public override string GetInletDisplayName(Inlet inlet)
        {
            throw new NotSupportedException();
        }
    }
}
