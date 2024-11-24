using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;
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

        internal readonly OperatorFactory _operatorFactory;
        private readonly CurveFactory _curveFactory;
        private readonly SampleManager _sampleManager;
        private readonly ConfigResolver _configResolver;

        public SynthWishes(IContext context)
        {
            _ = new CaptureIndexer(this);
            
            Context = context ?? ServiceFactory.CreateContext();

            _operatorFactory = ServiceFactory.CreateOperatorFactory(context);
            _curveFactory = ServiceFactory.CreateCurveFactory(context);
            _sampleManager = ServiceFactory.CreateSampleManager(context);
            _configResolver = new ConfigResolver();
        }

        public SynthWishes(IContext context, double beat = 1, double bar = 4)
            : this(context)
        {
            InitializeTimeIndexers(beat, bar);
        }
        
        public SynthWishes(double beat = 1, double bar = 4)
            : this(null, beat, bar)
        {
            InitializeTimeIndexers(beat, bar);
        }
        
        // Helpers

        private static string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string fileName = Path.GetFileNameWithoutExtension(name);
            string fileExtension = audioFileFormatEnum.GetFileExtension();
            fileName += fileExtension;
            return fileName;
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