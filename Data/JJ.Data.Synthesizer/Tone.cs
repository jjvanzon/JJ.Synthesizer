using System;
using System.Collections.Generic;
using System.Linq;

namespace JJ.Data.Synthesizer
{
    public class Tone
    {
        public virtual int ID { get; set; }

        /// <summary> parent, not nullable summary>
        public virtual Scale Scale { get; set; }

        /// <summary>
        /// 1-based.
        /// Not only a mere indicator of the octave position within the tone scale,
        /// but often also used in the calculation of the absolute frequency.
        /// </summary>
        public virtual int Octave { get; set; }

        /// <summary>
        /// Depending on Scale.ScaleType this is either the 
        /// frequency, factor, exponential grade or a fraction of a semitone.
        /// 
        /// It is 1-based for semitones. The exponential grade is one that goes from 0 to 1 between
        /// this octace and the next.
        /// 
        /// This number is also a substitute for the ordinal number within an octave.
        /// You can derive an integer number from it by sorting it and taking the list position.
        /// </summary>
        public virtual double Number { get; set; }
    }
}
