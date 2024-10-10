namespace JJ.Business.Synthesizer.Tests.Wishes
{
    public partial class SynthSugarBase
    {
        private void InitializeNoteWishes(double beat, double bar)
        {
            this.bar = new BarIndexer(this, bar);
            bars = new BarsIndexer(this, bar);
            this.beat = new BeatIndexer(this, beat);
            beats = new BeatsIndexer(this, beat);
            t = new TimeIndexer(this, bar, beat);
        }

        // ReSharper disable InconsistentNaming

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

        // ReSharper restore InconsistentNaming

    }
}
