using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Logging;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        /// <summary>
        /// Always filled in. Holds the main LogWishes instance to use.
        /// </summary>
        internal LogWishes Logging { get; }
        
        // Defined here to ditch `this` qualifier in case of inheritance.
        
        public   void Log           (                   string message = null) => Logging.Log(message);
        internal void Log           (object   category, string message       ) => Logging.Log(category, message);
        public   void LogSpaced     (                   string message       ) => Logging.LogSpaced(message);
        internal void LogSpaced     (object   category, string message       ) => Logging.LogSpaced(category, message);
        public   void LogTitle      (                   string title         ) => Logging.LogTitle(title);
        internal void LogTitle      (object   category, string title         ) => Logging.LogTitle(category, title);
        public   void LogTitleStrong(                   string title         ) => Logging.LogTitleStrong(title);
        internal void LogTitleStrong(object   category, string title         ) => Logging.LogTitleStrong(category, title);
        
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
        
        public IList<string> GetLogCats => Logging.Logger.GetCategories();
        
        public SynthWishes OnlyLogCat(object category) => OnlyLogCats(category);
        public SynthWishes OnlyLogCats(params object[] categories) => OnlyLogCats((IList<object>)categories);
        public SynthWishes OnlyLogCats(IList<object> categories)
        {
            _config.OnlyLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes AlsoLogCat(object category) => AlsoLogCats(category);
        public SynthWishes AlsoLogCats(params object[] categories) => AlsoLogCats((IList<object>)categories);
        public SynthWishes AlsoLogCats(IList<object> categories)
        {
            if (!Has(categories)) throw new Exception($"{nameof(categories)} not supplied.");
            
            foreach (object category in categories)
            {
                string categoryString = ResolveName(category);
                if (!Logging.Logger.WillLog(categoryString)) // TODO: This is may not be sound logic. I feel that edge cases would slip through, where former categories are excluded.
                {
                    _config.AddLogCat(categoryString);
                }
            }
            
            Logging.UpdateLogger(_config);
            
            return this;
        }

        public SynthWishes DontLogCat(object category) => DontLogCats(category);
        public SynthWishes DontLogCats(params object[] categories) => DontLogCats((IList<object>)categories);
        public SynthWishes DontLogCats(IList<object> categories)
        {
            _config.DontLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }

        public SynthWishes ClearLogCats()
        {
            _config.ClearLogCats();
            Logging.UpdateLogger(_config);
            return this;
        }
    }
}
