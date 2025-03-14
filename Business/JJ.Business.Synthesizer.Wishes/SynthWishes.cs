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
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.NoteWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Business.Synthesizer.Wishes.Logging;
using static JJ.Business.Synthesizer.Wishes.Helpers.DebuggerDisplayFormatter;
using static JJ.Framework.Core.IO.FileWishes;
using static JJ.Framework.Core.Text.StringWishes;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public override string ToString() => GetDebuggerDisplay(this);

        private readonly OperatorFactory _operatorFactory;
        private readonly CurveFactory _curveFactory;
        private readonly SampleManager _sampleManager;
        private readonly TapeCollection _tapes;
        internal readonly TapeRunner _tapeRunner;
        private ConfigResolver _config;
        private bool _isRunning;

        public IContext Context { get; }

        /// <inheritdoc cref="_captureindexer" />
        public SynthWishes _ { get; }

        /// <summary>
        /// Crazy conversion operator, reintroducing the discard notation _
        /// sort of, for FlowNodes.
        /// </summary>
        // ReSharper disable once UnusedParameter.Global
        public static implicit operator FlowNode(SynthWishes synthWishes) => null;

        public SynthWishes()
            : this(default)
        { }
        
        public SynthWishes(IContext context)
        {
            Context = context ?? ServiceFactory.CreateContext();

            _operatorFactory = ServiceFactory.CreateOperatorFactory(context);
            _curveFactory = ServiceFactory.CreateCurveFactory(context);
            _sampleManager = ServiceFactory.CreateSampleManager(context);
            
            _config = new ConfigResolver();
            Logging = new LogWishes(_config);
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
            newInstance._config = _config.Clone();

            // Try run on new instance
            MethodInfo methodInfo = action.Method;
            try
            {
                // Rebind the delegate to the new instance
                var newAction = (Action)Delegate.CreateDelegate(typeof(Action), newInstance, methodInfo);
            
                // Run the action on the new instance
                return newInstance.RunOnThisOne(() => newAction());
            }
            catch (ArgumentException)
            {
                // Work-around for exception:
                // "Cannot bind to the target method because its signature or security transparency
                // is not compatible with that of the delegate type."
                // Honestly I don't know how to solve other than catch the exception.
                Log("Run", $"Warning: Could not start a new SynthWishes for {concreteType.Name}. " +
                           $"Reusing already running SynthWishes.");
                
                return RunOnThisOne(action);
            }            
        }
        
        public SynthWishes RunOnThisOne(Action action)
        {
            RunChannelSignals(action);

            LogConfig();
            
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