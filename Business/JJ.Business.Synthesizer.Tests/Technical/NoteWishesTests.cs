using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

// ReSharper disable ParameterHidesMember
// ReSharper disable PossibleInvalidOperationException

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class NoteWishesTests : MySynthWishes
    {
        // BarLength
        
        [TestMethod] public void BarLength_Default_Test() => Run(BarLength_Default);
        void BarLength_Default()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            IsNotNull(() => GetBarLength.AsConst);
            AreEqual(1.0, () => GetBarLength.AsConst.Value);
        }
        
        [TestMethod] public void BarLength_Explicit_Test() => Run(BarLength_Explicit);
        void BarLength_Explicit()
        {
            WithBarLength(2);
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            IsNotNull(() => GetBarLength.AsConst);
            AreEqual(2, () => GetBarLength.AsConst.Value);
        }
        
        [TestMethod] public void BarLength_From_BeatLength_Test() => Run(BarLength_From_BeatLength);
        void BarLength_From_BeatLength()
        {
            // 4 * BeatLength
            ResetBarLength();
            WithBeatLength(0.12);
            IsNotNull(() => GetBarLength);
            IsTrue(() => GetBarLength.IsConst);
            AreEqual(0.48, () => GetBarLength.Value);
        }
        
        [TestMethod] public void BarLength_Dynamic_Test() => Run(BarLength_Dynamic);
        void BarLength_Dynamic()
        {
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBarLength);
            IsFalse(() => GetBarLength.IsConst);
            AreEqual(2, () => GetBarLength.Calculate(0.5));
        }
        
        [TestMethod] public void BarLength_Dynamic_From_BeatLength_Test() => Run(BarLength_Dynamic_From_BeatLength);
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
        
        [TestMethod] public void BeatLength_Default_Test() => Run(BeatLength_Default);
        void BeatLength_Default()
        {
            // Default (from config or hard-coded)
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.25, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod] public void BeatLength_From_BarLength_Test() => Run(BeatLength_From_BarLength);
        void BeatLength_From_BarLength()
        {
            // 1/4 BarLength
            WithBarLength(Curve(2));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.5, () => GetBeatLength.Value); // 1/4 * 2.0 = 0.5.
        }
        
        [TestMethod] public void BeatLength_Explicit_Test() => Run(BeatLength_Explicit);
        void BeatLength_Explicit()
        {
            WithBeatLength(0.3);
            IsNotNull(() => GetBeatLength);
            IsTrue(() => GetBeatLength.IsConst);
            IsNotNull(() => GetBeatLength.AsConst);
            AreEqual(0.3, () => GetBeatLength.AsConst.Value);
        }
        
        [TestMethod] public void BeatLength_Dynamic_From_BarLength_Test() => Run(BeatLength_Dynamic_From_BarLength);
        void BeatLength_Dynamic_From_BarLength()
        {
            // Dynamic 1/4 BarLength
            WithBarLength(Curve(0, 4));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.5, () => GetBeatLength.Calculate(0.5)); // 1/4 * midpoint of 2.0
        }
        
        [TestMethod] public void BeatLength_Dynamic_Explicit_Test() => Run(BeatLength_Dynamic_Explicit);
        void BeatLength_Dynamic_Explicit()
        {
            WithBeatLength(Curve(0, 0.3));
            IsNotNull(() => GetBeatLength);
            IsFalse(() => GetBeatLength.IsConst);
            AreEqual(0.15, () => GetBeatLength.Calculate(0.5)); // Midpoint: 0.15
        }
        
        // Note Length
        
        /// <inheritdoc cref="docs._notelengthfallbacktests" />
        [TestMethod] public void Fluent_NoteLength_Fallbacks_Test() => Run(Fluent_NoteLength_Fallbacks);
        /// <inheritdoc cref="docs._notelengthfallbacktests" />
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
                instrument(G4).Play();
            }
            
            // NoteLength from config file / hard-coded default
            {
                AreEqual(0.2, () => GetNoteLength.Value);
                Note(instrument(C4), time, volume).Play();
            }
            
            // WithNoteLength
            {
                WithNoteLength(0.33);
                AreEqual(0.33, () => GetNoteLength.Value);
                Note(instrument(D4), time, volume).Play();
            }
            
            // ResetNoteLength() => defaults to config file or hard-coded default
            {
                ResetNoteLength();
                AreEqual(0.2, () => GetNoteLength.Value);
                Note(instrument(E4), time, volume).Play();
            }
            
            // Dynamic NoteLength explicitly set
            {
                WithNoteLength(Curve(0.75, 1.5));
                AreEqual(1.125, () => GetNoteLength.Calculate(0.5));
                Note(instrument(F4), time, volume).Play();
            }
            
            // Fallback to BeatLength
            {
                ResetNoteLength();
                WithBeatLength(1);
                AreEqual(1, () => GetNoteLength.Value);
                Note(instrument(G4), time, volume).Play();
            }
            
            // Fallback to BeatLength (dynamic)
            {
                ResetNoteLength();
                WithBeatLength(Curve(1.5, 2.0));
                AreEqual(1.75, GetNoteLength.Calculate(0.5), delta);
                Note(instrument(A4), time, volume).Play();
            }
            
            // StrikeNote parameter
            {
                var noteLength = _[2.2];
                Note(instrument(B4, noteLength), time, volume, noteLength).Play();
            }
            
            // StrikeNote parameter (dynamic duration)
            {
                var noteLength = Curve(3.5, 5);
                Note(instrument(C5, noteLength), time, volume, noteLength).Play();
            }
        }
        
        // Note Arrangements
        
        FlowNode TremoloSpeed   => _[7];
        FlowNode PanbrelloSpeed => _[3];
               
        FlowNode FluteNoParams()
            => A4.Sine();
        
        FlowNode Flute1Param(FlowNode freq) 
            => Sine(freq);
        
        FlowNode Flute2Params(FlowNode freq, FlowNode len = null) 
            => Sine(freq) * RecorderCurve.Stretch(SnapNoteLength(len));

        FlowNode Flute3Params(FlowNode freq, FlowNode len = null, FlowNode fx1 = null)
            => Sine(freq).Tremolo(fx1, 0.3) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Flute4Params(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null)
            => Sine(freq).Tremolo(fx1, fx2) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Flute5Params(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null)
            => Sine(freq).Tremolo(fx1, fx2).Panning(fx3) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode Flute6Params(FlowNode freq, FlowNode len = null, FlowNode fx1 = null, FlowNode fx2 = null, FlowNode fx3 = null, FlowNode fx4 = null)
            => Sine(freq).Tremolo(fx1, fx2).Panning(fx3).Panbrello(fx4) * RecorderCurve.Stretch(SnapNoteLength(len));
        
        FlowNode MyCurve => Curve(@"
        *
         *
          *
           *");
 
        void SetNoteArrangementOptions()
        {
            WithStereo();
            WithNoteLength(0.25);
            WithAudioLength(t[2, 1] + GetNoteLength);
        }

        [TestMethod]
        public void NoteArrangement_SingleNote_Test() => new NoteWishesTests().NoteArrangement_SingleNote();

        void NoteArrangement_SingleNote()
        {
            Save(() => MyCurve);
            
            Save(() => _[ t[2, 1], A5, Flute1Param, MyCurve , l[0.5] ]).Play();
            //Save(() => _[ t[1, 1], A5, Flute1Param, MyCurve , l[0.5] ]).Play();
            //Save(() => _[ t[1, 1], A5, Flute1Param, MyCurve ]).Play();
        }
        
        [TestMethod] public void NoteArrangement_FluteNoParams_Test() => new NoteWishesTests().NoteArrangement_FluteNoParams();
        void NoteArrangement_FluteNoParams()
        {
            SetNoteArrangementOptions();
            Save(() => 0.05 * Add
            (
            _[              FluteNoParams                   ],
            _[              FluteNoParams, 0.8              ],
            _[              FluteNoParams, 0.8     , l[0.5] ],
            _[              FluteNoParams, MyCurve          ],
            _[              FluteNoParams, MyCurve , l[0.5] ],
            _[ 0.00   ,     FluteNoParams                   ],
            _[ 0.00   ,     FluteNoParams, 0.8              ],
            _[ 0.00   ,     FluteNoParams, 0.8     , l[0.5] ],
            _[ 0.00   ,     FluteNoParams, MyCurve          ],
            _[ 0.00   ,     FluteNoParams, MyCurve , l[0.5] ],
            _[ t[1, 1],     FluteNoParams                   ],
            _[ t[1, 1],     FluteNoParams, 0.8              ],
            _[ t[1, 1],     FluteNoParams, 0.8     , l[0.5] ],
            _[ t[1, 1],     FluteNoParams, MyCurve          ],
            _[ t[1, 1],     FluteNoParams, MyCurve , l[0.5] ]
            )).Play();
        }

        [TestMethod] public void NoteArrangement_Flute1Param_Test() => new NoteWishesTests().NoteArrangement_Flute1Param();
        void NoteArrangement_Flute1Param()
        {
            SetNoteArrangementOptions();
            Save(() => 0.1 * Add
            (
            _[          A4, Flute1Param                   ],
            _[          A4, Flute1Param, 0.8              ],
            _[          A4, Flute1Param, 0.8     , l[0.5] ],
            _[          A4, Flute1Param, MyCurve          ],
            _[          A4, Flute1Param, MyCurve , l[0.5] ],
            _[ 0.00   , A4, Flute1Param                   ],
            _[ 0.25   , C5, Flute1Param, 0.8              ],
            _[ 0.50   , E5, Flute1Param, 0.8     , l[0.5] ],
            _[ 0.75   , G5, Flute1Param, MyCurve          ],
            _[ 1.00   , A5, Flute1Param, MyCurve , l[0.5] ],
            _[ t[1, 1], A4, Flute1Param                   ],
            _[ t[1, 2], C5, Flute1Param, 0.8              ],
            _[ t[1, 3], E5, Flute1Param, 0.8     , l[0.5] ],
            _[ t[1, 4], G5, Flute1Param, MyCurve          ],
            _[ t[2, 1], A5, Flute1Param, MyCurve , l[0.5] ]
            )).Play();
        }

        [TestMethod]
        public void NoteArrangement_Flute2Params_Test() => new NoteWishesTests().NoteArrangement_Flute2Params();
        void NoteArrangement_Flute2Params()
        {
            SetNoteArrangementOptions();
            Save(() => 0.1 * Add
            (
            _[          A4, Flute2Params                   ],
            _[          A4, Flute2Params, 0.8              ],
            _[          A4, Flute2Params, 0.8     , l[0.5] ],
            _[          A4, Flute2Params, MyCurve          ],
            _[          A4, Flute2Params, MyCurve , l[0.5] ],
            _[ 0.00   , A4, Flute2Params                   ],
            _[ 0.25   , C5, Flute2Params, 0.8              ],
            _[ 0.50   , E5, Flute2Params, 0.8     , l[0.5] ],
            _[ 0.75   , G5, Flute2Params, MyCurve          ],
            _[ 1.00   , A5, Flute2Params, MyCurve , l[0.5] ],
            _[ t[1, 1], A4, Flute2Params                   ],
            _[ t[1, 2], C5, Flute2Params, 0.8              ],
            _[ t[1, 3], E5, Flute2Params, 0.8     , l[0.5] ],
            _[ t[1, 4], G5, Flute2Params, MyCurve          ],
            _[ t[2, 1], A5, Flute2Params, MyCurve , l[0.5] ]
            )).Play();
        }

        [TestMethod] public void NoteArrangement_Flute3Params_Test() => new NoteWishesTests().NoteArrangement_Flute3Params();
        void NoteArrangement_Flute3Params()
        {
            SetNoteArrangementOptions();
            Save(() => 0.1 * Add
            (
            _[          A4, Flute3Params                   ],
            _[          A4, Flute3Params, 0.8              ],
            _[          A4, Flute3Params, 0.8     , l[0.5] ],
            _[          A4, Flute3Params, MyCurve          ],
            _[          A4, Flute3Params, MyCurve , l[0.5] ],
            _[ 0.00   , A4, Flute3Params                   ],
            _[ 0.25   , C5, Flute3Params, 0.8              ],
            _[ 0.50   , E5, Flute3Params, 0.8     , l[0.5] ],
            _[ 0.75   , G5, Flute3Params, MyCurve          ],
            _[ 1.00   , A5, Flute3Params, MyCurve , l[0.5] ],
            _[ t[1, 1], A4, Flute3Params                   ],
            _[ t[1, 2], C5, Flute3Params, 0.8              ],
            _[ t[1, 3], E5, Flute3Params, 0.8     , l[0.5] ],
            _[ t[1, 4], G5, Flute3Params, MyCurve          ],
            _[ t[2, 1], A5, Flute3Params, MyCurve , l[0.5] ],
            _[          A4, Flute3Params                  , fx1: TremoloSpeed],
            _[          A4, Flute3Params, 0.8             , fx1: TremoloSpeed],
            _[          A4, Flute3Params, 0.8     , l[0.5],      TremoloSpeed],
            _[          A4, Flute3Params, MyCurve         , fx1: TremoloSpeed],
            _[          A4, Flute3Params, MyCurve , l[0.5],      TremoloSpeed],
            _[ 0.00   , A4, Flute3Params                  , fx1: TremoloSpeed],
            _[ 0.25   , C5, Flute3Params, 0.8             , fx1: TremoloSpeed],
            _[ 0.50   , E5, Flute3Params, 0.8     , l[0.5],      TremoloSpeed],
            _[ 0.75   , G5, Flute3Params, MyCurve         , fx1: TremoloSpeed],
            _[ 1.00   , A5, Flute3Params, MyCurve , l[0.5],      TremoloSpeed],
            _[ t[1, 1], A4, Flute3Params                  , fx1: TremoloSpeed],
            _[ t[1, 2], C5, Flute3Params, 0.8             , fx1: TremoloSpeed],
            _[ t[1, 3], E5, Flute3Params, 0.8     , l[0.5],      TremoloSpeed],
            _[ t[1, 4], G5, Flute3Params, MyCurve         , fx1: TremoloSpeed],
            _[ t[2, 1], A5, Flute3Params, MyCurve , l[0.5],      TremoloSpeed]
            )).Play();
        }

        [TestMethod] public void NoteArrangement_Flute4Params_Test() => new NoteWishesTests().NoteArrangement_Flute4Params();
        void NoteArrangement_Flute4Params()
        {
            SetNoteArrangementOptions();
            Save(() => 0.1 * Add
            (
            _[          A4, Flute4Params                   ],
            _[          A4, Flute4Params, 0.8              ],
            _[          A4, Flute4Params, 0.8     , l[0.5] ],
            _[          A4, Flute4Params, MyCurve          ],
            _[          A4, Flute4Params, MyCurve , l[0.5] ],
            _[ 0.00   , A4, Flute4Params                   ],
            _[ 0.25   , C5, Flute4Params, 0.8              ],
            _[ 0.50   , E5, Flute4Params, 0.8     , l[0.5] ],
            _[ 0.75   , G5, Flute4Params, MyCurve          ],
            _[ 1.00   , A5, Flute4Params, MyCurve , l[0.5] ],
            _[ t[1, 1], A4, Flute4Params                   ],
            _[ t[1, 2], C5, Flute4Params, 0.8              ],
            _[ t[1, 3], E5, Flute4Params, 0.8     , l[0.5] ],
            _[ t[1, 4], G5, Flute4Params, MyCurve          ],
            _[ t[2, 1], A5, Flute4Params, MyCurve , l[0.5] ],
            _[          A4, Flute4Params                  , fx1: TremoloSpeed ],
            _[          A4, Flute4Params, 0.8             , fx1: TremoloSpeed ],
            _[          A4, Flute4Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[          A4, Flute4Params, MyCurve         , fx1: TremoloSpeed ],
            _[          A4, Flute4Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[ 0.00   , A4, Flute4Params                  , fx1: TremoloSpeed ],
            _[ 0.25   , C5, Flute4Params, 0.8             , fx1: TremoloSpeed ],
            _[ 0.50   , E5, Flute4Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[ 0.75   , G5, Flute4Params, MyCurve         , fx1: TremoloSpeed ],
            _[ 1.00   , A5, Flute4Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[ t[1, 1], A4, Flute4Params                  , fx1: TremoloSpeed ],
            _[ t[1, 2], C5, Flute4Params, 0.8             , fx1: TremoloSpeed ],
            _[ t[1, 3], E5, Flute4Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[ t[1, 4], G5, Flute4Params, MyCurve         , fx1: TremoloSpeed ],
            _[ t[2, 1], A5, Flute4Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[          A4, Flute4Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute4Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute4Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[          A4, Flute4Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute4Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ 0.00   , A4, Flute4Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 0.25   , C5, Flute4Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 0.50   , E5, Flute4Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ 0.75   , G5, Flute4Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 1.00   , A5, Flute4Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ t[1, 1], A4, Flute4Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[1, 2], C5, Flute4Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[1, 3], E5, Flute4Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ t[1, 4], G5, Flute4Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[2, 1], A5, Flute4Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ]
            )).Play();
        }

        [TestMethod] public void NoteArrangement_Flute5Params_Test() => new NoteWishesTests().NoteArrangement_Flute5Params();
        void NoteArrangement_Flute5Params()
        {
            SetNoteArrangementOptions();
            Save(() => 0.1 * Add
            (
            _[          A4, Flute5Params                   ],
            _[          A4, Flute5Params, 0.8              ],
            _[          A4, Flute5Params, 0.8     , l[0.5] ],
            _[          A4, Flute5Params, MyCurve          ],
            _[          A4, Flute5Params, MyCurve , l[0.5] ],
            _[ 0.00   , A4, Flute5Params                   ],
            _[ 0.25   , C5, Flute5Params, 0.8              ],
            _[ 0.50   , E5, Flute5Params, 0.8     , l[0.5] ],
            _[ 0.75   , G5, Flute5Params, MyCurve          ],
            _[ 1.00   , A5, Flute5Params, MyCurve , l[0.5] ],
            _[ t[1, 1], A4, Flute5Params                   ],
            _[ t[1, 2], C5, Flute5Params, 0.8              ],
            _[ t[1, 3], E5, Flute5Params, 0.8     , l[0.5] ],
            _[ t[1, 4], G5, Flute5Params, MyCurve          ],
            _[ t[2, 1], A5, Flute5Params, MyCurve , l[0.5] ],
            _[          A4, Flute5Params                  , fx1: TremoloSpeed ],
            _[          A4, Flute5Params, 0.8             , fx1: TremoloSpeed ],
            _[          A4, Flute5Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[          A4, Flute5Params, MyCurve         , fx1: TremoloSpeed ],
            _[          A4, Flute5Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[ 0.00   , A4, Flute5Params                  , fx1: TremoloSpeed ],
            _[ 0.25   , C5, Flute5Params, 0.8             , fx1: TremoloSpeed ],
            _[ 0.50   , E5, Flute5Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[ 0.75   , G5, Flute5Params, MyCurve         , fx1: TremoloSpeed ],
            _[ 1.00   , A5, Flute5Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[ t[1, 1], A4, Flute5Params                  , fx1: TremoloSpeed ],
            _[ t[1, 2], C5, Flute5Params, 0.8             , fx1: TremoloSpeed ],
            _[ t[1, 3], E5, Flute5Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[ t[1, 4], G5, Flute5Params, MyCurve         , fx1: TremoloSpeed ],
            _[ t[2, 1], A5, Flute5Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[          A4, Flute5Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute5Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute5Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[          A4, Flute5Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute5Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ 0.00   , A4, Flute5Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 0.25   , C5, Flute5Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 0.50   , E5, Flute5Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ 0.75   , G5, Flute5Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 1.00   , A5, Flute5Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ t[1, 1], A4, Flute5Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[1, 2], C5, Flute5Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[1, 3], E5, Flute5Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ t[1, 4], G5, Flute5Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[2, 1], A5, Flute5Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[          A4, Flute5Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[          A4, Flute5Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[          A4, Flute5Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[          A4, Flute5Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[          A4, Flute5Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ 0.00   , A4, Flute5Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ 0.25   , C5, Flute5Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ 0.50   , E5, Flute5Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ 0.75   , G5, Flute5Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ 1.00   , A5, Flute5Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ t[1, 1], A4, Flute5Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ t[1, 2], C5, Flute5Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ t[1, 3], E5, Flute5Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ t[1, 4], G5, Flute5Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ t[2, 1], A5, Flute5Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ]
            )).Play();
        }

        [TestMethod] public void NoteArrangement_Flute6Params_Test() => new NoteWishesTests().NoteArrangement_Flute6Params();
        void NoteArrangement_Flute6Params()
        {
            SetNoteArrangementOptions();
            Save(() => 0.1 * Add
            (
            _[          A4, Flute6Params                   ],
            _[          A4, Flute6Params, 0.8              ],
            _[          A4, Flute6Params, 0.8     , l[0.5] ],
            _[          A4, Flute6Params, MyCurve          ],
            _[          A4, Flute6Params, MyCurve , l[0.5] ],
            _[ 0.00   , A4, Flute6Params                   ],
            _[ 0.25   , C5, Flute6Params, 0.8              ],
            _[ 0.50   , E5, Flute6Params, 0.8     , l[0.5] ],
            _[ 0.75   , G5, Flute6Params, MyCurve          ],
            _[ 1.00   , A5, Flute6Params, MyCurve , l[0.5] ],
            _[ t[1, 1], A4, Flute6Params                   ],
            _[ t[1, 2], C5, Flute6Params, 0.8              ],
            _[ t[1, 3], E5, Flute6Params, 0.8     , l[0.5] ],
            _[ t[1, 4], G5, Flute6Params, MyCurve          ],
            _[ t[2, 1], A5, Flute6Params, MyCurve , l[0.5] ],
            _[          A4, Flute6Params                  , fx1: TremoloSpeed ],
            _[          A4, Flute6Params, 0.8             , fx1: TremoloSpeed ],
            _[          A4, Flute6Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[          A4, Flute6Params, MyCurve         , fx1: TremoloSpeed ],
            _[          A4, Flute6Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[ 0.00   , A4, Flute6Params                  , fx1: TremoloSpeed ],
            _[ 0.25   , C5, Flute6Params, 0.8             , fx1: TremoloSpeed ],
            _[ 0.50   , E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[ 0.75   , G5, Flute6Params, MyCurve         , fx1: TremoloSpeed ],
            _[ 1.00   , A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[ t[1, 1], A4, Flute6Params                  , fx1: TremoloSpeed ],
            _[ t[1, 2], C5, Flute6Params, 0.8             , fx1: TremoloSpeed ],
            _[ t[1, 3], E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed ],
            _[ t[1, 4], G5, Flute6Params, MyCurve         , fx1: TremoloSpeed ],
            _[ t[2, 1], A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed ],
            _[          A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[          A4, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[          A4, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ 0.00   , A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 0.25   , C5, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 0.50   , E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ 0.75   , G5, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ 1.00   , A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ t[1, 1], A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[1, 2], C5, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[1, 3], E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[ t[1, 4], G5, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6] ],
            _[ t[2, 1], A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6] ],
            _[          A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[          A4, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[          A4, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[          A4, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[          A4, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ 0.00   , A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ 0.25   , C5, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ 0.50   , E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ 0.75   , G5, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ 1.00   , A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ t[1, 1], A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ t[1, 2], C5, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ t[1, 3], E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[ t[1, 4], G5, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2] ],
            _[ t[2, 1], A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2] ],
            _[          A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[          A4, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[          A4, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2],      PanbrelloSpeed ],
            _[          A4, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[          A4, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2],      PanbrelloSpeed ],
            _[ 0.00   , A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[ 0.25   , C5, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[ 0.50   , E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2],      PanbrelloSpeed ],
            _[ 0.75   , G5, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[ 1.00   , A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2],      PanbrelloSpeed ],
            _[ t[1, 1], A4, Flute6Params                  , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[ t[1, 2], C5, Flute6Params, 0.8             , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[ t[1, 3], E5, Flute6Params, 0.8     , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2],      PanbrelloSpeed ],
            _[ t[1, 4], G5, Flute6Params, MyCurve         , fx1: TremoloSpeed, fx2: _[0.6], fx3: _[0.2], fx4: PanbrelloSpeed ],
            _[ t[2, 1], A5, Flute6Params, MyCurve , l[0.5],      TremoloSpeed,      _[0.6],      _[0.2],      PanbrelloSpeed ]
            )).Play();
        }

        [TestMethod] public void NoteArrangement_TimeIndexers_Test() => new NoteWishesTests().NoteArrangement_TimeIndexers();
        void NoteArrangement_TimeIndexers()
        {
            SetNoteArrangementOptions();
            Save(() => 0.7 * Add
            (
            _[ b[1]            , A4, Flute3Params                                ],
            _[ b[2]            , C5, Flute4Params, 0.6                           ],
            _[ b[3]            , G4, Flute5Params, MyCurve, len[0.5]             ],
            _[ beat[4]         , D5, Flute5Params, _[0.4] , l[0.8], _[7]         ],
            _[ bar[2] + beat[1], A4, Flute5Params, MyCurve, _[0.2], _[7], _[0.6] ],
            _[ 1.75            , E5, Flute5Params, MyCurve, len[0.5]             ]
            )).Play();
        }
    }
}