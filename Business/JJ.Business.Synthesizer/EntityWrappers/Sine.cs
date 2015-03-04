using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.LinkTo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJ.Business.Synthesizer.EntityWrappers
{
    public class Sine : OperatorWrapperBase
    {
        public const int VOLUME_INDEX = 0;
        public const int PITCH_INDEX = 1;
        public const int LEVEL_INDEX = 2;
        public const int PHASE_START_INDEX = 3;
        public const int RESULT_INDEX = 0;

        public Sine(Operator op)
            :base(op)
        { }

        public Outlet Volume
        {
            get { return _operator.Inlets[VOLUME_INDEX].Input; }
            set { _operator.Inlets[VOLUME_INDEX].LinkTo(value); }
        }

        public Outlet Pitch
        {
            get { return _operator.Inlets[PITCH_INDEX].Input; }
            set { _operator.Inlets[PITCH_INDEX].LinkTo(value); }
        }

        public Outlet Level
        {
            get { return _operator.Inlets[LEVEL_INDEX].Input; }
            set { _operator.Inlets[LEVEL_INDEX].LinkTo(value); }
        }

        public Outlet PhaseStart
        {
            get { return _operator.Inlets[PHASE_START_INDEX].Input; }
            set { _operator.Inlets[PHASE_START_INDEX].LinkTo(value); }
        }

        public Outlet Result
        {
            get { return _operator.Outlets[RESULT_INDEX]; }
        }

        public static implicit operator Outlet(Sine wrapper)
        {
            return wrapper.Result;
        }
    }
}
