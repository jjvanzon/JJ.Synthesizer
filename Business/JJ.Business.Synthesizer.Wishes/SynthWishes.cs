using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;
using System.Runtime.CompilerServices;
using System.Reflection;

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
    
    public partial class FlowNode
    {
        /// <inheritdoc cref="docs._captureindexer" />
        public CaptureIndexer _ => _synthWishes._;
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
            set
            {
                if (value == null) throw new ArgumentException(nameof(Config));
                _config = value;
            }
        }

        private readonly OperatorFactory _operatorFactory;
        private readonly CurveFactory _curveFactory;
        private readonly SampleManager _sampleManager;
        private readonly TapeCollection _tapes;
        private readonly TapeRunner _tapeRunner;
        
        public SynthWishes(IContext context = null)
        {
            Context = context ?? ServiceFactory.CreateContext();

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
        
        public void Run(Action action)
        {
            // Create a new instance of the derived class
            Type concreteType = this.GetType();
            var newInstance = (SynthWishes)Activator.CreateInstance(concreteType);
            
            // Yield over settings
            newInstance.Config = Config;

            // Rebind the delegate to the new instance
            MethodInfo methodInfo = action.Method;
            Action newAction = (Action)Delegate.CreateDelegate(typeof(Action), newInstance, methodInfo);

            // Run the action on the new instance
            newInstance.RunActionOnThisInstance(() => newAction(), newAction.Method.Name);
        }

        private void RunActionOnThisInstance(Action action, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            RunFuncOnThisInstance(
                () =>
                {
                    action();

                    // HACK: To avoid the FlowNode return value,
                    // and make tape runner run all tapes without an explicit root node specified,
                    // create a root node here after all.
                    FlowNode[] tapeSignals = _tapes.GetAll().Where(x => x.Channel == GetChannel).Select(x => x.Signal).ToArray();
                    FlowNode root = Add(tapeSignals).Tape().SetName("Root");
                    return root;
                },
                name, callerMemberName);
        }

        private void RunFuncOnThisInstance(Func<FlowNode> func, string name = null, [CallerMemberName] string callerMemberName = null)
        {
            var channelSignals = GetChannelSignals(func);

            if (GetParallelTaping)
            {
                _tapeRunner.RunAllTapes(channelSignals);
            }
            else
            {
                Cache(func, name, callerMemberName);
            }
        }
                
        private IList<FlowNode> GetChannelSignals(Func<FlowNode> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            var originalChannel = GetChannel;
            try
            {
                // Evaluate the left/mono signal
                WithChannel(0);
                var channel0Signal = func();
                
                // Determine channel configuration AFTER evaluating func
                if (IsMono)
                {
                    return new[] { channel0Signal };
                }
                if (IsStereo)
                {
                    // Only evaluate the second channel if Stereo
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