﻿using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Persistence;

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBeProtected.Global

namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        public SynthSugarBase(IContext context, double beat = 1, double bar = 4)
            : this(context)
        {
            this.bar = new BarIndexer(this, bar);
            bars = new BarsIndexer(this, bar);
            this.beat = new BeatIndexer(this, beat);
            beats = new BeatsIndexer(this, beat);
            t = new TimeIndexer(this, bar, beat);
        }

        /// <inheritdoc cref="BarIndexer" />
        public BarIndexer bar { get; }

        /// <inheritdoc cref="BarsIndexer" />
        public BarsIndexer bars { get; }

        /// <inheritdoc cref="BeatIndexer" />
        public BeatIndexer beat { get; }

        /// <inheritdoc cref="BeatsIndexer" />
        public BeatsIndexer beats { get; }

        /// <inheritdoc cref="TimeIndexer" />
        public TimeIndexer t { get; }

        /// <summary>
        /// Returns the time in seconds of the start of a bar.
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BarIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _barLength;

            /// <inheritdoc cref="BarIndexer" />
            internal BarIndexer(SynthSugarBase parent, double barLength)
            {
                _parent = parent;
                _barLength = barLength;
            }

            /// <inheritdoc cref="BarIndexer" />
            public ValueOperatorWrapper this[double count]
                => _parent.Value((count - 1) * _barLength);
        }

        /// <summary>
        /// Returns duration of a number of bars in seconds.<br />
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BarsIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _barLength;

            /// <inheritdoc cref="BarsIndexer" />
            internal BarsIndexer(SynthSugarBase parent, double barLength)
            {
                _parent = parent;
                _barLength = barLength;
            }

            /// <inheritdoc cref="BarsIndexer" />
            public ValueOperatorWrapper this[double count]
                => _parent.Value(count * _barLength);
        }

        /// <summary>
        /// Returns the start time of a beat in seconds.
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BeatIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _beatLength;

            /// <inheritdoc cref="BeatIndexer" />
            internal BeatIndexer(SynthSugarBase parent, double beatLength)
            {
                _parent = parent;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="BeatIndexer" />
            public ValueOperatorWrapper this[double count]
                => _parent.Value((count - 1) * _beatLength);
        }

        /// <summary>
        /// Returns duration of a number of beats in seconds.
        /// </summary>
        /// <returns> ValueOperatorWrapper which can also be used as an Outlet or a double. </returns>
        public class BeatsIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _beatLength;

            /// <inheritdoc cref="BeatsIndexer" />
            internal BeatsIndexer(SynthSugarBase parent, double beatLength)
            {
                _parent = parent;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="BeatsIndexer" />
            public ValueOperatorWrapper this[double count]
                => _parent.Value(count * _beatLength);
        }

        /// <summary>
        /// This TimeIndexer provides shorthand for specifying bar and beat in a musical sense.
        /// Access by bar and beat to get time-based value.
        /// Example usage: t[bar: 2, beat: 1.5] will return the number of seconds.
        /// The numbers are 1-based, so the first bar is bar 1, the first beat is beat 1.
        /// </summary>
        /// <returns> ValueOperatorWrapper also usable as Outlet or double. </returns>
        public class TimeIndexer
        {
            private readonly SynthSugarBase _parent;
            private readonly double _barLength;
            private readonly double _beatLength;

            internal TimeIndexer(SynthSugarBase parent, double barLength, double beatLength)
            {
                _parent = parent;
                _barLength = barLength;
                _beatLength = beatLength;
            }

            /// <inheritdoc cref="TimeIndexer" />
            public ValueOperatorWrapper this[double bar, double beat]
                => _parent.Value((bar - 1) * _barLength + (beat - 1) * _beatLength);
        }
    }
}