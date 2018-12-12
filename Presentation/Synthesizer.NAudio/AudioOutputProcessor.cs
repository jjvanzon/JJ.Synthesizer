using System;
using System.Threading;
using JJ.Framework.Exceptions.Comparative;
using NAudio.Wave;

namespace JJ.Presentation.Synthesizer.NAudio
{
	/// <summary> Thread-safe. Most of the work is done by the AudioOutputSampleProvider. </summary>
	internal class AudioOutputProcessor
	{
		private readonly AudioOutputSampleProvider _sampleProvider;
		private readonly object _lock = new object();

		private int _desiredLatencyInMilliseconds;
		private WaveOut _waveOut;

		/// <summary>
		/// Even though this constructor initializes a lot of things,
		/// you can still mutate some of the things using the UpdateAudioProperties method.
		/// </summary>
		public AudioOutputProcessor(
			IPatchCalculatorContainer patchCalculatorContainer,
			TimeProvider timeProvider,
			int samplingRate,
			int channelCount,
			double desiredBufferDuration)
		{
			_sampleProvider = new AudioOutputSampleProvider(patchCalculatorContainer, timeProvider, samplingRate, channelCount);

			UpdateAudioProperties(samplingRate, channelCount, desiredBufferDuration);
		}

		public void UpdateAudioProperties(int samplingRate, int channelCount, double desiredBufferDuration)
		{
			lock (_lock)
			{
				_desiredLatencyInMilliseconds = ConvertDurationToMilliseconds(desiredBufferDuration);
				_sampleProvider.SetAudioProperties(samplingRate, channelCount);
			}
		}

		public void StartThread()
		{
			var thread = new Thread(Start);
			thread.Start();

			// Starting AudioOutputProcessor on another thread seems to 
			// start and keep alive a new Windows message loop.
			// The thread keeps running too.
		}

		private void Start()
		{
			try
			{
				lock (_lock)
				{
					_waveOut = new WaveOut
					{
						DesiredLatency = _desiredLatencyInMilliseconds
					};

					_waveOut.Init(_sampleProvider);

					_sampleProvider.IsRunning = true;

					_waveOut.Play();
				}
			}
			catch
			{
				// Do not crash your whole application, if midi device communication fails.
				Stop();
			}
		}

		public void Stop()
		{
			try
			{
				lock (_lock)
				{
					if (_waveOut != null)
					{
						_waveOut.Stop();
						_waveOut.Dispose();
					}

					_sampleProvider.IsRunning = false;
				}
			}
			catch
			{
				// Do not crash your whole application, if audio device communication fails.
			}
		}

		// Helpers

		private int ConvertDurationToMilliseconds(double duration)
		{
			if (duration <= 0.0) throw new LessThanOrEqualException(() => duration, 0.0);
			var milliseconds = (int)Math.Ceiling(duration * 1000.0);
			return milliseconds;
		}
	}
}
