using System;
using System.Collections.Generic;
using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Framework.Persistence;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_IO_Wishes;
using static JJ.Business.Synthesizer.Enums.SpeakerSetupEnum;
using System.Runtime.CompilerServices;

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
        
        public void Run(Func<FlowNode> func, [CallerMemberName] string callerMemberName = null)
        {
            var channels = GetChannelSignals(func);
            
            // To make call in constructor work whether Flow() is implemented or not.
            //if (channels.All(x => x == null))
            //{
            //    return;
            //}

            if (GetParallelTaping)
            {
                _tapeRunner.RunAllTapes(channels);
            }
            else
            {
                Cache(func, callerMemberName);
            }
        }
                
        private IList<FlowNode> GetChannelSignals(Func<FlowNode> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            
            var originalChannel = GetChannel;
            try
            {
                switch (GetSpeakers)
                {
                    case 1:
                        WithCenter(); return new[] { func() };
                    
                    case 2:
                        WithLeft(); var leftSignal = func();
                        WithRight(); var rightSignal = func();
                        return new[] { leftSignal, rightSignal };

                    default: 
                        throw new ValueNotSupportedException(GetSpeakers);
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
        
        internal static string FormatMetrics(double audioDuration, double calculationDuration, int complexity)
        {
            string realTimeMessage = FormatRealTimeMessage(audioDuration, calculationDuration);
            string sep = realTimeMessage != default ? " | " : "";
            string complexityMessage = $"Complexity Ｏ ( {complexity} )";
            string metricsMessage = $"{realTimeMessage}{sep}{complexityMessage}";
            return metricsMessage;
        }
        
        private static string FormatRealTimeMessage(double audioDuration, double calculationDuration)
        {
            //var isRunningInTooling = ToolingHelper.IsRunningInTooling;
            //if (isRunningInTooling)
            //{
            //    // If running in tooling, omitting the performance message from the result,
            //    // because it has little meaning with sampling rates  below 150
            //    // that are employed for tooling by default, to keep them running fast.
            //    return default;
            //}

            double realTimePercent = audioDuration / calculationDuration* 100;

            string realTimeStatusGlyph;
            if (realTimePercent < 100)
            {
                realTimeStatusGlyph = "❌";
            }
            else
            {
                realTimeStatusGlyph = "✔";
            }

            var realTimeMessage = $"{realTimeStatusGlyph} {realTimePercent:F0} % Real Time";

            return realTimeMessage;

        }
    }
}