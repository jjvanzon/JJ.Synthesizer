using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBeProtected.Global

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public SynthWishes(IContext context, double beat = 1, double bar = 4)
            : this(context)
        {
            InitializeTimeIndexers(beat, bar);
        }

        public SynthWishes(double beat = 1, double bar = 4)
            : this()
        {
            InitializeTimeIndexers(beat, bar);
        }

        private void InitializeTimeIndexers(double beatDuration, double barDuration)
        {
            bar = new BarIndexer(this, barDuration);
            bars = new BarsIndexer(this, barDuration);
            beat = new BeatIndexer(this, beatDuration);
            beats = new BeatsIndexer(this, beatDuration);
            t = new TimeIndexer(this, barDuration, beatDuration);
            l = new NoteLengthIndexer(this, beatDuration);
        }

        /// <inheritdoc cref="BarIndexer" />
        public BarIndexer bar { get; private set; }

        /// <inheritdoc cref="BarsIndexer" />
        public BarsIndexer bars { get; private set; }

        /// <inheritdoc cref="BeatIndexer" />
        public BeatIndexer beat { get; private set; }

        /// <inheritdoc cref="BeatsIndexer" />
        public BeatsIndexer beats { get; private set; }

        /// <inheritdoc cref="TimeIndexer" />
        public TimeIndexer t { get; private set; }

        /// <inheritdoc cref="NoteLengthIndexer" />
        public NoteLengthIndexer l { get; private set; }

        /// <summary>
        /// Returns the time in seconds of the start of a bar.
        /// </summary>
        /// <inheritdoc cref="docs._timeindexer"/>
        public class BarIndexer
        {
            private readonly SynthWishes _parent;
            private readonly double _barDuration;

            /// <inheritdoc cref="BarIndexer" />
            internal BarIndexer(SynthWishes parent, double barDuration)
            {
                _parent = parent;
                _barDuration = barDuration;
            }

            /// <inheritdoc cref="BarIndexer" />
            public FluentOutlet this[double count]
                => _parent._[(count - 1) * _barDuration];
        }

        /// <summary>
        /// Returns duration of a number of bars in seconds.<br />
        /// </summary>
        /// <inheritdoc cref="docs._timeindexer"/>
        public class BarsIndexer
        {
            private readonly SynthWishes _parent;
            private readonly double _barDuration;

            /// <inheritdoc cref="BarsIndexer" />
            internal BarsIndexer(SynthWishes parent, double barDuration)
            {
                _parent = parent;
                _barDuration = barDuration;
            }

            /// <inheritdoc cref="BarsIndexer" />
            public FluentOutlet this[double count]
                => _parent._[count * _barDuration];
        }

        /// <summary>
        /// Returns the start time of a beat in seconds.
        /// </summary>
        /// <inheritdoc cref="docs._timeindexer"/>
        public class BeatIndexer
        {
            private readonly SynthWishes x;
            private readonly double _beatDuration;

            /// <inheritdoc cref="BeatIndexer" />
            internal BeatIndexer(SynthWishes parent, double beatDuration)
            {
                x = parent;
                _beatDuration = beatDuration;
            }

            /// <inheritdoc cref="BeatIndexer" />
            public FluentOutlet this[double count]
            {
                get
                {
                    double value = (count - 1) * _beatDuration;
                    return x._[value];
                }
            }
        }

        /// <summary>
        /// Returns duration of a number of beats in seconds.
        /// </summary>
        /// <inheritdoc cref="docs._timeindexer"/>
        public class BeatsIndexer
        {
            private readonly SynthWishes x;
            private readonly double _beatLength;

            /// <inheritdoc cref="BeatsIndexer" />
            internal BeatsIndexer(SynthWishes parent, double beatLength)
            {
                x = parent;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="BeatsIndexer" />
            public FluentOutlet this[double count]
                => x._[count * _beatLength];

            /// <inheritdoc cref="BeatsIndexer" />
            public FluentOutlet this[FluentOutlet count]
                => count * _beatLength;
        }

        /// <summary>
        /// This TimeIndexer provides shorthand for specifying bar and beat in a musical sense.
        /// Access by bar and beat to get time-based value.
        /// Example usage: t[bar: 2, beat: 1.5] will return the number of seconds.
        /// The numbers are 1-based, so the first bar is bar 1, the first beat is beat 1.
        /// </summary>
        /// <inheritdoc cref="docs._timeindexer"/>
        public class TimeIndexer
        {
            private readonly SynthWishes x;
            private readonly double _barLength;
            private readonly double _beatLength;

            internal TimeIndexer(SynthWishes parent, double barLength, double beatLength)
            {
                x = parent;
                _barLength = barLength;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="TimeIndexer" />
            public FluentOutlet this[double bar, double beat]
            {
                get
                {
                    var result = (bar - 1) * _barLength + (beat - 1) * _beatLength;
                    return x._[result];
                }
            }

            /// <inheritdoc cref="TimeIndexer" />
            public FluentOutlet this[FluentOutlet bar, FluentOutlet beat]
            {
                get
                {
                    var result = (bar - 1) * _barLength + (beat - 1) * _beatLength;
                    return result;
                }
            }
        }

        public class NoteLengthIndexer
        { 
            private readonly SynthWishes x;
            private readonly double _beatLength;

            internal NoteLengthIndexer(SynthWishes parent, double beatLength)
            {
                x = parent;
                _beatLength = beatLength;
            }
            
            // l[3] alternative for beats[4], for shorter duration notation.

            /// <inheritdoc cref="TimeIndexer" />
            public FluentOutlet this[double beats] => x.beats[beats];

            /// <inheritdoc cref="TimeIndexer" />
            public FluentOutlet this[FluentOutlet beats] => x.beats[beats];
        }
    }
}