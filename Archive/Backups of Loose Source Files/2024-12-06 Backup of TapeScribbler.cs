using System;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    /// <inheritdoc cref="docs._tapecopyscribbler" />
    internal static class TapeScribbler
    {
        ///// <inheritdoc cref="docs._tapecopyscribbler" />
        //public static Tape CopyMetaData(Tape original)
        //{
        //    var copy = new Tape();
        //    CopyMetaData(original, copy);
        //    return copy;
        //}

        /// <inheritdoc cref="docs._tapecopyscribbler" />
        public static void CopyMetaData(Tape original, Tape copy)
        {
            if (original == null) throw new ArgumentNullException(nameof(original));
            if (copy == null) throw new ArgumentNullException(nameof(copy));
            copy.Duration = original.Duration;
            copy.FallBackName = original.FallBackName;
            copy.IsPlay = original.IsPlay;
            copy.IsSave = original.IsSave;
            copy.IsCache = original.IsCache;
            copy.IsPadding = original.IsPadding;
            copy.FilePath = original.FilePath;
        }
    }
}
