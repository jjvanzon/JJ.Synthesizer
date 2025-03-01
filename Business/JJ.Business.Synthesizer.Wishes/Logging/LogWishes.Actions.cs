using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Framework.Wishes.Text;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Business.Synthesizer.Wishes.docs;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Text.StringWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public partial class LogWishes
    {
        public void LogAction(FlowNode entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Operator), action, entity.Name, message));
        }
        
        public void LogAction(Tape entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Tape), action, entity.Descriptor(), message));
        }

        /// <inheritdoc cref="_logtapeaction" />
        public void Log(TapeAction action, string message = null)
            => LogAction(action, message);
        
        public void LogAction(TapeAction action, string message = null)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            Log(ActionMessage("Action", action.Type, action.Tape.Descriptor(), message));
        }
        
        public void LogAction(Buff entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Buff), action, entity.Name, message));
        }
        
        public void LogAction(AudioFileOutput entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage("Out", action, entity.Name, message ?? ConfigLog(entity)));
        }
        
        public void LogAction(Sample entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(nameof(Sample), action, entity.Name, message ?? ConfigLog(entity)));
        }
        
        // ReSharper disable once UnusedParameter.Global
        public void LogAction(byte[] entity, string action, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage("Memory", action, "", message));
        }
        
        public void LogAction(object entity, string action, string name, string message = null)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            Log(ActionMessage(entity.GetType().Name, action, name, message));
        }
        
        public void LogAction<TEntity>(string message) 
            => Log(ActionMessage(typeof(TEntity).Name, null, null, message));
        
        public void LogAction<TEntity>(string action, string message) 
            => Log(ActionMessage(typeof(TEntity).Name, action, null, message));
        
        public void LogAction<TEntity>(string action, string name, string message) 
            => Log(ActionMessage(typeof(TEntity).Name, action, name, message));
        
        public void LogAction(string typeName, string message) 
            => Log(ActionMessage(typeName, null, null, message));
        
        public void LogAction(string typeName, string action, string message) 
            => Log(ActionMessage(typeName, action, null, message));
        
        public void LogAction(string typeName, string action, string name, string message) 
            => Log(ActionMessage(typeName, action, name, message));

        public string ActionMessage(string typeName, ActionEnum action, string name, string message)
            => ActionMessage(typeName, action.ToString(), name, message);
        
        public string ActionMessage(string typeName, string action, string name, string message)
        {
            string text = PrettyTime();
                
            if (Has(typeName))
            {
                text += " [" + typeName.ToUpper() + "]";
            }
            
            if (Has(action))
            {
                text += " " + action;
            }
            
            if (Has(name))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + '"' + name + '"';
            }
            
            if (Has(message))
            {
                if (!text.EndsWithPunctuation()) text += ":";
                text += " " + message;
            }
            
            return text;
        }
    }
}

// ReSharper disable once CheckNamespace
namespace JJ.Business.Synthesizer.Wishes
{
    public partial class SynthWishes
    {
        public void LogAction(FlowNode        entity, string action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Tape            entity, string action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(TapeAction      action,                             string message = null) => Logging.LogAction(action,                 message);
        /// <inheritdoc cref="_logtapeaction" />
        public void Log      (TapeAction      action,                             string message = null) => Logging.Log      (action,                 message);
        public void LogAction(Buff            entity, string action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(Sample          entity, string action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(AudioFileOutput entity, string action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(byte[]          entity, string action,              string message = null) => Logging.LogAction(entity,   action,       message);
        public void LogAction(object          entity, string action, string name, string message = null) => Logging.LogAction(entity,   action, name, message);
        public void LogAction(string typeName,                                    string message       ) => Logging.LogAction(typeName,               message);
        public void LogAction(string typeName,        string action,              string message       ) => Logging.LogAction(typeName, action,       message);
        public void LogAction(string typeName,        string action, string name, string message       ) => Logging.LogAction(typeName, action, name, message);
        public void LogAction<TEntity>(                                           string message       ) => Logging.LogAction<TEntity>(               message);
        public void LogAction<TEntity>(               string action,              string message       ) => Logging.LogAction<TEntity>( action,       message);
        public void LogAction<TEntity>(               string action, string name, string message       ) => Logging.LogAction<TEntity>( action, name, message);
    }
}

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static partial class LogExtensionWishes
    {
        public static void LogAction(this FlowNode        entity, string action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        public static void LogAction(this Tape            entity, string action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        /// <inheritdoc cref="_logtapeaction" />
        public static void Log      (this TapeAction      action,                string message = null) => action.Logging  .Log      (action,         message);
        public static void LogAction(this TapeAction      action,                string message = null) => action.Logging  .LogAction(action,         message);
        public static void LogAction(this Buff            entity, string action, string message = null) => entity.Logging  .LogAction(entity, action, message);
        public static void LogAction(this byte[]          entity, string action, string message = null) => Static          .LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, string action, string message = null) => entity.Logging().LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, string action, string message = null) => entity.Logging().LogAction(entity, action, message);
        public static void LogAction(this AudioFileOutput entity, SynthWishes synthWishes, string action, string message = null) => entity.Logging(synthWishes).LogAction(entity, action, message);
        public static void LogAction(this Sample          entity, SynthWishes synthWishes, string action, string message = null) => entity.Logging(synthWishes).LogAction(entity, action, message);
    }
}
