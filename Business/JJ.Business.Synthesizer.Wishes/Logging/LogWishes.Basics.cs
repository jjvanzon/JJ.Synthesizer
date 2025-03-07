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
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using static System.Environment;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        public static LogWishes Static { get; } = new LogWishes(ConfigResolver.Static.LoggingConfig);

        private ILogger _logger;

        public bool Enabled { get; set; } = true; // = Config.LoggerConfig.Active ?? DefaultLoggingEnabled; // TODO: Use config somehow

        private readonly RootLoggingConfig _loggingConfig;

        private void UpdateLogger() => _logger = CreateLogger(_loggingConfig);
        internal LogWishes(RootLoggingConfig loggingConfig)
        {
            _loggingConfig = loggingConfig ?? throw new NullException(() => loggingConfig);
            UpdateLogger();
        }
        
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

        internal static LogWishes Resolve(SynthWishes synthWishes) => ResolveLogging(synthWishes);
        internal static LogWishes ResolveLogging(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return synthWishes.Logging;
        }
        
        internal static LogWishes Resolve(FlowNode flowNode) => ResolveLogging(flowNode);
        internal static LogWishes ResolveLogging(FlowNode flowNode)
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            return flowNode.SynthWishes.Logging;
        }
        
        //internal static LogWishes Resolve(ConfigResolver configResolver) => ResolveLogging(configResolver);
        //internal static LogWishes ResolveLogging(ConfigResolver configResolver)
        //{
        //    if (configResolver == null) throw new NullException(() => configResolver);
        //    return configResolver.SynthWishes?.Logging ?? Static;
        //}
        
        internal static LogWishes Resolve(Tape tape) => ResolveLogging(tape);
        internal static LogWishes ResolveLogging(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            return tape.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(TapeConfig tapeConfig) => ResolveLogging(tapeConfig);
        internal static LogWishes ResolveLogging(TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            return tapeConfig.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(TapeActions tapeActions) => ResolveLogging(tapeActions);
        internal static LogWishes ResolveLogging(TapeActions tapeActions)
        {
            if (tapeActions == null) throw new NullException(() => tapeActions);
            return tapeActions.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(TapeAction tapeActions) => ResolveLogging(tapeActions);
        internal static LogWishes ResolveLogging(TapeAction tapeAction)
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            return tapeAction.SynthWishes.Logging;
        }
        
        internal static LogWishes Resolve(Buff buff) => ResolveLogging(buff);
        internal static LogWishes ResolveLogging(Buff buff)
        {
            if (buff == null) throw new NullException(() => buff);
            return buff.SynthWishes?.Logging ?? Static;
        }
        
        // ReSharper disable UnusedParameter.Global
        
        //internal static LogWishes Resolve       (ConfigSection   configSection                           ) =>                         Static;
        //internal static LogWishes ResolveLogging(ConfigSection   configSection                           ) =>                         Static;
        //internal static LogWishes Resolve       (ConfigSection   configSection,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        //internal static LogWishes ResolveLogging(ConfigSection   configSection,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (AudioFileOutput audioFileOutput                         ) =>                         Static;
        internal static LogWishes ResolveLogging(AudioFileOutput audioFileOutput                         ) =>                         Static;
        internal static LogWishes Resolve       (AudioFileOutput audioFileOutput, SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(AudioFileOutput audioFileOutput, SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (Sample          sample                                  ) =>                         Static;
        internal static LogWishes ResolveLogging(Sample          sample                                  ) =>                         Static;
        internal static LogWishes Resolve       (Sample          sample,          SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(Sample          sample,          SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (AudioInfoWish   audioInfoWish                           ) =>                         Static;
        internal static LogWishes ResolveLogging(AudioInfoWish   audioInfoWish                           ) =>                         Static;
        internal static LogWishes Resolve       (AudioInfoWish   audioInfoWish,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(AudioInfoWish   audioInfoWish,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (AudioFileInfo   audioFileInfo                           ) =>                         Static;
        internal static LogWishes ResolveLogging(AudioFileInfo   audioFileInfo                           ) =>                         Static;
        internal static LogWishes Resolve       (AudioFileInfo   audioFileInfo,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(AudioFileInfo   audioFileInfo,   SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (WavHeaderStruct wavHeader                               ) =>                         Static;
        internal static LogWishes ResolveLogging(WavHeaderStruct wavHeader                               ) =>                         Static;
        internal static LogWishes Resolve       (WavHeaderStruct wavHeader,       SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(WavHeaderStruct wavHeader,       SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (byte[]          bytes                                   ) =>                         Static;
        internal static LogWishes ResolveLogging(byte[]          bytes                                   ) =>                         Static;
        internal static LogWishes Resolve       (byte[]          bytes,           SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(byte[]          bytes,           SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes Resolve       (object          entity                                  ) =>                         Static;
        internal static LogWishes ResolveLogging(object          entity                                  ) =>                         Static;
        internal static LogWishes Resolve       (object          entity,          SynthWishes synthWishes) => synthWishes?.Logging ?? Static;
        internal static LogWishes ResolveLogging(object          entity,          SynthWishes synthWishes) => synthWishes?.Logging ?? Static;

        // ReSharper restore UnusedParameter.Global

        internal static LogWishes Resolve(IList<Tape> tapes) => ResolveLogging(tapes);
        internal static LogWishes ResolveLogging(IList<Tape> tapes)
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
        public LogWishes Logging { get; } = new LogWishes();

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
    }
}