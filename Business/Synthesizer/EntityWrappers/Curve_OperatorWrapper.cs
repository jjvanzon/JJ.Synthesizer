using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Data.Synthesizer.DefaultRepositories.Interfaces;
using System;
using JJ.Business.Synthesizer.Resources;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Curve_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private readonly ICurveRepository _curveRepository;

        public Curve_OperatorWrapper(Operator op, ICurveRepository curveRepository)
            : base(op)
        {
            if (curveRepository == null) throw new NullException(() => curveRepository);
            _curveRepository = curveRepository;
        }

        public int? CurveID
        {
            get { return DataPropertyParser.TryGetInt32(WrappedOperator, PropertyNames.CurveID); }
            set { DataPropertyParser.SetValue(WrappedOperator, PropertyNames.CurveID, value); }
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

        public override string GetInletDisplayName(int listIndex)
        {
            throw new NotSupportedException();
        }
    }
}
