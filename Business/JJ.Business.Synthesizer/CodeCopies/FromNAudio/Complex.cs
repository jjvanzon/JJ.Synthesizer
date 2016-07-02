// Jan-Joost van Zon, 2016-06-25:
// And made class internal.

using System;
using System.Collections.Generic;
using System.Text;

namespace NAudio.Dsp
{
    /// <summary>
    /// Type to represent complex number
    /// </summary>
    internal struct Complex
    {
        /// <summary>
        /// Real Part
        /// </summary>
        public float X;
        /// <summary>
        /// Imaginary Part
        /// </summary>
        public float Y;
    }
}
