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
        [TestMethod]
        public void Fluent_BarLength_Fallbacks_Test() => new ConfigWishesTests().Fluent_BarLength_Fallbacks();
        
        void Fluent_BarLength_Fallbacks()
        {
            // Default (from config or hard-coded)
            {
                IsNotNull(() => GetBarLength);
                IsTrue(() => GetBarLength.IsConst);
                IsNotNull(() => GetBarLength.AsConst);
                AreEqual(1.0, () => GetBarLength.AsConst.Value);
            }
            
            // WithBarLength (explicitly set)
            {
                WithBarLength(2);
                IsNotNull(() => GetBarLength);
                IsTrue(() => GetBarLength.IsConst);
                IsNotNull(() => GetBarLength.AsConst);
                AreEqual(2, () => GetBarLength.AsConst.Value);
            }
            
            // 4 * BeatLength
            {
                WithBarLength();
                WithBeatLength(0.12);
                IsNotNull(() => GetBarLength);
                IsTrue(() => GetBarLength.IsConst);
                AreEqual(0.48, () => GetBarLength.Value);
            }
            
            // WithBarLength (dynamic)
            {
                WithBarLength(Curve(0, 4));
                IsNotNull(() => GetBarLength);
                IsFalse(() => GetBarLength.IsConst);
                AreEqual(2, () => GetBarLength.Calculate(0.5));
            }
            
            // 4 * BeatLength (dynamic)
            {
                WithBarLength();
                WithBeatLength(Curve(0, 0.24));
                IsNotNull(() => GetBarLength);
                IsFalse(() => GetBarLength.IsConst);
                AreEqual(0.48, () => GetBarLength.Calculate(0.5));
            }
        }
        
        [TestMethod]
        public void Fluent_BeatLength_Fallbacks_Test() => new ConfigWishesTests().Fluent_BeatLength_Fallbacks();
        
        void Fluent_BeatLength_Fallbacks()
        {
            // Default (from config or hard-coded)
            {
                IsNotNull(() => GetBeatLength);
                IsTrue(() => GetBeatLength.IsConst);
                IsNotNull(() => GetBeatLength.AsConst);
                AreEqual(0.25, () => GetBeatLength.AsConst.Value);
            }
            
            // 1/4 BarLength
            {
                WithBarLength(Curve(2));
                IsNotNull(() => GetBeatLength);
                IsFalse(() => GetBeatLength.IsConst); 
                AreEqual(0.5, () => GetBeatLength.Value);
            }
            
            // WithBeatLength (explicitly set)
            {
                WithBeatLength(0.3);
                IsNotNull(() => GetBeatLength);
                IsTrue(() => GetBeatLength.IsConst);
                IsNotNull(() => GetBeatLength.AsConst);
                AreEqual(0.3, () => GetBeatLength.AsConst.Value);
            }
        }
        
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
