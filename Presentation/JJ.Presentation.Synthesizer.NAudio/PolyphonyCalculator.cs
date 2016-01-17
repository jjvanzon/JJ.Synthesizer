using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using JJ.Framework.Common;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Framework.Reflection.Exceptions;
using JJ.Business.Synthesizer.Enums;

namespace JJ.Presentation.Synthesizer.NAudio
{
    public class PolyphonyCalculator
    {
        private class PatchCalculatorInfo
        {
            public IPatchCalculator PatchCalculator { get; set; }
            public bool IsActive { get; set; }
            public double Delay { get; set; }
            public int NoteListIndex { get; set; }
        }

        private class ThreadInfo
        {
            public Thread Thread { get; set; }
            public double[] Buffer { get; set; }
            public IList<PatchCalculatorInfo> PatchCalculatorInfos { get; set; }
        }

        private const int MAX_EXPECTED_PATCH_CALCULATORS_PER_THREAD = 32;
        private const int DEFAULT_CHANNEL_INDEX = 0; // TODO: Make multi-channel.

        private readonly IList<PatchCalculatorInfo> _patchCalculatorInfos;
        private readonly IList<ThreadInfo> _threadInfos;
        private readonly double _sampleDuration;
        private readonly double[] _buffer;
        private double _t0;

        public PolyphonyCalculator(int threadCount, int bufferSize, double sampleDuration)
        {
            if (threadCount < 0) throw new LessThanException(() => threadCount, 0);
            if (bufferSize < 0) throw new LessThanException(() => bufferSize, 0);

            _buffer = new double[bufferSize];
            _sampleDuration = sampleDuration;
            _patchCalculatorInfos = new List<PatchCalculatorInfo>();
            _threadInfos = new ThreadInfo[threadCount];

            for (int i = 0; i < threadCount; i++)
            {
                var threadInfo = new ThreadInfo
               {
                    //Thread = new Thread(CalculateSingleThread),
                    Buffer = new double[bufferSize],
                    PatchCalculatorInfos = new List<PatchCalculatorInfo>(MAX_EXPECTED_PATCH_CALCULATORS_PER_THREAD)
                };

                _threadInfos[i] = threadInfo;
            }
        }

        // Adding and removing calculators.

        public int AddPatchCalculator(IPatchCalculator patchCalculator, int noteListIndex)
        {
            if (patchCalculator == null) throw new NullException(() => patchCalculator);
            if (noteListIndex < 0) throw new LessThanException(() => noteListIndex, 0);

            _patchCalculatorInfos.Add(new PatchCalculatorInfo
            {
                PatchCalculator = patchCalculator,
                IsActive = true,
                NoteListIndex = noteListIndex
            });

            ApplyToThreadInfos();

            return _patchCalculatorInfos.Count - 1;
        }

        public void RemovePatchCalculator(int index)
        {
            AssertPatchCalculatorInfosListIndex(index);

            _patchCalculatorInfos.RemoveAt(index);

            ApplyToThreadInfos();
        }

        public void RemovePatchCalculator(IPatchCalculator patchCalculator)
        {
            if (patchCalculator == null) throw new NullException(() => patchCalculator);

            _patchCalculatorInfos.RemoveFirst(x => x.PatchCalculator == patchCalculator);

            ApplyToThreadInfos();
        }

        public void SetDelay(int patchCalculatorIndex, double delay)
        {
            AssertPatchCalculatorInfosListIndex(patchCalculatorIndex);

            _patchCalculatorInfos[patchCalculatorIndex].Delay = delay;
        }

        private void ApplyToThreadInfos()
        {
            // Clear threads
            for (int i = 0; i < _threadInfos.Count; i++)
            {
                ThreadInfo threadInfo = _threadInfos[i];
                threadInfo.PatchCalculatorInfos.Clear();
            }

            // Determine calculators per thread
            IList<PatchCalculatorInfo> activePatchCalculators = _patchCalculatorInfos.Where(x => x.IsActive).ToArray();
            int calculatorsPerThread = activePatchCalculators.Count / _threadInfos.Count;

            // Spread calculators over threads
            int activePatchCalculatorInfoIndex = 0;
            for (int threadIndex = 0; threadIndex < _threadInfos.Count; threadIndex++)
            {
                ThreadInfo threadInfo = _threadInfos[threadIndex];

                for (int i = 0; i < calculatorsPerThread; i++)
                {
                    PatchCalculatorInfo activePatchCalculatorInfo = activePatchCalculators[activePatchCalculatorInfoIndex];
                    threadInfo.PatchCalculatorInfos.Add(activePatchCalculatorInfo);

                    activePatchCalculatorInfoIndex++;
                }
            }

            // Excessive calculators go on the last thread.
            ThreadInfo lastThreadInfo = _threadInfos.Last();
            while (activePatchCalculatorInfoIndex < activePatchCalculators.Count)
            {
                PatchCalculatorInfo activePatchCalculatorInfo = activePatchCalculators[activePatchCalculatorInfoIndex];
                lastThreadInfo.PatchCalculatorInfos.Add(activePatchCalculatorInfo);

                activePatchCalculatorInfoIndex++;
            }
        }

        // Calculate

        /// <param name="channelIndex">
        /// Currently not used, but I want this abstraction to stay similar
        /// to PatchCalculator, or I would be refactoring my brains out.
        /// </param>
        public double[] Calculate(double t0, int channelIndex)
        {
            _t0 = t0;

            for (int i = 0; i < _threadInfos.Count; i++)
            {
                ThreadInfo threadInfo = _threadInfos[i];
                threadInfo.Thread = new Thread(CalculateSingleThread);
                threadInfo.Thread.Start(threadInfo);
            }

            for (int i = 0; i < _threadInfos.Count; i++)
            {
                ThreadInfo threadInfo = _threadInfos[i];
                threadInfo.Thread.Join();
            }
            
            // TODO: There has got to be a way of doing it without filling up yet another buffer.
            for (int bufferIndex = 0; bufferIndex < _buffer.Length; bufferIndex++)
            {
                double multiThreadValue = 0.0;

                for (int threadIndex = 0; threadIndex < _threadInfos.Count; threadIndex++)
                {
                    double singleThreadValue = _threadInfos[threadIndex].Buffer[bufferIndex];
                    multiThreadValue += singleThreadValue;
                }

                _buffer[bufferIndex] = multiThreadValue;
            }

            return _buffer;
        }

        private void CalculateSingleThread(object threadInfoObject)
        {
            ThreadInfo threadInfo = (ThreadInfo)threadInfoObject;

            IList<PatchCalculatorInfo> patchCalculatorInfos = threadInfo.PatchCalculatorInfos;

            double t = _t0;

            for (int i = 0; i < threadInfo.Buffer.Length; i++)
            {
                double value = 0.0;

                for (int j = 0; j < patchCalculatorInfos.Count; j++)
                {
                    PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[j];

                    double delay = patchCalculatorInfo.Delay;

                    double value2 = patchCalculatorInfo.PatchCalculator.Calculate(t - delay, DEFAULT_CHANNEL_INDEX);

                    value += value2;
                }

                threadInfo.Buffer[i] = value;

                t += _sampleDuration;
            }
        }

        // Values

        public void SetValue(InletTypeEnum inletTypeEnum, int noteListIndex, double value)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);

            if (inletTypeEnum == InletTypeEnum.NoteStart)
            {
                patchCalculatorInfo.Delay = value;
            }
            else
            {
                patchCalculatorInfo.PatchCalculator.SetValue(inletTypeEnum, value);
            }
        }

        public void ResetState(int noteListIndex)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);
            patchCalculatorInfo.PatchCalculator.ResetState();
        }

        public double GetValue(InletTypeEnum inletTypeEnum, int noteListIndex)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);

            if (inletTypeEnum == InletTypeEnum.NoteStart)
            {
                return patchCalculatorInfo.Delay;
            }
            else
            {
                double value = patchCalculatorInfo.PatchCalculator.GetValue(inletTypeEnum);
                return value;
            }
        }

        public void CloneValues(PolyphonyCalculator sourceCalculator)
        {
            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
            {
                PatchCalculatorInfo source = sourceCalculator._patchCalculatorInfos[i];
                PatchCalculatorInfo dest = _patchCalculatorInfos[i];

                dest.Delay = source.Delay;
                dest.PatchCalculator.CloneValues(source.PatchCalculator);
            }
        }

        // Helpers

        private void AssertPatchCalculatorInfosListIndex(int patchCalcultorInfosListIndex)
        {
            if (patchCalcultorInfosListIndex < 0) throw new LessThanException(() => patchCalcultorInfosListIndex, 0);
            if (patchCalcultorInfosListIndex >= _patchCalculatorInfos.Count) throw new GreaterThanOrEqualException(() => patchCalcultorInfosListIndex, () => _patchCalculatorInfos.Count);
        }

        private PatchCalculatorInfo GetPatchCalculatorInfo(int noteListIndex)
        {
            AssertPatchCalculatorInfosListIndex(noteListIndex);

            //PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos.Where(x => x.NoteListIndex == noteListIndex)
            //                                                               .SingleOrDefault();

            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[noteListIndex];
            //if (patchCalculatorInfo == null)
            //{
            //    throw new Exception(String.Format("No PatchCalculator exits info for noteListIndex '{0}'.", noteListIndex));
            //}

            return patchCalculatorInfo;
        }

        //public IPatchCalculator GetPatchCalculator(int noteListIndex)
        //{
        //    AssertPatchCalculatorInfosListIndex(noteListIndex);

        //    return _patchCalculatorInfos[noteListIndex].PatchCalculator;
        //}
    }
}
