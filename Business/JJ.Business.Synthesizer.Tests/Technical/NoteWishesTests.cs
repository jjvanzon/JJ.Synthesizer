using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable ParameterHidesMember
// ReSharper disable PossibleInvalidOperationException

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class NoteWishesTests : MySynthWishes
    {
        // BarLength
        
        [TestMethod]
        public void BarLength_Default_Test() => new NoteWishesTests().BarLength_Default();
        
        void BarLength_Default()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            IsNotNull(() => GetBarLength.AsConst);
            AreEqual(1.0, () => GetBarLength.AsConst.Value);
        }
        
        [TestMethod]
        public void BarLength_Explicit_Test() => new NoteWishesTests().BarLength_Explicit();
        
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
        public void BarLength_From_BeatLength_Test() => new NoteWishesTests().BarLength_From_BeatLength();
        
        void BarLength_From_BeatLength()
        {
            // 4 * BeatLength
            ResetBarLength();
            WithBeatLength(0.12);
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            AreEqual(0.48, () => GetBarLength.Value);
        }
        
        [TestMethod]
        public void BarLength_Dynamic_Test() => new NoteWishesTests().BarLength_Dynamic();
        
        void BarLength_Dynamic()
        {
            // WithBarLength (dynamic)
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBarLength);
            IsFalse(() => GetBarLength.IsConst);
            AreEqual(2, () => GetBarLength.Calculate(0.5));
        }
        
        [TestMethod]
        public void BarLength_Dynamic_From_BeatLength_Test() => new NoteWishesTests().BarLength_Dynamic_From_BeatLength();
        
        void BarLength_Dynamic_From_BeatLength()
        {
            // 4 * BeatLength (dynamic)
            ResetBarLength();
            WithBeatLength(Curve(0, 0.24));
            IsNotNull(() => GetBarLength);
            IsFalse(() => GetBarLength.IsConst);
            AreEqual(0.48, () => GetBarLength.Calculate(0.5));
        }
        
        // BeatLength
        
        [TestMethod]
        public void BeatLength_Default_Test() => new NoteWishesTests().BeatLength_Default();
        
        void BeatLength_Default()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.25, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod]
        public void BeatLength_From_BarLength_Test() => new NoteWishesTests().BeatLength_From_BarLength();
        
        void BeatLength_From_BarLength()
        {
            // 1/4 BarLength
            WithBarLength(Curve(2));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.5, () => GetBeatLength.Value); // 1/4 * 2.0 = 0.5.
        }
        
        [TestMethod]
        public void BeatLength_Explicit_Test() => new NoteWishesTests().BeatLength_Explicit();
        
        void BeatLength_Explicit()
        {
            WithBeatLength(0.3);
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.3, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod]
        public void BeatLength_Dynamic_From_BarLength_Test() => new NoteWishesTests().BeatLength_Dynamic_From_BarLength();
        
        void BeatLength_Dynamic_From_BarLength()
        {
            // Dynamic 1/4 BarLength
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.5, () => GetBeatLength.Calculate(0.5)); // 1/4 * midpoint of 2.0
        }
        
        [TestMethod]
        public void BeatLength_Dynamic_Explicit_Test() => new NoteWishesTests().BeatLength_Dynamic_Explicit();
        
        void BeatLength_Dynamic_Explicit()
        {
            WithBeatLength(Curve(0, 0.3));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.15, () => GetBeatLength.Calculate(0.5)); // Midpoint: 0.15
        }
        
        // Note Length
        
        [TestMethod]
        public void Fluent_NoteLength_Fallbacks_Test() => new NoteWishesTests().Fluent_NoteLength_Fallbacks();
        
        void Fluent_NoteLength_Fallbacks()
        {
            WithAudioLength(4);
            
            var    time   = _[0];
            var    volume = 0.8;
            double delta  = 0.000000000000001;
            
            FlowNode instrument(FlowNode freq = null, FlowNode noteLength = null)
            {
                freq = freq ?? A4;
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
                ResetNoteLength();
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
                ResetNoteLength();
                WithBeatLength(1);
                AreEqual(1, () => GetNoteLength.Value);
                Play(() => StrikeNote(instrument(G4), time, volume));
            }
            
            // Fallback to BeatLength (dynamic)
            {
                ResetNoteLength();
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
        
        // Note Arrangements
        
        //[TestMethod]
        //public void NoteArrangement_NoTime_Test() => new NoteWishesTests().NoteArrangement_NoTime();
        
        //void NoteArrangement_NoTime()
        //{
        //    FlowNode instrument(FlowNode freq, FlowNode length) => Sine(freq);
            
        //    Play(() => _[A4, instrument, 0.8]);
        //}
        
        [TestMethod]
        public void NoteArrangementsTest() => new NoteWishesTests().NoteArrangements();
        
        void NoteArrangements()
        {
            var tremoloSpeed   = _[7];
            var panbrelloSpeed = _[3];

            Play(() => Add
                 (
                     _[ 0      ,     Instrument1                   ],
                     _[ 0      ,     Instrument1, 0.8              ],
                     _[ 0      ,     Instrument1, MyCurve          ],
                     _[ t[1, 1],     Instrument1                   ],
                     _[ b[1]   , A4, Instrument4                   ],
                     _[ 0.25   ,     Instrument1, MyCurve          ],
                     _[ t[1, 2],     Instrument1, 0.8              ],
                     _[ b[2]   , C5, Instrument5, 0.6              ],
                     _[ 0.50   , A4, Instrument2, 0.8              ],
                     _[ t[1, 3],     Instrument1, MyCurve          ],
                     _[ b[3]   , G4, Instrument6, MyCurve, len[0.5]],
                     _[ 0.75   , C5, Instrument3, MyCurve          ],
                     _[ t[1, 4], A4, Instrument2, 0.8              ],
                     _[ beat[4], D5, Instrument6, _[0.4],  l[0.8], _[7]],
                     _[ 1      , G4, Instrument3                   ],
                     _[ t[2, 1], A4, Instrument2, MyCurve          ],
                     _[ bar[2] + beat[1], A4, Instrument6, MyCurve, _[0.2], _[7], _[0.6]],
                     _[ 1.25   , D5, Instrument4, 0.6              ],
                     _[ t[2, 2], A4, Instrument3                   ],
                     _[ t[2, 2], E4, Instrument6, 0.8     , l[0.5], tremoloSpeed, _[0.6], _[0.2]],
                     _[ 1.50   , A4, Instrument5, MyCurve          ],
                     _[ t[2, 3], C4, Instrument3, 0.8              ],
                     _[ t[2, 3], C4, Instrument7, MyCurve , l[0.5], tremoloSpeed, _[0.6], _[0.2], panbrelloSpeed],
                     _[ 1.75   , E5, Instrument6, MyCurve , len[0.5]],
                     _[ t[2, 4], E4, Instrument3, 0.8     , l[0.5]],
                     _[ t[2, 4], C4, Instrument7, 0.8     , l[0.5], tremoloSpeed, _[0.6], _[0.2], panbrelloSpeed],
                     _[ 2      , D5, Instrument4, _[0.4]  , l[0.8], _[7]],
                     _[ 2.25   , E4, Instrument5, MyCurve , _[0.2], _[7], _[0.6]],
                     _[ 2.50   , C4, Instrument6, 0.8     , l[0.5], tremoloSpeed, _[0.6], _[0.2]],
                     _[ 2.75   , C4, Instrument7, MyCurve , l[0.5], tremoloSpeed, _[0.6], _[0.2], panbrelloSpeed],
                     _[ 3      , C4, Instrument7, 0.8     , l[0.5], tremoloSpeed, _[0.6], _[0.2], panbrelloSpeed]
                 ));
        }
        
        FlowNode Instrument1()
            => A4.Sine();
        
        FlowNode Instrument2(FlowNode freq) 
            => Sine(freq);
        
        FlowNode Instrument3(FlowNode freq, FlowNode len) 
            => Sine(freq) * RecorderCurve.Stretch(SnapNoteLength(len));

        FlowNode Instrument4(FlowNode freq, FlowNode len, FlowNode tremoloSpeed = null)
            => Sine(freq).Tremolo(tremoloSpeed, 0.3) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Instrument5(FlowNode freq, FlowNode len, FlowNode tremoloSpeed = null, FlowNode tremoloDepth = null)
            => Sine(freq).Tremolo(tremoloSpeed, tremoloDepth) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Instrument6(FlowNode freq, FlowNode len, FlowNode tremoloSpeed = null, FlowNode tremoloDepth = null, FlowNode panning = null)
            => Sine(freq).Tremolo(tremoloSpeed, tremoloDepth).Panning(panning) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Instrument7(FlowNode freq, FlowNode len, FlowNode tremoloSpeed = null, FlowNode tremoloDepth = null, FlowNode panning = null, FlowNode panbrelloSpeed = null)
            => Sine(freq).Tremolo(tremoloSpeed, tremoloDepth).Panning(panning).Panbrello(panbrelloSpeed) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode MyCurve => Curve(@"
              >          
           >     >      >  >           
          >          >        >      >    >
                                 >            >
        >                                            >");
    }
}