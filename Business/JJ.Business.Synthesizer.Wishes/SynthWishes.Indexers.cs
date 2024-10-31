using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using System;
using static JJ.Business.Synthesizer.Wishes.docs;

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

        /// <inheritdoc cref="_barindexer"/>
        public BarIndexer bar { get; private set; }

        /// <inheritdoc cref="_barsindexer"/>
        public BarsIndexer bars { get; private set; }

        /// <inheritdoc cref="_beatindexer"/>
        public BeatIndexer beat { get; private set; }

        /// <inheritdoc cref="_beatindexer"/>
        public BeatIndexer b { get; private set; }

        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer beats { get; private set; }

        /// <inheritdoc cref="_timeindexer"/>
        public TimeIndexer t { get; private set; }

        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer l { get; private set; }

        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer len { get; private set; }

        /// <inheritdoc cref="_beatsindexer"/>
        public BeatsIndexer length { get; private set; }

        /// <inheritdoc cref="_barindexer"/>
        public class BarIndexer
        {
            private readonly SynthWishes _parent;
            private readonly double _barDuration;

            /// <inheritdoc cref="_barindexer"/>
            internal BarIndexer(SynthWishes parent, double barDuration)
            {
                _parent = parent;
                _barDuration = barDuration;
            }

            /// <inheritdoc cref="_barindexer"/>
            public FluentOutlet this[double count]
                => _parent._[(count - 1) * _barDuration];
        }

        /// <inheritdoc cref="_barsindexer"/>
        public class BarsIndexer
        {
            private readonly SynthWishes _parent;
            private readonly double _barDuration;

            /// <inheritdoc cref="_barsindexer"/>
            internal BarsIndexer(SynthWishes parent, double barDuration)
            {
                _parent = parent;
                _barDuration = barDuration;
            }

            /// <inheritdoc cref="_barsindexer"/>
            public FluentOutlet this[double count]
                => _parent._[count * _barDuration];
        }

        /// <inheritdoc cref="_beatindexer"/>
        public class BeatIndexer
        {
            private readonly SynthWishes x;
            private readonly double _beatDuration;

            /// <inheritdoc cref="_beatindexer"/>
            internal BeatIndexer(SynthWishes parent, double beatDuration)
            {
                x = parent;
                _beatDuration = beatDuration;
            }

            /// <inheritdoc cref="_beatindexer"/>
            public FluentOutlet this[double count]
            {
                get
                {
                    double value = (count - 1) * _beatDuration;
                    return x._[value];
                }
            }
        }

        /// <inheritdoc cref="_beatsindexer"/>
        public class BeatsIndexer
        {
            private readonly SynthWishes x;
            private readonly double _beatLength;

            /// <inheritdoc cref="_beatsindexer"/>
            internal BeatsIndexer(SynthWishes parent, double beatLength)
            {
                x = parent;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="_beatsindexer"/>
            public FluentOutlet this[double count]
                => x._[count * _beatLength];

            /// <inheritdoc cref="_beatsindexer"/>
            public FluentOutlet this[FluentOutlet count]
                => count * _beatLength;
        }

        /// <inheritdoc cref="_timeindexer"/>
        public class TimeIndexer
        {
            private readonly SynthWishes x;
            private readonly double _barLength;
            private readonly double _beatLength;

            /// <inheritdoc cref="_timeindexer"/>
            internal TimeIndexer(SynthWishes parent, double barLength, double beatLength)
            {
                x = parent;
                _barLength = barLength;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="_timeindexer"/>
            public FluentOutlet this[double bar, double beat]
            {
                get
                {
                    var result = (bar - 1) * _barLength + (beat - 1) * _beatLength;
                    return x._[result];
                }
            }

            /// <inheritdoc cref="_timeindexer"/>
            public FluentOutlet this[FluentOutlet bar, FluentOutlet beat]
            {
                get
                {
                    var result = (bar - 1) * _barLength + (beat - 1) * _beatLength;
                    return result;
                }
            }
        }
                        
        /// <inheritdoc cref="_captureindexer" />
        public CaptureIndexer _;

        /// <inheritdoc cref="_captureindexer" />
        public class CaptureIndexer
        {
            private readonly SynthWishes _parent;

            /// <inheritdoc cref="_captureindexer" />
            internal CaptureIndexer(SynthWishes parent)
            {
                _parent = parent;
            }

            // For Value Operators
            
            /// <inheritdoc cref="_captureindexer" />
            public FluentOutlet this[double value] => new FluentOutlet(_parent, _parent._operatorFactory.Value(value));

            // Turn Outlet into FluentOutlet
            
            /// <inheritdoc cref="_captureindexer" />
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
                FluentOutlet t, FluentOutlet freq, 
                Func<FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet> sound, FluentOutlet vol = null, 
                FluentOutlet param2 = null, FluentOutlet param3 = null] 
                => _parent.StrikeNote(sound(freq, param2, param3), t, vol);

            // _[ t[1, 1], A4, Flute, 0.8, l(0.5), _[0.14] ]
            // _[ t[1, 2], C4, Flute, 1.0, l(1.0), _[0.14] ]

            public FluentOutlet this[
                FluentOutlet t, FluentOutlet freq, 
                Func<FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet> sound, double vol, 
                FluentOutlet param2 = null, FluentOutlet param3 = null] 
                => _parent.StrikeNote(sound(freq, param2, param3), t, vol);

        
            // _[ t[1, 1], A4, Flute, MyEnvelope, l(0.5), _[0.14], _[1.08] ]
            // _[ t[1, 2], C4, Flute, MyEnvelope, l(1.0), _[0.25] ]

            public FluentOutlet this[
                FluentOutlet t, FluentOutlet freq, 
                Func<FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet> sound, FluentOutlet vol = null,
                FluentOutlet param2 = null, FluentOutlet param3 = null, FluentOutlet param4 = null] 
                => _parent.StrikeNote(sound(freq, param2, param3, param4), t, vol);

            // _[ t[1, 1], A4, Flute, 0.8, l(0.5), _[0.14], _[1.08] ]
            // _[ t[1, 2], C4, Flute, 1.0, l(1.0), _[0.14], _[1.02]]

            public FluentOutlet this[
                FluentOutlet t, FluentOutlet freq, 
                Func<FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet, FluentOutlet> sound, double vol, 
                FluentOutlet param2 = null, FluentOutlet param3 = null, FluentOutlet param4 = null] 
                => _parent.StrikeNote(sound(freq, param2, param3, param4), t, vol);
        }
    }
}