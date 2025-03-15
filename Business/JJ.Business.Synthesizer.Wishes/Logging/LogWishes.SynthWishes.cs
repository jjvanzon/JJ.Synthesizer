using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Logging;
using static JJ.Framework.Nully.Core.FilledInWishes;

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
        
        public void Log           (                 string message = null) => Logging.Log(message);
        public void Log           (string category, string message       ) => Logging.Log(category, message);
        public void LogSpaced     (                 string message       ) => Logging.LogSpaced(message);
        public void LogSpaced     (string category, string message       ) => Logging.LogSpaced(category, message);
        public void LogTitle      (                 string title         ) => Logging.LogTitle(title);
        public void LogTitle      (string category, string title         ) => Logging.LogTitle(category, title);
        public void LogTitleStrong(                 string title         ) => Logging.LogTitleStrong(title);
        public void LogTitleStrong(string category, string title         ) => Logging.LogTitleStrong(category, title);
        
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
        
        public SynthWishes OnlyLogCats(params string[] categories)
        {
            _config.OnlyLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public SynthWishes OnlyLogCats(IList<string> categories)
        {
            _config.OnlyLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
        
        public IList<string> GetLogCats => Logging.Logger.GetCategories();
        
        public SynthWishes AlsoLogCat(string category) => AlsoLogCats(category);
        public SynthWishes AlsoLogCats(params string[] categories) => AlsoLogCats((IList<string>)categories);
        public SynthWishes AlsoLogCats(IList<string> categories)
        {
            if (!Has(categories)) throw new Exception($"{nameof(categories)} not supplied.");
            
            foreach (string category in categories)
            {
                // TODO: This is may not be sound logic. I feel that edge cases would slip through, where former categories are excluded.
                if (!Logging.Logger.WillLog(category))
                {
                    _config.AddLogCat(category);
                }
            }
            
            Logging.UpdateLogger(_config);
            
            return this;
        }

        public SynthWishes DontLogCat(string category) => DontLogCats(category);
        public SynthWishes DontLogCats(params string[] categories) => DontLogCats((IList<string>)categories);
        public SynthWishes DontLogCats(IList<string> categories)
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
