using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;

[TestClass]
public class SynthesizerHardCodedFMTests
{
	[TestMethod]
	public void GenerateConstantTimbreFmSoundWavFile()
	{
		// Audio parameters
		int sampleRate = 44100;
		float carrierFrequency = 440f; // Carrier frequency
		float modulationFrequency = 880f; // Modulation frequency
		float modulationDepth = 5f; // Modulation depth
		int duration = 5; // Duration in seconds
		string outputFilePath = "FM_Sound.wav"; // Output WAV file path

		using (var fs = new FileStream(outputFilePath, FileMode.Create))
		{
			using (var bw = new BinaryWriter(fs))
			{
				// Write WAV header
				bw.Write("RIFF".ToCharArray());
				bw.Write(0); // Placeholder for file size
				bw.Write("WAVE".ToCharArray());
				bw.Write("fmt ".ToCharArray());
				bw.Write(16); // Subchunk1Size
				bw.Write((short)1); // AudioFormat (PCM)
				bw.Write((short)1); // NumChannels
				bw.Write(sampleRate); // SampleRate
				bw.Write(sampleRate * 2); // ByteRate
				bw.Write((short)2); // BlockAlign
				bw.Write((short)16); // BitsPerSample
				bw.Write("data".ToCharArray());
				bw.Write(0); // Placeholder for data chunk size

				// Generate samples
				int sampleCount = sampleRate * duration;
				int dataStartPosition = (int)fs.Position;

				float carrierPhase = 0f; // Phase of the carrier
				float modulatorPhase = 0f; // Phase of the modulator
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
					short scaledSample = (short)(sample * short.MaxValue); // Scale to 16-bit range

					bw.Write(scaledSample);
				}

				// Update sizes in the header
				long fileSize = fs.Length - 8; // Exclude RIFF header
				fs.Seek(4, SeekOrigin.Begin);
				bw.Write((int)fileSize);
				fs.Seek(dataStartPosition - 4, SeekOrigin.Begin);
				bw.Write((int)(fs.Length - dataStartPosition - 8)); // Size of data chunk
			}
		}

		Console.WriteLine($"FM sound saved to {outputFilePath}");

		// Optional: Assert that the file was created
		Assert.IsTrue(File.Exists(outputFilePath), "WAV file was not created successfully.");
	}
}
