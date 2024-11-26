using System;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable PossibleInvalidOperationException

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class ConfigWishesTests : MySynthWishes
    {
        // BarLength
        
        [TestMethod]
        public void BarLength_Default_Test() => new ConfigWishesTests().BarLength_Default();
        
        void BarLength_Default()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            IsNotNull(() => GetBarLength.AsConst);
            AreEqual(1.0, () => GetBarLength.AsConst.Value);
        }
        
        [TestMethod]
        public void BarLength_Explicit_Test() => new ConfigWishesTests().BarLength_Explicit();
        
        void BarLength_Explicit()
        {
            // WithBarLength (explicitly set)
            WithBarLength(2);
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            IsNotNull(() => GetBarLength.AsConst);
            AreEqual(2, () => GetBarLength.AsConst.Value);
        }
        
        [TestMethod]
        public void BarLength_From_BeatLength_Test() => new ConfigWishesTests().BarLength_From_BeatLength();
        
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
        public void BarLength_Dynamic_Test() => new ConfigWishesTests().BarLength_Dynamic();
        
        void BarLength_Dynamic()
        {
            // WithBarLength (dynamic)
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBarLength);
            IsFalse(() => GetBarLength.IsConst);
            AreEqual(2, () => GetBarLength.Calculate(0.5));
        }
        
        [TestMethod]
        public void BarLength_Dynamic_From_BeatLength_Test() => new ConfigWishesTests().BarLength_Dynamic_From_BeatLength();
        
        void BarLength_Dynamic_From_BeatLength()
        {
            // 4 * BeatLength (dynamic)
            WithBarLength();
            WithBeatLength(Curve(0, 0.24));
            IsNotNull(() => GetBarLength);
            IsFalse(() => GetBarLength.IsConst);
            AreEqual(0.48, () => GetBarLength.Calculate(0.5));
        }
        
        // BeatLength
        
        [TestMethod]
        public void BeatLength_Default_Test() => new ConfigWishesTests().BeatLength_Default();
        
        void BeatLength_Default()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.25, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod]
        public void BeatLength_From_BarLength_Test() => new ConfigWishesTests().BeatLength_From_BarLength();
        
        void BeatLength_From_BarLength()
        {
            // 1/4 BarLength
            WithBarLength(Curve(2));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.5, () => GetBeatLength.Value); // 1/4 * 2.0 = 0.5.
        }
        
        [TestMethod]
        public void BeatLength_Explicit_Test() => new ConfigWishesTests().BeatLength_Explicit();
        
        void BeatLength_Explicit()
        {
            WithBeatLength(0.3);
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.3, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod]
        public void BeatLength_Dynamic_From_BarLength_Test() => new ConfigWishesTests().BeatLength_Dynamic_From_BarLength();
        
        void BeatLength_Dynamic_From_BarLength()
        {
            // Dynamic 1/4 BarLength
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.5, () => GetBeatLength.Calculate(0.5)); // 1/4 * midpoint of 2.0
        }
        
        [TestMethod]
        public void BeatLength_Dynamic_Explicit_Test() => new ConfigWishesTests().BeatLength_Dynamic_Explicit();
        
        void BeatLength_Dynamic_Explicit()
        {
            WithBeatLength(Curve(0, 0.3));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.15, () => GetBeatLength.Calculate(0.5)); // Midpoint: 0.15
        }
        
        // Note Length
        
        [TestMethod]
        public void Fluent_NoteLength_Fallbacks_Test() => new ConfigWishesTests().Fluent_NoteLength_Fallbacks();
        
        private FlowNode Instrument(FlowNode freq, FlowNode duration) => Sine(freq).Curve(RecorderEnvelope.Stretch(duration));
        
        void Fluent_NoteLength_Fallbacks()
        {
            // TODO: Can't easily apply an envelope consistently to the note length?
            // TODO: Not enough overloads in the note arrangement indexer.
            //_[time, A4, instrument, volume]
            
            WithAudioLength(2);
            
            var    time       = _[0];
            var    volume     = 0.8;
            var    instrument = A4.Sine();
            double delta    = 0.0000000000000001;
            
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
            
            // Dynamic NoteLength explicitly set
            {
                WithNoteLength(Curve(0.3, 0.6));
                var noteLength = GetNoteLength;
                AreEqual(0.45, noteLength.Calculate(0.5), delta); // Midpoint of 0.3 and 0.6 is 0.45.
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // WithNoteLength() => defaults to Config file (0.5)
            {
                WithNoteLength();
                var noteLength = GetNoteLength;
                AreEqual(0.5, () => noteLength.Value);
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // Fallback to BeatLength (0.25)
            {
                WithBeatLength(0.25);
                WithNoteLength(); // Ensure no explicit NoteLength is set.
                var noteLength = GetNoteLength;
                AreEqual(0.25, () => noteLength.Value); // Should fall back to BeatLength.
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // Fallback to BeatLength (dynamic)
            {
                WithBeatLength(Curve(0.2, 0.4));
                WithNoteLength();              // Ensure no explicit NoteLength is set.
                var noteLength = GetNoteLength;
                AreEqual(0.3, noteLength.Calculate(0.5), delta); // Midpoint of 0.2 and 0.4 is 0.3.
                Play(() => StrikeNote(instrument, time, volume));
            }
            
            // StrikeNote parameter (1.5)
            {
                Play(() => StrikeNote(instrument, time, volume, duration: _[1.5]));
            }
            
            // StrikeNote parameter (dynamic duration)
            {
                Play(() => StrikeNote(instrument, time, volume, duration: Curve(1.0, 1.5)));
            }
            
            // Just the instrument for reference
            Play(() => instrument);
        }
    }
}