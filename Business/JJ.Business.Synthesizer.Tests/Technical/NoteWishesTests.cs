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
        
        [TestMethod]
        public void NoteArrangementsTest() => new NoteWishesTests().NoteArrangements();
        
        void NoteArrangements()
        {
            WithAudioLength(4);
            
            var tremoloSpeed   = _[7];
            var panbrelloSpeed = _[3];

            Save(() => 0.0025 * Add
                 (
                     _[              Flute1                   ],
                     _[              Flute1, 0.8              ],
                     _[              Flute1, 0.8     , l[0.5] ],
                     _[              Flute1, MyCurve          ],
                     _[              Flute1, MyCurve , l[0.5] ],
                     _[ 0.00   ,     Flute1                   ],
                     _[ 0.00   ,     Flute1, 0.8              ],
                     _[ 0.00   ,     Flute1, 0.8     , l[0.5] ],
                     _[ 0.00   ,     Flute1, MyCurve          ],
                     _[ 0.00   ,     Flute1, MyCurve , l[0.5] ],
                     _[ t[1, 1],     Flute1                   ],
                     _[ t[1, 1],     Flute1, 0.8              ],
                     _[ t[1, 1],     Flute1, 0.8     , l[0.5] ],
                     _[ t[1, 1],     Flute1, MyCurve          ],
                     _[ t[1, 1],     Flute1, MyCurve , l[0.5] ],
                     
                     _[          A4, Flute2                   ],
                     _[          A4, Flute2, 0.8              ],
                     _[          A4, Flute2, 0.8     , l[0.5] ],
                     _[          A4, Flute2, MyCurve          ],
                     _[          A4, Flute2, MyCurve , l[0.5] ],
                     _[ 0.00   , A4, Flute2                   ],
                     _[ 0.25   , C5, Flute2, 0.8              ],
                     _[ 0.50   , E5, Flute2, 0.8     , l[0.5] ],
                     _[ 0.75   , G5, Flute2, MyCurve          ],
                     _[ 1.00   , A5, Flute2, MyCurve , l[0.5] ],
                     _[ t[1, 1], A4, Flute2                   ],
                     _[ t[1, 2], C5, Flute2, 0.8              ],
                     _[ t[1, 3], E5, Flute2, 0.8     , l[0.5] ],
                     _[ t[1, 4], G5, Flute2, MyCurve          ],
                     _[ t[2, 0], A5, Flute2, MyCurve , l[0.5] ],
                     
                     _[          A4, Flute3                   ],
                     _[          A4, Flute3, 0.8              ],
                     _[          A4, Flute3, 0.8     , l[0.5] ],
                     _[          A4, Flute3, MyCurve          ],
                     _[          A4, Flute3, MyCurve , l[0.5] ],
                     _[ 0.00   , A4, Flute3                   ],
                     _[ 0.25   , C5, Flute3, 0.8              ],
                     _[ 0.50   , E5, Flute3, 0.8     , l[0.5] ],
                     _[ 0.75   , G5, Flute3, MyCurve          ],
                     _[ 1.00   , A5, Flute3, MyCurve , l[0.5] ],
                     _[ t[1, 1], A4, Flute3                   ],
                     _[ t[1, 2], C5, Flute3, 0.8              ],
                     _[ t[1, 3], E5, Flute3, 0.8     , l[0.5] ],
                     _[ t[1, 4], G5, Flute3, MyCurve          ],
                     _[ t[2, 0], A5, Flute3, MyCurve , l[0.5] ],
                     
                     _[          A4, Flute4                   ],
                     _[          A4, Flute4, 0.8              ],
                     _[          A4, Flute4, 0.8     , l[0.5] ],
                     _[          A4, Flute4, MyCurve          ],
                     _[          A4, Flute4, MyCurve , l[0.5] ],
                     _[ 0.00   , A4, Flute4                   ],
                     _[ 0.25   , C5, Flute4, 0.8              ],
                     _[ 0.50   , E5, Flute4, 0.8     , l[0.5] ],
                     _[ 0.75   , G5, Flute4, MyCurve          ],
                     _[ 1.00   , A5, Flute4, MyCurve , l[0.5] ],
                     _[ t[1, 1], A4, Flute4                   ],
                     _[ t[1, 2], C5, Flute4, 0.8              ],
                     _[ t[1, 3], E5, Flute4, 0.8     , l[0.5] ],
                     _[ t[1, 4], G5, Flute4, MyCurve          ],
                     _[ t[2, 0], A5, Flute4, MyCurve , l[0.5] ],
                     _[          A4, Flute4                  , fx1: tremoloSpeed],
                     _[          A4, Flute4, 0.8             , fx1: tremoloSpeed],
                     _[          A4, Flute4, 0.8     , l[0.5],      tremoloSpeed],
                     _[          A4, Flute4, MyCurve         , fx1: tremoloSpeed],
                     _[          A4, Flute4, MyCurve , l[0.5],      tremoloSpeed],
                     _[ 0.00   , A4, Flute4                  , fx1: tremoloSpeed],
                     _[ 0.25   , C5, Flute4, 0.8             , fx1: tremoloSpeed],
                     _[ 0.50   , E5, Flute4, 0.8     , l[0.5],      tremoloSpeed],
                     _[ 0.75   , G5, Flute4, MyCurve         , fx1: tremoloSpeed],
                     _[ 1.00   , A5, Flute4, MyCurve , l[0.5],      tremoloSpeed],
                     _[ t[1, 1], A4, Flute4                  , fx1: tremoloSpeed],
                     _[ t[1, 2], C5, Flute4, 0.8             , fx1: tremoloSpeed],
                     _[ t[1, 3], E5, Flute4, 0.8     , l[0.5],      tremoloSpeed],
                     _[ t[1, 4], G5, Flute4, MyCurve         , fx1: tremoloSpeed],
                     _[ t[2, 0], A5, Flute4, MyCurve , l[0.5],      tremoloSpeed],

                     _[          A4, Flute5                   ],
                     _[          A4, Flute5, 0.8              ],
                     _[          A4, Flute5, 0.8     , l[0.5] ],
                     _[          A4, Flute5, MyCurve          ],
                     _[          A4, Flute5, MyCurve , l[0.5] ],
                     _[ 0.00   , A4, Flute5                   ],
                     _[ 0.25   , C5, Flute5, 0.8              ],
                     _[ 0.50   , E5, Flute5, 0.8     , l[0.5] ],
                     _[ 0.75   , G5, Flute5, MyCurve          ],
                     _[ 1.00   , A5, Flute5, MyCurve , l[0.5] ],
                     _[ t[1, 1], A4, Flute5                   ],
                     _[ t[1, 2], C5, Flute5, 0.8              ],
                     _[ t[1, 3], E5, Flute5, 0.8     , l[0.5] ],
                     _[ t[1, 4], G5, Flute5, MyCurve          ],
                     _[ t[2, 0], A5, Flute5, MyCurve , l[0.5] ],
                     _[          A4, Flute5                  , fx1: tremoloSpeed ],
                     _[          A4, Flute5, 0.8             , fx1: tremoloSpeed ],
                     _[          A4, Flute5, 0.8     , l[0.5],      tremoloSpeed ],
                     _[          A4, Flute5, MyCurve         , fx1: tremoloSpeed ],
                     _[          A4, Flute5, MyCurve , l[0.5],      tremoloSpeed ],
                     _[ 0.00   , A4, Flute5                  , fx1: tremoloSpeed ],
                     _[ 0.25   , C5, Flute5, 0.8             , fx1: tremoloSpeed ],
                     _[ 0.50   , E5, Flute5, 0.8     , l[0.5],      tremoloSpeed ],
                     _[ 0.75   , G5, Flute5, MyCurve         , fx1: tremoloSpeed ],
                     _[ 1.00   , A5, Flute5, MyCurve , l[0.5],      tremoloSpeed ],
                     _[ t[1, 1], A4, Flute5                  , fx1: tremoloSpeed ],
                     _[ t[1, 2], C5, Flute5, 0.8             , fx1: tremoloSpeed ],
                     _[ t[1, 3], E5, Flute5, 0.8     , l[0.5],      tremoloSpeed ],
                     _[ t[1, 4], G5, Flute5, MyCurve         , fx1: tremoloSpeed ],
                     _[ t[2, 0], A5, Flute5, MyCurve , l[0.5],      tremoloSpeed ],
                     _[          A4, Flute5                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute5, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute5, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[          A4, Flute5, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute5, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ 0.00   , A4, Flute5                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 0.25   , C5, Flute5, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 0.50   , E5, Flute5, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ 0.75   , G5, Flute5, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 1.00   , A5, Flute5, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ t[1, 1], A4, Flute5                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[1, 2], C5, Flute5, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[1, 3], E5, Flute5, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ t[1, 4], G5, Flute5, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[2, 0], A5, Flute5, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     
                     _[          A4, Flute6                   ],
                     _[          A4, Flute6, 0.8              ],
                     _[          A4, Flute6, 0.8     , l[0.5] ],
                     _[          A4, Flute6, MyCurve          ],
                     _[          A4, Flute6, MyCurve , l[0.5] ],
                     _[ 0.00   , A4, Flute6                   ],
                     _[ 0.25   , C5, Flute6, 0.8              ],
                     _[ 0.50   , E5, Flute6, 0.8     , l[0.5] ],
                     _[ 0.75   , G5, Flute6, MyCurve          ],
                     _[ 1.00   , A5, Flute6, MyCurve , l[0.5] ],
                     _[ t[1, 1], A4, Flute6                   ],
                     _[ t[1, 2], C5, Flute6, 0.8              ],
                     _[ t[1, 3], E5, Flute6, 0.8     , l[0.5] ],
                     _[ t[1, 4], G5, Flute6, MyCurve          ],
                     _[ t[2, 0], A5, Flute6, MyCurve , l[0.5] ],
                     _[          A4, Flute6                  , fx1: tremoloSpeed ],
                     _[          A4, Flute6, 0.8             , fx1: tremoloSpeed ],
                     _[          A4, Flute6, 0.8     , l[0.5],      tremoloSpeed ],
                     _[          A4, Flute6, MyCurve         , fx1: tremoloSpeed ],
                     _[          A4, Flute6, MyCurve , l[0.5],      tremoloSpeed ],
                     _[ 0.00   , A4, Flute6                  , fx1: tremoloSpeed ],
                     _[ 0.25   , C5, Flute6, 0.8             , fx1: tremoloSpeed ],
                     _[ 0.50   , E5, Flute6, 0.8     , l[0.5],      tremoloSpeed ],
                     _[ 0.75   , G5, Flute6, MyCurve         , fx1: tremoloSpeed ],
                     _[ 1.00   , A5, Flute6, MyCurve , l[0.5],      tremoloSpeed ],
                     _[ t[1, 1], A4, Flute6                  , fx1: tremoloSpeed ],
                     _[ t[1, 2], C5, Flute6, 0.8             , fx1: tremoloSpeed ],
                     _[ t[1, 3], E5, Flute6, 0.8     , l[0.5],      tremoloSpeed ],
                     _[ t[1, 4], G5, Flute6, MyCurve         , fx1: tremoloSpeed ],
                     _[ t[2, 0], A5, Flute6, MyCurve , l[0.5],      tremoloSpeed ],
                     _[          A4, Flute6                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute6, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute6, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[          A4, Flute6, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute6, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ 0.00   , A4, Flute6                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 0.25   , C5, Flute6, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 0.50   , E5, Flute6, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ 0.75   , G5, Flute6, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 1.00   , A5, Flute6, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ t[1, 1], A4, Flute6                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[1, 2], C5, Flute6, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[1, 3], E5, Flute6, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ t[1, 4], G5, Flute6, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[2, 0], A5, Flute6, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[          A4, Flute6                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[          A4, Flute6, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[          A4, Flute6, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[          A4, Flute6, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[          A4, Flute6, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ 0.00   , A4, Flute6                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ 0.25   , C5, Flute6, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ 0.50   , E5, Flute6, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ 0.75   , G5, Flute6, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ 1.00   , A5, Flute6, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ t[1, 1], A4, Flute6                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ t[1, 2], C5, Flute6, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ t[1, 3], E5, Flute6, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ t[1, 4], G5, Flute6, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ t[2, 0], A5, Flute6, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                                          
                     _[          A4, Flute7                   ],
                     _[          A4, Flute7, 0.8              ],
                     _[          A4, Flute7, 0.8     , l[0.5] ],
                     _[          A4, Flute7, MyCurve          ],
                     _[          A4, Flute7, MyCurve , l[0.5] ],
                     _[ 0.00   , A4, Flute7                   ],
                     _[ 0.25   , C5, Flute7, 0.8              ],
                     _[ 0.50   , E5, Flute7, 0.8     , l[0.5] ],
                     _[ 0.75   , G5, Flute7, MyCurve          ],
                     _[ 1.00   , A5, Flute7, MyCurve , l[0.5] ],
                     _[ t[1, 1], A4, Flute7                   ],
                     _[ t[1, 2], C5, Flute7, 0.8              ],
                     _[ t[1, 3], E5, Flute7, 0.8     , l[0.5] ],
                     _[ t[1, 4], G5, Flute7, MyCurve          ],
                     _[ t[2, 0], A5, Flute7, MyCurve , l[0.5] ],
                     _[          A4, Flute7                  , fx1: tremoloSpeed ],
                     _[          A4, Flute7, 0.8             , fx1: tremoloSpeed ],
                     _[          A4, Flute7, 0.8     , l[0.5],      tremoloSpeed ],
                     _[          A4, Flute7, MyCurve         , fx1: tremoloSpeed ],
                     _[          A4, Flute7, MyCurve , l[0.5],      tremoloSpeed ],
                     _[ 0.00   , A4, Flute7                  , fx1: tremoloSpeed ],
                     _[ 0.25   , C5, Flute7, 0.8             , fx1: tremoloSpeed ],
                     _[ 0.50   , E5, Flute7, 0.8     , l[0.5],      tremoloSpeed ],
                     _[ 0.75   , G5, Flute7, MyCurve         , fx1: tremoloSpeed ],
                     _[ 1.00   , A5, Flute7, MyCurve , l[0.5],      tremoloSpeed ],
                     _[ t[1, 1], A4, Flute7                  , fx1: tremoloSpeed ],
                     _[ t[1, 2], C5, Flute7, 0.8             , fx1: tremoloSpeed ],
                     _[ t[1, 3], E5, Flute7, 0.8     , l[0.5],      tremoloSpeed ],
                     _[ t[1, 4], G5, Flute7, MyCurve         , fx1: tremoloSpeed ],
                     _[ t[2, 0], A5, Flute7, MyCurve , l[0.5],      tremoloSpeed ],
                     _[          A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[          A4, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[          A4, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ 0.00   , A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 0.25   , C5, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 0.50   , E5, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ 0.75   , G5, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ 1.00   , A5, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ t[1, 1], A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[1, 2], C5, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[1, 3], E5, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[ t[1, 4], G5, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6] ],
                     _[ t[2, 0], A5, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6] ],
                     _[          A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[          A4, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[          A4, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[          A4, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[          A4, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ 0.00   , A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ 0.25   , C5, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ 0.50   , E5, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ 0.75   , G5, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ 1.00   , A5, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ t[1, 1], A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ t[1, 2], C5, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ t[1, 3], E5, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[ t[1, 4], G5, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
                     _[ t[2, 0], A5, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2] ],
                     _[          A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[          A4, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[          A4, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2],      panbrelloSpeed ],
                     _[          A4, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[          A4, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2],      panbrelloSpeed ],
                     _[ 0.00   , A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[ 0.25   , C5, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[ 0.50   , E5, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2],      panbrelloSpeed ],
                     _[ 0.75   , G5, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[ 1.00   , A5, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2],      panbrelloSpeed ],
                     _[ t[1, 1], A4, Flute7                  , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[ t[1, 2], C5, Flute7, 0.8             , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[ t[1, 3], E5, Flute7, 0.8     , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2],      panbrelloSpeed ],
                     _[ t[1, 4], G5, Flute7, MyCurve         , fx1: tremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: panbrelloSpeed ],
                     _[ t[2, 0], A5, Flute7, MyCurve , l[0.5],      tremoloSpeed,      _[0.6],      _[0.2],      panbrelloSpeed ]

                     //_[ b[1]   , A4, Flute4                   ],
                     //_[ b[2]   , C5, Flute5, 0.6              ],
                     //_[ b[3]   , G4, Flute6, MyCurve, len[0.5]],
                     //_[ beat[4], D5, Flute6, _[0.4],  l[0.8], _[7]],
                     //_[ bar[2] + beat[1], A4, Flute6, MyCurve, _[0.2], _[7], _[0.6]],
                     //_[ 1.75   , E5, Flute6, MyCurve , len[0.5]]
                 )).Play();
        }
        
        FlowNode Flute1()
            => A4.Sine();
        
        FlowNode Flute2(FlowNode freq) 
            => Sine(freq);
        
        FlowNode Flute3(FlowNode freq, FlowNode len = null) 
            => Sine(freq) * RecorderCurve.Stretch(SnapNoteLength(len));

        FlowNode Flute4(FlowNode freq, FlowNode len = null, FlowNode fx1 = null)
            => Sine(freq).Tremolo(fx1, 0.3) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Flute5(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null)
            => Sine(freq).Tremolo(fx1, fx2) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Flute6(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null)
            => Sine(freq).Tremolo(fx1, fx2).Panning(fx3) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Flute7(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null)
            => Sine(freq).Tremolo(fx1, fx2).Panning(fx3).Panbrello(fx4) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode MyCurve => Curve(@"
              >          
           >     >      >  >           
          >          >        >      >    >
                                 >            >
        >                                            >");
    }
}