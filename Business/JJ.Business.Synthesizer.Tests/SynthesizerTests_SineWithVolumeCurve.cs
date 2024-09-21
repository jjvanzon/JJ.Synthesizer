using JJ.Business.Synthesizer.Calculation.AudioFileOutputs;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Validation;
using JJ.Business.Synthesizer.Validation.Entities;
using JJ.Business.Synthesizer.Warnings;
using JJ.Business.Synthesizer.Warnings.Entities;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Framework.Validation;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace JJ.Business.Synthesizer.Tests
{
	public class SineWithVolumeCurveTester
	{
		private IContext _context;

		public SineWithVolumeCurveTester(IContext context)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
		}

		/// <summary>
		/// Generates a Sine wave with a Volume Curve, testing both Block and Line Interpolation.
		/// Verifies data using (Warning)Validators and writes the output audio to a file.
		/// </summary>
		public void Test_Synthesizer_Sine_With_Volume_Curve()
		{
			// Arrange
			var curveFactory = TestHelper.CreateCurveFactory(_context);
			Curve curve = curveFactory.CreateCurve
			(
				new NodeInfo(time: 0.00, value: 0.00, NodeTypeEnum.Line),
				new NodeInfo(time: 0.05, value: 0.95, NodeTypeEnum.Line),
				new NodeInfo(time: 0.10, value: 1.00, NodeTypeEnum.Line),
				new NodeInfo(time: 0.20, value: 0.60, NodeTypeEnum.Line),
				new NodeInfo(time: 0.80, value: 0.20, NodeTypeEnum.Block),
				new NodeInfo(time: 1.00, value: 0.00, NodeTypeEnum.Block),
				new NodeInfo(time: 1.20, value: 0.20, NodeTypeEnum.Block),
				new NodeInfo(time: 1.40, value: 0.08, NodeTypeEnum.Block),
				new NodeInfo(time: 1.60, value: 0.30, NodeTypeEnum.Line),
				new NodeInfo(time: 4.00, value: 0.00, NodeTypeEnum.Line)
			);

			Outlet outlet;
			{
				var x = TestHelper.CreateOperatorFactory(_context);
				outlet = x.Sine(x.CurveIn(curve), x.Value(880));
			}

			AudioFileOutputManager audioFileOutputManager = TestHelper.CreateAudioFileOutputManager(_context);
			AudioFileOutput audioFileOutput = audioFileOutputManager.CreateAudioFileOutput();
			audioFileOutput.AudioFileOutputChannels[0].Outlet = outlet;
			audioFileOutput.FilePath = $"{MethodBase.GetCurrentMethod().Name}.wav";
			audioFileOutput.Duration = 4;
			audioFileOutput.Amplifier = 32000;

			// Verify
			IValidator[] validators =
			{
				new CurveValidator(curve),
				new VersatileOperatorValidator(outlet.Operator),
				new AudioFileOutputValidator(audioFileOutput),
				new AudioFileOutputWarningValidator(audioFileOutput),
				new VersatileOperatorWarningValidator(outlet.Operator)
			};
			validators.ForEach(x => x.Verify());

			// Calculate
			IAudioFileOutputCalculator audioFileOutputCalculator = AudioFileOutputCalculatorFactory.CreateAudioFileOutputCalculator(audioFileOutput);
			var sw = Stopwatch.StartNew();
			audioFileOutputCalculator.Execute();
			sw.Stop();

			// Report
			string filePath = Path.GetFullPath(audioFileOutput.FilePath);
			Assert.Inconclusive($"{sw.ElapsedMilliseconds}ms{Environment.NewLine}Output file: {filePath}");
		}
	}
}