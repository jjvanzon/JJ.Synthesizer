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

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        internal LogWishes LogWishes { get; } = new LogWishes();

        public SynthWishes WithLogging(bool enabled = true) { LogWishes.LoggingEnabled = enabled; return this; }
        
        public void Log            (string message = null)                         => LogWishes.Log(message);
        public void LogSpaced      (string message)                                => LogWishes.LogSpaced(message);
        public void LogTitle       (string title)                                  => LogWishes.LogTitle(title);
        public void LogTitleStrong (string title)                                  => LogWishes.LogTitleStrong(title);
        public void LogOutputFile  (string filePath, string sourceFilePath = null) => LogWishes.LogOutputFile(filePath, sourceFilePath);
    }
}

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
        
        internal static LogWishes GetLogWishes<T>(T entity, Func<T, SynthWishes> getSynthWishes)
        {
            if (entity == null) throw new NullException(() => entity);
            if (getSynthWishes == null) throw new NullException(() => getSynthWishes);
            SynthWishes synthWishes = getSynthWishes.Invoke(entity);
            return synthWishes?.LogWishes ?? Static;
        }
    }
    
    public static partial class LogExtensions
    {
        // The target objects aren't used for anything other than resolving a SynthWishes object,
        // and availability on multiple target types for convenience.
        
        public   static void Log           (this FlowNode       entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).Log(message);
        public   static void Log           (this Tape           entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).Log(message);
        public   static void Log           (this TapeConfig     entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).Log(message);
        public   static void Log           (this TapeActions    entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).Log(message);
        public   static void Log           (this TapeAction     entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).Log(message);
        public   static void Log           (this Buff           entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).Log(message);
        internal static void Log           (this ConfigResolver entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).Log(message);
        public   static void LogSpaced     (this FlowNode       entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).LogSpaced(message);
        public   static void LogSpaced     (this Tape           entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).LogSpaced(message);
        public   static void LogSpaced     (this TapeConfig     entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).LogSpaced(message);
        public   static void LogSpaced     (this TapeActions    entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).LogSpaced(message);
        public   static void LogSpaced     (this TapeAction     entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).LogSpaced(message);
        public   static void LogSpaced     (this Buff           entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).LogSpaced(message);
        internal static void LogSpaced     (this ConfigResolver entity, string message = "") => GetLogWishes(entity, x => x.SynthWishes).LogSpaced(message);
        public   static void LogTitle      (this FlowNode       entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitle(title);
        public   static void LogTitle      (this Tape           entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitle(title);
        public   static void LogTitle      (this TapeConfig     entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitle(title);
        public   static void LogTitle      (this TapeActions    entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitle(title);
        public   static void LogTitle      (this TapeAction     entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitle(title);
        public   static void LogTitle      (this Buff           entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitle(title);
        internal static void LogTitle      (this ConfigResolver entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitle(title);
        public   static void LogTitleStrong(this FlowNode       entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitleStrong(title);
        public   static void LogTitleStrong(this Tape           entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeConfig     entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeActions    entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeAction     entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitleStrong(title);
        public   static void LogTitleStrong(this Buff           entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitleStrong(title);
        internal static void LogTitleStrong(this ConfigResolver entity, string title) => GetLogWishes(entity, x => x.SynthWishes).LogTitleStrong(title);
        public   static void LogOutputFile (this FlowNode       entity, string filePath, string sourceFilePath = null) => GetLogWishes(entity, x => x.SynthWishes).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Tape           entity, string filePath, string sourceFilePath = null) => GetLogWishes(entity, x => x.SynthWishes).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeConfig     entity, string filePath, string sourceFilePath = null) => GetLogWishes(entity, x => x.SynthWishes).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeActions    entity, string filePath, string sourceFilePath = null) => GetLogWishes(entity, x => x.SynthWishes).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeAction     entity, string filePath, string sourceFilePath = null) => GetLogWishes(entity, x => x.SynthWishes).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Buff           entity, string filePath, string sourceFilePath = null) => GetLogWishes(entity, x => x.SynthWishes).LogOutputFile(filePath, sourceFilePath);
        internal static void LogOutputFile (this ConfigResolver entity, string filePath, string sourceFilePath = null) => GetLogWishes(entity, x => x.SynthWishes).LogOutputFile(filePath, sourceFilePath);
    }
}