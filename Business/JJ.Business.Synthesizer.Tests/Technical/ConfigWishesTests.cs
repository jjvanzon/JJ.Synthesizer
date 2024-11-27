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
        
        void Fluent_NoteLength_Fallbacks()
        {
            // TODO: Can't easily apply an envelope consistently to the note length?
            // TODO: Not enough overloads in the note arrangement indexer.
            //_[time, A4, instrument, volume]
            
            WithAudioLength(4);
            
            var    time   = _[0];
            var    volume = 0.8;
            double delta  = 0.000000000000001;
            
            FlowNode instrument(FlowNode freq = null, FlowNode noteLength = null)
            {
                freq     = freq ?? A4;
                return Sine(freq) * RecorderCurve.Stretch(SnapNoteLength(noteLength));
            }

            // Play the instrument for reference
            {
                Play(() => instrument(C3));
            }
            
            // NoteLength from config file / hard-coded default
            {
                AreEqual(0.5, () => GetNoteLength.Value);
                Play(() => StrikeNote(instrument(C4), time, volume));
            }
            
            // WithNoteLength
            {
                WithNoteLength(0.33);
                AreEqual(0.33, () => GetNoteLength.Value);
                Play(() => StrikeNote(instrument(D4), time, volume));
            }
            
            // WithNoteLength() => defaults to config file or hard-coded default
            {
                WithNoteLength();
                AreEqual(0.5, () => GetNoteLength.Value);
                Play(() => StrikeNote(instrument(E4), time, volume));
            }
            
            // Dynamic NoteLength explicitly set
            {
                WithNoteLength(Curve(0.75, 1.5));
                AreEqual(1.125, () => GetNoteLength.Calculate(0.5));
                Play(() => StrikeNote(instrument(F4), time, volume));
            }
            
            // Fallback to BeatLength
            {
                WithNoteLength();
                WithBeatLength(1);
                AreEqual(1, () => GetNoteLength.Value);
                Play(() => StrikeNote(instrument(G4), time, volume));
            }
            
            // Fallback to BeatLength (dynamic)
            {
                WithNoteLength();
                WithBeatLength(Curve(1.5, 2.0));
                AreEqual(1.75, GetNoteLength.Calculate(0.5), delta);
                Play(() => StrikeNote(instrument(A4), time, volume));
            }
            
            // StrikeNote parameter
            {
                var noteLength = _[2.2];
                Play(() => StrikeNote(instrument(B4, noteLength), time, volume, noteLength));
            }
            
            // StrikeNote parameter (dynamic duration)
            {
                var noteLength = Curve(3.5, 5);
                Play(() => StrikeNote(instrument(C5, noteLength), time, volume, noteLength));
            }
        }
    }
}