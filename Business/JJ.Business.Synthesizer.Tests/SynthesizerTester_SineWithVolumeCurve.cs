using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Framework.Persistence;
using JJ.Persistence.Synthesizer;

namespace JJ.Business.Synthesizer.Tests
{
    public class SynthesizerTester_SineWithVolumeCurve : SynthesizerSugarBase
    {
        public SynthesizerTester_SineWithVolumeCurve(IContext context)
            : base(context)
        { }

        /// <summary>
        /// Generates a Sine wave with a Volume Curve, testing both Block and Line Interpolation.
        /// Verifies data using (Warning)Validators and writes the output audio to a file.
        /// </summary>
        public void Test_Synthesizer_Sine_With_Volume_Curve()
        {
            // Arrange
            Curve curve = CurveFactory.CreateCurve
            (
                new NodeInfo(time: 0.00, value: 0.00),
                new NodeInfo(time: 0.05, value: 0.95),
                new NodeInfo(time: 0.10, value: 1.00),
                new NodeInfo(time: 0.20, value: 0.60),
                new NodeInfo(time: 0.80, value: 0.20),
                new NodeInfo(time: 1.00, value: 0.00),
                new NodeInfo(time: 1.20, value: 0.20),
                new NodeInfo(time: 1.40, value: 0.08),
                new NodeInfo(time: 1.60, value: 0.30),
                new NodeInfo(time: 4.00, value: 0.00)
            );
            new CurveValidator(curve).Verify();

            Outlet outlet = Sine(CurveIn(curve), _[880]);

            WriteToAudioFile(outlet, duration: 4);
        }
    }
}