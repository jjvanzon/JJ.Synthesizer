//using System;
//using System.Collections.Generic;
//using System.Linq;
//using JJ.Data.Synthesizer;
//using JJ.Framework.Reflection.Exceptions;
//using NAudio.Wave;

//namespace JJ.Presentation.Synthesizer.NAudio
//{
//    /// <summary> Most of the work is done by the AudioOutputSampleProvider. </summary>
//    public class AudioOutputProcessor
//    {
//        public AudioOutputProcessor(IPatchCalculatorContainer patchCalculatorContainer, AudioOutput audioOutput)
//        {
//            if (patchCalculatorContainer == null) throw new NullException(() => patchCalculatorContainer);

//            _patchCalculatorContainer = patchCalculatorContainer;

//            SetAudioOutput(audioOutput);
//        }
        
//        private readonly IPatchCalculatorContainer _patchCalculatorContainer;

//        private AudioOutputSampleProvider _sampleProvider;
//        private WaveOut _waveOut;
//        private double _bufferDurationInSeconds;

//        private void SetAudioOutput(AudioOutput audioOutput)
//        {
//            if (audioOutput == null) throw new NullException(() => audioOutput);

//            bool wasRunning = IsRunning;

//            if (wasRunning)
//            {
//                Stop();
//            }

//            _bufferDurationInSeconds = audioOutput.BufferDuration;
//            _sampleProvider = new AudioOutputSampleProvider(_patchCalculatorContainer, audioOutput);

//            if (wasRunning)
//            {
//                Start();
//            }
//        }

//        public double Time => _sampleProvider._time;

//        /// <summary>
//        /// Initializes and then immediately pauses, to prevent calculations at startup,
//        /// but also a hick-up upon the first note.
//        /// </summary>
//        public void StartAndPause()
//        {
//            Start();
//            Pause();
//        }

//        public void Start()
//        {
//            _waveOut = CreateWaveOut(_sampleProvider, _bufferDurationInSeconds);

//            _sampleProvider._time = 0;
//            _sampleProvider._isRunning = true;

//            _waveOut.Play();
//        }

//        public void Continue() => _sampleProvider._isRunning = true;

//        public bool IsRunning => _sampleProvider._isRunning;

//        // TODO: It is unintuitive that Pause does not work as an alternative way to start the calculations.
//        public void Pause() => _sampleProvider._isRunning = false;

//        public void Stop()
//        {
//            if (_waveOut != null)
//            {
//                _waveOut.Stop();
//                _waveOut.Dispose();
//            }

//            if (_sampleProvider != null)
//            {
//                _sampleProvider._isRunning = false;
//                _sampleProvider._time = 0;
//            }
//        }

//        // Helpers

//        private WaveOut CreateWaveOut(ISampleProvider sampleProvider, double bufferDurationInSeconds)
//        {
//            var waveOut = new WaveOut();
//            waveOut.DesiredLatency = (int)(bufferDurationInSeconds * 1000.0);
//            waveOut.Init(sampleProvider);
//            return waveOut;
//        }
//    }
//}
