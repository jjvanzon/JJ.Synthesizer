using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Wishes.Logging;

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

        public SynthWishes NoLogCat(string category)
        {
            _config.NoLogCat(category);
            Logging.UpdateLogger(_config);
            return this;
        }

        public SynthWishes NoLogCats(params string[] categories) 
        {
            _config.NoLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }
            
        public SynthWishes NoLogCats(IList<string> categories)
        {
            _config.NoLogCats(categories);
            Logging.UpdateLogger(_config);
            return this;
        }

    }
}
