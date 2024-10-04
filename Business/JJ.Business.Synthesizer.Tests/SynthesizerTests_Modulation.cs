using JetBrains.Annotations;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTests_Additive : SynthesizerSugarBase
    {
        [UsedImplicitly]
        public SynthesizerTests_Additive()
        { }

        public SynthesizerTests_Additive(IContext context)
            : base(context, beat: 0.45, bar: 4 * 0.45)
        { }

        #region Tests

        [TestMethod]
        public void Test_Synthesizer_Modulation_ShortBurstChord()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_Additive(context).Test_Modulation_ShortBurstChord();
        }

        private void Test_Modulation_ShortBurstChord()
            => WriteToAudioFile(MildEcho(ShortBurstChord()),
                                volume: 0.30,
                                duration: t[bar: 9, beat: 2] + MILD_ECHO_TIME);

        [TestMethod]
        public void Test_Synthesizer_Modulation_LongNoteComposition_DoesNotWork()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_Additive(context).Test_Modulation_LongNoteComposition_DoesNotWork();
        }

        private void Test_Modulation_LongNoteComposition_DoesNotWork()
            => WriteToAudioFile(MildEcho(LongNotesComposition_DoesNotWork()),
                                volume: 0.30,
                                duration: t[bar: 9, beat: 2] + MILD_ECHO_TIME);

        #endregion

        #region Composition

        private Outlet ShortBurstChord()
        {
            var vibratoDepth = _[0.005];
            var tremoloDepth = _[0.25];

            var melody = Adder
            (
                Multiply(_[0.80], ShortBurst(_[Notes.A4], vibratoDepth, tremoloDepth)),
                Multiply(_[0.70], ShortBurst(_[Notes.B4], vibratoDepth, tremoloDepth)),
                Multiply(_[0.85], ShortBurst(_[Notes.C5], vibratoDepth, tremoloDepth)),
                Multiply(_[0.75], ShortBurst(_[Notes.D5], vibratoDepth, tremoloDepth)),
                Multiply(_[0.90], ShortBurst(_[Notes.E5], vibratoDepth, tremoloDepth))
            );

            return melody;
        }

        private Outlet LongNotesComposition_DoesNotWork()
        {
            var detuneDepth = _[0.02];
            var vibratoDepth = _[0.005];
            var tremoloDepth = _[0.25];

            var melody = Adder
            (
                Multiply(_[0.80], LongModulatedNote(_[Notes.A4], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.70], LongModulatedNote(_[Notes.B4], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.85], LongModulatedNote(_[Notes.C5], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.75], LongModulatedNote(_[Notes.D5], detuneDepth, vibratoDepth, tremoloDepth)),
                Multiply(_[0.90], LongModulatedNote(_[Notes.E5], detuneDepth, vibratoDepth, tremoloDepth))
            );

            return melody;
        }

        #endregion

        #region Instruments

        private Outlet ShortBurst(Outlet freq, Outlet vibratoDepth, Outlet tremoloDepth)
        {
            // Base additive synthesis with harmonic content
            var harmonicContent = Adder
            (
                Sine(_[1], freq),
                Sine(_[0.5], Multiply(freq, _[2])),
                Sine(_[0.3], Multiply(freq, _[3])),
                Sine(_[0.2], Multiply(freq, _[4]))
            );

            // Apply vibrato by modulating frequency over time using an oscillator
            var vibrato = Sine(Add(_[1], vibratoDepth), _[5.5]); // 5.5 Hz vibrato
            var soundWithVibrato = Multiply(harmonicContent, vibrato);

            // Apply tremolo by modulating amplitude over time using an oscillator
            var tremolo = Sine(Add(_[1], tremoloDepth), _[4]); // 4 Hz tremolo
            var soundWithTremolo = Multiply(soundWithVibrato, tremolo);

            // Stretch and apply modulation over time
            var noteWithEnvelope = Multiply(soundWithTremolo, CurveIn(VolumeCurve));

            return noteWithEnvelope;
        }

        private Outlet LongModulatedNote(
            Outlet freq, Outlet detuneDepth, Outlet vibratoDepth, Outlet tremoloDepth)
        {
            // Base additive synthesis with harmonic content
            var harmonicContent = Adder
            (
                Sine(_[1], freq),
                Sine(_[0.5], Multiply(freq, _[2])),
                Sine(_[0.3], Multiply(freq, _[3])),
                Sine(_[0.2], Multiply(freq, _[4]))
            );

            // Apply detune by modulating frequencies slightly
            var detunedContent = Adder
            (
                Sine(_[1], Multiply(freq, Add(_[1], detuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[2], detuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[3], detuneDepth))),
                Sine(_[1], Multiply(freq, Add(_[4], detuneDepth)))
            );

            // Apply vibrato by modulating frequency over time using an oscillator
            var vibrato = Sine(Add(_[1], vibratoDepth), _[5.5]); // 5.5 Hz vibrato
            var soundWithVibrato = Multiply(harmonicContent, vibrato);

            // Apply tremolo by modulating amplitude over time using an oscillator
            var tremolo = Sine(Add(_[1], tremoloDepth), _[4]); // 4 Hz tremolo
            var soundWithTremolo = Multiply(soundWithVibrato, tremolo);

            // Stretch and apply modulation over time
            var noteWithEnvelope = Multiply(soundWithTremolo, CurveIn(VolumeCurve));

            return noteWithEnvelope;
        }
        #endregion

        #region Algorithms

        private const double MILD_ECHO_TIME = 0.33 * 5;

        private Outlet MildEcho(Outlet outlet)
            => EntityFactory.CreateEcho(this, outlet, count: 6, denominator: 4, delay: 0.33);

        #endregion

        #region Curves

        private Curve VolumeCurve => CurveFactory.CreateCurve(
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o");

        #endregion
    }
}
