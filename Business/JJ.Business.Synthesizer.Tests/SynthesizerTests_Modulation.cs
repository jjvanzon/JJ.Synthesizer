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
        public void Test_Synthesizer_Additive_ModulatedComposition()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new SynthesizerTests_Additive(context).Test_Additive_ModulatedComposition();
        }

        private void Test_Additive_ModulatedComposition()
            => WriteToAudioFile(MildEcho(ModulatedComposition()), volume: 0.30, duration: t[bar: 9, beat: 2] + MILD_ECHO_TIME);

        #endregion

        #region Composition

        private Outlet ModulatedComposition()
        {
            double detuneAmount = 0.02;
            double vibratoAmount = 0.005;
            double tremoloAmount = 0.25;

            var melody = Adder
            (
                Multiply(_[0.8], LongModulatedNote(_[Notes.A4], detuneAmount, vibratoAmount, tremoloAmount)),
                Multiply(_[0.7], LongModulatedNote(_[Notes.B4], detuneAmount, vibratoAmount, tremoloAmount)),
                Multiply(_[0.85], LongModulatedNote(_[Notes.C5], detuneAmount, vibratoAmount, tremoloAmount)),
                Multiply(_[0.75], LongModulatedNote(_[Notes.D5], detuneAmount, vibratoAmount, tremoloAmount)),
                Multiply(_[0.9], LongModulatedNote(_[Notes.E5], detuneAmount, vibratoAmount, tremoloAmount))
            );

            return melody;
        }

        #endregion

        #region Instruments

        private Outlet LongModulatedNote(Outlet freq, double detuneAmount, double vibratoAmount, double tremoloAmount)
        {
            // Base additive synthesis with harmonic content
            var harmonicContent = Adder
            (
                Sine(freq),
                Sine(Multiply(freq, _[2]), _[0.5]),
                Sine(Multiply(freq, _[3]), _[0.3]),
                Sine(Multiply(freq, _[4]), _[0.2])
            );

            // Apply detune by modulating frequencies slightly
            var detunedContent = Adder
            (
                Sine(Multiply(freq, Add(_[1], _[detuneAmount]))),
                Sine(Multiply(freq, Add(_[2], _[detuneAmount]))),
                Sine(Multiply(freq, Add(_[3], _[detuneAmount]))),
                Sine(Multiply(freq, Add(_[4], _[detuneAmount])))
            );

            // Apply vibrato by modulating frequency over time using an oscillator
            var vibrato = Sine(Multiply(_[vibratoAmount], Sine(_[5.5]))); // 5.5 Hz vibrato
            var vibratoSignal = Multiply(harmonicContent, vibrato);

            // Apply tremolo by modulating amplitude over time using an oscillator
            var tremolo = Sine(Multiply(_[tremoloAmount], Sine(_[4]))); // 4 Hz tremolo
            var tremoloSignal = Multiply(vibratoSignal, tremolo);

            // Stretch and apply modulation over time
            var modulatedNote = StretchCurve(ModulationCurve, tremoloSignal);

            return modulatedNote;
        }

        #endregion

        #region Algorithms

        private Outlet StretchCurve(Curve curve, Outlet signal)
            => Multiply(signal, CurveIn(curve));

        private const double MILD_ECHO_TIME = 0.33 * 5;

        private Outlet MildEcho(Outlet outlet)
            => EntityFactory.CreateEcho(this, outlet, count: 6, denominator: 4, delay: 0.33);

        #endregion

        #region Curves

        private Curve ModulationCurve => CurveFactory.CreateCurve(
            start: 0, end: 1, min: 0, max: 1,
            "   o                 ",
            " o   o               ",
            "                     ",
            "           o         ",
            "o                   o");

        #endregion
    }
}
