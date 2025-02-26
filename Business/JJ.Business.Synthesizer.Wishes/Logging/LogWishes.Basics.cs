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
        
        internal static LogWishes Resolve(FlowNode        entity                         ) => Resolve(entity, x => x.SynthWishes);
        internal static LogWishes Resolve(ConfigResolver  entity                         ) => Resolve(entity, x => x.SynthWishes);
        internal static LogWishes Resolve(ConfigSection   entity                         ) => Resolve(entity, x => null         );
        internal static LogWishes Resolve(ConfigSection   entity, SynthWishes synthWishes) => Resolve(entity, x => synthWishes);
        internal static LogWishes Resolve(Tape            entity                         ) => Resolve(entity, x => x.SynthWishes);
        internal static LogWishes Resolve(TapeConfig      entity                         ) => Resolve(entity, x => x.SynthWishes);
        internal static LogWishes Resolve(TapeActions     entity                         ) => Resolve(entity, x => x.SynthWishes);
        internal static LogWishes Resolve(TapeAction      entity                         ) => Resolve(entity, x => x.SynthWishes);
        internal static LogWishes Resolve(Buff            entity                         ) => Resolve(entity, x => x.SynthWishes);
        internal static LogWishes Resolve(AudioFileOutput entity                         ) => Resolve(entity, x => null);
        internal static LogWishes Resolve(AudioFileOutput entity, SynthWishes synthWishes) => Resolve(entity, x => synthWishes);
        internal static LogWishes Resolve(Sample          entity                         ) => Resolve(entity, x => null);
        internal static LogWishes Resolve(Sample          entity, SynthWishes synthWishes) => Resolve(entity, x => synthWishes);
        internal static LogWishes Resolve(AudioInfoWish   entity                         ) => Resolve(entity, x => null);
        internal static LogWishes Resolve(AudioInfoWish   entity, SynthWishes synthWishes) => Resolve(entity, x => synthWishes);
        internal static LogWishes Resolve(AudioFileInfo   entity                         ) => Resolve(entity, x => null);
        internal static LogWishes Resolve(AudioFileInfo   entity, SynthWishes synthWishes) => Resolve(entity, x => synthWishes);
        internal static LogWishes Resolve(WavHeaderStruct entity                         ) => Resolve(entity, x => null);
        internal static LogWishes Resolve(WavHeaderStruct entity, SynthWishes synthWishes) => Resolve(entity, x => synthWishes);
        internal static LogWishes Resolve(IList<Tape>     tapes                          ) => Resolve(tapes , x => x.Where(y => y != null).Select(y => y.SynthWishes).FirstOrDefault());

        private static LogWishes Resolve<T>(T entity, Func<T, SynthWishes> getSynthWishes)
        {
            if (entity == null) throw new NullException(() => entity);
            if (getSynthWishes == null) throw new NullException(() => getSynthWishes);
            SynthWishes synthWishes = getSynthWishes.Invoke(entity);
            return synthWishes?.Logging ?? Static;
        }
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        internal LogWishes Logging { get; } = new LogWishes();

        // TODO: Synonyms
        
        public SynthWishes WithLogging(bool enabled = true) { Logging.LoggingEnabled = enabled; return this; }
        public SynthWishes WithLoggingEnabled() => WithLogging(true);
        public SynthWishes WithLoggingDisabled() => WithLogging(false);

        // TODO: Turn into extension methods and add more target types?
        public void Log            (string message = null)                         => Logging.Log(message);
        public void LogSpaced      (string message)                                => Logging.LogSpaced(message);
        public void LogTitle       (string title)                                  => Logging.LogTitle(title);
        public void LogTitleStrong (string title)                                  => Logging.LogTitleStrong(title);
        public void LogOutputFile  (string filePath, string sourceFilePath = null) => Logging.LogOutputFile(filePath, sourceFilePath);
    }

    public partial class FlowNode
    {
        internal LogWishes Logging => LogWishes.Resolve(this);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        internal static LogWishes Logging(this FlowNode        entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this ConfigResolver  entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this ConfigSection   entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this ConfigSection   entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this Tape            entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this TapeConfig      entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this TapeActions     entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this TapeAction      entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this Buff            entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioFileOutput entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioFileOutput entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this Sample          entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this Sample          entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this AudioInfoWish   entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioInfoWish   entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this AudioFileInfo   entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this AudioFileInfo   entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this WavHeaderStruct entity                         ) => LogWishes.Resolve(entity);
        internal static LogWishes Logging(this WavHeaderStruct entity, SynthWishes synthWishes) => LogWishes.Resolve(entity, synthWishes);
        internal static LogWishes Logging(this IList<Tape>     tapes                          ) => LogWishes.Resolve(tapes);

        // The target objects aren't used for anything other than resolving a SynthWishes object,
        // and availability on multiple target types for convenience.
        
        public   static void Log           (this FlowNode       entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log           (this Tape           entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log           (this TapeConfig     entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log           (this TapeActions    entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log           (this TapeAction     entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log           (this Buff           entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        internal static void Log           (this ConfigResolver entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void LogSpaced     (this FlowNode       entity, string message = "") => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced     (this Tape           entity, string message = "") => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced     (this TapeConfig     entity, string message = "") => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced     (this TapeActions    entity, string message = "") => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced     (this TapeAction     entity, string message = "") => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced     (this Buff           entity, string message = "") => LogWishes.Resolve(entity).LogSpaced(message);
        internal static void LogSpaced     (this ConfigResolver entity, string message = "") => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogTitle      (this FlowNode       entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle      (this Tape           entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle      (this TapeConfig     entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle      (this TapeActions    entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle      (this TapeAction     entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle      (this Buff           entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        internal static void LogTitle      (this ConfigResolver entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitleStrong(this FlowNode       entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this Tape           entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeConfig     entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeActions    entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeAction     entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this Buff           entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        internal static void LogTitleStrong(this ConfigResolver entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogOutputFile (this FlowNode       entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Tape           entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeConfig     entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeActions    entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeAction     entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Buff           entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        internal static void LogOutputFile (this ConfigResolver entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
    }
}