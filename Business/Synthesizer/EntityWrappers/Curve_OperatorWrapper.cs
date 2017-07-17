using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Exceptions;
using JJ.Data.Synthesizer.Entities;
using JJ.Data.Synthesizer.RepositoryInterfaces;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Curve_OperatorWrapper : OperatorWrapper
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
                return curveID.HasValue ? _curveRepository.Get(curveID.Value) : null;
            }
            set => CurveID = value?.ID;
        }
    }
}
