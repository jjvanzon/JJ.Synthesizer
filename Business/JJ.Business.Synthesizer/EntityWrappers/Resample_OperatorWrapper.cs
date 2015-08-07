using JJ.Framework.Reflection.Exceptions;
using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Helpers;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Resample_OperatorWrapper : OperatorWrapperBase
    {
        public Resample_OperatorWrapper(Operator op)
            : base(op)
        { }

        public Outlet Signal
        {
            get { return GetInlet(OperatorConstants.RESAMPLE_SIGNAL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.RESAMPLE_SIGNAL_INDEX).LinkTo(value); }
        }

        public Outlet SamplingRate
        {
            get { return GetInlet(OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.RESAMPLE_SAMPLING_RATE_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.RESAMPLE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(Resample_OperatorWrapper wrapper)
        {
            if (wrapper == null) throw new NullException(() => wrapper);

            return wrapper.Result;
        }
    }
}
