using System;

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._captureindexer" />
    public partial class CaptureIndexer
    {
        private readonly SynthWishes _synthWishes;

        /// <inheritdoc cref="docs._captureindexer" />
        internal CaptureIndexer(SynthWishes synthWishes)
            => _synthWishes = synthWishes;

        // ReSharper disable once UnusedParameter.Global
        /// <summary>
        /// Crazy conversion operator, reintroducing the discard notation _
        /// sort of, for FlowNodes.
        /// </summary>
        /// <param name="captureIndexer"></param>
        public static implicit operator FlowNode(CaptureIndexer captureIndexer) => null;
    }
}
