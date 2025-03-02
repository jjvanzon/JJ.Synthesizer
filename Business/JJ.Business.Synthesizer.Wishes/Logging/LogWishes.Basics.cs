using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Logging;
using JJ.Framework.Wishes.Logging.Loggers;
using JJ.Framework.Wishes.Text;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.Environment;
using static System.IO.File;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        internal const string LogOutputFileCategoryNotSupported = "Use this instead: Log(category, FormatOutputFile(filePath, sourceFilePath));";

        public static LogWishes Static { get; } = new LogWishes();

        private readonly ILogger _logger = LoggingFactory.CreateLogger(ConfigResolver.Static.LoggerConfig);

        public bool Enabled { get; set; } = true; // = Config.LoggerConfig.Active ?? DefaultLoggingEnabled; // TODO: Use config somehow

        // NOTE: All the threading, locking and flushing helped
        // Test Explorer in Visual Studio 2022
        // avoid mangling blank lines, for the most part.
        
        private readonly object _logLock = new object();
        private bool _blankLinePending;

        public   void Log(string message = default) => Log(category: "", message);
        internal void Log(string category, string message)
        {
            if (!Enabled) return;
            if (!_logger.WillLog(category))
            {
                return;
            }
            
            lock (_logLock)
            {
                message = message ?? "";
               
                if (!message.FilledIn())
                {
                    _blankLinePending = true;
                    return;
                }
                
                if (_blankLinePending)
                {
                    if (!message.StartsWithBlankLine())
                    {
                        message = NewLine + message;
                    }
                }

                _blankLinePending = EndsWithBlankLine(message);

                _logger.Log(category, message.TrimEnd());
            }
        }

        public   void LogSpaced(string message) => LogSpaced(category: "", message);
        internal void LogSpaced(string category, string message) 
        {
            Log(category, ""); 
            Log(category, message); 
            Log(category, "");
        }

        public   void LogTitle(string title) => LogTitle(category: "", title);
        internal void LogTitle(string category, string title) => LogSpaced(category, PrettyTitle(title));
        
        public   void LogTitleStrong(string title) => LogTitleStrong(category: "", title);
        internal void LogTitleStrong(string category, string title)
        {
            string upperCase = (title ?? "").ToUpper();
            LogSpaced(category, PrettyTitle(upperCase, underlineChar: '='));
        }

        public void LogOutputFile(string filePath, string sourceFilePath = null)
        {
            Log(FormatOutputFile(filePath, sourceFilePath));
        }
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        // ReSharper disable UnusedParameter.Global
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal void LogOutputFile(string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        // ReSharper restore once UnusedParameter.Global
        
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

        internal static LogWishes Resolve(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return synthWishes.Logging;
        }
        
        internal static LogWishes Resolve(FlowNode flowNode)
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            return flowNode.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(ConfigResolver configResolver)
        {
            if (configResolver == null) throw new NullException(() => configResolver);
            return configResolver.SynthWishes?.Logging ?? Static;
        }
        
        internal static LogWishes Resolve(Tape tape )
        {
            if (tape == null) throw new NullException(() => tape);
            return tape.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            return tapeConfig.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(TapeActions tapeActions)
        {
            if (tapeActions == null) throw new NullException(() => tapeActions);
            return tapeActions.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(TapeAction tapeAction)
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            return tapeAction.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(Buff buff)
        {
            if (buff == null) throw new NullException(() => buff);
            return buff.SynthWishes?.Logging ?? Static;
        }
        
        // ReSharper disable UnusedParameter.Global
        
        internal static LogWishes Resolve(ConfigSection   configSection                           ) =>                         Static;
        internal static LogWishes Resolve(ConfigSection   configSection,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve(AudioFileOutput audioFileOutput                         ) =>                         Static;
        internal static LogWishes Resolve(AudioFileOutput audioFileOutput, SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve(Sample          sample                                  ) =>                         Static;
        internal static LogWishes Resolve(Sample          sample,          SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve(AudioInfoWish   audioInfoWish                           ) =>                         Static;
        internal static LogWishes Resolve(AudioInfoWish   audioInfoWish,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve(AudioFileInfo   audioFileInfo                           ) =>                         Static;
        internal static LogWishes Resolve(AudioFileInfo   audioFileInfo,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve(WavHeaderStruct wavHeader                               ) =>                         Static;
        internal static LogWishes Resolve(WavHeaderStruct wavHeader,       SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        
        // ReSharper restore UnusedParameter.Global

        internal static LogWishes Resolve(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            if (tapes.Count == 0) return Static;
            if (tapes[0] == null) return Static;
            return tapes[0].SynthWishes.Logging;
        }
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        /// <summary>
        /// Always filled in. Holds the main LogWishes instance to use.
        /// </summary>
        internal LogWishes Logging { get; } = new LogWishes();

        // TODO: Synonyms
        
        public SynthWishes WithLogging(bool enabled = true) { Logging.Enabled = enabled; return this; }
        public SynthWishes WithLoggingEnabled() => WithLogging(true);
        public SynthWishes WithLoggingDisabled() => WithLogging(false);
        
        // Defined here to ditch `this` qualifier in case of inheritance.
        
        public   void Log           (                 string message = null) => Logging.Log(message);
        internal void Log           (string category, string message       ) => Logging.Log(category, message);
        public   void LogSpaced     (                 string message       ) => Logging.LogSpaced(message);
        internal void LogSpaced     (string category, string message       ) => Logging.LogSpaced(category, message);
        public   void LogTitle      (                 string title         ) => Logging.LogTitle(title);
        internal void LogTitle      (string category, string title         ) => Logging.LogTitle(category, title);
        public   void LogTitleStrong(                 string title         ) => Logging.LogTitleStrong(title);
        internal void LogTitleStrong(string category, string title         ) => Logging.LogTitleStrong(category, title);
        
        public void LogOutputFile (string filePath, string sourceFilePath = null) => Logging.LogOutputFile(filePath, sourceFilePath);
        // ReSharper disable UnusedParameter.Global
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal void LogOutputFile(string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        // ReSharper restore once UnusedParameter.Global
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        internal static LogWishes Logging(this SynthWishes synthWishes) // Providing method call syntax alongside property syntax.
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return synthWishes.Logging;
        }
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
        
        public   static void Log(this FlowNode       entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        internal static void Log(this ConfigResolver entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log(this Tape           entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log(this TapeConfig     entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        public   static void Log(this TapeActions    entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        // NOTE: Log(TapeAction) resolves to the specialized LogAction(TapeAction) method instead of this basic log call.
        public   static void Log(this Buff           entity, string message = "") => LogWishes.Resolve(entity).Log(message);
        internal static void Log(this FlowNode       entity, string category, string message) => LogWishes.Resolve(entity).Log(category, message);
        internal static void Log(this ConfigResolver entity, string category, string message) => LogWishes.Resolve(entity).Log(category, message);
        internal static void Log(this Tape           entity, string category, string message) => LogWishes.Resolve(entity).Log(category, message);
        internal static void Log(this TapeConfig     entity, string category, string message) => LogWishes.Resolve(entity).Log(category, message);
        internal static void Log(this TapeActions    entity, string category, string message) => LogWishes.Resolve(entity).Log(category, message);
        // NOTE: Log(TapeAction) resolves to the specialized LogAction(TapeAction) method instead of this basic log call.
        internal static void Log(this Buff           entity, string category, string message) => LogWishes.Resolve(entity).Log(category, message);

        public   static void LogSpaced (this FlowNode       entity, string message) => LogWishes.Resolve(entity).LogSpaced(message);
        internal static void LogSpaced (this ConfigResolver entity, string message) => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced (this Tape           entity, string message) => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced (this TapeConfig     entity, string message) => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced (this TapeActions    entity, string message) => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced (this TapeAction     entity, string message) => LogWishes.Resolve(entity).LogSpaced(message);
        public   static void LogSpaced (this Buff           entity, string message) => LogWishes.Resolve(entity).LogSpaced(message);
        internal static void LogSpaced (this FlowNode       entity, string category, string message) => LogWishes.Resolve(entity).LogSpaced(category, message);
        internal static void LogSpaced (this ConfigResolver entity, string category, string message) => LogWishes.Resolve(entity).LogSpaced(category, message);
        internal static void LogSpaced (this Tape           entity, string category, string message) => LogWishes.Resolve(entity).LogSpaced(category, message);
        internal static void LogSpaced (this TapeConfig     entity, string category, string message) => LogWishes.Resolve(entity).LogSpaced(category, message);
        internal static void LogSpaced (this TapeActions    entity, string category, string message) => LogWishes.Resolve(entity).LogSpaced(category, message);
        internal static void LogSpaced (this TapeAction     entity, string category, string message) => LogWishes.Resolve(entity).LogSpaced(category, message);
        internal static void LogSpaced (this Buff           entity, string category, string message) => LogWishes.Resolve(entity).LogSpaced(category, message);
        
        public   static void LogTitle(this FlowNode       entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        internal static void LogTitle(this ConfigResolver entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle(this Tape           entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle(this TapeConfig     entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle(this TapeActions    entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle(this TapeAction     entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        public   static void LogTitle(this Buff           entity, string title) => LogWishes.Resolve(entity).LogTitle(title);
        internal static void LogTitle(this FlowNode       entity, string category, string title) => LogWishes.Resolve(entity).LogTitle(category, title);
        internal static void LogTitle(this ConfigResolver entity, string category, string title) => LogWishes.Resolve(entity).LogTitle(category, title);
        internal static void LogTitle(this Tape           entity, string category, string title) => LogWishes.Resolve(entity).LogTitle(category, title);
        internal static void LogTitle(this TapeConfig     entity, string category, string title) => LogWishes.Resolve(entity).LogTitle(category, title);
        internal static void LogTitle(this TapeActions    entity, string category, string title) => LogWishes.Resolve(entity).LogTitle(category, title);
        internal static void LogTitle(this TapeAction     entity, string category, string title) => LogWishes.Resolve(entity).LogTitle(category, title);
        internal static void LogTitle(this Buff           entity, string category, string title) => LogWishes.Resolve(entity).LogTitle(category, title);

        public   static void LogTitleStrong(this FlowNode       entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        internal static void LogTitleStrong(this ConfigResolver entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this Tape           entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeConfig     entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeActions    entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeAction     entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this Buff           entity, string title) => LogWishes.Resolve(entity).LogTitleStrong(title);
        internal static void LogTitleStrong(this FlowNode       entity, string category, string title) => LogWishes.Resolve(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this ConfigResolver entity, string category, string title) => LogWishes.Resolve(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this Tape           entity, string category, string title) => LogWishes.Resolve(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this TapeConfig     entity, string category, string title) => LogWishes.Resolve(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this TapeActions    entity, string category, string title) => LogWishes.Resolve(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this TapeAction     entity, string category, string title) => LogWishes.Resolve(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this Buff           entity, string category, string title) => LogWishes.Resolve(entity).LogTitleStrong(category, title);
        
        public   static void LogOutputFile (this FlowNode       entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Tape           entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeConfig     entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeActions    entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this TapeAction     entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        public   static void LogOutputFile (this Buff           entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        internal static void LogOutputFile (this ConfigResolver entity, string filePath, string sourceFilePath = null) => LogWishes.Resolve(entity).LogOutputFile(filePath, sourceFilePath);
        // ReSharper disable UnusedParameter.Global
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this FlowNode       entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this Tape           entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this TapeConfig     entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this TapeActions    entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this TapeAction     entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this Buff           entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        /// <inheritdoc cref="_logoutputfilewithcategory" />
        [Obsolete(LogOutputFileCategoryNotSupported, true)] internal static void LogOutputFile (this ConfigResolver entity, string category, string filePath, string sourceFilePath) => throw new NotSupportedException(LogOutputFileCategoryNotSupported);
        // ReSharper restore once UnusedParameter.Global
    }
}