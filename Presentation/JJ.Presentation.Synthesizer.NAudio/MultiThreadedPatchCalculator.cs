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
    public class MultiThreadedPatchCalculator : IPatchCalculator, IDisposable
    {
        private class PatchCalculatorInfo
        {
            public PatchCalculatorInfo(IPatchCalculator patchCalculator, int noteListIndex)
            {
                if (patchCalculator == null) throw new NullException(() => patchCalculator);

                PatchCalculator = patchCalculator;
                NoteListIndex = noteListIndex;
            }

            public IPatchCalculator PatchCalculator { get; private set; }
            public int NoteListIndex { get; private set; }
            public bool IsActive { get; set; }
            public double NoteStart { get; set; }
        }

        private class ThreadInfo
        {
            public ThreadInfo(Thread thread)
            {
                if (thread == null) throw new NullException(() => thread);

                Thread = thread;
                PatchCalculatorInfos = new List<PatchCalculatorInfo>(MAX_EXPECTED_PATCH_CALCULATORS_PER_THREAD);
                Lock = new AutoResetEvent(false);
            }

            public Thread Thread { get; private set; }
            public IList<PatchCalculatorInfo> PatchCalculatorInfos { get; private set; }
            public AutoResetEvent Lock { get; private set; }
        }

        private const int MAX_EXPECTED_PATCH_CALCULATORS_PER_THREAD = 32;
        private const int DEFAULT_CHANNEL_INDEX = 0; // TODO: Make multi-channel.

        private readonly NoteRecycler _noteRecycler;
        private readonly IList<PatchCalculatorInfo> _patchCalculatorInfos;
        private readonly IList<ThreadInfo> _threadInfos;
        private readonly CountdownEvent _countdownEvent;
        private readonly double _sampleDuration;
        private readonly double[] _buffer;
        private readonly object[] _bufferLocks;

        private double _t0;
        private bool _disposing;

        public MultiThreadedPatchCalculator(
            int threadCount, int bufferSize, double sampleDuration,
            NoteRecycler noteRecycler)
        {
            if (threadCount < 0) throw new LessThanException(() => threadCount, 0);
            if (bufferSize < 0) throw new LessThanException(() => bufferSize, 0);
            if (noteRecycler == null) throw new NullException(() => noteRecycler);

            _sampleDuration = sampleDuration;
            _noteRecycler = noteRecycler;
            _patchCalculatorInfos = new List<PatchCalculatorInfo>();

            _buffer = new double[bufferSize];
            _bufferLocks = new object[bufferSize];
            for (int i = 0; i < bufferSize; i++)
            {
                _bufferLocks[i] = new object();
            }

            _threadInfos = new ThreadInfo[threadCount];
            for (int i = 0; i < threadCount; i++)
            {
                Thread thread = new Thread(CalculateSingleThread);
                thread.Priority = ThreadPriority.AboveNormal;

                var threadInfo = new ThreadInfo(thread);
                _threadInfos[i] = threadInfo;

                thread.Start(threadInfo);
            }

            _countdownEvent = new CountdownEvent(threadCount);
        }

        ~MultiThreadedPatchCalculator()
        {
            Dispose();
        }

        public void Dispose()
        {
            _disposing = true;

            // TODO: This seems a lot of hassle to let the threads stop properly, but I do not know how else to do it.
            // It still does not work, though.
            if (_threadInfos != null)
            {
                if (_countdownEvent != null)
                {
                    _countdownEvent.Reset();
                }

                for (int i = 0; i < _threadInfos.Count; i++)
                {
                    ThreadInfo threadInfo = _threadInfos[i];
                    threadInfo.Lock.Set();
                }

                if (_countdownEvent != null)
                {
                    _countdownEvent.Wait();
                }

                //for (int i = 0; i < _threadInfos.Count; i++)
                //{
                //    ThreadInfo threadInfo = _threadInfos[i];
                //    threadInfo.Lock.Dispose();
                //}
            }

            //if (_countdownEvent != null)
            //{
            //    _countdownEvent.Dispose();
            //}

            GC.SuppressFinalize(this);
        }

        // Calculate

        /// <param name="channelIndex">
        /// This parameter is currently not used, but I want this abstraction to stay similar
        /// to PatchCalculator, or I would be refactoring my brains out.
        /// </param>
        public double[] Calculate(double t0, double sampleDuration, int count, int channelIndex)
        {
            // TODO: Document that count and sampleDuration are not used.

            _t0 = t0;

            Array.Clear(_buffer, 0, _buffer.Length);

            _countdownEvent.Reset();

            for (int i = 0; i < _threadInfos.Count; i++)
            {
                ThreadInfo threadInfo = _threadInfos[i];
                threadInfo.Lock.Set();
            }

            _countdownEvent.Wait();

            return _buffer;
        }

        public double Calculate(double time, int channelIndex)
        {
            throw new NotSupportedException();
        }

        private void CalculateSingleThread(object threadInfoObject)
        {
            var threadInfo = (ThreadInfo)threadInfoObject;

            IList<PatchCalculatorInfo> patchCalculatorInfos = threadInfo.PatchCalculatorInfos;

Wait:
            threadInfo.Lock.WaitOne();

            try
            {
                if (_disposing)
                {
                    // TODO: Low priority: Not sure how to clean up the threads properly without introducing an if-statement.
                    return;
                }

                for (int i = 0; i < patchCalculatorInfos.Count; i++)
                {
                    PatchCalculatorInfo patchCalculatorInfo = patchCalculatorInfos[i];

                    // Checking this here is not ideal, but the alternative, yet another thread, is too difficult to me.
                    patchCalculatorInfo.IsActive = !_noteRecycler.IsNoteReleased(patchCalculatorInfo.NoteListIndex, _t0);
                    if (!patchCalculatorInfo.IsActive)
                    {
                        continue;
                    }

                    IPatchCalculator patchCalculator = patchCalculatorInfo.PatchCalculator;
                    double noteStart = patchCalculatorInfo.NoteStart;

                    double t = _t0 - noteStart;

                    for (int j = 0; j < _buffer.Length; j++)
                    {
                        double value = patchCalculator.Calculate(t, DEFAULT_CHANNEL_INDEX);

                        // TODO: Low priority: Not sure how to do a quicker interlocked add for doubles.
                        lock (_bufferLocks[j])
                        {
                            _buffer[j] += value;
                        }

                        t += _sampleDuration;
                    }
                }
            }
            finally
            {
                _countdownEvent.Signal();
            }

            goto Wait;
        }

        // Adding and removing calculators.

        public int AddPatchCalculator(IPatchCalculator patchCalculator)
        {
            if (patchCalculator == null) throw new NullException(() => patchCalculator);

            var patchCalculatorInfo = new PatchCalculatorInfo(patchCalculator, _patchCalculatorInfos.Count);
            patchCalculatorInfo.IsActive = true;

            _patchCalculatorInfos.Add(patchCalculatorInfo);

            ApplyToThreadInfos();

            return patchCalculatorInfo.NoteListIndex;
        }

        public void AddPatchCalculators(IList<IPatchCalculator> patchCalculators)
        {
            for (int i = 0; i < patchCalculators.Count; i++)
            {
                IPatchCalculator patchCalculator = patchCalculators[i];

                var patchCalculatorInfo = new PatchCalculatorInfo(patchCalculator, _patchCalculatorInfos.Count);
                patchCalculatorInfo.IsActive = true;

                _patchCalculatorInfos.Add(patchCalculatorInfo);
            }

            ApplyToThreadInfos();
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

            int threadIndex = 0;
            for (int activePatchCalculatorInfoIndex = 0; activePatchCalculatorInfoIndex < activePatchCalculators.Count; activePatchCalculatorInfoIndex++)
            {
                PatchCalculatorInfo activePatchCalculatorInfo = activePatchCalculators[activePatchCalculatorInfoIndex];
                ThreadInfo threadInfo = _threadInfos[threadIndex];

                threadInfo.PatchCalculatorInfos.Add(activePatchCalculatorInfo);

                threadIndex++;
                threadIndex = threadIndex % _threadInfos.Count;
            }
        }

        // Values

        public double GetValue(int noteListIndex)
        {
            throw new NotSupportedException();
        }

        public void SetValue(int noteListIndex, double value)
        {
            throw new NotSupportedException();
        }

        public double GetValue(string name)
        {
            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos.FirstOrDefault();

            if (patchCalculatorInfo != null)
            {
                return patchCalculatorInfo.PatchCalculator.GetValue(name);
            }

            return 0.0;
        }

        public void SetValue(string name, double value)
        {
            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
            {
                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
                patchCalculatorInfo.PatchCalculator.SetValue(name, value);
            }
        }

        public double GetValue(string name, int noteListIndex)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);
            return patchCalculatorInfo.PatchCalculator.GetValue(name);
        }

        public void SetValue(string name, int noteListIndex, double value)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);
            patchCalculatorInfo.PatchCalculator.SetValue(name, value);
        }

        public double GetValue(InletTypeEnum inletTypeEnum)
        {
            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos.FirstOrDefault();

            if (patchCalculatorInfo != null)
            {
                return patchCalculatorInfo.PatchCalculator.GetValue(inletTypeEnum);
            }

            return 0.0;
        }

        public void SetValue(InletTypeEnum inletTypeEnum, double value)
        {
            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
            {
                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
                patchCalculatorInfo.PatchCalculator.SetValue(inletTypeEnum, value);
            }
        }

        public double GetValue(InletTypeEnum inletTypeEnum, int noteListIndex)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);

            if (inletTypeEnum == InletTypeEnum.NoteStart)
            {
                return patchCalculatorInfo.NoteStart;
            }
            else
            {
                double value = patchCalculatorInfo.PatchCalculator.GetValue(inletTypeEnum);
                return value;
            }
        }

        public void SetValue(InletTypeEnum inletTypeEnum, int noteListIndex, double value)
        {
            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);

            if (inletTypeEnum == InletTypeEnum.NoteStart)
            {
                patchCalculatorInfo.NoteStart = value;
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

        public void ResetState()
        {
            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
            {
                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
                patchCalculatorInfo.NoteStart = 0.0;
                patchCalculatorInfo.PatchCalculator.ResetState();
            }
        }

        public void ResetState(string name)
        {
            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
            {
                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
                patchCalculatorInfo.PatchCalculator.ResetState(name);
            }
        }

        public void CloneValues(IPatchCalculator sourceCalculator)
        {
            var castedSourceCalculator = sourceCalculator as MultiThreadedPatchCalculator;
            if (castedSourceCalculator == null)
            {
                throw new IsNotTypeException<MultiThreadedPatchCalculator>(() => castedSourceCalculator);
            }

            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
            {
                PatchCalculatorInfo source = castedSourceCalculator._patchCalculatorInfos[i];
                PatchCalculatorInfo dest = _patchCalculatorInfos[i];

                dest.NoteStart = source.NoteStart;
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

            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[noteListIndex];
            return patchCalculatorInfo;
        }

        // TODO: Remove outcommented method.
        //private void SetDelay(int patchCalculatorIndex, double delay)
        //{
        //    PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(patchCalculatorIndex);

        //    patchCalculatorInfo.NoteStart = delay;
        //}

        // Source: http://stackoverflow.com/questions/1400465/why-is-there-no-overload-of-interlocked-add-that-accepts-doubles-as-parameters
        //private static double InterlockedAddDouble(ref double location1, double value)
        //{
        //    double newCurrentValue = 0;
        //    while (true)
        //    {
        //        double currentValue = newCurrentValue;
        //        double newValue = currentValue + value;
        //        newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
        //        if (newCurrentValue == currentValue)
        //            return newValue;
        //    }
        //}
    }
}
