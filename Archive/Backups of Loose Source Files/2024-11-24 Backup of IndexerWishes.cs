﻿using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using System;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBeProtected.Global

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class FlowNode 
    { 
        /// <inheritdoc cref="docs._barindexer"/>
        public SynthWishes.BarIndexer bar => _synthWishes.bar;
        /// <inheritdoc cref="docs._barsindexer"/>
        public SynthWishes.BarsIndexer bars => _synthWishes.bars;
        /// <inheritdoc cref="docs._beatindexer"/>
        public SynthWishes.BeatIndexer beat => _synthWishes.beat;
        /// <inheritdoc cref="docs._beatindexer"/>
        public SynthWishes.BeatIndexer b => _synthWishes.b;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public SynthWishes.BeatsIndexer beats => _synthWishes.beats;
        /// <inheritdoc cref="docs._timeindexer"/>
        public SynthWishes.TimeIndexer t => _synthWishes.t;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public SynthWishes.BeatsIndexer l => _synthWishes.l;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public SynthWishes.BeatsIndexer len => _synthWishes.len;
        /// <inheritdoc cref="docs._beatsindexer"/>
        public SynthWishes.BeatsIndexer length  => _synthWishes.length;
        /// <inheritdoc cref="docs._captureindexer" />
        public SynthWishes.CaptureIndexer _ => _synthWishes._;
    }

    public partial class SynthWishes
    {
        public SynthWishes(IContext context, double beat = 1, double bar = 4)
            : this(context)
        {
            InitializeTimeIndexers(beat, bar);
        }

        public SynthWishes(double beat = 1, double bar = 4)
            : this(null, beat, bar)
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

        /// <inheritdoc cref="docs._barindexer"/>
        public BarIndexer bar { get; private set; }

        /// <inheritdoc cref="docs._barsindexer"/>
        public BarsIndexer bars { get; private set; }

        /// <inheritdoc cref="docs._beatindexer"/>
        public BeatIndexer beat { get; private set; }

        /// <inheritdoc cref="docs._beatindexer"/>
        public BeatIndexer b { get; private set; }

        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer beats { get; private set; }

        /// <inheritdoc cref="docs._timeindexer"/>
        public TimeIndexer t { get; private set; }

        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer l { get; private set; }

        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer len { get; private set; }

        /// <inheritdoc cref="docs._beatsindexer"/>
        public BeatsIndexer length { get; private set; }

        /// <inheritdoc cref="docs._barindexer"/>
        public class BarIndexer
        {
            private readonly SynthWishes _parent;
            private readonly double _barDuration;

            /// <inheritdoc cref="docs._barindexer"/>
            internal BarIndexer(SynthWishes parent, double barDuration)
            {
                _parent = parent;
                _barDuration = barDuration;
            }

            /// <inheritdoc cref="docs._barindexer"/>
            public FlowNode this[double count]
                => _parent._[(count - 1) * _barDuration];
        }

        /// <inheritdoc cref="docs._barsindexer"/>
        public class BarsIndexer
        {
            private readonly SynthWishes _parent;
            private readonly double _barDuration;

            /// <inheritdoc cref="docs._barsindexer"/>
            internal BarsIndexer(SynthWishes parent, double barDuration)
            {
                _parent = parent;
                _barDuration = barDuration;
            }

            /// <inheritdoc cref="docs._barsindexer"/>
            public FlowNode this[double count]
                => _parent._[count * _barDuration];
        }

        /// <inheritdoc cref="docs._beatindexer"/>
        public class BeatIndexer
        {
            private readonly SynthWishes x;
            private readonly double _beatDuration;

            /// <inheritdoc cref="docs._beatindexer"/>
            internal BeatIndexer(SynthWishes parent, double beatDuration)
            {
                x = parent;
                _beatDuration = beatDuration;
            }

            /// <inheritdoc cref="docs._beatindexer"/>
            public FlowNode this[double count]
            {
                get
                {
                    double value = (count - 1) * _beatDuration;
                    return x._[value];
                }
            }
        }

        /// <inheritdoc cref="docs._beatsindexer"/>
        public class BeatsIndexer
        {
            private readonly SynthWishes x;
            private readonly double _beatLength;

            /// <inheritdoc cref="docs._beatsindexer"/>
            internal BeatsIndexer(SynthWishes parent, double beatLength)
            {
                x = parent;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="docs._beatsindexer"/>
            public FlowNode this[double count]
                => x._[count * _beatLength];

            /// <inheritdoc cref="docs._beatsindexer"/>
            public FlowNode this[FlowNode count]
                => count * _beatLength;
        }

        /// <inheritdoc cref="docs._timeindexer"/>
        public class TimeIndexer
        {
            private readonly SynthWishes x;
            private readonly double _barLength;
            private readonly double _beatLength;

            /// <inheritdoc cref="docs._timeindexer"/>
            internal TimeIndexer(SynthWishes parent, double barLength, double beatLength)
            {
                x = parent;
                _barLength = barLength;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="docs._timeindexer"/>
            public FlowNode this[double bar, double beat]
            {
                get
                {
                    var result = (bar - 1) * _barLength + (beat - 1) * _beatLength;
                    return x._[result];
                }
            }

            /// <inheritdoc cref="docs._timeindexer"/>
            public FlowNode this[FlowNode bar, FlowNode beat]
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
            private readonly SynthWishes _synthWishes;

            /// <inheritdoc cref="docs._captureindexer" />
            internal CaptureIndexer(SynthWishes synthWishes)
            {
                _synthWishes = synthWishes;
            }

            // For Value Operators
            
            /// <inheritdoc cref="docs._captureindexer" />
            public FlowNode this[double value] => new FlowNode(_synthWishes, _synthWishes._operatorFactory.Value(value));

            // Turn Outlet into FlowNode
            
            /// <inheritdoc cref="docs._captureindexer" />
            public FlowNode this[Outlet outlet]
            {
                get
                {
                    if (outlet == null) throw new Exception(
                        "Outlet is null in the capture indexer _[myOutlet]. " +
                        "This indexer is meant to wrap something into a FlowNode so you can " +
                        "use fluent method chaining and C# operator overloads.");
                    
                    return new FlowNode(_synthWishes, outlet); 
                }
            }
            
            // For Note Arrangements
            
            // _[ t[1, 1], Flute(A4, l[1], fx: _[0.14]), MyEnvelope ]
            // _[ t[1, 3], Flute(C4, l[2], fx: _[0.25]), MyEnvelope ]
            
            public FlowNode this[FlowNode t, FlowNode sound, FlowNode volume = null] 
                => _synthWishes.StrikeNote(sound, t, volume);

            // _[ t[1, 1], Flute(A4, l[1], _[0.14]), 0.8 ]
            // _[ t[1, 2], Flute(C4, l[2], _[0.25]), 1.0 ]

            public FlowNode this[FlowNode t, FlowNode sound, double volume] 
                => _synthWishes.StrikeNote(sound, t, volume);
            
            // _[ t[1, 1], A4, Flute, MyEnvelope, l[0.5] ]
            // _[ t[1, 2], C4, Flute, MyEnvelope, l[1.0] ]

            public FlowNode this[
                FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound, FlowNode vol = null, FlowNode param2 = null] 
                => _synthWishes.StrikeNote(sound(freq, param2), t, vol);

            // _[ t[1, 1], A4, Flute, 0.8, l(0.5) ]
            // _[ t[1, 2], C4, Flute, 1.0, l(1.0) ]

            public FlowNode this[
                FlowNode t, FlowNode freq, Func<FlowNode, FlowNode, FlowNode> sound, double vol, FlowNode param2 = null] 
                => _synthWishes.StrikeNote(sound(freq, param2), t, vol);

            // _[ t[1, 1], A4, Flute, MyEnvelope, l(0.5), _[0.14] ]
            // _[ t[1, 2], C4, Flute, MyEnvelope, l(1.0), _[0.25] ]

            public FlowNode this[
                FlowNode t, FlowNode freq, 
                Func<FlowNode, FlowNode, FlowNode, FlowNode> sound, FlowNode vol = null, 
                FlowNode param2 = null, FlowNode param3 = null] 
                => _synthWishes.StrikeNote(sound(freq, param2, param3), t, vol);

            // _[ t[1, 1], A4, Flute, 0.8, l(0.5), _[0.14] ]
            // _[ t[1, 2], C4, Flute, 1.0, l(1.0), _[0.14] ]

            public FlowNode this[
                FlowNode t, FlowNode freq, 
                Func<FlowNode, FlowNode, FlowNode, FlowNode> sound, double vol, 
                FlowNode param2 = null, FlowNode param3 = null] 
                => _synthWishes.StrikeNote(sound(freq, param2, param3), t, vol);

        
            // _[ t[1, 1], A4, Flute, MyEnvelope, l(0.5), _[0.14], _[1.08] ]
            // _[ t[1, 2], C4, Flute, MyEnvelope, l(1.0), _[0.25] ]

            public FlowNode this[
                FlowNode t, FlowNode freq, 
                Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound, FlowNode vol = null,
                FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null] 
                => _synthWishes.StrikeNote(sound(freq, param2, param3, param4), t, vol);

            // _[ t[1, 1], A4, Flute, 0.8, l(0.5), _[0.14], _[1.08] ]
            // _[ t[1, 2], C4, Flute, 1.0, l(1.0), _[0.14], _[1.02]]

            public FlowNode this[
                FlowNode t, FlowNode freq, 
                Func<FlowNode, FlowNode, FlowNode, FlowNode, FlowNode> sound, double vol, 
                FlowNode param2 = null, FlowNode param3 = null, FlowNode param4 = null] 
                => _synthWishes.StrikeNote(sound(freq, param2, param3, param4), t, vol);
        }
    }
}