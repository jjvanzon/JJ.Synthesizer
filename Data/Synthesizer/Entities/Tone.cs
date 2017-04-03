using JetBrains.Annotations;

namespace JJ.Data.Synthesizer.Entities
{
    public class Tone
    {
        public virtual int ID { get; set; }

        /// <summary> parent, not nullable </summary>
        [NotNull]
        public virtual Scale Scale { get; set; }

        /// <summary>
        /// 0-based. But can freely range from the positive to the negative.
        /// Not only a mere indicator of the octave position within the tone scale,
        /// but often also used in the calculation of the absolute frequency.
        /// </summary>
        public virtual int Octave { get; set; }

        /// <summary>
        /// Depending on Scale.ScaleType this is either the 
        /// frequency, factor, exponential grade or a fraction of a semitone.
        /// 
        /// It is 1-based for semitones. The exponential grade is one that goes from 0 to 1 between
        /// this octace and the next. But you can freely range from the positive to the negative.
        /// 
        /// This number is also a substitute for the ordinal number within an octave.
        /// You can derive an integer number from it by sorting it and taking the list position.
        /// </summary>
        public virtual double Number { get; set; }
    }
}
