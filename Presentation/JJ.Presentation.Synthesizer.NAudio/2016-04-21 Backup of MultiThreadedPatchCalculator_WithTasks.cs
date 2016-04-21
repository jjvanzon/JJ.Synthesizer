//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading;
//using JJ.Framework.Common;
//using JJ.Business.Synthesizer.Calculation.Patches;
//using JJ.Framework.Reflection.Exceptions;
//using JJ.Business.Synthesizer.Enums;
//using JJ.Business.Synthesizer.Calculation;
//using System.Threading.Tasks;
//using JJ.Data.Synthesizer;
//using JJ.Business.Synthesizer.Extensions;

//namespace JJ.Presentation.Synthesizer.NAudio
//{
//    public class MultiThreadedPatchCalculator_WithTasks : IPatchCalculator, IDisposable
//    {
//        private class TaskInfo
//        {
//            public TaskInfo(int noteListIndex, int channelIndex)
//            {
//                NoteListIndex = noteListIndex;
//                ChannelIndex = channelIndex;
//                Lock = new AutoResetEvent(false);
//            }

//            public int NoteListIndex { get; private set; }
//            public int ChannelIndex { get; private set; }
//            public AutoResetEvent Lock { get; private set; }

//            public IPatchCalculator PatchCalculator { get; set; }
//            public Task Task { get; set; }
//            public bool IsActive { get; set; }
//        }

//        private const int TIME_DIMENSION_INDEX = (int)DimensionEnum.Time;
//        private const int CHANNEL_DIMENSION_INDEX = (int)DimensionEnum.Channel;

//        private readonly NoteRecycler _noteRecycler;
//        private readonly CountdownEvent _countdownEvent;

//        /// <summary> First index is channel, second index is frame. </summary>
//        private double[][] _buffers;
//        /// <summary> First index is channel, second index is frame. </summary>
//        private object[][] _bufferLocks;
//        /// <summary> First index is NoteListIndex, second index is channel. </summary>
//        private TaskInfo[][] _taskInfos;
//        private double _sampleDuration;
//        private int _channelCount;
//        private int _maxConcurrentNotes;
//        private double _t0;
//        private bool _isDisposing;

//        public void SetAudioOutput(AudioOutput audioOutput)
//        {
//            if (audioOutput == null) throw new NullException(() => audioOutput);

//            double sampleDuration = audioOutput.GetSampleDuration();
//            int channelCount = audioOutput.GetChannelCount();
//            int maxConcurrentNotes = audioOutput.MaxConcurrentNotes;

//            int bufferSize = 0; // TODO: Get from AudioOutput.
//            if (bufferSize < 0) throw new LessThanException(() => bufferSize, 0);

//            double[][] buffers = new double[channelCount][];
//            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
//            {
//                buffers[channelIndex] = new double[bufferSize];
//            }
            
//            object[][] bufferLocks = new object[channelCount][];
//            for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
//            {
//                bufferLocks[channelIndex] = new object[bufferSize];

//                for (int frameIndex = 0; frameIndex < bufferSize; frameIndex++)
//                {
//                    bufferLocks[channelIndex][frameIndex] = new object();
//                }
//            }
            
//            TaskInfo[][] taskInfos = new TaskInfo[maxConcurrentNotes][];
//            for (int noteListIndex = 0; noteListIndex < maxConcurrentNotes; noteListIndex++)
//            {
//                taskInfos[noteListIndex] = new TaskInfo[channelCount];

//                for (int channelIndex = 0; channelIndex < channelCount; channelIndex++)
//                {
//                    taskInfos[noteListIndex][channelIndex] = new TaskInfo(noteListIndex, channelIndex);
//                    // TODO: Not sure how to pass data to a task.
//                    // TODO: Not sure hot to set ThreadPriority.AboveNormal.
//                    // Also not sure if the task should be created beforehand,
//                    // instead of on-the-fly.
//                    //Task task = new Task(CalculateSingleThread);
//                }
//            }

//            _sampleDuration = sampleDuration;
//            _channelCount = channelCount;
//            _maxConcurrentNotes = maxConcurrentNotes;
//            _buffers = buffers;
//            _bufferLocks = bufferLocks;
//            _taskInfos = taskInfos;
//        }

//        public MultiThreadedPatchCalculator_WithTasks(
//            AudioOutput audioOutput,
//            NoteRecycler noteRecycler)
//        {
//            if (noteRecycler == null) throw new NullException(() => noteRecycler);
            
//            _noteRecycler = noteRecycler;
//            _countdownEvent = new CountdownEvent(_taskInfos.Count);

//            SetAudioOutput(audioOutput);
//        }

//        ~MultiThreadedPatchCalculator_WithTasks()
//        {
//            Dispose();
//        }

//        public void Dispose()
//        {
//            _isDisposing = true;

//            if (_taskInfos != null)
//            {
//                if (_countdownEvent != null)
//                {
//                    _countdownEvent.Reset();
//                }

//                for (int noteIndex = 0; noteIndex < _taskInfos.Length; noteIndex++)
//                {
//                    TaskInfo[] taskInfos2 = _taskInfos[noteIndex];
//                    for (int channelIndex = 0; channelIndex < taskInfos2.Length; channelIndex++)
//                    {
//                        TaskInfo taskInfo = taskInfos2[channelIndex];
//                        taskInfo.Lock.Set();
//                    }
//                }

//                if (_countdownEvent != null)
//                {
//                    _countdownEvent.Wait();
//                }

//                for (int noteIndex = 0; noteIndex < _taskInfos.Length; noteIndex++)
//                {
//                    TaskInfo[] taskInfos2 = _taskInfos[noteIndex];
//                    for (int channelIndex = 0; channelIndex < taskInfos2.Length; channelIndex++)
//                    {
//                        TaskInfo taskInfo = taskInfos2[channelIndex];
//                        taskInfo.Lock.Dispose();
//                    }
//                }

//            }

//            if (_countdownEvent != null)
//            {
//                _countdownEvent.Dispose();
//            }

//            GC.SuppressFinalize(this);
//        }

//        // Calculate

//        /// <param name="sampleDuration">
//        /// Not used. Alternative value is determined internally.
//        /// This parameter is currently not used, but I want this abstraction to stay similar
//        /// to PatchCalculator, or I would be refactoring my brains out.
//        /// </param>
//        /// <param name="count">
//        /// Not used. Alternative value is determined internally.
//        /// This parameter is currently not used, but I want this abstraction to stay similar
//        /// to PatchCalculator, or I would be refactoring my brains out.
//        /// </param>
//        /// <param name="dimensionStack">
//        /// Not used. Alternative value is determined internally.
//        /// This parameter is currently not used, but I want this abstraction to stay similar
//        /// to PatchCalculator, or I would be refactoring my brains out.
//        /// </param>
//        public double[] Calculate(double t0, double sampleDuration, int count, DimensionStack dimensionStack)
//        {
//            _t0 = t0;

//            Array.Clear(_buffers, 0, _buffers.Length);

//            _countdownEvent.Reset();

//            for (int i = 0; i < _taskInfos.Length; i++)
//            {
//                ThreadInfo threadInfo = _threadInfos[i];
//                threadInfo.Lock.Set();
//            }

//            _countdownEvent.Wait();

//            return _buffers;
//        }

//        public double Calculate(DimensionStack dimensionStack)
//        {
//            throw new NotSupportedException();
//        }

//        private void CalculateSingleThread(object threadInfoObject)
//        {
//            var threadInfo = (ThreadInfo)threadInfoObject;

//            IList<PatchCalculatorInfo> patchCalculatorInfos = threadInfo.PatchCalculatorInfos;

//Wait:
//            threadInfo.Lock.WaitOne();

//            try
//            {
//                if (_isDisposing)
//                {
//                    // TODO: Low priority: Not sure how to clean up the threads properly without introducing an if-statement.
//                    return;
//                }

//                for (int i = 0; i < patchCalculatorInfos.Count; i++)
//                {
//                    PatchCalculatorInfo patchCalculatorInfo = patchCalculatorInfos[i];

//                    // Checking this here is not ideal, but the alternative, yet another thread, is too difficult to me.
//                    patchCalculatorInfo.IsActive = !_noteRecycler.IsNoteReleased(patchCalculatorInfo.NoteListIndex, _t0);
//                    if (!patchCalculatorInfo.IsActive)
//                    {
//                        continue;
//                    }

//                    IPatchCalculator patchCalculator = patchCalculatorInfo.PatchCalculator;

//                    var dimensionStack = new DimensionStack();

//                    double t = _t0;

//                    for (int j = 0; j < _buffers.Length; j++)
//                    {
//                        dimensionStack.Set(TIME_DIMENSION_INDEX, t);

//                        double value = patchCalculator.Calculate(dimensionStack);

//                        // TODO: Low priority: Not sure how to do a quicker interlocked add for doubles.
//                        lock (_bufferLocks[j])
//                        {
//                            _buffers[j] += value;
//                        }

//                        t += _sampleDuration;
//                    }
//                }
//            }
//            finally
//            {
//                _countdownEvent.Signal();
//            }

//            goto Wait;
//        }

//        // Adding and removing calculators.

//        public int AddPatchCalculator(IPatchCalculator patchCalculator)
//        {
//            if (patchCalculator == null) throw new NullException(() => patchCalculator);

//            var patchCalculatorInfo = new PatchCalculatorInfo(patchCalculator, _patchCalculatorInfos.Count);
//            patchCalculatorInfo.IsActive = true;

//            _patchCalculatorInfos.Add(patchCalculatorInfo);

//            ApplyToThreadInfos();

//            return patchCalculatorInfo.NoteListIndex;
//        }

//        public void AddPatchCalculators(IList<IPatchCalculator> patchCalculators)
//        {
//            for (int i = 0; i < patchCalculators.Count; i++)
//            {
//                IPatchCalculator patchCalculator = patchCalculators[i];

//                var patchCalculatorInfo = new PatchCalculatorInfo(patchCalculator, _patchCalculatorInfos.Count);
//                patchCalculatorInfo.IsActive = true;

//                _patchCalculatorInfos.Add(patchCalculatorInfo);
//            }

//            ApplyToThreadInfos();
//        }

//        public void RemovePatchCalculator(int index)
//        {
//            AssertPatchCalculatorInfosListIndex(index);

//            _patchCalculatorInfos.RemoveAt(index);

//            ApplyToThreadInfos();
//        }

//        public void RemovePatchCalculator(IPatchCalculator patchCalculator)
//        {
//            if (patchCalculator == null) throw new NullException(() => patchCalculator);

//            _patchCalculatorInfos.RemoveFirst(x => x.PatchCalculator == patchCalculator);

//            ApplyToThreadInfos();
//        }

//        private void ApplyToThreadInfos()
//        {
//            // Clear threads
//            for (int i = 0; i < _threadInfos.Count; i++)
//            {
//                ThreadInfo threadInfo = _threadInfos[i];
//                threadInfo.PatchCalculatorInfos.Clear();
//            }

//            // Determine calculators per thread
//            IList<PatchCalculatorInfo> activePatchCalculators = _patchCalculatorInfos.Where(x => x.IsActive).ToArray();

//            int threadIndex = 0;
//            for (int activePatchCalculatorInfoIndex = 0; activePatchCalculatorInfoIndex < activePatchCalculators.Count; activePatchCalculatorInfoIndex++)
//            {
//                PatchCalculatorInfo activePatchCalculatorInfo = activePatchCalculators[activePatchCalculatorInfoIndex];
//                ThreadInfo threadInfo = _threadInfos[threadIndex];

//                threadInfo.PatchCalculatorInfos.Add(activePatchCalculatorInfo);

//                threadIndex++;
//                threadIndex = threadIndex % _threadInfos.Count;
//            }
//        }

//        // Values

//        public double GetValue(int noteListIndex)
//        {
//            throw new NotSupportedException();
//        }

//        public void SetValue(int noteListIndex, double value)
//        {
//            throw new NotSupportedException();
//        }

//        public double GetValue(string name)
//        {
//            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos.FirstOrDefault();

//            if (patchCalculatorInfo != null)
//            {
//                return patchCalculatorInfo.PatchCalculator.GetValue(name);
//            }

//            return 0.0;
//        }

//        public void SetValue(string name, double value)
//        {
//            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
//            {
//                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
//                patchCalculatorInfo.PatchCalculator.SetValue(name, value);
//            }
//        }

//        public double GetValue(string name, int noteListIndex)
//        {
//            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);
//            return patchCalculatorInfo.PatchCalculator.GetValue(name);
//        }

//        public void SetValue(string name, int noteListIndex, double value)
//        {
//            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);
//            patchCalculatorInfo.PatchCalculator.SetValue(name, value);
//        }

//        public double GetValue(DimensionEnum dimensionEnum)
//        {
//            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos.FirstOrDefault();

//            if (patchCalculatorInfo != null)
//            {
//                return patchCalculatorInfo.PatchCalculator.GetValue(dimensionEnum);
//            }

//            return 0.0;
//        }

//        public void SetValue(DimensionEnum dimensionEnum, double value)
//        {
//            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
//            {
//                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
//                patchCalculatorInfo.PatchCalculator.SetValue(dimensionEnum, value);
//            }
//        }

//        public double GetValue(DimensionEnum dimensionEnum, int noteListIndex)
//        {
//            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);

//            double value = patchCalculatorInfo.PatchCalculator.GetValue(dimensionEnum);
//            return value;
//        }

//        public void SetValue(DimensionEnum dimensionEnum, int noteListIndex, double value)
//        {
//            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);

//            patchCalculatorInfo.PatchCalculator.SetValue(dimensionEnum, value);
//        }

//        public void Reset(DimensionStack dimensionStack, int noteListIndex)
//        {
//            PatchCalculatorInfo patchCalculatorInfo = GetPatchCalculatorInfo(noteListIndex);
//            patchCalculatorInfo.PatchCalculator.Reset(dimensionStack);
//        }

//        public void Reset(DimensionStack dimensionStack)
//        {
//            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
//            {
//                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
//                patchCalculatorInfo.PatchCalculator.Reset(dimensionStack);
//            }
//        }

//        public void Reset(DimensionStack dimensionStack, string name)
//        {
//            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
//            {
//                PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[i];
//                patchCalculatorInfo.PatchCalculator.Reset(dimensionStack, name);
//            }
//        }

//        public void CloneValues(IPatchCalculator sourceCalculator)
//        {
//            var castedSourceCalculator = sourceCalculator as MultiThreadedPatchCalculator;
//            if (castedSourceCalculator == null)
//            {
//                throw new IsNotTypeException<MultiThreadedPatchCalculator>(() => castedSourceCalculator);
//            }

//            for (int i = 0; i < _patchCalculatorInfos.Count; i++)
//            {
//                PatchCalculatorInfo source = castedSourceCalculator._patchCalculatorInfos[i];
//                PatchCalculatorInfo dest = _patchCalculatorInfos[i];

//                dest.PatchCalculator.CloneValues(source.PatchCalculator);
//            }
//        }

//        // Helpers

//        private void AssertPatchCalculatorInfosListIndex(int patchCalcultorInfosListIndex)
//        {
//            if (patchCalcultorInfosListIndex < 0) throw new LessThanException(() => patchCalcultorInfosListIndex, 0);
//            if (patchCalcultorInfosListIndex >= _patchCalculatorInfos.Count) throw new GreaterThanOrEqualException(() => patchCalcultorInfosListIndex, () => _patchCalculatorInfos.Count);
//        }

//        private PatchCalculatorInfo GetPatchCalculatorInfo(int noteListIndex)
//        {
//            AssertPatchCalculatorInfosListIndex(noteListIndex);

//            PatchCalculatorInfo patchCalculatorInfo = _patchCalculatorInfos[noteListIndex];
//            return patchCalculatorInfo;
//        }

//        // Source: http://stackoverflow.com/questions/1400465/why-is-there-no-overload-of-interlocked-add-that-accepts-doubles-as-parameters
//        //private static double InterlockedAddDouble(ref double location1, double value)
//        //{
//        //    double newCurrentValue = 0;
//        //    while (true)
//        //    {
//        //        double currentValue = newCurrentValue;
//        //        double newValue = currentValue + value;
//        //        newCurrentValue = Interlocked.CompareExchange(ref location1, newValue, currentValue);
//        //        if (newCurrentValue == currentValue)
//        //            return newValue;
//        //    }
//        //}
//    }
//}
