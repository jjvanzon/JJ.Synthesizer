using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Framework.Reflection;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Logging.Loggers;
using JJ.Framework.Wishes.Text;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Wishes.Collections;
using static System.Environment;
using static JJ.Business.Synthesizer.Wishes.Logging.LogCategories;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Wishes.Logging.LoggingFactory;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using JJ.Framework.Wishes.Logging.Config;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    internal partial class LogWishes
    {
        private static readonly string DefaultCategory = Misc;
        private static LogWishes _static = new LogWishes(ConfigResolver.Static);
        private ILogger _logger;

        internal LogWishes(ConfigResolver configResolver) => UpdateLogger(configResolver);
        
        public void UpdateLogger(ConfigResolver configResolver)
        {
            if (configResolver == null) throw new NullException(() => configResolver);
            _logger = CreateLogger(configResolver.LoggingConfig);
        }
        
        // NOTE: All the threading, locking and flushing helped
        // Test Explorer in Visual Studio 2022
        // avoid mangling blank lines, for the most part.
        
        private readonly object _logLock = new object();
        private bool _blankLinePending;

        public   void Log(string message = default) => Log(DefaultCategory, message);
        internal void Log(string category, string message)
        {
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

        public   void LogSpaced(string message) => LogSpaced(DefaultCategory, message);
        internal void LogSpaced(string category, string message) 
        {
            Log(category, ""); 
            Log(category, message); 
            Log(category, "");
        }

        public   void LogTitle(string title) => LogTitle(DefaultCategory, title);
        internal void LogTitle(string category, string title) => LogSpaced(category, PrettyTitle(title));
        
        public   void LogTitleStrong(string title) => LogTitleStrong(DefaultCategory, title);
        internal void LogTitleStrong(string category, string title)
        {
            string upperCase = (title ?? "").ToUpper();
            LogSpaced(category, PrettyTitle(upperCase, underlineChar: '='));
        }

        internal static LogWishes ResolveLogging(SynthWishes synthWishes)
        {
            if (synthWishes == null) throw new NullException(() => synthWishes);
            return synthWishes.Logging;
        }
        
        internal static LogWishes ResolveLogging(FlowNode flowNode)
        {
            if (flowNode == null) throw new NullException(() => flowNode);
            return flowNode.SynthWishes.Logging;
        }

        internal static LogWishes ResolveLogging(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            return tape.SynthWishes.Logging;
        }
        
        internal static LogWishes ResolveLogging(TapeConfig tapeConfig)
        {
            if (tapeConfig == null) throw new NullException(() => tapeConfig);
            return tapeConfig.SynthWishes.Logging;
        }
        
        internal static LogWishes ResolveLogging(TapeActions tapeActions)
        {
            if (tapeActions == null) throw new NullException(() => tapeActions);
            return tapeActions.SynthWishes.Logging;
        }
        
        internal static LogWishes ResolveLogging(TapeAction tapeAction)
        {
            if (tapeAction == null) throw new NullException(() => tapeAction);
            return tapeAction.SynthWishes.Logging;
        }
        
        internal static LogWishes ResolveLogging(Buff buff)
        {
            if (buff == null) throw new NullException(() => buff);
            return buff.SynthWishes?.Logging ?? _static;
        }
        
        // ReSharper disable UnusedParameter.Global
        
        internal static LogWishes ResolveLogging(object entity                         ) =>                         _static;
        internal static LogWishes ResolveLogging(object entity, SynthWishes synthWishes) => synthWishes?.Logging ?? _static;

        // ReSharper restore UnusedParameter.Global

        internal static LogWishes Resolve(IList<Tape> tapes) => ResolveLogging(tapes);
        internal static LogWishes ResolveLogging(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            if (tapes.Count == 0) return _static;
            if (tapes[0] == null) return _static;
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
        internal LogWishes Logging { get; }

        // TODO: Synonyms
        
        //public SynthWishes WithLog(bool enabled = true){ Logging.Enabled = enabled; return this; }
        //public SynthWishes WithLogEnabled() => WithLog(true);
        //public SynthWishes WithLogDisabled() => WithLog(false);
        //public SynthWishes WithLogCats(params string[] categories) { Logging.SetCategories(categories); return this; }
        
        // Defined here to ditch `this` qualifier in case of inheritance.
        
        public   void Log           (                 string message = null) => Logging.Log(message);
        internal void Log           (string category, string message       ) => Logging.Log(category, message);
        public   void LogSpaced     (                 string message       ) => Logging.LogSpaced(message);
        internal void LogSpaced     (string category, string message       ) => Logging.LogSpaced(category, message);
        public   void LogTitle      (                 string title         ) => Logging.LogTitle(title);
        internal void LogTitle      (string category, string title         ) => Logging.LogTitle(category, title);
        public   void LogTitleStrong(                 string title         ) => Logging.LogTitleStrong(title);
        internal void LogTitleStrong(string category, string title         ) => Logging.LogTitleStrong(category, title);
        
        public SynthWishes SetLogActive(bool value)
        {
            _config.SetLogActive(value);
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes WithLog()
        {
            _config.WithLog();
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes NoLog()
        {
            _config.NoLog();
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes WithLogCats(params string[] categories)
        {
            _config.WithLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes WithLogCats(IList<string> categories)
        {
            _config.WithLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes AddLogCat(string category)
        {
            _config.AddLogCat(category);
            Logging.UpdateLogger(_config);
            return this;
        }
            
        public SynthWishes AddLogCats(params string[] categories)
        {
            _config.AddLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes AddLogCats(IList<string> categories)
        {
            _config.AddLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }

        public SynthWishes RemoveLogCat(string category)
        {
            _config.RemoveLogCat(category);
            Logging.UpdateLogger(_config);
            return this;
        }

        public SynthWishes RemoveLogCats(params string[] categories) 
        {
            _config.RemoveLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
            
        public SynthWishes RemoveLogCats(IList<string> categories)
        {
            _config.RemoveLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }

    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {

        // The target objects aren't used for anything other than resolving a SynthWishes object,
        // and availability on multiple target types for convenience.
        
        public   static void Log(this FlowNode        entity, string message = "") => ResolveLogging(entity).Log(message);
        internal static void Log(this ConfigResolver  entity, string message = "") => ResolveLogging(entity).Log(message);
        public   static void Log(this Tape            entity, string message = "") => ResolveLogging(entity).Log(message);
        public   static void Log(this TapeConfig      entity, string message = "") => ResolveLogging(entity).Log(message);
        public   static void Log(this TapeActions     entity, string message = "") => ResolveLogging(entity).Log(message);
        // NOTE: Log(TapeAction) resolves to the specialized LogAction(TapeAction) method instead of this basic log call.
        public   static void Log(this Buff            entity, string message = "") => ResolveLogging(entity).Log(message);
        public   static void Log(this AudioFileOutput entity, string message = "") => ResolveLogging(entity).Log(message);
        public   static void Log(this Sample          entity, string message = "") => ResolveLogging(entity).Log(message);
        
        internal static void Log(this FlowNode        entity, string category, string message) => ResolveLogging(entity).Log(category, message);
        internal static void Log(this ConfigResolver  entity, string category, string message) => ResolveLogging(entity).Log(category, message);
        internal static void Log(this Tape            entity, string category, string message) => ResolveLogging(entity).Log(category, message);
        internal static void Log(this TapeConfig      entity, string category, string message) => ResolveLogging(entity).Log(category, message);
        internal static void Log(this TapeActions     entity, string category, string message) => ResolveLogging(entity).Log(category, message);
        // NOTE: Log(TapeAction) resolves to the specialized LogAction(TapeAction) method instead of this basic log call.
        internal static void Log(this Buff            entity, string category, string message) => ResolveLogging(entity).Log(category, message);
        internal static void Log(this AudioFileOutput entity, string category, string message) => ResolveLogging(entity).Log(category, message);
        internal static void Log(this Sample          entity, string category, string message) => ResolveLogging(entity).Log(category, message);

        public   static void LogSpaced (this FlowNode        entity, string message) => ResolveLogging(entity).LogSpaced(message);
        internal static void LogSpaced (this ConfigResolver  entity, string message) => ResolveLogging(entity).LogSpaced(message);
        public   static void LogSpaced (this Tape            entity, string message) => ResolveLogging(entity).LogSpaced(message);
        public   static void LogSpaced (this TapeConfig      entity, string message) => ResolveLogging(entity).LogSpaced(message);
        public   static void LogSpaced (this TapeActions     entity, string message) => ResolveLogging(entity).LogSpaced(message);
        public   static void LogSpaced (this TapeAction      entity, string message) => ResolveLogging(entity).LogSpaced(message);
        public   static void LogSpaced (this Buff            entity, string message) => ResolveLogging(entity).LogSpaced(message);
        public   static void LogSpaced (this AudioFileOutput entity, string message) => ResolveLogging(entity).LogSpaced(message);
        public   static void LogSpaced (this Sample          entity, string message) => ResolveLogging(entity).LogSpaced(message);
        
        internal static void LogSpaced (this FlowNode        entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this ConfigResolver  entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this Tape            entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this TapeConfig      entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this TapeActions     entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this TapeAction      entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this Buff            entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this AudioFileOutput entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        internal static void LogSpaced (this Sample          entity, string category, string message) => ResolveLogging(entity).LogSpaced(category, message);
        
        public   static void LogTitle(this FlowNode        entity, string title) => ResolveLogging(entity).LogTitle(title);
        internal static void LogTitle(this ConfigResolver  entity, string title) => ResolveLogging(entity).LogTitle(title);
        public   static void LogTitle(this Tape            entity, string title) => ResolveLogging(entity).LogTitle(title);
        public   static void LogTitle(this TapeConfig      entity, string title) => ResolveLogging(entity).LogTitle(title);
        public   static void LogTitle(this TapeActions     entity, string title) => ResolveLogging(entity).LogTitle(title);
        public   static void LogTitle(this TapeAction      entity, string title) => ResolveLogging(entity).LogTitle(title);
        public   static void LogTitle(this Buff            entity, string title) => ResolveLogging(entity).LogTitle(title);
        public   static void LogTitle(this AudioFileOutput entity, string title) => ResolveLogging(entity).LogTitle(title);
        public   static void LogTitle(this Sample          entity, string title) => ResolveLogging(entity).LogTitle(title);
        
        internal static void LogTitle(this FlowNode        entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this ConfigResolver  entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this Tape            entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this TapeConfig      entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this TapeActions     entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this TapeAction      entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this Buff            entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this AudioFileOutput entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);
        internal static void LogTitle(this Sample          entity, string category, string title) => ResolveLogging(entity).LogTitle(category, title);

        public   static void LogTitleStrong(this FlowNode        entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        internal static void LogTitleStrong(this ConfigResolver  entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this Tape            entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeConfig      entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeActions     entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this TapeAction      entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this Buff            entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this AudioFileOutput entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        public   static void LogTitleStrong(this Sample          entity, string title) => ResolveLogging(entity).LogTitleStrong(title);
        
        internal static void LogTitleStrong(this FlowNode        entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this ConfigResolver  entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this Tape            entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this TapeConfig      entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this TapeActions     entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this TapeAction      entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this Buff            entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this AudioFileOutput entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
        internal static void LogTitleStrong(this Sample          entity, string category, string title) => ResolveLogging(entity).LogTitleStrong(category, title);
    }
}