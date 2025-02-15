using System;
using System.IO;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests.Technical
{
	[TestClass]
    [TestCategory("Technical")]
	public class HardCodedFMTests
	{
		[TestMethod]
		public void HardCodedFM_NoPhaseTracking_32BitFloat_HardFlute_IsNoise()
			=> RunTest_HardCodedFM_NoPhaseTracking_32BitFloat(carrierFrequency: 440f, modulationFrequency: 880f, modulationDepth: 5f);

		[TestMethod]
		public void HardCodedFM_NoPhaseTracking_32BitFloat_WapWap() 
			=> RunTest_HardCodedFM_NoPhaseTracking_32BitFloat(carrierFrequency: 440f, modulationFrequency: 5f, modulationDepth: 0.1f);

		void RunTest_HardCodedFM_NoPhaseTracking_32BitFloat(
			float carrierFrequency, 
			float modulationFrequency, 
			float modulationDepth,
			[CallerMemberName] string callerMemberName = null)
		{
			// Audio parameters
			int sampleRate = 44100;
			int duration = 5; // Duration in seconds
			string outputFileName = $"{callerMemberName}.wav";
			int sampleCount = sampleRate * duration;

			using (var fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				using (var writer = new BinaryWriter(fileStream))
				{
					writer.WriteWavHeader<Int16>(MonoChannels, sampleRate, sampleCount);

					// Generate samples
					for (int i = 0; i < sampleCount; i++)
					{
						// Calculate the modulator and carrier signals
						float modulator = (float)Math.Sin(2 * Math.PI * modulationFrequency * i / sampleRate);
						float modulatedFrequency = carrierFrequency + modulationDepth * carrierFrequency * modulator;
						float sample = (float)Math.Sin(2 * Math.PI * modulatedFrequency * i / sampleRate);
						short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

						writer.Write(scaledSample);
					}
				}
			}

			Console.WriteLine($"FM sound saved to {Path.GetFullPath(outputFileName)}");
		}

		[TestMethod]
		public void HardCodedFM_NoPhaseTracking_64BitDouble_HardFlute_IsNoise()
			=> RunTest_HardCodedFM_NoPhaseTracking_64BitDouble(carrierFrequency: 440.0, modulationFrequency: 880.0, modulationDepth: 5.0);

		[TestMethod]
		public void HardCodedFM_NoPhaseTracking_64BitDouble_WapWap()
			=> RunTest_HardCodedFM_NoPhaseTracking_64BitDouble(carrierFrequency: 440.0, modulationFrequency: 5.0, modulationDepth: 0.1);

		void RunTest_HardCodedFM_NoPhaseTracking_64BitDouble(
			double carrierFrequency,
			double modulationFrequency,
			double modulationDepth,
			[CallerMemberName] string callerMemberName = null)
		{
			// Audio parameters
			int sampleRate = 44100;
			int duration = 5; // Duration in seconds
			string outputFileName = $"{callerMemberName}.wav";
			int sampleCount = sampleRate * duration;

			using (var fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				using (var writer = new BinaryWriter(fileStream))
				{
					writer.WriteWavHeader<Int16>(MonoChannels, sampleRate, sampleCount);

					// Generate samples
					for (int i = 0; i < sampleCount; i++)
					{
						// Calculate the modulator and carrier signals
						double modulator = Math.Sin(2 * Math.PI * modulationFrequency * i / sampleRate);
						double modulatedFrequency = carrierFrequency + modulationDepth * carrierFrequency * modulator;
						double sample = Math.Sin(2 * Math.PI * modulatedFrequency * i / sampleRate);
						short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

						writer.Write(scaledSample);
					}
				}
			}

			Console.WriteLine($"FM sound saved to {Path.GetFullPath(outputFileName)}");
		}

		[TestMethod]
		public void HardCodedFM_WithPhaseTracking_32BitFloat_HardFlute()
			=> RunTest_HardCodedFM_WithPhaseTracking_32BitFloat(carrierFrequency: 440f, modulationFrequency: 880f, modulationDepth: 5f);

		[TestMethod]
		public void HardCodedFM_WithPhaseTracking_32BitFloat_WapWap()
			=> RunTest_HardCodedFM_WithPhaseTracking_32BitFloat(carrierFrequency: 440f, modulationFrequency: 10f, modulationDepth: 50f);

		void RunTest_HardCodedFM_WithPhaseTracking_32BitFloat(
			float carrierFrequency,
			float modulationFrequency,
			float modulationDepth,
			[CallerMemberName] string callerMemberName = null)
		{
			// Audio parameters
			int sampleRate = 44100;
			int duration = 5; // Duration in seconds
			string outputFileName = $"{callerMemberName}.wav";
			int sampleCount = sampleRate * duration;

			using (var fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				using (var writer = new BinaryWriter(fileStream))
				{
					writer.WriteWavHeader<Int16>(MonoChannels, sampleRate, sampleCount);

					// Generate samples
					float carrierPhase = 0f;
					float modulatorPhase = 0f;
					float twoPi = 2 * (float)Math.PI;

					for (int i = 0; i < sampleCount; i++)
					{
						// Update phases
						carrierPhase += twoPi * carrierFrequency / sampleRate;
						modulatorPhase += twoPi * modulationFrequency / sampleRate;

						// Calculate the modulator and carrier signals
						float modulator = (float)Math.Sin(modulatorPhase);
						
                        // Generate sample
						float sample = (float)Math.Sin(carrierPhase + modulator * modulationDepth);
						short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

						writer.Write(scaledSample);
					}
				}
			}

			Console.WriteLine($"FM sound saved to {Path.GetFullPath(outputFileName)}");
		}

		[TestMethod]
		public void HardCodedFM_WithPhaseTracking_64BitDouble_HardFlute()
			=> RunTest_HardCodedFM_WithPhaseTracking_64BitDouble(carrierFrequency: 440.0, modulationFrequency: 880.0, modulationDepth: 5.0);

		[TestMethod]
		public void HardCodedFM_WithPhaseTracking_64BitDouble_WapWap()
			=> RunTest_HardCodedFM_WithPhaseTracking_64BitDouble(carrierFrequency: 440.0, modulationFrequency: 10.0, modulationDepth: 50.0);

		void RunTest_HardCodedFM_WithPhaseTracking_64BitDouble(
			double carrierFrequency,
			double modulationFrequency,
			double modulationDepth,
			[CallerMemberName] string callerMemberName = null)
		{
			// Audio parameters
			int sampleRate = 44100;
			int duration = 5; // Duration in seconds
			string outputFileName = $"{callerMemberName}.wav";
			int sampleCount = sampleRate * duration;

			using (var fileStream = new FileStream(outputFileName, FileMode.Create, FileAccess.Write, FileShare.Read))
			{
				using (var writer = new BinaryWriter(fileStream))
				{
					writer.WriteWavHeader<Int16>(MonoChannels, sampleRate, sampleCount);

					// Generate samples
					double carrierPhase = 0f;
					double modulatorPhase = 0f;
					double twoPi = 2 * Math.PI;

					for (int i = 0; i < sampleCount; i++)
					{
						// Update phases
						carrierPhase += twoPi * carrierFrequency / sampleRate;
						modulatorPhase += twoPi * modulationFrequency / sampleRate;

						// Calculate the modulator and carrier signals
						double modulator = Math.Sin(modulatorPhase);

						// Generate sample
						double sample = Math.Sin(carrierPhase + (modulator * modulationDepth));
						short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

						writer.Write(scaledSample);
					}
				}
			}

			Console.WriteLine($"FM sound saved to {Path.GetFullPath(outputFileName)}");
		}
	}
}
