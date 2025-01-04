using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_IO_Wishes.FileWishes;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Text_Wishes.StringWishes;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Wishes
{
    [DebuggerDisplay("{DebuggerDisplay}")]
    public partial class SynthWishes
    {
        string DebuggerDisplay => GetDebuggerDisplay(this);

        private readonly OperatorFactory _operatorFactory;
        private readonly CurveFactory _curveFactory;
        private readonly SampleManager _sampleManager;
        private readonly TapeCollection _tapes;
        internal readonly TapeRunner _tapeRunner;

        private bool _isRunning;

        public IContext Context { get; }

        private ConfigWishes _config;
        public ConfigWishes Config
        {
            get => _config;
            set => _config = value ?? throw new ArgumentException(nameof(Config));
        }

        /// <inheritdoc cref="docs._captureindexer" />
        public SynthWishes _ { get; }

        /// <summary>
        /// Crazy conversion operator, reintroducing the discard notation _
        /// sort of, for FlowNodes.
        /// </summary>
        // ReSharper disable once UnusedParameter.Global
        public static implicit operator FlowNode(SynthWishes synthWishes) => null;

        public SynthWishes()
            : this(ServiceFactory.CreateContext())
        { }
        
        public SynthWishes(IContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));

            _operatorFactory = ServiceFactory.CreateOperatorFactory(context);
            _curveFactory = ServiceFactory.CreateCurveFactory(context);
            _sampleManager = ServiceFactory.CreateSampleManager(context);
            
            Config = new ConfigWishes();
            _tapes = new TapeCollection(this);
            _tapeRunner = new TapeRunner(this, _tapes);
            
            _ = this;
            bar = new BarIndexer(this);
            bars = new BarsIndexer(this);
            beat = new BeatIndexer(this);
            b = new BeatIndexer(this);
            beats = new BeatsIndexer(this);
            l = new BeatsIndexer(this);
            len = new BeatsIndexer(this);
            length = new BeatsIndexer(this);
            t = new TimeIndexer(this);
        }

        public int TapeCount => _tapes.Count;

        public SynthWishes Run(Action action) => RunOnNew(action);
        
        internal SynthWishes RunOnNew(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            // Create a new instance of the derived class
            Type concreteType = this.GetType();
            var newInstance = (SynthWishes)Activator.CreateInstance(concreteType);
            
            // Yield over settings
            newInstance.Config = Config;

            // Try run on new instance
            MethodInfo methodInfo = action.Method;
            try
            {
                // Rebind the delegate to the new instance
                var newAction = (Action)Delegate.CreateDelegate(typeof(Action), newInstance, methodInfo);
            
                // Run the action on the new instance
                return newInstance.RunOnThis(() => newAction());
            }
            catch (ArgumentException)
            {
                // Work-around for exception:
                // "Cannot bind to the target method because its signature or security transparency
                // is not compatible with that of the delegate type."
                // Honestly I don't know how to solve other than catch the exception.
                Log(
                    $"{PrettyTime()} [RUN] Warning: " +
                    $"Could not start a new SynthWishes for {concreteType.Name}. " +
                    $"Reusing already running SynthWishes.");
                
                return RunOnThis(action);
            }            
        }
        
        public SynthWishes RunOnThis(Action action)
        {
            RunChannelSignals(action);

            LogConfig(this);
            
            if (GetParallelProcessing)
            {
                _tapeRunner.RunAllTapes();
            }
            else
            {
                throw new Exception("Run method cannot work without ParallelProcessing.");
            }
            
            return this;
        }
        
        
        internal IList<FlowNode> GetChannelSignals(Func<FlowNode> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            AssertNotRunning();
            
            var originalChannel = GetChannel;
            try
            {
                _isRunning = true;
                
                LogMathBoostTitle(GetMathBoost);
                
                WithChannel(0);
                var channel0Signal = func();
                
                if (IsMono)
                {
                    return new[] { channel0Signal };
                }
                if (IsStereo)
                {
                    WithRight(); var channel1Signal = func();
                    return new[] { channel0Signal, channel1Signal };
                }
                
                throw new ValueNotSupportedException(GetChannels);
            }
            finally
            {
                _isRunning = false;
                WithChannel(originalChannel);
                
                LogMathBoostDone(GetMathBoost);
            }
        }
        
        private void RunChannelSignals(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            AssertNotRunning();

            var originalChannel = GetChannel;
            try
            {
                _isRunning = true;
                
                LogMathBoostTitle(GetMathBoost);
            
                WithChannel(0);
                action();
                
                if (IsStereo)
                {
                    WithRight(); action();
                }
            }
            finally
            {
                _isRunning = false;
                WithChannel(originalChannel);
                
                LogMathBoostDone(GetMathBoost);
            }
        }
        
        private void AssertNotRunning()
        {
            if (_isRunning)
            {
                throw new Exception(
                    "Process already running. Nested usage, such as " +
                    "`Run(() => Save(() => ...))`, is not supported due to potential performance penalties. " +
                    "Use alternatives like `Run(() => ...).Save()` or `Save(() => ...)` to avoid nesting, " +
                    "or try `Run(() => mySound.Save())` for deferred audio streaming.");
            }
        }

        // Helpers

        private string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string filePath = SanitizeFilePath(name);
            string fileNameWithoutExtension = GetFileNameWithoutExtension(filePath, GetFileExtensionMaxLength);
            string fileExtension = audioFileFormatEnum.FileExtension();
            return fileNameWithoutExtension + fileExtension;
        }
    }
}