﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Reflection;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Enums;
using System.Runtime.CompilerServices;

[TestClass]
public class SynthesizerTester_FMHardCoded
{
	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_NoPhaseTracking_32BitFloat_HardFlute_IsNoise()
		=> Test_NoPhaseTracking_32BitFloat(carrierFrequency: 440f, modulationFrequency: 880f, modulationDepth: 5f);

	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_NoPhaseTracking_32BitFloat_WapWap() 
		=> Test_NoPhaseTracking_32BitFloat(carrierFrequency: 440f, modulationFrequency: 5f, modulationDepth: 0.1f);

	private void Test_NoPhaseTracking_32BitFloat(
		float carrierFrequency, 
		float modulationFrequency, 
		float modulationDepth,
		[CallerMemberName] string callerMemberName = null)
	{
		// Audio parameters
		int sampleRate = 44100;
		int duration = 5; // Duration in seconds
		string outputFilePath = $"{callerMemberName}.wav";
		int sampleCount = sampleRate * duration;

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				bw.WriteWavHeader<Int16>(SpeakerSetupEnum.Mono, sampleRate, sampleCount);

				// Generate samples
				for (int i = 0; i < sampleCount; i++)
				{
					// Calculate the modulator and carrier signals
					float modulator = (float)Math.Sin(2 * Math.PI * modulationFrequency * i / sampleRate);
					float modulatedFrequency = carrierFrequency + modulationDepth * carrierFrequency * modulator;
					float sample = (float)Math.Sin(2 * Math.PI * modulatedFrequency * i / sampleRate);
					short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

					bw.Write(scaledSample);
				}
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");
	}

	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_NoPhaseTracking_64BitDouble_HardFlute_IsNoise()
	{
		// Audio parameters
		int sampleRate = 44100;
		double carrierFrequency = 440.0;
		double modulationFrequency = 880.0;
		double modulationDepth = 5.0;
		int duration = 5; // Duration in seconds
		string outputFilePath = MethodBase.GetCurrentMethod().Name + ".wav";
		int sampleCount = sampleRate * duration;

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				bw.WriteWavHeader<Int16>(SpeakerSetupEnum.Mono, sampleRate, sampleCount);

				// Generate samples
				for (int i = 0; i < sampleCount; i++)
				{
					// Calculate the modulator and carrier signals
					double modulator = (double)Math.Sin(2 * Math.PI * modulationFrequency * i / sampleRate);
					double modulatedFrequency = carrierFrequency + modulationDepth * carrierFrequency * modulator;
					double sample = (double)Math.Sin(2 * Math.PI * modulatedFrequency * i / sampleRate);
					short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

					bw.Write(scaledSample);
				}
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");
	}

	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_NoPhaseTracking_64BitDouble_WapWap()
	{
		// Audio parameters
		int sampleRate = 44100;
		double carrierFrequency = 440f;
		double modulationFrequency = 5f;
		double modulationDepth = 0.1f;
		int duration = 5; // Duration in seconds
		string outputFilePath = MethodBase.GetCurrentMethod().Name + ".wav";
		int sampleCount = sampleRate * duration;

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				bw.WriteWavHeader<Int16>(SpeakerSetupEnum.Mono, sampleRate, sampleCount);

				// Generate samples
				for (int i = 0; i < sampleCount; i++)
				{
					// Calculate the modulator and carrier signals
					double modulator = (double)Math.Sin(2 * Math.PI * modulationFrequency * i / sampleRate);
					double modulatedFrequency = carrierFrequency + modulationDepth * carrierFrequency * modulator;
					double sample = (double)Math.Sin(2 * Math.PI * modulatedFrequency * i / sampleRate);
					short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

					bw.Write(scaledSample);
				}
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");
	}

	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_WithPhaseTracking_32BitFloat_HardFlute()
	{
		// Audio parameters
		int sampleRate = 44100;
		float carrierFrequency = 440f;
		float modulationFrequency = 880f;
		float modulationDepth = 5f;
		int duration = 5; // Duration in seconds
		string outputFilePath = MethodBase.GetCurrentMethod().Name + ".wav";
		int sampleCount = sampleRate * duration;

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				bw.WriteWavHeader<Int16>(SpeakerSetupEnum.Mono, sampleRate, sampleCount);

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
					float modulatedFrequency = carrierFrequency + modulationDepth * modulator; // Adjust frequency with modulation depth

					// Generate sample
					float sample = (float)Math.Sin(carrierPhase + (modulator * modulationDepth));
					short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

					bw.Write(scaledSample);
				}
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");
	}

	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_WithPhaseTracking_32BitFloat_WapWap()
	{
		// Audio parameters
		int sampleRate = 44100;
		float carrierFrequency = 440f;
		float modulationFrequency = 10f;
		float modulationDepth = 50f;
		int duration = 5; // Duration in seconds
		string outputFilePath = MethodBase.GetCurrentMethod().Name + ".wav";
		int sampleCount = sampleRate * duration;

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				bw.WriteWavHeader<Int16>(SpeakerSetupEnum.Mono, sampleRate, sampleCount);

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
					float modulatedFrequency = carrierFrequency + modulationDepth * modulator; // Adjust frequency with modulation depth

					// Generate sample
					float sample = (float)Math.Sin(carrierPhase + (modulator * modulationDepth));
					short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

					bw.Write(scaledSample);
				}
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");
	}

	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_WithPhaseTracking_64BitDouble_HardFlute()
	{
		// Audio parameters
		int sampleRate = 44100;
		double carrierFrequency = 440.0;
		double modulationFrequency = 880.0;
		double modulationDepth = 5.0;
		int duration = 5; // Duration in seconds
		string outputFilePath = MethodBase.GetCurrentMethod().Name + ".wav";
		int sampleCount = sampleRate * duration;

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				bw.WriteWavHeader<Int16>(SpeakerSetupEnum.Mono, sampleRate, sampleCount);

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
					double modulatedFrequency = carrierFrequency + modulationDepth * modulator; // Adjust frequency with modulation depth

					// Generate sample
					double sample = Math.Sin(carrierPhase + (modulator * modulationDepth));
					short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

					bw.Write(scaledSample);
				}
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");
	}

	[TestMethod]
	public void Test_Synthesizer_FM_HardCoded_WithPhaseTracking_64BitDouble_WapWap()
	{
		// Audio parameters
		int sampleRate = 44100;
		double carrierFrequency = 440.0;
		double modulationFrequency = 10.0;
		double modulationDepth = 50.0;
		int duration = 5; // Duration in seconds
		string outputFilePath = MethodBase.GetCurrentMethod().Name + ".wav";
		int sampleCount = sampleRate * duration;

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				bw.WriteWavHeader<Int16>(SpeakerSetupEnum.Mono, sampleRate, sampleCount);

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
					double modulatedFrequency = carrierFrequency + modulationDepth * modulator; // Adjust frequency with modulation depth

					// Generate sample
					double sample = Math.Sin(carrierPhase + (modulator * modulationDepth));
					short scaledSample = (short)(sample * 32000); // Scale to 16-bit range

					bw.Write(scaledSample);
				}
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");
	}
}