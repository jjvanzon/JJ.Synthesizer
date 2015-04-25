using JJ.Data.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Constants;
using JJ.Framework.Validation;
using JJ.Business.Synthesizer.Validation.Entities;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class SineWrapper : OperatorWrapperBase
    {
        public SineWrapper(Operator op)
            : base(op)
        { }

        public Outlet Volume
        {
            get { return GetInlet(OperatorConstants.SINE_VOLUME_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SINE_VOLUME_INDEX).LinkTo(value); }
        }

        public Outlet Pitch
        {
            get { return GetInlet(OperatorConstants.SINE_PITCH_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SINE_PITCH_INDEX).LinkTo(value); }
        }

        public Outlet Level
        {
            get { return GetInlet(OperatorConstants.SINE_LEVEL_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SINE_LEVEL_INDEX).LinkTo(value); }
        }

        public Outlet PhaseStart
        {
            get { return GetInlet(OperatorConstants.SINE_PHASE_START_INDEX).InputOutlet; }
            set { GetInlet(OperatorConstants.SINE_PHASE_START_INDEX).LinkTo(value); }
        }

        public Outlet Result
        {
            get { return GetOutlet(OperatorConstants.SINE_RESULT_INDEX); }
        }

        public static implicit operator Outlet(SineWrapper wrapper)
        {
            return wrapper.Result;
        }
    }
}