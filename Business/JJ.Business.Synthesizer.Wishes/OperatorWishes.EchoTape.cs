using System.Collections.Generic;
using System.Runtime.CompilerServices;
namespace JJ.Business.Synthesizer.Wishes
{
    // Echo(Tape)

    public partial class SynthWishes
    {
        public FluentOutlet EchoTape(
            FluentOutlet signal, int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default,
            [CallerMemberName] string callerMemberName = null)
        {
            magnitude = magnitude ?? _[0.66];
            delay = delay ?? _[0.25];

            var tape = Tape(signal, callerMemberName);

            var cumulativeMagnitude = _[1];
            var cumulativeDelay = _[0];

            var repeats = new List<FluentOutlet>(count);

            for (int i = 0; i < count; i++)
            {
                var quieter = tape * cumulativeMagnitude;
                var shifted = Delay(quieter, cumulativeDelay);

                repeats.Add(shifted);

                cumulativeMagnitude *= magnitude;
                cumulativeDelay += delay;
            }

            // TODO: Go parallel?
            return Add(repeats).SetName();
        }

        public FluentOutlet Tape(FluentOutlet signal, [CallerMemberName] string callerMemberName = null)
            => Tape(signal, default, callerMemberName);

        public FluentOutlet Tape(
            FluentOutlet signal, FluentOutlet duration,
            [CallerMemberName] string callerMemberName = null)
        {
            duration = duration ?? GetAudioLength ?? _[1];

            var cacheResult = Cache(signal, callerMemberName);
            var sample = Sample(cacheResult);
            return sample;
        }
    }

    public partial class FluentOutlet
    {
        public FluentOutlet EchoTape(int count = 4, FluentOutlet magnitude = default, FluentOutlet delay = default, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.EchoTape(this, count, magnitude, delay, callerMemberName);

        public FluentOutlet Tape(FluentOutlet duration = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Tape(this, duration, callerMemberName);
    }
}