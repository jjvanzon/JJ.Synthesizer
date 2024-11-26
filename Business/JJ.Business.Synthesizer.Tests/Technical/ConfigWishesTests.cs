using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;
// ReSharper disable PossibleInvalidOperationException

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class ConfigWishesTests : MySynthWishes
    {
        // BarLength
        
        [TestMethod]
        public void Test_Default_BarLength() => new ConfigWishesTests().Default_BarLength();
        
        void Default_BarLength()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            IsNotNull(() => GetBarLength.AsConst);
            AreEqual(1.0, () => GetBarLength.AsConst.Value);
        }
        
        [TestMethod]
        public void Test_Explicit_BarLength() => new ConfigWishesTests().Explicit_BarLength();
        
        void Explicit_BarLength()
        {
            // WithBarLength (explicitly set)
            WithBarLength(2);
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            IsNotNull(() => GetBarLength.AsConst);
            AreEqual(2, () => GetBarLength.AsConst.Value);
        }
        
        [TestMethod]
        public void Test_BarLength_From_BeatLength() => new ConfigWishesTests().BarLength_From_BeatLength();
        
        void BarLength_From_BeatLength()
        {
            // 4 * BeatLength
            WithBarLength();
            WithBeatLength(0.12);
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            AreEqual(0.48, () => GetBarLength.Value);
        }
        
        [TestMethod]
        public void Test_Dynamic_BarLength() => new ConfigWishesTests().Dynamic_BarLength();
        
        void Dynamic_BarLength()
        {
            // WithBarLength (dynamic)
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBarLength);
            IsFalse(() => GetBarLength.IsConst);
            AreEqual(2, () => GetBarLength.Calculate(0.5));
        }
        
        [TestMethod]
        public void Test_Dynamic_BarLength_From_BeatLength() => new ConfigWishesTests().Dynamic_BarLength_From_BeatLength();
        
        void Dynamic_BarLength_From_BeatLength()
        {
            // 4 * BeatLength (dynamic)
            WithBarLength();
            WithBeatLength(Curve(0, 0.24));
            IsNotNull(() => GetBarLength);
            IsFalse(() => GetBarLength.IsConst);
            AreEqual(0.48, () => GetBarLength.Calculate(0.5));
        }
        
        // Beat Length
        
        [TestMethod]
        public void Test_Default_BeatLength() => new ConfigWishesTests().Default_BeatLength();
        
        void Default_BeatLength()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.25, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod]
        public void Test_BeatLength_From_BarLength() => new ConfigWishesTests().BeatLength_From_BarLength();
        
        void BeatLength_From_BarLength()
        {
            // 1/4 BarLength
            WithBarLength(Curve(2));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst); 
            AreEqual(0.5, () => GetBeatLength.Value); // 1/4 * 2.0 = 0.5.
        }
        
        [TestMethod]
        public void Test_Explicit_BeatLength() => new ConfigWishesTests().Explicit_BeatLength();
        
        void Explicit_BeatLength()
        {
            WithBeatLength(0.3);
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.3, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod]
        public void Test_Dynamic_BeatLength_From_BarLength() => new ConfigWishesTests().Dynamic_BeatLength_From_BarLength();
        
        void Dynamic_BeatLength_From_BarLength()
        {
            // Dynamic 1/4 BarLength
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.5, () => GetBeatLength.Calculate(0.5)); // 1/4 * midpoint of 2.0
        }
        
        [TestMethod]
        public void Test_Dynamic_Explicit_BeatLength() => new ConfigWishesTests().Dynamic_Explicit_BeatLength();
        
        void Dynamic_Explicit_BeatLength()
        {
            WithBeatLength(Curve(0, 0.3));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.15, () => GetBeatLength.Calculate(0.5)); // Midpoint: 0.15
        }

        // Note Length

        [TestMethod]
        public void Fluent_NoteLength_Fallbacks_Test() => new ConfigWishesTests().Fluent_NoteLength_Fallbacks();
        
        void Fluent_NoteLength_Fallbacks()
        {
            // TODO: Can't easily apply an envelope consistently to the note length?
            // TODO: Not enough overloads in the note arrangement indexer.
            //_[time, A4, instrument, volume]

            WithAudioLength(2);

            var time       = _[0];
            var volume     = 0.8;
            var instrument = A4.Sine();
            
            // Config file (0.5)
            {
                var noteLength = GetNoteLength;
                AreEqual(0.5, () => noteLength.Value);
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // WithNoteLength (0.8)
            {
                WithNoteLength(0.8);
                var noteLength = GetNoteLength;
                AreEqual(0.8, () => noteLength.Value);
                Play(() => StrikeNote(instrument, time, volume));
            }

            // WithNoteLength() => defaults to Config file (0.5)
            {
                WithNoteLength();
                
                var noteLength = GetNoteLength;
                AreEqual(0.5, () => noteLength.Value);
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // StrikeNote parameter (1.5)
            {
                Play(() => StrikeNote(instrument, time, volume, duration: _[1.5]));
            }
            
            // Just the instrument for reference
            Play(() => instrument);
        }
    }
}
