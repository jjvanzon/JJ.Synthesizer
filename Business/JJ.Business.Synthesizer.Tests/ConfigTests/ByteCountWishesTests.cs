using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    public class ByteCountWishesTests
    {
        [TestMethod]
        public void ByteCount_ConversionFormula_Basics()
        {
            // FromAudioLength
            {
                const double audioLength    = 0.5;
                const int    samplingRate   = 24000;
                const int    bits           = 16;
                const int    channels       = 2;
                const int    headerLength   = 44;
                const int    courtesyFrames = 3;
                
                int expected = ((int)(audioLength * samplingRate) + courtesyFrames) * bits / 8 * channels + headerLength;
                
                AreEqual(expected, () => audioLength.ByteCount(samplingRate, bits, channels, headerLength, courtesyFrames));
            }
        }
    }
}
