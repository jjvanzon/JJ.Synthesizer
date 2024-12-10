using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;

// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable AssignmentInsteadOfDiscard

namespace JJ.Business.Synthesizer.Wishes
{
    /// <inheritdoc cref="docs._captureindexer" />
    public partial class CaptureIndexer
    {
        private readonly SynthWishes _synthWishes;
        
        /// <inheritdoc cref="docs._captureindexer" />
        internal CaptureIndexer(SynthWishes synthWishes) 
            => _synthWishes = synthWishes;
    }
    
    public partial class SynthWishes
    {
        /// <inheritdoc cref="docs._captureindexer" />
        public readonly CaptureIndexer _;
        
        public IContext Context { get; }

        private ConfigWishes _config;
        public ConfigWishes Config
        {
            get => _config;
            set => _config = value ?? throw new ArgumentException(nameof(Config));
        }

        private readonly OperatorFactory _operatorFactory;
        private readonly CurveFactory _curveFactory;
        private readonly SampleManager _sampleManager;
        private readonly TapeCollection _tapes;
        private readonly TapeRunner _tapeRunner;

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
            
            _ = new CaptureIndexer(this);
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
        
        public void Run(Action action) => RunOnNewInstance(action);
        
        private void RunOnNewInstance(Action action)
        {
            // Create a new instance of the derived class
            Type concreteType = this.GetType();
            var newInstance = (SynthWishes)Activator.CreateInstance(concreteType);
            
            // Yield over settings
            newInstance.Config = Config;
            
            // Rebind the delegate to the new instance
            MethodInfo methodInfo = action.Method;
            var newAction = (Action)Delegate.CreateDelegate(typeof(Action), newInstance, methodInfo);
            
            // Run the action on the new instance
            newInstance.RunOnThisInstance(() => newAction());
        }
        
        private void RunOnThisInstance(Action action)
        {
            RunForChannels(action);
            
            if (GetParallelTaping)
            {
                _tapeRunner.RunAllTapes();
            }
            else
            {
                throw new Exception("Run method cannot work without ParallelTaping.");
            }
        }
        
        internal IList<FlowNode> GetChannelSignals(Func<FlowNode> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            LogMathOptimizationTitle();
            
            var originalChannel = GetChannel;
            try
            {
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
                WithChannel(originalChannel);
            }
        }

        private void RunForChannels(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            LogMathOptimizationTitle();
            
            var originalChannel = GetChannel;
            try
            {
                WithChannel(0);
                action();
                
                if (IsStereo)
                {
                    WithRight(); action();
                }
            }
            finally
            {
                WithChannel(originalChannel);
            }
        }

        // Helpers

        private static string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string filePath = SanitizeFilePath(name);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string fileExtension = audioFileFormatEnum.GetFileExtension();
            return fileNameWithoutExtension + fileExtension;
        }
    }
}