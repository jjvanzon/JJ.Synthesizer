//using System;
//using System.Collections.Generic;
//using System.Linq;
//using NAudio.Wave;

//namespace JJ.Presentation.Synthesizer.NAudio
//{
//    class TempNAudioCodeExample : IWaveProvider
//    {
//        private int bytesPerSample;
//        private IEnumerable<object> inputs;
//        private int outputChannelCount;

//        public WaveFormat WaveFormat
//        {
//            get { throw new NotImplementedException(); }
//        }

//        public int Read(byte[] buffer, int offset, int count)
//        {
//            int sampleFramesRequested = count / (bytesPerSample * outputChannelCount);
//            int inputOffset = 0;
//            int sampleFramesRead = 0;
//            // now we must read from all inputs, even if we don't need their data, so they stay in sync
//            foreach (var input in inputs)
//            {
//                int bytesRequired = sampleFramesRequested * bytesPerSample * input.WaveFormat.Channels;
//                byte[] inputBuffer = new byte[bytesRequired];
//                int bytesRead = input.Read(inputBuffer, 0, bytesRequired);
//                sampleFramesRead = Math.Max(sampleFramesRead, bytesRead / (bytesPerSample * input.WaveFormat.Channels));

//                for (int n = 0; n < input.WaveFormat.Channels; n++)
//                {
//                    int inputIndex = inputOffset + n;
//                    for (int outputIndex = 0; outputIndex < outputChannelCount; outputIndex++)
//                    {
//                        if (mappings[outputIndex] == inputIndex)
//                        {
//                            int inputBufferOffset = n * bytesPerSample;
//                            int outputBufferOffset = offset + outputIndex * bytesPerSample;
//                            int sample = 0;
//                            while (sample < sampleFramesRequested && inputBufferOffset < bytesRead)
//                            {
//                                Array.Copy(inputBuffer, inputBufferOffset, buffer, outputBufferOffset, bytesPerSample);
//                                outputBufferOffset += bytesPerSample * outputChannelCount;
//                                inputBufferOffset += bytesPerSample * input.WaveFormat.Channels;
//                                sample++;
//                            }
//                            // clear the end
//                            while (sample < sampleFramesRequested)
//                            {
//                                Array.Clear(buffer, outputBufferOffset, bytesPerSample);
//                                outputBufferOffset += bytesPerSample * outputChannelCount;
//                                sample++;
//                            }
//                        }
//                    }
//                }
//                inputOffset += input.WaveFormat.Channels;
//            }

//            return sampleFramesRead * bytesPerSample * outputChannelCount;
//        }
//    }
//}
