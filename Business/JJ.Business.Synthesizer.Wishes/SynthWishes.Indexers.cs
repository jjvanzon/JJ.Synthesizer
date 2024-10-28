using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using System;

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
            b = new BeatIndexer(this, beatDuration);
            beats = new BeatsIndexer(this, beatDuration);
            l = new BeatsIndexer(this, beatDuration);
            len = new BeatsIndexer(this, beatDuration);
            length = new BeatsIndexer(this, beatDuration);
            t = new TimeIndexer(this, barDuration, beatDuration);
        }

        /// <inheritdoc cref="BarIndexer" />
        public BarIndexer bar { get; private set; }

        /// <inheritdoc cref="BarsIndexer" />
        public BarsIndexer bars { get; private set; }

        /// <inheritdoc cref="BeatIndexer" />
        public BeatIndexer beat { get; private set; }

        /// <inheritdoc cref="BeatIndexer" />
        public BeatIndexer b { get; private set; }

        /// <inheritdoc cref="BeatsIndexer" />
        public BeatsIndexer beats { get; private set; }

        /// <inheritdoc cref="TimeIndexer" />
        public TimeIndexer t { get; private set; }

        /// <inheritdoc cref="BeatsIndexer" />
        public BeatsIndexer l { get; private set; }

        /// <inheritdoc cref="BeatsIndexer" />
        public BeatsIndexer len { get; private set; }

        /// <inheritdoc cref="BeatsIndexer" />
        public BeatsIndexer length { get; private set; }

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
                        
        /// <inheritdoc cref="docs._captureindexer" />
        public CaptureIndexer _;

        /// <inheritdoc cref="docs._captureindexer" />
        public class CaptureIndexer
        {
            private readonly SynthWishes _parent;

            /// <inheritdoc cref="docs._captureindexer" />
            internal CaptureIndexer(SynthWishes parent)
            {
                _parent = parent;
            }

            // For Value Operators
            
            /// <inheritdoc cref="docs._captureindexer" />
            public FluentOutlet this[double value] => new FluentOutlet(_parent, _parent._operatorFactory.Value(value));

            // Turn Outlet into FluentOutlet
            
            /// <inheritdoc cref="docs._captureindexer" />
            public FluentOutlet this[Outlet outlet]
            {
                get
                {
                    if (outlet == null) throw new Exception(
                        "Outlet is null in the capture indexer _[myOutlet]. " +
                        "This indexer is meant to wrap something into a FluentOutlet so you can " +
                        "use fluent method chaining and C# operator overloads.");
                    
                    return new FluentOutlet(_parent, outlet); 
                }
            }
            
            // For Note Arrangements
            
            // _[ t[1, 1], Flute(A4, l[1], fx: _[0.14]), MyEnvelope ]
            // _[ t[1, 3], Flute(C4, l[2], fx: _[0.25]), MyEnvelope ]
            
            public FluentOutlet this[FluentOutlet t, FluentOutlet sound, FluentOutlet volume = null] 
                => _parent.StrikeNote(sound, t, volume);

            // _[ t[1, 1], Flute(A4, l[1], _[0.14]), 0.8 ]
            // _[ t[1, 2], Flute(C4, l[2], _[0.25]), 1.0 ]

            public FluentOutlet this[FluentOutlet t, FluentOutlet sound, double volume] 
                => _parent.StrikeNote(sound, t, volume);
            
            // _[ t[1, 1], A4, Flute, MyEnvelope, l[0.5] ]
            // _[ t[1, 2], C4, Flute, MyEnvelope, l[1.0] ]

            public FluentOutlet this[
                FluentOutlet t, FluentOutlet freq, Func<FluentOutlet, FluentOutlet, FluentOutlet> sound, FluentOutlet vol = null, FluentOutlet param2 = null] 
                => _parent.StrikeNote(sound(freq, param2), t, vol);

            // _[ t[1, 1], A4, Flute, 0.8, l(0.5) ]
            // _[ t[1, 2], C4, Flute, 1.0, l(1.0) ]

            public FluentOutlet this[
                FluentOutlet t, FluentOutlet freq, Func<FluentOutlet, FluentOutlet, FluentOutlet> sound, double vol, FluentOutlet param2 = null] 
                => _parent.StrikeNote(sound(freq, param2), t, vol);

            // _[ t[1, 1], A4, Flute, MyEnvelope, l(0.5), _[0.14] ]
            // _[ t[1, 2], C4, Flute, MyEnvelope, l(1.0), _[0.25] ]

            public FluentOutlet this[
                FluentOutlet t, FluentOutlet freq, Func<FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet> sound, FluentOutlet vol = null, FluentOutlet param2 = null, FluentOutlet param3 = null] 
                => _parent.StrikeNote(sound(freq, param2, param3), t, vol);

            // _[ t[1, 1], A4, Flute, 0.8, l(0.5), _[0.14] ]
            // _[ t[1, 2], C4, Flute, 1.0, l(1.0), _[0.14] ]

            public FluentOutlet this[
                FluentOutlet t, FluentOutlet freq, Func<FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet> sound, double vol, FluentOutlet param2 = null, FluentOutlet param3 = null] 
                => _parent.StrikeNote(sound(freq, param2, param3), t, vol);
        }
    }
}