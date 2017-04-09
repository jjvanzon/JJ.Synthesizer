using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer;
using System;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Curve_OperatorWrapper : OperatorWrapperBase_WithResult
    {
        private readonly ICurveRepository _curveRepository;

        public Curve_OperatorWrapper(Operator op, ICurveRepository curveRepository)
            : base(op)
        {
            _curveRepository = curveRepository ?? throw new NullException(() => curveRepository);
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
