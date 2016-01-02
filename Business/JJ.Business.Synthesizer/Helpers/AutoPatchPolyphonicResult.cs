using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Data.Synthesizer;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    public class AutoPatchPolyphonicResult
    {
        public AutoPatchPolyphonicResult(
            Outlet signalOutlet,
            IList<string> volumeInletNames,
            IList<string> frequencyInletNames,
            IList<string> delayInletNames)
        {
            if (signalOutlet == null) throw new NullException(() => signalOutlet);
            if (volumeInletNames == null) throw new NullException(() => volumeInletNames);
            if (frequencyInletNames == null) throw new NullException(() => frequencyInletNames);
            if (delayInletNames == null) throw new NullException(() => delayInletNames);

            SignalOutlet = signalOutlet;
            VolumeInletNames = volumeInletNames.ToArray();
            FrequencyInletNames = frequencyInletNames.ToArray();
            DelayInletNames = delayInletNames.ToArray();
        }

        public Outlet SignalOutlet { get; private set; }
        public IList<string> VolumeInletNames { get; private set; }
        public IList<string> FrequencyInletNames { get; private set; }
        public IList<string> DelayInletNames { get; private set; }
    }
}
