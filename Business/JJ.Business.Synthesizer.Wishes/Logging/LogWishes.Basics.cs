using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Logging;
using static System.Environment;
using static System.IO.File;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Wishes.Logging.LoggingFactory;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        public static LogWishes Static { get; } = new LogWishes();

        private readonly ILogger _logger = CreateLoggerFromConfig(ConfigResolver.Static.LoggerConfig);

        public bool LoggingEnabled { get; set; } = true; // = Config.LoggerConfig.Active ?? DefaultLoggingEnabled; // TODO: Use config somehow

        // NOTE: All the threading, locking and flushing helped
        // Test Explorer in Visual Studio 2022
        // avoid mangling blank lines, for the most part.
        
        private readonly object _logLock = new object();
        private readonly ThreadLocal<bool> _blankLinePending = new ThreadLocal<bool>();

        public void Log(string message = default)
        {
            if (!LoggingEnabled) return;
            
            lock (_logLock)
            {
                message = message ?? "";
               
                if (!message.FilledIn())
                {
                    _blankLinePending.Value = true;
                    return;
                }
                
                if (_blankLinePending.Value)
                {
                    if (!message.StartsWithBlankLine())
                    {
                        message = NewLine + message;
                    }
                }

                _blankLinePending.Value = EndsWithBlankLine(message);

                _logger.Log(message.TrimEnd());
            }
        }

        public void LogSpaced(string message) { Log(); Log(message); Log(); }

        public void LogTitle(string title) => LogSpaced(PrettyTitle(title));
        
        public void LogTitleStrong(string title)
        {
            string upperCase = (title ?? "").ToUpper();
            LogSpaced(PrettyTitle(upperCase, underlineChar: '='));
        }

        public void LogOutputFile(string filePath, string sourceFilePath = null) => Log(FormatOutputFile(filePath, sourceFilePath));
        
        internal string FormatOutputFile(string filePath, string sourceFilePath = null)
        {
            if (!Has(filePath)) return default;
            if (!Exists(filePath)) return default;
            string prefix = "  ";
            string sourceFileString = default;
            if (Has(sourceFilePath)) sourceFileString += $" (copied {sourceFilePath})";
            string message = prefix + filePath + sourceFileString;
            return message;
        }
                
        internal string FormatOutputBytes(byte[] bytes)
        {
            if (!Has(bytes)) return default;
            return $"  {PrettyByteCount(bytes.Length)} written to memory.";
        }
        
        // Helper
        
        internal static LogWishes GetLogWishes(FlowNode        entity                         ) => GetLogWishes(entity, x => x.SynthWishes);
        internal static LogWishes GetLogWishes(ConfigResolver  entity                         ) => GetLogWishes(entity, x => x.SynthWishes);
        internal static LogWishes GetLogWishes(ConfigSection   entity                         ) => GetLogWishes(entity, x => null         );
        internal static LogWishes GetLogWishes(ConfigSection   entity, SynthWishes synthWishes) => GetLogWishes(entity, x => synthWishes);
        internal static LogWishes GetLogWishes(Tape            entity                         ) => GetLogWishes(entity, x => x.SynthWishes);
        internal static LogWishes GetLogWishes(TapeConfig      entity                         ) => GetLogWishes(entity, x => x.SynthWishes);
        internal static LogWishes GetLogWishes(TapeActions     entity                         ) => GetLogWishes(entity, x => x.SynthWishes);
        internal static LogWishes GetLogWishes(TapeAction      entity                         ) => GetLogWishes(entity, x => x.SynthWishes);
        internal static LogWishes GetLogWishes(Buff            entity                         ) => GetLogWishes(entity, x => x.SynthWishes);
        internal static LogWishes GetLogWishes(AudioFileOutput entity                         ) => GetLogWishes(entity, x => null);
        internal static LogWishes GetLogWishes(AudioFileOutput entity, SynthWishes synthWishes) => GetLogWishes(entity, x => synthWishes);
        internal static LogWishes GetLogWishes(Sample          entity                         ) => GetLogWishes(entity, x => null);
        internal static LogWishes GetLogWishes(Sample          entity, SynthWishes synthWishes) => GetLogWishes(entity, x => synthWishes);
        internal static LogWishes GetLogWishes(AudioInfoWish   entity                         ) => GetLogWishes(entity, x => null);
        internal static LogWishes GetLogWishes(AudioInfoWish   entity, SynthWishes synthWishes) => GetLogWishes(entity, x => synthWishes);
        internal static LogWishes GetLogWishes(AudioFileInfo   entity                         ) => GetLogWishes(entity, x => null);
        internal static LogWishes GetLogWishes(AudioFileInfo   entity, SynthWishes synthWishes) => GetLogWishes(entity, x => synthWishes);
        internal static LogWishes GetLogWishes(WavHeaderStruct entity                         ) => GetLogWishes(entity, x => null);
        internal static LogWishes GetLogWishes(WavHeaderStruct entity, SynthWishes synthWishes) => GetLogWishes(entity, x => synthWishes);
        internal static LogWishes GetLogWishes(IList<Tape>     tapes                          ) => GetLogWishes(tapes , x => x.Where(y => y != null).Select(y => y.SynthWishes).FirstOrDefault());

        private static LogWishes GetLogWishes<T>(T entity, Func<T, SynthWishes> getSynthWishes)
        {
            if (entity == null) throw new NullException(() => entity);
            if (getSynthWishes == null) throw new NullException(() => getSynthWishes);
            SynthWishes synthWishes = getSynthWishes.Invoke(entity);
            return synthWishes?.LogWishes ?? Static;
        }
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        internal LogWishes LogWishes { get; } = new LogWishes();

        // TODO: Synonyms
        public SynthWishes WithLogging(bool enabled = true) { LogWishes.LoggingEnabled = enabled; return this; }
        public SynthWishes WithLoggingEnabled() => WithLogging(true);
        public SynthWishes WithLoggingDisabled() => WithLogging(false);

        public void Log            (string message = null)                         => LogWishes.Log(message);
        public void LogSpaced      (string message)                                => LogWishes.LogSpaced(message);
        public void LogTitle       (string title)                                  => LogWishes.LogTitle(title);
        public void LogTitleStrong (string title)                                  => LogWishes.LogTitleStrong(title);
        public void LogOutputFile  (string filePath, string sourceFilePath = null) => LogWishes.LogOutputFile(filePath, sourceFilePath);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        // The target objects aren't used for anything other than resolving a SynthWishes object,
        // and availability on multiple target types for convenience.
        
        public   static void Log           (this FlowNode       entity, string message = "") => entity.GetLogWishes().Log(message);
        public   static void Log           (this Tape           entity, string message = "") => entity.GetLogWishes().Log(message);
        public   static void Log           (this TapeConfig     entity, string message = "") => entity.GetLogWishes().Log(message);
        public   static void Log           (this TapeActions    entity, string message = "") => entity.GetLogWishes().Log(message);
        public   static void Log           (this TapeAction     entity, string message = "") => entity.GetLogWishes().Log(message);
        public   static void Log           (this Buff           entity, string message = "") => entity.GetLogWishes().Log(message);
        internal static void Log           (this ConfigResolver entity, string message = "") => entity.GetLogWishes().Log(message);
        public   static void LogSpaced     (this FlowNode       entity, string message = "") => entity.GetLogWishes().LogSpaced(message);
        public   static void LogSpaced     (this Tape           entity, string message = "") => entity.GetLogWishes().LogSpaced(message);
        public   static void LogSpaced     (this TapeConfig     entity, string message = "") => entity.GetLogWishes().LogSpaced(message);
        public   static void LogSpaced     (this TapeActions    entity, string message = "") => entity.GetLogWishes().LogSpaced(message);
        public   static void LogSpaced     (this TapeAction     entity, string message = "") => entity.GetLogWishes().LogSpaced(message);
        public   static void LogSpaced     (this Buff           entity, string message = "") => entity.GetLogWishes().LogSpaced(message);
        internal static void LogSpaced     (this ConfigResolver entity, string message = "") => entity.GetLogWishes().LogSpaced(message);
        public   static void LogTitle      (this FlowNode       entity, string title) => entity.GetLogWishes().LogTitle(title);
        public   static void LogTitle      (this Tape           entity, string title) => entity.GetLogWishes().LogTitle(title);
        public   static void LogTitle      (this TapeConfig     entity, string title) => entity.GetLogWishes().LogTitle(title);
        public   static void LogTitle      (this TapeActions    entity, string title) => entity.GetLogWishes().LogTitle(title);
        public   static void LogTitle      (this TapeAction     entity, string title) => entity.GetLogWishes().LogTitle(title);
        public   static void LogTitle      (this Buff           entity, string title) => entity.GetLogWishes().LogTitle(title);
        internal static void LogTitle      (this ConfigResolver entity, string title) => entity.GetLogWishes().LogTitle(title);
        public   static void LogTitleStrong(this FlowNode       entity, string title) => entity.GetLogWishes().LogTitleStrong(title);
        public   static void LogTitleStrong(this Tape           entity, string title) => entity.GetLogWishes().LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeConfig     entity, string title) => entity.GetLogWishes().LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeActions    entity, string title) => entity.GetLogWishes().LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeAction     entity, string title) => entity.GetLogWishes().LogTitleStrong(title);
        public   static void LogTitleStrong(this Buff           entity, string title) => entity.GetLogWishes().LogTitleStrong(title);
        internal static void LogTitleStrong(this ConfigResolver entity, string title) => entity.GetLogWishes().LogTitleStrong(title);
        public   static void LogOutputFile (this FlowNode       entity, string filePath, string sourceFilePath = null) => entity.GetLogWishes().LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Tape           entity, string filePath, string sourceFilePath = null) => entity.GetLogWishes().LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeConfig     entity, string filePath, string sourceFilePath = null) => entity.GetLogWishes().LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeActions    entity, string filePath, string sourceFilePath = null) => entity.GetLogWishes().LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeAction     entity, string filePath, string sourceFilePath = null) => entity.GetLogWishes().LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Buff           entity, string filePath, string sourceFilePath = null) => entity.GetLogWishes().LogOutputFile(filePath, sourceFilePath);
        internal static void LogOutputFile (this ConfigResolver entity, string filePath, string sourceFilePath = null) => entity.GetLogWishes().LogOutputFile(filePath, sourceFilePath);

        internal static LogWishes GetLogWishes(this FlowNode        entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this ConfigResolver  entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this ConfigSection   entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this ConfigSection   entity, SynthWishes synthWishes) => LogWishes.GetLogWishes(entity, synthWishes);
        internal static LogWishes GetLogWishes(this Tape            entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this TapeConfig      entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this TapeActions     entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this TapeAction      entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this Buff            entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this AudioFileOutput entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this AudioFileOutput entity, SynthWishes synthWishes) => LogWishes.GetLogWishes(entity, synthWishes);
        internal static LogWishes GetLogWishes(this Sample          entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this Sample          entity, SynthWishes synthWishes) => LogWishes.GetLogWishes(entity, synthWishes);
        internal static LogWishes GetLogWishes(this AudioInfoWish   entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this AudioInfoWish   entity, SynthWishes synthWishes) => LogWishes.GetLogWishes(entity, synthWishes);
        internal static LogWishes GetLogWishes(this AudioFileInfo   entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this AudioFileInfo   entity, SynthWishes synthWishes) => LogWishes.GetLogWishes(entity, synthWishes);
        internal static LogWishes GetLogWishes(this WavHeaderStruct entity                         ) => LogWishes.GetLogWishes(entity);
        internal static LogWishes GetLogWishes(this WavHeaderStruct entity, SynthWishes synthWishes) => LogWishes.GetLogWishes(entity, synthWishes);
        internal static LogWishes GetLogWishes(this IList<Tape>     tapes                          ) => LogWishes.GetLogWishes(tapes);

    }
}