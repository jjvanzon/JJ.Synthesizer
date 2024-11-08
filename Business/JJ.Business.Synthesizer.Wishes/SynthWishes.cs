using System;
using System.IO;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Managers;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Persistence;

namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public IContext Context { get; }

        private readonly OperatorFactory _operatorFactory;
        private readonly CurveFactory _curveFactory;
        private readonly SampleManager _sampleManager;

        public SynthWishes(IContext context = null)
        {
            Context = context ?? PersistenceHelper.CreateContext();

            _operatorFactory = ServiceFactory.CreateOperatorFactory(context);
            _curveFactory = ServiceFactory.CreateCurveFactory(context);
            _sampleManager = ServiceFactory.CreateSampleManager(context);
            InitializeOperatorWishes();
        }

        // Helpers

        private static string FormatAudioFileName(string name, AudioFileFormatEnum audioFileFormatEnum)
        {
            string fileName = Path.GetFileNameWithoutExtension(name);
            string fileExtension = audioFileFormatEnum.GetFileExtension();
            fileName += fileExtension;
            return fileName;
        }
        
        private static string FormatMetrics(double audioDuration, double calculationDuration, int complexity)
        {
            string realTimeMessage = FormatRealTimeMessage(audioDuration, calculationDuration);
            string sep = realTimeMessage != default ? " | " : "";
            string complexityMessage = $"Complexity Ｏ ( {complexity} )";
            string metricsMessage = $"{realTimeMessage}{sep}{complexityMessage}";
            return metricsMessage;
        }
        
        private static string FormatRealTimeMessage(double audioDuration, double calculationDuration)
        {
            var isRunningInTooling = ToolingHelper.IsRunningInTooling;
            if (isRunningInTooling.Data)
            {
                // If running in tooling, omitting the performance message from the result,
                // because it has little meaning with sampling rates  below 150
                // that are employed for tooling by default, to keep them running fast.
                return default;
            }

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