using System;
using JetBrains.Annotations;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal class DocComments
    {
        /// <param name="freq"> The base frequency of the sound in Hz (default is A4/440Hz). </param>
        /// <param name="frequency"> The base frequency of the sound in Hz (default is A4/440Hz). </param>
        /// <param name="delay"> The time delay in seconds before the sound starts (default is 0). </param>
        /// <param name="vol"> The volume of the sound (default is 1). </param>
        /// <param name="volume"> The volume of the sound (default is 1). </param>
        /// <param name="duration"> The duration of the sound in seconds (default is 1). </param>
        /// <param name="soundFreq"> The base frequency in Hz for the carrier signal for the FM synthesis. </param>
        /// <param name="modSpeed"> The speed of the modulator in Hz. Determines much of the timbre. </param>
        /// <param name="modDepth"> The depth of the modulator. The higher the value, the more harmonic complexity. </param>
        /// <param name="sound"> The sound to be shaped. </param>
        /// <param name="tremoloSpeed">The speed in Hz at which the volume goes up and down. A typical value can be 8 Hz.</param>
        /// <param name="tremoloDepth">
        /// The factor by which the volume decreases and increases periodically.
        /// A good default might be a factor of 0.33 of the volume.
        /// </param>
        /// <param name="vibratoSpeed">The speed of the vibrato modulation. A typical value might be 5.5.</param>
        /// <param name="vibratoDepth">The depth of the vibrato modulation. Typical values might include 0.0005.</param>
        /// <returns> An Outlet representing the output sound. </returns>
        [UsedImplicitly]
        public static object Default { get;set; }
    }
}